using Lab_2.AbstractFactory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab_2
{
    [XmlInclude(typeof(MilitaryPlane))]
    [XmlInclude(typeof(CargoPlane))]
    [XmlInclude(typeof(CivilPlane))]
    [Serializable]
    public class Plane
    {
        [Required]
        [Identif]
        public string id { get; set; }
        public string type;
        public string model;
        [Required]
        public int seats;
        public DateTime dateProd;
    }
}
