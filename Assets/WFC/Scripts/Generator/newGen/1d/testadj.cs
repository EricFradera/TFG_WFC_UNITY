using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WFC;

public class testadj : MonoBehaviour
{
    public WFCConfig Config;
    void Start()
    {
        foreach (var tile in Config.wfcTilesList)
        {
            Debug.Log("Tile with name: "+ tile.tileName);
            for (int i = 0; i < tile.Getdim(); i++)
            {
                Debug.Log("In pos: "+ i+ " it has a partner:");
                foreach (var tiledest in tile.adjacencyPairs[i])
                {
                    Debug.Log(tiledest.tileName);
                }
            }
        }
    }

    
}
