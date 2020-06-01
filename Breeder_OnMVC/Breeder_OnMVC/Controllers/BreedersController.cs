using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Breeder_OnMVC.Data;
using Breeder_OnMVC.Models;
using Microsoft.AspNetCore.Authorization;

namespace Breeder_OnMVC.Controllers
{
    [Authorize]
    public class BreedersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BreedersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Детали
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

        // GET: создаем
        public IActionResult Create()
        {
            return View();
        }

        // POST: создаем
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

        // GET: обновляем
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

        // POST: обновляем
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

        // GET: удаление
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

        // POST: удаление
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var breeder = await _context.Breeder.FindAsync(id);
            _context.Breeder.Remove(breeder); 
            await _context.SaveChangesAsync(); 
            return RedirectToAction(nameof(Index)); 
        }

        //cуществует
        private bool BreederExists(int id)
        {
            return _context.Breeder.Any(e => e.Id == id);
        }

        //поиск|фильтр
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            //фильтр
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.AuthorSortParm = sortOrder == "Author" ? "author_desc" : "Author";
            ViewBag.ParentVarietiesSortParm = sortOrder == "ParentVarieties" ? "parentVarieties_desc" : "ParentVarieties";
            ViewBag.ProductivitySortParm = sortOrder == "Productivity" ? "productivity_desc" : "Productivity";
            ViewBag.CharacteristicSortParm = sortOrder == "Characteristic" ? "characteristic_desc" : "Characteristic";
            ViewBag.FrostResistanceSortParm = sortOrder == "FrostResistance" ? "frostResistance_desc" : "FrostResistance";
            ViewBag.DiseaseResistanceSortParm = sortOrder == "DiseaseResistance" ? "diseaseResistance_desc" : "DiseaseResistance";
            ViewBag.FundsSortParm = sortOrder == "Funds" ? "funds_desc" : "Funds";
            var breeders = from m in _context.Breeder
                           select m;

            switch (sortOrder)
            {
                case "name_desc":
                    breeders = breeders.OrderByDescending(s => s.Name);
                    break;
                case "Author":
                    breeders = breeders.OrderBy(s => s.Author);
                    break;
                case "author_desc":
                    breeders = breeders.OrderByDescending(s => s.Author);
                    break;
                case "ParentVarieties":
                    breeders = breeders.OrderBy(s => s.ParentVarieties);
                    break;
                case "parentVarieties_desc":
                    breeders = breeders.OrderByDescending(s => s.ParentVarieties);
                    break;
                case "Productivity":
                    breeders = breeders.OrderBy(s => s.Productivity);
                    break;
                case "productivity_desc":
                    breeders = breeders.OrderByDescending(s => s.Productivity);
                    break;
                case "Characteristic":
                    breeders = breeders.OrderBy(s => s.Characteristic);
                    break;
                case "characteristic_desc":
                    breeders = breeders.OrderByDescending(s => s.Characteristic);
                    break;
                case "FrostResistance":
                    breeders = breeders.OrderBy(s => s.FrostResistance);
                    break;
                case "frostResistance_desc":
                    breeders = breeders.OrderByDescending(s => s.FrostResistance);
                    break;
                case "DiseaseResistance":
                    breeders = breeders.OrderBy(s => s.DiseaseResistance);
                    break;
                case "diseaseResistance_desc":
                    breeders = breeders.OrderByDescending(s => s.DiseaseResistance);
                    break;
                case "Funds":
                    breeders = breeders.OrderBy(s => s.Funds);
                    break;
                case "funds_desc":
                    breeders = breeders.OrderByDescending(s => s.Funds);
                    break;
                default:
                    breeders = breeders.OrderBy(s => s.Name);
                    break;
            }

            //поиск
            if (!String.IsNullOrEmpty(searchString))
            {
                breeders = breeders.Where(s => s.Name.Contains(searchString) || s.Author.Contains(searchString) || s.ParentVarieties.Contains(searchString) || s.Productivity.Contains(searchString)
                                || s.Characteristic.Contains(searchString) || s.FrostResistance.Contains(searchString) || s.DiseaseResistance.Contains(searchString) || s.Funds.Contains(searchString));
            }
            return View(await breeders.ToListAsync());
        }
    }
}
