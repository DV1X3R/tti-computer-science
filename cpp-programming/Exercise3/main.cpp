#include <iostream>

using namespace std;

int main()
{
	int a, b, c = 0;
	cout << "Enter the first number> ";
	cin >> a;
	cout << "Enter the second number> ";
	cin >> b;

	for (int i = a; i <= b; i++)
		if (i % 10 == 7 || i % 10 == -7)
			c++;

	cout << "Count of numbers for which the last digit is '7': " << c << endl;

	system("pause");
	return 0;
}
