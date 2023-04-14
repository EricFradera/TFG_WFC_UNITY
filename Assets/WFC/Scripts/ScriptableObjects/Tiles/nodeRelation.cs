using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodeRelation
{
    public nodeRelation(int indexOutput, Object inputTile, int indexInput)
    {
        this.indexOutput = indexOutput;
        this.inputTile = inputTile;
        this.indexInput = indexInput;
    }

    public readonly int indexOutput;
    public readonly Object inputTile;
    public readonly int indexInput;
}