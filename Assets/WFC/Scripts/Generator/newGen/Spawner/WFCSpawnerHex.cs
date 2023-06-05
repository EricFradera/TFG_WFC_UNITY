using System.Collections;
using System.Collections.Generic;
using DeBroglie.Topo;
using UnityEngine;

public class WFCSpawnerHex : WFCSpawnerAbstact
{
    private Dictionary<string, Material> materials = new Dictionary<string, Material>();
    private GameObject[,] gameObjectArray;
    private float tileOffsetX = 1f;
    private float tileOffsetZ = 1f;

    public WFCSpawnerHex(Transform transform, int lineCount, float m_gridSize, float m_gridExtent) : base(transform,
        lineCount, m_gridSize, m_gridExtent)
    {
        gameObjectArray = new GameObject[base.lineCount, lineCount];
    }

    public override void spawnTiles(ITopoArray<WFCTile> result, bool useRotations,int tileSetIndex)
    {
        Debug.Log("Line count is: " + this.lineCount);
        GameObject tempGameObject;
        for (var x = 0; x < lineCount; x++)
        {
            for (var z = 0; z < lineCount; z++)
            {
                tempGameObject = Object.Instantiate(result.Get(x, z).tileVisuals[tileSetIndex]);
                //tempGameObject.transform.Rotate(new Vector3(90f, 0, 0));
                //tempGameObject.GetComponent<MeshRenderer>().material = genMat((WFCHEXTile)result.Get(x, z));
                tempGameObject.transform.position = z % 2 == 0
                    ? new Vector3(x * tileOffsetX, 0, z * tileOffsetZ)
                    : new Vector3(x * tileOffsetX + tileOffsetX / 2, 0, z * tileOffsetZ);
                tempGameObject.transform.parent = this.transform;
            }
        }
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