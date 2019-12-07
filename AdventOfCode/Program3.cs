using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
	internal class Program3
	{
		private static void Main3(string[] args)
		{
			var file = File.ReadAllText("input3.txt");
			var (firstWire, secondWire) = file.Split('\n');
		
			var input1 = firstWire.Split(',');
			var input2 = secondWire.Split(',');

			var positions = RunWire(input1);
			var positions2 = RunWire(input2);

			Console.WriteLine(GetMinimumDistance(positions, positions2));
		}

		public static int GetMinimumDistance(List<Line> source, List<Line> target)
		{
			var i = 0;
			var j = 0;
			var minimumDistance = int.MaxValue;

			while (i < source.Count && j < target.Count)
			{
				switch (source[i].CompareTo(target[j]))
				{
					case -1:
						i++;
						continue;
					case 1:
						j++;
						continue;
					default:
						minimumDistance = Math.Min(minimumDistance, source[i].Steps + target[j].Steps);
						j++;
						continue;
				}
			}

			return minimumDistance;
		}

		private static List<Line> RunWire(string[] input)
		{
			var posX = 0;
			var posY = 0;
			var steps = 0;

			var positions = new List<Line>();

			foreach (var str in input)
			{
				var direction = str[0];
				var number = int.Parse(str.AsSpan().Slice(1));

				switch (direction)
				{
					case 'U':
					{
						for (var i = 0; i < number; i++)
							positions.Add(new Line(posX, ++posY, ++steps));

						break;
					}
					case 'D':
					{
						for (var i = 0; i < number; i++)
							positions.Add(new Line(posX, --posY, ++steps));

						break;
					}
					case 'R':
					{
						for (var i = 0; i < number; i++)
							positions.Add(new Line(++posX, posY, ++steps));

						break;
					}
					case 'L':
					{
						for (var i = 0; i < number; i++)
							positions.Add(new Line(--posX, posY, ++steps));

						break;
					}
					default:
						throw new Exception();
				}
			}

			return positions.OrderBy(x => x).ToList();
		}

		public readonly struct Line : IComparable<Line>
		{
			public readonly int X;
			public readonly int Y;
			public readonly int Steps;

			public Line(int x, int y, int steps)
			{
				X = x;
				Y = y;
				Steps = steps;
			}

			public int CompareTo(Line other)
			{
				var xComparison = X.CompareTo(other.X);
				if (xComparison != 0) return xComparison;
				return Y.CompareTo(other.Y);
			}
		}
	}
}