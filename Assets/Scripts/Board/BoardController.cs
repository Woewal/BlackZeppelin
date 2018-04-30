using UnityEngine;
using System.Collections;
using Game.Obstacles;

public class BoardController : MonoBehaviour
{
    public static BoardController instance;

    public PathFinder pathFinder;

    public Tile[,] tiles;

    private void Start()
    {
        instance = this;
        
    }

    //Creates a pathfinder object that can be used to find the shortest path between two tiles.
    public void Initiate()
    {
        pathFinder = new PathFinder(tiles);
    }
}
