using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWall : MonoBehaviour
{
    /// <summary>
    /// True if the wall damages the player
    /// </summary>
    [SerializeField]
    bool damaging;

    /// <summary>
    /// Sprite representing a wall
    /// </summary>
    [SerializeField]
    SpriteRenderer sprite;

    /// <summary>
    /// Color for a wall when it damages the player
    /// </summary>
    [SerializeField]
    Color damagingColor;

    /// <summary>
    /// Color for a wall when it does not damage the player
    /// </summary>
    [SerializeField]
    Color normalColor;

    /// <summary>
    /// Collider component
    /// </summary>
    [SerializeField]
    new Collider2D collider;

    /// <summary>
    /// Collider component
    /// </summary>
    /// <value></value>
    public Collider2D Collider
    {
        get
        {
            return collider;
        }
    }

    /// <summary>
    /// True if this wall damages the player
    /// </summary>
    /// <value></value>
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

    /// <summary>
    /// True if this wall can be deleted in the editor
    /// </summary>
    [SerializeField]
    bool isDeletable = false;

    /// <summary>
    /// True if this wall can be deleted in the editor
    /// </summary>   
    public bool IsDeletable
    {
        get
        {
            return isDeletable;
        }
    }

    [SerializeField]
    GameWallAnchor anchor;

    /// <summary>
    /// If non null, and called within the editor, contains the anchor in control of this wall
    /// </summary>
    /// <value></value>
    public GameWallAnchor Anchor
    {
        get
        {
            return anchor;
        }
    }

    void Start()
    {
        SetColor(damaging ? damagingColor : normalColor);
    }

    /// <summary>
    /// Sets the color of the wall
    /// </summary>
    /// <param name="c"></param>
    void SetColor(Color c)
    {
        sprite.color = c;
    }

    /// <summary>
    /// Sets the alpha value of the walls color
    /// </summary>
    /// <param name="alpha"></param>
    public void SetSpriteAlpha(float alpha)
    {
        Color c = sprite.color;
        c.a = alpha;
        sprite.color = c;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (string.Compare(other.gameObject.tag, "Player", true) == 0)
        {
            //TODO: Find vector position of contact
            BaseGameManager.Manager.OnPlayerHitWall.Invoke(damaging);
        }
    }

    void OnValidate()
    {
        SetColor(damaging ? damagingColor : normalColor);
    }

    /// <summary>
    /// Sets all of the parameters for the wall (dimensions and damaging state)
    /// </summary>
    /// <param name="wallInfo"></param>
    public void SetParameters(Wall wallInfo)
    {
        Vector3 scale = new Vector3(wallInfo.ScaleX, wallInfo.ScaleY, wallInfo.ScaleZ);
        Vector3 position = new Vector3(wallInfo.PosX, wallInfo.PosY, wallInfo.PosZ);
        transform.localPosition = position;
        transform.localScale = scale;
        IsDamaging = wallInfo.IsDamaging;
    }

    /// <summary>
    /// True if the point is inside the bounds of the wall
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public bool ContainsPoint(Vector3 point)
    {
        return collider.bounds.Contains(point);
    }
}
