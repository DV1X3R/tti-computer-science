#include "Shop.h"
#include "ShopIO.h"
#include "ShopStructs.h"
#include <iostream>

using namespace std;

void Delete(StructId STRUCTNUM) {
	Show(STRUCTNUM);
	cout << "Enter ID to delete> ";

	int id;
	while (!(cin >> id)) {
		cout << "Error: wrong integer! Try again> ";
		cin.clear(); cin.ignore(INT_MAX, '\n');
	}
	cin.clear(); cin.ignore(INT_MAX, '\n');

	// Delete requested record
	if (RemoveFromStruct(STRUCTNUM, id))
		cout << "Record has been successfully deleted!" << endl;
	else
		cout << "Error: Record not found!" << endl;

	// Cascade delete
	if (STRUCTNUM == STRUCT_CLIENTS || STRUCTNUM == STRUCT_COMPUTERS)
	{
		int *count = new int;
		Offers *offers = (Offers*)ReadStruct(STRUCT_OFFERS, count);
		for (int i = 0; i < *count; i++) {
			if ((STRUCTNUM == STRUCT_CLIENTS && offers[i].clientID == id) ||
				(STRUCTNUM == STRUCT_COMPUTERS && offers[i].computerID == id)) {
				RemoveFromStruct(STRUCT_OFFERS, offers[i].id);
				printf("Warning: offer %d has been deleted!\n", offers[i].id);
			}
		}
	}
}
