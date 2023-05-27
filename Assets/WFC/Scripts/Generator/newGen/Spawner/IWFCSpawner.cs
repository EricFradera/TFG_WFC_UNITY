using System.Collections;
using System.Collections.Generic;
using DeBroglie.Topo;
using UnityEngine;

public interface IWFCSpawner
{
    public void spawnTiles(ITopoArray<WFCTile> result);
    public void clear();
}