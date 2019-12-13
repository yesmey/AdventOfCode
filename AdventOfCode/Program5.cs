using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
	internal class Program5
	{
		public static void Main7(string[] args)
		{
			var input = File.ReadAllText("input5.txt").Split(',').Select(int.Parse).ToArray();
			var intComputer = new IntComputer(input);
			intComputer.Parse();
		}
	}
}