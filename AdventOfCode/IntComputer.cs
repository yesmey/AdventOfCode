using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
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

	public class IntComputer
	{
		private readonly int[] _memory;
		private int _position;
		private readonly State _currentState;
		private Queue<int> _inputs = new Queue<int>();

		public IntComputer(int[] memory)
		{
			_memory = memory.ToArray();
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

		public Func<int> InputAction = () => int.Parse(Console.ReadLine());
		public Action<int> OutputAction = Console.WriteLine;

		public void Parse()
		{
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
					var inputValue = InputAction();
					StoreValue(inputValue);
				}
				else if (_currentState.Opcode == Opcode.WriteOutput)
				{
					OutputAction(ReadValue());
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
					return;
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
}
