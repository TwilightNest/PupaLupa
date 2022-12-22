using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PupaLupaServer.Models.EF;

namespace PupaLupaServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserFriendsController : ControllerBase
    {
        private readonly PupaLupaDbContext _context;

        public UserFriendsController(PupaLupaDbContext context)
        {
            _context = context;
        }

        // GET: api/UserFriends
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<UserFriend>>> GetUserFriends()
        //{
        //    return await _context.UserFriends.ToListAsync();
        //}

        // GET: api/UserFriends/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserFriend>> GetUserFriend(Guid id)
        {
            var userFriend = await _context.UserFriends.FindAsync(id);

            if (userFriend == null)
            {
                return NotFound();
            }

            return userFriend;
        }

        // PUT: api/UserFriends/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutUserFriend(UserFriend userFriend)
        {
            _context.Entry(userFriend).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserFriendExists(userFriend.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserFriends
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<UserFriend>> PostUserFriend(UserFriend userFriend)
        //{
        //    _context.UserFriends.Add(userFriend);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (UserFriendExists(userFriend.UserId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetUserFriend", new { id = userFriend.UserId }, userFriend);
        //}

        // DELETE: api/UserFriends/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUserFriend(Guid id)
        //{
        //    var userFriend = await _context.UserFriends.FindAsync(id);
        //    if (userFriend == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.UserFriends.Remove(userFriend);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool UserFriendExists(Guid id)
        {
            return _context.UserFriends.Any(e => e.UserId == id);
        }
    }
}
