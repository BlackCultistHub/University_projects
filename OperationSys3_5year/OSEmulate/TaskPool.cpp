#include "TaskPool.h"

namespace OSEmulate {

    int TaskPool::add(Task newTask)
    {
        int id = takeId();
        pool.insert(TaskPair(id, newTask));
        std::map<const int, Task>::iterator backIt = pool.find(id);
        if (newTask.getParents()->size() != 0)
        {
            for (int parentInd = 0; parentInd < newTask.getParents()->size(); parentInd++)
            {
                for (auto it = pool.begin(); it != pool.end(); it++)
                {
                    if (&it->second == (*newTask.getParents())[parentInd])
                        it->second.addChild(&(this->at(backIt)));
                }
            }
        }
        return id;
    }
    int TaskPool::add(const char* name_, double priority_, int tReq_, Task* parent_, std::function<bool(bool&)> funcb1_)
    {
        int id = takeId();
        pool.insert(TaskPair(id, Task(name_, priority_, tReq_, parent_, funcb1_)));
        std::map<const int, Task>::iterator backIt = pool.find(id);
        if (parent_ != NULL)
        {
            for (auto it = pool.begin(); it != pool.end(); it++)
            {
                if (&it->second == parent_)
                    it->second.addChild(&(this->at(backIt)));
            }
        }
        return id;
    }
    int TaskPool::add(const char* name_, double priority_, int tReq_, Task* parent_, std::function<bool(bool&, bool&, bool&)> funcb3_)
    {
        int id = takeId();
        pool.insert(TaskPair(id, Task(name_, priority_, tReq_, parent_, funcb3_)));
        std::map<const int, Task>::iterator backIt = pool.find(id);
        if (parent_ != NULL)
        {
            for (auto it = pool.begin(); it != pool.end(); it++)
            {
                if (&it->second == parent_)
                    it->second.addChild(&(this->at(backIt)));
            }
        }
        return id;
    }
    int TaskPool::add(const char* name_, double priority_, int tReq_, std::vector<Task*>& parents_, std::function<bool(bool&)> funcb1_)
    {
        int id = takeId();
        pool.insert(TaskPair(id, Task(name_, priority_, tReq_, parents_, funcb1_)));
        std::map<const int, Task>::iterator backIt = pool.find(id);
        if (parents_.size() != 0)
        {
            for (int parentInd = 0; parentInd < parents_.size(); parentInd++)
            {
                for (auto it = pool.begin(); it != pool.end(); it++)
                {
                    if (&it->second == parents_[parentInd])
                        it->second.addChild(&(this->at(backIt)));
                }
            }
        }
        return id;
    }
    int TaskPool::add(const char* name_, double priority_, int tReq_, std::vector<Task*>& parents_, std::function<bool(bool&, bool&, bool&)> funcb3_)
    {
        int id = takeId();
        pool.insert(TaskPair(id, Task(name_, priority_, tReq_, parents_, funcb3_)));
        std::map<const int, Task>::iterator backIt = pool.find(id);
        if (parents_.size() != 0)
        {
            for (int parentInd = 0; parentInd < parents_.size(); parentInd++)
            {
                for (auto it = pool.begin(); it != pool.end(); it++)
                {
                    if (&it->second == parents_[parentInd])
                        it->second.addChild(&(this->at(backIt)));
                }
            }
        }
        return id;
    }

    void TaskPool::remove(int poolId)
    {
        pool.erase(poolId);
    }

    //void TaskPool::remove(std::map<const int, Task>::iterator iterator)
    //{
    //    if (iterator != pool.end())
    //        pool.erase(iterator);
    //}

    int TaskPool::getMostPriorTask()
    {
        int targetId = -1;
        for (auto it = pool.begin(); it != pool.end(); it++) //перебором найти задачу с наибольшим приоритетом
        {
            if (targetId == -1)
                targetId = it->first;
            else if (this->at(targetId).getPrior() < it->second.getPrior())
                targetId = it->first;
        }
        return targetId;
    }

    int TaskPool::takeId()
    {
        int randId = 0;
        bool unique = false;
        while (!unique)
        {
            randId = rand();
            unique = true;
            for (auto it = pool.begin(); it != pool.end(); it++)
            {
                if (it->first == randId)
                {
                    unique = false;
                    break;
                }
            }
        }
        return randId;
    }

    //forwardings
    Task& TaskPool::at(const int poolId)
    {
        return pool.at(poolId);
    }
    Task& TaskPool::at(std::map<const int, Task>::iterator iterator)
    {
        return pool.at(iterator->first);
    }
    std::map<const int, Task>::iterator TaskPool::find(const int poolId)
    {
        return pool.find(poolId);
    }
    std::map<const int, Task>::iterator TaskPool::begin()
    {
        return pool.begin();
    }
    std::map<const int, Task>::iterator TaskPool::end()
    {
        return pool.end();
    }
}