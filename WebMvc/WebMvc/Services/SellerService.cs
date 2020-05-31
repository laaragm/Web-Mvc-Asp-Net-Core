using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Data;
using WebMvc.Models;
using Microsoft.EntityFrameworkCore;
using WebMvc.Services.Exceptions;

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

		public void Update(Seller seller)
		{
			if (!Context.Seller.Any(x => x.Id == seller.Id))
			{
				throw new NotFoundException("Seller Id not found.");
			}

			try
			{
				Context.Update(seller);
				Context.SaveChanges();
			}
			//Layer Segregation
			//This is important because our service layer cannot propagate an exception related to data access.
			//If an exception occurs, our service layer is going to throw an exception from its own layer
			//and our controller needs to deal only with exceptions related to the service layer.
			//This is a way that we can respect the MVC architecture.
			catch (DbUpdateConcurrencyException exception)
			{
				throw new DbConcurrencyException(exception.Message);
			}
		}
	}
}
