using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MenaxhimiIKinemase.Models
{
    public class Film
    {
        [Key]
        public int FilmiID { get; set; }

        [Required(ErrorMessage = "Duhet te shenoni titullin e filmit")]
        [StringLength(150, MinimumLength = 3)]
        public string Titulli { get; set; }

        [Required(ErrorMessage = "Duhet te zgjedhni nje date")]
        [Display(Name = "Premiera")]
        public DateTime Premiera { get; set; }

        [Required(ErrorMessage = "Duhet te shenoni regjisorin e filmit")]
        public string Regjisori { get; set; }

        [Required]
        public string Zhanri { get; set; }

        public string Pershkrimi { get; set; }


        [Display(Name = "Image")]
        public string MoviePicture { get; set; }
        [Required(ErrorMessage = "Duhet te shtoni foto")]
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile MovieFile { get; set; }



        [Display(Name = "Trailer")]
        public string MovieTrailer { get; set; }

        [NotMapped]
        [DisplayName("Upload Trailer")]
        [Required(ErrorMessage = "Duhet te shtoni trailer")]
        public IFormFile TrailerFile { get; set; }
    }
}
