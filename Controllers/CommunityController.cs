using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reddit.Dtos;
using Reddit.Entities;
using Reddit.Models;

namespace Reddit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityController : ControllerBase
    {
        private readonly ApplcationDBContext _context;

        public CommunityController(ApplcationDBContext context)
        {
            _context = context;
        }

        // GET: api/Community
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Communities>>> GetCommunities()
        {
            return await _context.Communities.Include(c => c.Posts).Include(c => c.Users).ToListAsync();
        }

        // GET: api/Community/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Communities>> GetCommunity(int id)
        {
            var community = await _context.Communities.Include(c => c.Posts).Include(c => c.Users).FirstOrDefaultAsync(c => c.Id == id);

            if (community == null)
            {
                return NotFound();
            }

            return community;
        }

        // POST: api/Community
        [HttpPost]
        public async Task<ActionResult<Communities>> PostCommunity(CreateCommunityDto createCommunityDto)
        {
            var community = new Communities
            {
                Name = createCommunityDto.Name,
                Description = createCommunityDto.Description
            };

            _context.Communities.Add(community);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCommunity", new { id = community.Id }, community);
        }
        // PUT: api/Community/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommunity(int id, Communities community)
        {
            if (id != community.Id)
            {
                return BadRequest();
            }

            _context.Entry(community).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommunityExists(id))
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

        // DELETE: api/Community/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommunity(int id)
        {
            var community = await _context.Communities.FindAsync(id);
            if (community == null)
            {
                return NotFound();
            }

            _context.Communities.Remove(community);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommunityExists(int id)
        {
            return _context.Communities.Any(e => e.Id == id);
        }
    }
}