using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [HideInInspector] public RoundController roundController;
    [HideInInspector] public CameraController cameraController;
    [HideInInspector] public ActionController actionController;
    [HideInInspector] public BoardController boardController;
    [HideInInspector] public CursorInput cursor;
    public static GameController instance;

    public List<Player> players = new List<Player>();

    private void Awake()
    {
        instance = this;
        roundController = GetComponent<RoundController>();
        actionController = GetComponent<ActionController>();
        cursor = GetComponent<CursorInput>();

        for (int i = 0; i < 4; i++)
        {
            players.Add(new Player());
        }
    }

}
