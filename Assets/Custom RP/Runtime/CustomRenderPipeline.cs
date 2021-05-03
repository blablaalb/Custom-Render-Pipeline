using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;

public class CustomRenderPipeline : RenderPipeline
{
    private bool _useDynamicBatching, _useGPUInstancing;
    private CameraRenderer _cameraRenderer = new CameraRenderer();

    public CustomRenderPipeline(bool useDynamicBatching, bool useGPUInstancing, bool useSRPBatcher)
    {
        _useDynamicBatching = useDynamicBatching;
        _useGPUInstancing = useGPUInstancing;
        GraphicsSettings.useScriptableRenderPipelineBatching = useSRPBatcher;
    }

    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        foreach (Camera camera in cameras)
        {
            _cameraRenderer.Render(context, camera, _useDynamicBatching, _useGPUInstancing);
        }
    }
}