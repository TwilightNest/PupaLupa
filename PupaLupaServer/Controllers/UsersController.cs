using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PupaLupaServer.Models.EF;

namespace PupaLupaServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly PupaLupaDbContext _context;

        public UsersController(PupaLupaDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        //{
        //    return await _context.Users.ToListAsync();
        //}

        // GET: api/Users/5
        [HttpGet("{data}")]
        public async Task<ActionResult<User>> GetUser(string data)
        {
            var userByName = await _context.Users.FirstOrDefaultAsync(u => u.Login == data);

            if (userByName == null)
            {
                var userByEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == data);
                if (userByEmail == null)
                {
                    var userByPhoneNumber = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == data);
                    if (userByPhoneNumber == null)
                        return NotFound();
                    else return userByPhoneNumber;
                }
                else return userByEmail;
            }
            else return userByName;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUser(Guid id, User user)
        //{
        //    if (id != user.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(user).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            //add user
            user.Id = Guid.NewGuid();
            _context.Users.Add(user);
            //initialize userLocation
            var userLocation = new UsersLocation();
            userLocation.UserId = user.Id;
            userLocation.Latitude = null;
            userLocation.Longitude = null;
            _context.UsersLocations.Add(userLocation);
            //initialize userFriends
            var userFriend = new UserFriend();
            userFriend.UserId = user.Id;
            userFriend.FriendsIds = new Guid[0];
            _context.UserFriends.Add(userFriend);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("PostUser", user);
        }

        // DELETE: api/Users/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUser(Guid id)
        //{
        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Users.Remove(user);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}