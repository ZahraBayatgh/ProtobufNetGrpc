using Domain.IServices;
using Domain.Models;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace Client.Grpc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private ICounterService _serverGrpcConfig;

        public HomeController(ServerGrpcConfig serverGrpcConfig)
        {
            _serverGrpcConfig = serverGrpcConfig.CreateCounterServiceGrpc();
        }

        [Route("Increment/{inc:int}")]
        public IActionResult Increment(int inc)
        {
            try
            {
                var result = _serverGrpcConfig.Increment(new IncrementRequest { Inc = inc });

                return Ok(result);
            }
            catch (RpcException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Decrement/{dec:int}")]
        public IActionResult Decrement(int dec)
        {
            try
            {
                var result = _serverGrpcConfig.Decrement(new DecrementRequest(dec));

                return Ok(result);
            }
            catch (RpcException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

