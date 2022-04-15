using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System;

[RequireComponent(typeof(RawImage))]
public class UIGif : MonoBehaviour
{
    private RawImage rawImage;
    public Texture2D texture;
    [Range(10, 60)]
    public int playFrame = 24;
    private float time;
    private List<Texture2D> texture2Ds = new List<Texture2D>();
    private int curframe = 0;
    // Start is called before the first frame update
    async void Start()
    {
        rawImage = GetComponent<RawImage>();
        byte[] data2 = texture.GetRawTextureData();
        byte[] data = await FastDev.FileUtil.ReadFile("Assets/1.gif");
        using (MemoryStream memoryStream = new MemoryStream(data))
        {
            System.Drawing.Image image = System.Drawing.Image.FromStream(memoryStream, false, true);
            LoadGif(image);
        }
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= 1f / playFrame)
        {
            rawImage.texture = texture2Ds[curframe];
            curframe++;
            if (curframe >= texture2Ds.Count)
                curframe = 0;
            time = 0;
        }
    }

    private void LoadGif(System.Drawing.Image image)
    {
        FrameDimension frameDimension = new FrameDimension(image.FrameDimensionsList[0]);
        int framCount = image.GetFrameCount(frameDimension);
        for (int i = 0; i < framCount; i++)
        {
            image.SelectActiveFrame(frameDimension, i);
            var framBitmap = new Bitmap(image.Width, image.Height);
            System.Drawing.Graphics.FromImage(framBitmap).DrawImage(image, Point.Empty);
            var frameTexture2D = new Texture2D(framBitmap.Width, framBitmap.Height);
            for (int x = 0; x < framBitmap.Width; x++)
            {
                for (int y = 0; y < framBitmap.Height; y++)
                {
                    System.Drawing.Color sourceColor = framBitmap.GetPixel(x, y);
                    frameTexture2D.SetPixel(x, framBitmap.Height - 1 - y, new Color32(sourceColor.R, sourceColor.G, sourceColor.B, sourceColor.A));
                }
            }
            frameTexture2D.Apply();
            texture2Ds.Add(frameTexture2D);
        }
    }
}
