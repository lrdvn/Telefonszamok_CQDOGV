﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 2025. 03. 17. 11:16:37
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Models;

namespace cnTelefonkonyv
{

    public partial class TelefonkonyvContext : DbContext
    {

        public TelefonkonyvContext() :
            base()
        {
            OnCreated();
        }

        public TelefonkonyvContext(DbContextOptions<TelefonkonyvContext> options) :
            base(options)
        {
            OnCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured ||
                (!optionsBuilder.Options.Extensions.OfType<RelationalOptionsExtension>().Any(ext => !string.IsNullOrEmpty(ext.ConnectionString) || ext.Connection != null) &&
                 !optionsBuilder.Options.Extensions.Any(ext => !(ext is RelationalOptionsExtension) && !(ext is CoreOptionsExtension))))
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=Telefonszamok;Integrated Security=True;TrustServerCertificate=True;");
            }
            CustomizeConfiguration(ref optionsBuilder);
            base.OnConfiguring(optionsBuilder);
        }

        partial void CustomizeConfiguration(ref DbContextOptionsBuilder optionsBuilder);

        public virtual DbSet<enSzemely> enSzemelyek
        {
            get;
            set;
        }

        public virtual DbSet<enTelefonszam> enTelefonszamok
        {
            get;
            set;
        }

        public virtual DbSet<enHelyseg> enHelysegek
        {
            get;
            set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            this.enSzemelyMapping(modelBuilder);
            this.CustomizeenSzemelyMapping(modelBuilder);

            this.enTelefonszamMapping(modelBuilder);
            this.CustomizeenTelefonszamMapping(modelBuilder);

            this.enHelysegMapping(modelBuilder);
            this.CustomizeenHelysegMapping(modelBuilder);

            RelationshipsMapping(modelBuilder);
            CustomizeMapping(ref modelBuilder);
        }

        #region enSzemely Mapping

        private void enSzemelyMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<enSzemely>().ToTable(@"Szemely");
            modelBuilder.Entity<enSzemely>().Property(x => x.id).HasColumnName(@"id").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<enSzemely>().Property(x => x.Vezeteknev).HasColumnName(@"Vezeteknev").ValueGeneratedNever();
            modelBuilder.Entity<enSzemely>().Property(x => x.Utonev).HasColumnName(@"Utonev").ValueGeneratedNever();
            modelBuilder.Entity<enSzemely>().Property(x => x.Lakcim).HasColumnName(@"Lakcim").ValueGeneratedNever();
            modelBuilder.Entity<enSzemely>().Property(x => x.enHelysegid).HasColumnName(@"enHelysegid").ValueGeneratedNever();
            modelBuilder.Entity<enSzemely>().HasKey(@"id");
        }

        partial void CustomizeenSzemelyMapping(ModelBuilder modelBuilder);

        #endregion

        #region enTelefonszam Mapping

        private void enTelefonszamMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<enTelefonszam>().ToTable(@"Telefonszamok");
            modelBuilder.Entity<enTelefonszam>().Property(x => x.id).HasColumnName(@"id").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<enTelefonszam>().Property(x => x.Szam).HasColumnName(@"Szam").ValueGeneratedNever();
            modelBuilder.Entity<enTelefonszam>().Property(x => x.enSzemelyid).HasColumnName(@"enSzemelyid").ValueGeneratedNever();
            modelBuilder.Entity<enTelefonszam>().HasKey(@"id");
        }

        partial void CustomizeenTelefonszamMapping(ModelBuilder modelBuilder);

        #endregion

        #region enHelyseg Mapping

        private void enHelysegMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<enHelyseg>().ToTable(@"Helyseg");
            modelBuilder.Entity<enHelyseg>().Property(x => x.id).HasColumnName(@"id").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<enHelyseg>().Property(x => x.IRSZ).HasColumnName(@"IRSZ").IsRequired().ValueGeneratedNever().HasMaxLength(128);
            modelBuilder.Entity<enHelyseg>().Property(x => x.Nev).HasColumnName(@"Nev").IsRequired().ValueGeneratedNever().HasMaxLength(256);
            modelBuilder.Entity<enHelyseg>().HasKey(@"id");
        }

        partial void CustomizeenHelysegMapping(ModelBuilder modelBuilder);

        #endregion

        private void RelationshipsMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<enSzemely>().HasMany(x => x.enTelefonszamok).WithOne(op => op.enSzemely).HasForeignKey(@"enSzemelyid").IsRequired(true);
            modelBuilder.Entity<enSzemely>().HasOne(x => x.enHelyseg).WithMany(op => op.enSzemelyek).HasForeignKey(@"enHelysegid").IsRequired(true);

            modelBuilder.Entity<enTelefonszam>().HasOne(x => x.enSzemely).WithMany(op => op.enTelefonszamok).HasForeignKey(@"enSzemelyid").IsRequired(true);

            modelBuilder.Entity<enHelyseg>().HasMany(x => x.enSzemelyek).WithOne(op => op.enHelyseg).HasForeignKey(@"enHelysegid").IsRequired(true);
        }

        partial void CustomizeMapping(ref ModelBuilder modelBuilder);

        public bool HasChanges()
        {
            return ChangeTracker.Entries().Any(e => e.State == Microsoft.EntityFrameworkCore.EntityState.Added || e.State == Microsoft.EntityFrameworkCore.EntityState.Modified || e.State == Microsoft.EntityFrameworkCore.EntityState.Deleted);
        }

        partial void OnCreated();
    }
}
