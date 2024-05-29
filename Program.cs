// Разработка web-приложения на C# (семинары)
// Урок 1. ASP.NET база
// Доработайте контроллер, дополнив его возможностью удалять группы и продукты, а также задавать цены. Для каждого типа ответа создайте свою модель.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ProductController(MyDbContext context)
        {
            _context = context;
        }

        [HttpDelete("DeleteGroup/{groupId}")]
        public async Task<IActionResult> DeleteGroup(int groupId)
        {
            var group = await _context.Groups.FindAsync(groupId);

            if (group == null)
            {
                return NotFound();
            }

            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("DeleteProduct/{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("SetPrice")]
        public async Task<IActionResult> SetPrice([FromBody] SetPriceModel model)
        {
            var product = await _context.Products.FindAsync(model.ProductId);

            if (product == null)
            {
                return NotFound();
            }

            product.Price = model.Price;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

    public class SetPriceModel
    {
        public int ProductId { get; set; }
        public decimal Price { get; set; }
    }
}


// В данном контроллере продуктов реализован функционал удаления группы товаров и отдельного товара, а также установки цены на товар. 
// Метод deletegroup(groupid) получает идентификатор группы товаров и удаляет ее из контекста базы данных, если она существует. 
// Аналогично, метод deleteproduct(productid) удаляет товар с указанным идентификатором. 
// В обоих случаях происходит проверка наличия соответствующего объекта в базе данных, чтобы избежать возможных ошибок.
// Также в контроллере реализован метод setprice(setpricemodel model), который позволяет установить цену на конкретный товар. 
// В этом методе также проводится проверка наличия товара с указанным идентификатором в базе данных, 
// после чего обновляется его цена согласно переданным данным. После этого изменения сохраняются в базе данных с помощью метода savechangesasync().
// Важно отметить, что все методы контроллера асинхронные и возвращают объекты типа IActionResult, 
// что позволяет обрабатывать запросы от клиентов и возвращать соответствующие ответы. 
// Данный контроллер предоставляет удобный и безопасный способ управления товарами и группами товаров в веб-приложении, 
// обеспечивая корректное взаимодействие с базой данных и обработку запросов от пользователей.
