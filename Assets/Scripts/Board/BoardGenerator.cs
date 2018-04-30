using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Obstacles;

public class BoardGenerator : MonoBehaviour
{
    BoardController board;

    [SerializeField]
    int boardWidth;
    [SerializeField]
    int boardHeight;

    [SerializeField]
    GameObject tilePrefabLight;

    [SerializeField]
    bool spawnOnSameSide = false;

    public List<Unit> units;

    private void Start()
    {
        GenerateBoard();
    }

    void GenerateBoard()
    {
        board = gameObject.GetComponent<BoardController>();

        board.tiles = new Tile[boardWidth, boardHeight];

        for (int x = 0; x < boardWidth; x++)
        {
            for (int y = 0; y < boardHeight; y++)
            {
                Tile tile = Instantiate(tilePrefabLight, transform).GetComponent<Tile>();
                tile.transform.Translate(new Vector3((x - boardWidth) * Tile.size, 0, (y - boardHeight) * Tile.size));
                tile.x = x; tile.y = y;

                board.tiles[x, y] = tile;
            }
        }

        board.Initiate();
        if(spawnOnSameSide)
        {
            SpawnUnitsSameSide();
        }
        else
        {
            SpawnUnitsOnOtherSides();
        }

        Destroy(this);
    }

    void SpawnUnitsOnOtherSides()
    {
        var players = GameController.instance.players;

        GameController.instance.boardController = board;

        for (int i = 0; i < players.Count; i++)
        {
            int xStartingTile = 0;
            int yStartingTile = 0;

            int xDirection = 0;
            int yDirection = 0;

            switch (i)
            {
                case 0:
                    xDirection = 1;
                    break;
                case 1:
                    xStartingTile = board.tiles.GetLength(0) - 1;
                    yStartingTile = 0;
                    yDirection = 1;
                    break;
                case 2:
                    xStartingTile = board.tiles.GetLength(0) - 1;
                    yStartingTile = board.tiles.GetLength(1) - 1;

                    xDirection = -1;
                    break;
                case 3:
                    yStartingTile = board.tiles.GetLength(1) - 1;
                    yDirection = -1;
                    break;
            }

            for (int j = 0; j < units.Count; j++)
            {
                int xPos = xStartingTile + (j * xDirection) * 2 + (j * xDirection) + (4 * xDirection);
                int yPos = yStartingTile + (j * yDirection) * 2 + (j * yDirection) + (4 * yDirection);

                Unit newUnit = Instantiate(units[j], transform);

                Tile targetTile = board.tiles[xPos, yPos];

                newUnit.transform.position = targetTile.transform.position;
                newUnit.occupiedTile = targetTile;
                targetTile.occupyingObstacle = newUnit;
                GameController.instance.players[i].boardInformation.units.Add(newUnit);

                targetTile.ChangeColor(newUnit.color);
            }
        }

        GameController.instance.roundController.Initiate();
    }

    void SpawnUnitsSameSide()
    {
        var players = GameController.instance.players;

        GameController.instance.boardController = board;

        for (int sideIndex = 0; sideIndex < 4; sideIndex++ )
        {
            int xStartingTile = 0;
            int yStartingTile = 0;

            int xDirection = 0;
            int yDirection = 0;

            switch (sideIndex)
            {
                case 0:
                    xDirection = 1;
                    break;
                case 1:
                    xStartingTile = board.tiles.GetLength(0) - 1;
                    yStartingTile = 0;
                    yDirection = 1;
                    break;
                case 2:
                    xStartingTile = board.tiles.GetLength(0) - 1;
                    yStartingTile = board.tiles.GetLength(1) - 1;

                    xDirection = -1;
                    break;
                case 3:
                    yStartingTile = board.tiles.GetLength(1) - 1;
                    yDirection = -1;
                    break;
            }

            for (int tileIndex = 0; tileIndex < board.tiles.GetLength(0); tileIndex++)
            {
                int xPos = xStartingTile + (tileIndex * xDirection);
                int yPos = yStartingTile + (tileIndex * yDirection);

                Tile targetTile = board.tiles[xPos, yPos];
                targetTile.ChangeColor(units[sideIndex].color);
            }

            for (int playerIndex = 0; playerIndex < players.Count; playerIndex++)
            {
                int xPos = xStartingTile + (playerIndex * xDirection) * 2 + (playerIndex * xDirection) + (4 * xDirection);
                int yPos = yStartingTile + (playerIndex * yDirection) * 2 + (playerIndex * yDirection) + (4 * yDirection);

                Unit newUnit = Instantiate(units[sideIndex], transform);

                Tile targetTile = board.tiles[xPos, yPos];

                newUnit.transform.position = targetTile.transform.position;
                newUnit.occupiedTile = targetTile;
                targetTile.occupyingObstacle = newUnit;
                GameController.instance.players[playerIndex].boardInformation.units.Add(newUnit);

                targetTile.ChangeColor(newUnit.color);
            }
        }

        GameController.instance.roundController.Initiate();
    }
}
