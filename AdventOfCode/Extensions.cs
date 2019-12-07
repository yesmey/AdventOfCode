using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
	public static class Extensions
	{
		// ReSharper disable PossibleMultipleEnumeration
		public static void Deconstruct<T>(this IEnumerable<T> list, out T first)
		{
			first = list.First();
		}

		public static void Deconstruct<T>(this IEnumerable<T> list, out T first, out T second)
		{
			var array = list.ToArray();
			first = array[0];
			second = array[1];
		}

		public static void Deconstruct<T>(this IEnumerable<T> list, out T first, out T second, out T third)
		{
			var array = list.ToArray();
			first = array[0];
			second = array[1];
			third = array[2];
		}


		// ReSharper enable PossibleMultipleEnumeration
	}
}
