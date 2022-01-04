using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

//#nullable disable

namespace RentalKendaraan_110.Models
{
    public partial class Gender
    {
        public Gender()
        {
            Customers = new HashSet<Customer>();
        }

        [Required(ErrorMessage = "ID Gender hanya bisa diisi oleh angka")]
        public int IdGender { get; set; }

        [Required(ErrorMessage = "Nama Gender wajib diisi")]
        public string NamaGender { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
