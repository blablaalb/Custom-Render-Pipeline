using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;

public class CustomRenderPipeline : RenderPipeline
{
    private CameraRenderer _cameraRenderer = new CameraRenderer();
    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        foreach (Camera camera in cameras)
        {
            _cameraRenderer.Render(context, camera);
        }
    }
}