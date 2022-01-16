using SimpleWebApp.WebApi.Data;
using SimpleWebApp.WebApi.Models;

namespace SimpleWebApp.WebApi.Repositories;

public sealed class ItemRepository : Repository<Item>, IItemRepository
{
    public ItemRepository(AppDbContext context) :
        base(context, context.Items)
    { }
}
