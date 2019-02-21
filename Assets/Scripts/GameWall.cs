using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWall : MonoBehaviour
{
    [SerializeField]
    bool damaging;

    [SerializeField]
    SpriteRenderer sprite;

    [SerializeField]
    Color damagingColor;

    [SerializeField]
    Color normalColor;

    void Start()
    {
        SetColor(damaging ? damagingColor : normalColor);
    }

    void SetColor(Color c)
    {
        sprite.color = c;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(string.Compare(other.gameObject.tag, "Player", true) == 0)
        {
            Debug.LogWarning("Boop!");
            BaseGameManager.Manager.OnPlayerHitWall.Invoke(damaging);
        }
    }
}
