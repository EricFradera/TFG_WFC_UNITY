using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using WFC;

public class Node3dComponent : NodeTileComponent
{
    [JsonIgnore] public Texture2D previewTexture2D;

    public Node3dComponent(WFC3DTile tile)
    {
        this.tile = tile;
        this.viewDataKey = tile.tileId;
        this.title = tile.tileName;
        portNames = new[] { "y+", "y-", "x+", "x-", "z+", "z-" };
        input = new Port[6];
        output = new Port[6];
        style.left = tile.nodeData.position.X;
        style.top = tile.nodeData.position.Y;
        for (int i = 0; i < 6; i++)
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
        WFC3DTile imageTile = (WFC3DTile)this.tile;
        imageTile.previewTexture2D ??= Texture2D.whiteTexture;
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
            image = imageTile.previewTexture2D
        };


        container.style.height = new StyleLength(120);
        previewImage.StretchToParentSize();
        container.contentContainer.Add(previewImage);
        Add(container);
    }
}