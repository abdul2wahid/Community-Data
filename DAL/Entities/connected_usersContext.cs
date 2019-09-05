using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAL.Entities
{
    public partial class connected_usersContext : DbContext
    {
        public connected_usersContext()
        {
        }

        public connected_usersContext(DbContextOptions<connected_usersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Arabiceducation> Arabiceducation { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<CustomerAddress> CustomerAddress { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Education> Education { get; set; }
        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<Maritalstatus> Maritalstatus { get; set; }
        public virtual DbSet<Mulaqat> Mulaqat { get; set; }
        public virtual DbSet<Occupation> Occupation { get; set; }
        public virtual DbSet<Pincode> Pincode { get; set; }
        public virtual DbSet<Reltionship> Reltionship { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<States> States { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=new_password;database=connected_users");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Arabiceducation>(entity =>
            {
                entity.ToTable("arabiceducation", "connected_users");

                entity.Property(e => e.ArabicEducationId)
                    .HasColumnName("arabicEducationID")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.ArabicEducationName)
                    .HasColumnName("arabicEducationName")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("city", "connected_users");

                entity.Property(e => e.CityId)
                    .HasColumnName("cityID")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.City1)
                    .HasColumnName("city")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CustomerAddress>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.ToTable("customer_address", "connected_users");

                entity.HasIndex(e => e.CityId)
                    .HasName("cityID");

                entity.HasIndex(e => e.PinId)
                    .HasName("pinID");

                entity.HasIndex(e => e.StateId)
                    .HasName("stateID");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customerID")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address1)
                    .HasColumnName("address1")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasColumnName("address2")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Area)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CityId)
                    .HasColumnName("cityID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PinId)
                    .HasColumnName("pinID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StateId)
                    .HasColumnName("stateID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.CustomerAddress)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("customer_address_ibfk_4");

                entity.HasOne(d => d.Customer)
                    .WithOne(p => p.CustomerAddress)
                    .HasForeignKey<CustomerAddress>(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("customer_address_ibfk_1");

                entity.HasOne(d => d.Pin)
                    .WithMany(p => p.CustomerAddress)
                    .HasForeignKey(d => d.PinId)
                    .HasConstraintName("customer_address_ibfk_3");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.CustomerAddress)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("customer_address_ibfk_2");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.ToTable("customers", "connected_users");

                entity.HasIndex(e => e.ArabicEducationId)
                    .HasName("arabicEducationID");

                entity.HasIndex(e => e.EducationId)
                    .HasName("educationID");

                entity.HasIndex(e => e.GenderId)
                    .HasName("GenderID");

                entity.HasIndex(e => e.MaritalStatusId)
                    .HasName("maritalStatusID");

                entity.HasIndex(e => e.OccupationId)
                    .HasName("occupationID");

                entity.HasIndex(e => new { e.Name, e.Dob })
                    .HasName("Name")
                    .IsUnique();

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customerID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ArabicEducationId)
                    .HasColumnName("arabicEducationID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdBy")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedTime).HasColumnName("createdTime");

                entity.Property(e => e.Dob)
                    .HasColumnName("dob")
                    .HasColumnType("date");

                entity.Property(e => e.EducationDetail)
                    .HasColumnName("educationDetail")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EducationId)
                    .HasColumnName("educationID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.GenderId)
                    .HasColumnName("GenderID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MaritalStatusId)
                    .HasColumnName("maritalStatusID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasColumnName("mobile_number")
                    .HasColumnType("mediumtext");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.OccupationId)
                    .HasColumnName("occupationID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.OccupdationDetails)
                    .HasColumnName("occupdationDetails")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updatedBy")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedTime).HasColumnName("updatedTime");

                entity.HasOne(d => d.ArabicEducation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.ArabicEducationId)
                    .HasConstraintName("customers_ibfk_3");

                entity.HasOne(d => d.Education)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.EducationId)
                    .HasConstraintName("customers_ibfk_2");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("customers_ibfk_4");

                entity.HasOne(d => d.MaritalStatus)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.MaritalStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("customers_ibfk_5");

                entity.HasOne(d => d.Occupation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.OccupationId)
                    .HasConstraintName("customers_ibfk_1");
            });

            modelBuilder.Entity<Education>(entity =>
            {
                entity.ToTable("education", "connected_users");

                entity.Property(e => e.EducationId)
                    .HasColumnName("educationID")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.EducationName)
                    .HasColumnName("educationName")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Events>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.ToTable("events", "connected_users");

                entity.HasIndex(e => e.ResponsiblePerson)
                    .HasName("responsiblePerson");

                entity.Property(e => e.EventId)
                    .HasColumnName("eventID")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.EventType)
                    .HasColumnName("eventType")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Feeback)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ResponsiblePerson)
                    .HasColumnName("responsiblePerson")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TargetAudience)
                    .HasColumnName("targetAudience")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.ResponsiblePersonNavigation)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.ResponsiblePerson)
                    .HasConstraintName("events_ibfk_1");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("gender", "connected_users");

                entity.Property(e => e.GenderId)
                    .HasColumnName("GenderID")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Gender1)
                    .HasColumnName("GENDER")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Maritalstatus>(entity =>
            {
                entity.ToTable("maritalstatus", "connected_users");

                entity.Property(e => e.MaritalStatusId)
                    .HasColumnName("maritalStatusID")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.MaritalStatus1)
                    .HasColumnName("maritalStatus")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Mulaqat>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("mulaqat", "connected_users");

                entity.HasIndex(e => e.LastMetUserId)
                    .HasName("lastMetUserID");

                entity.Property(e => e.UserId)
                    .HasColumnName("userID")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.LastMetDate).HasColumnName("lastMetDate");

                entity.Property(e => e.LastMetUserId)
                    .HasColumnName("lastMetUserID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.LastMetUser)
                    .WithMany(p => p.MulaqatLastMetUser)
                    .HasForeignKey(d => d.LastMetUserId)
                    .HasConstraintName("mulaqat_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.MulaqatUser)
                    .HasForeignKey<Mulaqat>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mulaqat_ibfk_1");
            });

            modelBuilder.Entity<Occupation>(entity =>
            {
                entity.ToTable("occupation", "connected_users");

                entity.Property(e => e.OccupationId)
                    .HasColumnName("occupationID")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.OccuptionName)
                    .HasColumnName("occuptionName")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pincode>(entity =>
            {
                entity.HasKey(e => e.PinId);

                entity.ToTable("pincode", "connected_users");

                entity.Property(e => e.PinId)
                    .HasColumnName("pinID")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Pin)
                    .HasColumnName("pin")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Reltionship>(entity =>
            {
                entity.ToTable("reltionship", "connected_users");

                entity.HasIndex(e => e.ChildernId)
                    .HasName("childernID");

                entity.HasIndex(e => e.UserId)
                    .HasName("userID");

                entity.HasIndex(e => e.WifeId)
                    .HasName("wifeID");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.ChildernId)
                    .HasColumnName("childernID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .HasColumnName("userID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.WifeId)
                    .HasColumnName("wifeID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Childern)
                    .WithMany(p => p.ReltionshipChildern)
                    .HasForeignKey(d => d.ChildernId)
                    .HasConstraintName("reltionship_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ReltionshipUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reltionship_ibfk_1");

                entity.HasOne(d => d.Wife)
                    .WithMany(p => p.ReltionshipWife)
                    .HasForeignKey(d => d.WifeId)
                    .HasConstraintName("reltionship_ibfk_3");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("roles", "connected_users");

                entity.Property(e => e.RoleId)
                    .HasColumnName("roleID")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.RoleName)
                    .HasColumnName("roleName")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<States>(entity =>
            {
                entity.HasKey(e => e.StateId);

                entity.ToTable("states", "connected_users");

                entity.Property(e => e.StateId)
                    .HasColumnName("stateID")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("users", "connected_users");

                entity.HasIndex(e => e.RoleId)
                    .HasName("roleID");

                entity.Property(e => e.UserId)
                    .HasColumnName("userID")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("createdBy")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedTime).HasColumnName("createdTime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId)
                    .HasColumnName("roleID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updatedBy")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedTime).HasColumnName("updatedTime");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("users_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Users)
                    .HasForeignKey<Users>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("users_ibfk_1");
            });
        }
    }
}
