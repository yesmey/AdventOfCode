using System;
using System.Linq;

namespace AdventOfCode
{
	internal class Program4
	{
		private static void Main4(string[] args)
		{
			var line = "108457-562041";
			var (rangeStart, rangeEnd) = line.Split('-').Select(int.Parse);

			int count = 0;

			for (var i = rangeStart; i < rangeEnd; i++)
			{
				var digitString = i.ToString();

				bool isIncreasing = true;
				bool[] hasAdjacent = new bool[6];

				for (var index = 0; index < digitString.Length; index++)
				{
					var prevDigit = index > 0 ? digitString[index - 1] : default;
					var digit = digitString[index];

					if (prevDigit > digit)
					{
						isIncreasing = false;
					}
					else if (prevDigit == digit)
					{
						var prevPrevDigit = index > 1 ? digitString[index - 2] : default;

						if (prevPrevDigit != digit)
							hasAdjacent[index - 1] = true;
						else
							hasAdjacent[index - 2] = false;
					}
				}

				if (hasAdjacent.Any(x => x) && isIncreasing)
				{
					count++;
				}
			}

			Console.WriteLine(count);
		}
	}
}