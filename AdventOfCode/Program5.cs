using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
	internal class Program5
	{
		public enum PositonMode
		{
			Immediate,
			Positon
		}

		public enum Opcode
		{
			Unknown = 0,
			Addition = 1,
			Multiply = 2,
			ReadInput = 3,
			WriteOutput = 4,
			JumpNotZero = 5,
			JumpEqualZero = 6,
			IsLessThan = 7,
			IsEqual = 8,
			Quit = 99
		}

		public class State
		{
			public int ReadPosition { get; set; }
			public Opcode Opcode { get; set; }
			public PositonMode[] ParameterModes { get; set; }

			public void Reset()
			{
				ReadPosition = 0;
				ParameterModes = new[]
				{
					PositonMode.Immediate,
					PositonMode.Positon,
					PositonMode.Positon,
					PositonMode.Positon
				};
			}
		}

		public class Computer
		{
			private readonly int[] _memory;
			private int _position;
			private readonly State _currentState;

			public Computer(int[] memory)
			{
				_memory = memory;
				_position = 0;
				_currentState = new State();
			}

			private void ParseState()
			{
				_currentState.Reset();

				var opcode = ReadValue();
				if (opcode > 100)
				{
					var opcodeString = opcode.ToString().PadLeft(5, '0');
					for (var i = 0; i < 3; i++)
					{
						if (opcodeString[i] == '1')
						{
							_currentState.ParameterModes[3 - i] = PositonMode.Immediate;
						}
					}

					opcode = int.Parse($"{opcodeString[3]}{opcodeString[4]}");
				}

				_currentState.Opcode = (Opcode)opcode;
			}

			public int Parse()
			{
				int resultValue = -1;
				while (true)
				{
					ParseState();

					if (_currentState.Opcode == Opcode.Addition)
					{
						var parameter1 = ReadValue();
						var parameter2 = ReadValue();
						StoreValue(parameter1 + parameter2);
					}
					else if (_currentState.Opcode == Opcode.Multiply)
					{
						var parameter1 = ReadValue();
						var parameter2 = ReadValue();
						StoreValue(parameter1 * parameter2);
					}
					else if (_currentState.Opcode == Opcode.ReadInput)
					{
						Console.WriteLine("Write input");
						var inputValue = int.Parse(Console.ReadLine());
						StoreValue(inputValue);
					}
					else if (_currentState.Opcode == Opcode.WriteOutput)
					{
						resultValue = ReadValue();
					}
					else if (_currentState.Opcode == Opcode.JumpNotZero)
					{
						var value = ReadValue();
						var position = ReadValue();
						if (value != 0)
						{
							_position = position;
							_currentState.ReadPosition = 0;
						}
					}
					else if (_currentState.Opcode == Opcode.JumpEqualZero)
					{
						var value = ReadValue();
						var position = ReadValue();
						if (value == 0)
						{
							_position = position;
							_currentState.ReadPosition = 0;
						}
					}
					else if (_currentState.Opcode == Opcode.IsLessThan)
					{
						var input1 = ReadValue();
						var input2 = ReadValue();
						StoreValue(input1 < input2 ? 1 : 0);
					}
					else if (_currentState.Opcode == Opcode.IsEqual)
					{
						var input1 = ReadValue();
						var input2 = ReadValue();
						StoreValue(input1 == input2 ? 1 : 0);
					}
					else if (_currentState.Opcode == Opcode.Quit)
					{
						return resultValue;
					}

					_position += _currentState.ReadPosition;
				}
			}

			private int ReadValue()
			{
				var readPosition = _currentState.ReadPosition++;

				var mode = _currentState.ParameterModes[readPosition];
				if (mode == PositonMode.Immediate)
				{
					return _memory[_position + readPosition];
				}
				else
				{
					return _memory[_memory[_position + readPosition]];
				}
			}

			private void StoreValue(int value)
			{
				var readPosition = _currentState.ReadPosition++;

				var mode = _currentState.ParameterModes[readPosition];
				if (mode == PositonMode.Immediate)
				{
					_memory[_position + readPosition] = value;
				}
				else
				{
					_memory[_memory[_position + readPosition]] = value;
				}
			}
		}

		public static void Main5(string[] args)
		{
			var input = File.ReadAllText("input5.txt").Split(',').Select(int.Parse).ToArray();
			var intComputer = new Computer(input);
			var result = intComputer.Parse();
			Console.WriteLine(result);
		}
	}
}