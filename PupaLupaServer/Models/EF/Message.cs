namespace PupaLupaServer.Models.EF;

public partial class Message
{
    public Guid Id { get; set; }

    public Guid? InboxId { get; set; }

    public Guid? ReceiverUserId { get; set; }

    public string? MessageBody { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Inbox? Inbox { get; set; }

    public virtual User? ReceiverUser { get; set; }
}
