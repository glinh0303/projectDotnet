﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using Project.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Project.Controllers
{
    public class OrderDetailsController : BaseController
    {
        //private readonly ApplicationDbContext _context;

        public OrderDetailsController(ApplicationDbContext context) : base(context) { }

        // GET: OrderDetails
        public async Task<IActionResult> Index(int? toppingId)
        {
            String userName = User.Identity.Name;
            var user = _context.Users.Include(u => u.Profile).SingleOrDefault(u => u.UserName == userName);
            var existingProfile = user.Profile;
         
            IQueryable<OrderDetail> orderDetailsQuery = _context.OrderDetails.Include(o => o.Toppings)
                                                                             .Include(o => o.Drink);
            if (toppingId != null)
            {
                orderDetailsQuery = orderDetailsQuery.Where(o => o.Toppings.Any(t => t.Id == toppingId));
            }
            var orderDetails = await orderDetailsQuery.ToListAsync();
            return View(orderDetails);
        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            String userName = User.Identity.Name;
            var user = _context.Users.Include(u => u.Profile).SingleOrDefault(u => u.UserName == userName);
            var existingProfile = user.Profile;
            if (id == null || _context.OrderDetails == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails
                .Include(o => o.Drink)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public async Task<IActionResult> Create()
        {
            String userName = User.Identity.Name;
            var user = _context.Users.Include(u => u.Profile).SingleOrDefault(u => u.UserName == userName);
            var existingProfile = user.Profile;
            ViewData["Toppings"] = await _context.Toppings.ToListAsync();
            ViewData["DrinkId"] = new SelectList(_context.Drinks, "Id", "Name");
            /*            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Address");
            */
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quantity,Payment,Drink")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            /*            ViewData["DrinkId"] = new SelectList(_context.Drinks, "Id", "Name", orderDetail.DrinkId);
            */
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            String userName = User.Identity.Name;
            var user = _context.Users.Include(u => u.Profile).SingleOrDefault(u => u.UserName == userName);
            var existingProfile = user.Profile;
            if (id == null || _context.OrderDetails == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails.Include(o => o.Toppings).FirstOrDefaultAsync(o => o.Id == id);
            ViewData["Toppings"] = await _context.Toppings.ToListAsync();
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderId,DrinkId,Quantity,Payment")] OrderDetail orderDetail)
        {
            if (id != orderDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailExists(orderDetail.Id))
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
            /*            ViewData["DrinkId"] = new SelectList(_context.Drinks, "Id", "Name", orderDetail.DrinkId);
            */
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            String userName = User.Identity.Name;
            var user = _context.Users.Include(u => u.Profile).SingleOrDefault(u => u.UserName == userName);
            var existingProfile = user.Profile;
            if (id == null || _context.OrderDetails == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails
                .Include(o => o.Drink)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderDetails == null)
            {
                return Problem("Entity set 'ApplicationDbContext.OrderDetails'  is null.");
            }
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Cart));
        }

        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetails.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Order(int id)
        {
            var drink = await _context.Drinks.FindAsync(id);
            ViewBag.Drink = drink.Name;
            ViewBag.Price = drink.Price;
            ViewBag.Quantity = drink.Quantity;
            ViewData["Toppings"] = await _context.Toppings.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Order([Bind("Quantity,Payment,Drink,Size,UserId")] OrderDetail orderDetail, int[] selectedToppings, int id)
        {
            Drink drink = await _context.Drinks.FindAsync(id);
            orderDetail.Drink = drink;
            String userName = CurrentUser.UserName;
            var user = _context.Users.Include(u => u.Profile).SingleOrDefault(u => u.UserName == userName);  

            if (ModelState.IsValid)
            {
                decimal payment = DoSize(orderDetail);
                if (selectedToppings != null)
                {
                    orderDetail.Toppings = _context.Toppings.Where(x => selectedToppings.Contains(x.Id)).ToList();
                    List<Topping> toppings = (List<Topping>)orderDetail.Toppings;
                    payment = DoToppings(toppings, payment);
                }
                int quantity = orderDetail.Quantity;
                orderDetail.Payment = (payment * quantity);

                var bill = await _context.Orders.SingleOrDefaultAsync(order => order.UserId == CurrentUser.Id && order.Status == OrderStatus.Cart);
                if (bill != null)
                {
                    orderDetail.BillId = bill.Id;
                    orderDetail.Bill = bill;
                    _context.OrderDetails.Add(orderDetail);                  
                }
                else
                {
                    bill = new Bill()
                    {
                        OrderDetails = new List<OrderDetail>(),
                        UserId = CurrentUser.Id,
                        Status = OrderStatus.Cart
                    };
                    _context.Orders.Add(bill);
                    await _context.SaveChangesAsync();
                    //Luôn duy tri 1 bill ở trạng thái cart: 
                    //Chưa có bill: new Bill()
                    //Có rồi. Add OrderDetail vào Bill                 
                    orderDetail.BillId = bill.Id;
                    orderDetail.Bill = bill;
                    _context.OrderDetails.Add(orderDetail);                
                }
                ViewData["Toppings"] = await _context.Toppings.ToListAsync();
                await _context.SaveChangesAsync();
                return Redirect(Url.Action("Order", "Drinks"));
            }
            return View(orderDetail);
        }
        private decimal DoSize(OrderDetail orderDetail)
        {
            decimal price = orderDetail.Drink.Price;
            if (orderDetail.Size == DrinkSize.M)
            {
                price += 5;
            }
            else if (orderDetail.Size == DrinkSize.L)
            {
                price += 10;
            }
            return price;
        }

        private decimal DoToppings(List<Topping> toppings, decimal payment)
        {
            foreach (var topping in toppings)
            {
                payment += topping.Price;
            }
            return payment;
        }
        public async Task<IActionResult> Cart()
        {
            String userName = User.Identity.Name;
            var user = _context.Users.Include(u => u.Profile).SingleOrDefault(u => u.UserName == userName);
            int userId = user.Id;
            decimal totalBill = 0;           
            var cart = await _context.Orders
                                .Include(x => x.OrderDetails)
                                .ThenInclude(d => d.Drink)
                                .Include(x => x.OrderDetails)
                                    .ThenInclude(x => x.Toppings)
                                .FirstOrDefaultAsync(o => o.UserId == userId && o.Status == OrderStatus.Cart);
            if (cart == null)
            {
                ViewBag.OrderDetails = new List<OrderDetail>();               
            }
            else
            {
                ViewBag.OrderDetails = cart.OrderDetails.ToList();           
                totalBill = cart.OrderDetails.Sum(x => x.Payment * x.Quantity);
            }
            string total = totalBill.FormatNumber();
            ViewBag.Total = total;
            return View();
        }
    }
}
