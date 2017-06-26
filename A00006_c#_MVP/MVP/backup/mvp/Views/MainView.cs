using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinFormsMvp.Forms;
using WinformsMvpBasics.Models;
using WinformsMvpBasics.Views.ViewContracts;

namespace WinformsMvpBasics.Views
{
    public class MainView : MvpForm<MainViewModel>, IMainView
    {
        #region Private Variables
        private Button exitButton;
        private ListBox menuListBox;
        private Label menuTitleLabel;
        private MvpUserControl<InfoControlModel> panel;
        private bool firstLoad = true; 
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
           
            InitializeComponent();

            menuListBox.DataSource = Model.MenuItems;
            menuListBox.DisplayMember = "Value";
        }

        #region IMainView members

        public event EventHandler CloseFormClicked;

        public void Exit()
        {
            Close();
        } 

        #endregion

        private void InitializeComponent()
        {
            exitButton = new Button();
            menuListBox = new ListBox();
            menuTitleLabel = new Label();
            SuspendLayout();

            // 
            // exitButton
            // 
            exitButton.Location = new Point(5, 220);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(75, 23);
            exitButton.Text = "Exit";
            exitButton.UseVisualStyleBackColor = true;
            exitButton.Click += exitButton_Click;

            // 
            // menuListBox
            // 
            menuListBox.Location = new Point(5, 38);
            menuListBox.Name = "AllDataViewButton";
            menuListBox.Size = new Size(180, 50);
            menuListBox.TabIndex = 0;
            menuListBox.SelectedIndexChanged += MenuListBox_SelectedIndexChanged;

            //
            // menuTitleLabel
            // 
            menuTitleLabel.Text = "Select a UserControl";
            menuTitleLabel.Location = new Point(5, 20);
            menuTitleLabel.AutoSize = true;
            menuTitleLabel.Font = new Font("Segoe UI Light", 8.2515F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            menuTitleLabel.Name = "MenuTitleLabel";
            menuTitleLabel.Size = new Size(63, 15);
            menuTitleLabel.TabIndex = 0;
            menuTitleLabel.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 250);
            Controls.Add(menuListBox);
            Controls.Add(menuTitleLabel);
            Controls.Add(exitButton);
            Name = "Main View";
            Text = "WinFormsMvp - Example App";

            StartPosition = FormStartPosition.CenterScreen;
            ResumeLayout(false);
        }

        void exitButton_Click(object sender, EventArgs e)
        {
            CloseFormClicked(this, EventArgs.Empty);
        }

        void MenuListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type typeOfControlToLoad = ((KeyValuePair<Type, string>)menuListBox.SelectedItem).Key;

            //  The next call creates the usercontrol. The presenter binding for the UserControl occurs now as it is instantiated.
            //  Place a break point in the constructor of the relevant presenter to observe it's instantiation.
            panel = (Activator.CreateInstance(typeOfControlToLoad) as MvpUserControl<InfoControlModel>);
            
            panel.Location = new Point(200, 10);

            if (Controls.Count > 0)
            {
                if (Controls.Count > 3)
                {
                    //  Get rid of the existing panel
                    var controlToDiscard = Controls[3];
                    Controls.Remove(controlToDiscard);
                    controlToDiscard.Dispose();

                    //  Add the new panel selected from the ListBox
                    Controls.Add(panel);
                }

                if (firstLoad)
                {
                    //  On first load, the panel is added.
                    Controls.Add(panel);
                    firstLoad = false;
                }

            }

        }
    }
}
