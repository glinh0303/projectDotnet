using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using System.Data;


namespace Project.Controllers
{
    public class DrinksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DrinksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Drinks
        public async Task<IActionResult> Index(int? categoriesid, string sortOrder, string currentFilter,string searchString,int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CategorySortParm"] = sortOrder == "Category" ? "category_desc" : "Category";
           
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            IQueryable<Drink> drinkQuery = _context.Drinks.Include(d => d.Category);
/*            var drink = await drinkQuery.ToListAsync();
*/            var drinks = from d in drinkQuery
                           select d;
            if (!String.IsNullOrEmpty(searchString))
            {
                drinks = drinks.Where(d => d.Category.Name.Contains(searchString)
                                       || d.Name.Contains(searchString));
            }
            if (categoriesid != null)
            {
                drinks = drinks.Where(drink => drink.CategoryId == categoriesid);
            }
           
            switch (sortOrder)
            {
                case "name_desc":
                    drinks = drinks.OrderByDescending(s => s.Name);
                    break;
                case "Category":
                    drinks = drinks.OrderBy(s => s.Category.Name);
                    break;
                case "category_desc":
                    drinks = drinks.OrderByDescending(s => s.Category.Name);
                    break;
                default:
                    drinks = drinks.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 3;
            return View(await PaginatedList<Drink>.CreateAsync(drinks, pageNumber ?? 1, pageSize));
            /*return View(drink);*/
        }

        // GET: Drinks/Details/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Drinks == null)
            {
                return NotFound();
            }

            var drink = await _context.Drinks.Include(d => d.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drink == null)
            {
                return NotFound();
            }

            return View(drink);
        }

        // GET: Drinks/Create
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create()
        {
            ViewData["Types"] = new SelectList(_context.Categories, nameof(Category.Id), nameof(Category.Name));           
            return View();
        }

        // POST: Drinks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,Quantity,Price,CategoryId")] Drink drink)
        {
            if (ModelState.IsValid)
            {
                _context.Add(drink);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }        
            ViewData["Types"] = new SelectList(_context.Set<Category>(), nameof(Category.Id), nameof(Category.Name), drink.CategoryId);
            return View(drink);
        }

        // GET: Drinks/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Drinks == null)
            {
                return NotFound();
            }

            var drink = await _context.Drinks.Include(d => d.Category).FirstOrDefaultAsync(d => d.Id == id);
            if (drink == null)
            {
                return NotFound();
            }
            ViewData["Types"] = new SelectList(_context.Set<Category>(), nameof(Category.Id), nameof(Category.Name), drink.CategoryId);
            return View(drink);
        }

        // POST: Drinks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Quantity,Price,CategoryId")] Drink drink)
        {
            if (id != drink.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drink);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrinkExists(drink.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Types"] = new SelectList(_context.Set<Category>(), nameof(Category.Id), nameof(Category.Name), drink.CategoryId);
            return View(drink);
        }

        // GET: Drinks/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Drinks == null)
            {
                return NotFound();
            }

            var drink = await _context.Drinks.Include(d => d.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drink == null)
            {
                return NotFound();
            }

            return View(drink);
        }

        // POST: Drinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Drinks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Drinks'  is null.");
            }
            var drink = await _context.Drinks.FindAsync(id);
            if (drink != null)
            {
                _context.Drinks.Remove(drink);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrinkExists(int id)
        {
          return _context.Drinks.Any(e => e.Id == id);
        }
        [Authorize]
        public async Task<IActionResult> Order(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {          
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CategorySortParm"] = sortOrder == "Category" ? "category_desc" : "Category";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            IQueryable<Drink> drinkQuery = _context.Drinks.Include(d => d.Category);           
            var drinks = from d in drinkQuery
                         select d;
            if (!String.IsNullOrEmpty(searchString))
            {
                drinks = drinks.Where(d => d.Category.Name.Contains(searchString)
                                       || d.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    drinks = drinks.OrderByDescending(s => s.Name);
                    break;
                case "Category":
                    drinks = drinks.OrderBy(s => s.Category.Name);
                    break;
                case "category_desc":
                    drinks = drinks.OrderByDescending(s => s.Category.Name);
                    break;
                default:
                    drinks = drinks.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 3;
            return View(await PaginatedList<Drink>.CreateAsync(drinks, pageNumber ?? 1, pageSize));
        }
       
    }
}
