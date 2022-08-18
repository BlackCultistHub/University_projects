#include "pch.h"

#include "Tree.h"
using std::cout;
using std::endl;


namespace HuffmanCore {
	Tree::Tree(char* uniqueSymbArr, int uniqueSymbols, int* chances) :
		_uniqueSymb(uniqueSymbArr),
		_uniqueSymbAm(uniqueSymbols),
		_chances(chances)
	{
		_currentNodeCounter = _uniqueSymbAm;
	}

	void Tree::coutNodeInfo(Node* target)
	{
#ifdef DEBUG
		std::cout << "[*******]\n" << "Node addr: " << target << " + 0 addr: " << (*nodes) << endl
			<< "Node number: " << target - (*nodes) << endl;
#endif
	}

	void Tree::genTree()
	{
		

		//mem 4 nodes
		std::cout << "Allocating mem..\n";
		nodes = (Node**)malloc(sizeof(Node*) * _uniqueSymbAm);
		std::cout << "Allocated addr: " << std::hex << nodes << "\n";
		std::cout << "creating NEW nodes..\n";
		for (uint64_t i = 0; i < _uniqueSymbAm; i++)
			nodes[i] = new Node(_chances[i], NULL, NULL, NULL, _uniqueSymb + i);
		std::cout << "done creating.\n";
		Node* n1, * n2;
		std::cout << "Checking gen suc...\n";
		while (!checkGenSuc(&nodes))
		{
			std::cout << "Getting less chance node...\n";
			getLessChanceNode(&nodes, &n2, &n1);
			//cout << sizeof(nodes)/sizeof(Node*) << endl;
			//cout << sizeof(nodes) << endl;
			this->setNodeCounter(this->getNodeCounter() + 1);
			std::cout << "Reallocating mem..\n";
			nodes = (Node**)realloc(nodes, sizeof(Node*) * this->getNodeCounter());
			std::cout << "Reallocated.\n";
			nodes[this->getNodeCounter() - 1] = new Node(n1->getChance() + n2->getChance(), NULL, n2, n1);
			n1->setParent(nodes[this->getNodeCounter() - 1]);
			n2->setParent(nodes[this->getNodeCounter() - 1]);
#ifdef DEBUG
			cout << "[*******]\n\tNEw node nuMber " << this->getNodeCounter() - 1 <<
				"\n\twith node ChAnce " << nodes[this->getNodeCounter() - 1]->getChance() <<
				"\n\tWas gened from nodes: " << this->getNodeInd(nodes, n1) << "(" << n1->getChance() << ") and " << this->getNodeInd(nodes, n2) << "(" << n2->getChance() << ")" << endl;
#endif // DEBUG
		}
#ifdef DEBUG
		cout << "calling less chance for root: " << endl;
#endif // DEBUG
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

	uint64_t* Tree::getCodes()
	{
		return _codes;
	}
	void Tree::setNodeCounter(int newCounter)
	{
		_currentNodeCounter = newCounter;
	}
	int Tree::getNodeCounter()
	{
		return _currentNodeCounter;
	}

	int Tree::getLessChanceNode(Node*** nodesfield, Node** node2, Node** node1)
	{
		//first -> n1
		//n1 =< n2
		std::cout << "entered getlessChance.\n";
		int numb1 = 0, numb2 = 0;
		Node* min1 = NULL;
		Node* min2 = NULL;
		std::cout << "Counting " << this->getNodeCounter() << " nodes...\n";
		std::cout << "Finding min...\n";
		for (uint64_t i = 0; i < this->getNodeCounter(); i++)
		{
			//std::cout << "Node #" << i << " Addr: " << std::hex << (*nodesfield)[i] << " with PARENT: " << std::hex << (*nodesfield)[i]->getParent() << " \n";
			if ((*nodesfield)[i]->getParent() == NULL)
			{
				min1 = (*nodesfield)[i];
				numb1 = i;
			}
		}
		std::cout << "Done.\n";
		std::cout << "Finding max...\n";
		for (uint64_t i = 0; i < this->getNodeCounter(); i++)
		{
			//std::cout << "Node #" << i << " Addr: " << std::hex << (*nodesfield)[i] << " with PARENT: " << std::hex << (*nodesfield)[i]->getParent() << " \n";
			if (((*nodesfield)[i]->getParent() == NULL) && (min1 != (*nodesfield)[i]))
			{
				min2 = (*nodesfield)[i];
				numb2 = i;
			}
		}
		std::cout << "Done.\n";

		cout << "TEmp: " << "\n\t--> Numb1 : " << numb1 << "\n\t--> numb2: " << numb2 << endl;
		std::cout << "min1 = " << std::hex << min1 << "\n";
		std::cout << "min2 = " << std::hex << min2 << "\n";
		if (min2->getChance() < min1->getChance())
		{
			Node* tBuff = min1;
			min1 = min2;
			min2 = tBuff;
		}
		std::cout << "going through nodes..\n";
		for (uint64_t i = 0; i < this->getNodeCounter(); i++)
		{
			//this->coutNodeInfo((*nodesfield)[i]);
			//std::cout << "node addr: " << (*nodesfield)[i] << "\n";
			if ((*nodesfield)[i]->getParent() == NULL)
			{
				if ((*nodesfield)[i]->getChance() < min1->getChance())
				{
					min2 = min1;
					min1 = (*nodesfield)[i];
					numb2 = numb1; //remove
					numb1 = i;
				}
				else if (((*nodesfield)[i]->getChance() < min2->getChance()) && (min1 != (*nodesfield)[i]))
				{
					min2 = (*nodesfield)[i];
					numb2 = i; //kebab
				}
			}
		}
		*node1 = min1;
		*node2 = min2;
		cout << "Current \t\\/\n\tmin1 : " << min1->getChance() << "\n\tmin2 : " << min2->getChance() << endl;
		cout << "\n**************\n\t--> Numb1 : " << numb1 << "\n\t--> numb2: " << numb2 << endl;
		return 0;
	}

	void Tree::showTree(Node*** nodes, int nodesAm)
	{
#ifdef DEBUG
		cout << "Begin showing tree" << endl;
		for (uint64_t i = 0; i < nodesAm; i++)
		{
			//if (((*nodes)[i]->getSymb() != NULL) && ((*nodes)[i]->getParent() != NULL))
				//cout << "Node number " << i << " : " << (*(*nodes)[i]->getSymb()) << endl;
			if ((*nodes)[i]->getParent() == NULL)
				cout << "No parent node number " << i << endl;
		}
		cout << "End showing tree" << endl;
#endif // DEBUG
	}

	bool Tree::checkGenSuc(Node*** nodesfield)
	{
		std::cout << "enter checkGenSuc.\n";
		int counter = 0;
		//int until = sizeof((*(*nodesfield)))/sizeof(Node*); //-> counter
		for (uint64_t i = 0; i < this->getNodeCounter(); i++)
			if ((*nodesfield)[i]->getParent() == NULL)
				counter++;
		return counter == 2 ? true : false;
	}

	int Tree::getNodeInd(Node** nodes, Node* target)
	{
		for (uint64_t i = 0; i < this->getNodeCounter(); i++)
			if (nodes[i] == target)
				return i;
	}
}