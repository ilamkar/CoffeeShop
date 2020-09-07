using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.Data;
using CoffeeShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {

        private QuotesDbContext _quotesDbContext;
        public QuotesController(QuotesDbContext quotesDbContext)
        {
            _quotesDbContext = quotesDbContext;
        }
        // GET: api/Quotes
        //[HttpGet]
        //public IActionResult Get()
        //{

        //    return Ok(_quotesDbContext.Quotes);
        //}

      //  GET: api/Quotes
       [HttpGet]
        public IActionResult  Get(String sort, String sortByName)
        {
            IQueryable<Quote> quotes;
            // var quote = await _quotesDbContext.Quotes.ToListAsync();
            if (sort != null)
            {
                switch (sort)
                {
                    case "desc":
                        quotes = _quotesDbContext.Quotes.OrderByDescending(q => q.CreatedAt);
                        break;
                    case "asc":
                        quotes = _quotesDbContext.Quotes.OrderBy(q => q.CreatedAt);
                        break;
                    default:
                        quotes = _quotesDbContext.Quotes;
                        break;
                }
            }
            switch (sortByName)
            {
                case "desc":
                    quotes = _quotesDbContext.Quotes.OrderByDescending(q => q.Author);
                    break;
                case "asc":
                    quotes = _quotesDbContext.Quotes.OrderBy(q => q.Author);
                    break;
                default:
                    quotes = _quotesDbContext.Quotes;
                    break;
            }

            return Ok(quotes);
        }
        [HttpGet("[action]")]
        public IActionResult PageQuote(int? pageNumber, int? pageSize)
        {
            var quotes = _quotesDbContext.Quotes;
            var currentPageNumber = pageNumber ?? 1;
            var currentPageSize = pageSize ?? 5;

            return Ok(quotes.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
        }

        //Search Functionality
        [HttpGet("[action]")]
        public IActionResult SearchQuote(String type)
        {
           var quotes= _quotesDbContext.Quotes.Where(q => q.Type.StartsWith(type));
            return Ok(quotes);
        }

        // GET: api/Quotes/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<Quote>> GetName(int id)
        {
            var quote = await _quotesDbContext.Quotes.FindAsync(id);
            if (quote !=null)
            {
                return quote;
            }
            else
            {
                return NotFound();
            }
        }
          //attribute base routing  api/quotes/getTitle/1
        [HttpGet("[action]/{title}")]

        public string GetTitle(string title)
        {
           
            return title;
        }
        // POST: api/Quotes
        [HttpPost]
        public IActionResult Post([FromBody] Quote value)
        {
            try
            {
                var current_timestamp = DateTime.Now;
                value.CreatedAt = current_timestamp;
                _quotesDbContext.Quotes.Add(value);
                _quotesDbContext.SaveChanges();
                return Ok(value);
            }
            catch (Exception e)
            {

                return Ok(e.Message);
            }
        }

        // PUT: api/Quotes/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Quote quote)
        {
            var entity = _quotesDbContext.Quotes.Find(id);
            if(entity == null){
                return NotFound("No record found");
            }
            else
            {
                var current_timestamp = DateTime.Now;
                entity.Title = quote.Title;
                entity.CreatedAt = quote.CreatedAt;
                entity.Description = quote.Description;
                entity.Type = quote.Type;
                entity.CreatedAt = quote.CreatedAt;
                _quotesDbContext.SaveChanges();
                return Ok(quote);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
