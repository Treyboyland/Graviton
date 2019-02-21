using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseGameManager
{
    [SerializeField]
    float secondsOfInvincibility;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        // OnPointsReceived.AddListener((points) => {
        //     FindPlayer();
        //     player.Score = player.Score + points;
        // });   
        OnPlayerHitWall.AddListener(DamagePlayer);
    }

    void FindPlayer()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
    }

    void DamagePlayer(bool damaging)
    {
        Debug.Log("We were called");
        FindPlayer();
        if(!player.IsInvincible() && damaging)
        {
            OnGrantPlayerInvincibility.Invoke(secondsOfInvincibility);
        }
    }
}
