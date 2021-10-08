using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MenaxhimiIKinemase.Models
{
    public class Orar
    {

        [Key]
        public int OrariID { get; set; }

        public DateTime Ora { get; set; }
        public int FilmiID { get; set; }

        [ForeignKey("FilmiID")]
        public virtual Film Filmi { get; set; }
    }
}
