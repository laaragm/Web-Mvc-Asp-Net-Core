using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebMvc.Data;
using WebMvc.Models;

namespace WebMvc.Services
{
	public class DepartmentService
	{
		private readonly WebMvcContext Context;

		//DI
		public DepartmentService(WebMvcContext context)
		{
			Context = context;
		}

		//Db data accessing is a slow operation. Then, if the operation is synchronous the application is going
		//to be blocked until the task is completed.
		//Async operations are way better in these cases because the task is executed separately (asynchronously)
		//and our application continues available (and executing). 
		public async Task<List<Department>> FindAllAsync()
		{
			//Returns all Departments ordered by name.
			return await Context.Department.OrderBy(x => x.Name).ToListAsync();
		}
	}
}
