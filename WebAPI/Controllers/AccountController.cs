using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("getbycustomerid")]
        public IActionResult GetByCustomerId(int customerId)
        {
            var result = _accountService.GetByCustomerId(customerId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getaccountfullattribute")]
        public IActionResult GetAccountFullAttribute()
        {
            var result = _accountService.GetAccountFullAttribute();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("showmoney")]
        public IActionResult ShowMoney(int customerId)
        {
            var result = _accountService.ShowMoney(customerId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _accountService.GetAll();
            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Account account)
        {
            var result = _accountService.Add(account);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Account account)
        {
            var result = _accountService.Delete(account);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Account account)
        {
            var result = _accountService.Update(account);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("deletebyid")]
        public IActionResult DeleteById(int id)
        {
            var result = _accountService.DeleteById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _accountService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("addmoney")]
        public IActionResult AddMoney(MoneyAddDto moneyAddDto)
        {
            var result = _accountService.AddMoney(moneyAddDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyaccountnumber")]
        public IActionResult GetByAccountNumber(string accountNumber)
        {
            var result = _accountService.GetByAccountNumber(accountNumber);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("dropmoney")]
        public IActionResult DropMoney(DropMoneyDto dropMoneyDto)
        {
            var result = _accountService.WithDrawMoney(dropMoneyDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("withdrawallmoney")]
        public IActionResult WithDrawAllMoney(int id)
        {
            var result = _accountService.WithDrawAllMoney(id);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("checkmaxmoney")]
        public IActionResult CheckMaxMoney(DropMoneyDto dropMoneyDto)
        {
            var result = _accountService.CheckMaxMoney(dropMoneyDto);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
    }
}
