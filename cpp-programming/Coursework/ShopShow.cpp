#include "Shop.h"
#include "ShopIO.h"
#include "ShopStructs.h"
#include <iostream>
#include <windows.h>

using namespace std;

void ShowStruct(StructId STRUCTNUM, char *data, int *count) {
	int *count2 = new int; // Linked tables 
	int *count3 = new int;

	switch (STRUCTNUM) {
	case STRUCT_CLIENTS: {
		Clients *clients = (Clients*)data;
		Offers *offers = (Offers*)ReadStruct(STRUCT_OFFERS, count2);
		cout << " ________________________________________________CLIENTS___________________________________________________" << endl
			<< " |--ID--|Personal Code-|-Client name--|-Client surname--|------Client address-------|--Phone nr.---|Offers|" << endl
			<< " |======|==============|==============|=================|===========================|==============|======|" << endl;
		for (int i = 0; i < *count; i++) {
			// Get number of active offers per client
			int oCount = 0;
			for (int x = 0; x < *count2; x++) {
				if (offers[x].clientID == clients[i].id) {
					oCount++;
				}
			}

			printf(" | %4d | %-12s | %-12s | %-15s | %-25s | %12s | %4d |\n",
				clients[i].id, clients[i].code, clients[i].name, clients[i].surname, clients[i].address, clients[i].phone, oCount);
		}
		cout << " |========================================================================================================|" << endl;
		break;
	}

	case STRUCT_COMPUTERS: {
		Computers *computers = (Computers*)data;
		Offers *offers = (Offers*)ReadStruct(STRUCT_OFFERS, count2);
		cout << " ______________________________________________________________COMPUTERS_____________________________________________________________" << endl
			<< " |--ID--|-Code--|------Computer model-------|------------CPU------------|----RAM-----|---------Software----------|--Price--|Reserved|" << endl
			<< " |======|=======|===========================|===========================|============|===========================|=========|========|" << endl;
		for (int i = 0; i < *count; i++) {
			// Is computer reserved (has active offer)
			bool reserved = false;
			for (int x = 0; x < *count2; x++) {
				if (offers[x].computerID == computers[i].id) {
					reserved = true;
					SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), 14);
					break;
				}
			}

			printf(" | %4d | %-5s | %-25s | %-25s | %-10s | %-25s | %4deur |   %s    |\n",
				computers[i].id, computers[i].code, computers[i].name, computers[i].cpu, computers[i].ram, computers[i].software, computers[i].price, reserved ? "+" : " ");

			SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), 7);
		}
		cout << " |==================================================================================================================================|" << endl;
		break;
	}

	case STRUCT_OFFERS: {
		Offers *offers = (Offers*)data;
		Clients *clients = (Clients*)ReadStruct(STRUCT_CLIENTS, count2);
		Computers *computers = (Computers*)ReadStruct(STRUCT_COMPUTERS, count3);
		cout << " ____________________________________________________OFFERS_____________________________________________________" << endl
			<< " |--ID--|----------------Client-----------------|--------------Computer--------------|Borrow date-|Refund date-|" << endl
			<< " |======|=======================================|====================================|============|============|" << endl;
		for (int i = 0; i < *count; i++) {
			printf(" | %4d |", offers[i].id);

			// Get client name and surname
			for (int x = 0; x < *count2; x++) {
				if (clients[x].id == offers[i].clientID) {
					printf(" %12s %-15s ID: %-4d |",
						clients[x].name, clients[x].surname, clients[x].id);
				}
			}

			// Get computer name
			for (int x = 0; x < *count3; x++) {
				if (computers[x].id == offers[i].computerID) {
					printf(" %-25s ID: %-4d |",
						computers[x].name, computers[x].id);
				}
			}

			printf(" %02d/%02d/%04d | %02d/%02d/%04d |\n",
				offers[i].borrow.day, offers[i].borrow.month, offers[i].borrow.year,
				offers[i].refund.day, offers[i].refund.month, offers[i].refund.year);
		}
		cout << " |=============================================================================================================|" << endl;
		break;
	}

	}

	if (*count == 0)
		cout << "Warning: File/Result Set does not exist or it is empty!" << endl;

}

void Show(StructId STRUCTNUM) {
	int *count = new int;
	char *data = ReadStruct(STRUCTNUM, count);
	ShowStruct(STRUCTNUM, data, count);
}
