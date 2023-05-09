using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using Object = UnityEngine.Object;

[Serializable]
public class nodeRelation
{
    public nodeRelation(int indexOutput, WFCTile inputTile, int indexInput)
    {
        this.indexOutput = indexOutput;
        this.inputTile = inputTile;
        this.indexInput = indexInput;
    }

    public nodeRelation(int indexOutput, Object inputTile)
    {
        this.indexOutput = indexOutput;
        this.inputTile = inputTile;
        this.indexInput = 0;
    }

    public readonly int indexOutput;
    public readonly Object inputTile;
    public readonly int indexInput;
    
}