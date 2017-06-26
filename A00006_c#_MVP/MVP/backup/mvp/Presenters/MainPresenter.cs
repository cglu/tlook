using System;
using System.Collections.Generic;
using WinFormsMvp;
using WinformsMvpBasics.Models;
using WinformsMvpBasics.Views;
using WinformsMvpBasics.Views.ViewContracts;

namespace WinformsMvpBasics.Presenters
{
    public class MainPresenter : Presenter<IMainView>
    {
        public MainPresenter(IMainView view)
            : base(view)
        {
            View.CloseFormClicked += View_CloseFormClicked;
            View.Load += View_Load;
        }

        private void View_CloseFormClicked(object sender, EventArgs e)
        {
            View.Exit();
        }

        private void View_Load(object sender, EventArgs e)
        {
            View.Model = new MainViewModel
                             {
                                 MenuItems = new List<KeyValuePair<Type, string>>
                                                 {
                                                     new KeyValuePair<Type, string>(typeof (FirstInfoControl),
                                                                                    "FirstInfoContol"),
                                                     new KeyValuePair<Type, string>(typeof (SecondInfoUserControl),
                                                                                    "SecondInfoUserControl")
                                                 }
                             };
        }
    }
}
