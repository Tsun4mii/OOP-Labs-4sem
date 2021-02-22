using Lab_2.AbstractFactory;
using Lab_2.Adapter;
using Lab_2.Builder;
using Lab_2.Momento;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Lab_2
{
    public partial class Form1 : Form
    {
        public static string lastMove;
        public Crew crew;
        public Crews crews = new Crews();
        public Crew crewsBackup = new Crew();
        public Crew buf = new Crew();
        public Form1()
        {
            InitializeComponent();
        }
        public List<Worker> wrkers = new List<Worker>();
        public Crews typeSearchResult = new Crews();
        public Crews seatsSearchResult = new Crews();
        public Crews modelSearchResults = new Crews();
        public static CaretakerCrews caretaker = new CaretakerCrews();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int exp;
                exp = Convert.ToInt32(textBox3.Text);
                Worker temp = new Worker();
                temp.FIO = textBox1.Text;
                temp.age = trackBar1.Value;
                temp.workExpirience = exp;
                temp.post = comboBox1.Text;
                foreach (Worker i in wrkers)
                {
                    if (temp.FIO == i.FIO)
                    {
                        throw new Exception("Попытка повторного добавления члена экипажа");
                    }
                }
                wrkers.Add(temp);
                textBox4.Text += temp.FIO + "\r\n";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label11.Text = string.Format($"Текущее значение: {trackBar1.Value}");
        }
        public int count = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            caretaker.History.Push(crews.SaveState());
            try
            {
                Crew crew;
                count = 0;
                if (checkBox1.Checked)
                {
                    crew = new Crew(new MilitaryFactory());
                    count++;
                }
                else if (checkBox2.Checked)
                {
                    crew = new Crew(new CivilFactory());
                    count++;
                }
                else
                {
                    crew = new Crew(new CargoFactory());
                    count++;
                }
                if (count > 1)
                    throw new Exception("Должен быть выбран 1 тип самолета");
                crew.plane.id = textBox5.Text;
                crew.plane.model = comboBox2.Text;
                crew.plane.dateProd = dateTimePicker1.Value;
                crew.plane.seats = Convert.ToInt32(textBox2.Text);
                Validate(crew.plane);
                crew.workers = wrkers;
                count = 0;
                XmlSerializer serializer = new XmlSerializer(typeof(Crew));
                using (FileStream stream = new FileStream("Crew.xml", FileMode.OpenOrCreate))
                {
                    serializer.Serialize(stream, crew);
                }
                crewsBackup = crew;
                crews.crews.Add(crew);
                wrkers.Clear();
                MessageBox.Show("Данные были сохранены в файл");
                textBox4.Clear();
            }
            catch(Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
        private static void Validate(Plane plane)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(plane);
            if (!Validator.TryValidateObject(plane, context, results, true))
            {
                throw new Exception(results[0].ErrorMessage);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Crew));
            using (FileStream stream = new FileStream("Crew.xml", FileMode.OpenOrCreate))
            {
                Crew c = (Crew)ser.Deserialize(stream);
                foreach(Worker i in c.workers)
                {
                    textBox4.Text += i.FIO + " ";
                }
            }
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (!Regex.IsMatch(textBox1.Text, @"(([А-Я][а-я]+(\s?))+$)|([А-Я][а-я]+(-?)([А-Я]?)([а-я]+)$)"))
            {
                textBox1.Tag = false;
            }
            else { textBox1.Tag = true; }
            Enable();
        }

        private void Enable()
        {
            button1.Enabled = (bool)textBox1.Tag;
        }

        private void поТипуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TypeSearchForm f2 = new TypeSearchForm();
            f2.Show(this);
            lastMove = "Поиск по типу";
            
        }

        private void поКолвуМестToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SeatsSearchForm f3 = new SeatsSearchForm();
            f3.Show(this);
            lastMove = "Поиск по местам";
        }

        private void поАвиакомпанииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModelSearchForm f4 = new ModelSearchForm();
            f4.Show(this);
            lastMove = "Поиск по модели";
        }

        private void идентификаторуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if(crews.crews.Count == 0)
                {
                    throw new Exception("Нет объектов для сортировки");
                }
                var idSort = from u in crews.crews
                             orderby u.plane.id
                             select u;
                StringBuilder str = new StringBuilder();
                foreach (Crew i in idSort)
                {
                    str.Append(Convert.ToString(i.plane.id) + "\n");
                }
                MessageBox.Show(str.ToString());
                lastMove = "Сортировка по идентификатору";
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Crews));
            if(typeSearchResult.crews.Count > 0)
            {
                using (FileStream stream = new FileStream("TypeSearch.xml", FileMode.OpenOrCreate))
                {
                    serializer.Serialize(stream, typeSearchResult);
                }
            }
            if(seatsSearchResult.crews.Count > 0)
            {
                using (FileStream stream = new FileStream("SeatsSearch.xml", FileMode.OpenOrCreate))
                {
                    serializer.Serialize(stream, seatsSearchResult);
                }
            }
            if (modelSearchResults.crews.Count > 0)
            {
                using (FileStream stream = new FileStream("ModelSearch.xml", FileMode.OpenOrCreate))
                {
                    serializer.Serialize(stream, modelSearchResults);
                }
            }
            lastMove = "Сохранение";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            toolStrip1.Visible = !toolStrip1.Visible;
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Текущее время - " + DateTime.Now.ToString() + "| Кол-во объектов - " + crews.crews.Count + "| " + lastMove;
        }

        private void датеОбслуживанияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (crews.crews.Count == 0)
                    throw new Exception("Нет объектов для сортировки");
                var idSort = from u in crews.crews
                             orderby u.plane.dateProd
                             select u;
                StringBuilder str = new StringBuilder();
                foreach (Crew i in idSort)
                {
                    str.Append(Convert.ToString(i.plane.id) + " " + i.plane.dateProd + "\n");
                }
                MessageBox.Show(str.ToString());
                lastMove = "Сортировка по дате обслуживания";
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Version: 1.0 \n Shust Yuri");
            lastMove = "О программе";
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (!(crews.crews.Contains(crewsBackup)))
            //        throw new Exception("Действие невозможно");
            //    buf = crewsBackup;
            //    crews.crews.Remove(crewsBackup);
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            crews.RestoreState(caretaker.History.Pop());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            StringBuilder str = new StringBuilder();
            foreach (Crew i in crews.crews)
            {
                str.Append(i.plane.id + " " + i.plane.model + " " + i.plane.type + " " + i.plane.seats + "\r\n");
            }
            MessageBox.Show(str.ToString());
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (crews.crews.Contains(buf))
                    throw new Exception("Действие невозможно");
                crews.crews.Add(buf);
                crewsBackup = buf;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public AppSettings Config { get; set; }
        private void button6_Click(object sender, EventArgs e)
        {
            Config = AppSettings.GetInstance(this.Size.ToString());
            MessageBox.Show(Config.settings);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            WorkerCreator creator = new WorkerCreator();
            BuilderW builder = new BuilderW();
            Worker test = creator.Create(builder, "Игорь А А", 32, 3);
            ITarget target = new Adapterr(test);
            MessageBox.Show(target.StartWork());
        }
    }
}
