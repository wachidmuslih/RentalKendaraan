﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentalKendaraan_110.Models;

namespace RentalKendaraan_110.Controllers
{
    public class KendaraansController : Controller
    {
        private readonly RentKendaraanContext _context;

        public KendaraansController(RentKendaraanContext context)
        {
            _context = context;
        }

        // GET: Kendaraans
        public async Task<IActionResult> Index(string ktsd, string seacrhString, string sortOrder, string currentFilter, int? pageNumber)
        {
            //buat list menyimpan ketersediaan
            var ktsdList = new List<string>();

            //query mengambil data
            var ktsdQuery = from d in _context.Kendaraans orderby d.Ketersediaan select d.Ketersediaan;
            ktsdList.AddRange(ktsdQuery.Distinct());

            //untuk menampilkan diview
            ViewBag.ktsd = new SelectList(ktsdList);

            //panggil db context
            var menu = from m in _context.Kendaraans.Include(k => k.IdJenisKendaraanNavigation) select m;

            //untuk memilih dropdown list ketersediaan
            if(!string.IsNullOrEmpty(ktsd))
            {
                menu = menu.Where(x => x.Ketersediaan == ktsd);
            }

            //untuk seacrh data
            if (!string.IsNullOrEmpty(seacrhString))
            {
                menu = menu.Where(s => s.NoPolisi.Contains(seacrhString) || s.NamaKendaraan.Contains(seacrhString) || s.NoStnk.Contains(seacrhString));
            }

            //var rentKendaraanContext = _context.Kendaraans.Include(k => k.IdJenisKendaraanNavigation);
            //return View(await menu.ToListAsync());

            ViewData["CurrentSort"] = sortOrder;
            if (seacrhString != null)
            {
                pageNumber = 1;
            }
            else
            {
                seacrhString = currentFilter;
            }
            ViewData["CurrentFilter"] = seacrhString;
            int pageSize = 5;

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            switch (sortOrder)
            {
                case "name_desc":
                    menu = menu.OrderByDescending(s => s.NamaKendaraan);
                    break;
                case "Date":
                    menu = menu.OrderBy(s => s.IdJenisKendaraanNavigation.IdJenisKendaraan);
                    break;
                case "date_desc":
                    menu = menu.OrderByDescending(s => s.IdJenisKendaraanNavigation.IdJenisKendaraan);
                    break;
                default:
                    menu = menu.OrderBy(s => s.NamaKendaraan);
                    break;
            }

            return View(await PaginatedList<Kendaraan>.CreateAsync(menu.AsNoTracking(), pageNumber ?? 1, pageSize));



        }

        // GET: Kendaraans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kendaraan = await _context.Kendaraans
                .Include(k => k.IdJenisKendaraanNavigation)
                .FirstOrDefaultAsync(m => m.IdKendaraan == id);
            if (kendaraan == null)
            {
                return NotFound();
            }

            return View(kendaraan);
        }

        // GET: Kendaraans/Create
        public IActionResult Create()
        {
            ViewData["IdJenisKendaraan"] = new SelectList(_context.JenisKendaraans, "IdJenisKendaraan", "IdJenisKendaraan");
            return View();
        }

        // POST: Kendaraans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdKendaraan,NamaKendaraan,NoPolisi,NoStnk,IdJenisKendaraan,Ketersediaan")] Kendaraan kendaraan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kendaraan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdJenisKendaraan"] = new SelectList(_context.JenisKendaraans, "IdJenisKendaraan", "IdJenisKendaraan", kendaraan.IdJenisKendaraan);
            return View(kendaraan);
        }

        // GET: Kendaraans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kendaraan = await _context.Kendaraans.FindAsync(id);
            if (kendaraan == null)
            {
                return NotFound();
            }
            ViewData["IdJenisKendaraan"] = new SelectList(_context.JenisKendaraans, "IdJenisKendaraan", "IdJenisKendaraan", kendaraan.IdJenisKendaraan);
            return View(kendaraan);
        }

        // POST: Kendaraans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdKendaraan,NamaKendaraan,NoPolisi,NoStnk,IdJenisKendaraan,Ketersediaan")] Kendaraan kendaraan)
        {
            if (id != kendaraan.IdKendaraan)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kendaraan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KendaraanExists(kendaraan.IdKendaraan))
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
            ViewData["IdJenisKendaraan"] = new SelectList(_context.JenisKendaraans, "IdJenisKendaraan", "IdJenisKendaraan", kendaraan.IdJenisKendaraan);
            return View(kendaraan);
        }

        // GET: Kendaraans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kendaraan = await _context.Kendaraans
                .Include(k => k.IdJenisKendaraanNavigation)
                .FirstOrDefaultAsync(m => m.IdKendaraan == id);
            if (kendaraan == null)
            {
                return NotFound();
            }

            return View(kendaraan);
        }

        // POST: Kendaraans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kendaraan = await _context.Kendaraans.FindAsync(id);
            _context.Kendaraans.Remove(kendaraan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KendaraanExists(int id)
        {
            return _context.Kendaraans.Any(e => e.IdKendaraan == id);
        }
    }
}
