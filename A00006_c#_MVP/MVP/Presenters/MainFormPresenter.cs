using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVP.Models;
using MVP.Views;
using System.Windows.Forms;

namespace MVP.Presenters
{
    public class MainFormPresenter : Presenter<Views.IMainForm<Models.MainFormModel>>
    {
        public MainFormPresenter(IMainForm<MainFormModel> view) : base(view)
        {
            view.Model = new MainFormModel();
            
     view.ViewLoadEvent += On_ViewLoad;
                 view.ButtonSubmitEvent += On_ButtonSubmitClick;
                init();
        }
        public void init()
     {
            //To Do something...
            MessageBox.Show("init");
     }
 
     public void On_ViewLoad(object sender, EventArgs e)
     {
            //To Do something...
            MessageBox.Show("onload");
            //获取数据，交给view表现
            View.TestButton.Text = "onload";
        }
 
     public void On_ButtonSubmitClick(object sender, EventArgs e)
     {
         View.ShowSubmitDialog();//通过view的实例调用view的方法来改变控件形态
     }
}
}
