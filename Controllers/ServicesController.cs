using System.Collections.Generic;
using System.ComponentModel.Design;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using org.cdshooks.example.Models;

namespace org.cdshooks.example.Controllers
{
    [EnableCors("WithCredentialsAnyOrigin")]
    public class ServicesController : Controller
    {
        ILogger _logger;
        public ServicesController(ILogger<ServicesController> logger)
        {
            this._logger = logger;
            _logger.LogInformation("Log Initialized!");
        }

        [HttpGet("/cds-services")]
        public ActionResult Discovery()
        {
            var services = new Dictionary<string, IList<Service>>
            {
                { "services", ServiceRepository.Get() }
           };

            return Json(services);
        }

        [HttpPost("/cds-services/static")]
        public async Task<ActionResult> Static(HttpRequest req)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            _logger.LogInformation($"Request Body: {requestBody}");

            var cards = new Dictionary<string, IList<Card>>
            {
                { "cards", CardRepository.Get() }
            };

            return Json(cards);
        }
    }
}
