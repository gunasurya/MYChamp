using System;
using System.Collections.Generic;
using System.Drawing; // Add this directive
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MYChamp.Controllers;
using MYChamp.DbContexts;
using MYChamp.Models;

namespace MYChamp.Pages.CardRegistration
{ 
    public class CardRegistrationModel : PageModel
    {
        private readonly MYChampDbContext _dbContext;
        private readonly CardRegistrationController _CardRegistrationController;
        public CardRegistrationModel(CardRegistrationController CardRegistrationController, MYChampDbContext MYChampDbContext)
        {
            _dbContext = MYChampDbContext;
            _CardRegistrationController = CardRegistrationController;
        }
        [BindProperty]
        public TestRegistrationModel testRegistrationModel { get; set; }
        public List<TestRegistrationModel> TestList  {get; set;}
 
        public async Task<IActionResult> OnGet()
        {
            var Cards = await _dbContext.testregistration.ToListAsync();
            TestList  = new List<TestRegistrationModel>();
            TestList = Cards;
           
            return Page();
        }

       
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check if an image file is uploaded
            if (testRegistrationModel.ImageFile != null && testRegistrationModel.ImageFile.Length > 0)
            {
                // Read the uploaded image file into a byte array
                using (var memoryStream = new MemoryStream())
                {
                    testRegistrationModel.ImageFile.CopyTo(memoryStream);
                    byte[] imageBytes = memoryStream.ToArray();

                    // Convert the byte array to a base64 string
                    string base64String = Convert.ToBase64String(imageBytes);

                    // Set the base64 string to the Imagebase64 property of TestRegistrationModel
                    testRegistrationModel.imagebase64 = base64String;

                    // You may also want to store the image name if needed
                    testRegistrationModel.imagename = testRegistrationModel.ImageFile.FileName;
                }
            }

            // Map CardViewModel to TestRegistrationModel entity
            var cardModelEntity = new TestRegistrationModel
            {
                name = testRegistrationModel.name,
                timer = testRegistrationModel.timer,
                description = testRegistrationModel.description,
                imagebase64 = testRegistrationModel.imagebase64,
                imagename = testRegistrationModel.imagename
            };

            if (testRegistrationModel != null)
            {
                _dbContext.testregistration.Add(testRegistrationModel);
                await _dbContext.SaveChangesAsync();
                TempData["SuccessMessage"] = "Your Test has been created successfully.";

                return RedirectToPage("/Success"); // Redirect to a success page
            }
            else
            {
                return BadRequest(ModelState);
               
            }
         

        }

    }
}
