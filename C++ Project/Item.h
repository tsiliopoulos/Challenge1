#pragma once
#include "ICommand.h"
#include "Grid.h"

struct coods
{
	int x;
	int y;
};

class Item: public ICommand
{
private:
	coods gridCood;
	Grid *myGrid;
public:
	Item(Grid* grid);
	void Execute() override;
	void Undo() override;
	void Redo() override;

private:

};