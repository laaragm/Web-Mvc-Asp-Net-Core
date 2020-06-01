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

		//Db data accessing is a slow operation. Then, if the operation is synchronous the application is going
		//to be blocked until the task is completed.
		//Async operations are way better in these cases because the task is executed separately (asynchronously)
		//and our application continues available (and executing). 
		public async Task<List<Seller>> FindAllAsync()
		{
			return await Context.Seller.ToListAsync();
		}

		public async Task InsertAsync(Seller seller)
		{
			Context.Add(seller);
			//We put async here because this is actually the operation that does some operations in the db. 
			//The Add() operation specified above does everything in memory.
			await Context.SaveChangesAsync();
		}

		//Eager loading is the process whereby a query for one type of entity also loads related entities as
		//part of the query, so that we don't need to execute a separate query for related entities.
		//Eager loading is achieved using the Include() method.
		public async Task<Seller> FindByIdAsync(int id)
		{
			return await Context.Seller
										.Include(seller => seller.Department)
										.FirstOrDefaultAsync(seller => seller.Id == id);
		}

		public async Task RemoveAsync(int id)
		{
			try
			{
				var seller = await Context.Seller.FindAsync(id);
				Context.Seller.Remove(seller);
				await Context.SaveChangesAsync();
			}
			catch (DbUpdateException exception)
			{
				throw new IntegrityException("You can't delete the specified Seller because he/she has sales");
			}
		}

		public async Task UpdateAsync(Seller seller)
		{
			if (! await Context.Seller.AnyAsync(x => x.Id == seller.Id))
			{
				throw new NotFoundException("Seller Id not found.");
			}

			try
			{
				Context.Update(seller);
				await Context.SaveChangesAsync();
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
