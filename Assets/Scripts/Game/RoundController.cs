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
        CurrentPlayer.boardInformation.SetAvailableUnits();
        GameController.instance.cameraController.target = CurrentUnit.gameObject;
        GameController.instance.movementController.Enable();
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

        GameController.instance.cameraController.target = CurrentUnit.gameObject;
        GameController.instance.movementController.Enable();
    }
}
