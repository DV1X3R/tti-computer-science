#include <windows.h>
#include <iostream>
#include <tchar.h>

using namespace std;

int main()
{
	setlocale(LC_ALL, "Russian");

	TCHAR *buffer;
	DWORD size;

	// Windows Version
	OSVERSIONINFO osvi;
	ZeroMemory(&osvi, sizeof(OSVERSIONINFO));
	osvi.dwOSVersionInfoSize = sizeof(OSVERSIONINFO);

	if (GetVersionEx(&osvi) == 0) _tprintf(_T("Error: Unable to get Windows Version\n"));
	else _tprintf(_T("Windows Version: %i.%i.%i %s\n"), osvi.dwMajorVersion, osvi.dwMinorVersion, LOWORD(osvi.dwBuildNumber), osvi.szCSDVersion);

	// Computer Name
	buffer = new TCHAR[MAX_COMPUTERNAME_LENGTH + 1];
	size = MAX_COMPUTERNAME_LENGTH + 1;

	if (GetComputerName(buffer, &size) == 0) _tprintf(_T("Error: Unable to get Computer Name\n"));
	else _tprintf(_T("Computer Name: %s\n"), buffer);

	// User Name
	buffer = new TCHAR[20 + 1];
	size = 20 + 1;

	if (GetUserName(buffer, &size) == 0) _tprintf(_T("Error: Unable to get User Name\n"));
	else _tprintf(_T("User Name: %s\n"), buffer);

	// User Language
	buffer = new TCHAR[LOCALE_NAME_MAX_LENGTH + 1];
	size = LOCALE_NAME_MAX_LENGTH + 1;

	if (VerLanguageName(GetUserDefaultLangID(), buffer, size) == 0) _tprintf(_T("Error: Unable to get User Language\n"));
	else _tprintf(_T("User Language: %s\n"), buffer);

	// Local Time
	SYSTEMTIME st;
	TIME_ZONE_INFORMATION tzi;
	GetLocalTime(&st);
	GetTimeZoneInformation(&tzi);
	_tprintf(_T("Current Time: %i:%i:%i \t Standard GMT%s%i \t Daylight GMT%s%i \n\n")
		, st.wHour, st.wMinute, st.wSecond
		, tzi.Bias < 0 ? _T("+") : _T(""), -(tzi.Bias / 60)
		, tzi.Bias + tzi.DaylightBias < 0 ? _T("+") : _T(""), -(tzi.Bias + tzi.DaylightBias) / 60);

	// Directories
	buffer = new TCHAR[MAX_PATH + 1];
	size = MAX_PATH + 1;

	if (GetCurrentDirectory(size, buffer) == 0) _tprintf(_T("Error: Unable to get Current Directory\n"));
	else _tprintf(_T("Current Directory: %s\n"), buffer);

	if (GetWindowsDirectory(buffer, size) == 0) _tprintf(_T("Error: Unable to get Windows Directory\n"));
	else _tprintf(_T("Windows Directory: %s\n"), buffer);

	if (GetSystemDirectory(buffer, size) == 0) _tprintf(_T("Error: Unable to get System Directory\n"));
	else _tprintf(_T("System Directory: %s\n"), buffer);

	// Drives Information
	_tprintf(_T("\nGetLogicalDrives: "));
	DWORD drives = GetLogicalDrives();

	for (int i = 0; i < 26; i++)
	{
		if (drives & 1 == 1)
			_tprintf(_T("%c:\\"), 'A' + i);
		drives >>= 1;
	}

	_tprintf(_T("\nGetLogicalDriveStrings:\n"));
	size = GetLogicalDriveStrings(0, NULL);
	buffer = new TCHAR[size];
	GetLogicalDriveStrings(size, buffer);

	while (*buffer)
	{
		TCHAR *type = new TCHAR[10];
		switch (GetDriveType(buffer))
		{
		case DRIVE_UNKNOWN: type = _T("Unknown"); break;
		case DRIVE_NO_ROOT_DIR: type = _T("Not Found"); break;
		case DRIVE_REMOVABLE: type = _T("Removable"); break;
		case DRIVE_FIXED: type = _T("Fixed"); break;
		case DRIVE_REMOTE: type = _T("Remote"); break;
		case DRIVE_CDROM: type = _T("CD-ROM"); break;
		case DRIVE_RAMDISK: type = _T("RAM Disk"); break;
		}

		DWORD SectorsPerCluster, BytesPerSector, NumberOfFreeClusters, TotalNumberOfClusters;
		GetDiskFreeSpace(buffer, &SectorsPerCluster, &BytesPerSector, &NumberOfFreeClusters, &TotalNumberOfClusters);

		UINT64 totalFreeB = UINT64(NumberOfFreeClusters) * SectorsPerCluster * BytesPerSector;
		UINT64 totalFreeMb = totalFreeB / pow(1024, 2); // Bytes -> MB

		_tprintf(_T(" %s %s \t %llu Bytes (%i.%i GB) \n"), buffer, type, totalFreeB, totalFreeMb / 1024, (totalFreeMb % 1024) / 100);
		buffer = &buffer[_tcslen(buffer) + 1]; // Next drive
	}

	system("pause");
	return 0;
}
