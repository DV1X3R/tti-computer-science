#include <iostream>

using namespace std;

char mostly(const char *s);

int main()
{
	char str[255];
	cout << "Enter a string> ";
	cin.getline(str, 255);
	cout << "Most used symbol: " << mostly(str) << endl;

	system("pause");
	return 0;
}

char mostly(const char *s)
{
	char maxChar = NULL;
	int maxCounter = 0, currentCounter;

	for (int i = 0; i < strlen(s); i++)
	{
		currentCounter = 0;

		for (int x = i; x < strlen(s); x++)
			if (s[i] == s[x])
				currentCounter++;

		if (currentCounter > maxCounter)
		{
			maxCounter = currentCounter;
			maxChar = s[i];
		}
	}

	return maxChar;
}
