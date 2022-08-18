#pragma once

#include "Task.h"

namespace OSEmulate {

    typedef std::pair<const int, Task> TaskPair;

    class TaskPool
    {
    public:
        TaskPool() {}
        ~TaskPool() {}

        int add(Task newTask);
        int add(const char* name_, double priority_, int tReq_, Task* parent_, std::function<bool(bool&)> funcb1_);
        int add(const char* name_, double priority_, int tReq_, Task* parent_, std::function<bool(bool&, bool&, bool&)> funcb3_);
        int add(const char* name_, double priority_, int tReq_, std::vector<Task*>& parents_, std::function<bool(bool&)> funcb1_);
        int add(const char* name_, double priority_, int tReq_, std::vector<Task*>& parents_, std::function<bool(bool&, bool&, bool&)> funcb3_);

        void remove(int poolId);
        //void remove(std::map<const int, Task>::iterator iterator);

        int getMostPriorTask(); //returns ID
        
        int takeId();

        //forwardings
        Task& at(const int poolId);
        Task& at(std::map<const int, Task>::iterator iterator);
        std::map<const int, Task>::iterator find(const int poolId);
        std::map<const int, Task>::iterator begin();
        std::map<const int, Task>::iterator end();
        int size() { return pool.size(); }
    private:
        std::map<const int, Task> pool;
    };
}