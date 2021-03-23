using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NAudio.Wave;
using NAudio.FileFormats;
using NAudio.CoreAudioApi;
using NAudio;
using WindowsFormsApp1.Game;

namespace WindowsFormsApp1
//{
//    public partial class Form1 : Form
//    {
//        public Form1()
//        {
//            InitializeComponent();
//        }
//
//        private void Form1_Load(object sender, EventArgs e)
//        {
//
//        }
//
//        private void button1_Click(object sender, EventArgs e)
//        {
//
//        }
//    }
//}


//namespace SpeechProject
{
    public partial class Form1 : Form
    {

        ClassGame Game;
        Recording Record; 

        public Form1()
        {
            InitializeComponent();
            Record=new Recording();
            Game = new ClassGame(panel1);
        }

        //Начинаем запись
        private void button1_Click(object sender, EventArgs e)
        {
            Record.StartRecord();
        }

        //Прерываем запись и конвертирование в цифры
        private void button2_Click(object sender, EventArgs e)
        {
            Record.StopRecord();
            Convert1.ProcConvert();
        }

        //Конвертирование аудио в цифры
        private void button3_Click(object sender, EventArgs e) 
        {
            Convert1.ProcConvert();
        }

        //Рисуем объект игрока
        private void button4_Click(object sender, EventArgs e)
        {

            Game.StartGame();
            //g.FillEllipse(new SolidBrush(Color.Black), 100, 100, 10, 10);
        }

        //Движение курсора по панели
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            Game.SetMousePosition(e.Location);
        }
    }
}