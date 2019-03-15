using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml;

/// <summary>
/// Puts a Vector2 or Vector3 into a format that is
/// serializable into XML
/// </summary>
[XmlRootAttribute("SpawnLocation")]
public struct SpawnLocation
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
    public SpawnLocation(Vector3 v)
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
    public SpawnLocation(Vector2 v)
    {
        x = v.x;
        y = v.y;
        z = 0;
    }
}

[XmlRootAttribute("SpawnLocations")]
public class SpawnLocations : List<SpawnLocation>
{
    public SpawnLocations() : base() { }
    public SpawnLocations(List<SpawnLocation> loc) : base(loc) { }
}