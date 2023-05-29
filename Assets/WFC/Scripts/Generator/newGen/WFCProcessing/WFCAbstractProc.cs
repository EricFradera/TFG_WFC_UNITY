using System;
using System.Collections;
using System.Collections.Generic;
using DeBroglie;
using DeBroglie.Models;
using DeBroglie.Topo;

public abstract class WFCAbstractProc
{
    protected List<WFCTile> listOfTiles;
    protected Generate_Adjacency adjacency;
    
    public WFCAbstractProc(List<WFCTile> listOfTiles, WFCManager manager)
    {
        this.listOfTiles = listOfTiles;
        adjacency = new Generate_Adjacency();
        
    }
    public abstract ITopoArray<WFCTile> RunWFC(int size);

    public abstract AdjacentModel RunModel();
    public abstract void clearRotationList();
}