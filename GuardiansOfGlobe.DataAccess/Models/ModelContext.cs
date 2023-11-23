using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GuardiansOfGlobe.DataAccess.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AlterEgo> AlterEgos { get; set; } = null!;
        public virtual DbSet<AlterEgoAbility> AlterEgoAbilities { get; set; } = null!;
        public virtual DbSet<AlterEgoFight> AlterEgoFights { get; set; } = null!;
        public virtual DbSet<AlterEgoWeakness> AlterEgoWeaknesses { get; set; } = null!;
        public virtual DbSet<Fight> Fights { get; set; } = null!;
        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<Relationship> Relationships { get; set; } = null!;
        public virtual DbSet<ScheduleEvent> ScheduleEvents { get; set; } = null!;
        public virtual DbSet<Sponsor> Sponsors { get; set; } = null!;
        public virtual DbSet<SponsorSource> SponsorSources { get; set; } = null!;
        public virtual DbSet<Sponsorship> Sponsorships { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("User Id=GUARDIAN;Password=GUARDIAN;Data Source=localhost:1521/XEPDB1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("GUARDIAN")
                .UseCollation("USING_NLS_COMP");

            modelBuilder.Entity<AlterEgo>(entity =>
            {
                entity.ToTable("ALTER_EGO");

                entity.Property(e => e.AlterEgoId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ALTER_EGO_ID");

                entity.Property(e => e.Alias)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ALIAS");

                entity.Property(e => e.IsHero)
                    .HasPrecision(1)
                    .HasColumnName("IS_HERO");

                entity.Property(e => e.Origin)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ORIGIN");

                entity.Property(e => e.PersonId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PERSON_ID");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.AlterEgos)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ALTER_EGO_PERSON_FK");
            });

            modelBuilder.Entity<AlterEgoAbility>(entity =>
            {
                entity.ToTable("ALTER_EGO_ABILITY");

                entity.Property(e => e.AlterEgoAbilityId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ALTER_EGO_ABILITY_ID");

                entity.Property(e => e.AbilityName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ABILITY_NAME");

                entity.Property(e => e.AlterEgoId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ALTER_EGO_ID");

                entity.HasOne(d => d.AlterEgo)
                    .WithMany(p => p.AlterEgoAbilities)
                    .HasForeignKey(d => d.AlterEgoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ALTER_EGO_ABILITY_ALTER_EGO_FK");
            });

            modelBuilder.Entity<AlterEgoFight>(entity =>
            {
                entity.ToTable("ALTER_EGO_FIGHT");

                entity.Property(e => e.AlterEgoFightId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ALTER_EGO_FIGHT_ID");

                entity.Property(e => e.AlterEgoId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ALTER_EGO_ID");

                entity.Property(e => e.FightId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("FIGHT_ID");

                entity.Property(e => e.Side)
                    .HasPrecision(1)
                    .HasColumnName("SIDE");

                entity.Property(e => e.Victory)
                    .HasPrecision(1)
                    .HasColumnName("VICTORY");

                entity.HasOne(d => d.AlterEgo)
                    .WithMany(p => p.AlterEgoFights)
                    .HasForeignKey(d => d.AlterEgoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ALTER_EGO_FIGHT_ALTER_EGO_FK");

                entity.HasOne(d => d.Fight)
                    .WithMany(p => p.AlterEgoFights)
                    .HasForeignKey(d => d.FightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ALTER_EGO_FIGHT_FIGHT_FK");
            });

            modelBuilder.Entity<AlterEgoWeakness>(entity =>
            {
                entity.ToTable("ALTER_EGO_WEAKNESS");

                entity.Property(e => e.AlterEgoWeaknessId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ALTER_EGO_WEAKNESS_ID");

                entity.Property(e => e.AlterEgoId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ALTER_EGO_ID");

                entity.Property(e => e.WeaknessName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("WEAKNESS_NAME");

                entity.HasOne(d => d.AlterEgo)
                    .WithMany(p => p.AlterEgoWeaknesses)
                    .HasForeignKey(d => d.AlterEgoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ALTER_EGO_WEAKNESS_ALTER_EGO_FK");
            });

            modelBuilder.Entity<Fight>(entity =>
            {
                entity.ToTable("FIGHT");

                entity.Property(e => e.FightId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("FIGHT_ID");

                entity.Property(e => e.DateEnd)
                    .HasColumnType("DATE")
                    .HasColumnName("DATE_END");

                entity.Property(e => e.DateStart)
                    .HasColumnType("DATE")
                    .HasColumnName("DATE_START");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.FightTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FIGHT_TITLE");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LOCATION");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("PERSON");

                entity.Property(e => e.PersonId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PERSON_ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Birthdate)
                    .HasColumnType("DATE")
                    .HasColumnName("BIRTHDATE");

                entity.Property(e => e.Occupation)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OCCUPATION");

                entity.Property(e => e.PersonName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PERSON_NAME");

                entity.Property(e => e.Sex)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("SEX")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Relationship>(entity =>
            {
                entity.ToTable("RELATIONSHIP");

                entity.Property(e => e.RelationshipId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RELATIONSHIP_ID");

                entity.Property(e => e.Person1Id)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PERSON1_ID");

                entity.Property(e => e.Person2Id)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PERSON2_ID");

                entity.Property(e => e.RelationshipType)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("RELATIONSHIP_TYPE");

                entity.HasOne(d => d.Person1)
                    .WithMany(p => p.RelationshipPerson1s)
                    .HasForeignKey(d => d.Person1Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RELATIONSHIP_PERSON1_FK");

                entity.HasOne(d => d.Person2)
                    .WithMany(p => p.RelationshipPerson2s)
                    .HasForeignKey(d => d.Person2Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RELATIONSHIP_PERSON2_FK");
            });

            modelBuilder.Entity<ScheduleEvent>(entity =>
            {
                entity.ToTable("SCHEDULE_EVENT");

                entity.Property(e => e.ScheduleEventId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("SCHEDULE_EVENT_ID");

                entity.Property(e => e.DateEnd)
                    .HasColumnType("DATE")
                    .HasColumnName("DATE_END");

                entity.Property(e => e.DateStart)
                    .HasColumnType("DATE")
                    .HasColumnName("DATE_START");

                entity.Property(e => e.EventName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EVENT_NAME");

                entity.Property(e => e.PersonId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PERSON_ID");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.ScheduleEvents)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SCHEDULE_EVENT_PERSON_FK");
            });

            modelBuilder.Entity<Sponsor>(entity =>
            {
                entity.ToTable("SPONSOR");

                entity.Property(e => e.SponsorId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("SPONSOR_ID");

                entity.Property(e => e.SponsorName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SPONSOR_NAME");

                entity.Property(e => e.SponsorSourceId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("SPONSOR_SOURCE_ID");

                entity.HasOne(d => d.SponsorSource)
                    .WithMany(p => p.Sponsors)
                    .HasForeignKey(d => d.SponsorSourceId)
                    .HasConstraintName("SPONSOR_SPONSOR_SOURCE_FK");
            });

            modelBuilder.Entity<SponsorSource>(entity =>
            {
                entity.ToTable("SPONSOR_SOURCE");

                entity.Property(e => e.SponsorSourceId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("SPONSOR_SOURCE_ID");

                entity.Property(e => e.Reliability)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("RELIABILITY");

                entity.Property(e => e.SponsorSourceName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SPONSOR_SOURCE_NAME");
            });

            modelBuilder.Entity<Sponsorship>(entity =>
            {
                entity.ToTable("SPONSORSHIP");

                entity.Property(e => e.SponsorshipId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("SPONSORSHIP_ID");

                entity.Property(e => e.AlterEgoId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ALTER_EGO_ID");

                entity.Property(e => e.SponsorId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("SPONSOR_ID");

                entity.HasOne(d => d.AlterEgo)
                    .WithMany(p => p.Sponsorships)
                    .HasForeignKey(d => d.AlterEgoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SPONSORSHIP_ALTER_EGO_FK");

                entity.HasOne(d => d.Sponsor)
                    .WithMany(p => p.Sponsorships)
                    .HasForeignKey(d => d.SponsorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SPONSORSHIP_SPONSOR_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
