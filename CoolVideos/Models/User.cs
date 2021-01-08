using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoolVideos.Models
{
    public class User
    {
        public int Id { get; set; }

        [StringLength(15, MinimumLength = 3, ErrorMessage = "Imię musi zawierać od 3 do 15 znaków")]
        public string FirstName { get; set; }

        [StringLength(20, MinimumLength = 3, ErrorMessage = "Nazwisko musi zawierać od 3 do 20 znaków")]
        public string LastName { get; set; }

        //[StringLength(35, MinimumLength = 5, ErrorMessage = "Email musi zawierać od 5 do 35 znaków")]
        //[EmailAddress(ErrorMessage = "Wprowadź poprawny email")]
        //[Required(ErrorMessage = "Email jest wymagany")]
        public string Email { get; set; }

        //[StringLength(255, MinimumLength = 6, ErrorMessage = "Hasło musi zawierać conajmniej 6 znaków")]
        //[Required(ErrorMessage = "Hasło jest wymagane")]
        public string Password { get; set; }

        public void toSmallUser()
        {
            Email = "";
            Password = "";
        }
    }
}
