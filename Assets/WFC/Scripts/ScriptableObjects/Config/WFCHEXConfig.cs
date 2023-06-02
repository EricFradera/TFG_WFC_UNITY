using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WFC;

[CreateAssetMenu(menuName = "WFC components/WFCHEXConfig", order = 4, fileName = "WFCHEXConfig"),
 Serializable]
public class WFCHexConfig : WFCConfig
{
    public override WFCManager createWFCManager()
    {
        return new WFCHEXManager(this);
    }

    public override WFCSpawnerAbstact CreateSpawner(Transform transform, int lineCount, float m_gridSize,
        float m_gridExtent)
    {
        return new WFCSpawnerHex(transform, lineCount, m_gridSize, m_gridExtent);
    }

    public override WFCAbstractProc CreateProcessor(List<WFCTile> listOfTiles, WFCManager manager)
    {
        return new WFCHEXProc(listOfTiles, manager);
    }
}