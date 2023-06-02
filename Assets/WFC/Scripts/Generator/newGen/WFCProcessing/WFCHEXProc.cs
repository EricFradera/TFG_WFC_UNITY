using System.Collections;
using System.Collections.Generic;
using DeBroglie.Models;
using DeBroglie.Topo;
using UnityEngine;

public class WFCHEXProc : WFCAbstractProc
{
    private Direction[] direction = { Di };
    private List<WFCTile> listOfRotatedTiles;
    private bool useRotation;

    public WFCHEXProc(List<WFCTile> listOfTiles, WFCManager manager) : base(listOfTiles, manager)
    {
    }

    public override ITopoArray<WFCTile> RunWFC(float m_gridExtent, float m_gridSize)
    {
        throw new System.NotImplementedException();
    }

    public override AdjacentModel RunModel()
    {
        throw new System.NotImplementedException();
    }

    public override void clearRotationList()
    {
        throw new System.NotImplementedException();
    }
}
