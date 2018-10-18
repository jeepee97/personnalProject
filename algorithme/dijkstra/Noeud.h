#pragma once

#ifndef NOEUD_H
#define NOEUD_H

#include "Direction.h"
#include <string>
#include <iostream>
#include <iomanip>
#include <list>

using namespace std;

class Noeud {
private:
	int posParent;
	int gCost;
	int position;
	double bateryLvl;
	bool borneRecharge;
	list<Direction> directions;
public:
	Noeud(int position_, bool borneRecharge_);
	void ajouterDirection(Direction a);
	int getPosition();
	list<Direction>* getDirections();
	void afficher();
	void setGCost(int g);
	void setBateryLvl(int l);
	int getGCost();
	void setParent(int index);
	int getPositionParent();
	double getBateryLvl();
	bool getBorneRecharge();
};
#endif 
