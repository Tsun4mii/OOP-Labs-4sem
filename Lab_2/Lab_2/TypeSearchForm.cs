using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_2
{
    public partial class TypeSearchForm : Form
    {
        public TypeSearchForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Crews result = new Crews();
            Form1 f1 = this.Owner as Form1; 
            if(radioButton1.Checked)
            {
                foreach(Crew i in f1.crews.crews)
                {
                    if (i.plane.type == "Военный")
                    {
                        result.crews.Add(i);
                        textBox1.Text += i.plane.id + ", " + i.plane.type + "\r\n";
                    }
                }
            }
            if(radioButton2.Checked)
            {
                foreach (Crew i in f1.crews.crews)
                {
                    if (i.plane.type == "Пассажирский")
                    {
                        result.crews.Add(i);
                        textBox1.Text += i.plane.id + ", " + i.plane.type + "\r\n";
                    }
                }
            }
            if(radioButton3.Checked)
            {
                foreach (Crew i in f1.crews.crews)
                {
                    if (i.plane.type == "Грузовой")
                    {
                        result.crews.Add(i);
                        textBox1.Text += i.plane.id + ", " + i.plane.type + "\r\n";
                    }
                }
            }
            f1.typeSearchResult.crews = result.crews;
            
        }
    }
}
