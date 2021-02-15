using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    [Serializable]
    public class Plane
    {
        [Required]
        [Identif]
        public string id { get; set; }
        public string type;
        public string model;
        [Required(ErrorMessage ="Должно быть заполнено поле кол-ва мест")]
        public int seats;
        public DateTime dateProd;
    }
}
