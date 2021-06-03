using Microsoft.EntityFrameworkCore;
using R.Entities.Entities;
using System;

namespace R.Entities.DataContext
{
    public class DbFactory : IDisposable
    {
        private bool _disposed;
        private Func<ReactTemplateContext> _instanceFunc;
        private DbContext _dbContext;
        public DbContext DbContext => _dbContext ?? (_dbContext = _instanceFunc.Invoke());

        public DbFactory(Func<ReactTemplateContext> dbContextFactory)
        {
            _instanceFunc = dbContextFactory;
        }

        public void Dispose()
        {
            if (!_disposed && _dbContext != null)
            {
                _disposed = true;
                _dbContext.Dispose();
            }
        }
    }
}
