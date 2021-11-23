using System;
using System.Collections.Generic;

#nullable disable

namespace TokoBuku.Models
{
    public partial class TransaksiDetail
    {
        public int IdTransaksiDetail { get; set; }
        public string KodeTransaksi { get; set; }
        public string KodeBuku { get; set; }

        public virtual Buku KodeBukuNavigation { get; set; }
        public virtual Transaksi KodeTransaksiNavigation { get; set; }
    }
}
