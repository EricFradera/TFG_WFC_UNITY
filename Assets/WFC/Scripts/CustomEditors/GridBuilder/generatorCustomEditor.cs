using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WFC;

namespace WFCEditor
{
    [CustomEditor(typeof(testscript))]
    public class generatorCustomEditor : Editor
    {
        public Object source;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            GUILayout.Label("Select Scriptable object of type Tile", EditorStyles.label);
            source = EditorGUILayout.ObjectField(source, typeof(WFCConfig), true);
            testscript testscript = (testscript)target;
            //if (GUILayout.Button("Reset Values")) testscript.testRel(source);
        }
    }
}