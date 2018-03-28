#include <windows.h>
#include <iostream>

using namespace std;

int main()
{
	setlocale(LC_ALL, "Russian");

	WCHAR *buffer;
	DWORD size;

	// Windows Version
	OSVERSIONINFO osvi;
	ZeroMemory(&osvi, sizeof(OSVERSIONINFO));
	osvi.dwOSVersionInfoSize = sizeof(OSVERSIONINFO);

	if (GetVersionEx(&osvi) == 0) wprintf(L"Error: Unable to get Windows Version\n");
	else wprintf(L"Windows Version: %i.%i.%i %s\n", osvi.dwMajorVersion, osvi.dwMinorVersion, LOWORD(osvi.dwBuildNumber), osvi.szCSDVersion);

	// Computer Name
	buffer = new WCHAR[MAX_COMPUTERNAME_LENGTH + 1];
	size = MAX_COMPUTERNAME_LENGTH + 1;

	if (GetComputerName(buffer, &size) == 0) wprintf(L"Error: Unable to get Computer Name\n");
	else wprintf(L"Computer Name: %s\n", buffer);

	// User Name
	buffer = new WCHAR[20 + 1];
	size = 20 + 1;

	if (GetUserName(buffer, &size) == 0) wprintf(L"Error: Unable to get User Name\n");
	else wprintf(L"User Name: %s\n", buffer);

	// User Language
	buffer = new WCHAR[LOCALE_NAME_MAX_LENGTH + 1];
	size = LOCALE_NAME_MAX_LENGTH + 1;

	if (VerLanguageName(GetUserDefaultLangID(), buffer, size) == 0) wprintf(L"Error: Unable to get User Language\n");
	else wprintf(L"User Language: %s\n", buffer);

	// Local Time
	SYSTEMTIME st;
	TIME_ZONE_INFORMATION tzi;
	GetLocalTime(&st);
	GetTimeZoneInformation(&tzi);
	wprintf(L"Current Time: %i:%i:%i \t Standard GMT%s%i \t Daylight GMT%s%i \n\n"
		, st.wHour, st.wMinute, st.wSecond
		, tzi.Bias < 0 ? L"+" : L"", -(tzi.Bias / 60)
		, tzi.Bias + tzi.DaylightBias < 0 ? L"+" : L"", -(tzi.Bias + tzi.DaylightBias) / 60);

	// Directories
	buffer = new WCHAR[MAX_PATH + 1];
	size = MAX_PATH + 1;

	if (GetCurrentDirectory(size, buffer) == 0) wprintf(L"Error: Unable to get Current Directory\n");
	else wprintf(L"Current Directory: %s\n", buffer);

	if (GetWindowsDirectory(buffer, size) == 0) wprintf(L"Error: Unable to get Windows Directory\n");
	else wprintf(L"Windows Directory: %s\n", buffer);

	if (GetSystemDirectory(buffer, size) == 0) wprintf(L"Error: Unable to get System Directory\n");
	else wprintf(L"System Directory: %s\n", buffer);

	// Drives Information
	wprintf(L"\nGetLogicalDrives: ");
	DWORD drives = GetLogicalDrives();

	for (int i = 0; i < 26; i++)
	{
		if (drives & 1 == 1)
			wprintf(L"%c:\\", 'A' + i);
		drives >>= 1;
	}

	wprintf(L"\nGetLogicalDriveStrings:\n");
	size = GetLogicalDriveStrings(0, NULL);
	buffer = new WCHAR[size];
	GetLogicalDriveStrings(size, buffer);

	while (*buffer)
	{
		WCHAR *type = new WCHAR[10];
		switch (GetDriveType(buffer))
		{
		case DRIVE_UNKNOWN: wcscpy(type, L"Unknown"); break;
		case DRIVE_NO_ROOT_DIR: wcscpy(type, L"Not Found"); break;
		case DRIVE_REMOVABLE: wcscpy(type, L"Removable"); break;
		case DRIVE_FIXED: wcscpy(type, L"Fixed"); break;
		case DRIVE_REMOTE: wcscpy(type, L"Remote"); break;
		case DRIVE_CDROM: wcscpy(type, L"CD-ROM"); break;
		case DRIVE_RAMDISK: wcscpy(type, L"RAM Disk"); break;
		}

		DWORD SectorsPerCluster, BytesPerSector, NumberOfFreeClusters, TotalNumberOfClusters;
		GetDiskFreeSpace(buffer, &SectorsPerCluster, &BytesPerSector, &NumberOfFreeClusters, &TotalNumberOfClusters);

		UINT64 totalFreeB = UINT64(NumberOfFreeClusters) * SectorsPerCluster * BytesPerSector;
		UINT64 totalFreeMb = totalFreeB / pow(1024, 2); // Bytes -> MB

		wprintf(L" %s %s \t %llu Bytes (%i.%i GB) \n", buffer, type, totalFreeB, totalFreeMb / 1024, (totalFreeMb % 1024) / 100);
		buffer = &buffer[wcslen(buffer) + 1]; // Next drive
	}

	system("pause");
	return 0;
}
