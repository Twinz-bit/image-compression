using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Image_Compression
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Обработка событий кнопки "Сжать"
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                CompressImageJpeg(textBox1.Text, Convert.ToInt32(comboBox1.Text)); //Сжатие изображения
                string NewAddressImages = textBox1.Text.Substring(0, textBox1.Text.Length - 4) + $"_сжато_на_{comboBox1.Text}.jpg"; //Путь до сжатого изображения
                pictureBox1.Image = Image.FromFile(NewAddressImages); //Вывод сжатого изображения на форму
                MessageBox.Show("Изображение успешно сжато и сохраненно по пути " + NewAddressImages);
            }
            catch
            {
                MessageBox.Show("Степень сжатия не выбрана или выбранна не коректно");
            }
        }

        //Выббор файла в ручную
        private void button2_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;

            //Открытие проводника
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\Users\\User\\Pictures"; //Задаёт изначальную папку в проводнике

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Получение пути к указанному файлу
                    filePath = openFileDialog.FileName;
                }
            }
            textBox1.Text = filePath; //Выводит путь в textBox1 для последующей работы с ним
        }

        //Загрузка изображения
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image = Image.FromFile(textBox1.Text); //Вывод изображения на форму
            }
            catch
            {
                MessageBox.Show("Изображение не выбрано или выбрано не коректно");
            }
        }

        /// <summary>
        /// Метод по сжатию изображения Jpeg
        /// </summary>
        /// <param name="AddressImages">Путь до изображения</param>
        /// <param name="compressionRatio">Степень сжатия изображения</param>
        static void CompressImageJpeg(string AddressImages, long compressionRatio)
        {
            using (Bitmap bmp = new Bitmap(AddressImages)) //Объект используемый для работы с изображениями
                bmp.Save( //Сохранение изображения
                    Path.ChangeExtension(AddressImages, "").Trim('.') + $"_сжато_на_{compressionRatio}.jpg",
                    ImageCodecInfo.GetImageEncoders()[1],
                    new EncoderParameters() //Само сжатие изображения
                    {
                        Param = new EncoderParameter[]
                        {
                            new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L - compressionRatio)
                        }
                    });
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
