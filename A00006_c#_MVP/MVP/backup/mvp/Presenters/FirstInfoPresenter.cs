using WinFormsMvp;
using WinformsMvpBasics.Models;
using WinformsMvpBasics.Views.ViewContracts;

namespace WinformsMvpBasics.Presenters
{
    public class FirstInfoPresenter : Presenter<IFirstInfoView>
    {
        public FirstInfoPresenter(IFirstInfoView view) : base(view)
        {
            View.Load += View_Load;
            View.PanelClicked += View_PanelClicked;
        }

        void View_PanelClicked(object sender, System.EventArgs e)
        {
            View.ClearPanel();
        }

        void View_Load(object sender, System.EventArgs e)
        {
            View.Model = new InfoControlModel { Message = "Convention bound;This control's presenter was bound by convention. The View is called FirstInfoControl and lives in the Views directory. The Presenter is called FirstInfoPresenter and lives in the Presenters directory. Both classes have the prefix \"FirstInfo\". As the View's name ends in \"Control\" and the Presenter's name ends in \"Presenter, the binder has enough information to perform the binding without any specific/express binding (i.e. outside of the framework itself)." };
        }
    }
}
