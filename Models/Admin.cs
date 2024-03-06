using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S6L1.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [Required(ErrorMessage = "Nome Utente Obbligatorio")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Min 3 e max 20 caratteri")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password Obbligatoria")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Min 8 e max 15 caratteri")]
        public string Password { get; set; }

    }
}