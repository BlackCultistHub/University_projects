#include "stdafx.h"

using namespace OSEmulate;

int main()
{
    std::vector<Task> queue; //очередь задач
    int id = 0; //идентификатор задачи
    int clock = 0; //тактируемый сигнал
    int customTimeInt = 0;
    std::string lineBuff = "";
    std::string argBuff = "";
    int args[3];
    std::regex reArgs("([0-9]{1,9});([0-9]{1,9});([0-9]{1,9})"); //регулярное выражение для обработки ввода из файла
    std::smatch matches;
    std::cout << "Enter custom server interval or 0 for default(250): ";
    std::cin >> lineBuff;
    std::cout << std::endl;

    std::ifstream source("io\\tasks.csv"); // Pr0; ai; treq
    if (!source.is_open())
        return ERROR_OPENING_SOURCE;
    std::cout << "Source opened." << std::endl;

    CPU core1;
    if (lineBuff == "0")
        core1.init(&clock); //объект процессора
    else
        core1.init(&clock, std::stoi(lineBuff)); //объект процессора

    std::cout << "Reading tasks:" << std::endl;
    while (!source.eof()) //построчное добавление задачи в вектор
    {
        source >> lineBuff;
        std::cout << lineBuff << std::endl;
        std::regex_search(lineBuff, matches, reArgs);
        for (int i = 1; i < matches.size(); i++)
            args[i - 1] = std::stoi(matches[i]);
        queue.push_back(Task(id, args[0], args[1], 0, args[2])); //добавление задачи
        std::cout << "Task #" << id << " Pr0=" << args[0] << " a=" << args[1] << " Treq=" << args[2] << std::endl;
        id++;
    }
    std::cout << "Tasks read." << std::endl;
    source.close();
    std::cout << "Source closed." << std::endl;

    std::cout << "\nServing tasks: " << std::endl;
    while (!queue.empty()) //обработка очереди задач
    {
        std::cout << std::endl;
        for (int i = 0; i < queue.size(); i++)
            std::cout << "Task #" << queue[i].getId() << " Priority= " << queue[i].getPrior() << std::endl;
        Task* taskToGo = getMostPriorTask(queue); //получение задачи с наибольшим приоритетом
        std::cout << "Start serving Task #" << taskToGo->getId() << " at " << clock << " tick." << std::endl;
        if (core1.serveTask(taskToGo) == -1) //если задача обработана полностью за интервал работы
        {
            std::vector<Task>::iterator doneTaskIter = getTaskIterator(queue, *taskToGo); //найти итератор этой задачи в векторе
            std::cout << "Task #" << doneTaskIter->getId() << " served at " << clock << " tick." << std::endl;
            queue.erase(doneTaskIter); //удалить задачу из вектора- выполнено
        }
        else
            std::cout << "Interval done with Task #" << taskToGo->getId() << " at " << clock << " tick." << std::endl;
        std::cout << "Recalculating priorities..." << std::endl;
        for (int i = 0; i < queue.size(); i++) //пересчёт приоритетов задач в векторе
            queue[i].setPrior(clock);
    }
    std::cout << "All tasks served." << std::endl;

    system("pause");
    return 0;
}