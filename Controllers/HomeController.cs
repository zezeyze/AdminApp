using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AdminBlog.Models;
using AdminBlog.Models;

namespace AdminBlog.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BlogContext _context;



    public HomeController(ILogger<HomeController> logger, BlogContext context)
    {
        _logger = logger;
        _context = context;
    }
    public async Task<IActionResult> AddCategory(Category category)
    {
        if (category.Id == 0)
        {
            await _context.AddAsync(category);
        }
        else
        {
            _context.Update(category);
        }
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Category));
    }

    public async Task<IActionResult> CategoryDetails(int Id)
    {
        var category = await _context.Category.FindAsync(Id);
        return Json(category);
    }
    public IActionResult Category()
    {
        List<Category> list=_context.Category.ToList();
        return View(list);
    }
    public async Task<IActionResult> DeleteCategory(int? Id)
    {
        Category category = await _context.Category.FindAsync(Id);
        _context.Remove(category);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Category));
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
