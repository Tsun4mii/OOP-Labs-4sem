using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;
using Lab_2.Prototype;
using Lab_2.Momento;

namespace Lab_2
{
    [Serializable]
    public class Crew : IClone 
    {
        public List<Worker> workers;
        public Plane plane;
        public Crew()
        {
            workers = new List<Worker>();
            plane = new Plane();
        }
        public Crew(AbstractFactory.AbstractFactory factory)
        {
            workers = new List<Worker>();
            plane = factory.CreatePlane();
        }
        public Crew(List<Worker> workers, Plane plane)
        {
            this.workers = workers;
            this.plane = plane;
        }
        public IClone Clone()
        {
            return new Crew(workers, plane);
        }
    }
}
