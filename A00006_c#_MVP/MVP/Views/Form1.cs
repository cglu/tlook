using MVP.Models;
using MVP.Presenters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVP
{
    [Views.PresenterBinding(typeof(MainFormPresenter))]
    public partial class Form1 : Views.MvpForm, Views.IMainForm<MainFormModel>
    {
        public Form1()
        {
            InitializeComponent();
        }

        public MainFormModel Model { get; set; }

        public Button TestButton
        {
            get
            {
                return button1;
            }
        }

        public TextBox TestTextBox
        {
            get
            {
                return textBox1;
            }
        }

        public event EventHandler ButtonSubmitEvent;
        public event EventHandler ViewLoadEvent;

        public void ShowSubmitDialog()
        {
            MessageBox.Show("to submit?");
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (ViewLoadEvent != null) ViewLoadEvent(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ButtonSubmitEvent != null) ButtonSubmitEvent(sender, e);
        }
    }
}
