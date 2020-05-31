using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models;
using WebMvc.Models.Enumerations;

namespace WebMvc.Data
{
	public class SeedingService
	{
		//Implementing the service (registering a SeedingService dependency with our DbContext).
		private WebMvcContext Context;

		//Dependency Injection Using Constructor.
		public SeedingService(WebMvcContext context)
		{
			Context = context;
		}

		//This method is responsible for populating our Database.
		public void Seed()
		{
			if (Context.Department.Any() || Context.Seller.Any() || Context.SalesRecord.Any())
			{
				return; //DB has been already seeded.
			}

			//Workflow: Code first targets a database that doesn't exist and Code First will create it. 
			//It can also be used if you have an empty database and then Code First will add new tables to it.
			Department department = new Department(1, "Computers");
			Department department2 = new Department(2, "Books");
			Department department3 = new Department(3, "Music");
			Department department4 = new Department(4, "Cell Phones");

			Seller seller = new Seller(1, "Lara G.", "lara@gmail.com", new DateTime(2000, 05, 09), 5000.00, department);
			Seller seller2 = new Seller(2, "Duda A.", "duda@gmail.com", new DateTime(2003, 04, 14), 5000.00, department2);
			Seller seller3 = new Seller(3, "Jord V.", "jord@gmail.com", new DateTime(1997, 03, 01), 5000.00, department);
			Seller seller4 = new Seller(4, "Gustavo N.", "gustavo@gmail.com", new DateTime(2000, 09, 06), 5000.00, department3);
			Seller seller5 = new Seller(5, "Lorena G.", "lorena@gmail.com", new DateTime(1999, 06, 09), 5000.00, department4);

			SalesRecord salesRecord = new SalesRecord(1, new DateTime(2020, 05, 31), 2000.00, SaleStatus.Billed, seller);
			SalesRecord salesRecord2 = new SalesRecord(2, new DateTime(2020, 05, 30), 2000.00, SaleStatus.Billed, seller2);
			SalesRecord salesRecord3 = new SalesRecord(3, new DateTime(2020, 05, 30), 3000.00, SaleStatus.Billed, seller2);
			SalesRecord salesRecord4 = new SalesRecord(4, new DateTime(2020, 05, 30), 4000.00, SaleStatus.Billed, seller2);
			SalesRecord salesRecord5 = new SalesRecord(5, new DateTime(2020, 05, 30), 5000.00, SaleStatus.Billed, seller);
			SalesRecord salesRecord6 = new SalesRecord(6, new DateTime(2020, 05, 30), 5000.00, SaleStatus.Billed, seller);
			SalesRecord salesRecord7 = new SalesRecord(7, new DateTime(2020, 05, 30), 5000.00, SaleStatus.Billed, seller3);
			SalesRecord salesRecord8 = new SalesRecord(8, new DateTime(2020, 05, 30), 5000.00, SaleStatus.Billed, seller3);
			SalesRecord salesRecord9 = new SalesRecord(9, new DateTime(2020, 05, 30), 5000.00, SaleStatus.Billed, seller3);
			SalesRecord salesRecord10 = new SalesRecord(10, new DateTime(2020, 05, 30), 5000.00, SaleStatus.Billed, seller4);
			SalesRecord salesRecord11 = new SalesRecord(11, new DateTime(2020, 05, 30), 25000.00, SaleStatus.Billed, seller4);
			SalesRecord salesRecord12 = new SalesRecord(12, new DateTime(2020, 05, 9), 25000.00, SaleStatus.Billed, seller4);
			SalesRecord salesRecord13 = new SalesRecord(13, new DateTime(2020, 05, 29), 5000.00, SaleStatus.Billed, seller4);
			SalesRecord salesRecord14 = new SalesRecord(14, new DateTime(2020, 05, 22), 5000.00, SaleStatus.Billed, seller4);
			SalesRecord salesRecord15 = new SalesRecord(15, new DateTime(2020, 05, 21), 5000.00, SaleStatus.Billed, seller);
			SalesRecord salesRecord16 = new SalesRecord(16, new DateTime(2020, 05, 4), 1000.00, SaleStatus.Billed, seller);
			SalesRecord salesRecord17 = new SalesRecord(17, new DateTime(2020, 05, 30), 12000.00, SaleStatus.Billed, seller3);
			SalesRecord salesRecord18 = new SalesRecord(18, new DateTime(2020, 05, 31), 12000.00, SaleStatus.Billed, seller2);
			SalesRecord salesRecord19 = new SalesRecord(19, new DateTime(2020, 05, 31), 200.00, SaleStatus.Billed, seller2);
			SalesRecord salesRecord20 = new SalesRecord(20, new DateTime(2020, 05, 31), 5000.00, SaleStatus.Billed, seller);
			SalesRecord salesRecord21 = new SalesRecord(21, new DateTime(2020, 05, 31), 100.00, SaleStatus.Billed, seller3);
			SalesRecord salesRecord22 = new SalesRecord(22, new DateTime(2020, 05, 12), 200.00, SaleStatus.Billed, seller2);
			SalesRecord salesRecord23 = new SalesRecord(23, new DateTime(2020, 05, 23), 300.00, SaleStatus.Canceled, seller5);
			SalesRecord salesRecord24 = new SalesRecord(24, new DateTime(2020, 05, 31), 20700.00, SaleStatus.Billed, seller);
			SalesRecord salesRecord25 = new SalesRecord(25, new DateTime(2020, 05, 13), 350.00, SaleStatus.Pending, seller5);

			//Add data into DB
			Context.Department.AddRange(department, department2, department3, department4);
			Context.Seller.AddRange(seller, seller2, seller3, seller4, seller5);
			Context.SalesRecord.AddRange(salesRecord, salesRecord2, salesRecord3, salesRecord4, salesRecord5,
										salesRecord6, salesRecord7, salesRecord8, salesRecord9, salesRecord10,
										salesRecord11, salesRecord12, salesRecord13, salesRecord14, salesRecord15,
										salesRecord16, salesRecord17, salesRecord18, salesRecord19, salesRecord20,
										salesRecord21, salesRecord22, salesRecord23, salesRecord24,	salesRecord25);

			Context.SaveChanges();
		}
	}
}
