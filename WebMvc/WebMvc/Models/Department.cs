using System;
using System.Collections.Generic;
using System.Linq;

namespace WebMvc.Models
{
	public class Department
	{
		public int Id { get; set; }
		public string Name { get; set; }
		//ICollection implements IEnumerable<T>, IEnumerable and the following methods: Add(T item), Clear(),
		//Remove(T item)
		public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

		public Department()
		{

		}

		public Department(int id, string name)
		{
			Id = id;
			Name = name;
		}

		public void AddSeller(Seller seller) => Sellers.Add(seller);

		//For each seller in our list we call our Total Sales method (in that specified DateTime), then we sum
		//everything in order to get the total sales for all sellers (which are in the specified department)
		public double TotalSales(DateTime initial, DateTime final) => Sellers.Sum(seller => seller.TotalSales(initial, final));
	}
}
