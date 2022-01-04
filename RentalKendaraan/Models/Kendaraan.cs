using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

//nullable disable

namespace RentalKendaraan_110.Models
{
    public partial class Kendaraan
    {
        public Kendaraan()
        {
            Peminjamen = new HashSet<Peminjaman>();
        }

        [Required(ErrorMessage = "ID  Kendaraan hanya bisa diisi oleh angka")]
        public int IdKendaraan { get; set; }

        [Required(ErrorMessage = "Nama Kendaraan wajib diisi")]
        public string NamaKendaraan { get; set; }
        [Required(ErrorMessage = "Nomor Polisi wajib diisi")]
        public string NoPolisi { get; set; }

        [Required(ErrorMessage = "Nomor STNK wajib diisi")]
        public string NoStnk { get; set; }

        [Required(ErrorMessage = "ID Jenis Kendaraan hanya bisa diisi oleh angka")]
        public int? IdJenisKendaraan { get; set; }

        [Required(ErrorMessage = "Ketersediaan wajib diisi")]
        public string Ketersediaan { get; set; }

        public virtual JenisKendaraan IdJenisKendaraanNavigation { get; set; }
        public virtual ICollection<Peminjaman> Peminjamen { get; set; }
    }
}
