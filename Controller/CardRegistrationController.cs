using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYChamp.DbContexts;
using MYChamp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYChamp.Controllers // Note: Corrected "Controller" to "Controllers"
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardRegistrationController : ControllerBase
    {
        private readonly MYChampDbContext _dbContext;

        public CardRegistrationController(MYChampDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<TestRegistrationModel>>> GetAllCards()
        {
            var cards = await _dbContext.testregistration.ToListAsync();
            return Ok(cards);
        }

        //[HttpPost]
        //public async Task<ActionResult<TestRegistrationModel>> AddCard([FromBody] TestRegistrationModel TestRegistrationModel)
        //{
        //    if (TestRegistrationModel != null)
        //    {
        //        _dbContext.testregistration.Add(TestRegistrationModel);
        //        await _dbContext.SaveChangesAsync();
        //        return CreatedAtAction(nameof(GetAllCards), new { id = TestRegistrationModel.testid }, TestRegistrationModel);
        //    }
        //    else
        //    {
        //        return BadRequest(ModelState);
        //    }
        //}
        //[HttpPost]
        //public async Task<ActionResult<TestAttempt>> SubmitUserResponse([FromBody] TestAttempt UserResponseModel)
        //{
        //    if (UserResponseModel != null)
        //    {
        //        _dbContext.QuestionModel.Add(UserResponseModel);
        //        await _dbContext.SaveChangesAsync();
        //        return CreatedAtAction(nameof(UserResponseModel), new { id = UserResponseModel.Id }, UserResponseModel);
        //    }
        //    else
        //    {
        //        return BadRequest(ModelState);
        //    }
        //}


    }
}
