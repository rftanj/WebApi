using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        public readonly ApplicationService _service;
        public MoviesController(ApplicationService service)
        {
            _service = service;
        }

        // GET: api/<MovieController>
        [HttpGet]
        public IActionResult Get()
        {

            try
            {
                var datas = _service.GetMovies();
                if (datas is null) return NotFound("Data is not found!");
                return Ok(datas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message.ToString() });
            }
           
        }

        // GET api/<MovieController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var datas = _service.GetMovie(id);
                if (datas is null) return NotFound("Data is not found!");
                    
                return Ok(datas);
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message.ToString() });
            }
            
        }

        // POST api/<MovieController>
        [HttpPost]
        public IActionResult Post([FromBody] MovieDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Something went wrong!");
                }
                _service.SaveMovie(dto);
                return CreatedAtAction(nameof(Get), "Data Saved!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message.ToString() });
            }
        }

        // PUT api/<MovieController>/5
        [HttpPatch("{id}")]
        public IActionResult Put(int id, [FromBody] MovieDTO value)
        {
            try
            {
                var data = _service.EditMovie(id, value);
                if (data is null) return NotFound("Data is not found!");

                return Ok("Data Edited");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message.ToString() });
            }
           
        }

        // DELETE api/<MovieController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var data = _service.DeleteMovie(id);
                if (!data) return NotFound("Data is not found!");
                return Ok("Data Deleted!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message.ToString() });
            }
        }
    }
}
