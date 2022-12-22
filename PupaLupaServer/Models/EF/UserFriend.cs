namespace PupaLupaServer.Models.EF;

public partial class UserFriend
{
    public Guid UserId { get; set; }

    public Guid[] FriendsIds { get; set; } = null!;
}