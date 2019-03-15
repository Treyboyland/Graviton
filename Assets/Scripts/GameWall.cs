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

    [SerializeField]
    new Collider2D collider;

    public Collider2D Collider
    {
        get
        {
            return collider;
        }
    }

    public bool IsDamaging
    {
        get
        {
            return damaging;
        }
        set
        {
            damaging = value;
            SetColor(damaging ? damagingColor : normalColor);    
        }
    }

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
            BaseGameManager.Manager.OnPlayerHitWall.Invoke(damaging);
        }
    }

    void OnValidate()
    {
        SetColor(damaging ? damagingColor : normalColor);
    }
}
