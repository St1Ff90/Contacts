using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsMVC.Controllers
{
    public class ClientsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyAppContext db;

        public ClientsController(MyAppContext appContext, ILogger<HomeController> logger)
        {
            this.db = appContext;
            _logger = logger;

        }

        public async Task<IActionResult> Index()
        {
            return db.Clients != null ?
                               View(await db.Clients.ToListAsync()) :
                               Problem("Entity set 'Clients'  is null.");
        }

        // GET: Goods/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || db.Clients == null)
            {
                return NotFound();
            }

            var client = await db.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Goods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Goods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CreateDate,Description,FullName,ShortName,UpdateDate,RegistrationNumber,TaxNumber")] Client client)
        {
            if (ModelState.IsValid)
            {
                client.Id = Guid.NewGuid();
                db.Clients.Add(client);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Goods/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || db.Clients == null)
            {
                return NotFound();
            }

            var client = await db.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Goods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,CreateDate,Description,FullName,ShortName,UpdateDate,RegistrationNumber,TaxNumber")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Clients.Update(client);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
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
            return View(client);
        }

        // GET: Goods/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || db.Clients == null)
            {
                return NotFound();
            }

            var client = await db.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Goods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (db.Clients == null)
            {
                return Problem("Entity set 'GWContext.Goods'  is null.");
            }
            var good = await db.Clients.FindAsync(id);
            if (good != null)
            {
                db.Clients.Remove(good);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(Guid id)
        {
            return (db.Clients?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
