#include "Item.h"

Item::Item(Grid * grid)
{
	myGrid = grid;
}

void Item::Execute()
{
	myGrid->SetGrid(gridCood.x, gridCood.y,true);
}

void Item::Undo()
{
	myGrid->SetGrid(gridCood.x, gridCood.y, false);

}

void Item::Redo()
{
	myGrid->SetGrid(gridCood.x, gridCood.y, true);
}
