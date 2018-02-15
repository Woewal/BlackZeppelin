using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Unit;

public class RoundController : MonoBehaviour
{
    GameController gameController;
    GlobalController globalController;

    [HideInInspector] public Player currentPlayer;
    [HideInInspector] public Unit currentUnit;
    int playerTurnIndex;

    public void Initiate()
    {
        gameController = GetComponent<GameController>();
        globalController = GlobalController.instance;

        SetPlayerTurn(0);
        playerTurnIndex = 0;
    }

    public void SetPlayerTurn(int playerNumber)
    {
        currentPlayer = GameController.instance.players[playerNumber];
        currentPlayer.boardInformation.SetAvailableUnits();
        currentUnit = currentPlayer.boardInformation.availableUnits[0];
        GameController.instance.cameraController.target = currentUnit.gameObject;
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

    void Attack(Tile tile)
    {

    }
}
