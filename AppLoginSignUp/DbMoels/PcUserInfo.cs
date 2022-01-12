using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppLoginSignUp
{
    public partial class PcUserInfo
    {
        public PcUserInfo()
        {
            Account = new HashSet<Account>();
        }

        public int Id { get; set; }
        public string IpAress { get; set; }
        public string PcUsername { get; set; }
        public string PcName { get; set; }

        public virtual ICollection<Account> Account { get; set; }
    }
}
