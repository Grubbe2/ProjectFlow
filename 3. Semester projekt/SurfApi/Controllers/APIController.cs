//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Surf.Areas.Identity.Data;
//using Surf.Data;
//using SurfApi.Models;

//namespace SurfApi.Controllers
//{
//    [Route("[controller]")]
//    [ApiController]
//    public class APIController : ControllerBase
//    {
//        private readonly SurfDbContext _context;
//        private readonly UserManager<ApplicationUser> _userManager;

//        public APIController(SurfDbContext context, UserManager<ApplicationUser> userManager)
//        {
//            _context = context;
//            _userManager = userManager;
//        }

//        // GET: api/RentalsAPI
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Surfboard>>> GetAll()
//        {

//            if (!_context.Surfboard.Any())
//            {
//                return NoContent();
//            }
//            else
//            { return Ok(await _context.Surfboard.ToListAsync()); }    
           


//        }

//        // GET: api/API/5
//        //[HttpGet("{id}")]
//        //public async Task<ActionResult<Surfboard>> GetSurfboard(int id)
//        //{
//        //    var surfboard = await _context.Surfboard.FindAsync(id);

//        //    if (surfboard == null)
//        //    {
//        //        return NotFound();
//        //    }

//        //    return surfboard;
//        //}

//        // PUT: api/RentalsAPI/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        //[HttpPut("{id}")]
//        //public async Task<IActionResult> PutRental(int id, Rental rental)
//        //{
//        //    if (id != rental.RentalId)
//        //    {
//        //        return BadRequest();
//        //    }

//        //    _context.Entry(rental).State = EntityState.Modified;

//        //    try
//        //    {
//        //        await _context.SaveChangesAsync();
//        //    }
//        //    catch (DbUpdateConcurrencyException)
//        //    {
//        //        if (!RentalExists(id))
//        //        {
//        //            return NotFound();
//        //        }
//        //        else
//        //        {
//        //            throw;
//        //        }
//        //    }

//        //    return NoContent();
//        //}

//        // POST: api/API
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        //[HttpPost("/Post")]
//        //public async Task<ActionResult<Surfboard>> Post([FromQuery]DateTime startDate, [FromQuery] int id)
//        //{
//        //    var surfboardRental = await _context.Surfboard.FirstOrDefaultAsync(s => s.ID == id);
//        //    var user = await _userManager.GetUserAsync(User);
//        //    var rental = await _context.Rental.FirstOrDefaultAsync(s => s.SurfboardId == id);
//        //    var enddate = startDate.AddDays(5);


//        //    if (surfboardRental != null && User != null && (rental == null || (rental.StartDate > enddate || rental.EndDate < startDate)))
//        //    {
//        //        var rentalToBeMade = new Rental
//        //        {
//        //            SurfboardId = surfboardRental.ID,
//        //            UserId = user.Id,
//        //            StartDate = startDate,
//        //            EndDate = enddate
//        //        };
//        //        if (startDate == DateTime.Today)
//        //        {
//        //            surfboardRental.IsRented = true;
//        //        }


//        //        await _context.Rental.AddAsync(rentalToBeMade);
//        //        await _context.SaveChangesAsync();

//        //        //TempData["SuccesMessage"] = $"Surfboard is now rented from {startDate.ToShortDateString()} to {enddate.ToShortDateString()}";
//        //    }
//        //    else
//        //    {
//        //        return BadRequest("Surfboard not available");
//        //    }
//        //    return Ok(surfboardRental);
//        //}

//        // DELETE: api/API/5
//        //[HttpDelete("{id}")]
//        //public async Task<IActionResult> DeleteRental(int id)
//        //{
//        //    var rental = await _context.Rental.FindAsync(id);
//        //    if (rental == null)
//        //    {
//        //        return NotFound();
//        //    }

//        //    _context.Rental.Remove(rental);
//        //    await _context.SaveChangesAsync();

//        //    return NoContent();
//        //}

//        //private bool RentalExists(int id)
//        //{
//        //    return _context.Rental.Any(e => e.RentalId == id);
//        //}
//    }
//}
