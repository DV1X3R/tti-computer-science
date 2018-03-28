#include "FindFile.h"

using namespace std;

vector<FileInfo> FindFile(wstring folderPath, wstring fileName, bool recursive)
{
	vector<FileInfo> v; // Вектор с найдеными файлами и директориями

	WIN32_FIND_DATA wfd;
	HANDLE hf = FindFirstFile((folderPath + L"\\" + fileName).c_str(), &wfd);

	if (hf != INVALID_HANDLE_VALUE)
	{
		do
		{
			// Файл найден - записываем информацию в вектор

			SYSTEMTIME st;
			FileTimeToSystemTime(&wfd.ftCreationTime, &st);

			FileInfo fi;
			wcscpy_s(fi.FileName, wfd.cFileName);
			wcscpy_s(fi.Path, folderPath.c_str());
			fi.IsDirectory = (wfd.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY) ? true : false;
			fi.CreatedDate = st;

			v.push_back(fi);

		} while (FindNextFile(hf, &wfd) != 0); // Ищем следующий файл в текущей директории

		FindClose(hf);
	}

	if (recursive) // Если выбран рекурсивный поиск
	{
		hf = FindFirstFile((folderPath + L"\\*").c_str(), &wfd); // Все файлы в текущей папке

		if (hf != INVALID_HANDLE_VALUE)
		{
			do
			{
				if ((wfd.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY) // Если нашли папку
					&& wstring(wfd.cFileName) != L"." && wstring(wfd.cFileName) != L"..")
				{
					vector<FileInfo> r = FindFile(folderPath + L"\\" + wfd.cFileName, fileName, true); // Ищем в ней нужный файл, и записываем результат в вектор
					v.insert(v.end(), r.begin(), r.end()); // Конкатинируем векторы
				}
			} while (FindNextFile(hf, &wfd) != 0);

			FindClose(hf);
		}
	}

	return v;
}

int WriteFileInfo(wstring path, vector<FileInfo> info)
{
	if (info.size() == 0) return 2; // Если структура пустая - возвращаем код ошибки

	HANDLE hf = CreateFile(path.c_str(), GENERIC_WRITE, 0, NULL, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);
	if (hf == INVALID_HANDLE_VALUE) return 1;
	else
	{
		DWORD wsize;

		// Запись структур из вектора в файл
		for (auto it = info.begin(); it < info.end(); it++)
			WriteFile(hf, it._Ptr, sizeof(FileInfo), &wsize, NULL);

		CloseHandle(hf);
		return 0;
	}
}

wstring ReadFileInfo(wstring path)
{
	wstring result;

	HANDLE hf = CreateFile(path.c_str(), GENERIC_READ, 0, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
	if (hf == INVALID_HANDLE_VALUE) result = L"Error: Unable to open file\n";
	else
	{
		FileInfo buffer;
		DWORD size = GetFileSize(hf, NULL);
		DWORD rsize;

		// Расчитываем количество структур с данными в файле, и прогоняем цикл
		for (int i = 0; i < size / sizeof(FileInfo); i++)
		{
			ReadFile(hf, &buffer, sizeof(FileInfo), &rsize, NULL);
			result += wstring(buffer.FileName)
				+ (buffer.IsDirectory ? L" <DIR>" : L"")
				+ L"\tCreated Date: "
				+ to_wstring(buffer.CreatedDate.wDay) + L"/" + to_wstring(buffer.CreatedDate.wMonth) + L"/" + to_wstring(buffer.CreatedDate.wYear)
				+ L"\n";
		}

		CloseHandle(hf);
	}

	return result;
}
