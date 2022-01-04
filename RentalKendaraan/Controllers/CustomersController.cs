using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentalKendaraan_110.Models;

namespace RentalKendaraan_110.Controllers
{
    public class CustomersController : Controller
    {
        private readonly RentKendaraanContext _context;

        public CustomersController(RentKendaraanContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index(string nama, string searchString, string sortOrder, string currentFilter, int? pageNumber)
        {
            //buat list menyimpan ketersediaan
            var namaList = new List<string>();

            //query mengambil data
            var namaQuery = from d in _context.Customers orderby d.IdGender select d.IdGender.ToString();
            namaList.AddRange(namaQuery.Distinct());

            //untuk menampilkan diview
            ViewBag.nama = new SelectList(namaList);

            //panggil db context
            var menu = from m in _context.Customers.Include(k => k.IdGenderNavigation) select m;

            //untuk memilih dropdown list ketersediaan
            if (!string.IsNullOrEmpty(nama))
            {
                menu = menu.Where(x => x.IdGender.ToString() == nama);
            }

            //untuk seacrh data
            if (!string.IsNullOrEmpty(searchString))
            {
                menu = menu.Where(s => s.NamaCostumer.Contains(searchString) || s.Nik.Contains(searchString) || s.NoHp.Contains(searchString));
            }
            //var rentKendaraanContext = _context.Customers.Include(c => c.IdGenderNavigation);
            //return View(await menu.ToListAsync());

            ViewData["CurrentSort"] = sortOrder;
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            switch (sortOrder)
            {
                case "name_desc":
                    menu = menu.OrderByDescending(s => s.NamaCostumer);
                    break;
                case "Date":
                    menu = menu.OrderBy(s => s.IdGenderNavigation.NamaGender);
                    break;
                case "date_desc":
                    menu = menu.OrderByDescending(s => s.IdGenderNavigation.NamaGender);
                    break;
                default: //name ascending
                    menu = menu.OrderBy(s => s.NamaCostumer);
                    break;
            }
            ViewData["CurrentFilter"] = searchString;
            int pageSize = 5;
            return View(await PaginatedList<Customer>.CreateAsync(menu.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.IdGenderNavigation)
                .FirstOrDefaultAsync(m => m.IdCostumer == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            ViewData["IdGender"] = new SelectList(_context.Genders, "IdGender", "IdGender");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCostumer,NamaCostumer,Nik,Alamat,NoHp,IdGender")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdGender"] = new SelectList(_context.Genders, "IdGender", "IdGender", customer.IdGender);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["IdGender"] = new SelectList(_context.Genders, "IdGender", "IdGender", customer.IdGender);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCostumer,NamaCostumer,Nik,Alamat,NoHp,IdGender")] Customer customer)
        {
            if (id != customer.IdCostumer)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.IdCostumer))
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
            ViewData["IdGender"] = new SelectList(_context.Genders, "IdGender", "IdGender", customer.IdGender);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.IdGenderNavigation)
                .FirstOrDefaultAsync(m => m.IdCostumer == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.IdCostumer == id);
        }
    }
}
