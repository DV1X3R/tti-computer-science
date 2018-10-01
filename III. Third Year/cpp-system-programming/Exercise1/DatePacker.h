#pragma once

struct date { unsigned int year, month, day; };
unsigned int PackData(date data);
date RepackData(unsigned int data);
