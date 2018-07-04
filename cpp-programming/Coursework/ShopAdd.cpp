#include "Shop.h"
#include "ShopIO.h"
#include "ShopStructs.h"
#include <iostream>

using namespace std;

enum IntType { Year, Month, Day, Price };

int CinInt(IntType type)
{
	int n = 0;
	int limit = INT_MAX;

	// Input borders for different types
	switch (type) {
	case Year: limit = 3000; break;
	case Month: limit = 12; break;
	case Day: limit = 31; break;
	}

	while (!(cin >> n) || n < 1 || n > limit) {
		cout << "Error: wrong integer! Try again> ";
		cin.clear(); cin.ignore(INT_MAX, '\n');
	}
	cin.clear(); cin.ignore(INT_MAX, '\n');

	return n;
}

char *CinCode(StructId STRUCTNUM) {
	int *count = new int;
	bool unique = true;

	// Check code for uniqueness
	switch (STRUCTNUM) {
	case STRUCT_CLIENTS: {
		char *code = new char[13];
		Clients *clients = (Clients*)ReadStruct(STRUCT_CLIENTS, count);
		do {
			unique = true;
			cin.get(code, 13); cin.clear(); cin.ignore(INT_MAX, '\n');
			for (int i = 0; i < *count; i++) {
				if (strcmp(code, clients[i].code) == 0 || strcmp(code, "") == 0) {
					unique = false;
					cout << "Error: Non unique code! Try again> ";
					break;
				}
			}
		} while (!unique);

		return code;
	}

	case STRUCT_COMPUTERS: {
		char *code = new char[6];
		Computers *computers = (Computers*)ReadStruct(STRUCT_COMPUTERS, count);
		do {
			unique = true;
			cin.get(code, 6); cin.clear(); cin.ignore(INT_MAX, '\n');
			for (int i = 0; i < *count; i++) {
				if (strcmp(code, computers[i].code) == 0 || strcmp(code, "") == 0) {
					unique = false;
					cout << "Error: Non unique code! Try again> ";
					break;
				}
			}
		} while (!unique);
		return code;
	}

	default:
		return NULL;
	}
}

int GetNewId(StructId STRUCTNUM) {
	int *count = new int;
	bool free = true;

	// Get first available int number
	switch (STRUCTNUM) {
	case STRUCT_CLIENTS: {
		Clients *clients = (Clients*)ReadStruct(STRUCT_CLIENTS, count);
		for (int i = 0; i < INT_MAX; i++) {
			free = true;
			for (int x = 0; x < *count; x++) {
				if (clients[x].id == i) {
					free = false; break;
				}
			}
			if (free) return i;
		}
		break;
	}

	case STRUCT_COMPUTERS: {
		Computers *computers = (Computers*)ReadStruct(STRUCT_COMPUTERS, count);
		for (int i = 0; i < INT_MAX; i++) {
			free = true;
			for (int x = 0; x < *count; x++) {
				if (computers[x].id == i) {
					free = false; break;
				}
			}
			if (free) return i;
		}
		break;
	}

	case STRUCT_OFFERS: {
		Offers *offers = (Offers*)ReadStruct(STRUCT_OFFERS, count);
		for (int i = 0; i < INT_MAX; i++) {
			free = true;
			for (int x = 0; x < *count; x++) {
				if (offers[x].id == i) {
					free = false; break;
				}
			}
			if (free) return i;
		}
		break;
	}

	}

	// No available ID found
	return -1;
}

