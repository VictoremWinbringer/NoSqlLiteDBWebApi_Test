using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UserApi.Dal.Abstract;
using UserApi.Models;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserRepository repository;

        public UsersController(IUserRepository repository)
        {
            this.repository = repository;
        }
        // GET api/user
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(repository.Get());
        }

        // GET api/user/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = repository.Get(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // POST api/user
        [HttpPost]
        public IActionResult Post([FromBody]User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            repository.Create(user);

            return Created($"api/users/{user.Id}", user);
        }

        // PUT api/user/5
        [HttpPut]
        public IActionResult Put([FromBody]User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            repository.Update(user);

            return Ok();
        }

        // DELETE api/user/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            repository.Delete(id);

            return Ok();
        }
    }
}
