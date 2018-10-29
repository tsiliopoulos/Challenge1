#pragma once
#include <vector>
#include "ICommand.h"
#include "Item.h"
class World
{
public:
	World();
	void AddCommand(Item currentCommand);
	~World();
private:
	std::vector<ICommand> OldCommands;
	

};