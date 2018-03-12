using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardSelection : MonoBehaviour
{
    [HideInInspector] public List<Tile> traversableTiles = new List<Tile>();

    [HideInInspector] public Tile selectedTile;

    public void Clear()
    {
        foreach (var tile in traversableTiles)
        {
            tile.UnHighLight();
        }
        traversableTiles.Clear();

        Debug.Log("Clear");
    }

    public void SetSelectable(List<Tile> tiles)
    {
        foreach(var tile in tiles)
        {
            traversableTiles.Add(tile);
            tile.HighLight();
        }
    }

    public void SelectTile(Tile tile)
    {
        if (selectedTile != null)
        {
            if (traversableTiles.Contains(selectedTile))
            {
                selectedTile.UnSelect();
                selectedTile.HighLight();
            }
            else
            {
                selectedTile.UnSelect();
                selectedTile.UnHighLight();
            }
        }
        selectedTile = tile;
        selectedTile.Select();
    }
}
