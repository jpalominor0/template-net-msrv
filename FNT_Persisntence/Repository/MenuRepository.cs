namespace FNT_Persistence.Repository
{
    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
        public MenuRepository(SecurityContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Menu>> GetAllActive()
        {
            return await _dbSet.Where(a => a.Active == Constants.Active).ToListAsync(); 
        }

        public async Task<Menu> GetById(string id)
        {
            return await _dbSet.Where(a => a.MenuId == id).FirstOrDefaultAsync();
        }
        

        public async Task<Menu> UpdateMenu(Menu data)
        {
            var old = await _dbSet.Where(a => a.MenuId == data.MenuId).FirstOrDefaultAsync();

            if (old != null)
            {
                data.CreatedUser = old.CreatedUser;
                data.CreatedDate = old.CreatedDate;
                data.ModifiedDate = DateTime.Now;

                _context.Update(data);
                _context.SaveChanges();

                return data;
            }
            else 
            {
                return new Menu();
            }
        }
    }
}
