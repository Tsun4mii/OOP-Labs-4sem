using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lab_6_7
{
    interface IUndo : ICommand
    {
        void Unexecute(object obj);
    }
}
