using Microsoft.AspNetCore.Mvc;
using WebApplicationVisual.Repositories;

namespace WebApplicationVisual.Controllers;
[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    
    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet, Route("getServiceById")]
    public async Task<ActionResult> GetServiceById(int id)
    {
        try
        {
            var sr = new ServicesRepository();
            _logger.LogInformation("Получен тариф с именем =" + sr.GetServiceById(id).ServiceName);
            return Ok(sr.GetServiceById(id));
        }
        catch (Exception e)
        {
            return BadRequest("Тариф не найден");
        }
    }

    [HttpGet, Route("getUserCards")]
    public async Task<ActionResult> GetUserCards(int id)
    {
        var cr = new CreditCardRepository();
        return Ok(cr.GetCardsOfUser(id));
    }

    [HttpGet, Route("isSubscribed")]
    public async Task<ActionResult> IsSubscribed(int userId,int serviceId)
    {
        try
        {
            var sr = new SubjectRepository();
            foreach (var a in sr.GetSubscribesList(serviceId))
            {
                if (userId == a)
                {
                    _logger.LogInformation("Пользователь подписан");
                    return Ok("Пользователь подписан");
                }
            }

            return Ok("Пользователь не подписан");
        }
        catch (Exception e)
        {
            _logger.LogInformation("Невозможно проверить подписку");
            return BadRequest("Невозможно проверить подписку");
        }
    }
}