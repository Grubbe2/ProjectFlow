using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Surf.Data;
using Surf.Models;

namespace Surf.Controllers
{
    [Authorize(Roles =  "Admin")]
    public class SurfboardsController : Controller
    {
        private readonly SurfDbContext _context;

        public SurfboardsController(SurfDbContext context)
        {
            _context = context;
        }

        // GET: Surfboards
        public async Task<IActionResult> Index(
    string sortOrder,
    string currentFilter,
    string searchString,
    int? pageNumber)
        {
            
            

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

            // Retrieve all surfboards from _context local database
            //And storing in the 'surfboards' variable
            var surfboards = from s in _context.Surfboard
                             select s;
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
            return View(await PaginatedList<Surfboard>.CreateAsync(surfboards.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        

        // GET: Surfboards/Details/5
        public async Task<IActionResult> Details(int? id)
            {
                if (id == null || _context.Surfboard == null)
                {
                    return NotFound();
                }

                var surfboard = await _context.Surfboard
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (surfboard == null)
                {
                    return NotFound();
                }

                return View(surfboard);
            }

            // GET: Surfboards/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: Surfboards/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("ID,Name,Length,Width,Thickness,Volume,Price,Type,Equipment,Image")] Surfboard surfboard)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(surfboard);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(surfboard);
            }

            // GET: Surfboards/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null || _context.Surfboard == null)
                {
                    return NotFound();
                }

                var surfboard = await _context.Surfboard.FindAsync(id);
                if (surfboard == null)
                {
                    return NotFound();
                }
                return View(surfboard);
            }

        // POST: Surfboards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, byte[] rowVersion)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Surfboard
            
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);

            var surfboardToUpdate = await _context.Surfboard.FirstOrDefaultAsync(m => m.ID == id);

            if (surfboardToUpdate == null)
            {
                Surfboard deletedSurfboard = new Surfboard();
                await TryUpdateModelAsync(deletedSurfboard);
                ModelState.AddModelError(string.Empty,
                    "Unable to save changes. The Surfboard was deleted by another user.");
                ViewData["SurfboardID"] = new SelectList(_context.Surfboard, "ID", "Name", deletedSurfboard.ID);
                return View(deletedSurfboard);
            }

            _context.Entry(surfboardToUpdate).Property("RowVersion").OriginalValue = rowVersion;

            if (await TryUpdateModelAsync<Surfboard>(
                surfboardToUpdate,
                "",
                s => s.Name, s => s.Length, s => s.Width, s => s.Thickness, s => s.Volume, s => s.Price, s => s.Type, s => s.ID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (Surfboard)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty,
                            "Unable to save changes. The Surfboard was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Surfboard)databaseEntry.ToObject();

                        if (databaseValues.Name != clientValues.Name)
                        {
                            ModelState.AddModelError("Name", $"Current value: {databaseValues.Name}");
                        }
                        if (databaseValues.Length != clientValues.Length)
                        {
                            ModelState.AddModelError("Length", $"Current value: {databaseValues.Length:c}");
                        }
                        if (databaseValues.Width != clientValues.Width)
                        {
                            ModelState.AddModelError("Width", $"Current value: {databaseValues.Width:d}");
                        }
                        if (databaseValues.Thickness != clientValues.Thickness)
                        {
                            ModelState.AddModelError("Thickness", $"Current value: {databaseValues?.Thickness}");
                        }
                        if (databaseValues.Volume != clientValues.Volume)
                        {
                            ModelState.AddModelError("Volume", $"Current value: {databaseValues?.Volume}");
                        }
                        if (databaseValues.Price != clientValues.Price)
                        {
                            ModelState.AddModelError("Price", $"Current value: {databaseValues?.Price}");
                        }
                        if (databaseValues.Type != clientValues.Type)
                        {
                            ModelState.AddModelError("Type", $"Current value: {databaseValues?.Type}");
                        }
                        if (databaseValues.ID != clientValues.ID)
                        {
                            ModelState.AddModelError("ID", $"Current value: {databaseValues?.ID}");
                        }

                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                                + "was modified by another user after you got the original value. The "
                                + "edit operation was canceled and the current values in the database "
                                + "have been displayed. If you still want to edit this record, click "
                                + "the Save button again. Otherwise click the Back to List hyperlink.");
                        surfboardToUpdate.RowVersion = (byte[])databaseValues.RowVersion;
                        ModelState.Remove("RowVersion");
                    }
                }
            }
            ViewData["ID"] = new SelectList(_context.Surfboard, "ID", "Name", surfboardToUpdate.ID);
            return View(surfboardToUpdate);
        }

        // GET: Surfboards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Surfboard == null)
            {
                return NotFound();
            }

            var surfboard = await _context.Surfboard
                .FirstOrDefaultAsync(m => m.ID == id);
            if (surfboard == null)
            {
                return NotFound();
            }

            return View(surfboard);
        }

        // POST: Surfboards/Delete/5
        [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                if (_context.Surfboard == null)
                {
                    return Problem("Entity set 'SurfContext.Surfboard'  is null.");
                }
                var surfboard = await _context.Surfboard.FindAsync(id);
                if (surfboard != null)
                {
                    _context.Surfboard.Remove(surfboard);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool SurfboardExists(int id)
            {
                return (_context.Surfboard?.Any(e => e.ID == id)).GetValueOrDefault();
            }
        }
    }

