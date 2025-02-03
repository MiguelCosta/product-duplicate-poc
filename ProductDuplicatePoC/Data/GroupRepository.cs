namespace ProductDuplicatePoC.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using ProductDuplicatePoC.Models;

public class GroupRepository
{
    private readonly IMongoCollection<Group> _collection;

    public GroupRepository(IMongoClient mongoClient)
    {
        _collection = mongoClient.GetDatabase("productduplicate").GetCollection<Models.Group>("groups");
    }

    public async Task InsertAsync(Models.Group group)
    {
        await _collection.InsertOneAsync(group);
    }

    public async Task InsertManyAsync(IEnumerable<Models.Group> groups)
    {
        try
        {
            var operations = new List<WriteModel<Group>>();

            foreach (var group in groups)
            {
                var filter = Builders<Group>.Filter.Eq(g => g.Id, group.Id);
                var update = new ReplaceOneModel<Group>(filter, group)
                {
                    IsUpsert = true
                };

                operations.Add(update);
            }

            await _collection.BulkWriteAsync(operations);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public async Task UpdateAsync(List<(string ItemId, string NewDescription)> updates, CancellationToken cancellationToken)
    {
        try
        {
            var operations = new List<WriteModel<Group>>();

            foreach (var item in updates)
            {
                var filter = Builders<Models.Group>.Filter.ElemMatch<Models.Product>(
                    x => x.Items,
                    Builders<Models.Product>.Filter.Eq(p => p.Id, item.ItemId));

                var updateDefinition = Builders<Models.Group>.Update.Set("Items.$.Description", item.NewDescription);

                var update = new UpdateManyModel<Group>(filter, updateDefinition);

                operations.Add(update);
            }

            await _collection.BulkWriteAsync(operations, null, cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public async Task<List<Models.Group>> GetAllAsync(
        Models.GroupFilter filter,
        CancellationToken cancellationToken)
    {
        var queryFilter = Builders<Models.Group>.Filter.Empty;

        if (filter.AssignedTo.HasValue)
        {
            queryFilter &= Builders<Models.Group>.Filter.Eq(x => x.AssigneeUserId, filter.AssignedTo.Value);
        }

        if (filter.BrandIds?.Any() ?? false)
        {
            queryFilter &= Builders<Models.Group>.Filter.ElemMatch<Models.Product>(
                x => x.Items,
                Builders<Models.Product>.Filter.In(p => p.BrandId, filter.BrandIds));
        }

        if (filter.BrandProductIds?.Any() ?? false)
        {
            queryFilter &= Builders<Models.Group>.Filter.ElemMatch<Models.Product>(
                x => x.Items,
                Builders<Models.Product>.Filter.In(p => p.BrandProductId, filter.BrandProductIds));
        }

        if (filter.Type.HasValue)
        {
            queryFilter &= Builders<Models.Group>.Filter.Eq(x => x.Type, filter.Type.Value);
        }

        if (filter.Catalogues?.Any() ?? false)
        {
            queryFilter &= Builders<Models.Group>.Filter.ElemMatch<Models.Product>(
                x => x.Items,
                Builders<Models.Product>.Filter.In(p => p.Catalog, filter.Catalogues));
        }

        if (filter.ChildHasDigitalAsset.HasValue)
        {
            queryFilter &= Builders<Models.Group>.Filter.Eq(x => x.HasChildDigitalAsset, filter.ChildHasDigitalAsset.Value);
        }

        if (filter.ChildHasStock.HasValue)
        {
            queryFilter &= Builders<Models.Group>.Filter.Eq(x => x.HasChildStock, filter.ChildHasStock.Value);
        }

        if (filter.ChildMerchantIds is { Count: > 0 })
        {
            queryFilter &= Builders<Models.Group>.Filter.AnyIn(x => x.ChildMerchantsIds, filter.ChildMerchantIds);
        }

        if (filter.CreatedDateStart.HasValue)
        {
            queryFilter &= Builders<Models.Group>.Filter.Gte(x => x.CreatedDate, filter.CreatedDateStart.Value);
        }

        if (filter.CreatedDateEnd.HasValue)
        {
            queryFilter &= Builders<Models.Group>.Filter.Lte(x => x.CreatedDate, filter.CreatedDateEnd.Value);
        }

        if (filter.Genders?.Any() ?? false)
        {
            queryFilter &= Builders<Models.Group>.Filter.ElemMatch<Models.Product>(
                x => x.Items,
                Builders<Models.Product>.Filter.In(p => p.Gender, filter.Genders));
        }

        if (filter.GroupIds?.Any() ?? false)
        {
            queryFilter &= Builders<Models.Group>.Filter.In(x => x.Id, filter.GroupIds);
        }

        if (filter.GroupStatus?.Any() ?? false)
        {
            queryFilter &= Builders<Models.Group>.Filter.In(x => x.Status, filter.GroupStatus);
        }

        if (filter.HasNotes.HasValue)
        {
            queryFilter &= Builders<Models.Group>.Filter.Eq(x => x.HasNotes, filter.HasNotes.Value);
        }

        if (filter.ItemIds?.Any() ?? false)
        {
            queryFilter &= Builders<Models.Group>.Filter.ElemMatch<Models.Product>(
                x => x.Items,
                Builders<Models.Product>.Filter.In(p => p.Id, filter.ItemIds));
        }

        if (filter.ItemStatus?.Any() ?? false)
        {
            queryFilter &= Builders<Models.Group>.Filter.ElemMatch<Models.Product>(
                x => x.Items,
                Builders<Models.Product>.Filter.In(p => p.Status, filter.ItemStatus));
        }

        if (filter.Markets?.Any() ?? false)
        {
            queryFilter &= Builders<Models.Group>.Filter.ElemMatch<Models.Product>(
                x => x.Items,
                Builders<Models.Product>.Filter.In(p => p.Market, filter.Markets));
        }

        if (filter.MatchScoreMin.HasValue)
        {
            queryFilter &= Builders<Models.Group>.Filter.Gte(x => x.MatchScore, filter.MatchScoreMin.Value);
        }

        if (filter.MatchScoreMax.HasValue)
        {
            queryFilter &= Builders<Models.Group>.Filter.Lte(x => x.MatchScore, filter.MatchScoreMax.Value);
        }

        if (filter.MatchType.HasValue)
        {
            queryFilter &= Builders<Models.Group>.Filter.Eq(x => x.MatchType, filter.MatchType);
        }

        if (filter.MergeDateStart.HasValue)
        {
            queryFilter &= Builders<Models.Group>.Filter.Gte(x => x.MergeDate, filter.MergeDateStart.Value);
        }

        if (filter.MergeDateEnd.HasValue)
        {
            queryFilter &= Builders<Models.Group>.Filter.Lte(x => x.MergeDate, filter.MergeDateEnd.Value);
        }

        if (filter.ModifiedDateStart.HasValue)
        {
            queryFilter &= Builders<Models.Group>.Filter.Gte(x => x.ModifiedDate, filter.ModifiedDateStart.Value);
        }

        if (filter.ModifiedDateEnd.HasValue)
        {
            queryFilter &= Builders<Models.Group>.Filter.Lte(x => x.MergeDate, filter.ModifiedDateEnd.Value);
        }

        if (filter.MatchDateStart.HasValue)
        {
            queryFilter &= Builders<Models.Group>.Filter.Gte(x => x.MatchDate, filter.MatchDateStart.Value);
        }

        if (filter.MatchDateEnd.HasValue)
        {
            queryFilter &= Builders<Models.Group>.Filter.Lte(x => x.MatchDate, filter.MatchDateEnd.Value);
        }

        if (filter.ParentHasStock.HasValue)
        {
            queryFilter &= Builders<Models.Group>.Filter.Eq(x => x.HasParentStock, filter.ParentHasStock.Value);
        }

        if (filter.PlatformCategoryIds?.Any() ?? false)
        {
            queryFilter &= Builders<Models.Group>.Filter.ElemMatch<Models.Product>(
                x => x.Items,
                Builders<Models.Product>.Filter.ElemMatch<Models.Category>(
                    p => p.PlatformCategories,
                    Builders<Models.Category>.Filter.In(c => c.Id, filter.PlatformCategoryIds)));
        }

        if (filter.ProductStatus?.Any() ?? false)
        {
            queryFilter &= Builders<Models.Group>.Filter.ElemMatch<Models.Product>(
                x => x.Items,
                Builders<Models.Product>.Filter.In(p => p.ProductStatusId, filter.ProductStatus));
        }

        if (filter.ProductionTypes?.Any() ?? false)
        {
            queryFilter &= Builders<Models.Group>.Filter.AnyIn(x => x.ChildProductionTypeStatus, filter.ProductionTypes);
        }

        if (filter.ScrapeDateStart.HasValue)
        {
            queryFilter &= Builders<Models.Group>.Filter.ElemMatch<Models.Product>(
                x => x.Items,
                Builders<Models.Product>.Filter.Gte(p => p.ScrapeDate, filter.ScrapeDateStart));
        }

        if (filter.ScrapeDateEnd.HasValue)
        {
            queryFilter &= Builders<Models.Group>.Filter.ElemMatch<Models.Product>(
                x => x.Items,
                Builders<Models.Product>.Filter.Lte(p => p.ScrapeDate, filter.ScrapeDateEnd));
        }

        if (filter.SlotStatus?.Any() ?? false)
        {
            queryFilter &= Builders<Models.Group>.Filter.AnyIn(x => x.ChildSlotStatus, filter.SlotStatus);
        }

        // Renderizar o filtro como JSON para debug
        var serializer = BsonSerializer.SerializerRegistry.GetSerializer<Models.Group>();
        var renderArgs = new RenderArgs<Models.Group>(serializer, BsonSerializer.SerializerRegistry);

        var renderedFilter = queryFilter.Render(renderArgs);

        var queryStr = renderedFilter.ToJson();
        Console.WriteLine(queryStr);

        var options = new FindOptions<Models.Group>
        {
            Skip = (filter.Page - 1) * filter.PageSize,
            Limit = filter.PageSize,
            Sort = GetSort(filter.Sort)
        };

        var query = await _collection.FindAsync(queryFilter, options, cancellationToken);

        var results = await query.ToListAsync(cancellationToken);

        return results;
    }

    private static SortDefinition<Group>? GetSort(SortOptions? sortOptions)
    {
        return sortOptions switch
        {
            SortOptions.RelevanceDesc => Builders<Models.Group>.Sort.Descending(x => x.Relevance),
            SortOptions.CreatedDateAsc => Builders<Models.Group>.Sort.Ascending(x => x.CreatedDate),
            SortOptions.ModifiedDateAsc => Builders<Models.Group>.Sort.Ascending(x => x.ModifiedDate),
            SortOptions.MatchDateAsc => Builders<Models.Group>.Sort.Ascending(x => x.MatchDate),
            SortOptions.MatchScoreDesc => Builders<Models.Group>.Sort.Descending(x => x.MatchScore),
            _ => null
        };
    }
}
