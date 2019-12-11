using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimalCrossing.Models;
using AnimalCrossingApi.Models;
using AnimalCrossing.Models.ViewModels;
using AutoMapper;

namespace AnimalCrossingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatDateApiController : ControllerBase
    {
        private readonly AnimalDateApiContext _context;
        private readonly IMapper _mapper;

        public CatDateApiController(AnimalDateApiContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: api/CatDateApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatDateVM>>> GetCatDates()
        {
            var catDates = await _context.CatDates.ToListAsync();
            return _mapper.Map<List<AnimalCrossing.Models.CatDate>, List<CatDateVM>>(catDates);
        }

        // GET: api/CatDateApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalCrossing.Models.CatDate>> GetCatDate(int id)
        {
            var catDate = await _context.CatDates.FindAsync(id);

            if (catDate == null)
            {
                return NotFound();
            }

            return catDate;
        }

        // PUT: api/CatDateApi/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatDate(int id, AnimalCrossing.Models.CatDate catDate)
        {
            if (id != catDate.CatDateId)
            {
                return BadRequest();
            }

            _context.Entry(catDate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatDateExists(id))
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

        // POST: api/CatDateApi
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<AnimalCrossing.Models.CatDate>> PostCatDate(AnimalCrossing.Models.CatDate catDate)
        {
            _context.CatDates.Add(catDate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCatDate", new { id = catDate.CatDateId }, catDate);
        }

        // DELETE: api/CatDateApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AnimalCrossing.Models.CatDate>> DeleteCatDate(int id)
        {
            var catDate = await _context.CatDates.FindAsync(id);
            if (catDate == null)
            {
                return NotFound();
            }

            _context.CatDates.Remove(catDate);
            await _context.SaveChangesAsync();

            return catDate;
        }

        private bool CatDateExists(int id)
        {
            return _context.CatDates.Any(e => e.CatDateId == id);
        }
    }
}
