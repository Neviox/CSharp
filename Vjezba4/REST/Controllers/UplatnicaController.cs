using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.IO;
using Transakcije.Data;
using Transakcije.Models;

namespace REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UplatnicaController : ControllerBase
    {
        private readonly DataContext _context;

        public UplatnicaController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Uplatnica
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Uplatnica>>> GetUplatnicas()
        {
            if (_context.Uplatnicas == null)
            {
                return NotFound();
            }
            return await _context.Uplatnicas.ToListAsync();
        }

        // GET: api/Uplatnica/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Uplatnica>> GetUplatnica(long id)
        {
            if (_context.Uplatnicas == null)
            {
                return NotFound();
            }
            var uplatnica = await _context.Uplatnicas.FindAsync(id);

            if (uplatnica == null)
            {
                return NotFound();
            }

            return uplatnica;
        }

        // PUT: api/Uplatnica/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUplatnica(long id, Uplatnica uplatnica)
        {
            if (id != uplatnica.IdUplatnica)
            {
                return BadRequest();
            }

            _context.Entry(uplatnica).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UplatnicaExists(id))
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

        // POST: api/Uplatnica
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Uplatnica>> PostUplatnica(Uplatnica uplatnica)
        {
            if (_context.Uplatnicas == null)
            {
                return Problem("Entity set 'DataContext.Uplatnicas'  is null.");
            }
            _context.Uplatnicas.Add(uplatnica);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUplatnica", new { id = uplatnica.IdUplatnica }, uplatnica);
        }

        // DELETE: api/Uplatnica/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUplatnica(long id)
        {
            if (_context.Uplatnicas == null)
            {
                return NotFound();
            }
            var uplatnica = await _context.Uplatnicas.FindAsync(id);
            if (uplatnica == null)
            {
                return NotFound();
            }

            _context.Uplatnicas.Remove(uplatnica);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UplatnicaExists(long id)
        {
            return (_context.Uplatnicas?.Any(e => e.IdUplatnica == id)).GetValueOrDefault();
        }

        [HttpGet("valute")]
        public async Task<IEnumerable<Uplatnica>> GetValuta(string valuta)
        {
            return await _context.Uplatnicas
                .Where(p => p.Valuta == valuta)
                .ToListAsync();
        }

        [HttpGet("tecaj")]
        public string getRequest()
        {
            string url = "https://api.hnb.hr/tecajn/v1";

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "GET";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader sr = new StreamReader(response.GetResponseStream());
            return sr.ReadToEnd();
        }
    }
}
