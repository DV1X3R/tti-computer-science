#include <windows.h>
#include <iostream>

using namespace std;

int n;
int *arr;
HANDLE hSemaphore;

DWORD WINAPI worker(LPDWORD lpData)
{
	printf("worker> attempting semaphore...\n");
	WaitForSingleObject(hSemaphore, INFINITE);
	printf("worker> got semaphore...\n");

	int avg = 0;
	for (int i = 0; i < n; i++)
	{
		avg += arr[i];
		Sleep(12);
	}

	avg /= n;
	printf("worker> average value: %i\n", avg);

	ReleaseSemaphore(hSemaphore, 1, NULL);
	printf("worker> semaphore released\n");
	return avg;
}

int main()
{
	printf("main> Enter the size of the array> ");
	cin >> n;
	arr = new int[n];

	for (int i = 0; i < n; i++)
	{
		printf("main> Enter the value of '%i' aray element> ", i);
		cin >> arr[i];
	}

	hSemaphore = CreateSemaphore(
		NULL // lpSemaphoreAttributes
		, 1 // lInitialCount
		, 1, // lMaximumCount
		NULL); // lpName

	DWORD tid;
	HANDLE hWorker = CreateThread(
		NULL // атрибуты защиты
		, 0 // размер стека потока в байтах
		,(LPTHREAD_START_ROUTINE)worker // адрес функции
		, NULL // адрес параметра
		, 0 // флаги создания потока
		, &tid); // идентификатор потока

	printf("main> attempting semaphore...\n");
	WaitForSingleObject(hSemaphore, INFINITE);
	printf("main> got semaphore...\n");

	int min = arr[0];
	int max = arr[0];
	for (int i = 0; i < n; i++)
	{
		if (min > arr[i]) min = arr[i];
		if (max < arr[i]) max = arr[i];
		Sleep(7);
	}

	printf("main> min value: %i\n", min);
	printf("main> max value: %i\n", max);

	ReleaseSemaphore(
		hSemaphore // hSemaphore
		, 1 // lReleaseCount
		, NULL); // lpPreviousCount

	printf("main> semaphore released\n");
	
	printf("main> waiting for 'worker' thread...\n");
	WaitForSingleObject(hWorker, INFINITE);
	DWORD avg;
	GetExitCodeThread(hWorker, &avg);
	CloseHandle(hWorker);

	int count = 0;
	for (int i = 0; i < n; i++)
		if (avg < arr[i]) count++;

	printf("main> number of elements greater than average value: %i\n", count);

	system("pause");
	return 0;
}
