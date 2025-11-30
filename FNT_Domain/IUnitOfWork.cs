namespace FNT_Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IMenuRepository Menu { get; }
        IDbTransaction BeginTransaction();
    }
}
