using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PupaLupaServer.Models;
using PupaLupaServer.Models.EF;

namespace PupaLupaServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly PupaLupaDbContext _context;

        public ChatsController(PupaLupaDbContext context)
        {
            _context = context;
        }

        // GET: api/Chats
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Chat>>> GetChats()
        //{
        //    return await _context.Chats.ToListAsync();
        //}

        // GET: api/Chats/5
        [HttpGet("{chatId}")]
        public async Task<ActionResult<Chat>> GetChat(Guid chatId)
        {
            var chat = await _context.Chats.FirstOrDefaultAsync(c => c.Id == chatId);

            if (chat == null)
            {
                return NotFound();
            }

            return chat;
        }

        // PUT: api/Chats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutChat(Guid id, Chat chat)
        //{
        //    if (id != chat.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(chat).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ChatExists(id))
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

        // POST: api/Chats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Chat>> PostChat(NewChatModel chatModel)
        {
            var newChat = chatModel.ToChat();
            newChat.Id = Guid.NewGuid();
            _context.Chats.Add(newChat);
            var firstUser = _context.Users.FirstOrDefault(u => u.Id == chatModel.FirstUserId);
            var secondUser = _context.Users.FirstOrDefault(u => u.Id == chatModel.SecondUserId);
            _context.UsersChats.Add(new UsersChat() { UserId = firstUser.Id, ChatId = newChat.Id });
            _context.UsersChats.Add(new UsersChat() { UserId = secondUser.Id, ChatId = newChat.Id });

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ChatExists(newChat.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("PostChat", newChat);
        }

        // DELETE: api/Chats/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteChat(Guid id)
        //{
        //    var chat = await _context.Chats.FindAsync(id);
        //    if (chat == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Chats.Remove(chat);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool ChatExists(Guid id)
        {
            return _context.Chats.Any(e => e.Id == id);
        }
    }
}