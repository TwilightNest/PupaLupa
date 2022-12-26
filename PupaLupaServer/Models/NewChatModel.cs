using PupaLupaServer.Models.EF;

namespace PupaLupaServer.Models
{
    public class NewChatModel
    {
        public Guid Id { get; set; }

        public string LastMessage { get; set; }

        public Guid LastSenderUserId { get; set; }

        public Guid FirstUserId { get; set; }
        public Guid SecondUserId { get; set; }

        public Chat ToChat()
        {
            var tmp = new Chat();
            tmp.Id = Id;
            tmp.LastMessage = LastMessage;
            tmp.LastSenderUserId = LastSenderUserId;
            return tmp;
        }
    }
}
