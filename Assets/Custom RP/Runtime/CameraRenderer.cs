using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;

public partial class CameraRenderer
{
    private ScriptableRenderContext _scriptableRenderContext;
    private Camera _camera;
    private const string BUFFER_NAME = "Render Camera";
    private CommandBuffer _commandBuffer = new CommandBuffer
    {
        name = BUFFER_NAME
    };
    private CullingResults _cullingResults;
    private static ShaderTagId _unlitShaderTagID = new ShaderTagId("SRPDefaultUnlit");

    public void Render(ScriptableRenderContext scriptableRenderContext, Camera camera, bool useDynamicBatching, bool useGPUInstancing)
    {
        _scriptableRenderContext = scriptableRenderContext;
        _camera = camera;

        PrepareBuffer();
        PrepareForSceneWindow();
        if (!Cull())
        {
            return;
        }

        Setup();
        DrawVisibleGeometry(useDynamicBatching, useGPUInstancing);
        DrawUnsupportedShaders();
        Submit();
    }

    private void DrawVisibleGeometry(bool useDynamicBatching, bool useGPUInstancing)
    {
        SortingSettings sortingSettings = new SortingSettings(_camera) { criteria = SortingCriteria.CommonOpaque };
        DrawingSettings drawingSettings = new DrawingSettings(_unlitShaderTagID, sortingSettings) { enableDynamicBatching = useDynamicBatching, enableInstancing = useGPUInstancing };
        FilteringSettings filteringSettings = new FilteringSettings(RenderQueueRange.opaque);

        _scriptableRenderContext.DrawRenderers(_cullingResults, ref drawingSettings, ref filteringSettings);

        _scriptableRenderContext.DrawSkybox(_camera);

        sortingSettings.criteria = SortingCriteria.CommonTransparent;
        drawingSettings.sortingSettings = sortingSettings;
        filteringSettings.renderQueueRange = RenderQueueRange.transparent;

        _scriptableRenderContext.DrawRenderers(_cullingResults, ref drawingSettings, ref filteringSettings);
    }

    private void Submit()
    {
        _commandBuffer.EndSample(SAMPLE_NAME);
        ExecuteBuffer();
        _scriptableRenderContext.Submit();
    }

    private void Setup()
    {
        _scriptableRenderContext.SetupCameraProperties(_camera);
        CameraClearFlags flags = _camera.clearFlags;
        _commandBuffer.ClearRenderTarget(flags <= CameraClearFlags.Depth, flags == CameraClearFlags.Color, flags == CameraClearFlags.Color ? _camera.backgroundColor.linear : Color.clear);
        _commandBuffer.BeginSample(SAMPLE_NAME);
        ExecuteBuffer();
    }

    private void ExecuteBuffer()
    {
        _scriptableRenderContext.ExecuteCommandBuffer(_commandBuffer);
        _commandBuffer.Clear();
    }

    private bool Cull()
    {
        if (_camera.TryGetCullingParameters(out ScriptableCullingParameters scriptableCullingParameters))
        {
            _cullingResults = _scriptableRenderContext.Cull(ref scriptableCullingParameters);
            return true;
        }
        return false;
    }
}