using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace AdventOfCode
{
	class Program1
	{
		static void Main(string[] args)
		{
			var input = File.ReadAllLines("input.txt").Select(double.Parse).ToArray();
			var sum = 0L;
			foreach (var mass in input)
			{
				long a = (long) Math.Floor((double) mass / 3) - 2;
				while (a > 0)
				{
					sum += a;
					a = (long) Math.Floor((double) a / 3) - 2;
				}
			}
			Console.WriteLine(sum);
		}
	}
}
