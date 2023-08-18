﻿using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        //In here we only create a defintion of a method not an actual implementation
        Task<Category>CreateAsync(Category category); 

        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetById(Guid id);

        Task<Category?>UpdateAsync(Category category);

        Task<Category?> DeleteAsync(Guid id);
    }
}
