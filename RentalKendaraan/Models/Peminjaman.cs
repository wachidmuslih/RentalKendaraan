using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

//nullable disable

namespace RentalKendaraan_110.Models
{
    public partial class Peminjaman
    {
        public Peminjaman()
        {
            Pengembalians = new HashSet<Pengembalian>();
        }

        [Required(ErrorMessage = "Id Peminjaman hanya bisa diisi oleh angka")]
        public int IdPeminjaman { get; set; }

        [Required(ErrorMessage = "Tanggal Peminjaman tidak boleh kosong")]
        public DateTime? TglPeminjaman { get; set; }

        [Required(ErrorMessage = "Id Kendaraan hanya bisa diisi oleh angka")]
        public int? IdKendaraan { get; set; }
        [Required(ErrorMessage = "Id Costumer hanya bisa diisi oleh angka")]
        public int? IdCostumer { get; set; }
        [Required(ErrorMessage = "Id Jaminan hanya bisa diisi oleh angka")]
        public int? IdJaminan { get; set; }
        [Required(ErrorMessage = "Biaya hanya bisa diisi oleh angka")]
        public int? Biaya { get; set; }

        public virtual Customer IdCostumerNavigation { get; set; }
        public virtual Jaminan IdJaminanNavigation { get; set; }
        public virtual Kendaraan IdKendaraanNavigation { get; set; }
        public virtual ICollection<Pengembalian> Pengembalians { get; set; }
    }
}
