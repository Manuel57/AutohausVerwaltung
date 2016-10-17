using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string fileNameFiltered;


        private string fileName;
        public string FileName
        {
            get { return fileName; }
            private set
            {

                this.fileName = value;
                if (!string.IsNullOrEmpty(this.fileName))
                {
                    this.imageOrig.SetImagSource(this.fileName);
                    this.imageFiltered.SetImagSource(this.fileName);
                    this.enableAllButtons();
                }
            }
        }
        public MainWindow()
        {

            InitializeComponent();
            prog();
            disableAllButtons();
            ShowFileDialog();
            fileNameFiltered = "tmp.jpg";


        }



        private void ShowFileDialog()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            dialog.InitialDirectory = @"C:\";
            dialog.Title = "Please select an image file to encrypt.";
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string path = dialog.FileName;
                this.FileName = path;
            }

        }

        private void disableAllButtons()
        {
            this.btnRed.Disable();
            this.btnBlue.Disable();
            this.btnGreen.Disable();
        }
        private void enableAllButtons()
        {
            this.btnRed.Enable();
            this.btnBlue.Enable();
            this.btnGreen.Enable();
        }




        private void button_Click(object sender, RoutedEventArgs e)
        {

            Task.Run(() =>
            {

                this.Dispatcher.BeginInvoke(
                    (Action)(() =>
                    {
                        this.imageFiltered.ApplyRgbFilter((RgbColor)Enum.Parse(
            typeof(RgbColor),
            (sender as Button).Content.ToString().ToUpper()), reportProgress);
                        saveImage(this.imageFiltered);

                    }));


            });

        }

        private void saveImage(System.Windows.Controls.Image img)
        {
            BitmapImage bImg = img.Source as BitmapImage;

            byte[] bits;

            BitmapEncoder enc = new JpegBitmapEncoder();
            enc.Frames.Add(BitmapFrame.Create(bImg));

            using (MemoryStream ms = new MemoryStream())
            {
                enc.Save(ms);
                bits = ms.ToArray();
            }

            File.WriteAllBytes(this.fileNameFiltered, bits);
        }


        private void reportProgress(int progress)
        {
            this.Dispatcher.Invoke(() =>
                {

                    this.progress.Value = progress;
                });

        }
        private void prog()
        {
            progress.Minimum = 0;
            progress.Maximum = (int)(imageOrig.Height * imageOrig.Width);
            this.progress.Value = 0;
        }



        private void btnChooseImage_Click(object sender, RoutedEventArgs e)
        {
            ShowFileDialog();
        }

        private void btnMedianFilter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.medianFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void medianFilter()
        {
            if (string.IsNullOrEmpty(this.tbX.Text) ||
                   string.IsNullOrEmpty(this.tbY.Text))
            {
                throw new Exception("Bitte Werte für X und Y eingeben");
            }
            int x = int.Parse(this.tbX.Text);
            int y = int.Parse(this.tbY.Text);

            if (x % 2 == 0 || y % 2 == 0)
            {
                throw new Exception("Bitte ungerade Zahlen eingeben!");
            }
            try
            {
                Task.Run(() =>
                {
                    this.Dispatcher.BeginInvoke(
                        (Action)(() =>
                        {
                            try
                            {
                                this.imageFiltered.ApplyMedianFilter(x,
                                y, reportProgress, this.imageOrig);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }));

                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
