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
    public class TopUpChannelsController : Controller
    {
        private readonly Prepaid_Mobile_Topup_DBContext _context;

        public TopUpChannelsController(Prepaid_Mobile_Topup_DBContext context)
        {
            _context = context;
        }
        [Authorize]
        // GET: TopUpChannels
        public async Task<IActionResult> Index()
        {
            return View(await _context.TopUpChannel.ToListAsync());
        }
        [Authorize]
        // GET: TopUpChannels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topUpChannel = await _context.TopUpChannel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topUpChannel == null)
            {
                return NotFound();
            }

            return View(topUpChannel);
        }
        [Authorize]
        // GET: TopUpChannels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TopUpChannels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] TopUpChannel topUpChannel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topUpChannel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(topUpChannel);
        }
        [Authorize]
        // GET: TopUpChannels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topUpChannel = await _context.TopUpChannel.FindAsync(id);
            if (topUpChannel == null)
            {
                return NotFound();
            }
            return View(topUpChannel);
        }

        // POST: TopUpChannels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] TopUpChannel topUpChannel)
        {
            if (id != topUpChannel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topUpChannel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopUpChannelExists(topUpChannel.Id))
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
            return View(topUpChannel);
        }
        [Authorize]
        // GET: TopUpChannels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topUpChannel = await _context.TopUpChannel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topUpChannel == null)
            {
                return NotFound();
            }

            return View(topUpChannel);
        }

        // POST: TopUpChannels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var topUpChannel = await _context.TopUpChannel.FindAsync(id);
            _context.TopUpChannel.Remove(topUpChannel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopUpChannelExists(int id)
        {
            return _context.TopUpChannel.Any(e => e.Id == id);
        }
    }
}
