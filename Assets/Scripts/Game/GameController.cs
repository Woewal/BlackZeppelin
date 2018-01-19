using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public RoundController roundController;
    public static GameController instance;

    public List<Player> players = new List<Player>();

    private void Awake()
    {
        instance = this;
        roundController = GetComponent<RoundController>();

        for (int i = 0; i < 3; i++)
        {
            players.Add(new Player());
        }
    }

}
