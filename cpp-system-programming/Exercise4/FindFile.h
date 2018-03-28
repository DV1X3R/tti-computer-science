#pragma once
#include <windows.h>
#include <string>
#include <vector>

struct FileInfo
{
	WCHAR FileName[MAX_PATH];
	WCHAR Path[MAX_PATH];
	bool IsDirectory;
	SYSTEMTIME CreatedDate;
};

std::vector<FileInfo> FindFile(std::wstring folderPath, std::wstring fileName, bool recursive);
int WriteFileInfo(std::wstring path, std::vector<FileInfo> info);
std::wstring ReadFileInfo(std::wstring path);
