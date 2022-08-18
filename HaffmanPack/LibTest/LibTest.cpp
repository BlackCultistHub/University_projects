#include <iostream>
#include <stdio.h>
#include <stdlib.h>

#include <Huffman_RBCLib.h>

#include <stdint.h>

typedef unsigned char byte;

int main()
{
    byte input[] = {0, 1};
    std::cout << "Testing coder 4jpeg.\n";
    //inits
    


    uint16_t** codes = (uint16_t**)malloc(sizeof(uint16_t*));
    //auto& rcodes = codes;
    int codesLen = 0;
    char** values = (char**)malloc(sizeof(char*));
    //auto& rvalues = values;
    int valuesLen = 0;
    HuffmanRBC::Encode_getCodes(input, 2, codes, &codesLen, values, &valuesLen);
    for (int i = 0; i < codesLen; i++)
        printf("\t| Symbol | %4d | Code | %4d |\n", (int)(*values)[i], (*codes)[i]);
    system("pause");
    return 0;
}
