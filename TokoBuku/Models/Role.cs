using System;
using System.Collections.Generic;

#nullable disable

namespace TokoBuku.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int IdRole { get; set; }
        public string NamaRole { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
