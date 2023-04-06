using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WFC
{
    [CreateAssetMenu(menuName = "WFC components/WFC3DTile",order = 2, fileName = "WFC3dTile"),Serializable]
    public class WFC3DTile : ScriptableObject
    {
        public int tileId;
        //3d asset
        //This has to become a jagged array or dictionary
        public String id_up, id_right, id_down, id_left, id_zUp, id_zDown;
        public List<int> up;
        public List<int> right;
        public List<int> down;
        public List<int> left;
        public List<int> zup;
        public List<int> zDown;
    }
}