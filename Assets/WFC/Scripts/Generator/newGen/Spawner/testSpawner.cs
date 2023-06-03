using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Serialization;

public class testSpawner : MonoBehaviour
{
    public GameObject hex;
    public int size;
    public float tileXOffset;
    public float tileZOffset;

    void Start()
    {
        CreateHexMap();
    }

    private void CreateHexMap()
    {
        GameObject tempGameObject;
        for (var x = 0; x < size; x++)
        for (var z = 0; z < size; z++)
        {
            tempGameObject = Instantiate(hex);
            tempGameObject.transform.position = z % 2 == 0
                ? new Vector3(x * tileXOffset, 0, z * tileZOffset)
                : new Vector3(x * tileXOffset + tileXOffset / 2, 0, tileZOffset);
        }
    }
}