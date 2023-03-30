using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using WFC;
using Object = UnityEngine.Object;

namespace WFCEditor
{
    public class ExportTilesCustomUI : EditorWindow
    {
        [MenuItem("WFC/Tile Exporter")]
        public static void showWindow()
        {
            EditorWindow.GetWindow<ExportTilesCustomUI>("Tiles exporter");
        }

        public Object source;
        public Object textAsset;
        private JsonGen _generator;
        public String path;
        GUIOPTIONS _enumVar = GUIOPTIONS.TileToJson;
        private String processState = "";

        enum GUIOPTIONS
        {
            TileToJson,
            JsonToTile
        }

        void OnGUI()
        {
            _generator = new JsonGen();
            GUIStyle s = new GUIStyle(EditorStyles.textField)
            {
                alignment = TextAnchor.MiddleCenter,
                normal =
                {
                    textColor = Color.green
                }
            };

            GUILayout.Label("Choose operation");
            _enumVar = (GUIOPTIONS)EditorGUILayout.EnumPopup(_enumVar);
            GUILayout.Space(20);
            if (_enumVar == GUIOPTIONS.TileToJson)
            {
                GUILayout.Label("Select Scriptable object of type Tile", EditorStyles.label);
                source = EditorGUILayout.ObjectField(source, typeof(WFC2DTile), true);
                processState = "";
                GUILayout.Space(10);
                GUILayout.Label("Select where the file will be saved");
                GUILayout.TextArea(path);
                if (GUILayout.Button("Select path"))
                {
                    path = EditorUtility.OpenFolderPanel("Specify target location", "Assets/", "a");
                }

                GUILayout.Space(20);
                try
                {
                    if (GUILayout.Button("WFC2DTile to JSON")) _generator.GenerateJsonFromTile((WFC2DTile)source, path);
                    processState = "SUCCESS";
                }
                catch 
                {
                    if (source.IsUnityNull()) Debug.Log("Tile field is empty");
                    processState = "ERROR";
                    s.normal.textColor = Color.red;
                }
            }
            else
            {
                GUILayout.Label("Select JSON file to convert into a tile", EditorStyles.label);
                textAsset = EditorGUILayout.ObjectField(textAsset, typeof(TextAsset), true);
                processState = "";
                GUILayout.Space(10);
                GUILayout.Label("Select where the file will be saved");
                GUILayout.TextArea(path);
                if (GUILayout.Button("Select path"))
                {
                    path = EditorUtility.OpenFolderPanel("Specify target location", "Assets/", "a");
                    processState = "";
                }

                GUILayout.Space(20);
                try
                {
                    if (GUILayout.Button("JSON to WFC2DTile"))
                        _generator.GenerateTileFromJson((TextAsset)textAsset, path);
                    processState = "SUCCESS";
                }
                catch 
                {
                    if (textAsset.IsUnityNull()) Debug.Log("Text field is empty");
                    processState = "ERROR";
                    s.normal.textColor = Color.red;
                }
            }
            
            EditorGUILayout.LabelField(processState,s);
            //this.Repaint();
            
        }
    }
}