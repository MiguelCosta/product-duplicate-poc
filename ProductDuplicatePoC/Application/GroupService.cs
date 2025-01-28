namespace ProductDuplicatePoC.Application;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using ProductDuplicatePoC.Data;
using ProductDuplicatePoC.Dtos;

public class GroupService
{
    private readonly GroupRepository _groupRepository;

    public GroupService(Data.GroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<List<Dtos.Group>> GetFromMongoAsync(Dtos.GroupFilter filter, CancellationToken cancellationToken)
    {
        var modelFilter = filter.ToModel();

        var groups = await _groupRepository.GetAllAsync(modelFilter, cancellationToken);

        return groups.ToDto();
    }

    public async Task GenerateGroupsAsync(int totalGroups)
    {
        var groupIds = 1;
        var itemIds = 1000000;
        var user1 = new Guid("7F9AD521-2E5C-4FD9-B6D8-1FD4507775A5");
        var user2 = new Guid("EB3B5A8F-C00C-4F65-90A7-AA5B1A392F0A");
        var user3 = new Guid("a7b3c509-325c-4cb5-87e0-8ab320b122e4");
        var platformCategories = new List<Dtos.Category>
        {
            new Dtos.Category { Id = new Guid("5cb3ab9f-fda7-449a-b855-0153a862d812"), Name = "Clothing", Order = 1 },
            new Dtos.Category { Id = new Guid("c847aedb-8a9e-4d81-8ba1-0153a863115b"), Name = "Jackets", Order = 2 },
            new Dtos.Category { Id = new Guid("af3eeaf6-a3af-4d83-9621-0153a8633ed3"), Name = "Biker Jackets", Order = 3 },
            new Dtos.Category { Id = new Guid("5cb3ab9f-fda7-449a-b855-0153a862d812"), Name = "Clothing", Order = 1 },
            new Dtos.Category { Id = new Guid("f3e7e61c-9122-4bc6-a9a8-0153a86311c8"), Name = "Dresses", Order = 2 },
            new Dtos.Category { Id = new Guid("3309f494-2966-4ae3-8d17-0153a8633886"), Name = "Day Dresses", Order = 3 },
        };

        var webCategories = new List<Dtos.Category>
        {
            new Dtos.Category { Id = new Guid("5cb3ab9f-fda7-449a-b855-0153a862d812"), Name = "Clothing Web", Order = 1 },
            new Dtos.Category { Id = new Guid("c847aedb-8a9e-4d81-8ba1-0153a863115b"), Name = "Jackets Web", Order = 2 },
            new Dtos.Category { Id = new Guid("af3eeaf6-a3af-4d83-9621-0153a8633ed3"), Name = "Biker Jackets Web", Order = 3 },
            new Dtos.Category { Id = new Guid("5cb3ab9f-fda7-449a-b855-0153a862d812"), Name = "Clothing Web", Order = 1 },
            new Dtos.Category { Id = new Guid("f3e7e61c-9122-4bc6-a9a8-0153a86311c8"), Name = "Dresses Web", Order = 2 },
            new Dtos.Category { Id = new Guid("3309f494-2966-4ae3-8d17-0153a8633886"), Name = "Day Dresses Web", Order = 3 },
        };

        var sizeScales = new List<Guid>
        {
            new ("8638e357-9037-48ea-961f-2b4bb63563e6"),
            new ("eb786315-0d30-4e88-b369-2ea5b5542398"),
            new ("cef9ecbd-ad9d-4d68-b3a4-b3ff232e004d"),
            new ("4691282e-35a4-4795-bea4-a4dea3468642"),
            new ("762bcea6-bb66-41bc-ab11-83ac8f05d3bd"),
            new ("d689ea7d-da87-471c-ba7b-7a7d6f21dac6"),
            new ("0527f270-102f-4a7c-86c1-0589f4aa9b9e"),
            new ("8a71b9f0-78ad-4aca-8ad4-aea113be66a6"),
        };

        var productStatusIds = new List<Guid>
        {
            new ("1e7eb8bd-5d9f-4030-b547-4b90a06bf47e"),
            new ("957f0e38-0233-4fbf-9852-d28de45fc2d5"),
            new ("e605d9bb-2236-461c-a817-61564c0a9ad6"),
            new ("92696dad-2d31-44bd-bdab-1865d885d6f1"),
            new ("2699e640-7497-4a24-a2d4-003fd26cf28d"),
            new ("2801563f-bb17-4b48-bfc6-8df177c6983e")
        };

        var listOfMarkets = new List<string> { "UK", "US", "FR", "DE", "IT", "ES", "JP", "CN", "PT" };

        var faker = new Faker<Dtos.Group>()
            .RuleFor(g => g.Id, f => groupIds++)
            .RuleFor(g => g.Type, f => f.PickRandom<Dtos.GroupType>())
            .RuleFor(g => g.MatchType, f => f.PickRandom<Dtos.MatchType>())
            .RuleFor(g => g.MatchScore, f => f.Random.Decimal(0, 100))
            .RuleFor(g => g.CreatedDate, f => f.Date.Past(1))
            .RuleFor(g => g.ModifiedDate, f => f.Date.Recent(1))
            .RuleFor(g => g.AssigneeUserId, f => f.PickRandom(user1, user2, user3))
            .RuleFor(g => g.Status, f => f.PickRandom<Dtos.GroupStatus>())
            .RuleFor(g => g.Note, f => f.Lorem.Sentence().OrNull(f))
            .RuleFor(g => g.Items, f => f.Make(2, () => new Dtos.Product
            {
                Id = (itemIds++).ToString(),
                Catalog = f.PickRandom("Farfetch", "Competitor"),
                CatalogType = f.PickRandom<Dtos.CatalogType>(),
                Name = f.Commerce.ProductName(),
                Description = f.Lorem.Sentence().OrNull(f),
                Status = f.PickRandom<Dtos.Status>(),
                Price = decimal.Parse(f.Commerce.Price(10, 10000)),
                Currency = "EUR",
                BrandProductId = f.Random.AlphaNumeric(10).OrNull(f),
                MainColourId = Guid.NewGuid().OrNull(f),
                MainColourName = f.Commerce.Color().OrNull(f),
                BrandId = Guid.NewGuid(),
                BrandName = f.Company.CompanyName(),
                Gender = f.PickRandom("Male", "Female", "Unisex").ToString(),
                PlatformCategories = f.Make(3, index =>
                {
                    var category = f.PickRandom(platformCategories.Where(x => x.Order == index));
                    return category;
                }).ToList().OrNull(f),
                WebCategories = f.Make(3, index =>
                {
                    var category = f.PickRandom(webCategories.Where(x => x.Order == index));
                    return category;
                }).ToList().OrNull(f),
                Compositions = f.Make(3, () => f.Commerce.ProductName()).ToList().OrNull(f),
                Measurements = f.Commerce.ProductName().OrNull(f),
                SizeScaleId = f.PickRandom(sizeScales).OrNull(f),
                SizeScaleName = f.Commerce.Color().OrNull(f),
                ProductStatusId = f.PickRandom(productStatusIds),
                ProductionType = f.PickRandom<Dtos.ProductionType>().OrNull(f),
                Stock = f.Random.Int(0, 100).OrNull(f),
                DigitalAssets = f.Make(2, () => new Dtos.DigitalAsset
                {
                    Id = f.Random.Int(),
                    Url = f.Image.PicsumUrl(),
                    Order = f.Random.Int(1, 10)
                }).ToList(),
                ScrapeDate = f.Date.Past(1),
                ScoreGBFC = f.Random.Int(0, 100),
                Relevance = f.Random.Int(0, 100),
                MerchantCodes = [f.Random.Int(0, 100)],
                Market = f.PickRandom(listOfMarkets),
                SlotStatus = f.PickRandom<Dtos.SlotStatus>(),
            }))
            .FinishWith((f, g) =>
            {
                g.MatchScore = g.MatchType == Dtos.MatchType.Automatic ? f.Random.Decimal(0, 100) : null;

                if (g.Items.Any(x => x.CatalogType == Dtos.CatalogType.Competitor))
                {
                    g.Type = GroupType.FarfetchCompetitor;
                }

                foreach (var item in g.Items)
                {
                    item.Catalog = item.CatalogType == CatalogType.Farfetch
                        ? "Farfetch"
                        : f.PickRandom("cettire", "luisaviaroma", "mytheresa", "ssense", "mrporter", "netaporter", "netaporterfengmao");
                }
            });

        var groups = faker.Generate(totalGroups);

        var groupsModels = groups.ToModel();

        await _groupRepository.InsertManyAsync(groupsModels);
    }
}
