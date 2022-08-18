#include "TaskPoolSave.h"

//parentList ops
namespace OSEmulate {

    TaskPoolSave::TaskPoolSave(TaskPool* targetPool_):
        targetPool(targetPool_)
    {}

    void TaskPoolSave::addParent(Task* parent)
    {
        auto contains = [&](Task* target) {
            for (int i = 0; i < parentList.size(); i++)
            {
                if (target == parentList[i])
                    return false;
            }
            return true;
        };
        if (!contains(parent))
            parentList.push_back(parent);
    }
    void TaskPoolSave::addParent(const int poolId)
    {
        auto iterator = (*targetPool).find(poolId);
        if (iterator != (*targetPool).end())
            parentList.push_back(&(*targetPool).at(iterator));
    }
    void TaskPoolSave::flush()
    {
        parentList.clear();
    }
    void TaskPoolSave::attach(const int target_poolId, bool autoflush)
    {
        (*targetPool).at(target_poolId).addParentList(&parentList);
        std::map<const int, Task>::iterator backIt = (*targetPool).find(target_poolId);
        for (int i = 0; i < parentList.size(); i++)
        {
            for (auto it = (*targetPool).begin(); it != (*targetPool).end(); it++)
            {
                if (&it->second == parentList[i] && !it->second.containsChild(&(*targetPool).at(target_poolId)))
                {
                    it->second.addChild(&(*targetPool).at(target_poolId));
                    break;
                }
            }
        }
        if (autoflush)
            flush();
    }
    int TaskPoolSave::size()
    {
        return parentList.size();
    }
    std::vector<Task*>& TaskPoolSave::get()
    {
        return parentList;
    }
}