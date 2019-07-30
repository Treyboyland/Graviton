using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

[XmlRootAttribute("PlayerSpawn")]
public struct PlayerSpawn
{
    /// <summary>
    /// The X coordinate
    /// </summary>
    [XmlAttribute("X")]
    public float x;
    /// <summary>
    /// The Y coordinate
    /// </summary>
    [XmlAttribute("Y")]
    public float y;
    [XmlAttribute("Z")]
    public float z;


    /// <summary>
    /// Sets x, y, and z to values given by the Vector3
    /// </summary>
    /// <param name="v"></param>
    public PlayerSpawn(Vector3 v)
    {
        x = v.x;
        y = v.y;
        z = v.z;
    }

    /// <summary>
    /// Sets x and y to values given by the Vector2, and z
    /// to 0
    /// </summary>
    /// <param name="v"></param>
    public PlayerSpawn(Vector2 v)
    {
        x = v.x;
        y = v.y;
        z = 0;
    }

    public static implicit operator Vector3(PlayerSpawn x)
    {
        return new Vector3(x.x, x.y, x.z);
    }

    public static implicit operator PlayerSpawn(Vector3 x)
    {
        return new PlayerSpawn(x);
    }
}
