#include <iostream>
#include <string>

using namespace std;

int main()
{
	char str[255], str2[255];
	cout << "Enter first line> ";
	cin.getline(str, 255);
	cout << "Enter second line> ";
	cin.getline(str2, 255);

	reverse(str, str + strlen(str));
	reverse(str2, str2 + strlen(str2));
	strcat_s(str2, str); 

	cout << "Merged and reversed char arrays: " << str2 << endl;

	string str3, str4;
	cout << "Enter first line> ";
	getline(cin, str3);
	cout << "Enter second line> ";
	getline(cin, str4);

	reverse(str3.begin(), str3.end());
	reverse(str4.begin(), str4.end());
	str4.append(str3.begin(), str3.end());

	cout << "Merged and reversed strings: " << str4 << endl;

	system("pause");
	return 0;
}
