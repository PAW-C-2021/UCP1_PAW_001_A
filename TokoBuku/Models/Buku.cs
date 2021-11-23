using System;
using System.Collections.Generic;

#nullable disable

namespace TokoBuku.Models
{
    public partial class Buku
    {
        public Buku()
        {
            TransaksiDetails = new HashSet<TransaksiDetail>();
        }

        public string KodeBuku { get; set; }
        public string Judul { get; set; }
        public string Penerbit { get; set; }
        public string Deskripsi { get; set; }
        public decimal Harga { get; set; }
        public int CreatedBy { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual ICollection<TransaksiDetail> TransaksiDetails { get; set; }
    }
}
