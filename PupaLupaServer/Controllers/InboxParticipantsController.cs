using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PupaLupaServer.Models.EF;

namespace PupaLupaServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InboxParticipantsController : ControllerBase
    {
        private readonly PupaLupaDbContext _context;

        public InboxParticipantsController(PupaLupaDbContext context)
        {
            _context = context;
        }

        // GET: api/InboxParticipants
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<InboxParticipant>>> GetInboxParticipants()
        //{
        //    return await _context.InboxParticipants.ToListAsync();
        //}

        // GET: api/InboxParticipants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InboxParticipant>> GetInboxParticipant(Guid userId)
        {
            var inboxParticipant = await _context.InboxParticipants.FirstOrDefaultAsync(ip => ip.SenderUserId == userId);

            if (inboxParticipant == null)
            {
                return NotFound();
            }

            return inboxParticipant;
        }

        // PUT: api/InboxParticipants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutInboxParticipant(Guid id, InboxParticipant inboxParticipant)
        //{
        //    if (id != inboxParticipant.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(inboxParticipant).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!InboxParticipantExists(id))
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

        // POST: api/InboxParticipants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InboxParticipant>> PostInboxParticipant(InboxParticipant inboxParticipant)
        {
            _context.InboxParticipants.Add(inboxParticipant);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InboxParticipantExists(inboxParticipant.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInboxParticipant", new { id = inboxParticipant.Id }, inboxParticipant);
        }

        // DELETE: api/InboxParticipants/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteInboxParticipant(Guid id)
        //{
        //    var inboxParticipant = await _context.InboxParticipants.FindAsync(id);
        //    if (inboxParticipant == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.InboxParticipants.Remove(inboxParticipant);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool InboxParticipantExists(Guid id)
        {
            return _context.InboxParticipants.Any(e => e.Id == id);
        }
    }
}
