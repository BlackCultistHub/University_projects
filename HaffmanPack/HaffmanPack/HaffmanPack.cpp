#include <stdio.h>
#include <stdlib.h>
#include <iostream>
#include <fstream>
#include <math.h>

using namespace std;

class Node
{
public:
	Node(int chance, Node* parent, Node* left, Node* right, char* symb);
	int getChance() { return _chance; }
	void setParent(Node* parent) { _parent = parent; }
	char* getSymb() { return _symb; }
	Node* getLeft() { return _left; }
	Node* getRight() { return _right; }
	Node* getParent() { return _parent; }
	void steps(Node* startNode);
	void addCodeSymb(Node* target, char symb);
	Node* getR() { return _right; }
	Node* getL() { return _left; }
private:
	char* _symb;
	int _chance; //byte val
	Node* _parent;
	Node* _left;
	Node* _right;
};

Node::Node(int chance, Node* parent, Node* left, Node* right, char* symb = NULL) :
	_chance(chance),
	_parent(parent),
	_left(left),
	_right(right),
	_symb(symb) {
}

class Tree
{
public:
	Tree(char* uniqueSymbArr, int uniqueSymbols, int* chances);
	void genTree();
	void deGenTree();
	int getLessChanceNode(Node*** nodesfield, Node** node1, Node** node2);
	bool checkGenSuc(Node*** nodesfield);
	void steps(Node* startNode);
	void addCodeSymb(Node* target, char symb);
	uint64_t* getCodes() { return _codes; }
	void setNodeCounter(int newCounter) { _currentNodeCounter = newCounter; }
	int getNodeCounter() { return _currentNodeCounter; }
	void coutNodeInfo(Node* target);
	void showTree(Node*** nodes, int nodesAm);

	int getNodeInd(Node** nodes, Node* target)
	{
		for (uint64_t i = 0; i < this->getNodeCounter(); i++)
			if (nodes[i] == target)
				return i;
	}
private:
	Node* _root;
	int* _chances; //arr
	uint64_t* _codes; //arr
	int _uniqueSymbAm;
	char* _uniqueSymb; //arr
	int _currentNodeCounter;
	Node** nodes;
};

Tree::Tree(char* uniqueSymbArr, int uniqueSymbols, int* chances) :
	_uniqueSymb(uniqueSymbArr),
	_uniqueSymbAm(uniqueSymbols),
	_chances(chances)
{
	_currentNodeCounter = _uniqueSymbAm;
}

void Tree::coutNodeInfo(Node* target)
{
	cout << "[*******]\n" << "Node addr: " << target << " + 0 addr: " << (*nodes) << endl
		<< "Node number: " << target - (*nodes) << endl;
}

void Tree::genTree()
{
	//mem 4 nodes
	nodes = (Node**)malloc(sizeof(Node*) * _uniqueSymbAm);
	for (uint64_t i = 0; i < _uniqueSymbAm; i++)
		nodes[i] = new Node(_chances[i], NULL, NULL, NULL, _uniqueSymb + i);
	Node* n1, * n2;
	while (!checkGenSuc(&nodes))
	{
		getLessChanceNode(&nodes, &n2, &n1);
		//cout << sizeof(nodes)/sizeof(Node*) << endl;
		//cout << sizeof(nodes) << endl;
		this->setNodeCounter(this->getNodeCounter() + 1);
		nodes = (Node**)realloc(nodes, sizeof(Node*) * this->getNodeCounter());
		nodes[this->getNodeCounter() - 1] = new Node(n1->getChance() + n2->getChance(), NULL, n2, n1);
		n1->setParent(nodes[this->getNodeCounter() - 1]);
		n2->setParent(nodes[this->getNodeCounter() - 1]);
		cout << "[*******]\n\tNEw node nuMber " << this->getNodeCounter()-1 << 
			"\n\twith node ChAnce " << nodes[this->getNodeCounter()-1]->getChance() << 
			"\n\tWas gened from nodes: " << this->getNodeInd(nodes, n1) << "(" << n1->getChance() << ") and " << this->getNodeInd(nodes, n2) << "(" << n2->getChance() << ")" << endl;
	} 
	
	cout << "calling less chance for root: " << endl;
	getLessChanceNode(&nodes, &n2, &n1);
	_root = new Node(n1->getChance() + n2->getChance(), NULL, n2, n1);
	n1->setParent(_root);
	n2->setParent(_root);
	this->showTree(&nodes, this->getNodeCounter());
}

void Tree::deGenTree()
{
	_codes = new uint64_t[_uniqueSymbAm];
	for (uint64_t i = 0; i < _uniqueSymbAm; i++)
		_codes[i] = 0;
	addCodeSymb(_root, 1);
	steps(_root);
}

