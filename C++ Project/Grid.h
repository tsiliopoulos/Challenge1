#pragma once

#define GRID_SIZE 7
class Grid
{
public:
	Grid();
	void SetGrid(int x, int y,bool value);
	bool GetGrid(int x, int y);
	~Grid();

private:
	bool gridCoodinates[GRID_SIZE][GRID_SIZE];
};