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

    public void SetSpriteAlpha(float alpha)
    {
        Color c = sprite.color;
        c.a = alpha;
        sprite.color = c;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(string.Compare(other.gameObject.tag, "Player", true) == 0)
        {
            //TODO: Find vector position of contact
            BaseGameManager.Manager.OnPlayerHitWall.Invoke(damaging);
        }
    }

    void OnValidate()
    {
        SetColor(damaging ? damagingColor : normalColor);
    }

    public void SetParameters(Wall wallInfo)
    {
        Vector3 scale = new Vector3(wallInfo.ScaleX, wallInfo.ScaleY, wallInfo.ScaleZ);
        Vector3 position = new Vector3(wallInfo.PosX, wallInfo.PosY, wallInfo.PosZ);
        transform.localPosition = position;
        transform.localScale = scale;
        damaging = wallInfo.IsDamaging;
    }

    public bool ContainsPoint(Vector3 point)
    {
        Debug.LogWarning(point);
        return collider.bounds.Contains(point);
    }
}
