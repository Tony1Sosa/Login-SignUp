using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppLoginSignUp
{
    public partial class User
    {
        public User()
        {
            Account = new HashSet<Account>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public DateTime? BirthDay { get; set; }

        public virtual ICollection<Account> Account { get; set; }
    }
}
