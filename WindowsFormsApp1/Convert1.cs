using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NAudio.Wave;
using NAudio.FileFormats;
using NAudio.CoreAudioApi;
using NAudio;

namespace WindowsFormsApp1
{
    //Конвертирование аудио в цифры
    class Convert1
    {
        public static void ProcConvert()
        {
            using (WaveFileReader reader = new WaveFileReader("F:/VSProg/file.wav"))
            {
               // Assert.AreEqual(16, reader.WaveFormat.BitsPerSample, "Only works with 16 bit audio");
                byte[] buffer = new byte[reader.Length];
                int read = reader.Read(buffer, 0, buffer.Length);
                //short[] sampleBuffer = new short[read / 2];
                //Buffer.BlockCopy(buffer, 0, sampleBuffer, 0, read);

                //Console.WriteLine(sampleBuffer);
                for (int i = 44; i < buffer.Length; i++) //в wav даееые с 44 байта
                {
                    //Console.WriteLine(buffer[i]);
                    Console.WriteLine(Convert.ToString(buffer[i], 2));
                }
            }
        }
    }
}
