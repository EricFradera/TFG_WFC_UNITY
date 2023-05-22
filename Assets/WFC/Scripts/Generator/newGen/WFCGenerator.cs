using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WFC;

[Serializable]
public class WFCGenerator : MonoBehaviour
{
    //Maybe it's useless.
    public WFCConfig WFCConfigFile;
    public float m_gridSize = 1f;
    public float m_gridExtent = 10f;
    public Color lineColor = Color.white;
    public List<WFCTile> wfcTilesList;
    private GameObject[,] gameObjectArray;
    private WFCProc generator;
    public Vector2 vecSize;
    private bool tileMode = false;
    private Dictionary<string, Material> materials = new Dictionary<string, Material>();


    public void Generate()
    {
        var lineCount = Mathf.RoundToInt((m_gridExtent * 2) / m_gridSize);
        if (lineCount % 2 == 0) lineCount++;
        lineCount--;
        var half = lineCount / 2;
        ClearPreviousIteration();
        gameObjectArray = new GameObject[lineCount, lineCount];

        //Generation
        generator ??= new WFCProc(WFCConfigFile.wfcTilesList, WFCConfigFile.useRotations);

        gen2D(lineCount, half);
    }

    private void gen2D(int lineCount, int half)
    {
        var res = generator.runWFC(lineCount);
        for (int i = 0; i < lineCount; i++)
        {
            for (int j = 0; j < lineCount; j++)
            {
                float xCoord = (i - half) * m_gridSize + (m_gridSize / 2);
                float zCoord = (j - half) * m_gridSize + (m_gridSize / 2);
                if (false)
                {
                    gameObjectArray[i, j] = Instantiate(res.Get(i, j).tileVisuals,
                        new Vector3(xCoord, 0, zCoord),
                        transform.rotation);
                }
                else
                {
                    var primitive = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    primitive.transform.position = new Vector3(xCoord, 0, zCoord);
                    primitive.transform.Rotate(new Vector3(90f, 0, 0));
                    primitive.GetComponent<MeshRenderer>().material = genMat((WFC2DTile)res.Get(i, j));
                    gameObjectArray[i, j] = primitive;
                }

                gameObjectArray[i, j].transform.localScale = new Vector3(m_gridSize, 1, m_gridSize);
                gameObjectArray[i, j].transform.parent = gameObject.transform;
            }
        }
    }

    private Material genMat(WFC2DTile tile)
    {
        if (materials.ContainsKey(tile.tileId)) return materials[tile.tileId];
        var mat = new Material(Shader.Find("Universal Render Pipeline/Unlit"))
        {
            mainTexture = tile.tileTexture
        };
        materials.Add(tile.tileId, mat);
        return mat;
    }

    private void gen3D(int lineCount, int half)
    {
        var res = generator.runWFC(lineCount);
        for (int k = 0; k < lineCount; k++)
        {
            for (int i = 0; i < lineCount; i++)
            {
                for (int j = 0; j < lineCount; j++)
                {
                    float xCoord = (i - half) * m_gridSize + (m_gridSize / 2);
                    float zCoord = (j - half) * m_gridSize + (m_gridSize / 2);
                    float yCoord = (k - half) * m_gridSize + (m_gridSize / 2);
                    gameObjectArray[i, j] = Instantiate(res.Get(i, j).tileVisuals,
                        new Vector3(xCoord, yCoord, zCoord),
                        transform.rotation);
                    gameObjectArray[i, j].transform.localScale = new Vector3(m_gridSize, m_gridSize, m_gridSize);
                    gameObjectArray[i, j].transform.parent = gameObject.transform;
                }
            }
        }
    }

    private void genWithTexture()
    {
    }

    public void populateList() => wfcTilesList = WFCConfigFile.wfcTilesList;
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