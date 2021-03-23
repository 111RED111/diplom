using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NAudio.Wave;
using NAudio.FileFormats;
using NAudio.CoreAudioApi;
using NAudio;

namespace WindowsFormsApp1
{
    //Запись с микрфоона в файл
    class Recording
    {
        // WaveIn - поток для записи
        WaveIn waveIn;
        //Класс для записи в файл
        WaveFileWriter writer;
        //Имя файла для записи
        string outputFilename = "F:/VSProg/file.wav";

        //Получение данных из входного буфера и обработка полученных с микрофона данных
        void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
    //    if (this.InvokeRequired)
        //{
        //        this.BeginInvoke(new EventHandler<WaveInEventArgs>(waveIn_DataAvailable), sender, e);
        //}
        //    else
            {
                //Записываем данные из буфера в файл
                writer.WriteData(e.Buffer, 0, e.BytesRecorded);

            }

        }

        //Завершаем запись
        void StopRecording()
        {
            MessageBox.Show("StopRecording");
            waveIn.StopRecording();
            waveIn.Dispose();
            waveIn = null;
            writer.Close();
            writer = null;
        }

        //Окончание записи
        //private void waveIn_RecordingStopped(object sender, EventArgs e)
        //{
        //    if (this.InvokeRequired)
        //    {
        //        this.BeginInvoke(new EventHandler(waveIn_RecordingStopped), sender, e);
        //    }
        //    else
        //    {
        //        waveIn.Dispose();
        //        waveIn = null;
        //        writer.Close();
        //        writer = null;
        //    }
        //}

        //Начало записи
        public void StartRecord()
        {
            try
            {
                MessageBox.Show("Start Recording");
                waveIn = new WaveIn();
                //Дефолтное устройство для записи (если оно имеется)
                waveIn.DeviceNumber = 0;
                //Прикрепляем к событию DataAvailable обработчик, возникающий при наличии записываемых данных
                waveIn.DataAvailable += waveIn_DataAvailable;
                //Прикрепляем обработчик завершения записи
                //waveIn.RecordingStopped += new EventHandler(waveIn_RecordingStopped);
              //  waveIn.RecordingStopped += waveIn_RecordingStopped;
                //Формат wav-файла - принимает параметры - частоту дискретизации и количество каналов(здесь mono)
                waveIn.WaveFormat = new WaveFormat(8000, 1);
                //Инициализируем объект WaveFileWriter
                writer = new WaveFileWriter(outputFilename, waveIn.WaveFormat);
                //Начало записи
                waveIn.StartRecording();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Остановка записи
        public void StopRecord()
        {
            if (waveIn != null)
            {
                StopRecording();
            }
        }
    }
}
