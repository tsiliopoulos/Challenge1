#include "Grid.h"

Grid::Grid()
{
	for (int i = 0; i < GRID_SIZE; i++)
	{
		for (int j = 0; j < GRID_SIZE; j++)
		{
			gridCoodinates[i][j] = false;
		}
	}
}

void Grid::SetGrid(int x, int y, bool value)
{

		gridCoodinates[x][y]=value;
	
}

bool Grid::GetGrid(int x, int y)
{
	return gridCoodinates[x][y];
}

Grid::~Grid()
{
}
