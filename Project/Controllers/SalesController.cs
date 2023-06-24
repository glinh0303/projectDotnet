using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Project.Models;
using Project.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Services;

namespace Project.Controllers
{
   

    public class SalesController : BaseController
    {
        public SalesController(ApplicationDbContext context) : base(context)
        {
        }
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> TopProductsAsync()
        {
            var drinkQuantities = await _context.OrderDetails.Include(od => od.Drink)
        .GroupBy(od => od.Drink.Id)
         .Select(g => new
         {
             DrinkName = g.FirstOrDefault().Drink.Name,
             TotalQuantity = g.Sum(od => od.Quantity)
         }).OrderBy(d => d.TotalQuantity)
        .ToListAsync();
            var chartData = new List<ChartItem>();
            foreach (var item in drinkQuantities)
            {
                chartData.Add(new ChartItem
                {
                    Label = item.DrinkName,
                    Value = item.TotalQuantity
                });
            }

            return View(chartData);
        
         
        }
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DailyStatistics()
      {
            // Retrieve sales data from your data source
          /*  List<Sale> salesData = GetSalesData();*/

            // Group sales by order date and calculate statistics
           
            var dailyStats = await _context.Orders.Where(s => s.Status!=0).GroupBy(sale => sale.CreatedDate.Date)
                .Select(group => new
                {
                    OrderDate = group.Key,
                    OrderCount = group.Count(),
                    Revenue = group.Sum(sale => sale.Payment)
                })
                .OrderBy(stats => stats.OrderDate)
                .ToListAsync();
            var daily = new List<DailyStatisticsViewModel>();
            foreach (var item in dailyStats)
            {
                daily.Add(new DailyStatisticsViewModel
                {
                    OrderDate = item.OrderDate,
                    OrderCount = item.OrderCount,
                    Revenue = (double)item.Revenue              
                });
            }
            return View(daily);
        }
    

    }

}
