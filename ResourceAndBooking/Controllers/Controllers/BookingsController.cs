using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourceAndBooking.Data;
using ResourceAndBooking.Models;

namespace ResourceAndBooking.Controllers
{
    public class BookingsController(ApplicationDbContext context) : Controller
    {

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var bookings = context.Bookings
                .Include(b => b.Employee)
                .Include(b => b.Resource);

            return View(await bookings.ToListAsync());
        }

        private bool BookingExists(int resourceId, DateTime startDate, DateTime endDate)
        => context.Bookings.Any(b =>
                b.ResourceId == resourceId &&
                b.StartTime < endDate &&
                b.EndTime > startDate);



        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var booking = await context.Bookings
                .Include(b => b.Employee)
                .Include(b => b.Resource)
                .FirstOrDefaultAsync(m => m.BookingId == id);

            if (booking == null) return NotFound();

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(context.Employees, "EmployeeId", "Name");
            ViewBag.ResourceID = new SelectList(context.Resources.Where(r => r.IsAvailable), "Id", "Name");
            return View();
        }

        // POST: Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingId,EmployeeId,ResourceId,StartTime,EndTime,Purpose")] Bookings booking)
        {
            ModelState.Remove("BookedBy");
            ModelState.Remove("Employee");
            ModelState.Remove("Resource");

            if (ModelState.IsValid)
            {
                if (BookingExists(booking.ResourceId, booking.StartTime, booking.EndTime))
                    return DisplayError(booking);

                context.Add(booking);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["EmployeeId"] = new SelectList(context.Employees, "EmployeeId", "EmployeeName", booking.EmployeeId);
            ViewData["ResourceId"] = new SelectList(context.Resources, "Id", "Name", booking.ResourceId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var booking = await context.Bookings.FindAsync(id);
            if (booking == null) return NotFound();

            ViewData["EmployeeId"] = new SelectList(context.Employees, "EmployeeId", "EmployeeName", booking.EmployeeId);
            ViewData["ResourceId"] = new SelectList(context.Resources, "Id", "Name", booking.ResourceId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind("BookingId,EmployeeId,ResourceId,StartTime,EndTime,Purpose")] Bookings booking)
        {

            if (id != booking.BookingId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    if (BookingExists(booking.ResourceId, booking.StartTime, booking.EndTime))
                        return DisplayError(booking);

                    context.Update(booking);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["EmployeeId"] = new SelectList(context.Employees, "EmployeeId", "EmployeeName", booking.EmployeeId);
            ViewData["ResourceId"] = new SelectList(context.Resources, "Id", "Name", booking.ResourceId);
            return View(booking);
        }

        private ActionResult DisplayError(Bookings booking)
        {
            ModelState.AddModelError(string.Empty, "This resource is already booked during the requested time. Please choose another slot or resource, or adjust your times.");
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var booking = await context.Bookings
                .Include(b => b.Employee)
                .Include(b => b.Resource)
                .FirstOrDefaultAsync(m => m.BookingId == id);

            if (booking == null) return NotFound();

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await context.Bookings.FindAsync(id);
            if (booking != null)
            {
                context.Bookings.Remove(booking);
                await context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}