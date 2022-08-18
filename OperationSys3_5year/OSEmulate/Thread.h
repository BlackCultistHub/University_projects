#pragma once

#include "CPU.h"
#include "TaskPool.h"

namespace OSEmulate {

	class Thread : private CPU
	{
	public:
		Thread(int* clock);
		void servePool(TaskPool& pool);
	private:
		std::mutex* mt;
	};
}