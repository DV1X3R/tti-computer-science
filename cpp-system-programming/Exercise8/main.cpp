#include <windows.h>
#include <iostream>

struct MyStruct { char charVal; int intVal[500]; };

int main()
{
	_SYSTEM_INFO si;
	GetSystemInfo(&si);

	printf("Page Size: %i \n", si.dwPageSize);
	printf("Allocation Granularity: %i \n", si.dwAllocationGranularity);

	LPVOID lpReservedMem = VirtualAlloc( // Резервация области физической памяти
		NULL // область для распределения или резервирования
		, si.dwAllocationGranularity // размер области
		, MEM_RESERVE // тип распределения
		, PAGE_READWRITE); // тип защиты доступа

	LPVOID lpCommitedMem = VirtualAlloc(lpReservedMem, si.dwPageSize, MEM_COMMIT, PAGE_READWRITE); // Выполняется выделение страниц памяти для непосредственной работы с ними
	DWORD pagesCommited = 1; // Количество выделенных страниц
	DWORD shift = 0; // Сдвиг в байтах (для последовательной записи)

	printf("Virtual memory address: %p \n\n", lpCommitedMem);

	int *inum = new int(1234);
	double *dnum = new double(3.14);

	int scount = 8;
	MyStruct *structs = new MyStruct[scount];

	for (int i = 0; i < scount; i++)
	{
		structs[i].intVal[499] = i;
		structs[i].charVal = 'A' + i;
	}

	*(int*)lpCommitedMem = *inum; // Записываем int в область памяти
	printf("Integer added \t | %p \n", lpCommitedMem);
	shift += sizeof(int); // К сдвигу прибавляется размер int (4 байта)

	*(double*)((char*)lpCommitedMem + shift) = *dnum; // Записываем double по выделенному адресу со сдвигом в 4 байта
	printf("Double added \t | %p \n\n", ((char*)lpCommitedMem + shift));
	shift += sizeof(double);

	for (int i = 0; i < scount; i++)
	{
		__try
		{
			*(MyStruct*)((char*)lpCommitedMem + shift) = structs[i];
			printf("Struct %i added | %p \n", i, ((char*)lpReservedMem + shift));
			shift += sizeof(MyStruct);
		}
		__except (EXCEPTION_EXECUTE_HANDLER)
		{ // Если произошла ошибка при записи, выделяем ещё 4Кб памяти
			printf("\tException at %i structure \n", i);
			VirtualAlloc(
				((char*)lpReservedMem) + (si.dwPageSize * pagesCommited)
				, si.dwPageSize, MEM_COMMIT, PAGE_READWRITE);
			pagesCommited += 1;
			i--;
		}
	}

	printf("\n Pages used: %i \n", pagesCommited);
	printf(" %p : %i (integer) \n", lpReservedMem, *(int*)lpReservedMem);
	printf(" %p : %f (double) \n", ((int*)lpReservedMem + 1), *(double*)((int*)lpReservedMem + 1));

	for (int i = 0; i < scount; i++)
	{
		MyStruct *s = ((MyStruct*)((char*)lpCommitedMem + sizeof(int) + sizeof(double)) + i);
		printf("%p | &intVal: %p | %i / %c \n", s, &s->intVal, s->intVal[499], s->charVal);
	}

	system("pause");
	return 0;
}
