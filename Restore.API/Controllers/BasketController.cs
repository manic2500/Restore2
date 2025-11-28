using Microsoft.AspNetCore.Mvc;
using Restore.API.Requests.Basket;
using Restore.Application.Baskets.DTOs;
using Restore.Application.Baskets.UseCases;

namespace Restore.API.Controllers;


public class BasketController(
    ICreateBasketUseCase createBasket,
    IClearBasketUseCase clearBasket,
    IGetBasketUseCase getBasket,
    IAddBasketItemUseCase addBasketItem,
    IIncreaseBasketItemUseCase increaseBasketItem,
    IDecreaseBasketItemUseCase decreaseBasketItem,
    IRemoveBasketItemUseCase removeItem,
    IApplyVoucherUseCase applyVoucher,
    IRemoveVoucherUseCase removeVoucher) : BaseApiController
{


    // GET Basket - /api/baskets/{xid}
    [HttpGet]
    public async Task<ActionResult<BasketDto>> GetBasket()
    {
        var basketId = await GetOrCreateBasketIdAsync();
        var result = await getBasket.ExecuteAsync(basketId);
        return Ok(result);
    }

    // Create Basket and add Item
    // POST /api/baskets
    [HttpPost]
    public async Task<ActionResult<Guid>> AddItem(BasketItemRequest request)
    {
        var basketId = await GetOrCreateBasketIdAsync(true);
        await addBasketItem.ExecuteAsync(basketId, request.ProductId, request.Quantity);
        return Ok(new { basketId });
    }


    [HttpPost("items/{itemId}/increment")]
    public async Task<IActionResult> IncrementItem(Guid itemId)
    {
        /* bool success = Guid.TryParse(Request.Cookies["basketId"], out Guid basketId);
        if (!success) throw new NotFoundException("Basket Not Found"); */
        var basketId = await GetOrCreateBasketIdAsync();

        await increaseBasketItem.ExecuteAsync(basketId, itemId);
        return NoContent();
    }

    [HttpPost("items/{itemId}/decrement")]
    public async Task<IActionResult> DecrementItem(Guid itemId)
    {
        var basketId = await GetOrCreateBasketIdAsync();
        await decreaseBasketItem.ExecuteAsync(basketId, itemId);
        return NoContent();
    }

    // DELETE /{basketId}/items/{itemId} - Remove Basket Item
    [HttpDelete("items/{itemId:guid}")]
    public async Task<IActionResult> RemoveItem(Guid itemId)
    {
        var basketId = await GetOrCreateBasketIdAsync();
        await removeItem.ExecuteAsync(basketId, itemId);
        return NoContent();
    }

    // DELETE /{basketId} - CLEAR basket
    [HttpDelete]
    public async Task<IActionResult> ClearBasket()
    {
        var basketId = await GetOrCreateBasketIdAsync();
        await clearBasket.ExecuteAsync(basketId);
        return NoContent();
    }

    [HttpPost("apply-voucher")]
    public async Task<ActionResult<BasketDto>> ApplyVoucher([FromBody] ApplyVoucherRequest req)
    {
        var basketId = await GetOrCreateBasketIdAsync();
        var result = await applyVoucher.ExecuteAsync(basketId, req.VoucherCode);
        return Ok(result);
    }

    [HttpDelete("remove-voucher")]
    public async Task<IActionResult> RemoveVoucher()
    {
        var basketId = await GetOrCreateBasketIdAsync();
        await removeVoucher.ExecuteAsync(basketId);
        return NoContent();
    }


    private async Task<Guid> GetOrCreateBasketIdAsync(bool canCreate = false)
    {
        const string cookieName = "basketId";

        // Try to read the cookie
        if (Request.Cookies.TryGetValue(cookieName, out var basketIdString)
            && Guid.TryParse(basketIdString, out var basketId))
        {
            return basketId;
        }

        basketId = await createBasket.ExecuteAsync();

        Response.Cookies.Append(cookieName, basketId.ToString(), new CookieOptions
        {
            HttpOnly = true,
            Secure = true,                // required for SameSite=None
            SameSite = SameSiteMode.None, // required for cross-site cookies
            IsEssential = true,
            Expires = DateTime.UtcNow.AddDays(30)
        });

        return basketId;

        throw new InvalidOperationException("BasketId not available in HttpContext.");

    }
}
