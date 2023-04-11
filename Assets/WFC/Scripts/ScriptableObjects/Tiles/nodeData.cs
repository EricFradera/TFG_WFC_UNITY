using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class nodeData
{
    public Vector2 position;
    public List<nodeRelation> relationShips = new List<nodeRelation>();
}