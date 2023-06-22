using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;

namespace Project.Controllers
{
    public class BaseController : Controller
    {
        private AppUser _currentUser;
        protected readonly ApplicationDbContext _context;

        public BaseController(ApplicationDbContext context)
        {
            _context = context;
        }
        protected AppUser CurrentUser
        {
            get
            {
                if (_currentUser == null && User.Identity.IsAuthenticated)
                {
                    var username = User.Identity.Name;
                    _currentUser = _context.Users.Include(user => user.Profile)
                        .FirstOrDefault(x => x.UserName == username);
                }
                return _currentUser;
            }
        }
    }
}
