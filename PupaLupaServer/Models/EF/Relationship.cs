namespace PupaLupaServer.Models.EF;

public partial class Relationship
{
    public Guid FirstUserId { get; set; }

    public Guid SecondUserId { get; set; }

    public Guid? StatisticsId { get; set; }

    public virtual Statistic? Statistics { get; set; }
}
