using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RotationData2D
{
    public List<rotationData> PosibleRotation = new List<rotationData>();

    public struct rotationData
    {
        public string name;
        public Rotation image;
    }

    public enum Rotation
    {
        NoRotation,
        Degrees90,
        Degrees180,
        Degrees270
    };
}