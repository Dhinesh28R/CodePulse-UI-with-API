using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace CodePulse.API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            //throw new NotImplementedException();

            await dbContext.Categories.AddAsync(category);//provide the category to the cllections we have inside  the dbcontext
            await dbContext.SaveChangesAsync();// this EF will make a changes on the db

            return category;
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
           var exisitingCategory =  await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if(exisitingCategory is null)
            {
                return null;
            }

            dbContext.Categories.Remove(exisitingCategory);
            await dbContext.SaveChangesAsync();
            return exisitingCategory;

        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            //we have to use dbcontext for get all items from list
            return await dbContext.Categories.ToListAsync(); // get all the categories from the databse

        }

        public async Task<Category?> GetById(Guid id)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            //first find the category
         var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(x=>x.Id== category.Id);

            if (existingCategory != null)
            {
                dbContext.Entry(existingCategory).CurrentValues.SetValues(category);
                await dbContext.SaveChangesAsync();
                return category;
            }
            return null;
        }
    }
}
