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
            for (int i = 7; i < 50; i++)
            {
                var a = new Ticket
                {
                    NumTickets= i,
                    UseDate = DateTime.Now,
                    IsUsed = false,
                    EntranceGate = "sur",
                };
             
                _context.Tickets.Add(a);
               
            }
          
            _context.SaveChanges();

            var Tickets = _context.Tickets.ToList(); // Get all tickets from the database

            return View("TicketList", Tickets); // Pass ticket list to view
        }
        public IActionResult ValidateTicketForm()
        {
            var ticket = new Ticket();
            return View(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> ValidateTicket(int id, string entranceGate)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
            {
                ViewBag.ErrorMessage = "Boleta no válida";
                return View("Warning", await _context.Tickets.ToListAsync());
            }

            if (ticket.IsUsed)
            {
                ViewBag.ErrorMessage = $"La boleta ya fue usada el {ticket.UseDate} en la entrada {ticket.EntranceGate}";
                return View("Warning", await _context.Tickets.ToListAsync());
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

