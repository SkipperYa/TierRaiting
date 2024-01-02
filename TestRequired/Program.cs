using System;
using System.ComponentModel.DataAnnotations;

namespace TestRequired
{
	internal class MyClass
	{
		public required string Field { get; set; }
	}

	internal class Program
	{
		static void Main(string[] args)
		{
			var myClass = new MyClass() { Field = "Required Field" };
			Console.WriteLine(myClass.Field);
			Console.ReadKey();
		}
	}
}
