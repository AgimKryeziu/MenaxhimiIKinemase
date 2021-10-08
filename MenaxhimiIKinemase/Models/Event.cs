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
    public class Event
    {
        int _eventiID;
        string _emertimi;
        string _pershkrimi;
        DateTime _data;

        [Key]
        public int EventiID
        {
            get { return _eventiID; }
            set { _eventiID = value; }
        }
        public string Emertimi
        {
            get { return _emertimi; }
            set { _emertimi = value; }
        }
        public string Pershkrimi
        {
            get { return _pershkrimi; }
            set { _pershkrimi = value; }
        }
        public DateTime Data
        {
            get { return _data; }
            set { _data = value; }
        }

        [Display(Name = "Image")]
        public string EventPicture { get; set; }
        [Required(ErrorMessage = "Duhet te shtoni foto")]
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile EventFile { get; set; }
    }
}
