using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
      
        private readonly Data _data;
        private string headerKey = "page-size";

        public ValuesController(Data data)
        {
            this._data = data;
        }

        [HttpPost("save")]
        public IActionResult SaveWords([FromBody] List<string> words)
        {
            Request.Headers.TryGetValue(headerKey, out var headerValue);
            _data.PageSize = Convert.ToInt32(headerValue);
            _data.Words = words;

            return Ok();


        }

        [HttpGet("get")]
        public ActionResult<IEnumerable<string>> GetWords()
        {
            var result = string.Empty;
            foreach (var item in _data.Words)
            {
                var maxLength = Math.Min(item.Length, _data.PageSize);
                result += item.Substring(0, maxLength)+"\r\n";
            }
            return Ok(result);
        }
    }
}
