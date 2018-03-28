#include "DatePacker.h"

unsigned int PackData(date data) {
	unsigned int encrypted = (data.day & 31);	// 5 bits
	encrypted |= (data.month & 15) << 5;		// 4 bits
	encrypted |= (data.year & 4095) << 9;		// 12 bits

	return encrypted;
}

date RepackData(unsigned int data) {
	date decrypted;
	decrypted.day = (data & 31);
	decrypted.month = (data & (15 << 5)) >> 5;
	decrypted.year = (data & 4095 << 9) >> 9;

	return decrypted;
}
