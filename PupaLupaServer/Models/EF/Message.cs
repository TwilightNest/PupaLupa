namespace PupaLupaServer.Models.EF;

public partial class Message
{
    public Guid ChatId { get; set; }

    public Guid? SenderUserId { get; set; }

    public string? MessageBody { get; set; }
}
