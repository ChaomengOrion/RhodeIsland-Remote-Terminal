// Created by ChaomengOrion
// Create at 2022-07-25 23:45:00
// Last modified on 2022-07-26 15:45:37

using UnityEngine;
using Cysharp.Threading.Tasks;

public static class ImageUtil
{
    public static async UniTask SaveTexture2DToPathAsync(this Texture2D texture, string path)
    {
        await System.IO.File.WriteAllBytesAsync(path, texture.EncodeToPNG());
    }

    public static void SaveTexture2DToPath(this Texture2D texture, string path)
    {
        System.IO.File.WriteAllBytes(path, texture.EncodeToPNG());
    }

    public static Color CalculateMainColor(this Texture2D texture, Material mat, int sample = 16)
    {
        Texture2D copy = DuplicateTextureWithSize(texture, mat, texture.height / sample, texture.width / sample);
        Color[] colors = copy.GetPixels(0, 0, copy.width, copy.height);
        Vector3 sum = new();
        float sumA = 0f;
        for (int i = 0; i < colors.Length; i++)
        {
            Color c = colors[i];
            sum += new Vector3(c.r, c.g, c.b) * c.a;
            sumA += c.a;
        }
        sum /= sumA;
        return new(sum.x, sum.y, sum.z);
    }

    public static Color CalculateMainColor(this Texture2D texture, int sample = 16)
    {
        Texture2D copy = DuplicateTextureWithSize(texture, texture.height / sample, texture.width / sample);
        Color[] colors = copy.GetPixels(0, 0, copy.width, copy.height);
        Vector3 sum = new();
        float sumA = 0f;
        for (int i = 0; i < colors.Length; i++)
        {
            Color c = colors[i];
            sum += new Vector3(c.r, c.g, c.b) * c.a;
            sumA += c.a;
        }
        sum /= sumA;
        return new(sum.x, sum.y, sum.z);
    }

    public static Texture2D DuplicateTextureWithSize(this Texture2D source, Material mat, int width, int height)
    {
        RenderTexture renderTex = RenderTexture.GetTemporary(
                    width,
                    height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);

        Graphics.Blit(source, renderTex, mat);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        Texture2D readableText = new(width, height);
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableText;
    }

    public static Texture2D DuplicateTextureWithSize(this Texture2D source, int width, int height)
    {
        RenderTexture renderTex = RenderTexture.GetTemporary(
                    width,
                    height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);

        Graphics.Blit(source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        Texture2D readableText = new(width, height);
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableText;
    }

    public static Texture2D DuplicateTexture(this Texture2D source)
    {
        return DuplicateTextureWithSize(source, source.width, source.height);
    }

    public static Texture2D DuplicateTexture(this Texture2D source, Material mat)
    {
        return DuplicateTextureWithSize(source, mat, source.width, source.height);
    }
}
