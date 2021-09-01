using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
[ImageEffectAllowedInSceneView]
//	다양한 이미지 효과를 혼합해서 사용하는 스크립트다.
public class CustomImageEffects : MonoBehaviour
{
    //	EffectDesc는 이미지 효과의 Description을 담는다.
    [Serializable]
    public class EffectDesc
    {
        public Material mat;
        public bool isActive = true;
    }
    public List<EffectDesc> imageEffects = new List<EffectDesc>();

    public EffectDesc FindEffect(string matName)
    {
        return imageEffects.FirstOrDefault(x => x.mat.name == matName);
    }
    //	RenderImage함수를 활용해 EffectDesc에 있는 마테리얼 쉐이더 패스에 씬 텍스쳐를 넘긴다.
    [ImageEffectOpaque]
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        RenderTexture[] textures = new RenderTexture[imageEffects.Count + 1];
        if (imageEffects.Count == 0)
        {
            Graphics.Blit(src, dest);
            return;
        }
        var currentSource = textures[0] = src;
        for (var i = 1; i < textures.Length; i++)
        {
            textures[i] = RenderTexture.GetTemporary(dest.descriptor);
        }
        var currentDest = dest;
        //	현재 씬 Texture를 여러 마테리얼에 넘겨가며 혼합한다.
        for (var i = 1; i < textures.Length; i++)
        {
            if (!imageEffects[i - 1].isActive) continue;
            currentDest = textures[i];
            Graphics.Blit(currentSource, currentDest, imageEffects[i - 1].mat);
            currentSource = currentDest;
        }
        Graphics.Blit(currentSource, dest);
        //	사용된 텍스쳐를 해제한다.
        for (var i = 0; i < textures.Length; i++)
        {
            RenderTexture.ReleaseTemporary(textures[i]);
        }
    }
}