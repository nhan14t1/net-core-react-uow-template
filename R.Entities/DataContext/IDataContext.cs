using System;

namespace R.Entities.DataContext
{
    public interface IDataContext : IDisposable
    {
        int SaveChanges();
    }
}
