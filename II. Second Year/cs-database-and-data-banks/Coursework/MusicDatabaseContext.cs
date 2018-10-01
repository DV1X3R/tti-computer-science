using Microsoft.EntityFrameworkCore;

using Coursework.Entities;

namespace Coursework
{
    public class MusicDatabaseContext : DbContext
    {
        public DbSet<Album> albums { get; set; }
        public DbSet<Artist> artists { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Genre> genres { get; set; }
        public DbSet<Invoice> invoices { get; set; }
        public DbSet<InvoiceLine> invoice_lines { get; set; }
        public DbSet<Track> tracks { get; set; }
        
        private string connectionString;

        public MusicDatabaseContext(string connectionString)
            => this.connectionString = connectionString;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite(connectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>().HasKey(x => x.AlbumId);
            modelBuilder.Entity<Album>().Property("Title").HasColumnType("nvarchar(160)").IsRequired();
            modelBuilder.Entity<Album>()
                .HasOne(o => o.Artist)
                .WithMany(m => m.Albums)
                .HasForeignKey(o => o.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Artist>().HasKey(x => x.ArtistId);
            modelBuilder.Entity<Artist>().Property("Name").HasColumnType("nvarchar(120)").IsRequired();
            modelBuilder.Entity<Artist>().HasIndex(x => x.Name).IsUnique();

            modelBuilder.Entity<Customer>().HasKey(x => x.CustomerId);
            modelBuilder.Entity<Customer>().Property("FirstName").HasColumnType("nvarchar(40)").IsRequired();
            modelBuilder.Entity<Customer>().Property("LastName").HasColumnType("nvarchar(20)").IsRequired();
            modelBuilder.Entity<Customer>().Property("Email").HasColumnType("nvarchar(60)").IsRequired();
            modelBuilder.Entity<Customer>().Property("Company").HasColumnType("nvarchar(80)");
            modelBuilder.Entity<Customer>().Property("Address").HasColumnType("nvarchar(70)");
            modelBuilder.Entity<Customer>().Property("City").HasColumnType("nvarchar(40)");
            modelBuilder.Entity<Customer>().Property("Country").HasColumnType("nvarchar(40)");
            modelBuilder.Entity<Customer>().Property("Phone").HasColumnType("nvarchar(24)");

            modelBuilder.Entity<Genre>().HasKey(x => x.GenreId);
            modelBuilder.Entity<Genre>().Property("Name").HasColumnType("nvarchar(120)").IsRequired();
            modelBuilder.Entity<Genre>().HasIndex(x => x.Name).IsUnique();

            modelBuilder.Entity<Invoice>().HasKey(x => x.InvoiceId);
            modelBuilder.Entity<Invoice>().Property("InvoiceDate").HasColumnType("date").IsRequired();
            modelBuilder.Entity<Invoice>()
                .HasOne(o => o.Customer)
                .WithMany(m => m.Invoices)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InvoiceLine>().HasKey(x => x.InvoiceLineId);
            modelBuilder.Entity<InvoiceLine>()
                .HasOne(o => o.Invoice)
                .WithMany(m => m.InvoiceLines)
                .HasForeignKey(o => o.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<InvoiceLine>()
                .HasOne(o => o.Track)
                .WithMany(m => m.InvoiceLines)
                .HasForeignKey(o => o.TrackId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Track>().HasKey(x => x.TrackId);
            modelBuilder.Entity<Track>().Property("Name").HasColumnType("nvarchar(200)").IsRequired();
            modelBuilder.Entity<Track>().Property("Milliseconds").HasColumnType("int").IsRequired();
            modelBuilder.Entity<Track>().Property("Bytes").HasColumnType("int").IsRequired();
            modelBuilder.Entity<Track>().Property("UnitPrice").HasColumnType("currency").IsRequired();
            modelBuilder.Entity<Track>()
                .HasOne(o => o.Album)
                .WithMany(m => m.Tracks)
                .HasForeignKey(o => o.AlbumId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Track>()
                .HasOne(o => o.Genre)
                .WithMany(m => m.Tracks)
                .HasForeignKey(o => o.GenreId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
