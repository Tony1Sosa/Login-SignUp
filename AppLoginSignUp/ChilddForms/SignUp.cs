using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AppLoginSignUp.ChilddForms
{
    public partial class SignUp : Form
    {
        //fields
        private ApplicationContext context = new ApplicationContext();
        private List<User> users = new List<User>();
        StringBuilder sb = new StringBuilder();
        private Form1 fomr1;
        public SignUp(Form1 frm1)
        {
            InitializeComponent();
            fomr1 = frm1;
        }

        private void SignUp_Load(object sender, EventArgs e)
        {
            OutputPanel.Visible = false;
            PassworddTextBox.UseSystemPasswordChar = true;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            sb.Clear();
            outPutTextBox.Text = null;
            users = context.User.ToList();
            bool valid = true;
            string rgPattern = @"((?=.*\d)(?=.*[A-Z])(?=.*\W).{8,10})";

            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            string username = UsernameTextBox.Text;
            DateTime birthDay = dateTimePicker1.Value;
            string password = PassworddTextBox.Text;

            string hostName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;//User
            string[] arr = hostName.Split("\\").ToArray();
            string ComputerName = Environment.MachineName.ToString();//Pc Name
            string ipAdress = GetIPAddress();//Ip adress

            //validation fields
            string validUsername = users.FirstOrDefault(x => x.Username == username)?.ToString();
            string pcUsernameForCheck = context.Account.FirstOrDefault(x => x.PcInfo.PcName == ComputerName)?.ToString();
            string ipAdressForCheck = context.Account.FirstOrDefault(x => x.PcInfo.IpAress == ipAdress)?.ToString();

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName)
                || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                sb.AppendLine("Please fill all fields!");
                valid = false;
            }
            else
            {
                if (validUsername != null)
                {
                    sb.AppendLine($"The username: {username}, already exists!");
                    valid = false;
                }

                if (birthDay > DateTime.Now)
                {
                    sb.AppendLine($"Please enter valid birthday!");
                    valid = false;
                }

                if (!Regex.IsMatch(password, rgPattern))
                {
                    sb.AppendLine(@"Password must contain at least one digit.");
                    sb.AppendLine(@"Password must contain at least one uppercase character.");
                    sb.AppendLine(@"Password must contain at least one special symbol.");
                    sb.AppendLine(@"Password length must be between 8-10 symbols.");
                    valid = false;
                }

                if (pcUsernameForCheck != null)
                {
                    sb.AppendLine($"The Pc: {ComputerName} already have an account, please Log In!");
                    valid = false;
                }

                if (ipAdressForCheck != null)
                {
                    sb.AppendLine($"There is an existing account already on this network!");
                    valid = false;
                }
            }

            if (valid == false)
            {
                OutputPanel.Visible = true;
                outPutTextBox.Text = sb.ToString();
            }
            else
            {
                User newUser = new User()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Username = username,
                    BirthDay = birthDay
                };

                Password userPassword = new Password()
                {
                    Content = Encrypting(password)
                };

                PcUserInfo userInfo = new PcUserInfo()
                {
                    IpAress = ipAdress,
                    PcUsername = arr[1],
                    PcName = ComputerName
                };

                Account userAccount = new Account()
                {
                    User = newUser,
                    Pwd = userPassword,
                    PcInfo = userInfo
                };

                context.Account.Add(userAccount);
                context.SaveChanges();

                fomr1.StartChildForm(new Application(), sender);
            }
        }

        private void viewPasswordChechBox_CheckedChanged(object sender, EventArgs e)
        {
            if (viewPasswordChechBox.Checked)
            {
                PassworddTextBox.UseSystemPasswordChar = false;
            }

            if (!viewPasswordChechBox.Checked)
            {
                PassworddTextBox.UseSystemPasswordChar = true;
            }
        }

        public string Encrypting(string password)
        {
            string s = password;
            byte[] ASCIIValues = Encoding.ASCII.GetBytes(s);

            StringBuilder sb = new StringBuilder();
            StringBuilder bs = new StringBuilder();

            Random rnd = new Random();
            int n = rnd.Next(3);

            if (n == 1)// A
            {
                foreach (byte b in ASCIIValues)
                {
                    int nu = b;
                    nu += 10;
                    string nnn = nu.ToString();

                    sb.Append($"A{nnn.Length}");
                    bs.Append($"{nu}");
                }
            }
            else if (n == 2)//B
            {
                foreach (byte b in ASCIIValues)
                {
                    int nu = b;
                    nu += 20;
                    string nnn = nu.ToString();

                    sb.Append($"B{nnn.Length}");
                    bs.Append($"{nu}");
                }
            }
            else if (n == 3)//C
            {
                foreach (byte b in ASCIIValues)
                {
                    int nu = b;
                    nu += 30;
                    string nnn = nu.ToString();

                    sb.Append($"C{nnn.Length}");
                    bs.Append($"{nu}");
                }
            }
            else//D
            {
                foreach (byte b in ASCIIValues)
                {
                    int nu = b;
                    nu += 40;
                    string nnn = nu.ToString();

                    sb.Append($"D{nnn.Length}");
                    bs.Append($"{nu}");
                }
            }

            return $"{sb} {bs}";
        }

        static string GetIPAddress()
        {
            String address = "";
            WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
            using (WebResponse response = request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                address = stream.ReadToEnd();
            }

            int first = address.IndexOf("Address: ") + 9;
            int last = address.LastIndexOf("</body>");
            address = address.Substring(first, last - first);

            return address;
        }
    }
}
