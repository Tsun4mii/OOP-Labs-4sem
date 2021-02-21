using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2.AbstractFactory
{
    public abstract class AbstractFactory
    {
        public abstract Plane CreatePlane();
    }
    
    public class MilitaryFactory : AbstractFactory
    {
        public override Plane CreatePlane()
        {
            return new MilitaryPlane();
        }
    }
    public class CivilFactory : AbstractFactory
    {
        public override Plane CreatePlane()
        {
            return new CivilPlane();
        }
    }
    public class CargoFactory : AbstractFactory
    {
        public override Plane CreatePlane()
        {
            return new CargoPlane();
        }
    }
    [Serializable]
    public class MilitaryPlane : Plane
    {
        public MilitaryPlane()
        {
            type = "Военный";
        }
    }
    [Serializable]
    public class CivilPlane : Plane
    {
        public CivilPlane()
        {
            type = "Пассажирский";
        }
    }
    [Serializable]
    public class CargoPlane : Plane
    {
        public CargoPlane()
        {
            type = "Грузовой";
        }
    }
}
