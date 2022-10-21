using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JacobTestWebApp.Data;
using JacobTestWebApp.Models;

namespace JacobTestWebApp.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly JacobTestWebApp.Data.ApplicationDbContext _context;

        public DetailsModel(JacobTestWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
               .Include(s => s.Enrollments)
               .ThenInclude(e => e.Course)
               .AsNoTracking()
               .FirstOrDefaultAsync(m => m.ID == id);

            if (student == null)
            {
                return NotFound();
            }
            else 
            {
                Student = student;
            }
            return Page();
        }
    }
}
