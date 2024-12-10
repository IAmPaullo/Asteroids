using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class AssetPreviewAttribute : PropertyAttribute
{
    public float Height { get; private set; }

    public AssetPreviewAttribute(float height = 64)
    {
        Height = height;
    }
}
