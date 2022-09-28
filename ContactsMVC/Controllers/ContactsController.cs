using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ContactsMVC.Controllers
{
    public class ContactsController : Controller
    {
        private readonly MyAppContext db;

        public ContactsController(MyAppContext context)
        {
            db = context;
        }

        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            var webApplication2Context = db.Contacts.Include(c => c.Client);
            return View(await webApplication2Context.ToListAsync());
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || db.Contacts == null)
            {
                return NotFound();
            }

            var contact = await db.Contacts
                .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Contacts/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(db.Clients, "Id", "Id");
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Phone,CreatedDate,LastModifiedDate,BirthDayDate,ClientId")] Contact contact)
        {
            if (contact.ClientId != Guid.Empty)
            {
                contact.Client = db.Clients.FirstOrDefault(x => x.Id == contact.ClientId);

                if (ModelState.IsValid)
                {
                    contact.Id = Guid.NewGuid();
                    db.Add(contact);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewData["ClientId"] = new SelectList(db.Clients, "Id", "Id", contact.ClientId);
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || db.Contacts == null)
            {
                return NotFound();
            }

            var contact = await db.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(db.Clients, "Id", "Id", contact.ClientId);
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,Phone,CreatedDate,LastModifiedDate,BirthDayDate,ClientId")] Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(contact);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.Id))
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
            ViewData["ClientId"] = new SelectList(db.Clients, "Id", "Id", contact.ClientId);
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || db.Contacts == null)
            {
                return NotFound();
            }

            var contact = await db.Contacts
                .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (db.Contacts == null)
            {
                return Problem("Entity set 'WebApplication2Context.Contact'  is null.");
            }
            var contact = await db.Contacts.FindAsync(id);
            if (contact != null)
            {
                db.Contacts.Remove(contact);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(Guid id)
        {
            return db.Contacts.Any(e => e.Id == id);
        }
    }
}
