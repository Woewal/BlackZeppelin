using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardSelection : MonoBehaviour
{
    [HideInInspector] public List<Tile> traversableTiles = new List<Tile>();
    [HideInInspector] public List<List<Tile>> availablePaths = new List<List<Tile>>();

    [HideInInspector] public Tile selectedTile;
    [HideInInspector] public List<Tile> selectedPath;

    public void Clear()
    {
        foreach (var tile in traversableTiles)
        {
            tile.UnHighLight();
        }
        selectedTile.UnSelect();
        traversableTiles.Clear();

        foreach(var path in availablePaths)
        {
            if (path != null)
            {
                foreach (var tile in path)
                {
                    tile.UnHighLight();
                    tile.UnSelect();
                }
            }
        }
        selectedPath = null;
        availablePaths.Clear();
    }

    public void SetSelectableTiles(List<Tile> tiles)
    {
        foreach(var tile in tiles)
        {
            traversableTiles.Add(tile);
            tile.HighLight();
        }
    }

    public void SetSelectablePaths(List<List<Tile>> paths)
    {
        foreach(var path in paths)
        {
            availablePaths.Add(path);

            if(path != null)
            {
                foreach (var tile in path)
                {
                    tile.HighLight();
                }
            }            
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

    public void SelectPath(int index)
    {
        if(selectedPath != null)
        {
            foreach(var tile in selectedPath)
            {
                tile.UnSelect();
            }
        }

        if(availablePaths[index] != null)
        {
            selectedPath = availablePaths[index];

            foreach (var tile in selectedPath)
            {
                tile.Select();
            }
        }
    }
}
