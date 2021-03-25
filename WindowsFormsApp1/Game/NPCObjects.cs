using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApp1.Game
{
    //Все объекты(NPC)
    class NPCObjects:GameObjects
    {
        private double angle_rad; //направление движения
        private int Hstep; //число шагов в одном направлении
        private int radobz; //радиус обзора объекта 
        private LinkedList<NPCObjects> WhoEat;//список, тех кто может съесть
        private NPCObjects target;//самый приоритетный объект 

        //конструктор, описывает, как создаётся нпс
        public NPCObjects(Size panel_size) : base(panel_size)//size_p - длина панели
        {
            radobz = 5 * radius; //радиус обзора
        }

        //Сдвигаем объект
        public override void MoveObject(Point MousePosition, Size sizepanel)
        {

            KudaMove(sizepanel);

            //if (0 > center.Y && 0 > center.X)
            //{
             //   angle_rad = Math.PI * 3 / 2;
             //   Hstep = 1;
            //}
            
            Hstep--; //шаги
            double stepx = Math.Cos(angle_rad) * step;  //свдиг по X и Y
            double stepy = Math.Sin(angle_rad) * step;
            center.X = (int)(center.X + stepx); //прибавление сдвига к координатам
            center.Y = (int)(center.Y - stepy);
        }

        public override void DrawObject(Graphics g)
        {
            base.DrawObject(g); //вызывается DrawObject из GameObjects

            g.DrawEllipse(new Pen(Color.Black), center.X - radobz, center.Y - radobz, 2 * radobz, 2 * radobz);//рисуем радиус обзора объекта

            //SolidBrush redBrush = new SolidBrush(Color.Blue);
            // Create rectangle for ellipse.
            //Rectangle rect = new Rectangle(center.X - radobz, center.Y - radobz, 2 * radobz, 2 * radobz);
            
            //float startAngle = (float)(360-((angle_rad * 180)/Math.PI))+60;
            //float sweepAngle = -120;
            //g.FillPie(redBrush, rect, startAngle, sweepAngle); //рисуем сектор обзора
        }

        public bool YouCanSeeMe(NPCObjects npc)//проверка видит конкретный нпс,  кого-то или нет
        {
            double d = Math.Sqrt(Math.Pow(npc.center.X - center.X, 2) + Math.Pow(npc.center.Y - center.Y, 2)); ;//расчёт расстояние между окружностями(объект и обзор) 
            if (d<=(npc.radius+radobz))//пересечение объекта и обзора
            {
                //double angle = Math.Atan2(npc.center.Y - center.Y, npc.center.X - center.X);//попадает ли объект в сектор
                //angle = (angle * 180) / Math.PI;//перевод в градусы
                //Console.WriteLine(angle);
                return true;
            }
            return false;
        }

        public void Vidno_ne(LinkedList<NPCObjects>List1)//проверка видно кого или нет
        {
            WhoEat = new LinkedList<NPCObjects>(); //новый список, от кого убегать
            target = null;
            for (int i=0; i<List1.Count; i++)
            {
                NPCObjects npc_obj = List1.ElementAt(i);//перебор всех нпс
                if (npc_obj != this)//проверка какой это нпс(что-бы не проверять самого себя)
                {
                    bool vidno = YouCanSeeMe(npc_obj);//нпс проверяет видит ли он другой объект   
                    if (vidno == true)
                    {
                        bool nadoubegat = Ubegat(npc_obj);
                        if (nadoubegat == true) //надо ли убегать?
                        {
                            WhoEat.AddLast(npc_obj); //добавляем в лист 
                        }
                        bool mojnoestb = Estb(npc_obj); 
                        if (mojnoestb == true) //можно есть?
                        {
                            if (target == null)
                            {
                                target = npc_obj;
                            }
                            else
                            {
                                if (npc_obj.radius > target.radius)//поиск наилучшей цели  МОЖНО ОДИН ИФ
                                {
                                    target = npc_obj;//выбирается новая цель
                                }
                            }
                        }
                    }
                }
               
            }
        }

        private bool Ubegat(NPCObjects npc_obj)//проверка убегать объекту или нет
        {
            double bolshe = npc_obj.radius / this.radius;//радиус объекта, который видно делится с текущим
            if (bolshe > 1.15)//есть можно, если на 15% меньше
            {
                return true;                
            }
            else
            {
                return false;
            }
        }

        private bool Estb(NPCObjects npc_obj)//проверка можно ли съесть или нет
        {
            double bolshe = this.radius / npc_obj.radius;//радиус текущего объекта, делится с который видно
            if (bolshe > 1.15)//есть можно, если на 15% меньше
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void KudaMove(Size sizepanel) //куда двигается (направление движения) 
            // 1)контроль границ, 2)убегание, 3)погоня, 4)рандомное движение
        {
            bool granb = MoveGranici(sizepanel); // контроль границ
            if (granb == false)
            {
                bool run = MoveUbeganie(); // убегание
                if (run == false)
                {
                    bool hunt = MovePogonia(); // погоня
                    if (hunt == false)
                    {
                        MoveRand(); // рандомное движение
                    }
                }
            }

        }

        private bool MoveGranici(Size sizepanel) // контроль границ
        {
            if (sizepanel.Width < center.X) // уехал вправо
            {
                angle_rad = Math.PI;
                Hstep = 1;
                return true;
            }

            if (sizepanel.Height < center.Y) //уехал вниз
            {
                angle_rad = Math.PI / 2;
                Hstep = 1;
                return true;
            }

            //if (sizepanel.Height < center.Y && sizepanel.Width < center.X)
            //{
            //    angle_rad = 2*Math.PI*135/360;
            //    Hstep = 1;
            //}

            if (0 > center.X) //уехал влево
            {
                angle_rad = 0;
                Hstep = 1;
                return true;
            }

            if (0 > center.Y) //уехал вверх
            {
                angle_rad = Math.PI * 3 / 2;
                Hstep = 1;
                return true;
            }
            return false;
        }

        private bool MoveUbeganie() // убегание
        {
            return false;
        }

        private bool MovePogonia() // погоня НАДА ПОЧИНИТЬ!!!!!!!!!!
        {
            if (target != null)
            {
                angle_rad = Math.Atan2(target.center.Y - center.Y, target.center.X - center.X); // движение в сторону цели
                Hstep = 5;
                Console.WriteLine("Поiхали");
                return true;
            }
            
            return false;
        }

        private void MoveRand() // рандомное движение
        {
            if (Hstep == 0)//выбор направления движения
            {
                int angle_deg = rnd.Next(0, 360); //определение направление движения npc
                angle_rad = (angle_deg * Math.PI) / 180;
                Hstep = rnd.Next(5, 20); //определение кол-ва шагов
            }
        }

    }
}
