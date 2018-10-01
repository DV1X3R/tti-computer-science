#include <iostream>

using namespace std;

int main()
{
	int n;
	cout << "Enter the size of the array> ";
	cin >> n;

	int *pArr = new int[n];

	for (int i = 0; i < n; i++)
	{
		cout << "Enter the value of '" << i << "' array element> ";
		cin >> pArr[i];
	}

	// 1. Product of negative elements

	int negativeProduct = 1;
	bool hasNegativeElements = false;

	for (int i = 0; i < n; i++)
		if (pArr[i] < 0)
		{
			hasNegativeElements = true;
			negativeProduct *= pArr[i];
		}

	if (hasNegativeElements)
		cout << "1. Product of negative elements: " << negativeProduct << endl;
	else
		cout << "1. Array does not have negative elements" << endl;

	// 2. Sum of positive elements located before max element

	int positiveMaxIndex = 0, positiveSum = 0;

	for (int i = 0; i < n; i++)
		if (pArr[i] > pArr[positiveMaxIndex])
			positiveMaxIndex = i;

	for (int i = 0; i < positiveMaxIndex; i++)
		if (pArr[i] > 0)
			positiveSum += pArr[i];

	cout << "2. Sum of positive elements located before max element: " << positiveSum << endl;

	// 3. Reversed array

	int tmp;
	for (int i = 0; i < n / 2; i++)
	{
		tmp = pArr[i];
		pArr[i] = pArr[n - i - 1];
		pArr[n - i - 1] = tmp;
	}

	cout << "3. Reversed array: ";
	for (int i = 0; i < n; i++)
		cout << pArr[i] << " ";
	cout << endl;

	system("pause");
	return 0;
}
