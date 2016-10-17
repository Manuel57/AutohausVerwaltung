using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApplication1
{
    public static class Filter
    {
        /// <summary>
        /// 
        /// </summary>
        private static Action<int> execute = (int propress) => { };



        private unsafe static Action<int, IntPtr, int,
            int, int, int, int, int, int, IntPtr> filterMethod = delegate (int stride, IntPtr intPtr, int width,
             int heigt, int x, int y, int size, int size2, int color, IntPtr ptr2)
             { };
        public static void SetImagSource(this System.Windows.Controls.Image image, string imageSource)
        {
            image.DataContext = imageSource;
            image.Source = new BitmapImage(new Uri(imageSource, UriKind.RelativeOrAbsolute));
        }

        public static void Disable(this Button btn)
        {
            btn.IsEnabled = false;
        }
        public static void Enable(this Button btn)
        {
            btn.IsEnabled = true;
        }

        public static void ApplyRgbFilter(this System.Windows.Controls.Image src,
            RgbColor color, Action<int> callBack)
        {
            execute = callBack;
            filterMethod = rgbFilter;
            ApplyFilter(src, color, 0, 0, null);
        }


        public static void ApplyMedianFilter(this System.Windows.Controls.Image src,
            int sizeX, int sizeY, Action<int> callBack, System.Windows.Controls.Image orig)
        {
            try
            {
                execute = callBack;
                filterMethod = medianFilter;
                ApplyFilter(src, 0, sizeX, sizeY, orig);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private unsafe static void medianFilter(int stride, IntPtr intPtr, int width,
            int heigt, int x, int y, int size, int size2, int color, IntPtr intPtr2)
        {
            try
            {
                calculateMedian(stride, (byte*)intPtr.ToPointer(), width,
                   heigt, x, y, size, size2, (byte*)intPtr2.ToPointer());
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        private unsafe static void rgbFilter(int stride, IntPtr intPtr, int width,
            int heigt, int x, int y, int size, int size2, int color, IntPtr intPtr2)
        {
            byte* ptr = (byte*)intPtr2.ToPointer();
            byte[] arr = new byte[3] { 0, 0, 0 };
            arr[(int)color] = ptr[(x * 3) + y * stride + (int)color];
            for (int i = 0; i < arr.Length; i++)
            {
                ptr[(x * 3) + y * stride + i] = arr[i];
            }
        }



        private static void ApplyFilter(this System.Windows.Controls.Image src,
            RgbColor color, int size, int size2, System.Windows.Controls.Image orig)
        {
            try
            {
                Bitmap imgToTransform = null;
                imgToTransform = (Bitmap)(System.Drawing.Image.FromFile(src.DataContext.ToString()));

                Bitmap imgToTransform2 = null;
                imgToTransform2 = (Bitmap)(System.Drawing.Image.FromFile(src.DataContext.ToString()));


                DateTime start = DateTime.Now;

                BitmapData data = imgToTransform.LockBits(new System.Drawing.Rectangle(0, 0, imgToTransform.Width, imgToTransform.Height), ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                BitmapData data2 = imgToTransform2.LockBits(new System.Drawing.Rectangle(0, 0, imgToTransform2.Width, imgToTransform2.Height), ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                int stride = data.Stride;

                unsafe
                {
                    byte* ptr = (byte*)data.Scan0;
                    byte* ptr2 = (byte*)data2.Scan0;

                    for (int y = 0; y < imgToTransform.Height; y++)
                    {
                        for (int x = 0; x < imgToTransform.Width; x++)
                        {


                            filterMethod(stride, new IntPtr(ptr), imgToTransform.Width,
                 imgToTransform.Height, x, y, size, size2, (int)color, new IntPtr(ptr2));


                            execute((x * 3) + y * stride);
                        }
                    }
                }
                src.Source = imgToTransform2.ToBitmapImage();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static unsafe void calculateMedian(int stride, byte* ptr, int width,
            int heigt, int pxX, int pxY, int size, int size2, byte* ptr2)
        {
            try
            {
                int med = 0;
                if (pxX >= (size - 1) / 2 && pxX <= (width - (size - 1) / 2) && pxY >= (size - 1) / 2 && pxY <= (heigt - (size - 1) / 2) &&
                  (pxX >= (size2 - 1) / 2 && pxX <= (width - (size2 - 1) / 2) && pxY >= (size2 - 1) / 2 && pxY <= (heigt - (size2 - 1) / 2)))
                {

                    byte[] rectBlue = new byte[size * size2];
                    byte[] rectGreen = new byte[size * size2];
                    byte[] rectRed = new byte[size * size2];
                    int v = size / 2;
                    int w = size2 / 2;
                    for (int j = -v; j <= v; j++)
                    {
                        for (int i = -w; i <= w; i++)
                        {

                            rectBlue[(i + w) + (j + v) * 3] = ptr[((pxX + i) * 3) + (pxY + j) * stride];
                            rectGreen[(i + w) + (j + v) * 3] = ptr[((pxX + i) * 3) + (pxY + j) * stride + 1];
                            rectRed[(i + w) + (j + v) * 3] = ptr[((pxX + i) * 3) + (pxY + j) * stride + 2];

                        }
                    }
                    Array.Sort(rectBlue);
                    byte medBlue = rectBlue[rectBlue.Length / 2];
                    Array.Sort(rectGreen);
                    byte medGreen = rectGreen[rectBlue.Length / 2];
                    Array.Sort(rectRed);
                    byte medRed = rectRed[rectBlue.Length / 2];

                    ptr2[(pxX * 3) + pxY * stride + 2] = medRed;
                    ptr2[(pxX * 3) + pxY * stride + 1] = medGreen;
                    ptr2[(pxX * 3) + pxY * stride] = medBlue;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static BitmapImage ToBitmapImage(this Bitmap src)
        {
            MemoryStream ms = new MemoryStream();
            src.Save(ms, ImageFormat.Bmp);
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            img.StreamSource = ms;
            img.EndInit();
            return img;
        }

    }
}
