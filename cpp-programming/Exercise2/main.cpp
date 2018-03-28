#include <iostream>

using namespace std;

int main()
{
	double x;
	cout << "Enter the value of 'x'> ";
	cin >> x;

	if (x <= -2)
		cout << "f1(x) = " << 1 / pow(x, 2) << endl;
	else if (x > -2 && x <= 2)
		cout << "f2(x) = " << sin(x + 1) << endl;
	else
		cout << "f3(x) = " << log(x) << endl;

	system("pause");
	return 0;
}
