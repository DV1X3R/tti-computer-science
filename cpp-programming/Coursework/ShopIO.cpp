#include "ShopIO.h"
#include "ShopStructs.h"
#include <fstream>

using namespace std;

char *StructSelect(StructId STRUCTNUM, int *size) {
	switch (STRUCTNUM) {
	case STRUCT_CLIENTS:
		*size = sizeof(Clients);
		return (char*)PATH_CLIENTS;
	case STRUCT_COMPUTERS:
		*size = sizeof(Computers);
		return (char*)PATH_COMPUTERS;
	case STRUCT_OFFERS:
		*size = sizeof(Offers);
		return (char*)PATH_OFFERS;
	default:
		*size = 0;
		return NULL;
	}
}

char *ReadStruct(StructId STRUCTNUM, int *count) {
	int *size = new int;
	char *path = StructSelect(STRUCTNUM, size);

	// Try open file
	ifstream inFile;
	inFile.open(path, ios::binary);
	if (!inFile)
	{
		*count = 0;
		return NULL;
	}

	// Get data size
	inFile.seekg(0, ios::end);
	*count = (int)inFile.tellg() / *size;
	char *data = new char[*count * *size];

	// Read and return data as char array
	inFile.seekg(0, ios::beg);
	inFile.read(data, *count * *size);
	inFile.close();
	return data;
}

void AddToStruct(StructId STRUCTNUM, char *data) {
	int *size = new int;
	char *path = StructSelect(STRUCTNUM, size);

	// Append data to binary file
	ofstream outFile;
	outFile.open(path, ios::binary | ios::app);
	outFile.write(data, *size);
	outFile.close();
}

bool RemoveFromStruct(StructId STRUCTNUM, int id) {
	int *size = new int;
	char *path = StructSelect(STRUCTNUM, size);
	int *count = new int;
	char *data = ReadStruct(STRUCTNUM, count);

	bool deleted = false;

	ofstream outFile;
	outFile.open(path, ios::binary);

	// Rewrite file without selected id
	switch (STRUCTNUM) {
	case STRUCT_CLIENTS:
		for (int i = 0; i < *count; i++) {
			if (((Clients*)data)[i].id != id)
				outFile.write((char*)&((Clients*)data)[i], *size);
			else
				deleted = true;
		}
		break;

	case STRUCT_COMPUTERS:
		for (int i = 0; i < *count; i++) {
			if (((Computers*)data)[i].id != id)
				outFile.write((char*)&((Computers*)data)[i], *size);
			else
				deleted = true;
		}
		break;

	case STRUCT_OFFERS:
		for (int i = 0; i < *count; i++) {
			if (((Offers*)data)[i].id != id)
				outFile.write((char*)&((Offers*)data)[i], *size);
			else
				deleted = true;
		}
		break;

	}

	outFile.close();
	return deleted;
}

void RewriteStruct(StructId STRUCTNUM, char *data, int *count) {
	int *size = new int;
	char *path = StructSelect(STRUCTNUM, size);

	ofstream outFile;
	outFile.open(path, ios::binary);
	outFile.write(data, *size * *count);
}
