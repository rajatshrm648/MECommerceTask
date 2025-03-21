using MECommerceTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MECommerceTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("applicableCouponsCartWise")]
        public async Task<ActionResult<List<CartWiseCoupon>>> GetApplicableCartWiseCoupon([FromBody] Cart cart)
        {
            try
            {
                var coupons = await _context.CartWiseCoupons.Include(c => c.Coupon).ToListAsync();
                if (coupons.Count == 0)
                    return NotFound(new { message = "No Coupon Found." });

                var applicableCoupons = coupons
                    .Where(c => cart.TotalPrice >= c.Threshold)
                    .Select(c => new
                    {
                        couponId = c.Coupon.Id,
                        type= c.Coupon.Type,
                        discountValue = c.Coupon.DiscountValueInPercent,
                        discountApplied = (c.Coupon.DiscountValueInPercent / 100) * cart.TotalPrice,
                        expirationDate = c.Coupon.ExpirationDate,
                    })
                    .ToList();

                if (applicableCoupons.Count == 0)
                    return NotFound(new { message = "No applicable coupons found for this cart." });

                return Ok(new { applicableCoupons });

            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }
        [HttpPost("applicableCouponsCartWise/{id}")]
        public async Task<ActionResult<Cart>> ApplyCartWiseCoupon(int id, [FromBody] Cart cart)
        {
            try
            {
                var cartWiseCoupon = await _context.CartWiseCoupons.Include(c => c.Coupon)
                .FirstOrDefaultAsync(c => c.CouponId == id);
                if (cartWiseCoupon == null)
                {
                    return NotFound("Coupon not found or not applicable.");
                }
                if (cart.TotalPrice < cartWiseCoupon.Threshold)
                {
                    return BadRequest("Cart total does not meet the required threshold for this coupon.");
                }
                decimal discountAmount = (cartWiseCoupon.Coupon.DiscountValueInPercent / 100) * cart.TotalPrice;
                cart.FinalPrice = cart.TotalPrice - discountAmount;
                return Ok(new { message = "Coupon applied successfully!", updatedCart = cart });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }
    }
}