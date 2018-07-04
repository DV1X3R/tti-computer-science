#include "Shop.h"
#include "ShopIO.h"
#include "ShopStructs.h"
#include <iostream>
#include <algorithm>

using namespace std;

void Sort(StructSort SORT) {
	int *count = new int;

	switch (SORT) {
	case SortClientsID: {
		Clients *clients = (Clients*)ReadStruct(STRUCT_CLIENTS, count);
		sort(clients, clients + *count, [](Clients const &a, Clients const &b) { return a.id < b.id; });
		RewriteStruct(STRUCT_CLIENTS, (char*)clients, count);
		break;
	}

	case SortClientsName: {
		Clients *clients = (Clients*)ReadStruct(STRUCT_CLIENTS, count);
		sort(clients, clients + *count, [](Clients const &a, Clients const &b) { return strcmp(a.name, b.name) < 0; });
		RewriteStruct(STRUCT_CLIENTS, (char*)clients, count);
		break;
	}

	case SortComputersID: {
		Computers *computers = (Computers*)ReadStruct(STRUCT_COMPUTERS, count);
		sort(computers, computers + *count, [](Computers const &a, Computers const &b) { return a.id < b.id; });
		RewriteStruct(STRUCT_COMPUTERS, (char*)computers, count);
		break;
	}

	case SortComputersName: {
		Computers *computers = (Computers*)ReadStruct(STRUCT_COMPUTERS, count);
		sort(computers, computers + *count, [](Computers const &a, Computers const &b) { return strcmp(a.name, b.name) < 0; });
		RewriteStruct(STRUCT_COMPUTERS, (char*)computers, count);
		break;
	}

	case SortOffersID: {
		Offers *offers = (Offers*)ReadStruct(STRUCT_OFFERS, count);
		sort(offers, offers + *count, [](Offers const &a, Offers const &b) { return a.id < b.id; });
		RewriteStruct(STRUCT_OFFERS, (char*)offers, count);
		break;
	}

	case SortOffersBorrow: {
		Offers *offers = (Offers*)ReadStruct(STRUCT_OFFERS, count);
		sort(offers, offers + *count, [](Offers const &a, Offers const &b) { return a.borrow.day < b.borrow.day; });
		sort(offers, offers + *count, [](Offers const &a, Offers const &b) { return a.borrow.month < b.borrow.month; });
		sort(offers, offers + *count, [](Offers const &a, Offers const &b) { return a.borrow.year < b.borrow.year; });
		RewriteStruct(STRUCT_OFFERS, (char*)offers, count);
		break;
	}

	case SortOffersRefund: {
		Offers *offers = (Offers*)ReadStruct(STRUCT_OFFERS, count);
		sort(offers, offers + *count, [](Offers const &a, Offers const &b) { return a.refund.day < b.refund.day; });
		sort(offers, offers + *count, [](Offers const &a, Offers const &b) { return a.refund.month < b.refund.month; });
		sort(offers, offers + *count, [](Offers const &a, Offers const &b) { return a.refund.year < b.refund.year; });
		RewriteStruct(STRUCT_OFFERS, (char*)offers, count);
		break;
	}

	default:
		break;
	}

	cout << "Sorting completed!" << endl;
}
