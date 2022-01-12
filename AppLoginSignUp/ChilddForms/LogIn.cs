using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AppLoginSignUp.ChilddForms
{
    public partial class LogIn : Form
    {
        private StringBuilder sb = new StringBuilder();
        private ApplicationContext context = new ApplicationContext();
        private bool valid = true;
        private Form1 form1;

        public LogIn(Form1 frm1)
        {
            InitializeComponent();
            form1 = frm1;
        }

        private void LogIn_Load(object sender, EventArgs e)
        {
            PasswordTextBox.UseSystemPasswordChar = true;
            OutputPanel.Visible = false;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
            sb.Clear();
            outPutTextBox.Text = null;

            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;

            User loginUser = context.User.FirstOrDefault(x => x.Username == username);

            if (string.IsNullOrEmpty(username)||string.IsNullOrEmpty(password))
            {
                sb.AppendLine("Please fill all fields!");
                valid = false;
            }
            else
            {
                if (loginUser == null)
                {
                    sb.AppendLine("Invalid username!");
                    valid = false;
                }
                else
                {
                    Account account = context.Account.FirstOrDefault(x => x.UserId == loginUser.Id);
                    Password passwordE = context.Password.FirstOrDefault(x => x.Id == account.PwdId);

                    string decrypted = Decrypting(passwordE.Content);

                    if (decrypted == password)
                    {
                        form1.StartChildForm(new Application(), sender);
                    }
                    else
                    {
                        sb.AppendLine("Wrong password pal!");
                        valid = false;
                    }
                }
            }
            if (valid == false)
            {
                OutputPanel.Visible = true;
                outPutTextBox.Text = sb.ToString();
            }
        }

        private void viewPasswordChechBox_CheckedChanged(object sender, EventArgs e)
        {
            if (viewPasswordChechBox.Checked)
            {
                PasswordTextBox.UseSystemPasswordChar = false;
            }

            if (!viewPasswordChechBox.Checked)
            {
                PasswordTextBox.UseSystemPasswordChar = true;
            }
        }

        public static string Decrypting(string passworddContent)
        {
            Regex rgx = new Regex(@"([A-D]\d)+ \d+");
            bool pass = rgx.IsMatch(passworddContent);

            if (pass == false)
            {
                MessageBox.Show("Incorect Syntax", "Wrong Encrypted txt!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            string mo = passworddContent;
            char[] symbols = { 'A', 'B', 'C', 'D' };
            string[] crypto = mo.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();
            string[] possitioning = crypto[0].Split(symbols, StringSplitOptions.RemoveEmptyEntries);

            int decreaser = 0;

            if (mo.Contains("A"))
            {
                decreaser = 10;
            }

            if (mo.Contains("B"))
            {
                decreaser = 20;
            }

            if (mo.Contains("C"))
            {
                decreaser = 30;
            }

            if (mo.Contains("D"))
            {
                decreaser = 40;
            }

            string code = crypto[1];
            List<int> letters = new List<int>();
            string coded = code;

            foreach (var s in possitioning)
            {
                int n = int.Parse(s);
                int mn = int.Parse(coded.Substring(0, n));
                letters.Add(mn);

                coded = coded.Remove(0, n);
            }

            StringBuilder sbb = new StringBuilder();

            foreach (var letter in letters)
            {
                int ll = letter - decreaser;
                char ch = Convert.ToChar(ll);
                sbb.Append($"{ch}");
            }

            return sbb.ToString();
        }

    }
}
