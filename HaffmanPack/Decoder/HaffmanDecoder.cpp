#include <stdio.h>
#include <stdlib.h>
#include <iostream>
#include <fstream>
#include <math.h>
#include <sys/stat.h>

using namespace std;

bool checkCW(uint64_t codeWord, uint64_t* codeWordField, int codeWordFieldLen);
bool getBit(unsigned char code, int pointer);
void writeSymbinFile(uint64_t* codes, char* symbs, int limit, uint64_t codeword, std::ofstream* target);

int main()
{

  ifstream fileContent("res.bin", ios_base::binary);
  /*int i = 0;
  while (!fileContent.eof())
  {
	  printf("i=%d\n", i);
	  fileContent.get();
	  i++;
  }
  cout << "got end at" << i << endl;
  struct stat fi;
  stat("E:\\res.bin", &fi);
  printf("file size: %d", fi.st_size);*/
  ifstream fileHeader("resH.bin", ios_base::binary); 
  ofstream fileD("decode.txt", ios_base::binary);
  unsigned char zeros = 0, tpairs = 0, buffer = 0;
  uint64_t codeBuff = 0;
  char temp = 0;
  unsigned int pairs = 0;
  fileHeader.read(&temp, sizeof(char)); // head of head
  tpairs = temp;
  pairs = tpairs+1;
  fileHeader.read(&temp, sizeof(char)); //
  zeros = temp;
  char* symbs = (char*)malloc(pairs * sizeof(char));
  uint64_t* codes = (uint64_t*)malloc(pairs * sizeof(uint64_t));
  for (uint16_t i = 0; i < pairs; i++)
  {
    symbs[i] = 0;
    codes[i] = 0;
  }
  for (uint16_t i = 0; i < pairs; i++)
  {
    fileHeader.read(&temp, sizeof(char));
    buffer = temp;
    symbs[i] = buffer;
    buffer = 0;
    for (uint16_t j = 0; j < 8; j++)
    {
      fileHeader.read(&temp, sizeof(char));
      buffer = temp;
      codes[i] <<= 8;
      codes[i] ^= buffer;
      buffer = 0;
    }
    cout << "Symb: " << (int)symbs[i] << " Code: " << codes[i] << endl;
  }
  cout << "Decoded:\nPairs: " << pairs << " Zeros: " << (int)zeros << endl;
  cout << "Read:" << endl;
  for (uint16_t i = 0; i < pairs; i++)
    cout << "Symb: " << (int)symbs[i] << " Code: " << codes[i] << endl;
  //delete 1's from codes
  uint64_t bufferCW = 0;
  int shift = 0;
  char lesserBuffer4Read = 0;
  unsigned char lesserBuffer = 0;
  bool foundCW = false;
  

  while (!fileContent.eof())
  {
    fileContent.read(&lesserBuffer4Read, sizeof(char));
    if (fileContent.eof())
    {
        lesserBuffer = lesserBuffer4Read;
        break;
    }
    fileContent.seekg(-1, ios::cur);
    bufferCW += 1;
    for (uint16_t i = 0; i < 64; i++)
    {
      if (shift == 0)
      {
        fileContent.read(&lesserBuffer4Read, sizeof(char));
        lesserBuffer = lesserBuffer4Read;
      }
      for (uint16_t j = 0 + shift; j < 8; j++)
      {
        if (getBit(lesserBuffer, shift))
        {
          bufferCW <<= 1;
          bufferCW++;
        }
        else
          bufferCW <<= 1;
        shift++;
        if(checkCW(bufferCW, codes, pairs))
        {
          foundCW = true;
          break;
        }
      }
      if (shift == 8)
        shift = 0;
      if (foundCW)
        break;
      //cycle if full 8bit buffer is not enough for cw
    }
    if (foundCW) // has some shift in lesser code buffer
    {
      cout << "CodeWord: " << bufferCW << endl;
      writeSymbinFile(codes, symbs, pairs, bufferCW, &fileD);
      bufferCW = 0;
      foundCW = false;
      
    }
  }
  //last byte
  bufferCW += 1;
  for (uint16_t j = 0 + shift; j < 8-zeros; j++)
  {
    if (getBit(lesserBuffer, shift))
    {
        bufferCW <<= 1;
        bufferCW++;
    }
    else
        bufferCW <<= 1;
    shift++;
    if(checkCW(bufferCW, codes, pairs))
    {
        foundCW = true;
        cout << "CodeWord: " << bufferCW << endl;
        writeSymbinFile(codes, symbs, pairs, bufferCW, &fileD);
        bufferCW = 1;
    }
  }
  fileHeader.close();
  fileContent.close();
  fileD.close();
  _fgetchar();
  return 0;
}

void writeSymbinFile(uint64_t* codes, char* symbs, int limit, uint64_t codeword, std::ofstream* target)
{
    for (uint16_t i = 0; i < limit; i++)
        if (codes[i] == codeword)
            target->put(symbs[i]);
}

bool checkCW(uint64_t codeWord, uint64_t* codeWordField, int codeWordFieldLen)
{
  for (uint16_t i = 0; i < codeWordFieldLen; i++)
    if (codeWordField[i] == codeWord)
      return true;
  return false;
}

bool getBit(unsigned char code, int pointer) //pointer [0 - 7]
{
  //int length = 0;
  //length = binaryLen(code) - 1;
  unsigned char pointerBit = 0, temp = 0;
  pointerBit = pow(2, 7 - pointer);
  temp = code ^ pointerBit;
  if (temp < code)
    return true;
  else
    return false;
}
