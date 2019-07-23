using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RestWithAspNETUdemy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        // GET api/Sum/1/1
        [HttpGet("Sum/{firstNumber}/{secondNumber}")]
        public IActionResult Sum(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var result = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);

                return Ok(result.ToString());
            }

            return BadRequest("Invalid Input");
        }

        // GET api/Subtract/1/1
        [HttpGet("Subtract/{firstNumber}/{secondNumber}")]
        public IActionResult Subtract(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var result = ConvertToDecimal(firstNumber) - ConvertToDecimal(secondNumber);

                return Ok(result.ToString());
            }

            return BadRequest("Invalid Input");
        }

        [HttpGet("Multiply/{firstNumber}/{secondNumber}")]
        public IActionResult Multiply(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var result = ConvertToDecimal(firstNumber) * ConvertToDecimal(secondNumber);

                return Ok(result.ToString());
            }

            return BadRequest("Invalid Input");
        }

        [HttpGet("Divide/{firstNumber}/{secondNumber}")]
        public IActionResult Divide(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var result = ConvertToDecimal(firstNumber) / ConvertToDecimal(secondNumber);

                return Ok(result.ToString());
            }

            return BadRequest("Invalid Input");
        }

        [HttpGet("Mean/{firstNumber}/{secondNumber}")]
        public IActionResult Mean(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var result = (ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber)) / 2;

                return Ok(result.ToString());
            }

            return BadRequest("Invalid Input");
        }

        [HttpGet("SquareRoot/{number}")]
        public IActionResult Divide(string number)
        {
            if (IsNumeric(number))
            {
                var result = Math.Sqrt((double)ConvertToDecimal(number));

                return Ok(result.ToString());
            }

            return BadRequest("Invalid Input");
        }

        private bool IsNumeric(string number)
        {
            decimal value;
            bool isNumeric = decimal.TryParse(number, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out value);
            return isNumeric;
        }

        private decimal ConvertToDecimal(string number)
        {
            decimal value;
            if (decimal.TryParse(number, out value))
            {
                return value;
            }
            return 0;
        }
    }
}
