#pragma once

using namespace System;

namespace InputFirst {
	public ref class Class1
	{
		// TODO: Add your methods for this class here.
	public:
		int GetInput()
		{
			int i = 0;
			bool success = false;

			do
			{
				Console::Write("C++: Please enter an integer> ");
				success = Int32::TryParse(Console::ReadLine(), i);
			} while (!success);

			return i;
		}
	};
}
