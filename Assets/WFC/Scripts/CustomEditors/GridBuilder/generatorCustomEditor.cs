using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WFC;

namespace WFCEditor
{
    [CustomEditor(typeof(GridBuilder))]
    public class generatorCustomEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            GridBuilder gridBuilder = (GridBuilder)target;
            gridBuilder.size = EditorGUILayout.IntField("size", gridBuilder.size);
            if(GUILayout.Button("Reset Values"))gridBuilder.Genarate();

        }
    }
}

