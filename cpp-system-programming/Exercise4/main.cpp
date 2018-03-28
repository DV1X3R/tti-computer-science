#include "FindFile.h"
#include <iostream>

using namespace std;

int main()
{
	setlocale(LC_ALL, "Russian");

	// Find files
	vector<FileInfo> v = FindFile(L"C:\\FILES", L"*.txt", true);
	wprintf(L"Search comleted: %i files found\n", v.size());

	// Create directory
	wprintf(L"%s\n"
		, CreateDirectory(L"C:\\FIND", NULL) ?
		L"'FIND' directory has been created" : L"'FIND' directory already exists");

	// Write results to file
	int wr = WriteFileInfo(L"C:\\FIND\\find.dat", v);
	if (wr == 0) wprintf(L"'find.dat' has been recreated");
	else if (wr == 1) wprintf(L"Error: Unable to create file 'find.dat'");
	else if (wr == 2) wprintf(L"Warning: 'find.dat' was not changed");

	// Read results from file
	wprintf(L"\n\n find.dat\n%s\n", ReadFileInfo(L"C:\\FIND\\find.dat").c_str());

	system("pause");
	return 0;
}
