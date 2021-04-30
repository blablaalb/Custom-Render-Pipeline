using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;

[CreateAssetMenu(menuName="Rendering/Custom Render Pipeline")]
public class CustomRenderPipelineAsset : RenderPipelineAsset
{
    protected override RenderPipeline CreatePipeline()
    {
        return new CustomRenderPipeline();
    }
}