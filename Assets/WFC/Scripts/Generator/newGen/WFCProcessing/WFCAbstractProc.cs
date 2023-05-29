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
    
    public  ITopoArray<WFCTile> RunWFC(int size)
    {
        if (listOfTiles is null) throw new Exception("List of tiles is Empty");
        var model = RunModel();
        var topology = new GridTopology(size, size, periodic: false);
        var propagator = new TilePropagator(model, topology, true); //backtrackinh need s tp be able to turn off
        var status = propagator.Run();
        if (status != Resolution.Decided) throw new Exception("The WFC resulted as undecided");
        var output = propagator.ToValueArray<WFCTile>();
        return output;
    }

    public abstract AdjacentModel RunModel();
    public abstract void clearRotationList();
}