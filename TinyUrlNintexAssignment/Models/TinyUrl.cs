using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyUrlNintexAssignment.Models
{
    public class TinyUrl
    {
        public int Id { get; set; }

        [Required]
        public string OriginalUrl { get; set; }
    }
}
