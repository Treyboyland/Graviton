using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagedSound : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip damagedSound;

    // Start is called before the first frame update
    void Start()
    {
        BaseGameManager.Manager.OnPlayerTakeDamage.AddListener(() =>
        {
            audioSource.PlayOneShot(damagedSound);
        }
        );
    }


}
