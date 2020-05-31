using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallAtApplicationItemsController : ControllerBase
    {
        private readonly CallAtApplicationContext _context;

        public CallAtApplicationItemsController(CallAtApplicationContext context)
        {
            _context = context;
        }

        // GET: api/CallAtApplicationItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CallAtApplicationItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/CallAtApplicationItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CallAtApplicationItem>> GetCallAtApplicationItem(long id)
        {
            var callAtApplicationItem = await _context.TodoItems.FindAsync(id);

            if (callAtApplicationItem == null)
            {
                return NotFound();
            }

            return callAtApplicationItem;
        }

        // PUT: api/CallAtApplicationItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCallAtApplicationItem(long id, CallAtApplicationItem callAtApplicationItem)
        {
            if (id != callAtApplicationItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(callAtApplicationItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CallAtApplicationItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CallAtApplicationItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CallAtApplicationItem>> PostCallAtApplicationItem(CallAtApplicationItem callAtApplicationItem)
        {
            _context.TodoItems.Add(callAtApplicationItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCallAtApplicationItem), new { id = callAtApplicationItem.Id }, callAtApplicationItem);
        }

        // DELETE: api/CallAtApplicationItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CallAtApplicationItem>> DeleteCallAtApplicationItem(long id)
        {
            var callAtApplicationItem = await _context.TodoItems.FindAsync(id);
            if (callAtApplicationItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(callAtApplicationItem);
            await _context.SaveChangesAsync();

            return callAtApplicationItem;
        }

        private bool CallAtApplicationItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
