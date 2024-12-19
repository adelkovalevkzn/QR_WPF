using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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

using QRCoder;
using static System.Net.Mime.MediaTypeNames;


namespace QR_WPF
{
    /// <summary>
    /// Логика взаимодействия для Index.xaml
    /// </summary>
    public partial class Index : Page
    {
        public Index()
        {
            InitializeComponent();
        }
        private void GenerateQR(object sender, RoutedEventArgs e)
        {
            string url = UrlField.Text;
            using (QRCodeGenerator generator = new QRCodeGenerator())
            {
                QRCodeData data = generator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
                using (QRCode qrCode = new QRCode(data))
                {
                    Bitmap qrBitmap = qrCode.GetGraphic(20); // 20 - размер пикселей

                    // Конвертация Bitmap в BitmapSource для отображения в WPF
                    BitmapImage qrImage = ConvertBitmapToImageSource(qrBitmap);

                    // Устанавливаем изображение в ImageControl
                    QRImage.Source = qrImage;
                }
            }
        }
        private void ShowUsers(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Users());
        }
        private BitmapImage ConvertBitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                memoryStream.Position = 0;

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }
    }
}
