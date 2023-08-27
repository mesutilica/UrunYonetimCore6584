using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrunYonetimCore6584.Core.Entities;
using UrunYonetimCore6584.Data;
using UrunYonetimCore6584.Service.Abstract;

namespace UrunYonetimCore6584.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IService<Contact> _context;

        public ContactsController(IService<Contact> context)
        {
            _context = context;
        }

        // GET: api/Contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            return await _context.GetAllAsync();
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            var contact = await _context.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        // PUT: api/Contacts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(int id, Contact contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }

            _context.Update(contact);

            try
            {
                await _context.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Problem();
            }

            return NoContent();
        }

        // POST: api/Contacts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(Contact contact)
        {
            _context.Add(contact);
            await _context.SaveAsync();

            return CreatedAtAction("GetContact", new { id = contact.Id }, contact);
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var contact = await _context.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Delete(contact);
            await _context.SaveAsync();

            return NoContent();
        }

    }
}
