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
        public virtual DbSet<Menu> Menu { get; set; }



        public virtual DbSet<Cate> Cate { get; set; }
        public virtual DbSet<Languages> Languages { get; set; }
        public virtual DbSet<Mapmenu> Mapmenu { get; set; }
        public virtual DbQuery<MenuItems> vw_menuadmin { get; set; }
        public virtual DbQuery<MenuItems> MenuItems { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=localhost;User Id=sa;Password=sushiadmin;Database=bobacha;");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");
            modelBuilder.Query<MenuItems>().ToView("vw_menuadmin");
            modelBuilder.Entity<Admins>(entity =>
            {
                entity.ToTable("admins");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Images).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Price)
                    .IsRequired()
                    .HasMaxLength(20);
            });
        }
    }

}
