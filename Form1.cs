using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq; //Обработка XML в Linq
using System.Collections;
using System.Collections.ObjectModel;

namespace Linq_Examples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class Employers{
        public int Salary { get; set; }
        public string Name { get; set; }
        //Поле коллекции, тип которого относится к классу EmpSkill    
        //public EmpSkill[] Skills { get; set; }
        public List<EmpSkill> Skills { get; set; }
        
        }


        public class EmpSkill //:IEnumerable
        {
            public int SkillID { get; set; }
            public string SkillName { get; set; }
            public int SkillLevel { get; set; }

            /*public override void Add(string s1, string s2, string s3)
            {
                new EmpSkill(s1, s2, s3);
            }*/
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Все методы (стандартные операторы запросов) LINQ предназначены для обработки последовательностей, 
            //которые представляют собой объекты, типы (классы) которых реализуют интерфейсы
            //INumerable<T>, IQueryable<T>. Стандартные операторы запросов представляют возможности
            //запроса, включая фильтрацию, проекции, статистическую обработку, сортировку и многое другое
            /*
             Существуют два набора стандартных операторов запросов LINQ: один, работающий с объектами типа IEnumerable<T>, 
             и другой, работающий с объектами типа IQueryable<T>. Методы, составляющие каждый набор, 
             являются статическими членами классов Enumerable и Queryable соответственно. 
             Они определяются как методы расширения типа, с которым они работают. 
             Это значит, что их можно вызывать либо с помощью синтаксиса статического метода, 
             либо с помощью синтаксиса метода экземпляра.
             */

            //--Простой пример использования запроса LINQ запроса
            int[] SomeDigits = {1,2,3,4,5,6,7,8,9,10 };

            //Нетипизированная (var) переменная SelectedDigits1
            var SelectedDigits1 =
                from dg in SomeDigits
                where dg > 3
                select dg;

            richTextBox1.Text = "Простой пример работы оператора Select с фильтрацией массива цифр - dg>3 " + "\n";
            foreach(var x in SelectedDigits1)
            {
                richTextBox1.Text = richTextBox1.Text + " " + x;
            }
            //-----------------------------------------
            richTextBox1.Text = richTextBox1.Text + "\n" + "===============================" + "\n";



            //--Пример использования запроса LINQ запроса c в двойной конструкцией where
            richTextBox1.Text = richTextBox1.Text + "\n" + "Пример работы оператора Select с двойной фильтрацией - 2 конструкции where:" + "\n";            

            //Нетипизированная (var) переменная SelectedDigits2
            var SelectedDigits2 =
                from dg in SomeDigits
                where dg > 3
                where dg > 8
                select dg;

            foreach (var x in SelectedDigits2)
            {
                richTextBox1.Text = richTextBox1.Text + " " + x;
            }
            richTextBox1.Text = richTextBox1.Text + "\n" + "===============================" + "\n";
            //-----------------------------------------


            richTextBox1.Text = richTextBox1.Text + "\n" + "Пример работы оператора Select с коллекцией:" + "\n";

            //Инициализатор коллекции, в которой одно из полей (Skills) является другой коллекцией, 
            //который инициализируется аналогично

            List<Employers> worker = new List<Employers>        
            {

            new Employers(){Name = "Ivan", Salary=10000,   Skills= new List<EmpSkill>
                                                            {new EmpSkill {SkillID=1, SkillName="Programming on C#", SkillLevel=5},
                                                             new EmpSkill {SkillID=2, SkillName="Programming on Python", SkillLevel=6},
                                                             new EmpSkill {SkillID=3, SkillName="Programming on Java", SkillLevel=7}                                                             
                                                            }
                           },
            new Employers(){Name = "Dmitriy", Salary=10000,   Skills= new List<EmpSkill>
                                                            {new EmpSkill {SkillID=1, SkillName="Programming on C#", SkillLevel=15},
                                                             new EmpSkill {SkillID=2, SkillName="Programming on Python", SkillLevel=16},
                                                             new EmpSkill {SkillID=3, SkillName="Programming on Java", SkillLevel=17}                                                             
                                                            }
                           },                          
            new Employers(){Name = "Volodya", Salary=25000, Skills= new List<EmpSkill>
                                                            {new EmpSkill {SkillID=1, SkillName="Programming on C#", SkillLevel=25},
                                                             new EmpSkill {SkillID=2, SkillName="Programming on Python", SkillLevel=26},
                                                             new EmpSkill {SkillID=3, SkillName="Programming on Java", SkillLevel=27}                                                             
                                                            }
                           },
            new Employers(){Name = "Sasha", Salary=14000, Skills= new List<EmpSkill>
                                                            {new EmpSkill {SkillID=1, SkillName="Programming on C#", SkillLevel=35},
                                                             new EmpSkill {SkillID=2, SkillName="Programming on Python", SkillLevel=36},
                                                             new EmpSkill {SkillID=3, SkillName="Programming on Java", SkillLevel=37}                                                         
                                                            }
                           }
            };

            var BigSalary =
                from dg in worker
                where dg.Salary > 20000 && dg.Name == "Volodya"                
                select dg;

            foreach (var x in BigSalary)
            {
                richTextBox1.Text = richTextBox1.Text + " " + x.Name + ":" + x.Salary.ToString() + "\n";
            }
            richTextBox1.Text = richTextBox1.Text + "\n" + "===============================" + "\n";
            //-----------------------------------------

            richTextBox1.Text = richTextBox1.Text + "\n" + "Пример работы оператора Select с коллекцией у которой поле Skills является вложенной коллекцией:" + "\n";

            var BigSalaryShowSkill =
               from dg in worker
               where dg.Salary > 20000 && dg.Name == "Volodya"
               select dg;

            foreach (var x in BigSalaryShowSkill)
            {
                richTextBox1.Text = richTextBox1.Text + " " + x.Name + ":" + x.Salary.ToString() + "\n";
                foreach (var sk in x.Skills)
                {
                    richTextBox1.Text = richTextBox1.Text + " Skills: " + sk.SkillID.ToString() + " " + sk.SkillName + " " + sk.SkillLevel.ToString()+ "\n";
                } 
            }
            richTextBox1.Text = richTextBox1.Text + "\n" + "===============================" + "\n";
            //-----------------------------------------


            //-----------------------------------------

            richTextBox1.Text = richTextBox1.Text + "\n" + "Пример работы оператора Select с коллекцией и возвратом списка только по одному его полю:" + "\n";

            var AllEmployers =
               from dg in worker
               select dg.Name;

            foreach (var emplName in AllEmployers)
            {
                richTextBox1.Text = richTextBox1.Text + " " + emplName + "\n";               
            }
            richTextBox1.Text = richTextBox1.Text + "\n" + "===============================" + "\n";


           

            //-----------------------------------------

            richTextBox1.Text = richTextBox1.Text + "\n" + "Пример работы оператора Select с проекцией числового массива на массив строк:" + "\n";

            int[] Month = { 0, 1, 2, 3, 4, 5, 6 };
            string[] MonthStrings = { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль" };

            var MonthText =
                from m in Month
                select MonthStrings[m];

            foreach (var mn in MonthText)
            {
                richTextBox1.Text = richTextBox1.Text + " " + mn + "\n";    
            }

            richTextBox1.Text = richTextBox1.Text + "\n" + "===============================" + "\n";

            //-----------------------------------------


            //-----------------------------------------

            richTextBox1.Text = richTextBox1.Text + "\n" + "Пример работы оператора Select с преобразованием результирующих значений:" + "\n";

            
            

            var Workers =
                from w in worker
                //В данном случае LenghtName, SalaryWithBonus являются производными переменными, значение которых
                //вычислено на основании значений полей коллекции
                select new {LenghtName=w.Name.Length, SalaryWithBonus=w.Salary+1000, SalaryBig=(w.Salary>12000) };

            foreach (var wr in Workers)
            {
                richTextBox1.Text = richTextBox1.Text + " " + wr.LenghtName + "|" + wr.SalaryWithBonus + "|" + wr.SalaryBig + "\n";
            }

            richTextBox1.Text = richTextBox1.Text + "\n" + "===============================" + "\n";

            //-----------------------------------------


            richTextBox1.Text = richTextBox1.Text + "\n" + "Пример работы оператора Select сравнивающий два множества:" + "\n";

            int[] digits1 = { 0, 2, 4, 5, 6, 8, 9 };
            int[] digits2 = { 1, 3, 5, 7, 8 };

            var comb =
                from d1 in digits1
                from d2 in digits2
                where d1 == d2
                select new {d1, d2};




            foreach (var d in comb)
            {
                richTextBox1.Text = richTextBox1.Text + " " + d.d1 + "|" + d.d2 + "\n";
            }

            richTextBox1.Text = richTextBox1.Text + "\n" + "===============================" + "\n";

            //-----------------------------------------

            richTextBox1.Text = richTextBox1.Text + "\n" + "Пример работы оператора Select выбирающий данные на основании двух связанных коллекций:" + "\n";

            var workerSk =
                    from w in worker
                    from sk in w.Skills
                    where sk.SkillLevel>20
                    select new {w.Name, w.Salary, sk.SkillName, sk.SkillLevel};




            foreach (var w in workerSk)
            {
                richTextBox1.Text = richTextBox1.Text + " " + w.Name + "|" + w.Salary + "|" + w.SkillName + "|" + w.SkillLevel + "\n";
            }

            richTextBox1.Text = richTextBox1.Text + "\n" + "===============================" + "\n";

            //-----------------------------------------


            richTextBox1.Text = richTextBox1.Text + "\n" + "Пример работы оператора Select в котором для результирующего набора выполняется подзапрос:" + "\n";

            var workerNestedQuery =
                    from w in worker
                    where w.Salary>10000
                    from sk in w.Skills
                    where sk.SkillLevel > 30

                    select new { w.Name, w.Salary, sk.SkillName, sk.SkillLevel };




            foreach (var w in workerNestedQuery)
            {
                richTextBox1.Text = richTextBox1.Text + " " + w.Name + "|" + w.Salary + "|" + w.SkillName + "|" + w.SkillLevel + "\n";
            }

            richTextBox1.Text = richTextBox1.Text + "\n" + "===============================" + "\n";

            //-----------------------------------------
        }
    }
}
