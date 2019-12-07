using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
	internal class Program1
	{
		private static void Main1(string[] args)
		{
			var input = File.ReadAllLines("input.txt").Select(double.Parse).ToArray();
			var sum = 0.0;
			foreach (var mass in input)
			{
				var a = Math.Floor(mass / 3) - 2;
				while (a > 0.0)
				{
					sum += a;
					a = Math.Floor(a / 3) - 2;
				}
			}

			Console.WriteLine((long)sum);
		}
	}
}