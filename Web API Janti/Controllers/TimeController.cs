using Microsoft.AspNetCore.Mvc;

namespace Web_API_Janti.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeController : ControllerBase
    {
        private readonly ITimeManager _timeManager;

        public TimeController(ITimeManager timeManager)
        {
            _timeManager = timeManager;
        }

        // PUT: api/Subscriptions/5
        [HttpGet]
        public string GetTime()
        {
            return _timeManager.GetTime();
        }

        // PUT: api/Time
        [HttpPost]
        public bool SetTimeZone(string timeZone)
        {
            return _timeManager.SetTimeZone(timeZone);
        }

        // GET: api/Time/
        [HttpPost("/convert")]
        public string ConvertDate(string date)
        {
            return _timeManager.ConvertDate(date);
        }
    }
}
