using System;
using System.Collections;
using System.Collections.Generic;
using DeBroglie.Topo;
using UnityEditor;
using UnityEngine;
using WFC;

[Serializable, ExecuteInEditMode]
public class WFCGenerator : MonoBehaviour
{
    public WFCConfig WFCConfigFile;

    public float m_gridSize = 1f;
    public float m_gridExtent = 10f;
    public Color lineColor = Color.white;
    public List<WFCTile> wfcTilesList;
    private GameObject[,] gameObjectArray;
    private WFCAbstractProc generator;
    private WFCSpawnerAbstact spawner;
    private WFCManager manager;
    public Vector2 vecSize;
    private Dictionary<string, Material> materials = new Dictionary<string, Material>();


    public void Generate()
    {
        if (WFCConfigFile is null) throw new Exception("WFCConfig file is empty");
        var lineCount = Mathf.RoundToInt((m_gridExtent * 2) / m_gridSize);
        if (lineCount % 2 == 0) lineCount++;
        lineCount--;
        ClearPreviousIteration();
        spawner = WFCConfigFile.CreateSpawner(transform, lineCount, m_gridSize, m_gridExtent);
        generator = WFCConfigFile.CreateProcessor(WFCConfigFile.wfcTilesList, WFCConfigFile.createWFCManager());
        spawner.spawnTiles(generator.RunWFC(m_gridExtent, m_gridSize), WFCConfigFile.useRotations);
        if (WFCConfigFile.useRotations) generator.clearRotationList();
    }


    public void populateList()
    {
        if (WFCConfigFile != null) wfcTilesList = WFCConfigFile.wfcTilesList;
    }

    public void clearList() => wfcTilesList = null;

    public void ClearPreviousIteration()
    {
        materials.Clear();

        if (gameObjectArray == null)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }
        else
        {
            foreach (var tile in gameObjectArray)
            {
                DestroyImmediate(tile);
            }
        }
    }
}