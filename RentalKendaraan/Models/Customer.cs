using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

//nullable disable

namespace RentalKendaraan_110.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Peminjamen = new HashSet<Peminjaman>();
        }

        [Required(ErrorMessage = "Hanya bisa diisi oleh angka")]
        public int IdCostumer { get; set; }

        [Required(ErrorMessage ="Nama Costumer wajib diisi")]
        public string NamaCostumer { get; set; }

        [Required(ErrorMessage = "NIK wajib diisi")]
        public string Nik { get; set; }

        [Required(ErrorMessage = "Alamat wajib diisi")]
        public string Alamat { get; set; }

        [MinLength(10, ErrorMessage = " ID Customer minimal 10 angka")]
        [MaxLength(12, ErrorMessage = " ID Customer maksimal 12 angka")]
        public string NoHp { get; set; }
        [Required(ErrorMessage = "Gender wajib diisi")]
        public int? IdGender { get; set; }

        public virtual Gender IdGenderNavigation { get; set; }
        public virtual ICollection<Peminjaman> Peminjamen { get; set; }
    }
}
