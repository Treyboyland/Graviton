using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public static class ListExtensions
{
    public static T GetRandomObject<T>(this List<T> list)
    {
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    public static string AsString<T>(this List<T> list, string delimiter = " ")
    {
        StringBuilder sb = new StringBuilder();
        foreach (T obj in list)
        {
            sb.Append(obj.ToString() + delimiter);
        }

        return sb.ToString();
    }
}


public static class Vector2Extensions
{
    public static Vector2 IsolateGreater(this Vector2 vector)
    {
        var v = vector;
        if (Mathf.Abs(v.x) > Mathf.Abs(v.y))
        {
            v.y = 0;
            v.x = Mathf.Sign(v.x);
        }
        else if (Mathf.Abs(v.x) < Mathf.Abs(v.y))
        {
            v.x = 0;
            v.y = Mathf.Sign(v.y);
        }
        else
        {
            v.x = 0;
            v.y = 0;
        }

        return v;
    }
}