using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using Project.Models.BindingModels;

namespace Project.Controllers
{
    public class BillsController : BaseController
    {
        //private readonly ApplicationDbContext _context;

        public BillsController(ApplicationDbContext context) : base(context)
        {
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            /* var applicationDbContext = _context.Orders.Include(o => o.User);*/
            /*return View(await applicationDbContext.ToListAsync());*/
            return View();
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                /*  .Include(o => o.User)*/
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create()
        {
            String userName = User.Identity.Name;
            var profile = CurrentUser?.Profile;
            ViewBag.UserName = profile?.FirstName == null || profile?.LastName == null ? userName : profile.FullName;
            int userId = CurrentUser.Id;
            //var orderDetails = 
            //    await _context.OrderDetails.Include(d => d.Drink).Include(t => t.Toppings).Where(o => o.UserId == userId && o.OrderStatus == 0).ToListAsync();

            var bill = await _context.Orders.Include(x => x.OrderDetails)
                .ThenInclude(x => x.Drink)
                .SingleOrDefaultAsync(or => or.UserId == profile.UserId && or.Status == OrderStatus.Cart);
            //foreach (var item in orderDetails)
            //{
            //    total += item.Payment;
            //}
            var total = bill.OrderDetails.Sum(x => x.Payment);
            ViewBag.Total = total;
            /*ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");*/
            var model = new BillBindingModel
            {
                Id = bill.Id,
                Phone = bill.Phone,
                Payment = bill.Payment,
                Quantity = bill.Quantity,
                Address = bill.Address,
                Note = bill.Note,
                OrderDetails = bill.OrderDetails,
            };
            return View(model);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderDetailId,UserId,CreatedDate,Payment,Quantity,Address,Phone,Note")] Bill order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", order.UserId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", order.UserId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderDetailId,UserId,CreatedDate,Payment,Quantity,Address,Phone,Note")] Bill order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", order.UserId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                /*.Include(o => o.User)*/
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
        /* public async Task<IActionResult> Bill([Bind("Id,UserId,Payment")])
         {
             if (ModelState.IsValid)
             {
                 List<OrderDetail> orderDetails = cart.OrderDetails.ToList();
                 foreach (var item in orderDetails)
                 {
                     Drink drink = item.Drink;
                     drink.Quantity -= item.Quantity;
                     await TryUpdateModelAsync(drink);
                 }

                 _context.Add(cart);
                 await _context.SaveChangesAsync();
                 return Redirect(Url.Action("Index", "Order"));
             }
             return View(cart);
         }*/
    }
}
