using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Obstacles;
using Game.Actions;

public class ActionController : MonoBehaviour
{
    BoardSelection boardSelection;
    CursorInput cursorInput;
    PathInput pathInput;

    GameController gameController;
    [HideInInspector] public Unit currentUnit;
    public Action CurrentAction
    {
        get
        {
            return currentUnit.actions[currentActionIndex];
        }
    }
    int currentActionIndex = 0;

    void Start()
    {
        gameController = GameController.instance;
        boardSelection = GetComponent<BoardSelection>();
        cursorInput = GetComponent<CursorInput>();
        pathInput = GetComponent<PathInput>();
    }

    public void StartUnitTurn()
    {
        currentUnit = gameController.roundController.currentUnit;
        currentActionIndex = 0;

        CheckInput();
            
        CurrentAction.StartAction();

    }

    public bool CanExecute()
    {
        if(boardSelection.traversableTiles.Contains(boardSelection.selectedTile))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Execute()
    {
        StartCoroutine(ExecuteAction(CurrentAction.InvokeAction()));
    }

    public void NextAction()
    {
        currentActionIndex++;

        if (currentActionIndex >= currentUnit.actions.Count)
        {
            gameController.roundController.NextTurn();
        }
        else
        {
            CheckInput();
            CurrentAction.StartAction();
        }
    }

    private void CheckInput()
    {
        if (CurrentAction.inputType == Action.InputType.Cursor)
        {
            cursorInput.Enable(currentUnit.occupiedTile);
            boardSelection.SetSelectableTiles(CurrentAction.GetDirections());
        }
        else if (CurrentAction.inputType == Action.InputType.Path)
        {
            var paths = CurrentAction.GetPaths();

            boardSelection.SetSelectablePaths(paths);
            pathInput.Enable(paths[0]);
        }
    }

    public IEnumerator ExecuteAction(IEnumerator action)
    {
        yield return StartCoroutine(action);

        NextAction();
    }
}
