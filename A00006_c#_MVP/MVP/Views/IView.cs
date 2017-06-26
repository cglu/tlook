using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsMvp;

namespace MVP.Views
{
    public interface IView<T> : IView
    {
   T Model { get; set; }
  
    }
}
