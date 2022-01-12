using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AppLoginSignUp.ChilddForms
{
    public partial class Application : Form
    {
        public Application()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
        }

        private void Application_Load(object sender, EventArgs e)
        {
            label1.Visible = false;
        }
    }
}
