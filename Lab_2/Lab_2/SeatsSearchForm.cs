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
    public partial class SeatsSearchForm : Form
    {
        public SeatsSearchForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            Crews searchResults = new Crews();
            Form1 f1 = this.Owner as Form1;
            int border = Convert.ToInt32(textBox1.Text);
            foreach (Crew i in f1.crews.crews)
            {
                if(i.plane.seats >= border)
                {
                    searchResults.crews.Add(i);
                    textBox3.Text += i.plane.id + " " + i.plane.type + " " + i.plane.seats + "\r\n";
                }
            }
            f1.seatsSearchResult = searchResults;
        }
    }
}
