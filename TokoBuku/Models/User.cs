using System;
using System.Collections.Generic;

#nullable disable

namespace TokoBuku.Models
{
    public partial class User
    {
        public User()
        {
            Bukus = new HashSet<Buku>();
            Transaksis = new HashSet<Transaksi>();
        }

        public int IdUser { get; set; }
        public int IdRole { get; set; }
        public string Nama { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NoTelp { get; set; }
        public string Alamat { get; set; }

        public virtual Role IdRoleNavigation { get; set; }
        public virtual ICollection<Buku> Bukus { get; set; }
        public virtual ICollection<Transaksi> Transaksis { get; set; }
    }
}
