using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;

[DisallowMultipleComponent]
public class PerObjectMaterialProperties : MonoBehaviour
{
    private static int _baseColorID = Shader.PropertyToID("_BaseColor");
    private static int _cutoffID = Shader.PropertyToID("_Cutoff");
    private static MaterialPropertyBlock _materialPropertyBlock;

    [SerializeField]
    private Color _baseColor = Color.white;
    [SerializeField, Range(0f, 1f)]
    private float _cutoff = 0.5f;

    internal void OnValidate()
    {
        if (_materialPropertyBlock == null)
        {
            _materialPropertyBlock = new MaterialPropertyBlock();
        }

        _materialPropertyBlock.SetColor(_baseColorID, _baseColor);
        _materialPropertyBlock.SetFloat(_cutoffID, _cutoff);
        GetComponent<Renderer>().SetPropertyBlock(_materialPropertyBlock);
    }

    internal void Awake()
    {
        OnValidate();
    }
}