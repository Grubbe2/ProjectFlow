using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurfApi.Data;
using SurfApi.Models;

namespace SurfApi.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/RentalsApi")]
    [ApiVersion("2.0")]
    public class RentalsApiController : ControllerBase
    {
        private readonly SurfApiContext _context;
        private IQueryable<Surfboard> _surfboards;

        public RentalsApiController(SurfApiContext context)
        {
            _context = context;
        }

        // GET: RentalsAPI/Boards
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Surfboard>>> GetAllBoards(string userId)
        {
            if (!_context.Surfboard.Any())
            {
                return NoContent();
            }
            else
            {
                var unavailableDate = DateTime.Today.AddDays(5);
                _surfboards = from s in _context.Surfboard
                              where (s.ID == 1 || s.ID == 2) &&
                              !_context.Rental.Any(r => r.SurfboardId == s.ID) ||
                              _context.Rental.Any(r => r.SurfboardId == s.ID && (DateTime.Today > r.EndDate || unavailableDate < r.StartDate)) ||
                              userId == "NotSignedIn" && _context.Rental.Any(r => r.SurfboardId == s.ID && r.UserId == userId)
                              select s;
                return Ok(await _surfboards.ToListAsync());
            }
        }

        //GET: api/API/5
        [HttpGet("Board/{id}")]
        public async Task<ActionResult<Surfboard>> GetSurfboard(int id)
        {
            if (id < 1 || id > 2)
            {
                return NotFound();
            }

            var surfboard = await _context.Surfboard.FirstOrDefaultAsync(r => r.ID == id);

            return Ok(surfboard);
        }

        // POST: api/API
        [HttpPost]
        public async Task<ActionResult<Surfboard>> Post([FromBody] Rental rental)
        {
            rental.Surfboard = await _context.Surfboard.FirstOrDefaultAsync(s => s.ID == rental.SurfboardId);
            
            var ExistingRentals = await _context.Rental
                .Where(r => r.UserId == rental.UserId && r.EndDate >= DateTime.Now.Date)
                .ToListAsync();

            if (ExistingRentals.Count > 0) 
            {
                return BadRequest("log-in required for more than 1 active rentals");
            }

            var rentalExist = await _context.Rental
                .Where(s => s.SurfboardId == rental.SurfboardId)
                .ToListAsync();

            if (rental.Surfboard != null && 
                rental.UserId != null && 
                (rental.StartDate >= DateTime.Now.Date) &&
                !rentalExist.Any(r => r.StartDate <= rental.EndDate && r.EndDate >= rental.StartDate))
            {
                await _context.Rental.AddAsync(rental);
                await _context.SaveChangesAsync();
            }
            else
            {
                return BadRequest("Surfboard not available");
            }
            return Ok(rental.Surfboard);
        }
    }
}
