using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Data;
using WebMvc.Models;

namespace WebMvc.Services
{
	public class SellerService
	{
		private readonly WebMvcContext Context;

		//DI
		public SellerService(WebMvcContext context)
		{
			Context = context;
		}

		public List<Seller> FindAll() => Context.Seller.ToList();
	}
}
