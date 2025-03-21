using MECommerceTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MECommerceTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CouponController(AppDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coupon>>> GetCoupons()
        {
            try
            {
                var coupon = await _context.Coupons.ToListAsync();
                if(coupon == null || !coupon.Any()){
                    return NotFound(new {message = "No coupons found."});
                }
                return Ok(coupon);
            }
            catch (Exception e)
            {
                return StatusCode(500, new{message= "An error occured while retrieving coupons.", error = e.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Coupon>> GetCoupon(int id)
        {
            try
            {
                var coupon = await _context.Coupons.FindAsync(id);
                if (coupon == null)
                {
                    return NotFound(new {message = "Coupon Not found."});
                }
                return Ok(coupon);

            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<Coupon>> CreateCoupon(Coupon coupon)
        {
            try
            {
                _context.Coupons.Add(coupon);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetCoupon), new { id = coupon.Id }, new { message = "Success",coupon });
            }
            catch (System.Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        [HttpPut("UpdateCoupon/{id}")]
        public async Task<IActionResult> UpdateCoupon(int id, Coupon updatedCoupon)
        {
            if (id != updatedCoupon.Id)
            {
                return BadRequest(new {message= "id not matched"});
            }
            _context.Entry(updatedCoupon).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return StatusCode(200, new { message = "Success" });
            }
            catch (Exception e)
            {
                if (!_context.Coupons.Any(c => c.Id == id))
                {
                    return NotFound(new { message = "Coupon Not found." });
                }
                return StatusCode(500, new { error = e.Message });

            }
        }

        [HttpDelete("DeleteCoupon/{id}")]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            var coupon = await _context.Coupons.FindAsync(id);
            if (coupon == null)
            {
                return NotFound(new {message = "coupon not found."});
            }
            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();
            return StatusCode(200, new { message = "Deleted Coupon Successfully." });
        }
    }


}