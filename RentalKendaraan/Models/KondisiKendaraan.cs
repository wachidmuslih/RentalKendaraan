using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

//nullable disable

namespace RentalKendaraan_110.Models
{
    public partial class KondisiKendaraan
    {
        public KondisiKendaraan()
        {
            Pengembalians = new HashSet<Pengembalian>();
        }

        [Required(ErrorMessage = "ID Kondisi hanya bisa diisi oleh angka")]
        public int IdKondisi { get; set; }

        [Required(ErrorMessage = "Nama Kondisi tidak boleh kosong")]
        public string NamaKondisi { get; set; }

        public virtual ICollection<Pengembalian> Pengembalians { get; set; }
    }
}
