#include "Shop.h"
#include "ShopIO.h"
#include "ShopStructs.h"
#include <iostream>

using namespace std;

void Search(StructSearch SEARCH) {
	switch (SEARCH) {
	case SearchClientsName: {
		int *count = new int;
		Clients *clients = (Clients*)ReadStruct(STRUCT_CLIENTS, count);

		char name[13];
		cout << "Enter clients name> ";
		cin.get(name, 13); cin.clear(); cin.ignore(INT_MAX, '\n');

		// Prepare new array
		int *ncount = new int(0);
		Clients *nclients = new Clients[*count];
		for (int i = 0; i < *count; i++) {
			if (strcmp(name, clients[i].name) == 0) {
				nclients[*ncount] = clients[i];
				*ncount += 1;
			}
		}

		ShowStruct(STRUCT_CLIENTS, (char*)nclients, ncount);
		break;
	}

	case SearchClientsSurname: {
		int *count = new int;
		Clients *clients = (Clients*)ReadStruct(STRUCT_CLIENTS, count);

		char surname[16];
		cout << "Enter clients surname> ";
		cin.get(surname, 16); cin.clear(); cin.ignore(INT_MAX, '\n');

		int *ncount = new int(0);
		Clients *nclients = new Clients[*count];
		for (int i = 0; i < *count; i++) {
			if (strcmp(surname, clients[i].surname) == 0) {
				nclients[*ncount] = clients[i];
				*ncount += 1;
			}
		}

		ShowStruct(STRUCT_CLIENTS, (char*)nclients, ncount);
		break;
	}

	case SearchComputersName: {
		int *count = new int;
		Computers *computers = (Computers*)ReadStruct(STRUCT_COMPUTERS, count);

		char name[26];
		cout << "Enter computers model> ";
		cin.get(name, 26); cin.clear(); cin.ignore(INT_MAX, '\n');

		int *ncount = new int(0);
		Computers *ncomputers = new Computers[*count];
		for (int i = 0; i < *count; i++) {
			if (strcmp(name, computers[i].name) == 0) {
				ncomputers[*ncount] = computers[i];
				*ncount += 1;
			}
		}

		ShowStruct(STRUCT_COMPUTERS, (char*)ncomputers, ncount);
		break;
	}

	default:
		break;
	}
}
