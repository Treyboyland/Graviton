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
