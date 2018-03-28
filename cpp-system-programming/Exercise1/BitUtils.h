#pragma once
#include <String>

std::string DecToBin(unsigned int value);
unsigned int CycleShift(unsigned int value, unsigned int bitCount);

unsigned int CountBits(unsigned int value);
unsigned int CountBitsTable(unsigned int value);
void FillBitsTable();
