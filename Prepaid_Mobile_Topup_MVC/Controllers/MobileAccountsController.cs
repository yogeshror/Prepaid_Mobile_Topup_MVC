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
    public class MobileAccountsController : Controller
    {
        private readonly Prepaid_Mobile_Topup_DBContext _context;

        public MobileAccountsController(Prepaid_Mobile_Topup_DBContext context)
        {
            _context = context;
        }
        [Authorize]
        // GET: MobileAccounts
        public async Task<IActionResult> Index()
        {
            var prepaid_Mobile_Topup_DBContext = _context.MobileAccount.Include(m => m.PrepaidCustomer);
            return View(await prepaid_Mobile_Topup_DBContext.ToListAsync());
        }
        [Authorize]
        // GET: MobileAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mobileAccount = await _context.MobileAccount
                .Include(m => m.PrepaidCustomer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mobileAccount == null)
            {
                return NotFound();
            }

            return View(mobileAccount);
        }
        [Authorize]
        // GET: MobileAccounts/Create
        public IActionResult Create()
        {
            ViewData["PrepaidCustomerId"] = new SelectList(_context.Set<PrepaidCustomer>(), "Id", "Id");
            return View();
        }

        // POST: MobileAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,Balance,PrepaidCustomerId")] MobileAccount mobileAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mobileAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PrepaidCustomerId"] = new SelectList(_context.Set<PrepaidCustomer>(), "Id", "Id", mobileAccount.PrepaidCustomerId);
            return View(mobileAccount);
        }
        [Authorize]
        // GET: MobileAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mobileAccount = await _context.MobileAccount.FindAsync(id);
            if (mobileAccount == null)
            {
                return NotFound();
            }
            ViewData["PrepaidCustomerId"] = new SelectList(_context.Set<PrepaidCustomer>(), "Id", "Id", mobileAccount.PrepaidCustomerId);
            return View(mobileAccount);
        }

        // POST: MobileAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,Balance,PrepaidCustomerId")] MobileAccount mobileAccount)
        {
            if (id != mobileAccount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mobileAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MobileAccountExists(mobileAccount.Id))
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
            ViewData["PrepaidCustomerId"] = new SelectList(_context.Set<PrepaidCustomer>(), "Id", "Id", mobileAccount.PrepaidCustomerId);
            return View(mobileAccount);
        }
        [Authorize]
        // GET: MobileAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mobileAccount = await _context.MobileAccount
                .Include(m => m.PrepaidCustomer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mobileAccount == null)
            {
                return NotFound();
            }

            return View(mobileAccount);
        }

        // POST: MobileAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mobileAccount = await _context.MobileAccount.FindAsync(id);
            _context.MobileAccount.Remove(mobileAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MobileAccountExists(int id)
        {
            return _context.MobileAccount.Any(e => e.Id == id);
        }
    }
}
