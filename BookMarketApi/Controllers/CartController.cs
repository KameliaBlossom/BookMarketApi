
using BookMarketApi.DataAccess.Contracts;
using BookMarketApi.Extension;
using BookMarketApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookMarketApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CartController : ControllerBase
{
    private readonly ICartServiceRepository _cartService;

    public CartController(ICartServiceRepository cartService)
    {
        _cartService = cartService;
    }

    [HttpGet]
    public async Task<ActionResult<Cart>> GetCart()
    {
        var userId = User.GetUserId();
        var cart = await _cartService.GetCartAsync(userId);
        return Ok(cart);
    }

    [HttpPost("items")]
    public async Task<ActionResult<CartItem>> AddToCart(Guid bookId, int quantity)
    {
        var userId = User.GetUserId();
        var item = await _cartService.AddToCartAsync(userId, bookId, quantity);
        return Ok(item);
    }

    [HttpDelete("items/{bookId}")]
    public async Task<ActionResult> RemoveFromCart(Guid bookId)
    {
        var userId = User.GetUserId();
        var result = await _cartService.RemoveFromCartAsync(userId, bookId);
        return result ? Ok() : NotFound();
    }

    [HttpPut("items/{bookId}")]
    public async Task<ActionResult> UpdateQuantity(Guid bookId, int quantity)
    {
        var userId = User.GetUserId();
        var result = await _cartService.UpdateQuantityAsync(userId, bookId, quantity);
        return result ? Ok() : NotFound();
    }

    [HttpPost("checkout")]
    public async Task<ActionResult<Order>> Checkout()
    {
        var userId = User.GetUserId();
        var order = await _cartService.CheckoutAsync(userId);
        return Ok(order);
    }

    [HttpDelete]
    public async Task<ActionResult> ClearCart()
    {
        var userId = User.GetUserId();
        await _cartService.ClearCartAsync(userId);
        return Ok();
    }
}
