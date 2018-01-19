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

    public Unit.Color color = Unit.Color.Blank;

    public static float size = 1;

    public bool walkAble = true;

    public Unit occupyingUnit;

    private void Start()
    {
        //color = Unit.Color.Blank;
    }

    private void OnMouseDown()
    {

        /**/

        GameController.instance.roundController.ClickTile(this);
    }

    public bool CheckWalkAble(Unit unit)
    {
        //check if tile is wall
        if (!walkAble)
        {
            return false;
        }

        //check if color is valid for the player
        if (unit.color != color && color != Unit.Color.Blank)
        {
            return false;
        }

        return true;
    }

    public void ChangeColor(Unit.Color targetColor)
    {
        Material tileMaterial = GetComponentInChildren<Renderer>().material;
        switch (targetColor)
        {
            case Unit.Color.Blue:
                color = Unit.Color.Blue;
                tileMaterial.color = Color.blue;
                break;
            case Unit.Color.Red:
                color = Unit.Color.Red;
                tileMaterial.color = Color.red;
                break;
            case Unit.Color.Green:
                color = Unit.Color.Green;
                tileMaterial.color = Color.green;
                break;
        }
    }
}
