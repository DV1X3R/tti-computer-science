#include <iostream>

using namespace std;

int main()
{
	int n, i = 1;
	cout << "Enter a number> ";
	cin >> n;

	while (i < n)
		i *= 3;

	if (i == n)
		cout << "Yes, " << n << " is a power of 3" << endl;
	else
		cout << "No, " << n << " is not a power of 3" << endl;

	system("pause");
	return 0;
}
