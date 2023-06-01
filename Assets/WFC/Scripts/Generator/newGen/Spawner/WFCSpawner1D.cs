using System.Collections;
using System.Collections.Generic;
using DeBroglie.Topo;
using UnityEngine;

public class WFCSpawner1D : WFCSpawnerAbstact
{
    private Dictionary<string, Material> materials = new Dictionary<string, Material>();
    private GameObject[,] gameObjectArray;

    public WFCSpawner1D(Transform transform, int lineCount, float m_gridSize, float m_gridExtent) : base(transform,
        lineCount, m_gridSize, m_gridExtent)
    {
        gameObjectArray = new GameObject[base.lineCount, lineCount];
    }

    public override void spawnTiles(ITopoArray<WFCTile> result, bool useRotations)
    {
        var primitive = GameObject.CreatePrimitive(PrimitiveType.Quad);

        var sliptCount = m_gridExtent / m_gridSize;
        
        float xCoord = (2-Mathf.RoundToInt(m_gridSize) / 2)*sliptCount;
        
        primitive.transform.position = new Vector3(xCoord, 0, 0);
        primitive.transform.Rotate(new Vector3(90f, 0, 0));
        primitive.transform.localScale = new Vector3(m_gridExtent / m_gridSize, m_gridExtent / m_gridSize, 1);
        //Mathf.RoundToInt(gridSize)
    }
    /*if (lineCount % 2 == 0) lineCount++;
        lineCount--;
        var halfLines = lineCount / 2;

        for (int i = 0; i < lineCount; i++)
        {
            for (int j = 0; j < lineCount; j++)
            {
                float xCoord = (i - halfLines) * m_gridSize + (m_gridSize / 2);
                float zCoord = (j - halfLines) * m_gridSize + (m_gridSize / 2);
                if (false)
                {
                    gameObjectArray[i, j] = Object.Instantiate(result.Get(i, j).tileVisuals,
                        new Vector3(xCoord, 0, zCoord),
                        transform.rotation);
                }
                else
                {
                    var primitive = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    primitive.transform.position = new Vector3(xCoord, 0, zCoord);
                    primitive.transform.Rotate(new Vector3(90f, 0, (result.Get(i, j).rotationModule) * -90));
                    primitive.GetComponent<MeshRenderer>().material = genMat((WFC1DTile)result.Get(i, j));
                    gameObjectArray[i, j] = primitive;
                }

                gameObjectArray[i, j].transform.localScale = new Vector3(m_gridSize, 1, m_gridSize);
                gameObjectArray[i, j].transform.parent = this.transform;
            }
        }*/

    private Material genMat(WFC1DTile tile)
    {
        if (materials.ContainsKey(tile.tileId)) return materials[tile.tileId];
        var mat = new Material(Shader.Find("Universal Render Pipeline/Unlit"))
        {
            mainTexture = tile.tileTexture
        };
        materials.Add(tile.tileId, mat);
        return mat;
    }

    public override void ClearPreviousIteration()
    {
        materials.Clear();

        if (gameObjectArray == null)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Object.DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }
        else
        {
            foreach (var tile in gameObjectArray)
            {
                Object.DestroyImmediate(tile);
            }
        }
    }
}