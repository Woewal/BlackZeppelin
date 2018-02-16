using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Unit;

public class ActionController : MonoBehaviour
{
    public Tile selectedTile;

    GameController gameController;
    public Unit currentUnit;
    public Action CurrentAction
    {
        get
        {
            return currentUnit.actions[currentActionIndex];
        }
    }
    int currentActionIndex = 0;

    public List<Tile> traversableTiles = new List<Tile>();

    void Start()
    {
        gameController = GameController.instance;
    }

    private void Update()
    {
        MoveCursor();

        if (Input.GetKeyDown("space"))
            Select();
    }

    public void StartUnitTurn()
    {
        currentUnit = gameController.roundController.CurrentUnit;
        currentActionIndex = 0;

        if(CurrentAction.needsInput)
            SelectTile(currentUnit.occupiedTile);
        CurrentAction.StartAction();
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

    public void Clear()
    {
        foreach (var tile in traversableTiles)
        {
            tile.UnHighLight(false);
            tile.UnhighlightTraversable();
        }
        traversableTiles.Clear();
    }

    void Select()
    {
        if(CurrentAction.CanExecute())
        {
            StartCoroutine(ExecuteAction(CurrentAction.InvokeAction()));
        }
    }

    void NextAction()
    {
        currentActionIndex++;

        if (currentActionIndex >= currentUnit.actions.Count)
        {
            gameController.roundController.NextUnit();
        }
        else
        {
            if (CurrentAction.needsInput)
                SelectTile(currentUnit.occupiedTile);
            CurrentAction.StartAction();
        }
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

    public IEnumerator ExecuteAction(IEnumerator action)
    {
        Clear();

        yield return StartCoroutine(action);

        NextAction();
    }
}
