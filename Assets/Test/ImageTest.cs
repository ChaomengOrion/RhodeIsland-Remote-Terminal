using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Cysharp.Threading.Tasks;
using NPinyin;

public class ImageTest : MonoBehaviour
{
    public Sprite sprite;
    public Color color;
    public int sample;
    public List<string> test;
    public string test2;

    [Button("Test")]
    public void Test()
    {
        Texture2D texture = sprite.texture;
        CalculateColor(texture);
        return;
    }

    public async UniTask SaveTexture2DToPath(Texture2D texture, string path)
    {
        await System.IO.File.WriteAllBytesAsync(path, DuplicateTexture(texture).EncodeToPNG());
    }

    public void CalculateColor(Texture2D texture)
    {
        System.DateTime dateTime = System.DateTime.Now;
        Texture2D copy = DuplicateTextureWithSize(texture, texture.height / sample, texture.width / sample);
        Debug.Log($"DuplicateTexture used {(System.DateTime.Now - dateTime).TotalMilliseconds}ms");
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
        color = new Color(sum.x, sum.y, sum.z);
        Debug.Log($"Calculated {copy.height}*{copy.width} with {colors.Length} pixels in {(System.DateTime.Now - dateTime).TotalMilliseconds}ms, with result {color}");
    }

    public static Texture2D DuplicateTextureWithSize(Texture2D source, int width, int height)
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

    public static Texture2D DuplicateTexture(Texture2D source)
    {
        RenderTexture renderTex = RenderTexture.GetTemporary(
                    source.width,
                    source.height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);

        Graphics.Blit(source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        Texture2D readableText = new(source.width, source.height);
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableText;
    }

    private static string GetCharPY(char c)
    {
        string str = c.ToString();

        if (c >= 32 && c <= 126)
        {
            return str;
        }

        byte[] array;
        array = System.Text.Encoding.Default.GetBytes(str);
        int i = (short)(array[0] - '\0') * 256 + ((short)(array[1] - '\0'));

        if (i < 0xB0A1) return "*";
        if (i < 0xB0C5) return "A";
        if (i < 0xB2C1) return "B";
        if (i < 0xB4EE) return "C";
        if (i < 0xB6EA) return "D";
        if (i < 0xB7A2) return "E";
        if (i < 0xB8C1) return "F";
        if (i < 0xB9FE) return "G";
        if (i < 0xBBF7) return "H";
        if (i < 0xBFA6) return "J";
        if (i < 0xC0AC) return "K";
        if (i < 0xC2E8) return "L";
        if (i < 0xC4C3) return "M";
        if (i < 0xC5B6) return "N";
        if (i < 0xC5BE) return "O";
        if (i < 0xC6DA) return "P";
        if (i < 0xC8BB) return "Q";
        if (i < 0xC8F6) return "R";
        if (i < 0xCBFA) return "S";
        if (i < 0xCDDA) return "T";
        if (i < 0xCEF4) return "W";
        if (i < 0xD1B9) return "X";
        if (i < 0xD4D1) return "Y";
        if (i < 0xD7FA) return "Z";
        return "*";

    }
}
