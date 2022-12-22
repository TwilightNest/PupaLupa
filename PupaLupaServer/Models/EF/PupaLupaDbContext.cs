﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PupaLupaServer.Models.EF;

public partial class PupaLupaDbContext : DbContext
{
    public PupaLupaDbContext()
    {
    }

    public PupaLupaDbContext(DbContextOptions<PupaLupaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserFriend> UserFriends { get; set; }

    public virtual DbSet<UsersLocation> UsersLocations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=PupaLupaDB;Username=postgres;Password=Yara25565j!");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_PK");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Login)
                .HasComment("nickname")
                .HasColumnName("login");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.PhoneNumber).HasColumnName("phoneNumber");
        });

        modelBuilder.Entity<UserFriend>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("Friends_PK");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("userId");
            entity.Property(e => e.FriendsIds).HasColumnName("friendsIds");
        });

        modelBuilder.Entity<UsersLocation>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("usersLocations_PK");

            entity.ToTable("UsersLocation");

            entity.HasIndex(e => e.UserId, "fki_usersLocation_FK");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("userId");
            entity.Property(e => e.Latitude)
                .HasComment("Ширина")
                .HasColumnName("latitude");
            entity.Property(e => e.Longitude)
                .HasComment("Долгота")
                .HasColumnName("longitude");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
