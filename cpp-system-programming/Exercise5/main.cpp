#include <windows.h>
#include <iostream>
#include <tchar.h>
#include <string>

using namespace std;

int main()
{
	setlocale(LC_ALL, "Russian");

	string sRoot = "", sPath = "";
	HKEY hRootKey = 0, hKey; // HKEY_LOCAL_MACHINE;
	WCHAR *cPath = new WCHAR[MAX_PATH + 1]; // _T("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Reliability\\");

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

	LONG dwRegOpenKey = RegOpenKeyEx(
		hRootKey // Идентифицирует открытый в текущий момент ключ
		, cPath // Адрес имени открываемого подключа
		, NULL // Зарезервировано
		, KEY_READ // Маска доступа безопасности
		, &hKey); // Адрес переменной, в которую возвращается дескриптор открытого ключа

	if (dwRegOpenKey == ERROR_SUCCESS)
	{
		_tprintf(_T("%s\n\n"), cPath);

		DWORD dwSubKeys, dwMaxSubKeyLen, dwValues, dwMaxValueNameLen, dwMaxValueLen;

		LONG dwRegQueryInfoKey = RegQueryInfoKey(
			hKey // дескриптор ключа
			, NULL // адрес буфера для имени класса
			, NULL // адрес размера буфер для имени класса
			, NULL // зарезервировано
			, &dwSubKeys // адрес буфера для количества подключей
			, &dwMaxSubKeyLen //адрес буфера для наибольшего размера имени подключа
			, NULL // адрес буфера для наибольшего размера имени класса
			, &dwValues // адрес буфера для количества вхождений значений
			, &dwMaxValueNameLen // адрес буфера для наибольшего размера имени значения
			, &dwMaxValueLen // адрес буфера для наибольшего размера данных значения
			, NULL // адрес буфера для длины дескриптора безопасности
			, NULL); // адрес буфера для получения времени последней записи

		if (dwRegQueryInfoKey == ERROR_SUCCESS)
		{
			if (dwSubKeys)
			{
				_tprintf(_T("Number of subkeys: %i\n"), dwSubKeys);

				for (int i = 0; i < dwSubKeys; i++)
				{
					DWORD dwSubKeyBufferLen = dwMaxSubKeyLen + 1;
					TCHAR *cSubKeyBuffer = new TCHAR[dwSubKeyBufferLen];
					ZeroMemory(cSubKeyBuffer, sizeof(cSubKeyBuffer));

					LONG dwRegEnumKeyEx = RegEnumKeyEx(
						hKey // дескриптор перечисляемого ключа
						, i // индекс перечисляемого подключа
						, cSubKeyBuffer // адрес буфера для имени подключа
						, &dwSubKeyBufferLen // адрес размера буфера подключа
						, NULL // зарезервировано
						, NULL // адрес буфера для имени класса
						, NULL // адрес размера буфера для имени класса
						, NULL); // адрес для времени последней записи ключа

					if (dwRegEnumKeyEx == ERROR_SUCCESS)
					{
						_tprintf(_T("\t%i %s\n"), i, cSubKeyBuffer);
					}
					else { _tprintf(_T("Error occurred during reading the subkeys: %d\n"), dwRegEnumKeyEx); }
				}

				_tprintf(_T("\n"));
			}

			if (dwValues)
			{
				_tprintf(_T("Number of values: %i\n"), dwValues);

				for (int i = 0; i < dwValues; i++)
				{
					DWORD dwValueNameBufferLen = dwMaxValueNameLen + 1;
					TCHAR *cValueNameBuffer = new TCHAR[dwValueNameBufferLen];

					LONG dwRegEnumValue = RegEnumValue(
						hKey, i, cValueNameBuffer, &dwValueNameBufferLen, NULL, NULL, NULL, NULL);

					if (dwRegEnumValue == ERROR_SUCCESS)
					{
						DWORD dwType;
						DWORD dwValueBufferLen = dwMaxValueLen + 1;
						BYTE *bValueBuffer = new BYTE[dwValueBufferLen];
						ZeroMemory(bValueBuffer, sizeof(bValueBuffer));

						LONG dwRegQueryValueEx = RegQueryValueEx(
							hKey // дескриптор ключа
							, cValueNameBuffer // адрес имени значения
							, NULL // зарезервировано
							, &dwType //адрес переменной для типа значения
							, bValueBuffer // адрес буфера для данных
							, &dwValueBufferLen); // адрес переменной для размер буфера данных

						if (dwRegQueryValueEx == ERROR_SUCCESS)
						{
							TCHAR *type = _T("");

							switch (dwType)
							{
							case REG_BINARY: type = _T("REG_BINARY"); break;
							case REG_DWORD: type = _T("REG_DWORD"); break;
							case REG_DWORD_BIG_ENDIAN: type = _T("REG_DWORD_BIG_ENDIAN"); break;
							case REG_EXPAND_SZ: type = _T("REG_EXPAND_SZ"); break;
							case REG_LINK: type = _T("REG_LINK"); break;
							case REG_MULTI_SZ: type = _T("REG_MULTI_SZ"); break;
							case REG_NONE: type = _T("REG_NONE"); break;
							case REG_QWORD: type = _T("REG_QWORD"); break;
							case REG_SZ: type = _T("REG_SZ"); break;
							}

							_tprintf(_T("\t%i %s (%s): %s\n"), i, cValueNameBuffer, type, bValueBuffer);
						}
						else { _tprintf(_T("Error occurred during reading the values data: %d\n"), dwRegQueryValueEx); }
					}
					else { _tprintf(_T("Error occurred during reading the values: %d\n"), dwRegEnumValue); }
				}

				_tprintf(_T("\n"));
			}
		}
		else { _tprintf(_T("Error occurred during getting info of the key: %d\n"), dwRegQueryInfoKey); }
	}
	else { _tprintf(_T("Error occurred during opening the key: %d\n"), dwRegOpenKey); }

	RegCloseKey(hKey);

	system("pause");
	return 0;
}
