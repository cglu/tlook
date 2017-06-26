using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace MVP.Views
{
    public interface IMainForm<T> : IView<T>
 {
     Button TestButton { get; }    //定义MainFrom的按钮引用
     TextBox TestTextBox { get; }    //定义MianForm的文本框引用
     event EventHandler ViewLoadEvent;    //定义窗体加载完毕执行事件
     event EventHandler ButtonSubmitEvent;    //定义按钮事件
     void ShowSubmitDialog();    //定义自定义的事件
 }
}
