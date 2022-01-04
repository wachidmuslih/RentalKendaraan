using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

//nullable disable

namespace RentalKendaraan_110.Models
{
    public partial class Jaminan
    {
        public Jaminan()
        {
            Peminjamen = new HashSet<Peminjaman>();
        }

        [Required(ErrorMessage = "ID Jaminan hanya bisa diisi oleh angka")]
        public int IdJaminan { get; set; }
        [Required(ErrorMessage = "Nama Jaminan  wajib diisi")]
        public string NamaJaminan { get; set; }

        public virtual ICollection<Peminjaman> Peminjamen { get; set; }
    }
}
