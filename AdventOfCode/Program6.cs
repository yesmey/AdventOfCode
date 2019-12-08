using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Channels;

namespace AdventOfCode
{
	internal class Program6
	{
		private static void Main(string[] args)
		{
			var input = File.ReadAllLines("input6.txt")
				.Select(x => x.Split(')'))
				.ToDictionary(p => p[1], p => p[0]);

			var sum = 0;
			foreach (var node in input.Keys)
			{
				sum += GetParents(input, node).Count();
			}
			Console.WriteLine(sum);

			var youParents = GetParents(input, "YOU");
			var santaParents = GetParents(input, "SAN");
			youParents.Reverse();
			santaParents.Reverse();

			var maxLength = Math.Max(youParents.Count, santaParents.Count);
			for (var i = 0; i < maxLength; i++)
			{
				if (youParents[i] != santaParents[i])
				{
					Console.WriteLine(youParents.Count - i + santaParents.Count - i);
					break;
				}
			}
		}

		private static List<string> GetParents(Dictionary<string, string> dict, string node)
		{
			var list = new List<string>();
			var parent = node;
			while (true)
			{
				if (dict.TryGetValue(parent, out parent))
				{
					list.Add(parent);
				}
				else
				{
					break;
				}
			}
			return list;
		}
	}
}