using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Obstacles;

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

    public Obstacle.Color color = Obstacle.Color.Blank;

    public static float size = 1;

    public bool walkAble = true;

    [HideInInspector] public Obstacle occupyingObstacle;

    private void Start()
    {
        //color = Unit.Color.Blank;
    }

    private void OnMouseDown()
    {
        Debug.Log(occupyingObstacle);
    }

    public bool CheckWalkAble(Unit unit)
    {
        if(occupyingObstacle != null)
        {
            return false;
        }

        //check if tile is wall
        if (!walkAble)
        {
            return false;
        }

        //check if color is valid for the player
        if (unit.color != color)
        {
            return false;
        }

        if(color == Obstacle.Color.Blank)
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
                tileMaterial.color = UnityEngine.Color.blue;
                break;
            case Unit.Color.Red:
                color = Unit.Color.Red;
                tileMaterial.color = UnityEngine.Color.red;
                break;
            case Unit.Color.Green:
                color = Unit.Color.Green;
                tileMaterial.color = UnityEngine.Color.green;
                break;
            case Unit.Color.Yellow:
                color = Unit.Color.Yellow;
                tileMaterial.color = UnityEngine.Color.yellow;
                break;
        }

        if(occupyingObstacle != null)
        {
            if(occupyingObstacle.color != targetColor)
            {
                occupyingObstacle.DestroyObstacle();
            }
        }
    }

    public void Select()
    {
        highlightPrefab.SetActive(true);
    }

    public void UnSelect()
    {
        highlightPrefab.SetActive(false);
    }

    public void HighLight()
    {
        traversablePrefab.SetActive(true);
    }

    public void UnHighLight()
    {
        traversablePrefab.SetActive(false);
    }

    public static Tile GetTile(int row, int column)
    {
        var tiles = BoardController.instance.tiles;

        if (row < 0 || column < 0 || row >= tiles.GetLength(0) || column >= tiles.GetLength(1))
        {
            return null;
        }
        else
        {
            return tiles[row, column];
        }
    }
}
