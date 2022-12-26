using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PupaLupaServer.Models.EF;

namespace PupaLupaServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersChatsController : ControllerBase
    {
        private readonly PupaLupaDbContext _context;

        public UsersChatsController(PupaLupaDbContext context)
        {
            _context = context;
        }

        // GET: api/UsersChats
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<UsersChat>>> GetUsersChats()
        //{
        //    return await _context.UsersChats.ToListAsync();
        //}

        // GET: api/UsersChats/5
        [HttpGet("user={userId}")]
        public async Task<ActionResult<List<Chat>>> GetUserChats(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            var userChatsIds = _context.UsersChats.Where(uc => uc.UserId == userId).Select(uc => uc.ChatId).ToList();
            var chats = _context.Chats.Where(c => userChatsIds.Contains(c.Id)).ToList();

            return chats;
        }

        // GET: api/UsersChats/5
        [HttpGet("chat={chatId}")]
        public async Task<ActionResult<List<User>>> GetChatUsers(Guid chatId)
        {
            var chat = await _context.Chats.FirstOrDefaultAsync(c => c.Id == chatId);

            if (chat == null)
            {
                return NotFound();
            }

            var chatsUser = _context.UsersChats.Where(uc => uc.ChatId == chatId).ToList();
            var users = _context.Users.Where(u => chatsUser.Select(uc => uc.UserId).ToList().Contains(u.Id)).ToList();

            return users;
        }

        // PUT: api/UsersChats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUsersChat(Guid id, UsersChat usersChat)
        //{
        //    if (id != usersChat.UserId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(usersChat).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UsersChatExists(id))
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

        // POST: api/UsersChats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<UsersChat>> PostUsersChat(UsersChat usersChat)
        //{
        //    _context.UsersChats.Add(usersChat);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (UsersChatExists(usersChat.UserId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetUsersChat", new { id = usersChat.UserId }, usersChat);
        //}

        // DELETE: api/UsersChats/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUsersChat(Guid id)
        //{
        //    var usersChat = await _context.UsersChats.FindAsync(id);
        //    if (usersChat == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.UsersChats.Remove(usersChat);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool UsersChatExists(Guid id)
        {
            return _context.UsersChats.Any(e => e.UserId == id);
        }
    }
}