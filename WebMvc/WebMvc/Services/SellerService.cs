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

		public void Insert(Seller seller)
		{
			Context.Add(seller);
			Context.SaveChanges();
		}

		public Seller FindById(int id) => Context.Seller.FirstOrDefault(seller => seller.Id == id);

		public void Remove(int id)
		{
			var seller = Context.Seller.Find(id);
			Context.Seller.Remove(seller);
			Context.SaveChanges();
		}
	}
}
