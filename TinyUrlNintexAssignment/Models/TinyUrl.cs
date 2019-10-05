using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyUrlNintexAssignment.Models
{
    public class TinyUrl
    {
        /// <summary>
        /// id of the url object. This is used for decoding and encoding as an incremental key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Original url value.
        /// </summary>
        [Required]
        public string OriginalUrl { get; set; }
    }
}
