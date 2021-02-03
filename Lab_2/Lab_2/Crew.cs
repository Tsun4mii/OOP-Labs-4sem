using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab_2
{
    [Serializable]
    public class Crew
    {
        public List<Worker> workers;
        public Plane plane;
        public Crew()
        {
            workers = new List<Worker>();
            plane = new Plane();
        }
    }
}
