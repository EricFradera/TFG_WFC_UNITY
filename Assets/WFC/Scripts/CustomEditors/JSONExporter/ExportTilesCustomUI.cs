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
            JsonToTile,
            ConfigToJSON
        }

        private String[][] menuText = new string[][]
        {
            new String[]
            {
                "Select Scriptable object of type Tile", "Select where the file will be saved", "Select path",
                "Specify target location", "WFC2DTile to JSON"
            },
            new String[]
            {
                "Select JSON file to convert into a tile", "Select where the file will be saved", "Select path",
                "Specify target location", "JSON to WFC2DTile"
            },
            new String[]
            {
                "Config object of type Tile", "Select where the file will be saved", "Select path",
                "Specify target location", "WFCTile to JSON"
            },
        };

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
            contructUI(s);
            EditorGUILayout.LabelField(processState, s);
            //this.Repaint();
        }

        private void contructUI(GUIStyle s)
        {
            GUILayout.Label(menuText[(int)_enumVar][0], EditorStyles.label);
            source = EditorGUILayout.ObjectField(source, typeof(WFC2DTile), true);
            processState = "";
            GUILayout.Space(10);
            GUILayout.Label(menuText[(int)_enumVar][1]);
            GUILayout.TextArea(path);
            if (GUILayout.Button(menuText[(int)_enumVar][2]))
            {
                path = EditorUtility.OpenFolderPanel(menuText[(int)_enumVar][3], "Assets/", "a");
            }

            GUILayout.Space(20);
            try
            {
                if (GUILayout.Button(menuText[(int)_enumVar][4]))
                    _generator.GenerateJsonFromTile((WFC2DTile)source, path);
                processState = "SUCCESS";
            }
            catch
            {
                if (source.IsUnityNull()) Debug.Log("Tile field is empty");
                processState = "ERROR";
                s.normal.textColor = Color.red;
            }
        }
    }
}