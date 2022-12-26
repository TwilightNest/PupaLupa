using Microsoft.AspNetCore.Mvc;
using PupaLupaServer.Models.EF;

namespace PupaLupaServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly PupaLupaDbContext _context;

        public StatisticsController(PupaLupaDbContext context)
        {
            _context = context;
        }

        // GET: api/Statistics
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Statistic>>> GetStatistics()
        //{
        //    return await _context.Statistics.ToListAsync();
        //}

        // GET: api/Statistics/5
        [HttpGet("{statisticId}")]
        public async Task<ActionResult<Statistic>> GetStatistic(Guid statisticId)
        {
            var statistic = await _context.Statistics.FindAsync(statisticId);

            if (statistic == null)
            {
                return NotFound();
            }

            return statistic;
        }

        // PUT: api/Statistics/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutStatistic(Guid id, Statistic statistic)
        //{
        //    if (id != statistic.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(statistic).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!StatisticExists(id))
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

        // POST: api/Statistics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Statistic>> PostStatistic(StatisticModel statisticModel)
        //{
        //    var newStatistic = statisticModel.ToStatistic();
        //    _context.Statistics.Add(newStatistic);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (StatisticExists(newStatistic.Id))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("PostStatistic", newStatistic);
        //}

        // DELETE: api/Statistics/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteStatistic(Guid id)
        //{
        //    var statistic = await _context.Statistics.FindAsync(id);
        //    if (statistic == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Statistics.Remove(statistic);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool StatisticExists(Guid id)
        {
            return _context.Statistics.Any(e => e.Id == id);
        }
    }
}