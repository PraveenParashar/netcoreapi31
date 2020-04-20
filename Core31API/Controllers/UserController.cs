using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Common;
using DataAccess.Repositories;
using DTO;
using Microsoft.AspNetCore.Authorization;
namespace Core31API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly MyDBSetting _dBSetting;
        private readonly IUserRepository _userRepository;
        public UserController(ILogger<UserController> logger,IOptions<MyDBSetting> dbsetting,IUserRepository userRepository)
        {
            _logger = logger;
            _dBSetting = dbsetting.Value;
            _userRepository = userRepository;

        }

        //GET: api/User
       
        [HttpGet]
       public IEnumerable<UserDTO> Get()
        {
            var userslist = from u in _userRepository.GetAllUser()
                            select new UserDTO()
                            {
                                UserID = u.UserID,
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                JobID = u.JobID,
                                Address = String.Format(@"City :{0} Country :{1} ", u.Address.City, u.Address.Country)
                            };
            return userslist;
        }

        // GET: api/Books/5


        // GET: api/User/5
        
        [HttpGet("{id}")]
        public string GetByID(int id)
        {
            return "your Id is "+ id;
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }



        // [ResponseType(typeof(BookDetailDTO))]
        //public async Task<IHttpActionResult> GetBook(int id)
        // {
        //     var book = await db.Books.Include(b => b.Author).Select(b =>
        //         new BookDetailDTO()
        //         {
        //             Id = b.Id,
        //             Title = b.Title,
        //             Year = b.Year,
        //             Price = b.Price,
        //             AuthorName = b.Author.Name,
        //             Genre = b.Genre
        //         }).SingleOrDefaultAsync(b => b.Id == id);
        //     if (book == null)
        //     {
        //         return NotFound();
        //     }

        //     return Ok(book);
        // }
    }
}
