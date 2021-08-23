using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prepaid_Mobile_Topup_MVC.Data;
using Prepaid_Mobile_Topup_MVC.Models;

namespace Prepaid_Mobile_Topup_MVC.Controllers
{
    public class PrepaidCustomersController : Controller
    {
        private readonly Prepaid_Mobile_Topup_DBContext _context;

        public PrepaidCustomersController(Prepaid_Mobile_Topup_DBContext context)
        {
            _context = context;
        }
        [Authorize]
        // GET: PrepaidCustomers
        public async Task<IActionResult> Index()
        {
            return View(await _context.PrepaidCustomer.ToListAsync());
        }
        [Authorize]
        // GET: PrepaidCustomers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prepaidCustomer = await _context.PrepaidCustomer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prepaidCustomer == null)
            {
                return NotFound();
            }

            return View(prepaidCustomer);
        }
        [Authorize]
        // GET: PrepaidCustomers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PrepaidCustomers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,RegistereddDate")] PrepaidCustomer prepaidCustomer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prepaidCustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prepaidCustomer);
        }
        [Authorize]
        // GET: PrepaidCustomers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prepaidCustomer = await _context.PrepaidCustomer.FindAsync(id);
            if (prepaidCustomer == null)
            {
                return NotFound();
            }
            return View(prepaidCustomer);
        }

        // POST: PrepaidCustomers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,RegistereddDate")] PrepaidCustomer prepaidCustomer)
        {
            if (id != prepaidCustomer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prepaidCustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrepaidCustomerExists(prepaidCustomer.Id))
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
            return View(prepaidCustomer);
        }
        [Authorize]
        // GET: PrepaidCustomers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prepaidCustomer = await _context.PrepaidCustomer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prepaidCustomer == null)
            {
                return NotFound();
            }

            return View(prepaidCustomer);
        }

        // POST: PrepaidCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prepaidCustomer = await _context.PrepaidCustomer.FindAsync(id);
            _context.PrepaidCustomer.Remove(prepaidCustomer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrepaidCustomerExists(int id)
        {
            return _context.PrepaidCustomer.Any(e => e.Id == id);
        }
    }
}
