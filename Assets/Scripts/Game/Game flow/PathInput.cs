using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathInput : MonoBehaviour
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
        ChangePath();

        if (Input.GetButtonDown("Submit"))
            Select();
    }

    void ChangePath()
    {
        int pathIndex;

        if (Input.GetKeyDown(KeyCode.A))
        {
            pathIndex = 0;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            pathIndex = 1;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            pathIndex = 2;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            pathIndex = 3;
        }
        else
        {
            return;
        }

        boardSelection.SelectPath(boardSelection.availablePaths[pathIndex]);
    }

    void Select()
    {
        if (actionController.CanExecute())
        {
            actionController.Execute();
            Disable();
        }
    }

    public void Enable(List<Tile> path)
    {
        if (boardSelection == null)
        {
            Start();
        }

        this.enabled = true;

        boardSelection.SelectPath(path);
    }

    public void Disable()
    {
        boardSelection.Clear();
        this.enabled = false;
    }
}
