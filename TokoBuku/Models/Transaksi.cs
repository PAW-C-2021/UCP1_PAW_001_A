using System;
using System.Collections.Generic;

#nullable disable

namespace TokoBuku.Models
{
    public partial class Transaksi
    {
        public Transaksi()
        {
            TransaksiDetails = new HashSet<TransaksiDetail>();
        }

        public string KodeTransaksi { get; set; }
        public DateTime Tanggal { get; set; }
        public decimal TotalNominal { get; set; }
        public int Qty { get; set; }
        public int IdUser { get; set; }

        public virtual User IdUserNavigation { get; set; }
        public virtual ICollection<TransaksiDetail> TransaksiDetails { get; set; }
    }
}
