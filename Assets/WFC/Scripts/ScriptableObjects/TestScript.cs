using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WFC;

public class TestScript : MonoBehaviour
{
    public WFCConfig configurationFile;

    private void Start()
    {
        translateData();
    }

    private void translateData()
    {
        foreach (var tile in configurationFile.wfcTilesList)
        {
            tile.adjacencyCodes[0] = tile.id_up;
            tile.adjacencyCodes[1] = tile.id_right;
            tile.adjacencyCodes[2] = tile.id_down;
            tile.adjacencyCodes[3] = tile.id_left;
            tile.adjacencyPairs = null;
            tile.adjacencyPairs = new List<int>[4];
            tile.adjacencyPairs[0] = new List<int>();
            tile.adjacencyPairs[1] = new List<int>();
            tile.adjacencyPairs[2] = new List<int>();
            tile.adjacencyPairs[3] = new List<int>();
        }

        Debug.Log("SUCCESS");
    }
}