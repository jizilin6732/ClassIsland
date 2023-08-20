﻿using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;
using System.Windows;
using System;

namespace ClassIsland;

/// <summary>
/// Source: https://www.cnblogs.com/kybs0/p/15768990.html
/// </summary>
public class BitmapConveters
{
    [DllImport("gdi32")]
    static extern int DeleteObject(IntPtr o);
    public static BitmapSource ConvertToBitMapSource(Bitmap bitmap)
    {
        IntPtr intPtrl = bitmap.GetHbitmap();
        BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(intPtrl,
            IntPtr.Zero,
            Int32Rect.Empty,
            BitmapSizeOptions.FromEmptyOptions());
        DeleteObject(intPtrl);
        return bitmapSource;
    }
    public static BitmapImage ConvertToBitmapImage(Bitmap bitmap, int? w=null, int? h=null)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            bitmap.Save(stream, ImageFormat.Bmp);
            stream.Position = 0;
            BitmapImage result = new BitmapImage();
            result.BeginInit();
            result.CacheOption = BitmapCacheOption.OnLoad;
            if (h != null)
            {
                    result.DecodePixelWidth = (int)((double)bitmap.Width / (double)bitmap.Height * (double)h);
            }
            else
            {
                   result.DecodePixelWidth = bitmap.Width;
            }
            result.DecodePixelHeight = h ?? bitmap.Height;
            result.StreamSource = stream;
            result.EndInit();
            result.Freeze();
            return result;
        }
    }
}