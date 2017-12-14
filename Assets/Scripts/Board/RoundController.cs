using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoundController : MonoBehaviour
{
    int playerTurn;

    public static RoundController instance;
    public GameController gameController;

    Player currentPlayer;
    Unit selectedUnit;

    private void Start()
    {
        instance = this;
        gameController = GameController.instance;
    }

    public void SetPlayerTurn(int playerNumber)
    {
        currentPlayer = GameController.instance.players[playerNumber];
        currentPlayer.boardInformation.SetAvailableUnits();
        selectedUnit = currentPlayer.boardInformation.availableUnits[0];
    }

}
