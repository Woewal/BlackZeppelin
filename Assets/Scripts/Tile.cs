using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour
{
    public int x;
    public int y;

    public int FStop
    {
        get
        {
            return gCost + hCost;
        }
    }
    public int gCost = 0;
    public int hCost = 0;
    public Tile parent;

    public static float size = 1;

    public bool walkAble = true;

    private void OnMouseDown()
    {
        Unit unit = Board.instance.selectedUnit;

        List<Tile> tilesToDestination = Board.instance.pathFinder.GetPath(new Vector2(unit.occupiedTile.x,unit.occupiedTile.y), new Vector2(x, y), unit);
        StartCoroutine(unit.Move(tilesToDestination));
    }

    public bool CheckWalkAble(Unit unit)
    {
        //check if tile is wall
        if (!walkAble)
        {
            return false;
        }

        //check if color is valid for the player

        return true;
    }

    /*private void OnMouseEnter()
    {
        Unit unit = Board.instance.selectedUnit;

        List<Tile> path = Board.instance.pathFinder.GetPath(new Vector2(unit.occupiedTile.x, unit.occupiedTile.y), new Vector2(x, y));
        foreach(Tile tile in path)
        {
            tile.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y - 0.1f, tile.transform.position.z);
        }
    }*/

    /*private void OnMouseExit()
    {
        foreach (Tile tile in Board.instance.tiles)
        {
            tile.transform.position = new Vector3(tile.transform.position.x, 0, tile.transform.position.z);
        }
    }*/

    
}
