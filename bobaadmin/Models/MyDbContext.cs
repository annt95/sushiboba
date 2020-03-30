using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace bobaadmin.Models
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admins> Admins { get; set; }
        public virtual DbSet<Cate> Cate { get; set; }
        public virtual DbSet<Languages> Languages { get; set; }
        public virtual DbSet<Mapmenu> Mapmenu { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbQuery<Menu> vw_menuadmin { get; set; }
        public virtual DbQuery<MenuItems> MenuItems { get; set; }
        // Unable to generate entity type for table 'dbo.VersionInfo'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=sql.freeasphost.net\\MSSQL2016;User Id=annt95;Password=sushiadmin;Database=annt95_sushi;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");
            modelBuilder.Query<Menu>().ToView("vw_menuadmin");
            modelBuilder.Entity<Admins>(entity =>
            {
                entity.ToTable("admins");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Cate>(entity =>
            {
                entity.ToTable("cate");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Catename)
                    .IsRequired()
                    .HasColumnName("catename")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Languages>(entity =>
            {
                entity.Property(e => e.LanguageId)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.LanguageName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Mapmenu>(entity =>
            {
                entity.ToTable("mapmenu");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cateid).HasColumnName("cateid");

                entity.Property(e => e.Menuid).HasColumnName("menuid");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("menu");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .IsUnicode(false);

                entity.Property(e => e.Images)
                    .IsRequired()
                    .HasColumnName("images")
                    .IsUnicode(false);

                entity.Property(e => e.Isdelete).HasColumnName("isdelete");

                entity.Property(e => e.Ishot).HasColumnName("ishot");

                entity.Property(e => e.Ismilktea).HasColumnName("ismilktea");

                entity.Property(e => e.Issushi).HasColumnName("issushi");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .IsRequired()
                    .HasColumnName("price")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
        }
    }
}
