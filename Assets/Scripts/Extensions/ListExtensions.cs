using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
    public static T GetRandomObject<T>(this List<T> list)
    {
        return list[UnityEngine.Random.Range(0, list.Count)];
    }
}
