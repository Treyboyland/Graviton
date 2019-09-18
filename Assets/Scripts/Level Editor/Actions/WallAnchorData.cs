using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains position and scale data for a wall
/// </summary>
public struct WallAnchorData
{
    /// <summary>
    /// Local anchor scale
    /// </summary>
    public Vector3 LocalScale;
    /// <summary>
    /// Anchor position in world space
    /// </summary>
    public Vector3 Position;
}