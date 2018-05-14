#include <windows.h>
#include <iostream>

using namespace std;

int n;
int *arr;
HANDLE hSemaphore;

DWORD WINAPI worker(LPDWORD lpData)
{
	cout << "worker> attempting semaphore..." << endl;
	WaitForSingleObject(hSemaphore, INFINITE);
	cout << "worker> got semaphore" << endl;

	int avg = 0;
	for (int i = 0; i < n; i++)
	{
		avg += arr[i];
		Sleep(12);
	}

	avg /= n;
	cout << "worker> average value: " << avg << endl;

	ReleaseSemaphore(hSemaphore, 1, NULL);
	cout << "worker> semaphore released" << endl;
	return avg;
}

int main()
{
	cout << "Enter the size of the array> ";
	cin >> n;
	arr = new int[n];

	for (int i = 0; i < n; i++)
	{
		cout << "Enter the value of '" << i << "' array element> ";
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

	cout << "main> attempting semaphore..." << endl;
	WaitForSingleObject(hSemaphore, INFINITE);
	cout << "main> got semaphore" << endl;

	int min = arr[0];
	int max = arr[0];
	for (int i = 0; i < n; i++)
	{
		if (min > arr[i]) min = arr[i];
		if (max < arr[i]) max = arr[i];
		Sleep(7);
	}

	cout << "main> min value: " << min << endl;
	cout << "main> max value: " << max << endl;

	ReleaseSemaphore(
		hSemaphore // hSemaphore
		, 1 // lReleaseCount
		, NULL); // lpPreviousCount

	cout << "main> semaphore released" << endl;
	
	cout << "main> waiting for 'worker' thread..." << endl;
	WaitForSingleObject(hWorker, INFINITE);
	DWORD avg;
	GetExitCodeThread(hWorker, &avg);
	CloseHandle(hWorker);

	int count = 0;
	for (int i = 0; i < n; i++)
		if (avg < arr[i]) count++;

	cout << "main> number of elements greater than average value: " << count << endl;

	system("pause");
	return 0;
}

