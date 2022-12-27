using System;
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

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Relationship> Relationships { get; set; }

    public virtual DbSet<Statistic> Statistics { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsersChat> UsersChats { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=PupaLupaDB;Username=postgres;Password=Yara25565j!");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Chats_PK");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.LastMessage).HasColumnName("lastMessage");
            entity.Property(e => e.LastSenderUserId).HasColumnName("lastSenderUserId");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("Locations_PK");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("userId");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.ChatId).HasName("Messages_PK");

            entity.Property(e => e.ChatId)
                .ValueGeneratedNever()
                .HasColumnName("chatId");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.MessageBody).HasColumnName("messageBody");
            entity.Property(e => e.SenderUserId).HasColumnName("senderUserId");
        });

        modelBuilder.Entity<Relationship>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.FriendId }).HasName("Relationships_PK");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.FriendId).HasColumnName("friendId");
            entity.Property(e => e.StatisticsId).HasColumnName("statisticsId");
        });

        modelBuilder.Entity<Statistic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Statistics_PK");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.FirstMetDate).HasColumnName("firstMetDate");
            entity.Property(e => e.MeetingsCount).HasColumnName("meetingsCount");
            entity.Property(e => e.MessagesCount).HasColumnName("messagesCount");
            entity.Property(e => e.RelationType).HasColumnName("relationType");
            entity.Property(e => e.TimeTogether)
                .HasComment("hoursCount")
                .HasColumnName("timeTogether");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Users_PK");

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

        modelBuilder.Entity<UsersChat>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ChatId }).HasName("UsersChats_PK");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ChatId).HasColumnName("chatId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
