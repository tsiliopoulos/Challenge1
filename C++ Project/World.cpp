#include "World.h"

World::World()
{

}


void World::AddCommand(Item currentCommand)
{
	OldCommands.push_back(currentCommand);
}



World::~World()
{

}
