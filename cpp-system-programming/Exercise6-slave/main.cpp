#include <windows.h>
#include <iostream>

int main(int argc, char *argv[])
{
	HANDLE hProc = OpenProcess(PROCESS_ALL_ACCESS, NULL, atoi(argv[0]));
	if (hProc == INVALID_HANDLE_VALUE || hProc == NULL)
	{
		printf("Failure occured during the finding %s process! \n", argv[0]);
		system("pause");
	}
	else
	{
		printf("Waiting for the %s process... \n", argv[0]);
		switch (WaitForSingleObject(hProc, 20000))
		{
		case WAIT_ABANDONED: printf("Waiting finished: ABANDONED \n"); break;
		case WAIT_OBJECT_0: printf("Waiting finished: SUCCESS \n"); break;
		case WAIT_TIMEOUT: printf("Waiting finished: TIMEOUT \n"); break;
		case WAIT_FAILED: printf("Waiting finished: FAILED \n"); break;
		default: printf("Waiting finished: UNKNOWN_ERROR \n"); break;
		}

		CloseHandle(hProc);
		Sleep(2000);
	}

	return 0;
}
