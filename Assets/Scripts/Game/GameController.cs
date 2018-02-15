using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [HideInInspector] public RoundController roundController;
    [HideInInspector] public CameraController cameraController;
    [HideInInspector] public MovementController movementController;
    [HideInInspector] public BoardController boardController;
    public static GameController instance;

    public List<Player> players = new List<Player>();

    private void Awake()
    {
        instance = this;
        roundController = GetComponent<RoundController>();
        movementController = GetComponent<MovementController>();

        for (int i = 0; i < 4; i++)
        {
            players.Add(new Player());
        }
    }

}
