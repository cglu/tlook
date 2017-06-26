using System;
using WinFormsMvp;
using WinformsMvpBasics.Models;

namespace WinformsMvpBasics.Views.ViewContracts
{
    public interface IMainView : IView<MainViewModel>
    {
        event EventHandler CloseFormClicked;

        void Exit();
    }
}
