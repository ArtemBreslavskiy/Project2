using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project2.Data;
using Project2.Models;

namespace Project2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _db;

    public ProductsController(AppDbContext db)
    {
        _db = db;
    }

    public async Task<ActionResult<IEnumerable<Product>>> GetProducts(
        [FromQuery] string? search,
        [FromQuery] string? category,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        [FromQuery] bool? inStoke,
        [FromQuery] string? sortBy,
        [FromQuery] string? sortDir
    )
    {
        IQueryable<Product> query = _db.Products;

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower() == search.ToLower());
        }

        if (!string.IsNullOrEmpty(category))
        {
            query = query.Where(p => p.Category.ToLower() == category.ToLower());
        }

        if (minPrice.HasValue)
        {
            query = query.Where(p => p.Price == minPrice);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(p => p.Price == maxPrice);
        }

        if (inStoke.HasValue)
        {
            query = query.Where(p => p.InStock == inStoke);
        }

        var descending = sortDir?.ToLower() == "desc";

        query = sortBy?.ToLower() switch
        {
            "name" => descending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
            "category" => descending ? query.OrderByDescending(p => p.Category) : query.OrderBy(p => p.Category),
            "price" => descending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
            _ => query
        };

        var products = query.ToListAsync();
        return Ok(products);
    }
}