using PupaLupaServer.Models.EF;

namespace PupaLupaServer.Models
{
    public class StatisticModel
    {
        public Guid Id { get; set; }

        public short? RelationType { get; set; }

        /// <summary>
        /// hoursCount
        /// </summary>
        public int? TimeTogether { get; set; }

        public string? FirstMetDate { get; set; }

        public long? MessagesCount { get; set; }

        public int? MeetingsCount { get; set; }

        public Statistic ToStatistic()
        {
            var tmp = new Statistic();
            tmp.Id = Id;
            tmp.RelationType = RelationType;
            tmp.TimeTogether = TimeTogether;
            tmp.FirstMetDate = Convert.ToDateTime(FirstMetDate).ToUniversalTime();
            tmp.MessagesCount = MessagesCount;
            tmp.MeetingsCount = MeetingsCount;
            return tmp;
        }
    }
}