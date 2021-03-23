using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;



namespace WindowsFormsApp1.Game
{
    //Объект игрока
    class PlayerObject:GameObjects
    {

        public PlayerObject(Size panel_size) :base(panel_size)
        {

        }

        //Сдвигаем объект
        public override void MoveObject(Point MousePosition, Size sizepanel)
        {
                    
            double dist = Math.Sqrt(Math.Pow(MousePosition.X - center.X, 2) + Math.Pow(MousePosition.Y - center.Y, 2)); //расчёт расстояния

            //Console.WriteLine(dist);

            if (dist <= step) //сдвиг объекта
            {
                center = MousePosition;
            }
            else
            {
                //сдвиг на шаг к курсору
                center.X = (int)(center.X + (MousePosition.X - center.X) * (step / dist));
                center.Y = (int)(center.Y + (MousePosition.Y - center.Y) * (step / dist));
            }

        }
    }
}
