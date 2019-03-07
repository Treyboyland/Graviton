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
        SceneLoader.Loader.OnSceneChanged.AddListener(DestroyIfOnMain);
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
        FindPlayer();
        if(!player.IsInvincible() && damaging)
        {
            OnGrantPlayerInvincibility.Invoke(secondsOfInvincibility);
            OnPlayerTakeDamage.Invoke();
            OnResetPlayerCombo.Invoke();
        }
    }

    /// <summary>
    /// Destroys the game object it is on the main screen
    /// </summary>
    void DestroyIfOnMain()
    {
        if(SceneLoader.Loader != null && SceneLoader.Loader.IsLoadingSceneLoaded())
        {
            Destroy(gameObject);
        }
    }
}
