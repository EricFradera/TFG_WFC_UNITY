using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WFC;

[CreateAssetMenu(menuName = "WFC components/WFCConfig/WFC3DConfig", order = 3, fileName = "WFC3DConfig"), Serializable]
public class WFC3DConfig : WFCConfig
{
    public override WFCManager createWFCManager()
    {
        return new WFC3DManager(this);
    }

    public override WFCSpawnerAbstact CreateSpawner(Transform transform, int lineCount, float m_gridSize, float m_gridExtent)
    {
        throw new NotImplementedException();
    }

    public override WFCAbstractProc CreateProcessor(List<WFCTile> listOfTiles, WFCManager manager)
    {
        throw new NotImplementedException();
    }
}