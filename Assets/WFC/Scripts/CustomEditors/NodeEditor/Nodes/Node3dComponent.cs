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

    public Node3dComponent(WFC3DTile tile) : base(tile)
    {
        
        portNames = new[] { "y+", "y-", "x+", "x-", "z+", "z-" };
        for (int i = 0; i <tile.Getdim(); i++)
        {
            CreateInputPort(i);
            CreateOutputPort(i);
        }
        ImageView();
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