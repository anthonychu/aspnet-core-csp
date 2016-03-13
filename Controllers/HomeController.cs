using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace csp_report.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpPost("~/cspreport")]
        public IActionResult CspReport([FromBody] CspReportRequest request)
        {
            // TODO: log request to a datastore somewhere
            _logger.LogWarning($"CSP Violation: {request.CspReport.DocumentUri}, {request.CspReport.BlockedUri}");
            
            return Ok();
        }
    }

    public class CspReportRequest
    {
        [JsonProperty(PropertyName = "csp-report")]
        public CspReport CspReport { get; set; }
    }

    public class CspReport
    {
        [JsonProperty(PropertyName = "document-uri")]
        public string DocumentUri { get; set; }

        [JsonProperty(PropertyName = "referrer")]
        public string Referrer { get; set; }

        [JsonProperty(PropertyName = "violated-directive")]
        public string ViolatedDirective { get; set; }

        [JsonProperty(PropertyName = "effective-directive")]
        public string EffectiveDirective { get; set; }

        [JsonProperty(PropertyName = "original-policy")]
        public string OriginalPolicy { get; set; }

        [JsonProperty(PropertyName = "blocked-uri")]
        public string BlockedUri { get; set; }

        [JsonProperty(PropertyName = "status-code")]
        public int StatusCode { get; set; }
    }
}
