#pragma once
#include "Shop.h"

const char PATH_CLIENTS[] = "clients.bin";
const char PATH_COMPUTERS[] = "computers.bin";
const char PATH_OFFERS[] = "offers.bin";

char *ReadStruct(StructId STRUCTNUM, int *count);
void AddToStruct(StructId STRUCTNUM, char *data);
bool RemoveFromStruct(StructId STRUCTNUM, int id);
void RewriteStruct(StructId STRUCTNUM, char *data, int *count);

void ShowStruct(StructId STRUCTNUM, char *data, int *count);
