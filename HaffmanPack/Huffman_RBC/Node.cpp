#include "pch.h"

#include "Node.h"

namespace HuffmanCore {
	Node::Node(int chance, Node* parent, Node* left, Node* right, char* symb) :
		_chance(chance),
		_parent(parent),
		_left(left),
		_right(right),
		_symb(symb) {
	}

	int Node::getChance()
	{
		return _chance;
	}
	void Node::setParent(Node* parent)
	{
		_parent = parent;
	}
	char* Node::getSymb()
	{
		return _symb;
	}
	Node* Node::getLeft()
	{
		return _left;
	}
	Node* Node::getRight()
	{
		return _right;
	}
	Node* Node::getParent()
	{
		return _parent;
	}
	Node* Node::getR()
	{
		return _right;
	}
	Node* Node::getL()
	{
		return _left;
	}
}