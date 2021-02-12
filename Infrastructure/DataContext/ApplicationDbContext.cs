using System;
using System.Linq;
using ClientSideACMS.Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


#nullable disable

namespace ClientSideACMS.Infrastructure.DataContext
{
    public partial class ApplicationDbContext : IdentityDbContext<User, UserRole, int>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AvailableClass> AvailableClasses { get; set; }
        public virtual DbSet<ClassCategory> ClassCategories { get; set; }
        public virtual DbSet<ClassReport> ClassReports { get; set; }
        public virtual DbSet<PaidSession> PaidSessions { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<RegistredClass> RegistredClasses { get; set; }
        public virtual DbSet<SessionSchedule> SessionSchedules { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=APIPrototype1;Trusted_Connection=false;");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AvailableClass>(entity =>
            {
                entity.HasKey(e => e.ClassId);

                entity.Property(e => e.ClassId).HasMaxLength(250);

                entity.Property(e => e.ClassName).HasMaxLength(250);

                entity.Property(e => e.TeacherId)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.AvailableClasses)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK_AvailableClasses_Teacher");
            });

            modelBuilder.Entity<ClassCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.ToTable("ClassCategory");

                entity.Property(e => e.CategoryId).HasMaxLength(250);

                entity.Property(e => e.DiscountedFee).HasColumnType("smallmoney");

                entity.Property(e => e.IncomeAiu)
                    .HasColumnType("smallmoney")
                    .HasColumnName("IncomeAIU");

                entity.Property(e => e.IncomeInstructor).HasColumnType("smallmoney");

                entity.Property(e => e.TotalTutionFee).HasColumnType("smallmoney");
            });

            modelBuilder.Entity<ClassReport>(entity =>
            {
                entity.ToTable("ClassReport");

                entity.Property(e => e.ClassReportId).HasMaxLength(250);
            });

            modelBuilder.Entity<PaidSession>(entity =>
            {
                entity.Property(e => e.PaidSessionId).HasMaxLength(250);

                entity.Property(e => e.ClassId)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.DatePaid).HasColumnType("datetime");

                entity.Property(e => e.PaymentsMonth).HasColumnType("date");

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.PaidSessions)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_PaidSessions_AvailableClasses");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.PaidSessions)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_PaidSessions_Student");
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.ToTable("PaymentMethod");

                entity.Property(e => e.PaymentMethodId).HasMaxLength(250);

                entity.Property(e => e.MethodName).HasMaxLength(250);
            });

            modelBuilder.Entity<RegistredClass>(entity =>
            {
                entity.ToTable("RegistredClass");

                entity.Property(e => e.RegistredClassId).HasMaxLength(250);

                entity.Property(e => e.CategoryId)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ClassId)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ClassReportId).HasMaxLength(250);

                entity.Property(e => e.DateRegistered).HasColumnType("datetime");

                entity.Property(e => e.Day).HasMaxLength(50);

                entity.Property(e => e.PaymentMethodId).HasMaxLength(250);

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.RegistredClasses)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_RegistredClass_ClassCategory");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.RegistredClasses)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_RegistredClass_AvailableClasses");

                entity.HasOne(d => d.ClassReport)
                    .WithMany(p => p.RegistredClasses)
                    .HasForeignKey(d => d.ClassReportId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_RegistredClass_ClassReport");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.RegistredClasses)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_RegistredClass_PaymentMethod");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.RegistredClasses)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<SessionSchedule>(entity =>
            {
                entity.HasKey(e => e.ScheduleId);

                entity.ToTable("SessionSchedule");

                entity.Property(e => e.ScheduleId).HasMaxLength(250);

                entity.Property(e => e.ClassId)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Day).HasMaxLength(50);

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.TeacherId)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.SessionSchedules)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SessionSchedule_AvailableClasses");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.SessionSchedules)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_SessionSchedule_Student");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.SessionSchedules)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK_SessionSchedule_Teacher")
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.StudentId).HasMaxLength(250);

                entity.Property(e => e.Address).HasMaxLength(450);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.FirstName).HasMaxLength(250);

                entity.Property(e => e.HomeNumber).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(250);

                entity.Property(e => e.ParentGuardianName).HasMaxLength(250);

                entity.Property(e => e.SchoolName).HasMaxLength(250);

                entity.Property(e => e.Sex).HasMaxLength(1);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(250);
                entity.HasOne(e => e.User)
                .WithOne(p => p.Student)
                .HasForeignKey<Student>(e => e.UserId);

            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teacher");

                entity.Property(e => e.TeacherId).HasMaxLength(250);

                entity.Property(e => e.Address).HasMaxLength(450);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.FirstName).HasMaxLength(250);

                entity.Property(e => e.HomeNumber).HasMaxLength(250);

                entity.Property(e => e.Sex).HasMaxLength(1);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(250);
                entity.HasOne(e => e.User)
                .WithOne(p => p.Teacher)
                .HasForeignKey<Teacher>(e => e.UserId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
