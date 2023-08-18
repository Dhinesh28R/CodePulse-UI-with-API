using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace CodePulse.API.Controllers
{
    //https://localhost:xxxx/api/categories
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        // private readonly ApplicationDbContext dbContext; // so we can use this field inside the post method 

        //inject dbcontext class into a controller 
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository=categoryRepository;
        }


        //Post a Data into DB
        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateCategory([FromBody]CreateCategoryRequestDto request)//request will send to angular app
        {
            // convert this dto to domain model
            //map DTO to domain model

            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            await categoryRepository.CreateAsync(category);
           

            //dont to expose the doamin models.. So we have to map       Domain model to Dto
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };
            

            return Ok(response);//give the response back to the caller 

        }

        //GET://https://localhost:7030/api/categories (Path for get all categories)

        [HttpGet]
        
        public async Task<IActionResult> GetAllCategories()
        {
           var categories= await categoryRepository.GetAllAsync();// will give all from database

            //map domain model to dto
            var response=new List<CategoryDto>();
            foreach (var category in categories)
            {
                response.Add(new CategoryDto
                { Id = category.Id, 
                    Name = category.Name,
                    UrlHandle=category.UrlHandle 
                });
            }
            return Ok(response);
        }

        //GET By Id://https://localhost:7030/api/categories/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute]Guid id)
        {
            var exisitingCategory= await categoryRepository.GetById(id);
            if(exisitingCategory is null)
            {
                return NotFound();
            }

            var response = new CategoryDto
            {
                Id = exisitingCategory.Id,
                Name = exisitingCategory.Name,
                UrlHandle = exisitingCategory.UrlHandle
            };

            return Ok(response);
        }


        //PUT://https://localhost:7030/api/categories/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> EditCategory([FromRoute] Guid id,UpdateCategoryRequestDto request)
        {
            //convert DTO to Domain model
            var category = new Category
            {
                Id = id,
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            category = await categoryRepository.UpdateAsync(category);
            if(category == null)
            {
                return NotFound();
            }
            //convert doamin model into DTO
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };
            return Ok(response);
        }

        //Delete://https://localhost:7030/api/categories/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
           var category = await categoryRepository.DeleteAsync(id);

            if( category is null)
            {
                return NotFound();
            }
             
            //convert domain model to DTO

            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }
    }
}