void Tree::steps(Node* startNode)
{
	if (startNode->getR() != NULL)
		addCodeSymb(startNode->getR(), 1);
	if (startNode->getL() != NULL)
		addCodeSymb(startNode->getL(), 0);
	if (startNode->getR() != NULL)
		steps(startNode->getR());
	if (startNode->getL() != NULL)
		steps(startNode->getL());
}

void Tree::addCodeSymb(Node* target, char symb)
{
	if (target->getSymb() != NULL)
	{
		//cout << "WHat rhe HEll Is ThiS : " << ((target->getSymb() - _uniqueSymb)) << endl;
		_codes[(target->getSymb() - _uniqueSymb)] = _codes[(target->getSymb() - _uniqueSymb)] << 1;//not sure
		_codes[(target->getSymb() - _uniqueSymb)] += symb;
	}
	else
	{
		addCodeSymb(target->getL(), symb);
		addCodeSymb(target->getR(), symb);
	}
}

int Tree::getLessChanceNode(Node*** nodesfield, Node** node2, Node** node1)
{
	//first -> n1
	//n1 =< n2
	int numb1 = 0, numb2 = 0;
	Node* min1 = NULL;
	Node* min2 = NULL;
	for (uint64_t i = 0; i < this->getNodeCounter(); i++)
	{
		if ((*nodesfield)[i]->getParent() == NULL)
		{
			min1 = (*nodesfield)[i];
			numb1 = i;
		}
	}
	for (uint64_t i = 0; i < this->getNodeCounter(); i++)
	{
		if (((*nodesfield)[i]->getParent() == NULL) && (min1 != (*nodesfield)[i]))
		{
			min2 = (*nodesfield)[i];
			numb2 = i;
		}
	}

	//cout << "TEmp: " << "\n\t--> Numb1 : " << numb1 << "\n\t--> numb2: " << numb2 << endl;

	if (min2->getChance() < min1->getChance())
	{
		Node* tBuff = min1;
		min1 = min2;
		min2 = tBuff;
	}
	for (uint64_t i = 0; i < this->getNodeCounter(); i++)
	{
		//this->coutNodeInfo((*nodesfield)[i]);
		if ((*nodesfield)[i]->getParent() == NULL)
		{
			if ((*nodesfield)[i]->getChance() < min1->getChance())
			{
				min2 = min1;
				min1 = (*nodesfield)[i];
				numb2 = numb1; //remove
				numb1 = i;
			}
			else if (((*nodesfield)[i]->getChance() < min2->getChance())&&(min1!= (*nodesfield)[i]))
			{
				min2 = (*nodesfield)[i];
				numb2 = i; //kebab
			}
		}
	}
	*node1 = min1;
	*node2 = min2;
	//cout << "Current \t\\/\n\tmin1 : " << min1->getChance() << "\n\tmin2 : " << min2->getChance() << endl;
    //cout << "\n**************\n\t--> Numb1 : " << numb1 << "\n\t--> numb2: " << numb2 << endl;
	return 0;
}

void Tree::showTree(Node*** nodes, int nodesAm)
{
	cout << "Begin showing tree" << endl;
	for (uint64_t i = 0; i < nodesAm; i++)
	{
		//if (((*nodes)[i]->getSymb() != NULL) && ((*nodes)[i]->getParent() != NULL))
			//cout << "Node number " << i << " : " << (*(*nodes)[i]->getSymb()) << endl;
		if ((*nodes)[i]->getParent() == NULL)
			cout << "No parent node number " << i << endl;
	}
	cout << "End showing tree" << endl;
}

bool Tree::checkGenSuc(Node*** nodesfield)
{
	int counter = 0;
	//int until = sizeof((*(*nodesfield)))/sizeof(Node*); //-> counter
	for (uint64_t i = 0; i < this->getNodeCounter(); i++)
		if ((*nodesfield)[i]->getParent() == NULL)
			counter++;
	return counter == 2 ? true : false;
}

/*int getCodeLenght(int code)  //WheRe ShOuld i be? I loST mY PLAcE in this WoRLd
{
	int i = 0;
	while (code<pow(2,i))
	{
		i++;
	}
	i--;
	return i;
}*/

bool isEmpty(std::ifstream& pFile);
bool getBit(uint64_t code, int pointer);
int binaryLen(uint64_t msg);
void writeHeader(uint64_t* codewords, char* uniqueSymbs, int pairs, int zeros, std::ofstream* target);
int writeByBit(uint64_t* codewords, char* uniqueSymbs, std::ifstream* content, std::ofstream* target);

