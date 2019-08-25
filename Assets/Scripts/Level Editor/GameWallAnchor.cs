using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWallAnchor : MonoBehaviour
{
    /// <summary>
    /// Game Wall to be anchored by this anchor
    /// </summary>
    [SerializeField]
    GameWall gameWall;

    public GameWall GameWall
    {
        get
        {
            return gameWall;
        }
    }

    /// <summary>
    /// Level that the game wall should be parented to on destruction
    /// </summary>
    GameLevel gameLevel;

    /// <summary>
    /// Level that the game wall should be parented to on destruction
    /// </summary>
    public GameLevel GameLevel
    {
        get
        {
            return gameLevel;
        }
        set
        {
            gameLevel = value;
        }
    }

    public Vector3 LocalScale
    {
        get
        {
            return transform.localScale;
        }
        set
        {
            transform.localScale = value;
        }
    }

    public ReticleController Reticle { get; set; }

    public bool ShouldTrack { get; set; }

    public bool ShouldScale { get; set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Reticle != null && ShouldTrack)
        {
            transform.position = Reticle.transform.position;
        }
        if (ShouldScale)
        {
            SetScale();
        }
    }

    public void ParentWallToLevel()
    {
        gameLevel.transform.SetParent(gameLevel.transform);
    }

    public void ParentWallToAnchor()
    {
        gameLevel.transform.SetParent(transform);
    }

    void SetScale()
    {
        float distanceX = Reticle.transform.position.x - transform.position.x;
        float distanceY = Reticle.transform.position.y - transform.position.y;

        Vector3 scale = transform.localScale;

        if ((distanceX < 0 && !(scale.x < 0)) ||
            (distanceX > 0 && scale.x < 0))
        {
            scale.x *= -1;
        }
        if ((distanceY < 0 && scale.y < 0) ||
        (distanceY > 0 && !(scale.y < 0)))
        {
            scale.y *= -1;
        }

        if (distanceX < .5f)
        {

        }
        if (distanceY < .5f)
        {

        }

        scale.x = Mathf.Sign(scale.x) * (Mathf.Abs(distanceX) < .5 ? .5f : Mathf.Abs(distanceX));
        scale.y = Mathf.Sign(scale.y) * (Mathf.Abs(distanceY) < .5 ? .5f : Mathf.Abs(distanceY));

        transform.localScale = scale;
    }
}
