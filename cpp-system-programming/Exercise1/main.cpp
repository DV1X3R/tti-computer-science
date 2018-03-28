#include "BitUtils.h"
#include "DatePacker.h"
#include <iostream>

using namespace std;

int main()
{
	FillBitsTable(); // Fill the array with number of active bits
	cout << "\tBitConverter demonstration" << endl << ">Enter an integer : ";
	unsigned int bcNumber; cin >> bcNumber;
	cout << bcNumber << " =\t" << DecToBin(bcNumber) << "\t(" << CountBits(bcNumber) << " bits, " << CountBitsTable(bcNumber) << " t)" << endl;

	cout << endl << "\tCycleShifter demonstration" << endl << ">Enter the step size: ";
	unsigned int csStep; cin >> csStep;
	unsigned int csResult = CycleShift(bcNumber, csStep);
	cout << "Shifted value: " << csResult << endl << " =\t" << DecToBin(csResult) << "\t(" << CountBits(csResult) << " bits)" << endl;

	cout << endl << "\tDatePacker demonstration" << endl;
	date dpDate;

	cout << ">Enter a day: "; cin >> dpDate.day;
	cout << dpDate.day << " =\t" << DecToBin(dpDate.day) << "\t(" << CountBits(dpDate.day) << " bits)" << endl;

	cout << ">Enter a month: "; cin >> dpDate.month;
	cout << dpDate.month << " =\t" << DecToBin(dpDate.month) << "\t(" << CountBits(dpDate.month) << " bits)" << endl;

	cout << ">Enter a year: "; cin >> dpDate.year;
	cout << dpDate.year << " =\t" << DecToBin(dpDate.year) << "\t(" << CountBits(dpDate.year) << " bits)" << endl;

	unsigned int dpEncrypted = PackData(dpDate);
	cout << endl << "Encrypted value: " << dpEncrypted << endl << " =\t" << DecToBin(dpEncrypted) << "\t(" << CountBits(dpEncrypted) << " bits)" << endl;
	date dpDecrypted = RepackData(dpEncrypted);
	cout << endl << "Decrypted date: " << dpDecrypted.day << "/" << dpDecrypted.month << "/" << dpDecrypted.year << endl;

	system("pause");
	return 0;
}
