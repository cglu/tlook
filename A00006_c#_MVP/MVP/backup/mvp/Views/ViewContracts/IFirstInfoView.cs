using System;
using WinFormsMvp;
using WinformsMvpBasics.Models;

namespace WinformsMvpBasics.Views.ViewContracts
{
    public interface IFirstInfoView : IView<InfoControlModel>
    {
        event EventHandler PanelClicked;

        void ClearPanel();
    }
}
