using System.Security.Claims;
using fStore.Business;
using fStore.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fStore.Controller;

[ApiController]
[Route("api/v1/[controller]s")]
public class OrderController : ControllerBase
{
    //BaseController<Order, OrderReadDTO, OrderCreateDTO, OrderUpdateDTO>
    IAuthorizationService _authorizationService;
    IOrderService _orderService;

    public OrderController(IOrderService orderService, IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
        _orderService = orderService;
    }

    [Authorize(Roles = "Customer")]
    [HttpPost()]
    public async Task<ActionResult<OrderReadDTO>> CreateOrder([FromBody] OrderCreateDTO createObject)
    {
        var id = GetUserId();
        var order = await _orderService.CreateOneAsync(id, createObject);
        return CreatedAtAction(nameof(CreateOrder), order);
    }

    [Authorize]
    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<OrderReadDTO>> GetOderById([FromRoute] Guid id)
    {
        var userId = GetUserId();
        var user = HttpContext.User;
        var adminRole = user.IsInRole("Admin");
        var order = await _orderService.GetByIdAsync(id);
        if (order.UserId == userId || adminRole)
        {
            return Ok(order);
        }
        return Unauthorized("Not authorized user to see this order.");

    }

    [Authorize]
    [HttpGet("user")]
    public async Task<ActionResult<OrderReadDTO>> GetUserOrder()
    {
        var userId = GetUserId();
        var order = await _orderService.GetUserAllOrdersAsync(userId);
        return Ok(order);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet()]
    public async Task<ActionResult<IEnumerable<OrderReadDTO>>> GetAll([FromQuery] GetAllParams options)
    {
        var records = await _orderService.GetAllAsync(options);
        return Ok(records);
    }

    [Authorize]
    [HttpPatch("{id:Guid}")]
    public async Task<ActionResult<IEnumerable<OrderReadDTO>>> UpdateOrder([FromRoute] Guid id, [FromBody] OrderUpdateDTO updateObject)
    {
        var userId = GetUserId();
        var user = HttpContext.User;
        var adminRole = user.IsInRole("Admin");
        var order = await _orderService.GetByIdAsync(id);
        if (order.UserId == userId || adminRole)
        {
            return Ok(await _orderService.UpdateOneAsync(id, updateObject));
        }
        return Unauthorized("Not authorized user to perform this action.");
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult<bool>> DeleteOrderById([FromRoute] Guid id)
    {
        return Ok(await _orderService.DeleteByIdAsync(id));
    }
    //return await _orderService.DeleteByIdAsync(id);

    // var order = await _orderService.GetByIdAsync(id);
    // var authorizationResult = await _authorizationService
    //     .AuthorizeAsync(HttpContext.User, order, "AdminOrOwner");

    // if (authorizationResult.Succeeded)
    // {
    //     return Ok(await _orderService.DeleteByIdAsync(id));
    // }
    // else if (User.Identity.IsAuthenticated)
    // {
    //     Console.WriteLine("User failed");
    //     return new ForbidResult();
    // }
    // else
    // {
    //     Console.WriteLine("User1 failed");
    //     return new ChallengeResult();
    //}
    //}

    private Guid GetUserId()
    {
        var authenticatedClaims = HttpContext.User;
        var value = authenticatedClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        Console.WriteLine("login user in order controller {0}", value);
        var id = Guid.Parse(value);
        return id;
    }
}