void Add(StructId STRUCTNUM) {
	switch (STRUCTNUM) {
	case STRUCT_CLIENTS: {
		Clients client;
		memset(&client, 0, sizeof(Clients));

		// Assign and check ID
		client.id = GetNewId(STRUCT_CLIENTS);
		if (client.id == -1) {
			cout << "Error: Client table is full!" << endl;
			break;
		}

		cout << "Enter new clients code> ";
		strcpy_s(client.code, 13, CinCode(STRUCT_CLIENTS));
		cout << "Enter new clients name> ";
		cin.get(client.name, 13); cin.clear(); cin.ignore(INT_MAX, '\n');
		cout << "Enter new clients surname> ";
		cin.get(client.surname, 16); cin.clear(); cin.ignore(INT_MAX, '\n');
		cout << "Enter new clients address> ";
		cin.get(client.address, 26); cin.clear(); cin.ignore(INT_MAX, '\n');
		cout << "Enter new clients phone number> ";
		cin.get(client.phone, 13); cin.clear(); cin.ignore(INT_MAX, '\n');

		AddToStruct(STRUCT_CLIENTS, (char*)&client);
		cout << "Client has been successfully added!" << endl;
		break;
	}

	case STRUCT_COMPUTERS: {
		Computers computer;
		memset(&computer, 0, sizeof(Computers));

		computer.id = GetNewId(STRUCT_COMPUTERS);
		if (computer.id == -1) {
			cout << "Error: Computers table is full!" << endl;
			break;
		}

		cout << "Enter new computers code> ";
		strcpy_s(computer.code, 6, CinCode(STRUCT_COMPUTERS));
		cout << "Enter new computers model> ";
		cin.get(computer.name, 26); cin.clear(); cin.ignore(INT_MAX, '\n');
		cout << "Enter new computers cpu> ";
		cin.get(computer.cpu, 26); cin.clear(); cin.ignore(INT_MAX, '\n');
		cout << "Enter new computers ram> ";
		cin.get(computer.ram, 11); cin.clear(); cin.ignore(INT_MAX, '\n');
		cout << "Enter new computers software> ";
		cin.get(computer.software, 26); cin.clear(); cin.ignore(INT_MAX, '\n');
		cout << "Enter new computers price in eur> ";
		computer.price = CinInt(Price);

		AddToStruct(STRUCT_COMPUTERS, (char*)&computer);
		cout << "Computer has been successfully added!" << endl;
		break;
	}

	case STRUCT_OFFERS: {
		Offers offer;
		memset(&offer, 0, sizeof(Offers));
		offer.id = GetNewId(STRUCT_OFFERS);
		if (offer.id == -1) {
			cout << "Error: Offers table is full!" << endl;
			break;
		}

		// Read Clients and Computers tables
		int *countClients = new int;
		int *countComputers = new int;
		Clients *clients = (Clients*)ReadStruct(STRUCT_CLIENTS, countClients);
		Computers *computers = (Computers*)ReadStruct(STRUCT_COMPUTERS, countComputers);
		if (*countClients == 0 || *countComputers == 0) {
			cout << "Error: Clients or Computers table is empty!" << endl;
			return;
		}

		// Check for existing client
		bool clientFound = false;
		Show(STRUCT_CLIENTS);
		cout << "Enter clients personal code> ";
		do {
			char *code = new char[13];
			cin.get(code, 13); cin.clear(); cin.ignore(INT_MAX, '\n');
			for (int i = 0; i < *countClients; i++) {
				if (strcmp(clients[i].code, code) == 0) {
					clientFound = true;
					offer.clientID = clients[i].id;
					break;
				}
			}
			if (!clientFound)
				cout << "Error: Client not found! Try again> ";
		} while (!clientFound);

		// Check for existing computer
		bool computerFound = false;
		Show(STRUCT_COMPUTERS);
		cout << "Enter computers code> ";
		do {
			char *code = new char[6];
			cin.get(code, 6); cin.clear(); cin.ignore(INT_MAX, '\n');
			for (int i = 0; i < *countClients; i++) {
				if (strcmp(computers[i].code, code) == 0) {
					computerFound = true;
					offer.computerID = computers[i].id;
					break;
				}
			}
			if (!computerFound)
				cout << "Error: Computer not found! Try again> ";
		} while (!computerFound);

		cout << "Enter borrow year> ";
		offer.borrow.year = CinInt(Year);
		cout << "Enter borrow month> ";
		offer.borrow.month = CinInt(Month);
		cout << "Enter borrow day> ";
		offer.borrow.day = CinInt(Day);
		cout << "Enter refund year> ";
		offer.refund.year = CinInt(Year);
		cout << "Enter refund month> ";
		offer.refund.month = CinInt(Month);
		cout << "Enter refund day> ";
		offer.refund.day = CinInt(Day);

		AddToStruct(STRUCT_OFFERS, (char*)&offer);
		cout << "Offer has been successfully added!" << endl;
		break;
	}

	}
}
