﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Obstacles;

public class RoundController : MonoBehaviour
{
    GameController gameController;
    GlobalController globalController;
    UnitSelectInput unitSelectInput;

    [HideInInspector] public Player CurrentPlayer
    {
        get
        {
            return gameController.players[playerTurnIndex];
        }
    }
    public Unit currentUnit;

    int playerTurnIndex;
    int unitTurnIndex;

    public void Initiate()
    {
        gameController = GetComponent<GameController>();
        globalController = GlobalController.instance;
        unitSelectInput = GetComponent<UnitSelectInput>();

        SetPlayerTurn(0);
        playerTurnIndex = 0;
    }

    public void SetPlayerTurn(int playerNumber)
    {
        unitTurnIndex = 0;
        CurrentPlayer.boardInformation.RemoveEmpty();
        unitSelectInput.Enable(CurrentPlayer.boardInformation.units);
    }

    public void NextTurn()
    {
        playerTurnIndex++;
        if (playerTurnIndex > GameController.instance.players.Count - 1)
        {
            playerTurnIndex = 0;
        }
        SetPlayerTurn(playerTurnIndex);
    }

    //public void NextUnit()
    //{
    //    unitTurnIndex++;

    //    CurrentPlayer.boardInformation.RemoveEmpty();

    //    if (unitTurnIndex >= CurrentPlayer.boardInformation.units.Count)
    //    {
    //        NextTurn();
    //    }

    //    if(CurrentUnit == null)
    //    {
    //        unitTurnIndex++;
    //        NextUnit();
    //    }

    //    GameController.instance.cameraController.target = CurrentUnit.gameObject;
    //    GameController.instance.actionController.StartUnitTurn();
    //}
}
