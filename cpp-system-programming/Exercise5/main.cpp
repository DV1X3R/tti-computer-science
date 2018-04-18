#include "RegSearch.h"
#include <string>

using namespace std;

int main()
{
	setlocale(LC_ALL, "Russian");

	string sRoot = "", sPath = "", sKeyName = "";
	HKEY hRootKey = 0, hKey; // HKEY_LOCAL_MACHINE;
	WCHAR *cPath = new WCHAR[MAX_PATH + 1]; // _T("SOFTWARE\\");
	WCHAR *cKeyName = new WCHAR[MAX_PATH + 1]; // _T("DisplayName");

	do
	{
		_tprintf(_T("Enter the root key name> "));
		getline(cin, sRoot);
		for (auto & c : sRoot) c = toupper(c);

		if (sRoot == "HKEY_CLASSES_ROOT") hRootKey = HKEY_CLASSES_ROOT;
		else if (sRoot == "HKEY_CURRENT_USER") hRootKey = HKEY_CURRENT_USER;
		else if (sRoot == "HKEY_LOCAL_MACHINE") hRootKey = HKEY_LOCAL_MACHINE;
		else if (sRoot == "HKEY_USERS") hRootKey = HKEY_USERS;
		else if (sRoot == "HKEY_CURRENT_CONFIG") hRootKey = HKEY_CURRENT_CONFIG;
	} while (hRootKey == 0);

	_tprintf(_T("Enter the key path> "));
	getline(cin, sPath);
	wcscpy(cPath, wstring(sPath.begin(), sPath.end()).c_str());

	_tprintf(_T("Enter the key name to search> "));
	getline(cin, sKeyName);
	wcscpy(cKeyName, wstring(sKeyName.begin(), sKeyName.end()).c_str());

	if (sKeyName == "")
		RegSearch(hRootKey, cPath, false, NULL);
	else
		RegSearch(hRootKey, cPath, true, cKeyName);

	system("pause");
	return 0;
}
