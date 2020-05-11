using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Breeder_OnMVC.Data;
using Breeder_OnMVC.Models;

namespace Breeder_OnMVC.Controllers
{
    public class BreedersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BreedersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Breeders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Breeder.ToListAsync());
        }

        // GET: Breeders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var breeder = await _context.Breeder
                .FirstOrDefaultAsync(m => m.Id == id);
            if (breeder == null)
            {
                return NotFound();
            }

            return View(breeder);
        }

        // GET: Breeders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Breeders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Author,ParentVarieties,Productivity,Characteristic,FrostResistance,DiseaseResistance,Funds")] Breeder breeder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(breeder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(breeder);
        }

        // GET: Breeders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var breeder = await _context.Breeder.FindAsync(id);
            if (breeder == null)
            {
                return NotFound();
            }
            return View(breeder);
        }

        // POST: Breeders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Author,ParentVarieties,Productivity,Characteristic,FrostResistance,DiseaseResistance,Funds")] Breeder breeder)
        {
            if (id != breeder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(breeder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BreederExists(breeder.Id))
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
            return View(breeder);
        }

        // GET: Breeders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var breeder = await _context.Breeder
                .FirstOrDefaultAsync(m => m.Id == id);
            if (breeder == null)
            {
                return NotFound();
            }

            return View(breeder);
        }

        // POST: Breeders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var breeder = await _context.Breeder.FindAsync(id);
            _context.Breeder.Remove(breeder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BreederExists(int id)
        {
            return _context.Breeder.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var breeders = from m in _context.Breeder
                           select m;


            if (!String.IsNullOrEmpty(searchString))
            {
                breeders = breeders.Where(s => s.Name.Contains(searchString) || s.Author.Contains(searchString) || s.ParentVarieties.Contains(searchString) || s.Productivity.Contains(searchString)
                                || s.Characteristic.Contains(searchString) || s.FrostResistance.Contains(searchString) || s.DiseaseResistance.Contains(searchString) || s.Funds.Contains(searchString));
            }

            return View(await breeders.ToListAsync());
        }
    }
}
