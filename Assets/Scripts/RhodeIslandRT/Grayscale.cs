// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-04-19 17:35:22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Grayscale : MonoBehaviour
{
    public Material mat;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        RenderTexture src = RenderTexture.GetTemporary(source.width, source.height);
        mat.SetTexture("_MainTex", source);
        Graphics.Blit(source, src, mat, 0);
        Graphics.Blit(src, destination);
        RenderTexture.ReleaseTemporary(src);
    }
}
