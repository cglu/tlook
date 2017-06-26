using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsMvp.Forms;
using WinformsMvpBasics.Models;
using WinformsMvpBasics.Views.ViewContracts;

namespace WinformsMvpBasics.Views
{
    public class FirstInfoControl : MvpUserControl<InfoControlModel>, IFirstInfoView
    {
        private Label infoLabel;
        private Panel infoPanel;
        private Label titleLabel;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            InitializeComponent();

            string[] totalTextForPanel = Model.Message.Split(';');

            titleLabel.Text = totalTextForPanel[0];
            infoLabel.Text = totalTextForPanel[1];
        }

        private void InitializeComponent()
        {
            infoLabel = new Label();
            titleLabel = new Label();
            infoPanel = new Panel();
            SuspendLayout();
            // 
            // TitleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI Light", 8.2515F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            titleLabel.Location = new Point(9, 8);
            titleLabel.Name = "TitleLabel";
            titleLabel.Size = new Size(63, 15);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "TitleLabel";
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // infoPanel
            // 
            infoPanel.BorderStyle = BorderStyle.FixedSingle;
            infoPanel.Location = new Point(9, 27);
            infoPanel.Name = "InfoPanel";
            infoPanel.Size = new Size(300, 150);
            infoPanel.TabIndex = 1;
            infoPanel.Click += InfoClick;

            // 
            // InfoLabel
            // 
            infoLabel.Location = new Point(4, 4);
            infoLabel.Name = "InfoLabel";
            BackColor = Color.White;
            infoLabel.Size = new Size(290, 140);
            infoLabel.TabIndex = 0;
            infoLabel.Text = "InfoLabel";
            infoLabel.Click += InfoClick;

            // 
            // FirstInfoControl
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(infoPanel);
            Controls.Add(titleLabel);
            infoPanel.Controls.Add(infoLabel);
            Name = "FirstInfoControl";
            Size = new Size(350, 180);
            ResumeLayout(false);
            PerformLayout();
        }

        void InfoClick(object sender, EventArgs e)
        {
            PanelClicked(this, EventArgs.Empty);
        }

        public event EventHandler PanelClicked;

        public void ClearPanel()
        {
            infoLabel.Text = string.Empty;
        }
    }
}
