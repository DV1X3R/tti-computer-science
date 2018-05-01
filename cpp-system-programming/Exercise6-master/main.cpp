#include <windows.h>
#include <iostream>

int main(int argc, char *argv[])
{
	for (int i = 0; i < argc; i++)
		printf("argv[%i]: %s \n", i, argv[i]);

	char *cPath = "slave.exe";
	if (argc > 2)
	{
		cPath = argv[2];
		printf("\nSelecting 'slave' from the command line \n");
	}
	else
	{
		printf("\nSelecting default 'slave.exe' \n");
	}

	char pid[255] = "";
	sprintf_s(pid, "%i", GetCurrentProcessId());

	STARTUPINFO cif;
	ZeroMemory(&cif, sizeof(STARTUPINFO));
	PROCESS_INFORMATION pi;
	if (CreateProcess(
		cPath // имя исполняемого модуля
		, pid // командная строка
		, NULL // защита процесса 
		, NULL // защита потока
		, FALSE // признак наследования дескриптора
		, CREATE_NEW_CONSOLE // флаги создания процесса
		, NULL // блок новой среды окружения
		, NULL // текущий каталог
		, &cif // вид главного окна
		, &pi)) // информация о процессе
	{
		printf("Process has been successfully created; args: %s \n", pid);
		CloseHandle(pi.hProcess);
	}
	else
	{
		printf("Failure occured during the process creation! \n");
	}

	if (argc > 1)
	{
		printf("Sleeping for %s ms \n", argv[1]);
		Sleep(atoi(argv[1]));
	}
	else
	{
		printf("sleep() was skipped \n");
		system("pause");
	}

	return 0;
}
