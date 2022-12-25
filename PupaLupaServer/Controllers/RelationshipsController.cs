using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PupaLupaServer.Models.EF;

namespace PupaLupaServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RelationshipsController : ControllerBase
    {
        private readonly PupaLupaDbContext _context;

        public RelationshipsController(PupaLupaDbContext context)
        {
            _context = context;
        }

        // GET: api/Relationships
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Relationship>>> GetRelationships()
        //{
        //    return await _context.Relationships.ToListAsync();
        //}

        // GET: api/Relationships/5
        [HttpGet("{userId}")]
        public async Task<ActionResult<List<Relationship>>> GetRelationships(Guid userId)
        {
            var relationships = _context.Relationships.Where(r => r.FirstUserId == userId).ToList();

            if (relationships == null)
            {
                return NotFound();
            }

            return relationships;
        }

        // PUT: api/Relationships/5
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutRelationship(Relationship relationship)
        {
            _context.Entry(relationship).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelationshipExists(relationship.FirstUserId))
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

        //POST: api/Relationships
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Relationship>> PostRelationship(Relationship relationship)
        {
            _context.Relationships.Add(relationship);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RelationshipExists(relationship.FirstUserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("PostRelationship", relationship);
        }

        // DELETE: api/Relationships/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteRelationship(Guid id)
        //{
        //    var relationship = await _context.Relationships.FindAsync(id);
        //    if (relationship == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Relationships.Remove(relationship);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool RelationshipExists(Guid id)
        {
            return _context.Relationships.Any(e => e.FirstUserId == id);
        }
    }
}
