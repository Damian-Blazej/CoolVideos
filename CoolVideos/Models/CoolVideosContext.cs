using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoolVideos.Models
{
    public class CoolVideosContext : DbContext
    {
        public CoolVideosContext(DbContextOptions<CoolVideosContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Like>()
                .HasKey(l => new { l.videoID, l.userID });
        }
    }
}
