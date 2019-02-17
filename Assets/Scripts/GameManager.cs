using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseGameManager
{


    Player player;

    // Start is called before the first frame update
    void Start()
    {
        OnPointsReceived.AddListener((points) => {
            FindPlayer();
            player.Score = player.Score + points;
        });   
    }

    void FindPlayer()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
    }
}
