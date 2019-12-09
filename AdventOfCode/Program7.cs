using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
	internal class Program7
	{
		private static void Main(string[] args)
		{
			var input = File.ReadAllText("input7.txt").Split(',').Select(int.Parse).ToArray();

			Console.WriteLine();
		}
	}
}