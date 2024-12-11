namespace ProductDuplicatePoC.Application;

using System;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using ProductDuplicatePoC.Data;
using ProductDuplicatePoC.Models;

public class GroupService
{
    private readonly GroupRepository _groupRepository;

    public GroupService(Data.GroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task GenerateGroupsAsync(int totalGroups)
    {
        var user1 = new Guid("7F9AD521-2E5C-4FD9-B6D8-1FD4507775A5");
        var user2 = new Guid("EB3B5A8F-C00C-4F65-90A7-AA5B1A392F0A");
        var faker = new Faker<Group>()
            .RuleFor(g => g.Id, f => f.IndexFaker + 1)
            .RuleFor(g => g.MatchType, f => f.PickRandom<MatchType>())
            .RuleFor(g => g.MatchScore, f => f.Random.Decimal(0, 100))
            .RuleFor(g => g.CreatedDate, f => f.Date.Past(1))
            .RuleFor(g => g.ModifiedDate, f => f.Date.Recent(1))
            .RuleFor(g => g.AssigneeUserId, f => f.PickRandom(user1, user2))
            .RuleFor(g => g.Status, f => f.PickRandom<GroupStatus>())
            .RuleFor(g => g.Note, f => f.Lorem.Sentence())
            .RuleFor(g => g.Products, f => f.Make(2, index => new Product
            {
                Id = f.IndexFaker + index + 1,
                Catalog = f.PickRandom("Farfetch", "Competitor"),
                CatalogType = f.PickRandom<CatalogType>(),
                Name = f.Commerce.ProductName(),
                Description = f.Lorem.Sentence(),
                Price = decimal.Parse(f.Commerce.Price(10, 10000)),
                Currency = "USD",
                BrandProductId = f.Random.AlphaNumeric(10),
                MainColourId = Guid.NewGuid(),
                MainColourName = f.Commerce.Color(),
                BrandId = Guid.NewGuid(),
                BrandName = f.Company.CompanyName(),
                Gender = f.PickRandom("Male", "Female"),
                PlatformCategories = f.Make(3, () => new Category
                {
                    Id = Guid.NewGuid(),
                    Name = f.Commerce.Department(),
                    Order = f.Random.Int(1, 5)
                }).ToList(),
                WebCategories = f.Make(2, () => new Category
                {
                    Id = Guid.NewGuid(),
                    Name = f.Commerce.Department(),
                    Order = f.Random.Int(1, 5)
                }).ToList(),
                Compositions = f.Make(3, () => f.Commerce.ProductName()).ToList(),
                Measurements = f.Commerce.ProductName(),
                SizeScaleId = Guid.NewGuid(),
                SizeScaleName = f.Commerce.Color(),
                ProductStatus = f.PickRandom<ProductStatus>(),
                ProductionType = f.PickRandom<ProductionType>(),
                Stock = f.Random.Int(0, 100),
                DigitalAssets = f.Make(2, () => new DigitalAsset
                {
                    Id = f.Random.Int(),
                    Url = f.Image.PicsumUrl(),
                    Order = f.Random.Int(1, 10)
                }).ToList(),
                ScrapeDate = f.Date.Past(1),
                ScoreGBFC = f.Random.Int(0, 100),
                Relevance = f.Random.Int(0, 100)
            }))
            .FinishWith((f, g) => { g.MatchScore = g.MatchType == MatchType.Automatic ? f.Random.Decimal(0, 100) : null; });

        var groups = faker.Generate(totalGroups);

        await _groupRepository.InsertManyAsync(groups);
    }
}
