using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Surf.Areas.Identity.Data;
using Surf.Data;
using Surf.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;
using System.Drawing.Printing;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Surf.Controllers
{
    public class UserController : Controller
    {
        private readonly SurfDbContext _context;
        private readonly HttpClient _httpClient;
        private readonly UserManager<ApplicationUser> _usermananger;
        private readonly IHttpClientFactory _iHttpClientFactory;

        public UserController(/*SurfDbContext context,*/ UserManager <ApplicationUser> usermanager, IHttpClientFactory iHttpClientFactory)
        {
            //_context = context;
            //_httpClient = new HttpClient();
            //_httpClient.BaseAddress = new Uri("https://localhost:7054");
            //_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _usermananger = usermanager;
            _iHttpClientFactory = iHttpClientFactory;
        }

        // GET: User
        public async Task<IActionResult> Index(
    string sortOrder,
    string currentFilter,
    string searchString,
    int? pageNumber)
        {

            // Retrieve all surfboards from _context local database
            //And storing in the 'surfboards' variable
            IEnumerable<Surfboard> surfboards = Enumerable.Empty<Surfboard>();
            var user = await _usermananger.GetUserAsync(User);
            HttpClient client = _iHttpClientFactory.CreateClient("API");
            if (user != null)
            {
                
                HttpResponseMessage boardResponse = client.GetAsync($"v1.0/RentalsApi/{user.Id}").Result;
                if (boardResponse.IsSuccessStatusCode)
                {
                    var data = boardResponse.Content.ReadAsStringAsync().Result;
                    surfboards = JsonConvert.DeserializeObject<IEnumerable<Surfboard>>(data);
                }
            }
            else
            {
                string notsigned = "NotSignedIn";
                
                HttpResponseMessage boardResponse = client.GetAsync($"v2.0/RentalsApi/{notsigned}").Result;
                if (boardResponse.IsSuccessStatusCode)
                {
                    var data = boardResponse.Content.ReadAsStringAsync().Result;
                    surfboards = JsonConvert.DeserializeObject<IEnumerable<Surfboard>>(data);
                }
            }
            

            // Sort
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = sortOrder == "Name" ? "name_desc" : "Name";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["LengthSortParm"] = sortOrder == "Length" ? "length_desc" : "Length";
            ViewData["WidthSortParm"] = sortOrder == "Width" ? "width_desc" : "Width";
            ViewData["ThicknessSortParm"] = sortOrder == "Thickness" ? "thickness_desc" : "Thickness";
            ViewData["VolumeSortParm"] = sortOrder == "Volume" ? "volume_desc" : "Volume";
            ViewData["TypeSortParm"] = sortOrder == "Type" ? "type_desc" : "Type";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            ViewData["EquipmentSortParm"] = sortOrder == "Equipment" ? "equipment_desc" : "Equipment";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            

            if (!String.IsNullOrEmpty(searchString))
            {
                surfboards = surfboards.Where(s => s.Name.Contains(searchString)
                                       || s.Type.Contains(searchString)
                                       || s.Equipment.Contains(searchString)
                                       || s.Length.ToString().Contains(searchString)
                                       || s.Width.ToString().Contains(searchString)
                                       || s.Thickness.ToString().Contains(searchString)
                                       || s.Volume.ToString().Contains(searchString)
                                       || s.Price.ToString().Contains(searchString));
            }

            //RentalCheck(); // tjekker om surfboardet er udlejet eller ej. 
            //sort
            switch (sortOrder)
            {
                case "Name":
                    surfboards = surfboards.OrderBy(s => s.Name);
                    break;
                case "name_desc":
                    surfboards = surfboards.OrderByDescending(s => s.Name);
                    break;
                case "Length":
                    surfboards = surfboards.OrderBy(s => s.Length);
                    break;
                case "length_desc":
                    surfboards = surfboards.OrderByDescending(s => s.Length);
                    break;
                case "Width":
                    surfboards = surfboards.OrderBy(s => s.Width);
                    break;
                case "width_desc":
                    surfboards = surfboards.OrderByDescending(s => s.Width);
                    break;
                case "Thickness":
                    surfboards = surfboards.OrderBy(s => s.Thickness);
                    break;
                case "thickness_desc":
                    surfboards = surfboards.OrderByDescending(s => s.Thickness);
                    break;
                case "Volume":
                    surfboards = surfboards.OrderBy(s => s.Volume);
                    break;
                case "volume_desc":
                    surfboards = surfboards.OrderByDescending(s => s.Volume);
                    break;
                case "Type":
                    surfboards = surfboards.OrderBy(s => s.Type);
                    break;
                case "type_desc":
                    surfboards = surfboards.OrderByDescending(s => s.Type);
                    break;
                case "Price":
                    surfboards = surfboards.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    surfboards = surfboards.OrderByDescending(s => s.Price);
                    break;
                case "Equipment":
                    surfboards = surfboards.OrderBy(s => s.Equipment);
                    break;
                case "equipment_desc":
                    surfboards = surfboards.OrderByDescending(s => s.Equipment);
                    break;
            }

            int pageSize = 5;
            return View(await PaginatedList<Surfboard>.CreateAsync(surfboards, pageNumber ?? 1, pageSize));


        }


        // GET: User/Edit/5
        //[Authorize]
        public async Task<IActionResult> Create(int? id) // Rental
        {
            var user = await _usermananger.GetUserAsync(User);
            HttpClient client = _iHttpClientFactory.CreateClient("API");
            HttpResponseMessage response;
            if (user != null)
            {
                response = client.GetAsync($"v1.0/RentalsApi/Board/{id}").Result;
            }
            else
            {
                 response = client.GetAsync($"v2.0/RentalsApi/Board/{id}").Result;
            }

            //HttpResponseMessage response = client.GetAsync($"v1.0/RentalsApi/Board/{id}").Result;
            Surfboard surfboard = new();
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                surfboard = JsonConvert.DeserializeObject<Surfboard>(data);
            }
            if (id == null || surfboard.ID == 0)
            {
                return NotFound();
            }

            return View(surfboard);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DateTime startDate, Surfboard surfboard)
        {
            var user = await _usermananger.GetUserAsync(User);
            HttpClient client = _iHttpClientFactory.CreateClient("API");
            HttpResponseMessage response;
            Rental rental = new Rental()
            {
                SurfboardId = surfboard.ID,
                StartDate = startDate,
                EndDate = startDate.AddDays(5),
                Surfboard = surfboard
            };

            if (user != null)
            {
                rental.UserId = user.Id;
                response = client.PostAsJsonAsync($"v1.0/RentalsApi", rental).Result;
            }
            else
            {
                rental.UserId = "NotSignedIn";
                response = client.PostAsJsonAsync($"v2.0/RentalsApi", rental).Result;
            }

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                surfboard = JsonConvert.DeserializeObject<Surfboard>(data);
                TempData["SuccesMessage"] = $"Surfboard is now rented";
            }
            else
            {
                var errorResponse = response.Content.ReadAsStringAsync().Result.Trim('"');

                if (errorResponse == "log-in required for more than 1 active rentals")
                {
                    ModelState.AddModelError(string.Empty, "Log-in required for more than 1 active rentals");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Unable to rent surfboard");
                }
            }
            return View(surfboard);
            

        }
    }
}
