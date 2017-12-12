using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour
{
    public static Board instance;

    public Unit selectedUnit;

    public AStar pathFinder;

    public Tile[,] tiles;

    private void Start()
    {
        instance = this;
        
    }

    public void Initiate()
    {
        pathFinder = new AStar(tiles);
        selectedUnit = Instantiate(selectedUnit, transform);
        selectedUnit.transform.position = tiles[0, 0].transform.position;
        selectedUnit.occupiedTile = tiles[0, 0];
    }
}
