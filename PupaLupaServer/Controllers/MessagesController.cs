using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PupaLupaServer.Models;
using PupaLupaServer.Models.EF;

namespace PupaLupaServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly PupaLupaDbContext _context;

        public MessagesController(PupaLupaDbContext context)
        {
            _context = context;
        }

        // GET: api/Messages
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        //{
        //    return await _context.Messages.ToListAsync();
        //}

        // GET: api/Messages/5
        [HttpGet("{chatId}")]
        public async Task<ActionResult<List<Message>>> GetChatMessages(Guid chatId)
        {
            var chatMessages = _context.Messages.Where(m => m.ChatId == chatId).ToList();

            if (chatMessages == null)
            {
                return NotFound();
            }

            return chatMessages;
        }

        // PUT: api/Messages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMessage(Guid id, Message message)
        //{
        //    if (id != message.ChatId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(message).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MessageExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Messages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage(MessageModel messageModel)
        {
            var newMessage = messageModel.ToMessage();
            _context.Messages.Add(newMessage);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MessageExists(newMessage.ChatId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("PostMessage", newMessage);
        }

        // DELETE: api/Messages/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMessage(Guid id)
        //{
        //    var message = await _context.Messages.FindAsync(id);
        //    if (message == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Messages.Remove(message);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool MessageExists(Guid id)
        {
            return _context.Messages.Any(e => e.ChatId == id);
        }
    }
}