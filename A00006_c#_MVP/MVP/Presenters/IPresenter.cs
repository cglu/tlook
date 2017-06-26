using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsMvp;

namespace MVP.Presenters
{
    public interface IPresenter<T> : IPresenter where T : class, IView
 {
     T View { get; }
 }
}
