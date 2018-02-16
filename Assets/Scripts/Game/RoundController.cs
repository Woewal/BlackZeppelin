using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Unit;

public class RoundController : MonoBehaviour
{
    GameController gameController;
    GlobalController globalController;

    [HideInInspector] public Player CurrentPlayer
    {
        get
        {
            return gameController.players[playerTurnIndex];
        }
    }
    [HideInInspector] public Unit CurrentUnit
    {
        get
        {
            var unit = CurrentPlayer.boardInformation.units[unitTurnIndex];

            if (unit == null)
            {
                CurrentPlayer.boardInformation.units.Remove(unit);
                return null;
            }

            return CurrentPlayer.boardInformation.units[unitTurnIndex];
        }
    }
    int playerTurnIndex;
    int unitTurnIndex;

    public void Initiate()
    {
        gameController = GetComponent<GameController>();
        globalController = GlobalController.instance;

        SetPlayerTurn(0);
        playerTurnIndex = 0;
    }

    public void SetPlayerTurn(int playerNumber)
    {
        unitTurnIndex = 0;
        GameController.instance.cameraController.target = CurrentUnit.gameObject;
        GameController.instance.actionController.StartUnitTurn();
    }

    public void NextTurn()
    {
        playerTurnIndex++;
        if (playerTurnIndex >= GameController.instance.players.Count)
        {
            playerTurnIndex = 0;
        }
        SetPlayerTurn(playerTurnIndex);
    }

    public void NextUnit()
    {
        unitTurnIndex++;

        if (unitTurnIndex >= CurrentPlayer.boardInformation.units.Count)
        {
            NextTurn();
        }

        if(CurrentUnit == null)
        {
            unitTurnIndex--;
            NextUnit();
        }

        GameController.instance.cameraController.target = CurrentUnit.gameObject;
        GameController.instance.actionController.StartUnitTurn();
    }
}
