using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour
{
    public static Board instance;

    public Unit selectedUnit;

    public PathFinder pathFinder;

    public Tile[,] tiles;

    private void Start()
    {
        instance = this;
        
    }

    public void Initiate()
    {
        pathFinder = new PathFinder(tiles);
        selectedUnit = Instantiate(selectedUnit, transform);
        selectedUnit.transform.position = tiles[0, 0].transform.position;
        selectedUnit.occupiedTile = tiles[0, 0];
    }
}
