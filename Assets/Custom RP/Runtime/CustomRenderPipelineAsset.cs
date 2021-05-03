using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;

[CreateAssetMenu(menuName = "Rendering/Custom Render Pipeline")]
public class CustomRenderPipelineAsset : RenderPipelineAsset
{
    [SerializeField]
    private bool _useDynamicBatching = true, _useGPUInstancing = true, _useSRPBatcher = true;

    protected override RenderPipeline CreatePipeline()
    {
        return new CustomRenderPipeline(_useDynamicBatching, _useGPUInstancing, _useSRPBatcher);
    }
}