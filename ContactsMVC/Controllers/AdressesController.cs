using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace WebApplication2.Controllers
{
    public class AdressesController : Controller
    {


        private readonly MyAppContext db;

        public AdressesController(MyAppContext context)
        {
            db = context;
        }

        // GET: Adresses
        public async Task<IActionResult> Index()
        {
            var webApplication2Context = db.Adresses.Include(a => a.Client);
            return View(await webApplication2Context.ToListAsync());
        }

        // GET: Adresses/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || db.Adresses == null)
            {
                return NotFound();
            }

            var adress = await db.Adresses
                .Include(a => a.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adress == null)
            {
                return NotFound();
            }

            return View(adress);
        }

        // GET: Adresses/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(db.Clients, "Id", "Id");
            return View();
        }

        // POST: Adresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreateDate,Country,AdressText,Description,ClientId")] Adress adress)
        {
            if (ModelState.IsValid)
            {
                adress.Id = Guid.NewGuid();
                db.Add(adress);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(db.Adresses, "Id", "Id", adress.ClientId);
            return View(adress);
        }

        // GET: Adresses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || db.Adresses == null)
            {
                return NotFound();
            }

            var adress = await db.Adresses.FindAsync(id);
            if (adress == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(db.Clients, "Id", "Id", adress.ClientId);
            return View(adress);
        }

        // POST: Adresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,CreateDate,Country,AdressText,Description,ClientId")] Adress adress)
        {
            if (id != adress.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(adress);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdressExists(adress.Id))
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
            ViewData["ClientId"] = new SelectList(db.Clients, "Id", "Id", adress.ClientId);
            return View(adress);
        }

        // GET: Adresses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || db.Adresses == null)
            {
                return NotFound();
            }

            var adress = await db.Adresses
                .Include(a => a.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adress == null)
            {
                return NotFound();
            }

            return View(adress);
        }

        // POST: Adresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (db.Adresses == null)
            {
                return Problem("Entity set 'WebApplication2Context.Adress'  is null.");
            }
            var adress = await db.Adresses.FindAsync(id);
            if (adress != null)
            {
                db.Adresses.Remove(adress);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdressExists(Guid id)
        {
            return db.Adresses.Any(e => e.Id == id);
        }
    }
}
