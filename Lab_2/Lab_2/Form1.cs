using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public Crew crew;
        public Form1()
        {
            InitializeComponent();
            crew = new Crew();
        }
        public List<Worker> wrkers = new List<Worker>();
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

                Plane plane = new Plane();
                plane.id = textBox5.Text;
                plane.model = comboBox2.Text;
                if (checkBox1.Checked)
                {
                    plane.type = checkBox1.Text;
                    count++;
                }
                else if (checkBox2.Checked)
                {
                    plane.type = checkBox2.Text;
                    count++;
                }
                else if (checkBox3.Checked)
                {
                    plane.type = checkBox3.Text;
                    count++;
                }
                if (count > 1)
                    throw new Exception("Должен быть выбран 1 тип самолета");
                plane.dateProd = dateTimePicker1.Value;
                crew.plane = plane;
                crew.workers = wrkers;
                XmlSerializer serializer = new XmlSerializer(typeof(Crew));
                using (FileStream stream = new FileStream("Crew.xml", FileMode.OpenOrCreate))
                {
                    serializer.Serialize(stream, crew);
                }
            MessageBox.Show("Данные были сохранены в файл");
            textBox4.Clear();
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
    }
}
