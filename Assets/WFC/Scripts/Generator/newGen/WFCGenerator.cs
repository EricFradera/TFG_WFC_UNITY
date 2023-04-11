using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WFC;

[Serializable]
public class WFCGenerator : MonoBehaviour
{
    //Maybe it's useless.
    public GeneratorModes mode;
    public WFCConfig WFCConfigFile;
    public float m_gridSize = 1f;
    public float m_gridExtent = 10f;
    public Color lineColor = Color.white;
    public List<WFCTile> wfcTilesList;


    public enum GeneratorModes
    {
        WFC2DMODE,
        WFC3DMODE,
        WFCHEXMODE,
        WFCGRAPHMODE
    }

    public void Generate()
    {
        ClearPreviousIteration();
        Debug.Log("we generate!!!");
    }

    public void populateList() => wfcTilesList = WFCConfigFile.wfcTilesList;
    public void clearList() => wfcTilesList = null;


    public void ClearPreviousIteration()
    {
        Debug.Log("we clear!!!");
    }
}