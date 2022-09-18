using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.Shared.Models;


namespace SignalR.CarProcessApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarProcessController : ControllerBase
    {
        //private readonly IDatabaseService service;

        //public CarProcessController(IDatabaseService service)
        //{
        //    this.service = service;
        //}

        [HttpPost]
        [Route("Process")]
        public IActionResult Process(CarStatus status)
        {
            //var result=service.updateCarStatus(status);

            return Ok("");
        }
    }
}
