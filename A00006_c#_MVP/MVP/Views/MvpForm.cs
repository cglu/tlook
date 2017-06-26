using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsMvp;

namespace MVP.Views
{
    public partial class MvpForm : Form, IView
    {
        private readonly PresenterBinder presenterBinder = new PresenterBinder();
     public MvpForm()
     {

         presenterBinder.PerformBinding(this);    //注册关系
     }

        public bool ThrowExceptionIfNoPresenterBound
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
