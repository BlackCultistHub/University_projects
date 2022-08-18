#include "pch.h"

#include "Huffman_RBCLib.h"


	bool Encode(byte* input_stream, int input_stream_len, byte* output_stream, int* output_stream_len, uint64_t* huffman_codes, int* huffman_codes_len, char* values, int* values_len, byte* header_stream, int* header_stream_len)
	{
		try
		{
			int input_stream_size = input_stream_len;

			char temp_byte;
			unsigned char temp_ubyte;
			int palette[256];
			int count = 0;
			for (uint16_t i = 0; i < sizeof(palette) / sizeof(int); i++)
				palette[i] = 0;
			for (uint64_t i = 0; i < input_stream_size; i++)
			{
				//file.read(&temp_byte, sizeof(char));
				temp_byte = input_stream[i];
				temp_ubyte = temp_byte;
				palette[temp_ubyte]++;
				//cout << i << endl;
			}
			for (uint16_t i = 0; i < 256; i++)
			{
				if (palette[i] != 0)
				{
					//cout << i << " = " << palette[i] << endl;
					count++;
				}
			}
			int summ = 0;
			for (int i = 0; i < 256; i++)
				summ += palette[i];
			//cout << "cells used: " << count << endl;
			//cout << "used cells summ: " << summ << endl;

			char* USymb;
			int* chance;
			int AnotherCounter = 0;
			USymb = (char*)malloc(count * sizeof(char));
			chance = (int*)malloc(count * sizeof(int));

			for (uint16_t i = 0; i < 256; i++)
			{
				if (palette[i] != 0)
				{
					USymb[AnotherCounter] = i;
					chance[AnotherCounter] = palette[i];
					AnotherCounter++;
				}
			}
			//values = USymb; //send values up!!!!!!!!!!!!!
			*values_len = count; //send up len
			values = (char*)realloc(values, count * sizeof(char));
			for (int i = 0; i < count; i++)
			{
				values[i] = USymb[i];
			}
			//cout << "[-- counter : " << AnotherCounter << endl;
			HuffmanCore::Tree huff(USymb, count, chance);
			huff.genTree();
			huff.deGenTree();
			uint64_t* codes = huff.getCodes();
			//huffman_codes = codes; //send upp!!!!!!!!!!!!!!!!!!!!!
			*huffman_codes_len = count;
			huffman_codes = (uint64_t*)realloc(huffman_codes, count * sizeof(uint64_t));
			for (int i = 0; i < count; i++)
			{
				huffman_codes[i] = codes[i];
			}
			//cout << "\t-------------------------------\n" <<
			//	"\t|        [Codes table]        |\n" <<
			//	"\t|_____________________________|" << endl;
			//for (uint16_t i = 0; i < count; i++)
			//	printf("\t| Symbol | %4d | Code | %4d |\n", (int)USymb[i], codes[i]);
			//cout << "\t-------------------------------" << endl;

			//call write compressed
			int zeros = 0;
			zeros = HuffmanLogic::writeByBit(codes, USymb, input_stream, input_stream_size, output_stream, output_stream_len);

			/*if (header_stream != nullptr)
			{
				HuffmanLogic::writeHeader(codes, USymb, count - 1, zeros, header_stream, header_stream_len);
			}*/
			return true;
		}
		catch (std::exception ex)
		{
			return false;
		}
	}

	bool Encode_getCodes(byte* input_stream, int input_stream_len, int* huffman_codes, int* huffman_codes_len, int* values, int* values_len)
	{
		try
		{
			int input_stream_size = input_stream_len;
			std::cout << "input_stream_size = " << input_stream_size << " \n";
			char temp_byte;
			unsigned char temp_ubyte;
			int palette[256];
			int count = 0;
			for (uint16_t i = 0; i < sizeof(palette) / sizeof(int); i++)
				palette[i] = 0;
			for (uint64_t i = 0; i < input_stream_size; i++)
			{
				std::cout << "input_stream[i] = " << (int)input_stream[i] << " \n";
				temp_byte = input_stream[i];
				std::cout << "temp_byte = " << (int)temp_byte << " \n";
				temp_ubyte = temp_byte;
				std::cout << "temp_ubyte = " << (int)temp_ubyte << " \n";
				palette[temp_ubyte]++;
			}
			for (uint16_t i = 0; i < 256; i++)
			{
				if (palette[i] != 0)
				{
					count++;
				}
			}
			int summ = 0;
			for (int i = 0; i < 256; i++)
				summ += palette[i];
			char* USymb;
			int* chance;
			int AnotherCounter = 0;
			USymb = (char*)malloc(count * sizeof(char));
			chance = (int*)malloc(count * sizeof(int));

			for (uint16_t i = 0; i < 256; i++)
			{
				if (palette[i] != 0)
				{
					USymb[AnotherCounter] = i;
					chance[AnotherCounter] = palette[i];
					AnotherCounter++;
				}
			}
			*values_len = count; //send up len
			*values = (int)USymb;
			std::cout << "Address of values array: " << *values << "\n";
			std::cout << "Creating tree...\n";
			HuffmanCore::Tree huff(USymb, count, chance);
			std::cout << "count = " << count << " \n";
			std::cout << "Gen tree...\n";
			huff.genTree();
			std::cout << "DeGen tree...\n";
			huff.deGenTree();
			std::cout << "Getting codes...\n";
			uint64_t* codes = huff.getCodes();
			std::cout << "SAVING codes len..\n";
			*huffman_codes_len = count;
			std::cout << "SAVING codes..\n";
			*huffman_codes = (int)codes;
			std::cout << "Address of codes array: " << *huffman_codes << "\n";
			std::cout << "\t-------------------------------\n" <<
				"\t|        [Codes table]        |\n" <<
				"\t|_____________________________|" << std::endl;
			for (uint16_t i = 0; i < count; i++)
				printf("\t| Symbol | %4d | Code | %4d |\n", (int)USymb[i], codes[i]);
			std::cout << "\t-------------------------------" << std::endl;

			//free(USymb);
			//free(chance);

			return true;
		}
		catch (std::exception ex)
		{
			return false;
		}
	}

	bool Encode_getCodes_Freq(byte* input_stream, int input_stream_len, int* huffman_codes, int* huffman_codes_len, int* values, int* values_len, int* freqs, int* freqs_len)
	{
		try
		{
			int input_stream_size = input_stream_len;
			std::cout << "input_stream_size = " << input_stream_size << " \n";
			char temp_byte;
			unsigned char temp_ubyte;
			int palette[256];
			int count = 0;
			for (uint16_t i = 0; i < sizeof(palette) / sizeof(int); i++)
				palette[i] = 0;
			for (uint64_t i = 0; i < input_stream_size; i++)
			{
				//std::cout << "input_stream[i] = " << (int)input_stream[i] << " \n";
				temp_byte = input_stream[i];
				//std::cout << "temp_byte = " << (int)temp_byte << " \n";
				temp_ubyte = temp_byte;
				//std::cout << "temp_ubyte = " << (int)temp_ubyte << " \n";
				palette[temp_ubyte]++;
			}
			for (uint16_t i = 0; i < 256; i++)
			{
				if (palette[i] != 0)
				{
					count++;
				}
			}

			if (count == 1)
			{
				char symb = input_stream[0];
				uint64_t code = 2;
				int freq = 1;

				char* Symbs = (char*)malloc(sizeof(char));
				Symbs[0] = symb;
				*values = (int)Symbs;

				uint64_t* Codes = (uint64_t*)malloc(sizeof(uint64_t));
				Codes[0] = code;
				*huffman_codes = (int)Codes;

				int* Freqs = (int*)malloc(sizeof(int));
				Freqs[0] = freq;
				*freqs = (int)Freqs;

				*freqs_len = 1;
				*huffman_codes_len = 1;
				*values_len = 1;
				return true;
			}

			int summ = 0;
			for (int i = 0; i < 256; i++)
				summ += palette[i];
			char* USymb;
			int* chance;
			int AnotherCounter = 0;
			USymb = (char*)malloc(count * sizeof(char));
			chance = (int*)malloc(count * sizeof(int));

			for (uint16_t i = 0; i < 256; i++)
			{
				if (palette[i] != 0)
				{
					USymb[AnotherCounter] = i;
					chance[AnotherCounter] = palette[i];
					AnotherCounter++;
				}
			}
			*values_len = count; //send up len
			*values = (int)USymb;
			*freqs = (int)chance;
			*freqs_len = count;
			std::cout << "Address of values array: " << *values << "\n";
			std::cout << "Creating tree...\n";
			HuffmanCore::Tree huff(USymb, count, chance);
			std::cout << "count = " << count << " \n";
			std::cout << "Gen tree...\n";
			huff.genTree();
			std::cout << "DeGen tree...\n";
			huff.deGenTree();
			std::cout << "Getting codes...\n";
			uint64_t* codes = huff.getCodes();
			std::cout << "SAVING codes len..\n";
			*huffman_codes_len = count;
			std::cout << "SAVING codes..\n";
			*huffman_codes = (int)codes;
			std::cout << "Address of codes array: " << *huffman_codes << "\n";
			std::cout << "\t-------------------------------\n" <<
				"\t|        [Codes table]        |\n" <<
				"\t|_____________________________|" << std::endl;
			for (uint16_t i = 0; i < count; i++)
				printf("\t| Symbol | %4d | Code | %4d |\n", (int)USymb[i], codes[i]);
			std::cout << "\t-------------------------------" << std::endl;

			return true;
		}
		catch (std::exception ex)
		{
			return false;
		}
	}

	bool Encode_getCodes_Wrapper(int ADDR_input_stream, int input_stream_len, int ADDR_OF_ARRAY_huffman_codes, int ADDR_huffman_codes_len, int ADDR_OF_ARRAY_values, int ADDR_values_len)
	{
		//byte * input_stream, int input_stream_len, uint16_t** huffman_codes, int* huffman_codes_len, char** values, int* values_len
		//int* ptr = reinterpret_cast<int*>(0x12345678);

		//simple casts
		byte* pinput_stream = reinterpret_cast<byte*>(ADDR_input_stream);
		int* phuffman_codes_len = reinterpret_cast<int*>(ADDR_huffman_codes_len);
		int* pvalues_len = reinterpret_cast<int*>(ADDR_values_len);

		//harder casts
		int* pphuffman_codes = reinterpret_cast<int*>(ADDR_OF_ARRAY_huffman_codes);
		int* ppvalues = reinterpret_cast<int*>(ADDR_OF_ARRAY_values);

		std::cout << "ADDRESSES: \nInput stream: " <<
			std::hex << ADDR_input_stream << "\n" <<
			"Pointer of addr of codes: " << std::hex << ADDR_OF_ARRAY_huffman_codes << "\n" <<
			"Codes len: " << std::hex << ADDR_huffman_codes_len << "\n" <<
			"Pointer of addr of values: " << std::hex << ADDR_OF_ARRAY_values << "\n" <<
			"values len: " << std::hex << ADDR_values_len << "\n";

		return Encode_getCodes(pinput_stream, input_stream_len, pphuffman_codes, phuffman_codes_len, ppvalues, pvalues_len);
	}

	bool Encode_getCodes_Freq_Wrapper(int ADDR_input_stream, int input_stream_len, 
								int ADDR_OF_ARRAY_huffman_codes, int ADDR_huffman_codes_len,
								int ADDR_OF_ARRAY_values, int ADDR_values_len,
								int ADDR_OF_ARRAY_freqs, int ADDR_freqs_len)
	{
		//byte * input_stream, int input_stream_len, uint16_t** huffman_codes, int* huffman_codes_len, char** values, int* values_len
		//int* ptr = reinterpret_cast<int*>(0x12345678);

		//simple casts
		byte* pinput_stream = reinterpret_cast<byte*>(ADDR_input_stream);
		int* phuffman_codes_len = reinterpret_cast<int*>(ADDR_huffman_codes_len);
		int* pvalues_len = reinterpret_cast<int*>(ADDR_values_len);
		int* pfreqs_len = reinterpret_cast<int*>(ADDR_freqs_len);

		//harder casts
		int* pphuffman_codes = reinterpret_cast<int*>(ADDR_OF_ARRAY_huffman_codes);
		int* ppvalues = reinterpret_cast<int*>(ADDR_OF_ARRAY_values);
		int* ppfreqs = reinterpret_cast<int*>(ADDR_OF_ARRAY_freqs);

		std::cout << "ADDRESSES: \nInput stream: " <<
			std::hex << ADDR_input_stream << "\n" <<
			"Pointer of addr of codes: " << std::hex << ADDR_OF_ARRAY_huffman_codes << "\n" <<
			"Codes len: " << std::hex << ADDR_huffman_codes_len << "\n" <<
			"Pointer of addr of values: " << std::hex << ADDR_OF_ARRAY_values << "\n" <<
			"values len: " << std::hex << ADDR_values_len << "\n";

		return Encode_getCodes_Freq(pinput_stream, input_stream_len, pphuffman_codes, phuffman_codes_len, ppvalues, pvalues_len, ppfreqs, pfreqs_len);
	}

	bool Free_Encode_getCodes_Wrapper(int ADDR_OF_ARRAY_huffman_codes, int ADDR_OF_ARRAY_values)
	{
		try
		{
			std::cout << "FREE ADDRESSES: \nCodes: " <<
				std::hex << ADDR_OF_ARRAY_huffman_codes << "\n" <<
				"values: " << std::hex << ADDR_OF_ARRAY_values << "\n";
			int* pphuffman_codes = reinterpret_cast<int*>(ADDR_OF_ARRAY_huffman_codes);
			int* ppvalues = reinterpret_cast<int*>(ADDR_OF_ARRAY_values);

			delete[] pphuffman_codes;
			delete[] ppvalues;
			//free(pphuffman_codes);
			//free(ppvalues);
			return true;
		}
		catch (std::exception ex)
		{
			return false;
		}
	}
