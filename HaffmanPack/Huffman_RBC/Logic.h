#pragma once

#include "Tree.h"
#include <fstream>
#include <iostream>

namespace HuffmanLogic {

	typedef unsigned char byte;

	//bool isEmpty(std::ifstream& pFile);
	bool getBit(uint64_t code, int pointer);
	int getBitLTG(int code, int pointer); //Lower-To-Greater
	int binaryLen(uint64_t msg);

	void writeHeader(uint64_t* codewords, char* uniqueSymbs, int pairs, int zeros, byte* output_stream, int* output_stream_len);
	int writeByBit(uint64_t* codewords, char* uniqueSymbs, byte* input_stream, int input_stream_len, byte* output_stream, int* output_stream_len);
}