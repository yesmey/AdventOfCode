using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode
{
	class Program2
	{
		static void Main2(string[] args)
		{
			var input = File.ReadAllText("input2.txt").Split(',').Select(int.Parse).ToArray();

			Parallel.For(0, 99, (noun, loopState) =>
			{
				for (var verb = 0; verb <= 99; verb++)
				{
					var memory = new int[input.Length + 3];

					Array.Copy(input, memory, input.Length);
					memory[1] = noun;
					memory[2] = verb;

					int position = 0;
					while (true)
					{
						var opcode = memory[position];
						var input1Position = memory[position + 1];
						var input2Position = memory[position + 2];
						var outputPosition = memory[position + 3];

						position += 4;

						if (opcode == 1)
						{
							memory[outputPosition] = memory[input1Position] + memory[input2Position];
						}
						else if (opcode == 2)
						{
							memory[outputPosition] = memory[input1Position] * memory[input2Position];
						}
						else if (opcode == 99)
						{
							break;
						}
					}

					if (memory[0] == 19690720)
					{
						loopState.Stop();
						Console.WriteLine(100 * noun + verb);
						return;
					}
				}
			});
		}
	}
}
