using Microsoft.AspNetCore.Mvc;
using Sentinel.Services;

namespace Sentinel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        [HttpGet("[action]/{key}/{value}")]
        public async Task<IActionResult> SetValue(string key, string value)
        {
            var redis = await RedisService.RedisMasterDatabase();
            await redis.StringSetAsync(key, value);
            return Ok();
        }

        [HttpGet("[action]/{key}/{value}")]
        public async Task<IActionResult> GetValue(string key)
        {
            var redis = await RedisService.RedisMasterDatabase();
            var data = await redis.StringGetAsync(key);
            //bu şekilde yazarsak isInteger, isNull, isNullorEmpty, hasValue bilgileri dönüyor
            //return Ok(data);

            //bu şekilde yazarsak key'e ait olan value bilgisini dönüyor
            return Ok(data.ToString());
        }
    }
}
