using System.Collections;
using System.Collections.Generic;
using DeBroglie.Topo;
using UnityEngine;

public abstract class WFCSpawnerAbstact
{
    protected int lineCount;
    protected Transform transform;
    protected float m_gridSize = 1f;
    protected float m_gridExtent = 10f;

    public WFCSpawnerAbstact(Transform transform, int lineCount, float m_gridSize, float m_gridExtent)
    {
        this.transform = transform;
        this.lineCount = lineCount;
        this.m_gridSize = m_gridSize;
        this.m_gridExtent = m_gridExtent;
    }

    public abstract void spawnTiles(ITopoArray<WFCTile> result, bool useRotations);
    public abstract void ClearPreviousIteration();
}