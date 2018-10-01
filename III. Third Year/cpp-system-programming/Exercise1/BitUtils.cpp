#include "BitUtils.h"

std::string DecToBin(unsigned int value)
{
	std::string result = "0b";
	int separator = 1;

	for (unsigned int i = (1 << 31); i > 0; i >>= 1)
	{
		result += (value & i) ? '1' : '0';
		if (separator++ % 4 == 0)
			result += ' ';
	}

	return result;
}

unsigned int CycleShift(unsigned int value, unsigned int bitCount)
{
	bool bitTmp;

	for (unsigned int i = 0; i < bitCount; i++)
	{
		bitTmp = value & (1 << 31) ? 1 : 0;
		value <<= 1;
		value |= bitTmp; // Restore last bit value
	}

	return value;
}

unsigned int CountBits(unsigned int value)
{
	unsigned int result = 0;

	for (unsigned int i = (1 << 31); i > 0; i >>= 1)
		result += (value & i) ? 1 : 0;

	return result;
}

char BitsTable[256];

unsigned int CountBitsTable(unsigned int value)
{
	unsigned int result = 0;

	while (value != 0)
	{
		result += BitsTable[value & 255];
		value >>= 8;
	}

	return result;
}

void FillBitsTable()
{
	for (unsigned int i = 0; i < 256; i++)
		for (unsigned int m = (1 << 7); m > 0; m >>= 1)
			BitsTable[i] += (i & m) ? 1 : 0; // Count active bits
}
