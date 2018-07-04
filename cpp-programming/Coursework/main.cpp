#include "Shop.h"
#include <iostream>
#include <conio.h>

using namespace std;

int main() {
	int menu = 0;
	int select = 0;

	while (select != -1) {
		system("cls");

		switch (menu) {
		case 0:
			cout << " ____________Main Menu____________\n";
			cout << " | 1.View records         " << (select == 0 ? " <<<" : "") << "\t | \n";
			cout << " | 2.Add new records      " << (select == 1 ? " <<<" : "") << "\t | \n";
			cout << " | 3.Delete records       " << (select == 2 ? " <<<" : "") << "\t | \n";
			cout << " | 4.Sort records         " << (select == 3 ? " <<<" : "") << "\t | \n";
			cout << " | 5.Find records         " << (select == 4 ? " <<<" : "") << "\t | \n";
			cout << " | 0.Exit application     " << (select == 5 ? " <<<" : "") << "\t | \n";
			cout << " |-------------------------------|\n\n";

			switch (_getch()) {
			case 72: if (select > 0) select--; break;
			case 80: if (select < 5) select++; break;
			case 13:
				if (select == 5) select = -1;
				else {
					menu = select + 1;
					select = 0;
				}
				break;
			}

			break;

		case 1:
			cout << " __________View Records___________\n";
			cout << " | 1.Client database      " << (select == 0 ? " <<<" : "") << "\t | \n";
			cout << " | 2.Computer database    " << (select == 1 ? " <<<" : "") << "\t | \n";
			cout << " | 3.Offer database       " << (select == 2 ? " <<<" : "") << "\t | \n";
			cout << " | 0.Return               " << (select == 3 ? " <<<" : "") << "\t | \n";
			cout << " |-------------------------------|\n\n";

			switch (_getch()) {
			case 72: if (select > 0) select--; break;
			case 80: if (select < 3) select++; break;
			case 13:
				switch (select) {
				case 0: Show(STRUCT_CLIENTS); system("pause"); break;
				case 1: Show(STRUCT_COMPUTERS); system("pause"); break;
				case 2: Show(STRUCT_OFFERS); system("pause"); break;
				case 3: menu = 0; select = 0; break;
				}
				break;
			}

			break;

		case 2:
			cout << " _________Add new Records_________\n";
			cout << " | 1.Client database      " << (select == 0 ? " <<<" : "") << "\t | \n";
			cout << " | 2.Computer database    " << (select == 1 ? " <<<" : "") << "\t | \n";
			cout << " | 3.Offer database       " << (select == 2 ? " <<<" : "") << "\t | \n";
			cout << " | 0.Return               " << (select == 3 ? " <<<" : "") << "\t | \n";
			cout << " |-------------------------------|\n\n";

			switch (_getch()) {
			case 72: if (select > 0) select--; break;
			case 80: if (select < 3) select++; break;
			case 13:
				switch (select) {
				case 0: Add(STRUCT_CLIENTS); system("pause"); break;
				case 1: Add(STRUCT_COMPUTERS); system("pause"); break;
				case 2: Add(STRUCT_OFFERS); system("pause"); break;
				case 3: menu = 0; select = 1; break;
				}
				break;
			}

			break;

		case 3:
			cout << " _________Delete Records__________\n";
			cout << " | 1.Client database      " << (select == 0 ? " <<<" : "") << "\t | \n";
			cout << " | 2.Computer database    " << (select == 1 ? " <<<" : "") << "\t | \n";
			cout << " | 3.Offer database       " << (select == 2 ? " <<<" : "") << "\t | \n";
			cout << " | 0.Return               " << (select == 3 ? " <<<" : "") << "\t | \n";
			cout << " |-------------------------------|\n\n";

			switch (_getch()) {
			case 72: if (select > 0) select--; break;
			case 80: if (select < 3) select++; break;
			case 13:
				switch (select) {
				case 0: Delete(STRUCT_CLIENTS); system("pause"); break;
				case 1: Delete(STRUCT_COMPUTERS); system("pause"); break;
				case 2: Delete(STRUCT_OFFERS); system("pause"); break;
				case 3: menu = 0; select = 2; break;
				}
				break;
			}

			break;

		case 4:
			cout << " __________Sort Records___________\n";
			cout << " | 1.Clients by ID        " << (select == 0 ? " <<<" : "") << "\t | \n";
			cout << " | 2.Clients by name      " << (select == 1 ? " <<<" : "") << "\t | \n";
			cout << " | 3.Computers by ID      " << (select == 2 ? " <<<" : "") << "\t | \n";
			cout << " | 4.Computers by model   " << (select == 3 ? " <<<" : "") << "\t | \n";
			cout << " | 5.Offers by ID         " << (select == 4 ? " <<<" : "") << "\t | \n";
			cout << " | 6.Offers by borrow date" << (select == 5 ? " <<<" : "") << "\t | \n";
			cout << " | 7.Offers by refund date" << (select == 6 ? " <<<" : "") << "\t | \n";
			cout << " | 0.Return               " << (select == 7 ? " <<<" : "") << "\t | \n";
			cout << " |-------------------------------|\n\n";

			switch (_getch()) {
			case 72: if (select > 0) select--; break;
			case 80: if (select < 7) select++; break;
			case 13:
				switch (select) {
				case 0: Sort(SortClientsID); system("pause"); break;
				case 1: Sort(SortClientsName); system("pause"); break;
				case 2: Sort(SortComputersID); system("pause"); break;
				case 3: Sort(SortComputersName); system("pause"); break;
				case 4: Sort(SortOffersID); system("pause"); break;
				case 5: Sort(SortOffersBorrow); system("pause"); break;
				case 6: Sort(SortOffersRefund); system("pause"); break;
				case 7: menu = 0; select = 3; break;
				}
				break;
			}

			break;

		case 5:
			cout << " ____Filter and Search Records____\n";
			cout << " | 1.Clients by Name      " << (select == 0 ? " <<<" : "") << "\t | \n";
			cout << " | 2.Clients by Surname   " << (select == 1 ? " <<<" : "") << "\t | \n";
			cout << " | 3.Computers by model   " << (select == 2 ? " <<<" : "") << "\t | \n";
			cout << " | 0.Return               " << (select == 3 ? " <<<" : "") << "\t | \n";
			cout << " |-------------------------------|\n\n";

			switch (_getch()) {
			case 72: if (select > 0) select--; break;
			case 80: if (select < 3) select++; break;
			case 13:
				switch (select) {
				case 0: Search(SearchClientsName); system("pause"); break;
				case 1: Search(SearchClientsSurname); system("pause"); break;
				case 2: Search(SearchComputersName); system("pause"); break;
				case 3: menu = 0; select = 4; break;
				}
				break;
			}

			break;
		}
	}

	return 0;
}
