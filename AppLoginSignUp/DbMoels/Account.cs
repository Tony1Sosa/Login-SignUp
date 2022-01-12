using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppLoginSignUp
{
    public partial class Account
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? PwdId { get; set; }
        public int? PcInfoId { get; set; }

        public virtual PcUserInfo PcInfo { get; set; }
        public virtual Password Pwd { get; set; }
        public virtual User User { get; set; }
    }
}
