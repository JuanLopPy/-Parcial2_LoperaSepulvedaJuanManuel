using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parcial2.DAL;
using Parcial2.DAL.Entities;

namespace Parcial2.Controllers
{
    public class TicketsController : Controller
    {
      

        private readonly DataBaseContext _context;

        public TicketsController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet]
       public async Task<JsonResult> Index()
{
    var tickets = await _context.Tickets.ToListAsync();
    return Json(tickets);
}
        [HttpGet]
        public IActionResult CreateRecords()
        {
            for (int i = 0; i < 5; i++)
            {
                var ticket = new Ticket
                {
                    Id= i,
                    UseDate = DateTime.Now,
                    IsUsed = false,
                    EntranceGate = null,
                };
             
                _context.Tickets.Add(ticket);
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var tickets = _context.Tickets.ToList(); // Get all tickets from the database

            return View("TicketList", tickets); // Pass ticket list to view
        }

        [HttpPost]
        public async Task<IActionResult> ValidateTicket(int id, string entranceGate)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
            {
                ViewBag.ErrorMessage = "Boleta no válida";
                return View("Index", await _context.Tickets.ToListAsync());
            }

            if (ticket.IsUsed)
            {
                ViewBag.ErrorMessage = $"La boleta ya fue usada el {ticket.UseDate} en la entrada {ticket.EntranceGate}";
                return View("Index", await _context.Tickets.ToListAsync());
            }

            ticket.IsUsed = true;
            ticket.UseDate = DateTime.Now;
            ticket.EntranceGate = entranceGate;
            _context.Update(ticket);
            await _context.SaveChangesAsync();

            return RedirectToAction("Success");
        }



    }
}

