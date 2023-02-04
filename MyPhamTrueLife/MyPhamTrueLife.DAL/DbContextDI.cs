using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyPhamTrueLife.DAL.Base;
using MyPhamTrueLife.DAL.Common;
using MyPhamTrueLife.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPhamTrueLife.DAL
{
    public static class DbContextDI
    {
		public static IServiceCollection ConfigureDbContext(this IServiceCollection services, string connectString)
		{
			services.AddScoped<IContext, dbDevNewContext>();
			services.AddScoped(typeof(IGenericDbContext<>), typeof(GenericDbContext<>));
			services.AddDbContext<dbDevNewContext>(options => options.UseSqlServer(connectString, o => o.CommandTimeout(180)));
			return services;
		}
	}
}
