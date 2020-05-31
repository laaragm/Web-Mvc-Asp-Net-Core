﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebMvc.Models
{
	public class Seller
	{
		public int Id { get; set; }
		public string Name { get; set; }

		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		//In order to set a name which is going to be shown in the Web App according to the property below
		[Display(Name = "Birth Date")]
		//In order to display only the date (not the hours)
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyy}")]
		public DateTime BirthDate { get; set; }

		[Display(Name = "Base Salary")]
		[DisplayFormat(DataFormatString = "{0:F2}")]
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
