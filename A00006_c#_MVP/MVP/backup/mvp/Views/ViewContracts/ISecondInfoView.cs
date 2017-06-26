using System;
using System.Drawing;
using WinFormsMvp;
using WinformsMvpBasics.Models;

namespace WinformsMvpBasics.Views.ViewContracts
{
    public interface ISecondInfoView : IView<InfoControlModel>
    {
        event EventHandler PanelClicked;

        void ChangePanelColor(Color color);
    }
}
