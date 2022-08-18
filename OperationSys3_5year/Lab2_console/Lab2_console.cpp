#include <iostream>

#include "OSEmulate.h"

bool FuncA(bool& Rand1, bool& Rand2, bool& Rand3)
{
    Rand1 = rand()%2;
    Rand2 = rand()%2;
    Rand3 = rand()%2;
    std::cout << "TASK A: Generated Rand1 = " << Rand1 << ", Rand2 = " << Rand2 << ", Rand3 = " << std::endl;
    return 0;
}

bool FuncB(bool Rand1, bool Rand2, bool Rand3)
{
    bool op = (Rand1 && Rand2 && Rand3);
    std::cout << "TASK B: R1 AND R2 AND R3 = " << op << std::endl;
    return op;
}

bool FuncC(bool Rand1, bool Rand2, bool Rand3)
{
    bool op = (Rand1 || Rand2 || Rand3);
    std::cout << "TASK C: R1 OR R2 OR R3 = " << op << std::endl;
    return op;
}

bool FuncD(bool Rand1, bool Rand2, bool Rand3)
{
    bool op = (Rand1 && Rand2 || Rand3);
    std::cout << "TASK D: R1 AND R2 OR R3 = " << op << std::endl;
    return op;
}

bool FuncE(bool val)
{
    bool op = (!val);
    std::cout << "TASK E: NOT (C) = " << op << std::endl;
    return op;
}

bool FuncF(bool val1, bool val2, bool val3)
{
    bool op = (val1 && val2 && val3);
    std::cout << "TASK F: (B) AND (D) AND (E) = " << op << std::endl;
    return op;
}

bool FuncG(bool val1, bool val2, bool val3)
{
    bool op = (val1 || val2 || val3);
    std::cout << "TASK G: (B) OR (D) OR (E) = " << op << std::endl;
    return op;
}

bool FuncH(bool val1, bool val2, bool val3)
{
    bool op = (val1 && val2 || val3);
    std::cout << "TASK H: (B) AND (D) OR (E) = " << op << std::endl;
    return op;
}

bool FuncK(bool val1, bool val2, bool val3)
{
    bool op = (val1 || val2 && val3);
    std::cout << "TASK K: (F) OR (G) AND (H) = " << op << std::endl;
    return op;
}

int main()
{
    //adding tasks
    OSEmulate::TaskPool chain;
    OSEmulate::TaskPoolSave save1(&chain), save2(&chain);
    bool Rand1 = 0, Rand2 = 0, Rand3 = 0;
    //adding A
    int taskAid = chain.add(OSEmulate::Task("taskA", 0.0, 100, NULL, FuncA));
    //adding B
    int taskBid = chain.add(OSEmulate::Task("taskB", 2.0, 200, &chain.at(taskAid), FuncB));
    save1.addParent(taskBid);
    //adding C
    int taskCid = chain.add(OSEmulate::Task("taskC", 1.0, 100, &chain.at(taskAid), FuncC));
    //adding D
    int taskDid = chain.add(OSEmulate::Task("taskD", 2.0, 200, &chain.at(taskAid), FuncD));
    save1.addParent(taskDid);
    //adding E
    int taskEid = chain.add(OSEmulate::Task("taskE", 2.0, 100, &chain.at(taskCid), FuncE));
    save1.addParent(taskEid);
    //adding F
    int taskFid = chain.add(OSEmulate::Task("taskF", 3.0, 100, save1.get(), FuncF));
    save2.addParent(taskFid);
    //adding G
    int taskGid = chain.add(OSEmulate::Task("taskG", 3.0, 100, save1.get(), FuncG));
    save2.addParent(taskGid);
    //adding H
    int taskHid = chain.add(OSEmulate::Task("taskH", 3.0, 100, save1.get(), FuncH));
    save2.addParent(taskHid);
    //flush save1
    save1.flush();
    //adding K
    int taskKid = chain.add(OSEmulate::Task("taskK", 4.0, 100, save2.get(), FuncK));
    save2.flush();

    //serving tasks
    std::vector<OSEmulate::Task*> inProcess;
    //find first
    auto it = chain.begin();
    for (it; it != chain.end(); it++)
    {
        if (it->second.getParents()->empty())
            break;
    }
    inProcess.push_back(&it->second);

    for (int tasksInChain = 0; tasksInChain < chain.size(); tasksInChain++)
    {
        //do inProcess
        for (int tasksInProcess = 0; tasksInProcess < inProcess.size(); tasksInProcess++)
        {
            if (inProcess[tasksInProcess]->getFunc3() == NULL)
            {
                //make thread
                inProcess[tasksInProcess]->Funcb1(Rand1);
            }
            else
            {
                //make thread
                inProcess[tasksInProcess]->Funcb3(Rand1, Rand2, Rand3);
            }
        }
        //if child of all is ==


        //if not
            //if multiple end
            //if chain

        //inProcess.clear();
        for (int parents = 0; parents < it->second.getParents()->size(); parents++)
        {
            inProcess.push_back((*it->second.getParents())[parents]);
        }


    }

    return 0;
}