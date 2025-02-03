namespace ProductDuplicatePoC.Data.Sql
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using EFCore.BulkExtensions;
    using Microsoft.EntityFrameworkCore;
    using MongoDB.Driver;
    using ProductDuplicatePoC.Models;
    using ProductDuplicatePoC.Models.Sql;

    public class DuplicateGroupRepository
    {
        private readonly ApplicationDbContext context;

        public DuplicateGroupRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<DuplicateGroup>> SearchAsync(GroupFilter groupFilter, CancellationToken cancellationToken)
        {
            var query = this.context.Groups
                .Include(g => g.GroupItems)
                    .ThenInclude(gi => gi.Product)
                .Include(g => g.GroupItems)
                    .ThenInclude(gi => gi.Product)
                        .ThenInclude(p => p.DigitalAssets)
                .AsNoTracking();

            if (groupFilter.GroupIds?.Any() == true)
            {
                query = query.Where(g => groupFilter.GroupIds.Contains(g.Id));
            }

            if (groupFilter.ItemIds?.Any() == true)
            {
                query = query.Where(g => g.GroupItems.Any(gi => groupFilter.ItemIds.Contains(gi.ProductId)));
            }

            if (groupFilter.Markets?.Any() == true)
            {
                query = query.Where(g => g.GroupItems.Any(gi => groupFilter.Markets.Contains(gi.Product.Market)));
            }

            if (groupFilter.GroupStatus?.Any() == true)
            {
                query = query.Where(g => groupFilter.GroupStatus.Contains(g.Status));
            }

            if (groupFilter.SlotStatus?.Any() == true)
            {
                query = query.Where(g => g.GroupItems.Any(gi => gi.Product.SlotStatus.HasValue && groupFilter.SlotStatus.Contains(gi.Product.SlotStatus.Value)));
            }

            if (groupFilter.Catalogues?.Any() == true)
            {
                query = query.Where(g => g.GroupItems.Any(gi => groupFilter.Catalogues.Contains(gi.Product.Catalog)));
            }

            if (groupFilter.Type.HasValue)
            {
                query = query.Where(g => g.Type == groupFilter.Type);
            }

            if (groupFilter.ProductionTypes?.Any() == true)
            {
                query = query.Where(g => g.GroupItems.Any(gi => gi.Product.ProductionType.HasValue && groupFilter.ProductionTypes.Contains(gi.Product.ProductionType.Value)));
            }

            if (groupFilter.AssignedTo.HasValue)
            {
                query = query.Where(g => g.AssigneeUserId == groupFilter.AssignedTo);
            }

            if (groupFilter.Type.HasValue)
            {
                query = query.Where(g => g.Type == groupFilter.Type);
            }

            if (groupFilter.ProductStatus?.Any() == true)
            {
                query = query.Where(g => g.GroupItems.Any(gi => gi.Product.ProductStatusId.HasValue && groupFilter.ProductStatus.Contains(gi.Product.ProductStatusId.Value)));
            }

            if(groupFilter.ChildMerchantIds?.Any() == true)
            {
                query = query.Where(g => g.GroupItems.Any(gi => gi.ItemStatus == Dtos.ItemStatus.MarkedAsChild && gi.Product.MerchantCode.HasValue && groupFilter.ChildMerchantIds.Contains(gi.Product.MerchantCode.Value)));
            }

            if (groupFilter.ParentHasStock.HasValue)
            {
                query = query.Where(g => g.GroupItems.Any(gi => gi.ItemStatus == Dtos.ItemStatus.MarkedAsChild && gi.Product.Stock > 0));
            }

            if (groupFilter.CreatedDateStart.HasValue)
            {
                query = query.Where(g => g.CreatedDate >= groupFilter.CreatedDateStart);
            }

            if (groupFilter.CreatedDateEnd.HasValue)
            {
                query = query.Where(g => g.CreatedDate <= groupFilter.CreatedDateEnd);
            }

            if (groupFilter.ModifiedDateStart.HasValue)
            {
                query = query.Where(g => g.ModifiedDate >= groupFilter.ModifiedDateStart);
            }

            if (groupFilter.ModifiedDateEnd.HasValue)
            {
                query = query.Where(g => g.ModifiedDate <= groupFilter.ModifiedDateEnd);
            }

            if (groupFilter.HasNotes.HasValue)
            {
                if (groupFilter.HasNotes.Value)
                {
                    query = query.Where(g => g.Note != null);
                }
                else
                {
                    query = query.Where(g => g.Note == null);
                }
            }

            if (groupFilter.Sort.HasValue)
            {
                switch (groupFilter.Sort.Value)
                {
                    case SortOptions.CreatedDateAsc:
                        query = query.OrderBy(g => g.CreatedDate);
                        break;
                    case SortOptions.MatchDateAsc:
                        break;
                    case SortOptions.ModifiedDateAsc:
                        query = query.OrderBy(g => g.ModifiedDate);
                        break;
                    case SortOptions.RelevanceDesc:
                        query = query.OrderBy(g => g.GroupItems
                            .Select(gi => gi.Product.Relevance)
                            .FirstOrDefault());
                        break;
                    case SortOptions.MatchScoreDesc:
                        query = query.OrderByDescending(g => g.MatchScore);
                        break;
                }
            }

            var paginatedResult = await query
                .Skip((groupFilter.Page - 1) * groupFilter.PageSize)
                .Take(groupFilter.PageSize)
                .ToListAsync();

            return paginatedResult;
        }

        public async Task InsertManyAsync(IEnumerable<DuplicateGroup> groups)
        {
            await context.BulkInsertAsync(groups);
        }

        public async Task InsertManyGroupItemsAsync(IEnumerable<DuplicateGroupItem> groupItems)
        {
            await context.BulkInsertAsync(groupItems);
        }

        public async Task InsertProductsAsync(List<ProductDuplicatePoC.Models.Sql.Product> products)
        {
            await context.BulkInsertAsync(products);
        }

        public async Task InsertDigitalAssetsAsync(List<ProductDuplicatePoC.Models.Sql.DigitalAsset> digitalAssets)
        {
            await context.BulkInsertAsync(digitalAssets);
        }

        public async Task InsertManyAsync(IEnumerable<Merchant> merchants)
        {
            await context.BulkInsertAsync(merchants);
        }
    }
}
