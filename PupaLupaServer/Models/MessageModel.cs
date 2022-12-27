using PupaLupaServer.Models.EF;

namespace PupaLupaServer.Models
{
    public class MessageModel
    {
        public Guid ChatId { get; set; }

        public Guid? SenderUserId { get; set; }

        public string MessageBody { get; set; }

        public string Created { get; set; }

        public Message ToMessage()
        {
            var tmp = new Message();
            tmp.ChatId = ChatId;
            tmp.SenderUserId = SenderUserId;
            tmp.MessageBody = MessageBody;
            tmp.Created = DateTime.UtcNow;
            return tmp;
        }
    }
}
