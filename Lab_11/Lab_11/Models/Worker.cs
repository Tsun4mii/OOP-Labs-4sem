using Lab_11.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab_11.Models
{
    public class Worker
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public int planeId { get; set; }

        public static async Task addWorker(Worker w)
        {
            WorkerContext wc = new WorkerContext();
            try
            {
                wc.Worker.Add(w);
                await wc.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void GetNameById(int id)
        {
            WorkerContext wc = new WorkerContext();
            var result = wc.Worker.Where(p => p.id == id);
            StringBuilder str = new StringBuilder();
            foreach( Worker i in result)
            {
                str.Append(i.name + "\r\n");
            }
            MessageBox.Show(str.ToString());
        }

        //public static void UpdateItem(string name)
        //{
            
        //}
    }

}
