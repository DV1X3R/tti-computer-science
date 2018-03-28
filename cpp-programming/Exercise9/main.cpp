#include <iostream>
#include <fstream>

using namespace std;

struct Animal
{
	int number;
	char name[255];
	float food;
};

const char FILE_NAME[255] = "animals.bin";

void add();
void del();
void show();
void search();

int main()
{
	char c = NULL;
	while (c != 'q')
	{
		system("cls");

		cout <<
			"1: Add animal" << endl <<
			"2: Detele animal" << endl <<
			"3: Show the array content" << endl <<
			"4: Show the most insatiable animal" << endl <<
			"q: Exit" << endl << endl << "> ";

		cin >> c;
		cout << endl;

		switch (c)
		{
		case '1': add(); break;
		case '2': show(); del(); break;
		case '3': show(); break;
		case '4': search(); break;
		}

		system("pause");
	}

	return 0;
}

void add()
{
	Animal animal;
	cout << "Enter the animal number> ";
	cin >> animal.number;
	cout << "Enter the animal name> ";
	cin >> animal.name;
	cout << "Enter the required food quantity (kg)> ";
	cin >> animal.food;

	ofstream file;
	file.open(FILE_NAME, ios::binary | ios::app);
	file.write((char*)&animal, sizeof(Animal));
	file.close();
}

void show()
{
	ifstream file;
	file.open(FILE_NAME, ios::binary);

	if (!file)
	{
		cout << "Error: File not found" << endl;
		return;
	}

	file.seekg(0, ios::end);
	int size = (int)file.tellg() / sizeof(Animal);
	file.seekg(0, ios::beg);

	Animal *pAnimals = new Animal[size];
	file.read((char*)pAnimals, size * sizeof(Animal));
	file.close();

	for (int i = 0; i < size; i++)
		cout <<
		"Animal number: " << pAnimals[i].number << endl <<
		"Animal name: " << pAnimals[i].name << endl <<
		"Required food quantity (kg): " << pAnimals[i].food << endl <<
		"- - - - - - - - - - - - - - - - - - - -" << endl;
}

void del()
{
	// Create temporary variable

	ifstream file;
	file.open(FILE_NAME, ios::binary);

	if (!file)
	{
		cout << "Error: File not found" << endl;
		return;
	}

	file.seekg(0, ios::end);
	int size = (int)file.tellg() / sizeof(Animal);
	file.seekg(0, ios::beg);

	if (size == 0)
	{
		cout << "Error: File is empty" << endl;
		file.close();
		return;
	}

	Animal *pAnimals = new Animal[size];
	file.read((char*)pAnimals, size * sizeof(Animal));
	file.close();

	// Recreate .bin file excluding animal with the selected number

	int number;
	cout << "Enter the animal number you want to delete> ";
	cin >> number;

	ofstream newfile;
	newfile.open(FILE_NAME, ios::binary);

	for (int i = 0; i < size; i++)
		if (pAnimals[i].number != number)
			newfile.write((char*)&pAnimals[i], sizeof(Animal));
		else cout << "Info: Animal has been successfully deleted" << endl;
		newfile.close();
}

void search()
{
	ifstream file;
	file.open(FILE_NAME, ios::binary);

	if (!file)
	{
		cout << "Error: File not found" << endl;
		return;
	}

	Animal animal;
	char maxName[255] = "There are no more animals alive";
	double maxFood = -1;

	while (file.read((char*)&animal, sizeof(Animal)))
		if (animal.food > maxFood)
		{
			strcpy_s(maxName, animal.name);
			maxFood = animal.food;
		}

	cout << "The most insatiable animal: " << maxName << endl;
	file.close();
}
