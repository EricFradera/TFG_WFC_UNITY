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
        public Object source;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            GUILayout.Label("Select Scriptable object of type Tile", EditorStyles.label);
            source = EditorGUILayout.ObjectField(source, typeof(WFCConfig), true);
            GridBuilder gridBuilder = (GridBuilder)target;
            gridBuilder.size = EditorGUILayout.IntField("size", gridBuilder.size);
            if (GUILayout.Button("Reset Values")) gridBuilder.Genarate((WFCConfig)source);
        }
    }
}