#include <windows.h>
#include <iostream>
#include <tchar.h>
#include <string>

using namespace std;

bool RegSearch(HKEY hRootKey, TCHAR *cPath, bool bSearch = false, TCHAR *sSearchKeyName = NULL)
{
	HKEY hKey;

	// Открытие ключа
	LONG dwRegOpenKey = RegOpenKeyEx(
		hRootKey // Идентифицирует открытый в текущий момент ключ
		, cPath // Адрес имени открываемого подключа
		, NULL // Зарезервировано
		, KEY_READ // Маска доступа безопасности
		, &hKey); // Адрес переменной, в которую возвращается дескриптор открытого ключа

	if (dwRegOpenKey == ERROR_SUCCESS)
	{
		// Информация о открытом ключе (количество подключей, количество значений и размеры буфферов)
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

		if (dwRegQueryInfoKey == ERROR_SUCCESS) {

			// Обработка переменных в текущем ключе
			if (dwValues) {
				for (int i = 0; i < dwValues; i++) {
					DWORD dwValueNameBufferLen = dwMaxValueNameLen + 1;
					TCHAR *cValueNameBuffer = new TCHAR[dwValueNameBufferLen];
					ZeroMemory(cValueNameBuffer, sizeof(cValueNameBuffer));

					// Получение названий переменных в ключе
					LONG dwRegEnumValue = RegEnumValue(hKey, i, cValueNameBuffer, &dwValueNameBufferLen, NULL, NULL, NULL, NULL);

					if (dwRegEnumValue == ERROR_SUCCESS) {
						DWORD dwType;
						DWORD dwValueBufferLen = dwMaxValueLen + 1;
						BYTE *bValueBuffer = new BYTE[dwValueBufferLen];
						ZeroMemory(bValueBuffer, sizeof(bValueBuffer));

						// Получение типов данных и содержимого переменных
						LONG dwRegQueryValueEx = RegQueryValueEx(hKey, cValueNameBuffer, NULL, &dwType, bValueBuffer, &dwValueBufferLen);

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

							_tprintf(_T("\n%s\\%s \n \t Type: %s \n \t Data: %s\n"), cPath, cValueNameBuffer, type, bValueBuffer);
						}

						// Если выбран поиск, и нужный ключ найден, то завершаем работу метода и завершаем обход в глубину
						if (bSearch && !wcscmp(cValueNameBuffer, sSearchKeyName)) {
							RegCloseKey(hKey);
							return true;
						}
					}
				}
			}

			// Обработка подключей, если выбран поиск определённого ключа
			if (dwSubKeys && bSearch) {
				for (int i = 0; i < dwSubKeys; i++) {
					DWORD dwSubKeyBufferLen = dwMaxSubKeyLen + 1;
					TCHAR *cSubKeyBuffer = new TCHAR[dwSubKeyBufferLen];
					ZeroMemory(cSubKeyBuffer, sizeof(cSubKeyBuffer));

					// Получение имени подключей
					LONG dwRegEnumKeyEx = RegEnumKeyEx(
						hKey // дескриптор перечисляемого ключа
						, i // индекс перечисляемого подключа
						, cSubKeyBuffer // адрес буфера для имени подключа
						, &dwSubKeyBufferLen // адрес размера буфера подключа
						, NULL // зарезервировано
						, NULL // адрес буфера для имени класса
						, NULL // адрес размера буфера для имени класса
						, NULL); // адрес для времени последней записи ключа

					if (dwRegEnumKeyEx == ERROR_SUCCESS) {
						TCHAR *newPath = new TCHAR[MAX_PATH + 1];
						ZeroMemory(newPath, sizeof(newPath));
						
						wcscat(newPath, cPath);
						if(wcscmp(newPath, L"") != 0)
							wcscat(newPath, L"\\"); 
						wcscat(newPath, cSubKeyBuffer);
						
						// Запуск рекурсии
						if (RegSearch(hRootKey, newPath, bSearch, sSearchKeyName))
							return true;
					}
				}
			}

		}
	}

	RegCloseKey(hKey);
	return false;
}

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

	if(sKeyName == "")
		RegSearch(hRootKey, cPath);
	else 
		RegSearch(hRootKey, cPath, true, cKeyName);

	system("pause");
	return 0;
}
