#pragma once

#include "TaskPool.h"

namespace OSEmulate {

	class TaskPoolSave : private TaskPool
	{
	public:
		TaskPoolSave(TaskPool* targetPool_);

		//parentList ops
		void addParent(Task* parent);
		void addParent(const int poolId);
		void flush();
		void attach(const int target_poolId, bool autoflush = false);
		int size();
		std::vector<Task*>& get();
	private:
		std::vector<Task*> parentList;
		TaskPool* targetPool;
	};
}