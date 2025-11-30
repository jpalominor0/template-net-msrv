
namespace FNT_Domain.MenuAggregates.Interface
{
    public interface IMenuRepository : IRepository<Menu>
    {
        Task<IEnumerable<Menu>> GetAllActive();
        Task<Menu> UpdateMenu(Menu data);
        Task<Menu> GetById(string id);        
    }
}
