using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PupaLupaServer.Models.EF;
using PupaLupaServer.Models.Enums;

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
            var relationships = _context.Relationships.Where(r => r.UserId == userId).ToList();

            if (relationships == null)
            {
                return NotFound();
            }

            return relationships;
        }

        // PUT: api/Relationships/5
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut]
        //public async Task<IActionResult> PutRelationship(Relationship relationship)
        //{
        //    _context.Entry(relationship).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RelationshipExists(relationship.FirstUserId))
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

        //POST: api/Relationships
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Relationship>> PostRelationship(Relationship relationship)
        {
            //add relationship
            _context.Relationships.Add(relationship);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RelationshipExists(relationship.UserId, relationship.FriendId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            //initialize statistic
            var statistic = new Statistic();
            statistic.Id = new Guid(relationship.StatisticsId.ToString());
            statistic.RelationType = (short)RelationType.NotFriendsYet;
            statistic.TimeTogether = 0;
            statistic.FirstMetDate = DateTime.Now.ToUniversalTime();
            statistic.MessagesCount = 0;
            statistic.MeetingsCount = 0;
            _context.Statistics.Add(statistic);
            await _context.SaveChangesAsync();

            CheckFriends(relationship.UserId, relationship.FriendId);

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

        private bool RelationshipExists(Guid userId, Guid friendId)
        {
            return _context.Relationships.Any(r => r.UserId == userId && r.FriendId == friendId);
        }

        private void CheckFriends(Guid userId, Guid friendId)
        {
            //if become friends
            if (RelationshipExists(userId, friendId) && RelationshipExists(friendId, userId))
            {
                //update statistic
                var firstStatisticId = _context.Relationships.FirstOrDefault(r => r.UserId == userId && r.FriendId == friendId).StatisticsId;
                var secondStatisticId = _context.Relationships.FirstOrDefault(r => r.UserId == friendId && r.FriendId == userId).StatisticsId;

                var firstStatisticRecord = _context.Statistics.FirstOrDefault(s => s.Id == firstStatisticId);
                var secondStatisticRecord = _context.Statistics.FirstOrDefault(s => s.Id == secondStatisticId);

                firstStatisticRecord.RelationType = (short)RelationType.Friends;
                secondStatisticRecord.RelationType = (short)RelationType.Friends;

                _context.Entry(firstStatisticRecord).State = EntityState.Modified;
                _context.Entry(secondStatisticRecord).State = EntityState.Modified;

                _context.SaveChanges();
            }
        }
    }
}