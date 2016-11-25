using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RumsBokning.Models.Entities
{
    public partial class BookingContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=Identitybooking;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room", "aaa");

                entity.Property(e => e.HasProjector).HasColumnName("Has_Projector");

                entity.Property(e => e.HasTvScreen).HasColumnName("Has_TvScreen");

                entity.Property(e => e.HasWhiteBoard).HasColumnName("Has_WhiteBoard");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(30)");
            });

            modelBuilder.Entity<RoomTime>(entity =>
            {
                entity.ToTable("RoomTime", "aaa");

                entity.HasIndex(e => new { e.RId, e.UId, e.StartTime, e.EndTime })
                    .HasName("uq_RoomTime")
                    .IsUnique();

                entity.Property(e => e.EndTime)
                    .IsRequired()
                    .HasColumnType("datetime");

                entity.Property(e => e.RId).HasColumnName("R_Id");

                entity.Property(e => e.StartTime)
                    .IsRequired()
                    .HasColumnType("datetime");

                entity.Property(e => e.UId)
                    .IsRequired()
                    .HasColumnName("U_Id")
                    .HasMaxLength(450);

                entity.HasOne(d => d.R)
                    .WithMany(p => p.RoomTime)
                    .HasForeignKey(d => d.RId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_RoomId");

                entity.HasOne(d => d.U)
                    .WithMany(p => p.RoomTime)
                    .HasForeignKey(d => d.UId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_UserId");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("Users", "aaa");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Users__A9D1053469AF9A27")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(450);

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasColumnType("varchar(44)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnType("varchar(24)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnType("varchar(24)");
            });
        }

        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<RoomTime> RoomTime { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}