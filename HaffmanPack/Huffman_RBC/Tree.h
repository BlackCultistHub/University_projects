#pragma once

#include <stdint.h>
#include <corecrt_malloc.h>
#include "Node.h"
#define DEBUG
#ifdef DEBUG
#include <iostream>
#endif

namespace HuffmanCore {
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

		uint64_t* getCodes();
		void setNodeCounter(int newCounter);
		int getNodeCounter();

		void coutNodeInfo(Node* target);
		void showTree(Node*** nodes, int nodesAm);

		int getNodeInd(Node** nodes, Node* target);
	private:
		Node* _root;
		int* _chances; //arr
		uint64_t* _codes; //arr
		int _uniqueSymbAm;
		char* _uniqueSymb; //arr
		int _currentNodeCounter;
		Node** nodes;
	};
}