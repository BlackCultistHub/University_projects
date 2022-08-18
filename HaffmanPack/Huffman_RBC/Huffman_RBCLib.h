#pragma once
//Main include file of DLL

#if defined(HUFFMANRBC_EXPORTS)
#define HUFFMANRBC_API __declspec(dllexport)
#else
#define HUFFMANRBC_API
#endif

//Node
#include "Node.h"
//Tree
#include "Tree.h"
//Logic
#include "Logic.h"

	typedef unsigned char byte;
	//not working
	extern "C" bool HUFFMANRBC_API Encode(byte* input_stream, int input_stream_len, byte* output_stream, int* output_stream_len, uint64_t* huffman_codes, int* huffman_codes_len, char* values, int* values_len, byte* header_stream, int* header_stream_len);

	extern "C" bool HUFFMANRBC_API Encode_getCodes(byte * input_stream, int input_stream_len, int* huffman_codes, int* huffman_codes_len, int* values, int* values_len);

	//leg-shooter
	extern "C" bool HUFFMANRBC_API Encode_getCodes_Wrapper(int ADDR_input_stream, int input_stream_len, int POINTER_ADDR_huffman_codes, int ADDR_huffman_codes_len, int POINTER_ADDR_values, int ADDR_values_len);
	
	extern "C" bool HUFFMANRBC_API Encode_getCodes_Freq(byte* input_stream, int input_stream_len, int* huffman_codes, int* huffman_codes_len, int* values, int* values_len, int* freqs, int* freqs_len);
	
	extern "C" bool HUFFMANRBC_API Encode_getCodes_Freq_Wrapper(int ADDR_input_stream, int input_stream_len,
		int ADDR_OF_ARRAY_huffman_codes, int ADDR_huffman_codes_len,
		int ADDR_OF_ARRAY_values, int ADDR_values_len,
		int ADDR_OF_ARRAY_freqs, int ADDR_freqs_len);
	
	//escape from leg-shooter
	extern "C" bool HUFFMANRBC_API Free_Encode_getCodes_Wrapper(int ADDR_OF_ARRAY_huffman_codes, int ADDR_OF_ARRAY_values);