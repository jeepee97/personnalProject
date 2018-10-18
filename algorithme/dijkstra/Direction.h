#pragma once

#ifndef DIRECTION_H
#define DIRECTION_H

#include <string>
#include <iostream>
#include <iomanip>
#include <map>
#include <list>

using namespace std;

class Direction {
private:
	int position;
	int price;
public:
	Direction(int position_, int price_);
	int getPosition();
	int getCout();
	void afficher();
};
#endif