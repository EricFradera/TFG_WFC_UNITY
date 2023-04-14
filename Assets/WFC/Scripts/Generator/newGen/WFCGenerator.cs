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
    private GameObject[,] gameObjectArray;
    private WFCProc generator;
    public Vector2 vecSize;


    public enum GeneratorModes
    {
        WFC2DMODE,
        WFC3DMODE,
        WFCHEXMODE,
        WFCGRAPHMODE
    }

    public void Generate()
    {
        var lineCount = Mathf.RoundToInt((m_gridExtent * 2) / m_gridSize);
        if (lineCount % 2 == 0) lineCount++;
        lineCount--;
        var half = lineCount / 2;
        ClearPreviousIteration();
        gameObjectArray = new GameObject[lineCount, lineCount];

        //Generation
        generator ??= new WFCProc(WFCConfigFile.wfcTilesList);
        var res = generator.runWFC(lineCount);


        for (int i = 0; i < lineCount; i++)
        {
            for (int j = 0; j < lineCount; j++)
            {
                float xCoord = (i - half) * m_gridSize + (m_gridSize / 2);
                float zCoord = (j - half) * m_gridSize + (m_gridSize / 2);
                gameObjectArray[i, j] = Instantiate(res.Get(i, j).tileVisuals,
                    new Vector3(xCoord, 0, zCoord),
                    transform.rotation);
                gameObjectArray[i, j].transform.localScale = new Vector3(m_gridSize, 1, m_gridSize);
                gameObjectArray[i, j].transform.parent = gameObject.transform;
            }
        }
    }

    public void populateList() => wfcTilesList = WFCConfigFile.wfcTilesList;
    public void clearList() => wfcTilesList = null;


    public void ClearPreviousIteration()
    {
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