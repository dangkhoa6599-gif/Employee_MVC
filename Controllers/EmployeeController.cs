using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using app01.Models;

namespace app01.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
              return _context.EmployeeModels != null ? 
                          View(await _context.EmployeeModels.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.EmployeeModels'  is null.");
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EmployeeModels == null)
            {
                return NotFound();
            }

            var employeeModel = await _context.EmployeeModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeModel == null)
            {
                return NotFound();
            }

            return View(employeeModel);
        }

        // GET: Employee/Create
        [HttpGet]
        public IActionResult Create()
        {
            EmployeeModel emp = new EmployeeModel();
            return PartialView("_EmployeePartial", emp);
            
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeModel emp)
        {
            _context.EmployeeModels.Add(emp);
            _context.SaveChanges();
            
            return RedirectToAction("Index");
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EmployeeModels == null)
            {
                return NotFound();
            }

            var employeeModel = await _context.EmployeeModels.FindAsync(id);
            if (employeeModel == null)
            {
                return NotFound();
            }
            return View(employeeModel);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Address,Phone")] EmployeeModel employeeModel)
        {
            if (id != employeeModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeModelExists(employeeModel.Id))
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
            return View(employeeModel);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EmployeeModels == null)
            {
                return NotFound();
            }

            var employeeModel = await _context.EmployeeModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeModel == null)
            {
                return NotFound();
            }

            return View(employeeModel);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EmployeeModels == null)
            {
                return Problem("Entity set 'AppDbContext.EmployeeModels'  is null.");
            }
            var employeeModel = await _context.EmployeeModels.FindAsync(id);
            if (employeeModel != null)
            {
                _context.EmployeeModels.Remove(employeeModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeModelExists(int id)
        {
          return (_context.EmployeeModels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
