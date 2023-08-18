﻿using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetAllAsync();

       Task<BlogPost?> GetByIdAsync(Guid id);



        Task<BlogPost?> GetByUrlHandleAysnc(string urlHandle);

        Task<BlogPost?> UpdateAsync(BlogPost blogPost);

        Task<BlogPost?> DeleteAsync(Guid id);
        Task GetByUrlHandleAsync(string urlHanlde);
    }
}