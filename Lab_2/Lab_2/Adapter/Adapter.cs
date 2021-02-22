using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2.Adapter
{
    public interface ITarget
    {
        string StartWork();
    }
    public class Adapterr : ITarget
    {
        private readonly Worker worker;
        public Adapterr(Worker worker)
        {
            this.worker = worker;
        }
        public string StartWork()
        {
            return $"{this.worker.FIO} says: {this.worker.Work()}";
        }
    }
}
