using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6_7
{
    public class Part
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

        public Part(){}
        public Part(int id, string name, string desc, int quantity, int price, string imgpath)
        {
            Id = id;
            Name = name;
            Description = desc;
            Quantity = quantity;
            Price = price;
            ImagePath = imgpath;
        }

    }
}
