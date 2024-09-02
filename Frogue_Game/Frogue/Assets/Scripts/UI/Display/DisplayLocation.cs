using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLocation : MonoBehaviour
{
    private const int GRID_SIZE = 4;
    [SerializeField] GridLayoutGroup grid;
    [SerializeField] GameObject dotPrefab;

    public void Display(Ability ability)
    {
        foreach (Transform t in grid.transform)
            Destroy(t.gameObject);

        bool[,] filledDots = LocationWho.GetFilledDots(ability);
        for (int x = 0; x < GRID_SIZE; x++)
        {
            for (int y = 0; y < GRID_SIZE; y++)
            {
                GameObject whoDot = Instantiate(dotPrefab, grid.transform);
                Image image = whoDot.GetComponent<Image>();
                image.color = Color.white;

                foreach (LocationWho locationWho in ability.LocationWhos)
                    if (filledDots[x, y])
                        image.color = Color.black;
            }
        }
    }
}