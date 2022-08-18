#include "pch.h"

#include "Logic.h"

namespace HuffmanLogic {

	//bool isEmpty(std::ifstream& pFile)
	//{
	//	pFile.seekg(0, ios::end);
	//	if (pFile.tellg() == 0)
	//		return true;
	//	else
	//	{
	//		pFile.clear();
	//		pFile.seekg(0, ios_base::beg);
	//		return false;
	//	}
	//}

	bool getBit(uint64_t code, int pointer)
	{
		//int length = 0;
		//length = binaryLen(code) - 1;
		uint64_t pointerBit = 0, temp = 0;
		pointerBit = pow(2, binaryLen(code) - pointer);
		temp = code ^ pointerBit;
		if (temp < code)
			return true;
		else
			return false;
	}

	int getBitLTG(int code, int pointer)
	{
		int pointerBit = 0, temp = 0;
		pointerBit = pow(2, pointer);
		temp = code ^ pointerBit;
		if (temp < code)
			return 1;
		else
			return 0;
	}

	int binaryLen(uint64_t msg)
	{
		int i;
		for (i = 0; msg >= pow(2, i); i++) {}
		i--;
		return i;
	}

	void writeHeader(uint64_t* codewords, char* uniqueSymbs, int pairs, int zeros, byte* output_stream, int* output_stream_len) //maybe make output as char
	{
		auto streamPut = [](byte contentByte, byte* output_stream, int* output_stream_len)
		{
			int current_size = *output_stream_len;
			output_stream = (byte*)realloc(output_stream, current_size + 1);
			output_stream[current_size] = contentByte;
		};
		
		//target->put((char)pairs);
		streamPut((byte)pairs, output_stream, output_stream_len);
		(*output_stream_len)++;
		//target->put((char)zeros);
		streamPut((byte)zeros, output_stream, output_stream_len);
		(*output_stream_len)++;
		char buffer = 0;
		uint64_t tempCode = 0;
		for (uint64_t i = 0; i < pairs + 1; i++) //as we write pairs-1 because of 256
		{
			//target->put(uniqueSymbs[i]);
			streamPut(uniqueSymbs[i], output_stream, output_stream_len);
			for (uint64_t j = 0; j < 8; j++)
			{
				tempCode = codewords[i];
				tempCode <<= j * 8;
				tempCode >>= 7 * 8;
				buffer = (char)tempCode;
				//target->put(buffer);
				streamPut(buffer, output_stream, output_stream_len);
				(*output_stream_len)++;
			}
		}
		/*for (uint16_t i = 0; i < pairs; i++)
		{
			*(target) << uniqueSymbs[i] << codewords[i];
		}*/
	}

	int writeByBit(uint64_t* codewords, char* uniqueSymbs, byte* input_stream, int input_stream_len, byte* output_stream, int* output_stream_len)
	{
		auto streamPut = [](byte contentByte, byte* output_stream, int* output_stream_len)
		{
			int current_size = *output_stream_len;
			output_stream = (byte*)realloc(output_stream, current_size + 1);
			output_stream[current_size] = contentByte;
		};
		int input_stream_pointer = 0;
		auto inputReadByte = [input_stream_pointer, input_stream](char* byte_) mutable
		{
			*byte_ = input_stream[input_stream_pointer];
			input_stream_pointer++;
		};

		int marker1 = 0, marker2 = 1;
		uint8_t buffer = 0;
		char symb;
		uint64_t tempCode = 0;
		int signBits = 0;
		int input_stream_size = input_stream_len;
		//reset stream
		//content->clear();
		//content->seekg(0, ios_base::beg);
		//for (uint64_t i = 0; !content->eof(); i++)
		for (uint64_t i = 0; i < input_stream_size; i++)
		{
			if (marker2 == 1)
			{
				symb = 0;
				//content->read(&symb, sizeof(char));
				inputReadByte(&symb);
				int j = 0;
				while (symb != uniqueSymbs[j])
					j++;
				tempCode = codewords[j];
			}
			while (marker1 < 8)
			{
				if (marker2 > binaryLen(tempCode))
				{
					marker2 = 1;
					break;
				}
				if (getBit(tempCode, marker2))
				{
					buffer <<= 1;
					buffer++;
					signBits++;
				}
				else
				{
					buffer <<= 1;
					signBits++;
				}
				marker2++;
				marker1++;
			}
			if (marker1 == 8)
			{
				marker1 = 0;
				//write to file
				streamPut(buffer, output_stream, output_stream_len);
				(*output_stream_len)++;
				//target->put(buffer);
				buffer = 0;
				signBits = 0;
			}
			//guess not this is not needed
			//else	// checking if next is EOF after last read symb in content
			//{
			//	content->get();
			//	if (content->eof())
			//		break;
			//	else
			//		content->seekg(-1, ios::cur);
			//}
		}
		if (buffer != 0)
		{
			buffer <<= 8 - signBits;
			//target->put(buffer);
			streamPut(buffer, output_stream, output_stream_len);
			(*output_stream_len)++;
			buffer = 0;
			//end of all
			return 8 - signBits;
		}
		else
			return 0;
	}
}