
namespace FNT_Persistence.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SecurityContext _context;
        private readonly IDbConnection dbConnection;
              
        public IMenuRepository Menu { get; }        

        public UnitOfWork(SecurityContext _context,                      
                      IMenuRepository menuRepository 
                      )
        {
            this._context = _context ?? throw new ArgumentNullException(nameof(_context));
            dbConnection = _context.Database.GetDbConnection();

            Menu = menuRepository;            
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        public IDbTransaction BeginTransaction()
        {
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }

            return dbConnection.BeginTransaction();
        }
    }
}
