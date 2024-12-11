namespace ProductDuplicatePoC.Data;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
}
