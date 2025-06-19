using AutoMapper;
using Blood.Contract.Repositories.Entity;
using Blood.Contract.Repositories.Interface;
using Blood.Contract.Services.Interface;
using Blood.Core.APIResponse;
using Blood.Core.Utils.Firebase;
using Blood.Core;
using Blood.ModelViews.BlogPostModelViews;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Blood.Services.Service
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public BlogPostService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<ApiResult<object>> AddAsync(BlogPostCreateModelView model)
        {
            var entity = _mapper.Map<BlogPost>(model);

            if (model.Image != null)
            {
                entity.ImageUrl = await ImageHelper.Upload(model.Image);
            }

            entity.CreatedTime = DateTime.UtcNow;
            entity.CreatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            await _unitOfWork.GetRepository<BlogPost>().InsertAsync(entity);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Blog post created successfully.");
        }

        public async Task<ApiResult<object>> UpdateAsync(int id, BlogPostUpdateModelView model)
        {
            var entity = await _unitOfWork.GetRepository<BlogPost>().GetByIdAsync(id);
            if (entity == null || entity.DeletedTime.HasValue)
                return new ApiErrorResult<object>("Blog post not found.");

            if (!string.IsNullOrWhiteSpace(model.Title))
                entity.Title = model.Title;

            if (!string.IsNullOrWhiteSpace(model.Content))
                entity.Content = model.Content;

            if (!string.IsNullOrWhiteSpace(model.Author))
                entity.Author = model.Author;

            if (model.Image != null)
                entity.ImageUrl = await ImageHelper.Upload(model.Image);

            entity.LastUpdatedTime = DateTime.UtcNow;
            entity.LastUpdatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            _unitOfWork.GetRepository<BlogPost>().Update(entity);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Blog post updated successfully.");
        }

        public async Task<ApiResult<object>> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.GetRepository<BlogPost>().GetByIdAsync(id);
            if (entity == null || entity.DeletedTime.HasValue)
                return new ApiErrorResult<object>("Blog post not found.");

            entity.DeletedTime = DateTime.UtcNow;
            entity.DeletedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            _unitOfWork.GetRepository<BlogPost>().Update(entity);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Blog post deleted successfully.");
        }

        public async Task<ApiResult<BlogPostModelView>> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.GetRepository<BlogPost>().GetByIdAsync(id);
            if (entity == null || entity.DeletedTime.HasValue)
                return new ApiErrorResult<BlogPostModelView>("Blog post not found.");

            var result = _mapper.Map<BlogPostModelView>(entity);
            return new ApiSuccessResult<BlogPostModelView>(result);
        }

        public async Task<ApiResult<BasePaginatedList<BlogPostModelView>>> GetAllAsync(int pageNumber, int pageSize, string? keyword)
        {
            var query = _unitOfWork.GetRepository<BlogPost>().Entities
                .Where(x => !x.DeletedTime.HasValue);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                query = query.Where(x =>
                    x.Title.Contains(keyword) ||
                    x.Author.Contains(keyword) ||
                    x.Content.Contains(keyword));
            }

            int totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(x => x.CreatedTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var modelViews = _mapper.Map<List<BlogPostModelView>>(items);
            var result = new BasePaginatedList<BlogPostModelView>(modelViews, totalCount, pageNumber, pageSize);

            return new ApiSuccessResult<BasePaginatedList<BlogPostModelView>>(result);
        }


        public async Task<ApiResult<List<BlogPostModelView>>> GetAllWithoutPagingAsync(string? title)
        {
            var query = _unitOfWork.GetRepository<BlogPost>().Entities
                .Where(x => !x.DeletedTime.HasValue);

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(x => x.Title.Contains(title));

            var list = await query
                .OrderByDescending(x => x.CreatedTime)
                .ToListAsync();

            var result = _mapper.Map<List<BlogPostModelView>>(list);
            return new ApiSuccessResult<List<BlogPostModelView>>(result);
        }


    }

}
