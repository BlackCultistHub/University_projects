#include "CPU.h"

namespace OSEmulate {

    int CPU::serveTask(Task* newTask) //обработка без лога
    {
        ready = false;
        newTask->setStartTime(*ticks);
        int reqTime = *ticks + this->serveInterval; //время на обработку = текущее время + интервал обработки
        for (*ticks; *ticks < reqTime; (*ticks)++)
        {
            newTask->setTimeLeft(newTask->getTimeLeft() - 1); //оставшееся время - 1
            if (newTask->getTimeLeft() == 0)
                return -1;
        }
        newTask->setStopTime(*ticks);
        ready = true;
        return 0;
    }

    int CPU::serveTask(Task* newTask, std::ofstream* log) //обработка с логом
    {
        ready = false;
        newTask->setStartTime(*ticks);
        int reqTime = *ticks + this->serveInterval;
        for (*ticks; *ticks < reqTime; (*ticks)++)
        {
            //(*log) << "tick #" << std::to_string(*ticks) << " task #" << newTask->getId() << std::endl;
            newTask->setTimeLeft(newTask->getTimeLeft() - 1); //оставшееся время - 1
            if (newTask->getTimeLeft() == 0)
                return -1;
        }
        newTask->setStopTime(*ticks);
        ready = true;
        return 0;
    }

    int CPU::servePool(TaskPool& pool)
    {
        //find first
        auto it = pool.begin();
        for (it; it != pool.end(); it++)
        {
            if (it->second.getParents()->empty())
                break;
        }

    }

}