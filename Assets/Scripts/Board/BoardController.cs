using UnityEngine;
using System.Collections;
using Game.Unit;

public class BoardController : MonoBehaviour
{
    public static BoardController instance;

    public PathFinder pathFinder;

    public Tile[,] tiles;

    private void Start()
    {
        instance = this;
        
    }

    public void Initiate()
    {
        pathFinder = new PathFinder(tiles);
    }
}
