#include "stdafx.h"
#include <stdio.h>
#include <malloc.h>
#include <iostream>

#define ERR_READ_FILE -1
#define ERR_INVALID_ALPHABET_SYMBOL -2
#define ERR_INVALID_ENCRYPTION_MODE -3
#define ERR_EMPTY_ALPHABET_FILE -4

#define MESSAGE_BUFFER 256

const char fAlpName[] = "alphabet.txt";

int alphabetSave(char**, int);
int msgLenDefine(char*);
int encrypt(char*, int, char*, int, int);

int main()
{
	setlocale(LC_ALL, "rus");
	char* alphabet = NULL;
	int alphabetLen = 0;
	int shift;
	char msg[MESSAGE_BUFFER];
	int msgLen;
	int mode;
	alphabetLen = alphabetSave(&alphabet, alphabetLen);
	std::cout << "**********[Ceasar encrypting/decrypting method tool]**********" << std::endl;
	switch (alphabetLen)
	{
	case ERR_READ_FILE:
		FILE * alph;
		fopen_s(&alph, fAlpName, "a+");
		fclose(alph);
		std::cout << "*                                                            *" << std::endl;
		std::cout << "* /!\\\tError opening file or had no alphabet file!" << std::endl;
		std::cout << "*\n**************************************************************" << std::endl;
		system("pause");
		return 0;
	case ERR_EMPTY_ALPHABET_FILE:
		std::cout << "*                                                            *" << std::endl;
		std::cout << "* /!\\\tYou have empty alphabet file!" << std::endl;
		std::cout << "*\n**************************************************************" << std::endl;
		system("pause");
		return 0;
	}
	std::cout << "*                                                            **" << std::endl;
	std::cout << "*\tEnter message: ";
	gets_s(msg, MESSAGE_BUFFER);
	std::cout << "*                                                            **" << std::endl;
	std::cout << "*\tEnter shift: ";
	std::cin >> shift;
	std::cout << "*                                                            **" << std::endl;
	msgLen = msgLenDefine(msg);
	mode = encrypt(alphabet, alphabetLen, msg, msgLen, shift);
	if (mode == ERR_INVALID_ENCRYPTION_MODE)
	{
		std::cout << "* /!\\\tInvalid encryption mode!" << std::endl;
		std::cout << "*\n**************************************************************" << std::endl;
		system("pause");
		return 0;
	}
	switch (mode)
	{
	case 1:
		std::cout << "*\tCrypted message: ";
		break;
	case 0:
		std::cout << "*\tDecrypted message: ";
		break;
	default:
		std::cout << "* /!\\\tSome error in encrypting process (mode?) occured!" << std::endl;
		std::cout << "*\n**************************************************************" << std::endl;
		system("pause");
		return 0;
	}
	for (int i = 0; i < msgLen; i++)
		std::cout << msg[i];
	std::cout << std::endl;
	free(alphabet);
	std::cout << "*                                                            **" << std::endl;
	std::cout << "**************************************************************" << std::endl;
	system("pause");
	return 0;
}

int alphabetSave(char** pAlphabet, int alphabLen)
{
	FILE *alph;
	int falphLen = 0;
	fopen_s(&alph, fAlpName, "r");
	if (alph == NULL)
		return ERR_READ_FILE;
	if (fgetc(alph) == EOF)
	{
		fclose(alph);
		return ERR_EMPTY_ALPHABET_FILE;
	}
	fopen_s(&alph, fAlpName, "r");
	while (fgetc(alph) != EOF)
		falphLen++;
	fclose(alph);
	fopen_s(&alph, fAlpName, "r");
	for (int i = 0; i < falphLen; i++)
	{
		*pAlphabet = (char*)realloc(*pAlphabet, ((alphabLen + 1) * sizeof(char)));
		(*pAlphabet)[i] = fgetc(alph);
		alphabLen++;
	}
	fclose(alph);
	return alphabLen;
}

int msgLenDefine(char* mssge)
{
	int len = 0;
	while (mssge[len] != '\0')
		len++;
	return len;
}

int encrypt(char* alphabet, int alphLen, char* message, int msgLen, int shift)
{
	int i;
	int temp = 0;
	int mode;
	if (shift > 0)
		mode = 1;
	else
		mode = 0;
	switch (mode)
	{
	case 1:
		for (int j = 0; j < msgLen; j++)
		{
			if (message[j] == ' ')
				continue;
			i = 0;
			while (message[j] != alphabet[i])
			{
				if (i > alphLen)
				{
					std::cout << "* /!\\\tThere is no such symbol in alphabet as '" << message[j] << "'!" << std::endl;
					return ERR_INVALID_ALPHABET_SYMBOL;
				}
				i++;
			}
			if (&alphabet[i + shift] > &alphabet[alphLen - 1])
			{
				temp = &alphabet[i + shift] - &alphabet[alphLen - 1];
				temp = temp / sizeof(char);
				while (temp > alphLen)
					temp = temp - alphLen;
			}
			if (temp != 0)
			{
				message[j] = alphabet[temp - 1];
				temp = 0;
			}
			else if (temp == 0)
				message[j] = alphabet[i + shift];
		}
		break;
	case 0:
		for (int j = 0; j < msgLen; j++)
		{
			if (message[j] == ' ')
				continue;
			i = 0;
			while (message[j] != alphabet[i])
			{
				if (i > alphLen)
				{
					std::cout << "* /!\\\tThere is no such symbol in alphabet as '" << message[j] << "'!" << std::endl;
					return ERR_INVALID_ALPHABET_SYMBOL;
				}
				i++;
			}
			if (&alphabet[i + shift] < &alphabet[0])
			{
				temp = &alphabet[0] - &alphabet[i + shift];
				temp = temp / sizeof(char);
				while (temp > alphLen)
					temp = temp - alphLen;
			}
			if (temp != 0)
			{
				message[j] = alphabet[alphLen- temp];
				temp = 0;
			}
			else if (temp == 0)
				message[j] = alphabet[i + shift];
		}
		break;
	default:
		return ERR_INVALID_ENCRYPTION_MODE;
	}
	return mode;
}
