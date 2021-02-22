using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2.Momento
{
    public class MementoCrews
    {
        public List<Crew> crewList = new List<Crew>();
        public MementoCrews(List<Crew> state)
        {
            if (state != null)
            {
                foreach (Crew i in state)
                {
                    this.crewList.Add(i);
                }
            }
            else if (state == null)
                this.crewList.Clear();
        }
    }
    public class CaretakerCrews
    {
        public Stack<MementoCrews> History { get; private set; }
        public CaretakerCrews()
        {
            History = new Stack<MementoCrews>();
        }
    }
}
