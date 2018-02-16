using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Unit;

public class Tile : MonoBehaviour
{
    [HideInInspector] public int x;
    [HideInInspector] public int y;

    [SerializeField]
    GameObject highlightPrefab;
    [SerializeField]
    GameObject traversablePrefab;

    [HideInInspector]
    public int FStop
    {
        get
        {
            return gCost + hCost;
        }
    }
    [HideInInspector] public int gCost = 0;
    [HideInInspector] public int hCost = 0;
    [HideInInspector] public Tile parent;

    public Unit.Color color = Unit.Color.Blank;

    public static float size = 1;

    public bool walkAble = true;

    [HideInInspector] public Unit occupyingUnit;

    private void Start()
    {
        //color = Unit.Color.Blank;
    }

    private void OnMouseDown()
    {
        Debug.Log(occupyingUnit);
    }

    public bool CheckWalkAble(Unit unit)
    {
        if(occupyingUnit != null)
        {
            return false;
        }

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

        if(occupyingUnit != null)
        {
            if (occupyingUnit.color != targetColor)
            {
                occupyingUnit.KillUnit();
            }
        }
    }

    public void HighLight()
    {
        UnhighlightTraversable();
        highlightPrefab.SetActive(true);
    }

    public void UnHighLight(bool shouldHighlightTraversable)
    {
        highlightPrefab.SetActive(false);
        if (shouldHighlightTraversable)
            HighlightTraversable();
    }

    public void HighlightTraversable()
    {
        traversablePrefab.SetActive(true);
    }

    public void UnhighlightTraversable()
    {
        traversablePrefab.SetActive(false);
    }
}
