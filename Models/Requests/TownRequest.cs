using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_DB.Models.Requests
{
    public class TownRequest
    {

        [Required(ErrorMessage ="Name dibutuhkan")]
        [MinLength(4, ErrorMessage ="Panjang karakter minimal 4 ")]
        [MaxLength(50, ErrorMessage ="Maksimal panjang karakter 50")]
        public string Name { get; set; }

    }
}
