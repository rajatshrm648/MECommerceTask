using MECommerceTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MECommerceTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductWiseController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductWiseController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("ProductWise")]
        public async Task<ActionResult<object>> ProductWiseCoupon(int productId)
        {
            try
            {
                var productWiseCoupon = await _context.ProductWiseCoupons
                .Include(pwc => pwc.Coupon)
                .FirstOrDefaultAsync(pwc => pwc.ProductId == productId);

                if (productWiseCoupon == null)
                    return NotFound(new { message = "No Product applicable Coupon Found." });

                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
                if(product == null)
                {
                    return NotFound(new {message ="Product not FOund."});
                }

                decimal discountAmount = (productWiseCoupon.Coupon.DiscountValueInPercent/100) * product.Price;
                decimal FinalPrice = product.Price - discountAmount ;
                return Ok(new
                {
                    productId = productId,
                    originalPrice = product.Price,
                    discountPercentage = productWiseCoupon.Coupon.DiscountValueInPercent,
                    discountAmount = discountAmount,
                    finalPrice = FinalPrice
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }
    }
}