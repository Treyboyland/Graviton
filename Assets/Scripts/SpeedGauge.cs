using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedGauge : MonoBehaviour
{
    [SerializeField]
    Image image;

    [SerializeField]
    Player player;


    // Start is called before the first frame update
    void Start()
    {
        BaseGameManager.Manager.OnPlayerSpeedUpdated.AddListener(ChangeGauge);
    }

    void ChangeGauge(float speed)
    {
        image.fillAmount = 1 - (player.Speed - player.MinSpeed) / (player.MaxSpeed - player.MinSpeed);
    }
}
