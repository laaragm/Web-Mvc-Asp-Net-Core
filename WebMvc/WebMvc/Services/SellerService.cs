using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Data;
using WebMvc.Models;
using Microsoft.EntityFrameworkCore;

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

		//Eager loading is the process whereby a query for one type of entity also loads related entities as
		//part of the query, so that we don't need to execute a separate query for related entities.
		//Eager loading is achieved using the Include() method.
		public Seller FindById(int id) => Context.Seller.Include(seller => seller.Department).FirstOrDefault(seller => seller.Id == id);

		public void Remove(int id)
		{
			var seller = Context.Seller.Find(id);
			Context.Seller.Remove(seller);
			Context.SaveChanges();
		}
	}
}
