namespace PupaLupaServer.Models.EF;

public partial class InboxParticipant
{
    public Guid Id { get; set; }

    public Guid? SenderUserId { get; set; }

    public Guid? InboxId { get; set; }

    public virtual Inbox? Inbox { get; set; }

    public virtual User? SenderUser { get; set; }
}
