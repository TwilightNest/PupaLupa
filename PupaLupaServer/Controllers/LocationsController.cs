using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PupaLupaServer.Models.EF;

namespace PupaLupaServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly PupaLupaDbContext _context;

        public LocationsController(PupaLupaDbContext context)
        {
            _context = context;
        }

        // GET: api/Locations
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Location>>> GetLocations()
        //{
        //    return await _context.Locations.ToListAsync();
        //}

        // GET: api/Locations/5
        [HttpGet("{userId}")]
        public async Task<ActionResult<Location>> GetLocation(Guid userId)
        {
            var location = await _context.Locations.FirstOrDefaultAsync(l => l.UserId == userId);

            if (location == null)
            {
                return NotFound();
            }

            return location;
        }

        // PUT: api/Locations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutLocation(Location location)
        {
            _context.Entry(location).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(location.UserId))
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

        // POST: api/Locations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Location>> PostLocation(Location location)
        //{
        //    _context.Locations.Add(location);

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (LocationExists(location.ObjectId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("PostLocation", location);
        //}

        // DELETE: api/Locations/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteLocation(Guid id)
        //{
        //    var location = await _context.Locations.FindAsync(id);
        //    if (location == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Locations.Remove(location);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool LocationExists(Guid id)
        {
            return _context.Locations.Any(e => e.UserId == id);
        }
    }
}