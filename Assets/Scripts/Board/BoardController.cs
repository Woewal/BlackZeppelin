using UnityEngine;
using System.Collections;
using Game.Unit;

public class BoardController : MonoBehaviour
{
    public static BoardController instance;

    public PathFinder pathFinder;

    public Unit unitPrefab;

    public Tile[,] tiles;

    private void Start()
    {
        instance = this;
        
    }

    public void Initiate()
    {
        pathFinder = new PathFinder(tiles);
        SpawnUnits();

        
    }

    void SpawnUnits()
    {
        var players = GameController.instance.players;

        GameController.instance.boardController = this;

        int h = 0;

        for (int i = 0; i < players.Count; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                Unit newUnit = Instantiate(unitPrefab, transform);
                newUnit.transform.position = tiles[h, 0].transform.position;
                newUnit.occupiedTile = tiles[h, 0];
                GameController.instance.players[i].boardInformation.units.Add(newUnit);
                h++;
            }
        }

        GameController.instance.roundController.Initiate();
    }
}
