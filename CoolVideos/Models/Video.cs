using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoolVideos.Models
{
    public class Video
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        [StringLength(255)]
        public string Uri { get; set; }
        // TODO: Jakiś properties z samym filmem

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Display(Name = "Tytuł")]
        [Required(ErrorMessage = "Tytuł jest wymagany")]
        [StringLength(255)]
        public string Title { get; set; }

        [Display(Name = "Opis")]
        [Required(ErrorMessage = "Opis jest wymagany")]
        [StringLength(5000, ErrorMessage = "Opis może zawierać do 5000 znaków")]
        public string Description { get; set; }

        [StringLength(255)]
        public string Image { get; set; }

        [NotMapped]
        public int Likes { get; set; }
    }
}
