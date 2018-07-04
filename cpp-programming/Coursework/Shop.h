#pragma once

enum StructId
{
	STRUCT_CLIENTS
	, STRUCT_COMPUTERS
	, STRUCT_OFFERS
};

enum StructSort
{
	SortClientsID
	, SortClientsName
	, SortComputersID
	, SortComputersName
	, SortOffersID
	, SortOffersBorrow
	, SortOffersRefund
};

enum StructSearch
{
	SearchClientsName
	, SearchClientsSurname
	, SearchComputersName
};

void Show(StructId STRUCTNUM);
void Add(StructId STRUCTNUM);
void Delete(StructId STRUCTNUM);
void Sort(StructSort SORT);
void Search(StructSearch SEARCH);
