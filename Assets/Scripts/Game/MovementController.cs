using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Unit;

public class MovementController : MonoBehaviour
{
    Tile selectedTile;

    GameController gameController;
    Unit unit;

    List<Tile> traversableTiles = new List<Tile>();

    void Start()
    {
        gameController = GameController.instance;
        Disable();
    }

    private void Update()
    {
        MoveCursor();

        if (Input.GetKeyDown("space"))
            Select();
    }

    public void Enable()
    {
        unit = gameController.roundController.CurrentUnit;
        CheckTiles();
        SelectTile(unit.occupiedTile);
        enabled = true;
    }

    void SelectTile(Tile tile)
    {
        if (selectedTile != null)
        {
            if (traversableTiles.Contains(selectedTile))
            {
                selectedTile.UnHighLight(true);
            }
            else
            {
                selectedTile.UnHighLight(false);
            }
        }
        selectedTile = tile;
        selectedTile.HighLight();
    }

    public void Disable()
    {
        foreach (var tile in traversableTiles)
        {
            tile.UnHighLight(false);
            tile.UnhighlightTraversable();
        }
        traversableTiles.Clear();
        enabled = false;
    }

    void CheckTiles()
    {
        Tile[,] tiles = gameController.boardController.tiles;

        foreach (Movement.Direction direction in unit.movement.directions)
        {

            if (
                unit.occupiedTile.x + direction.x < 0 ||
                unit.occupiedTile.y + direction.y < 0 ||
                unit.occupiedTile.x + direction.x >= tiles.GetLength(0) ||
                unit.occupiedTile.y + direction.y >= tiles.GetLength(1)
            )
            {
                continue;
            }

            Tile tile = tiles[unit.occupiedTile.x + direction.x, unit.occupiedTile.y + direction.y];

            if (!tile.CheckWalkAble(unit))
            {
                continue;
            }

            tile.HighlightTraversable();
            traversableTiles.Add(tile);
        }
    }

    void Select()
    {
        if (!traversableTiles.Contains(selectedTile) || !selectedTile.CheckWalkAble(unit))
            return;

        StartCoroutine(MoveCharacter(selectedTile));
        Disable();
        
    }

    void MoveCursor()
    {
        Vector2 direction = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.A))
        {
            direction = new Vector2(-1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            direction = new Vector2(0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = new Vector2(1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = new Vector2(0, -1);
        }
        else
        {
            return;
        }

        if (
            selectedTile.x + (int)direction.x < 0 ||
            selectedTile.y + (int)direction.y < 0 ||
            selectedTile.x + (int)direction.x >= gameController.boardController.tiles.GetLength(0) ||
            selectedTile.y + (int)direction.y >= gameController.boardController.tiles.GetLength(1)
        )
        {
            return;
        }

        Tile tile = gameController.boardController.tiles[selectedTile.x + (int)direction.x, selectedTile.y + (int)direction.y];

        SelectTile(tile);
    }

    IEnumerator MoveCharacter(Tile destinationTile)
    {
        yield return StartCoroutine(unit.MoveToTile(destinationTile));

        yield return new WaitForSeconds(0.5f);

        gameController.roundController.NextUnit();
    }
}
