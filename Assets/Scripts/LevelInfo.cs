using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml;
using System.Linq;

[XmlRootAttribute("Level")]
public class LevelInfo
{
    [XmlAttribute("Name")]
    public string Name;

    [XmlElement("SpawnLocation")]
    public SpawnLocations PointSpawns;

    [XmlElement("Wall")]
    public Walls GameWalls;

    public LevelInfo(GameWallHolder holder)
    {
        Name = holder.LevelName;
        PointSpawns = holder.Locations;
        GameWalls = new Walls(holder.Walls.Length);
        for(int i = 0; i < holder.Walls.Length; i++)
        {
            GameWalls.Add(new Wall(holder.Walls[i]));
        }
    }

    public LevelInfo()
    {

    }
}


[XmlRootAttribute("Wall")]
public class Wall
{
    [XmlAttribute("PosX")]
    public float PosX;
    
    [XmlAttribute("PosY")]
    public float PosY;
    
    [XmlAttribute("PosZ")]
    public float PosZ;
    
    [XmlAttribute("ScaleX")]
    public float ScaleX;
    
    [XmlAttribute("ScaleY")]
    public float ScaleY;
    
    [XmlAttribute("ScaleZ")]
    public float ScaleZ;
    
    [XmlAttribute("IsDamaging")]
    public bool IsDamaging;

    public Wall(GameWall gameWall)
    {
        Vector3 pos = gameWall.transform.position;
        Vector3 scale = gameWall.transform.localScale;
        PosX = pos.x;
        PosY = pos.y;
        PosZ = pos.z;

        ScaleX = scale.x;
        ScaleY = scale.y;
        ScaleZ = scale.z;
        IsDamaging = gameWall.IsDamaging;
    }

    public Wall()
    {
        
    }
}

[XmlRootAttribute("Walls")]
public class Walls : List<Wall>
{
    public Walls() : base()
    {

    }

    public Walls(int capacity) : base(capacity)
    {

    }

    public Walls(List<Wall> walls) : base(walls)
    {

    }

    public Walls(IEnumerable<Wall> walls) : base(walls)
    {

    }
}

