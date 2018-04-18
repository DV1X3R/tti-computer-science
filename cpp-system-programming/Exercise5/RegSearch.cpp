#include "RegSearch.h"

bool RegSearch(HKEY hRootKey, TCHAR *cPath, bool bSearch, TCHAR *sSearchKeyName)
{
	_tprintf(_T(" %s\n"), cPath);
	HKEY hKey;

	// �������� �����
	LONG dwRegOpenKey = RegOpenKeyEx(
		hRootKey // �������������� �������� � ������� ������ ����
		, cPath // ����� ����� ������������ ��������
		, NULL // ���������������
		, KEY_READ // ����� ������� ������������
		, &hKey); // ����� ����������, � ������� ������������ ���������� ��������� �����

	if (dwRegOpenKey == ERROR_SUCCESS)
	{
		// ���������� � �������� ����� (���������� ���������, ���������� �������� � ������� ��������)
		DWORD dwSubKeys, dwMaxSubKeyLen, dwValues, dwMaxValueNameLen, dwMaxValueLen;
		LONG dwRegQueryInfoKey = RegQueryInfoKey(
			hKey // ���������� �����
			, NULL // ����� ������ ��� ����� ������
			, NULL // ����� ������� ����� ��� ����� ������
			, NULL // ���������������
			, &dwSubKeys // ����� ������ ��� ���������� ���������
			, &dwMaxSubKeyLen //����� ������ ��� ����������� ������� ����� ��������
			, NULL // ����� ������ ��� ����������� ������� ����� ������
			, &dwValues // ����� ������ ��� ���������� ��������� ��������
			, &dwMaxValueNameLen // ����� ������ ��� ����������� ������� ����� ��������
			, &dwMaxValueLen // ����� ������ ��� ����������� ������� ������ ��������
			, NULL // ����� ������ ��� ����� ����������� ������������
			, NULL); // ����� ������ ��� ��������� ������� ��������� ������

		if (dwRegQueryInfoKey == ERROR_SUCCESS) {

			// ��������� ���������� � ������� �����
			if (dwValues) {
				for (int i = 0; i < dwValues; i++) {
					DWORD dwValueNameBufferLen = dwMaxValueNameLen + 1;
					TCHAR *cValueNameBuffer = new TCHAR[dwValueNameBufferLen];
					ZeroMemory(cValueNameBuffer, sizeof(cValueNameBuffer));

					// ��������� �������� ���������� � �����
					LONG dwRegEnumValue = RegEnumValue(hKey, i, cValueNameBuffer, &dwValueNameBufferLen, NULL, NULL, NULL, NULL);

					if (dwRegEnumValue == ERROR_SUCCESS) {
						DWORD dwType;
						TCHAR *cType = _T("");
						DWORD dwValueBufferLen = dwMaxValueLen + 1;
						BYTE *bValueBuffer = new BYTE[dwValueBufferLen];
						ZeroMemory(bValueBuffer, sizeof(bValueBuffer));

						// ��������� ����� ������ � ����������� ����������
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

						// ���� ������ �����, � ������ ���� ������, �� ��������� ������ ������ � ��������� ����� � �������
						if (searchFinished) {
							RegCloseKey(hKey);
							return true;
						}

					}
				}
			}

			// ��������� ���������, ���� ������ ����� ������������ �����
			if (dwSubKeys && bSearch) {
				for (int i = 0; i < dwSubKeys; i++) {
					DWORD dwSubKeyBufferLen = dwMaxSubKeyLen + 1;
					TCHAR *cSubKeyBuffer = new TCHAR[dwSubKeyBufferLen];
					ZeroMemory(cSubKeyBuffer, sizeof(cSubKeyBuffer));

					// ��������� ����� ���������
					LONG dwRegEnumKeyEx = RegEnumKeyEx(
						hKey // ���������� �������������� �����
						, i // ������ �������������� ��������
						, cSubKeyBuffer // ����� ������ ��� ����� ��������
						, &dwSubKeyBufferLen // ����� ������� ������ ��������
						, NULL // ���������������
						, NULL // ����� ������ ��� ����� ������
						, NULL // ����� ������� ������ ��� ����� ������
						, NULL); // ����� ��� ������� ��������� ������ �����

					if (dwRegEnumKeyEx == ERROR_SUCCESS) {
						TCHAR *newPath = new TCHAR[MAX_PATH + 1];
						ZeroMemory(newPath, sizeof(newPath));

						wcscat(newPath, cPath);
						if (wcscmp(newPath, L"") != 0)
							wcscat(newPath, L"\\");
						wcscat(newPath, cSubKeyBuffer);

						// ������ ��������
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
