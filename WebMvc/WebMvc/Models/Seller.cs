using System;
using System.Linq;
using System.Collections.Generic;

namespace WebMvc.Models
{
	public class Seller
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public DateTime BirthDate { get; set; }
		public double BaseSalary { get; set; }
		public Department Department { get; set; }
		//In order not to let the foreign key be null
		public int DepartmentId { get; set; } 
		public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

		public Seller()
		{

		}

		public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
		{
			Id = id;
			Name = name;
			Email = email;
			BirthDate = birthDate;
			BaseSalary = baseSalary;
			Department = department;
		}

		//Expression body for methods
		public void AddSale(SalesRecord salesRecord) => Sales.Add(salesRecord);

		public void RemoveSale(SalesRecord salesRecord) => Sales.Remove(salesRecord);
		
		public double TotalSales(DateTime initial, DateTime final)
		{
			return Sales
						//Filter data
						.Where(salesRecord => salesRecord.Date >= initial && salesRecord.Date <= final)
						//Sum amount according to the filtered data
						.Sum(salesRecord => salesRecord.Amount);
		}
	}
}
