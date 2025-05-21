using UnityEngine;

namespace BloodShadow.Unity.GameCore.Extension
{
    public static class Extension
    {
        public static byte[] ToByteArray(this Sprite sprite) => sprite.texture.EncodeToPNG();
        public static Sprite FromByteArray(this byte[] data)
        {
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(data);
            return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(.5f, .5f));
        }
        public static Color Lerp(this in Color a, in Color b, in float t) =>
            new Color(Mathf.Lerp(a.r, b.r, t), Mathf.Lerp(a.g, b.g, t), Mathf.Lerp(a.b, b.b, t), Mathf.Lerp(a.a, b.a, t));

        public static Texture2D MakingReadable(this Texture2D sourceTex)
        {
            Texture2D result = new Texture2D(sourceTex.width, sourceTex.height, TextureFormat.RGBA32, false);

            RenderTexture tmp = RenderTexture.GetTemporary(result.width, result.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
            Graphics.Blit(sourceTex, tmp);
            result.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);
            result.Apply();
            RenderTexture.ReleaseTemporary(tmp);

            return result;
        }
    }
}
