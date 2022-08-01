using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkedAccountController : ControllerBase
    {
        private ILinkedAccountService _linkedAccountService;

        public LinkedAccountController(ILinkedAccountService linkedAccountService)
        {
            _linkedAccountService = linkedAccountService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
           var result = _linkedAccountService.GetAll();

           if (result.Success)
           {
               return Ok(result);
           }

           return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(LinkedAccount account)
        {
            var result = _linkedAccountService.Add(account);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(LinkedAccount account)
        {
            var result = _linkedAccountService.Update(account);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(LinkedAccount account)
        {
            var result = _linkedAccountService.Delete(account);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
