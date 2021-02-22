using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Lab_2
{
    [Serializable]
    public class Worker
    {
        public string FIO;
        public int age;
        public int workExpirience;
        public string post;
        public string Work()
        {
            return "I`am working";
        }
        //public Worker(string fio, int age, int exp, string post)
        //{
        //    FIO = fio;
        //    this.age = age;
        //    workExpirience = exp;
        //    this.post = post;
        //}
    }
}
