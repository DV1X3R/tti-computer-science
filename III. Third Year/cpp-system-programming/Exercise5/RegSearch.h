#pragma once
#include <windows.h>
#include <iostream>
#include <tchar.h>

bool RegSearch(HKEY hRootKey, TCHAR *cPath, bool bSearch, TCHAR *sSearchKeyName);
