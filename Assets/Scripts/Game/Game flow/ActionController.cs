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

    bool acceptInput = false;

    GameController gameController;
    [HideInInspector] public Unit currentUnit;
    public Action CurrentAction
    {
        get
        {
            return currentUnit.actions[currentActionIndex];
        }
    }
    List<Action> usedActions = new List<Action>();
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
        usedActions.Clear();
        currentUnit = gameController.roundController.currentUnit;
        
        for(int i = 0; i < currentUnit.actions.Count; i++)
        {
            if(!usedActions.Contains(currentUnit.actions[i]))
                Debug.Log("Press " + i + " to use " + currentUnit.actions[i].name);
        }

        acceptInput = true;
    }

    public void SelectAction(int actionIndex)
    {
        currentActionIndex = actionIndex;

        CheckInput();

        CurrentAction.StartAction();

        acceptInput = false;
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

    private void Update()
    {
        if(acceptInput)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SelectAction(0);
            }
            else if(Input.GetKeyDown(KeyCode.E))
            {
                SelectAction(1);
            }
        }
    }

    public void Execute()
    {
        usedActions.Add(CurrentAction);
        StartCoroutine(ExecuteAction(CurrentAction.InvokeAction()));
    }

    public void NextAction()
    {
        if(usedActions.Count != currentUnit.actions.Count)
        {
            for (int i = 0; i < currentUnit.actions.Count; i++)
            {
                if (!usedActions.Contains(currentUnit.actions[i]))
                    Debug.Log("Press " + i + " to use " + currentUnit.actions[i].name);
            }
            acceptInput = true;
        }
        else
        {
            gameController.roundController.NextTurn();
        }

        //if (currentActionIndex >= currentUnit.actions.Count)
        //{
        //    gameController.roundController.NextTurn();
        //}
        //else
        //{
        //    CheckInput();
        //    CurrentAction.StartAction();
        //}
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
