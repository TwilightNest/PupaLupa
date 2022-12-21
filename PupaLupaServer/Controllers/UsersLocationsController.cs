using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PupaLupaServer.Models.EF;

namespace PupaLupaServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersLocationsController : ControllerBase
    {
        private readonly PupaLupaDbContext _context;

        public UsersLocationsController(PupaLupaDbContext context)
        {
            _context = context;
        }

        //// GET: api/UsersLocations
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<UsersLocation>>> GetUsersLocations()
        //{
        //    return await _context.UsersLocations.ToListAsync();
        //}

        // GET: api/UsersLocations/5
        [HttpGet("{userId}")]
        public async Task<ActionResult<UsersLocation>> GetUsersLocation(Guid userId)
        {
            var usersLocation = await _context.UsersLocations.FirstOrDefaultAsync(u => u.UserId == userId);

            if (usersLocation == null)
            {
                return NotFound();
            }

            return usersLocation;
        }

        // PUT: api/UsersLocations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutUsersLocation(UsersLocation newUsersLocation)
        {
            _context.Entry(newUsersLocation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersLocationExists(newUsersLocation.UserId))
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

        // POST: api/UsersLocations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<UsersLocation>> PostUsersLocation(UsersLocation usersLocation)
        //{
        //    _context.UsersLocations.Add(usersLocation);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (UsersLocationExists(usersLocation.Id))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetUsersLocation", new { id = usersLocation.Id }, usersLocation);
        //}

        // DELETE: api/UsersLocations/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUsersLocation(Guid id)
        //{
        //    var usersLocation = await _context.UsersLocations.FindAsync(id);
        //    if (usersLocation == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.UsersLocations.Remove(usersLocation);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool UsersLocationExists(Guid id)
        {
            return _context.UsersLocations.Any(e => e.UserId == id);
        }
    }
}