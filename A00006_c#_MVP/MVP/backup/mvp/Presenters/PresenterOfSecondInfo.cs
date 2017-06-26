using System.Drawing;
using WinFormsMvp;
using WinformsMvpBasics.Models;
using WinformsMvpBasics.Views.ViewContracts;

namespace WinformsMvpBasics.Presenters
{
    public class PresenterOfSecondInfo : Presenter<ISecondInfoView>
    {
        public PresenterOfSecondInfo(ISecondInfoView view) : base(view)
        {
            View.Load += View_Load;
            view.PanelClicked += view_PanelClicked;
        }

        void view_PanelClicked(object sender, System.EventArgs e)
        {
            View.ChangePanelColor(Color.LightSteelBlue);
        }

        void View_Load(object sender, System.EventArgs e)
        {
            View.Model = new InfoControlModel { Message = "Attribute bound;This control's presenter was bound by decorating it's class name with a PresenterBinding attribute - [PresenterBinding(typeof(PresenterOfSecondInfo))]" };
        }
    }
}
