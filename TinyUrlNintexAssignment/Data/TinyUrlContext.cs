using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyUrlNintexAssignment.Models;

namespace TinyUrlNintexAssignment.Data
{
    public class TinyUrlContext : DbContext
    {
        public TinyUrlContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<TinyUrl> TinyUrls { get; set; }


    }
}
