#include "RegSearch.h"

bool RegSearch(HKEY hRootKey, TCHAR *cPath, bool bSearch, TCHAR *sSearchKeyName)
{
	_tprintf(_T(" %s\n"), cPath);
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
						TCHAR *cType = _T("");
						DWORD dwValueBufferLen = dwMaxValueLen + 1;
						BYTE *bValueBuffer = new BYTE[dwValueBufferLen];
						ZeroMemory(bValueBuffer, sizeof(bValueBuffer));

						// Получение типов данных и содержимого переменных
						LONG dwRegQueryValueEx = RegQueryValueEx(hKey, cValueNameBuffer, NULL, &dwType, bValueBuffer, &dwValueBufferLen);

						if (dwRegQueryValueEx == ERROR_SUCCESS)
						{
							switch (dwType)
							{
							case REG_BINARY: cType = _T("REG_BINARY"); break;
							case REG_DWORD: cType = _T("REG_DWORD"); break;
							case REG_DWORD_BIG_ENDIAN: cType = _T("REG_DWORD_BIG_ENDIAN"); break;
							case REG_EXPAND_SZ: cType = _T("REG_EXPAND_SZ"); break;
							case REG_LINK: cType = _T("REG_LINK"); break;
							case REG_MULTI_SZ: cType = _T("REG_MULTI_SZ"); break;
							case REG_NONE: cType = _T("REG_NONE"); break;
							case REG_QWORD: cType = _T("REG_QWORD"); break;
							case REG_SZ: cType = _T("REG_SZ"); break;
							}
						}

						bool searchFinished = bSearch && !wcscmp(cValueNameBuffer, sSearchKeyName);

						if (!bSearch || searchFinished) {
							_tprintf(_T("\n%s\\%s \n \t Type: %s \n \t Data: %s\n"), cPath, cValueNameBuffer, cType, bValueBuffer);
						}

						// Если выбран поиск, и нужный ключ найден, то завершаем работу метода и завершаем обход в глубину
						if (searchFinished) {
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
						if (wcscmp(newPath, L"") != 0)
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
