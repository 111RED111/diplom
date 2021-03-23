using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace WindowsFormsApp1.Game
{
    //Общий класс объктов
    abstract class GameObjects
    {
        //protected Graphics g; //отрисовка шаров
        protected Point center; //центр шара 
        protected int radius; //размер шара
        protected Color color; //цвет шара
        static protected Random rnd = new Random(); //рандом, для характеристик
        protected int step; //шаг шара

        

        //Параметры объектов
        protected GameObjects(Size panel_size) //panel_size - размер панели
        {
            center = new Point(rnd.Next(0, panel_size.Width), rnd.Next(0, panel_size.Height));//рандомное место респауна
            radius = rnd.Next(10,40);//размер
            step = 50 - radius;//скорость движения
            color = new Color();//                       
        }

        //Рисуем объект
        public virtual void DrawObject(Graphics g) 
        {
            g.FillEllipse(new SolidBrush(Color.Red), center.X - radius, center.Y - radius, 2 * radius, 2 * radius);//рисуем объект            
        }

        //Сдвигаем объект
        public abstract void MoveObject(Point MousePosition, Size sizepanel);
        //{
        //    int step = 10;
        //    Point MousePosition1 = new Point(MousePosition.X - radius, MousePosition.Y - radius);

        //    double dist = Math.Sqrt(Math.Pow(MousePosition1.X - center.X, 2) + Math.Pow(MousePosition1.Y - center.Y, 2)); //расчёт расстояния

        //    Console.WriteLine(dist);

        //    if (dist <= step) //сдвиг объекта
        //    {
        //        center = MousePosition1;
        //    }
        //    else
        //    {
        //        //сдвиг на шаг к курсору
        //        center.X = (int)(center.X + (MousePosition1.X - center.X) * (step / dist));
        //        center.Y = (int)(center.Y + (MousePosition1.Y - center.Y) * (step / dist));
        //    }
        //}

    }
}
