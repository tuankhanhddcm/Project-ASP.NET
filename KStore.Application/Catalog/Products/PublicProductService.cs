using KStore.Application.Catalog.Products.Dtos;
using KStore.Application.Catalog.Products.Dtos.Public;
using KStore.Application.Dtos;
using KStore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KStore.Application.Catalog.Products
{
    public class PublicProductService : IPublicProductService
    {
        private readonly KStoreDBContext _dbContext;
        public PublicProductService(KStoreDBContext context)
        {
            _dbContext = context;
        }
        public async Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetProductPagingRequest request)
        {
            //1. Select join
            var query = from p in _dbContext.Products
                        join pt in _dbContext.ProductTranslations on p.Id equals pt.ProductId
                        join c in _dbContext.Categories on p.CategoryId equals c.Id
                        select new { p, pt, c };
            //2. filter
            
            if (request.CategoryId.HasValue && request.CategoryId.Value > 0 )
            {
                query = query.Where(p => p.c.Id == request.CategoryId);

            }
            //3. Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount,
                }).ToListAsync();

            //4. Select and projection
            var pageResult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data,
            };
            return pageResult;
        }
    }
}
