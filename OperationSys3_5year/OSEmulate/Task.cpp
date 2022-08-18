#include "Task.h"

namespace OSEmulate {

    Task::Task(int startPr_, int prСoeff_, int startT_, int tReq_) :
        start_priority(startPr_),
        priority_сoeff(prСoeff_),
        start_time(startT_),
        ticks_req(tReq_),
        ticks_left(tReq_)
    {
        priority = start_priority + priority_сoeff * exp(0); //приоритет при инициализации (Pr = Pr0 + a * exp(t))
        stop_time = -1;
        funcb1 = NULL;
        funcb3 = NULL;
    }
    Task::Task(const char* name_, double priority_, int tReq_, Task* parent_, std::function<bool(bool&)> funcb1_):
        name(name_),
        priority(priority_),
        ticks_req(tReq_),
        funcb1(funcb1_)
    {
        start_priority = priority;
        priority_сoeff = 1;
        start_time = NULL;
        stop_time = -1;
        ticks_left = ticks_req;
        funcb3 = NULL;

        if (parent_ != NULL)
        {
            parents.push_back(parent_);
        }
    }
    Task::Task(const char* name_, double priority_, int tReq_, Task* parent_, std::function<bool(bool&, bool&, bool&)> funcb3_):
        name(name_),
        priority(priority_),
        ticks_req(tReq_),
        funcb3(funcb3_)
    {
        start_priority = priority;
        priority_сoeff = 1;
        start_time = NULL;
        stop_time = -1;
        ticks_left = ticks_req;
        funcb1 = NULL;

        if (parent_ != NULL)
        {
            parents.push_back(parent_);
        }
    }
    Task::Task(const char* name_, double priority_, int tReq_, std::vector<Task*>& parents_, std::function<bool(bool&)> funcb1_):
        name(name_),
        priority(priority_),
        ticks_req(tReq_),
        funcb1(funcb1_)
    {
        start_priority = priority;
        priority_сoeff = 1;
        start_time = NULL;
        stop_time = -1;
        ticks_left = ticks_req;
        funcb3 = NULL;

        if (parents_.size() != 0)
        {
            for (int i = 0; i < parents_.size(); i++)
            {
                parents.push_back(parents_[i]);
            }
        }
    }
    Task::Task(const char* name_, double priority_, int tReq_, std::vector<Task*>& parents_, std::function<bool(bool&, bool&, bool&)> funcb3_):
        name(name_),
        priority(priority_),
        ticks_req(tReq_),
        funcb3(funcb3_)
    {
        start_priority = priority;
        priority_сoeff = 1;
        start_time = NULL;
        stop_time = -1;
        ticks_left = ticks_req;
        funcb1 = NULL;

        if (parents_.size() != 0)
        {
            for (int i = 0; i < parents_.size(); i++)
            {
                parents.push_back(parents_[i]);
            }
        }
    }

    void Task::setStartTime(int time)
    {
        start_time = time;
    }
    void Task::setTimeLeft(int ticks)
    {
        ticks_left = ticks;
    }
    void Task::setPrior(int time)
    {
        if (stop_time == -1) //если задача не обрабатывалась - не учитывать время остановки
            priority = start_priority + priority_сoeff * exp(time);
        else
        {
            int timeInQueue = time - stop_time;
            priority = start_priority + priority_сoeff * exp(timeInQueue);
        }
    }
    void Task::setStopTime(int time)
    {
        stop_time = time;
    }

    void Task::addChild(Task* child)
    {
        childs.push_back(child);
    }

    void Task::addParentList(std::vector<Task*>* parents_)
    {
        for (int i = 0; i < parents_->size(); i++)
        {
            parents.push_back((*parents_)[i]);
        }
    }

    bool Task::containsParent(Task* parent)
    {
        for (int i = 0; i < parents.size(); i++)
        {
            if (parent == parents[i])
                return true;
        }
        return false;
    }

    bool Task::containsChild(Task* child)
    {
        for (int i = 0; i < childs.size(); i++)
        {
            if (child == childs[i])
                return true;
        }
        return false;
    }
}