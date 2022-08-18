#pragma once

#include "Task.h"
#include "TaskPool.h"

namespace OSEmulate {

    class CPU //класс процессора
    {
    public:
        CPU() {}
        CPU(int* clock) : ticks(clock) {}
        CPU(int* clock_, int serverInterv_) : ticks(clock_), serveInterval(serverInterv_) {}
        ~CPU() {}
        //inits
        void init(int* clock_, int serverInterv_) { ticks = clock_; serveInterval = serverInterv_; }
        void init(int* clock_) { ticks = clock_; }
        //funcs
        int serveTask(Task* newTask);
        int serveTask(Task* newTask, std::ofstream* log);
        int servePool(TaskPool& pool);
    private:
        int* ticks = NULL; //тактируемый сигнал
        Task* task = NULL; //выполняемая задача
        int serveInterval = 250; //интервал обработки (стд = 250)
        bool ready = true;
    };

}