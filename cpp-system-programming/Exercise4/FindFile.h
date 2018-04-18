#pragma once
#include <windows.h>
#include <string>
#include <vector>

struct FileInfo
{
	WCHAR FileName[MAX_PATH + 1];
	WCHAR Path[MAX_PATH + 1];
	bool IsDirectory;
	SYSTEMTIME CreatedDate;
};

std::vector<FileInfo> FindFile(std::wstring folderPath, std::wstring fileName, bool recursive);
int WriteFileInfo(std::wstring path, std::vector<FileInfo> info);
std::wstring ReadFileInfo(std::wstring path);
