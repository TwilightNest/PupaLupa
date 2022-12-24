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

    public virtual DbSet<Inbox> Inboxes { get; set; }

    public virtual DbSet<InboxParticipant> InboxParticipants { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserFriend> UserFriends { get; set; }

    public virtual DbSet<UsersLocation> UsersLocations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=PupaLupaDB;Username=postgres;Password=Yara25565j!");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Inbox>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Inbox_PK");

            entity.ToTable("Inbox");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.LastMessage).HasColumnName("lastMessage");
            entity.Property(e => e.LastSentUserId).HasColumnName("lastSentUserId");
        });

        modelBuilder.Entity<InboxParticipant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("InboxParticipants_PK");

            entity.HasIndex(e => e.InboxId, "fki_InboxParticipants_Inbox_FK");

            entity.HasIndex(e => e.SenderUserId, "fki_InboxParticipants_Users_FK");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.InboxId).HasColumnName("inboxId");
            entity.Property(e => e.SenderUserId).HasColumnName("senderUserId");

            entity.HasOne(d => d.Inbox).WithMany(p => p.InboxParticipants)
                .HasForeignKey(d => d.InboxId)
                .HasConstraintName("InboxParticipants_Inbox_FK");

            entity.HasOne(d => d.SenderUser).WithMany(p => p.InboxParticipants)
                .HasForeignKey(d => d.SenderUserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("InboxParticipants_Users_FK");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Messages_PK");

            entity.HasIndex(e => e.InboxId, "fki_Messages_Inbox_FK");

            entity.HasIndex(e => e.ReceiverUserId, "fki_Messages_Users_FK");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.InboxId).HasColumnName("inboxId");
            entity.Property(e => e.MessageBody).HasColumnName("messageBody");
            entity.Property(e => e.ReceiverUserId).HasColumnName("receiverUserId");

            entity.HasOne(d => d.Inbox).WithMany(p => p.Messages)
                .HasForeignKey(d => d.InboxId)
                .HasConstraintName("Messages_Inbox_FK");

            entity.HasOne(d => d.ReceiverUser).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ReceiverUserId)
                .HasConstraintName("Messages_Users_FK");
        });

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
