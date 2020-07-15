using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Computers.ViewModels
{
    public interface ISaveViewModel
    {
        ICommand Exporter { get; }     
    }
}
