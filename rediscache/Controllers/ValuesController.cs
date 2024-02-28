using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace rediscache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IMemoryCache _memoryCache;

        public ValuesController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        [HttpGet("setName/{name}")]
        public void SetName(string name)
        {
            _memoryCache.Set("name", name);
        }
        [HttpGet]       
        public string GetName()
        {
            //buna return dedik ama bizim name dolu olmadığında hata alabiliriz.
            //name was null mesela
            //return _memoryCache.Get<string>("name");

            if(_memoryCache.TryGetValue<string>("name", out string name))
            {
                return name.Substring(2);
            }
            return "";
        }
        [HttpGet("setDate")]
        public void SetDate()
        {
            _memoryCache.Set<DateTime>("date", DateTime.Now, options: new()
            {
                //datanın mutlak süresi
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                //her işlemde datanın süresininb periyodu
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });
        }
        [HttpGet("getDate")]
        public DateTime GetDate()
        {
            return _memoryCache.Get<DateTime>("date");
        }
    }
}
