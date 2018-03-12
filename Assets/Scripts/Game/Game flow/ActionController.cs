using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Unit;

public class ActionController : MonoBehaviour
{
    BoardSelection boardSelection;
    CursorInput cursorInput;

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
    }

    public void StartUnitTurn()
    {
        currentUnit = gameController.roundController.currentUnit;
        currentActionIndex = 0;

        if (CurrentAction.needsInput)
        {
            cursorInput.Enable(currentUnit.occupiedTile);
            //boardSelection.SetSelectable(CurrentAction)
        }
            
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
        if (CurrentAction.CanExecute())
        {
            StartCoroutine(ExecuteAction(CurrentAction.InvokeAction()));
        }
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
            if (CurrentAction.needsInput)
                cursorInput.Enable(currentUnit.occupiedTile);
            CurrentAction.StartAction();
        }
    }

    public IEnumerator ExecuteAction(IEnumerator action)
    {
        yield return StartCoroutine(action);

        NextAction();
    }
}
