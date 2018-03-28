#include <iostream>
#include <vector>
#include <time.h> // random seed

using namespace std;

void printv(vector<int> v);

int main()
{
	vector<int> v;
	srand(time(NULL));

	for (int i = 0; i < 100; i++)
		v.push_back((rand() % 1000) + 1);
	
	cout << "  Source vector:" << endl;
	printv(v);
	
	for (int i = 10; i < v.size(); i += 11)
	{
		auto it = v.begin() + i;
		v.insert(it, rand() * -1);
	}
	v.insert(v.end(), rand() * -1);

	cout << endl << "  Step 2 [added negative numbers]:" << endl;
	printv(v);

	int negativeSum = 0;
	for (auto it = v.begin(); it != v.end(); it++)
	{
		if (*it < 0)
			negativeSum += *it;
	}

	cout << endl << "Sum of the negative numbers: " << negativeSum << endl;

	for (int i = 0; i < v.size(); i++)
	{
		auto it = v.begin() + i;
		if (*it > 500)
		{
			v.erase(it);
			i--; // ¯\_(ツ)_/¯
		}
	}

	cout << endl << "  Step 3 [removed values > 500]:" << endl;
	printv(v);

	reverse(v.begin(), v.end());
	cout << endl << "  Step 4 [reversed vector]:" << endl;
	printv(v);

	system("pause");
	return 0;
}

void printv(vector<int> v)
{
	for (int i = 0; i < v.size(); i++)
		cout << v[i] << " ";
	cout << endl;
}
