using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsMvp;

namespace MVP.Presenters
{
    public abstract class Presenter<T> : IPresenter<T> where T : class, IView
  {
      private readonly T view;
  
      //这里的view作为引用，用于在presenter中获取View的实例
      protected Presenter(T view)
       {
      this.view = view;
      }
 
     public T View { get { return view; } }
 }
}
