using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointTextPool : ObjectPool<TextMeshPro>
{
    [SerializeField]
    Vector3 offset;

    private void Start()
    {
        GameManager.Manager.OnPointTextAtPosition.AddListener((position, color, points) => SpawnPoint(position, points, color));
    }

    void SpawnPoint(Vector3 position, int points, Color color)
    {
        TextMeshPro textBox = GetObject();
        textBox.color = color;
        textBox.text = "" + points;

        textBox.transform.position = position + offset;

        textBox.gameObject.SetActive(true);
    }
}
