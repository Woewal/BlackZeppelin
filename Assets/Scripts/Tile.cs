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

    public Unit occupyingUnit;

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

    public void ChangeColor(Unit.Color color)
    {
        Material tileMaterial = GetComponentInChildren<Renderer>().material;
        switch (color)
        {
            case Unit.Color.Blue:
                tileMaterial.color = Color.blue;
                break;
            case Unit.Color.Red:
                tileMaterial.color = Color.red;
                break;
            case Unit.Color.Green:
                tileMaterial.color = Color.green;
                break;
        }
    }
}
