using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.DAL.Base
{
    public interface IContext : IDisposable
    {
        DbSet<T> Repository<T>() where T : class;
        int SaveChange();
        Task<int> SaveChangeAsync();
    }
}
