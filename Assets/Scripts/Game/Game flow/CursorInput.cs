using UnityEngine;
using System.Collections;

public class CursorInput : MonoBehaviour
{
    RoundController roundController;
    ActionController actionController;
    BoardSelection boardSelection;
    GameController gameController;

    private void Start()
    {
        roundController = GameController.instance.roundController;
        actionController = GameController.instance.actionController;
        gameController = GetComponent<GameController>();
        boardSelection = GetComponent<BoardSelection>();
    }

    private void Update()
    {
        MoveCursor();

        if (Input.GetButtonDown("Submit"))
            Select();
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
            boardSelection.selectedTile.x + (int)direction.x < 0 ||
            boardSelection.selectedTile.y + (int)direction.y < 0 ||
            boardSelection.selectedTile.x + (int)direction.x >= gameController.boardController.tiles.GetLength(0) ||
            boardSelection.selectedTile.y + (int)direction.y >= gameController.boardController.tiles.GetLength(1)
        )
        {
            return;
        }

        Tile tile = gameController.boardController.tiles[boardSelection.selectedTile.x + (int)direction.x, boardSelection.selectedTile.y + (int)direction.y];

        boardSelection.SelectTile(tile);
    }

    void Select()
    {
        if(actionController.CanExecute())
        {
            actionController.Execute();
            Debug.Log("execute");
            Disable();
        }
    }

    public void Enable(Tile beginTile)
    {
        if(boardSelection == null)
        {
            Start();
        }

        this.enabled = true;

        boardSelection.SelectTile(beginTile);
    }

    public void Disable()
    {
        boardSelection.Clear();
        this.enabled = false;
    }
}
