using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using WFC;
using Vector2 = System.Numerics.Vector2;

public class Node2dComponent : NodeComponent
{
    public WFC2DTile tile;

    public Node2dComponent(WFC2DTile tile)
    {
        this.tile = tile;
        this.viewDataKey = tile.tileId;
        this.title = tile.tileName;
        portNames = new[] { "up", "right", "down", "left" };
        input = new Port[4];
        output = new Port[4];
        style.left = tile.nodeData.position.X;
        style.top = tile.nodeData.position.Y;
        for (int i = 0; i < 4; i++)
        {
            CreateInputPort(i);
            CreateOutputPort(i);
        }

        ImageView();
        Label titleLabel = this.Q<Label>("title-label");
        titleLabel.bindingPath = "tileName";
        titleLabel.Bind(new SerializedObject(tile));
    }

    // to update the image https://docs.unity3d.com/Manual/UIE-bind-custom-control.html
    private void ImageView()
    {
        tile.tileTexture ??= Texture2D.whiteTexture;
        var container = new VisualElement
        {
            name = " Parent Container",
            pickingMode = PickingMode.Position,
            style = { overflow = Overflow.Visible },
        };
        var previewImage = new Image
        {
            name = "tileTexture",
            pickingMode = PickingMode.Ignore,
            image = tile.tileTexture
        };


        container.style.height = new StyleLength(120);
        previewImage.StretchToParentSize();
        container.contentContainer.Add(previewImage);
        Add(container);
    }


    protected override void setNodePos(float x, float y) => this.tile.nodeData.position = new Vector2(x, y);
}