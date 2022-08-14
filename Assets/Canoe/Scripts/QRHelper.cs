using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.Common;

public class QRHelper
{

    /// <summary>
    /// 生成2维码
    /// 经测试：能生成任意尺寸的正方形
    /// </summary>
    /// <param name="content"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public static Texture2D GenerateQRImage(string content, int width=256, int height=256)
    {
        // 编码成color32
        MultiFormatWriter writer = new MultiFormatWriter();
        Dictionary<EncodeHintType, object> hints = new Dictionary<EncodeHintType, object>();
        hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
        hints.Add(EncodeHintType.MARGIN, 1);
        hints.Add(EncodeHintType.ERROR_CORRECTION, ZXing.QrCode.Internal.ErrorCorrectionLevel.M);
        BitMatrix bitMatrix = writer.encode(content, BarcodeFormat.QR_CODE, width, height, hints);

        // 转成texture2d
        int w = bitMatrix.Width;
        int h = bitMatrix.Height;
        // Debug.Log(string.Format("w={0},h={1}", w, h));
        Texture2D texture = new Texture2D(w, h);
        for (int x=0; x<h; x++)
        {
            for(int y=0; y<w; y++)
            {
                if(bitMatrix[x,y])
                {
                    texture.SetPixel(y, x, Color.black);
                }
                else
                {
                    texture.SetPixel(y, x, Color.white);
                }
            }
        }
        texture.Apply();
        return texture;
        // img.texture = texture;
        //// 存储成文件
        //byte[] bytes = texture.EncodeToPNG();
        //string path = System.IO.Path.Combine(Application.dataPath, "qr.png");
        //System.IO.File.WriteAllBytes(path, bytes);
    }
}