int main()
{
	ifstream file("test.txt", ios::binary);
	ofstream fileOut("res.bin", ios::binary);
	ofstream fileHeader("resH.bin", ios::binary);
	//string* pcontent = new string;
	//string& content = *pcontent;
	char byte;
	unsigned char ubyte;
	int palette[256];
	int count = 0;
	for (uint16_t i = 0; i < sizeof(palette) / sizeof(int); i++)
		palette[i] = 0;
	for (uint64_t i = 0; !file.eof(); i++)
	{
		file.read(&byte, sizeof(char));
		ubyte = byte;
		//content += ubyte;
		palette[ubyte]++;
    cout << i << endl;
	}
	//cout << content << endl;
	for (uint16_t i = 0; i < 256; i++)
	{
		if (palette[i] != 0)
		{
			cout << i << " = " << palette[i] << endl;
			count++;
		}
	}
	int summ = 0;
	for (int i = 0; i < 256; i++)
		summ += palette[i];
	//cout << "chars in file: " << content.length() << endl;
	cout << "cells used: " << count << endl;
	cout << "used cells summ: " << summ << endl;

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
			//cout << "[-- i triggered at : " << USymb[AnotherCounter] << endl;
			chance[AnotherCounter] = palette[i];
			//cout << "[-- chance for triggered i : " << chance[AnotherCounter] << endl;
			AnotherCounter++;
		}
	}
	cout << "[-- counter : " << AnotherCounter << endl;
	Tree huff(USymb, count, chance);
	huff.genTree();
	huff.deGenTree();
	uint64_t* codes = huff.getCodes();
	cout << "\t-------------------------------\n" <<
		    "\t|        [Codes table]        |\n" << 
		    "\t|_____________________________|" << endl;
	for (uint16_t i = 0; i < count; i++)
		printf("\t| Symbol | %4d | Code | %4d |\n", (int)USymb[i], codes[i]);
	cout << "\t-------------------------------" << endl;
	//call write compressed
	int zeros = 0;
	zeros = writeByBit(codes, USymb, &file, &fileOut);
	//call write header
	//cout << count << endl;
	//cout << count - 1 << endl;
	//int temp = count - 1;
	writeHeader(codes, USymb, count-1, zeros, &fileHeader);
	file.close();
	fileOut.close();
	fileHeader.close();
	_fgetchar();
	return 0;
}

bool isEmpty(std::ifstream& pFile)
{
	pFile.seekg(0, ios::end);
	if (pFile.tellg() == 0)
		return true;
	else
	{
		pFile.clear();
		pFile.seekg(0, ios_base::beg);
		return false;
	}
}

bool getBit(uint64_t code, int pointer)
{
	//int length = 0;
	//length = binaryLen(code) - 1;
	uint64_t pointerBit = 0, temp = 0;
	pointerBit = pow(2, binaryLen(code)-pointer);
	temp = code ^ pointerBit;
	if (temp < code)
		return true;
	else
		return false;
}

int binaryLen(uint64_t msg)
{
	int i;
	for (i = 0; msg >= pow(2, i); i++) {}
	i--;
	return i;
}

void writeHeader(uint64_t* codewords, char* uniqueSymbs, int pairs, int zeros, std::ofstream* target)
{
	target->put((char)pairs);
	target->put((char)zeros);
	char buffer = 0;
	uint64_t tempCode = 0;
	for (uint64_t i = 0; i < pairs+1; i++) //as we write pairs-1 because of 256
	{
		target->put(uniqueSymbs[i]);
		for (uint64_t j = 0; j < 8; j++)
		{
			tempCode = codewords[i];
			tempCode <<= j * 8;
			tempCode >>= 7 * 8;
			buffer = (char)tempCode;
			target->put(buffer);
		}
	}
	/*for (uint16_t i = 0; i < pairs; i++)
	{
		*(target) << uniqueSymbs[i] << codewords[i];
	}*/
}

int writeByBit(uint64_t* codewords, char* uniqueSymbs, std::ifstream* content, std::ofstream* target)
{
	int marker1 = 0, marker2 = 1;
	uint8_t buffer = 0;
	char symb;
	uint64_t tempCode = 0;
	int signBits = 0;
	//reset stream
	content->clear();
	content->seekg(0, ios_base::beg);
	for (uint64_t i = 0; !content->eof(); i++)
	{
		if (marker2 == 1)
		{
			symb = 0;
			content->read(&symb, sizeof(char));
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
			target->put(buffer);
			buffer = 0;
			signBits = 0;
		}
		else	// checking if next is EOF after last read symb in content
		{
			content->get();
			if (content->eof())
				break;
			else
				content->seekg(-1, ios::cur);
		}
	}
	if (buffer != 0)
	{
		buffer <<= 8 - signBits;
		target->put(buffer);
		buffer = 0;
		//end of all
		return 8 - signBits;
	}
	else
		return 0;
}