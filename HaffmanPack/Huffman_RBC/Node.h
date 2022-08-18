#pragma once

namespace HuffmanCore {
	class Node
	{

	public:
		Node(int chance, Node* parent, Node* left, Node* right, char* symb = nullptr);

		int getChance();

		void setParent(Node* parent);

		char* getSymb();
		Node* getLeft();
		Node* getRight();
		Node* getParent();
		Node* getR();
		Node* getL();

	private:
		char* _symb;
		int _chance; //byte val
		Node* _parent;
		Node* _left;
		Node* _right;
	};
}