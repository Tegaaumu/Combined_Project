using Microsoft.EntityFrameworkCore;
using ProjectApi.Entities;
using ProjectApi.Models;
using ProjectApi.Services;

namespace ProjectApi
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortenedUrl>(builder =>
            {
                builder.Property(s => s.Code).HasMaxLength(UrlShorteningService.NumberOfChersIinShortLink);
                builder.HasIndex(s => s.Code).IsUnique();
            });
        }
    }
}
