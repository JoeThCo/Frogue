using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool UseRandomSeed = true;
    [SerializeField] private int Seed = 0;

    [Header("Player")]
    [SerializeField] DisplayBoard playerDisplayBoard;
    [SerializeField] PlayerBoardModify playerBoardModify;
    private Board player;

    [Header("Baddie")]
    [SerializeField, Range(100, 10000)] int randomBoardCount = 5000;
    [SerializeField] DisplayBoard baddieDisplayBoard;
    private Board baddie;

    public const string BADDIE_TAG = "Baddie";
    public const string PLAYER_TAG = "Player";
    public static System.Random Random { get; private set; }

    public static Camera MainCamera { get; private set; }

    public void Awake()
    {
        if (UseRandomSeed)
            Random = new System.Random(Seed);
        else
            Random = new System.Random();

        MainCamera = Camera.main;

        //player init
        player = new Board(true);
        playerDisplayBoard.DisplayBoardInit(player);
        playerBoardModify.PlayerBoardModifyInit(player, playerDisplayBoard);
        player.Add(5);

        //baddie init
        baddie = new Board(false);
        baddieDisplayBoard.DisplayBoardInit(baddie);
        baddie.Add(5);
    }

    public void Battle()
    {
        StartCoroutine(BattleI());
    }

    private IEnumerator BattleI()
    {
        Debug.LogError("Battle Start!");

        Board lowestBoard = GetLowestBoard();
        yield return baddieDisplayBoard.DisplaySwaps(lowestBoard);

        RealBattle realBattle = new RealBattle(player, lowestBoard);
        Debug.LogWarning($"+/-: {realBattle.PlusMinus}");

        yield return realBattle.DisplayBattle();

        Debug.LogError("Battle End!");
    }

    // move this?
    private Board GetLowestBoard()
    {
        ConcurrentDictionary<int, Board> scores = new ConcurrentDictionary<int, Board>();

        Parallel.For(0, randomBoardCount, i =>
        {
            SimulateBattle test = new SimulateBattle(player, baddie, i);
            if (!scores.ContainsKey(test.PlusMinus)) scores.TryAdd(test.PlusMinus, test.BaddieBoard);
        });

        int lowestScore = scores.Keys.Min();
        return new Board(scores[lowestScore].Clone());
    }

    public static void SetInteractableTag(GameObject gameObject, bool isPlayerInteractable)
    {
        gameObject.tag = isPlayerInteractable ? PLAYER_TAG : BADDIE_TAG;
    }
}