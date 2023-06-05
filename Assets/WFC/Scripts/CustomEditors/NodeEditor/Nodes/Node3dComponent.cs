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
    

    public Node3dComponent(WFC3DTile tile) : base(tile)
    {
        portNames = new[] { "Y+", "Y-", "X+", "X-", "Z+", "Z-" };
        for (int i = 0; i < tile.Getdim(); i++)
        {
            CreateOutputPort(i);
        }

        CreateInputPort();
        ImageView();
    }

    
    private void ImageView()
    {
        WFC3DTile imageTile = (WFC3DTile)this.tile;
        if (tile.tileVisuals[0] is null) imageTile.previewTexture2D = Texture2D.whiteTexture;
        else imageTile.previewTexture2D = AssetPreview.GetAssetPreview(tile.tileVisuals[0]);

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