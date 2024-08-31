using Discount.gRPC.Data;
using Discount.gRPC.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Services
{
    public class DiscountService(DiscountContext dbcontext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbcontext.Coupons
                .FirstOrDefaultAsync(c => c.ProductName == request.ProductName);

            if (coupon == null)
            {
                coupon = new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount for the selected product" };
            }
            logger.LogInformation("Discount is retrieved for ProductName :{productName},Amount :{amount}", coupon.ProductName, coupon.Amount);
            var couponModel = coupon.Adapt<CouponModel>();

            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();

            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));

            dbcontext.Coupons.Add(coupon);
            await dbcontext.SaveChangesAsync();
            logger.LogInformation("Discount is created successfully. ProductName = {prodcutName},Amount = {amount}", coupon.ProductName, coupon.Amount);

            var couponModel = coupon.Adapt<CouponModel>();

            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var updatedCoupon = request.Coupon.Adapt<Coupon>();

            if (updatedCoupon == null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));

            dbcontext.Coupons.Update(updatedCoupon);
            await dbcontext.SaveChangesAsync();
            logger.LogInformation("Discount is updated successfully. ProductName = {prodcutName},Amount = {amount}", updatedCoupon.ProductName, updatedCoupon.Amount);
            var couponModel = updatedCoupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbcontext.Coupons
                .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (coupon == null)
                throw new RpcException(new Status(StatusCode.NotFound, "There is no discount for this product!"));

            dbcontext.Coupons.Remove(coupon);
            await dbcontext.SaveChangesAsync();
            logger.LogInformation("Discount is deleted successfully for ProductName :{productName}", request.ProductName);

            return new DeleteDiscountResponse { Success = true };
        }
    }
}