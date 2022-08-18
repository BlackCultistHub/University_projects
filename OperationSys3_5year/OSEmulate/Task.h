#pragma once

#include "OSEmulate_stdafx.h"

namespace OSEmulate {    

    class Task //ÍÎ‡ÒÒ Á‡‰‡˜Ë
    {
    public:
        Task() {}
        Task(int startPr_, int pr—oeff_, int startT_, int tReq_);
        Task(const char* name_, double priority_, int tReq_, Task* parent_, std::function<bool(bool&)> funcb1_);
        Task(const char* name_, double priority_, int tReq_, Task* parent_, std::function<bool(bool&, bool&, bool&)> funcb3_);
        Task(const char* name_, double priority_, int tReq_, std::vector<Task*>& parents_, std::function<bool(bool&)> funcb1_);
        Task(const char* name_, double priority_, int tReq_, std::vector<Task*>& parents_, std::function<bool(bool&, bool&, bool&)> funcb3_);
        //template<class Function, class...Args>
        //explicit Task(int id_, const char* name_, double priority_, int tReq_, Task* parent_, std::function<Function(Args...args)> func_):
        //    id(id_),
        //    name(name_),
        //    priority(priority_),
        //    ticks_req(tReq_),
        //    parent(parent_)
        //{
        //    func = func_;
        //}
        //gets
        int getStartPr() { return start_priority; }
        double getPrior() { return priority; }
        int getPriorCoeff() { return priority_Òoeff; }
        int getStartTime() { return start_time; }
        int getReqTime() { return ticks_req; }
        int getStopTime() { return stop_time; }
        int getTimeLeft() { return ticks_left; }
        std::vector<Task*>* getParents() { return &parents; }
        //sets
        void setStartTime(int time);
        void setTimeLeft(int ticks);
        void setPrior(int time);
        void setStopTime(int time);
        void addChild(Task* child);
        void addParentList(std::vector<Task*>* parents);
        //funcs access
        //template<class T, class...Args>
        //static T (*func)(Args...args);
        bool Funcb1(bool& arg1) { return funcb1(arg1); }
        bool Funcb3(bool& arg1, bool& arg2, bool& arg3) { return funcb3(arg1, arg2, arg3); }
        std::function<bool(bool&)> getFunc1() { return funcb1; }
        std::function<bool(bool&, bool&, bool&)> getFunc3() { return funcb3; }

        //service
        bool containsParent(Task* parent);
        bool containsChild(Task* child);

    private:
        std::string name;
        int start_priority;
        double priority;
        int priority_Òoeff;
        int start_time;
        int stop_time;
        int ticks_req;
        int ticks_left;
        std::vector<Task*> parents;
        std::vector<Task*> childs;
        std::function<bool(bool&)> funcb1;
        std::function<bool(bool&, bool&, bool&)> funcb3;
    };
}