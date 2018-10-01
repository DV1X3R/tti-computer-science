#pragma once

struct Clients
{
	int id;
	char code[13];
	char name[13];
	char surname[16];
	char address[26];
	char phone[13];
};

struct Computers
{
	int id;
	char code[6];
	char name[26];
	char cpu[26];
	char ram[11];
	char software[26];
	int price;
};

struct Offers
{
	int id;
	int clientID;
	int computerID;
	struct date { int year; int month; int day; } borrow, refund;
};
