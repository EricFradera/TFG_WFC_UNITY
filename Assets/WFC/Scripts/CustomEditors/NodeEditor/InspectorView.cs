using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;

public class InspectorView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits>
    {
    }

    private Editor editor;

    public InspectorView()
    {
    }

    public void UpdateSelection(NodeComponent component)
    {
        Clear();
        UnityEngine.Object.DestroyImmediate(editor);
        editor = component switch
        {
            Node1dComponent node1dComponent => Editor.CreateEditor(node1dComponent.tile),
            Node2dComponent node2dComponent => Editor.CreateEditor(node2dComponent.tile),
            Node3dComponent node3dComponent => Editor.CreateEditor(node3dComponent.tile),
            NodeHEXComponent nodeHexComponent => Editor.CreateEditor(nodeHexComponent.tile),
            StringCodeNode stringCodeNode => Editor.CreateEditor(stringCodeNode.codeData),
            _ => editor
        };
        var container = new IMGUIContainer(() => { editor.OnInspectorGUI(); });

        Add(container);
    }
}