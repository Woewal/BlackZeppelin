using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoundController : MonoBehaviour
{
    enum Phase { Movement, Attack }

    GameController gameController;
    GlobalController globalController;

    Player currentPlayer;
    Unit selectedUnit;
    int playerTurnIndex;

    Phase unitPhase;

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
        selectedUnit = currentPlayer.boardInformation.availableUnits[0];
    }

    public void ClickTile(Tile tile)
    {
        if(unitPhase == Phase.Movement)
        {
            MoveToTile(tile);
        }
        else if (unitPhase == Phase.Attack)
        {
            Attack(tile);
        }
    }

    void MoveToTile(Tile tile)
    {
        selectedUnit.MoveToTile(tile);
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
