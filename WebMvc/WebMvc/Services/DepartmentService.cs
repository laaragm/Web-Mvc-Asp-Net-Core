using System;
using System.Collections.Generic;
using System.Linq;
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

		public List<Department> FindAll()
		{
			//Returns all Departments ordered by name.
			return Context.Department.OrderBy(x => x.Name).ToList();
		}
	}
}
