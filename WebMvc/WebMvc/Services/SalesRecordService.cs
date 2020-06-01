using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Data;
using WebMvc.Models;

namespace WebMvc.Services
{
	public class SalesRecordService
	{
		private readonly WebMvcContext Context;

		//DI
		public SalesRecordService(WebMvcContext context)
		{
			Context = context;
		}

		public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
		{
			var result = from sale in Context.SalesRecord select sale;

			if (minDate.HasValue)
			{
				result = result.Where(x => x.Date >= minDate.Value);
			}

			if (maxDate.HasValue)
			{
				result = result.Where(x => x.Date <= maxDate.Value);
			}

			return await result
						//Db Join: sales on seller
						.Include(x => x.Seller)
						//Db Join: sales on department
						.Include(x => x.Seller.Department)
						.OrderByDescending(x => x.Date)
						.ToListAsync();
		}

		public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
		{
			var result = from sale in Context.SalesRecord select sale;

			if (minDate.HasValue)
			{
				result = result.Where(x => x.Date >= minDate.Value);
			}

			if (maxDate.HasValue)
			{
				result = result.Where(x => x.Date <= maxDate.Value);
			}

			return await result
						.Include(x => x.Seller)
						.Include(x => x.Seller.Department)
						.OrderByDescending(x => x.Date)
						.GroupBy(x => x.Seller.Department)
						.ToListAsync();
		}
	}
}
