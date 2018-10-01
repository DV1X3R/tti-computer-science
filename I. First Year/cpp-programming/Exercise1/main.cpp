#include <iostream>

using namespace std;

int main()
{
	double a;
	cout << "Enter the value of 'a'> ";
	cin >> a;

	if (a != 0)
		cout << "z1 = " <<
		pow((1 + a + pow(a, 2)) / (2 * a + pow(a, 2)) + 2 - (1 - a + pow(a, 2)) / (2 * a - pow(a, 2)), -1) * (5 - 2 * pow(a, 2))
		<< endl;
	else
		cout << "No solution for z1. [a=0]" << endl;

	cout << "z2 = " << (4 - pow(a, 2)) / 2 << endl;

	system("pause");
	return 0;
}
