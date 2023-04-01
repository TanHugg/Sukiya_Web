using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Sukiya.Models
{
    public partial class FoodModelContext : DbContext
    {
        public FoodModelContext()
            : base("name=FoodModelContext")
        {
        }

        public virtual DbSet<CT_DonHang> CT_DonHang { get; set; }
        public virtual DbSet<DonHang> DonHang { get; set; }
        public virtual DbSet<LoaiMon> LoaiMon { get; set; }
        public virtual DbSet<Mon> Mon { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CT_DonHang>()
                .Property(e => e.Gia)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DonHang>()
                .HasMany(e => e.CT_DonHang)
                .WithRequired(e => e.DonHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Mon>()
                .Property(e => e.Gia)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Mon>()
                .HasMany(e => e.CT_DonHang)
                .WithRequired(e => e.Mon)
                .WillCascadeOnDelete(false);
        }
    }
}
