using Restore.Application.Baskets.UseCases;

namespace Restore.API.Middlewares;

public class BasketIdMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {

        // Check if cookie exists and is valid
        if (!context.Request.Cookies.TryGetValue("basketId", out var id) || !Guid.TryParse(id, out Guid basketId))
        {
            var createBasket = context.RequestServices.GetRequiredService<ICreateBasketUseCase>();

            // Create new basketId
            //basketId = Guid.NewGuid();
            basketId = await createBasket.ExecuteAsync();

            // Set cookie in response
            context.Response.Cookies.Append(
                "basketId",
                basketId.ToString(),
                new CookieOptions
                {
                    IsEssential = true,
                    //HttpOnly = true,
                    //Secure = true,
                    //SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(30)
                }
            );
        }

        // Store basketId in HttpContext.Items for downstream components
        context.Items["BasketId"] = basketId;

        // Continue request pipeline
        await next(context);
    }
}

