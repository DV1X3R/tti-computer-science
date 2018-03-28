#include <iostream>

using namespace std;

struct Animal
{
	int number;
	char name[255];
	double food;
};

int main()
{
	int n;
	cout << "Enter the number of animals> ";
	cin >> n;

	Animal *pAnimals = new Animal[n];

	for (int i = 0; i < n; i++)
	{
		cout << "Enter the animal number> ";
		cin >> pAnimals[i].number;
		cout << "Enter the animal name> ";
		cin >> pAnimals[i].name;
		cout << "Enter the required food quantity (kg)> ";
		cin >> pAnimals[i].food;
		cout << "- - - - - - - - - - - - - - - - - - - -" << endl;
	}

	char c = NULL;
	while (c != 'q')
	{
		system("cls");

		cout <<
			"1: Show the array content" << endl <<
			"2: Show the most insatiable animal" << endl <<
			"q: Quit" << endl << endl << "> ";

		cin >> c;
		cout << endl;

		switch (c)
		{
		case '1':
			for (int i = 0; i < n; i++)
				cout <<
				"Animal number: " << pAnimals[i].number << endl <<
				"Animal name: " << pAnimals[i].name << endl <<
				"Required food quantity (kg): " << pAnimals[i].food << endl <<
				"- - - - - - - - - - - - - - - - - - - -" << endl;
			break;

		case '2':
			int maxIndex = -1;
			double maxFood = -1;

			for (int i = 0; i < n; i++)
				if (pAnimals[i].food > maxFood)
				{
					maxIndex = i;
					maxFood = pAnimals[i].food;
				}

			if (maxIndex == -1) cout << "There are no more animals alive" << endl;
			else cout << "The most insatiable animal: " << pAnimals[maxIndex].name << endl;
			break;
		}

		system("pause");
	}

	return 0;
}
