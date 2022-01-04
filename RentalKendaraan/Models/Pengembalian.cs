using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

//nullable disable

namespace RentalKendaraan_110.Models
{
    public partial class Pengembalian
    {
        [Required(ErrorMessage = "Id Pengembalian hanya bisa diisi oleh angka")]
        public int IdPengembalian { get; set; }
        [Required(ErrorMessage = "Tanggal Pengembalian wajib diisi")]
        public DateTime? TglPengembalian { get; set; }
        [Required(ErrorMessage = "Id Peminjaman hanya bisa diisi oleh angka")]
        public int? IdPeminjaman { get; set; }

        [Required(ErrorMessage = "Id Kondisi hanya bisa diisi oleh angka")]
        public int? IdKondisi { get; set; }
        [Required(ErrorMessage = "Denda hanya bisa diisi oleh angka")]
        public int? Denda { get; set; }

        public virtual KondisiKendaraan IdKondisiNavigation { get; set; }
        public virtual Peminjaman IdPeminjamanNavigation { get; set; }
    }
}
