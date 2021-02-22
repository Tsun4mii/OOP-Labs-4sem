using Lab_2.Momento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    [Serializable]
    public class Crews
    {
        public List<Crew> crews = new List<Crew>();
        public void RestoreState(MementoCrews memento)
        {
            crews = memento.crewList;
        }
        public MementoCrews SaveState()
        {
            return new MementoCrews(crews);
        }
    }
}
