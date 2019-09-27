using MessagingToolkit.QRCode.Codec.Data;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace QRcoder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEncode_Click(object sender, EventArgs e)
        {
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;//оптимизируем изображение под pictureBox
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "JPEG|*.jpg|PNG|*.png|BMP|*.bmp", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    txtDecode.Text = null;//очищаем Decode TextBox
                    MessagingToolkit.QRCode.Codec.QRCodeEncoder encoder = new MessagingToolkit.QRCode.Codec.QRCodeEncoder(); //создатётся новая генерация QR-кода
                    encoder.QRCodeScale = 8;
                    Bitmap bmp = encoder.Encode(txtEncode.Text); //кодируем слово, введенное в Encode TextBox, в переменную bmp класса Bitmap(класс для работы с изобраениями)
                    pictureBox.Image = bmp;//выводим QR-код, как изображениее
                    bmp.Save(sfd.FileName, ImageFormat.Jpeg);//сохраниение
                }
            }
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;//оптимизируем изображение под pictureBox
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtEncode.Text = null;//очищаем Encode TextBox
                    pictureBox.Image = Image.FromFile(ofd.FileName);//открываем изображение
                    MessagingToolkit.QRCode.Codec.QRCodeDecoder decoder = new MessagingToolkit.QRCode.Codec.QRCodeDecoder();//раскодирование изображения 
                    txtDecode.Text = decoder.Decode(new QRCodeBitmapImage(pictureBox.Image as Bitmap));//запись раскодированного изобр
                }
            }
        }
    }
}
