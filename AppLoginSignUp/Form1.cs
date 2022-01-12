using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppLoginSignUp.ChilddForms;

namespace AppLoginSignUp
{
    public partial class Form1 : Form
    {
        //fields
        private Button currentBtn;
        private Form currentForm;

        public Form1()
        {
            InitializeComponent();
            
        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            StartChildForm(new SignUp(this), sender);
        }
        private void LogInButton_Click_1(object sender, EventArgs e)
        {
           StartChildForm(new LogIn(this), sender);
        }

        private void ActivateBtn(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentBtn != (Button)btnSender)
                {
                    DisableBtn();
                    currentBtn = (Button)btnSender;
                    currentBtn.Font = new System.Drawing.Font("Showcard Gothic", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
                }
            }
        }

        private void DisableBtn()
        {
            foreach (Control previousBtn in mainScreenPannel.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(51, 51, 76);
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                }
            }
        }

        public virtual void StartChildForm(Form childForm, object btnSender)
        {
            if (currentForm != null)
            {
                currentForm.Close();
            }

            currentForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.mainScreenPannel.Controls.Add(childForm);
            this.mainScreenPannel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
