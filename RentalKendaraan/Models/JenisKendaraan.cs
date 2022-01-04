using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

//nullable disable

namespace RentalKendaraan_110.Models
{
    public partial class JenisKendaraan
    {
        public JenisKendaraan()
        {
            Kendaraans = new HashSet<Kendaraan>();
        }

        [Required(ErrorMessage = "ID Jenis Kendaraan hanya bisa diisi oleh angka")]
        public int IdJenisKendaraan { get; set; }

        [Required(ErrorMessage = "Nama Jenis Kendaraan wajib diisi")]
        public string NamaJenisKendaraan { get; set; }

        public virtual ICollection<Kendaraan> Kendaraans { get; set; }
    }
}
