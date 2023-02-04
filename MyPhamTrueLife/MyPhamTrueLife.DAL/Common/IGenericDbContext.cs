using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.DAL.Common
{
    public interface IGenericDbContext<T>
    {
		DatabaseFacade Database { get; }
		DbSet<T> Repository<T>() where T : class;
		int SaveChanges();
		Task<int> SaveChangesAsync();
		void Dispose();
		T GetContext();
	}
}
