using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
	internal class Program5
	{
		private static void Main5(string[] args)
		{
			var input = File.ReadAllText("input5.txt").Split(',').Select(int.Parse).ToArray();

			var memory = new int[input.Length];
			Array.Copy(input, memory, input.Length);

			var position = 0;

			int ReadValue(bool immediateMode = false)
			{
				if (immediateMode)
				{
					return memory[position++];
				}
				else
				{
					return memory[memory[position++]];
				}
			}

			void StoreValue(int value, bool immediateMode = false)
			{
				if (immediateMode)
				{
					memory[position++] = value;
				}
				else
				{
					memory[memory[position++]] = value;
				}
			}

			while (true)
			{
				var opcode = memory[position++];

				if (opcode == 1)
				{
					var input1 = ReadValue();
					var input2 = ReadValue();
					StoreValue(input1 + input2);
				}
				else if (opcode == 2)
				{
					var input1 = ReadValue();
					var input2 = ReadValue();
					StoreValue(input1 * input2);
				}
				else if (opcode == 3)
				{
					Console.WriteLine("ENTER VALUE");
					var input1Position = memory[position++];
					var inputValue = int.Parse(Console.ReadLine());
					memory[input1Position] = inputValue;
				}
				else if (opcode == 4)
				{
					Console.WriteLine(ReadValue());
				}
				else if (opcode == 5)
				{
					if (ReadValue() != 0)
					{
						position = ReadValue();
					}
					else
					{
						position++;
					}
				}
				else if (opcode == 6)
				{
					if (ReadValue() == 0)
					{
						position = ReadValue();
					}
					else
					{
						position++;
					}
				}
				else if (opcode == 7)
				{
					var input1 = ReadValue();
					var input2 = ReadValue();
					StoreValue(input1 < input2 ? 1 : 0);
				}
				else if (opcode == 8)
				{
					var input1 = ReadValue();
					var input2 = ReadValue();
					StoreValue(input1 == input2 ? 1 : 0);
				}
				else if (opcode == 99)
				{
					break;
				}
				else if (opcode > 100)
				{
					var opcodeString = opcode.ToString().PadLeft(5, '0');
					var parameter3Mode = opcodeString[0] == '1';
					var parameter2Mode = opcodeString[1] == '1';
					var parameter1Mode = opcodeString[2] == '1';
					var opcode2 = int.Parse($"{opcodeString[3]}{opcodeString[4]}");

					if (opcode2 == 1)
					{
						var parameter1 = ReadValue(parameter1Mode);
						var parameter2 = ReadValue(parameter2Mode);
						StoreValue(parameter1 + parameter2, parameter3Mode);
					}
					else if (opcode2 == 2)
					{
						var parameter1 = ReadValue(parameter1Mode);
						var parameter2 = ReadValue(parameter2Mode);
						StoreValue(parameter1 * parameter2, parameter3Mode);
					}
					else if (opcode2 == 3)
					{
						var inputValue = int.Parse(Console.ReadLine());
						StoreValue(inputValue, parameter1Mode);
					}
					else if (opcode2 == 4)
					{
						var parameter1 = ReadValue(parameter2Mode);
						Console.WriteLine(parameter1);
					}
					else if (opcode2 == 5)
					{
						if (ReadValue(parameter1Mode) != 0)
						{
							position = ReadValue(parameter2Mode);
						}
						else
						{
							position++;
						}
					}
					else if (opcode2 == 6)
					{
						if (ReadValue(parameter1Mode) == 0)
						{
							position = ReadValue(parameter2Mode);
						}
						else
						{
							position++;
						}
					}
					else if (opcode2 == 7)
					{
						var input1 = ReadValue(parameter1Mode);
						var input2 = ReadValue(parameter2Mode);
						StoreValue(input1 < input2 ? 1 : 0, parameter3Mode);
					}
					else if (opcode2 == 8)
					{
						var input1 = ReadValue(parameter1Mode);
						var input2 = ReadValue(parameter2Mode);
						StoreValue(input1 == input2 ? 1 : 0, parameter3Mode);
					}
				}

				Console.WriteLine("CURRENT POS: " + position);
			}
		}
	}
}