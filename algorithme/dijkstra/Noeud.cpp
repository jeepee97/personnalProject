#include "Noeud.h"

Noeud::Noeud(int position_, bool borneRecharge_) {
	posParent = -1;
	gCost = 0;
	bateryLvl = 100;
	position = position_;
	borneRecharge = borneRecharge_;
	directions = list<Direction>();
}

void Noeud::ajouterDirection(Direction a) {
	directions.push_back(a);
}

int Noeud::getPosition() {
	return position;
}

list<Direction>* Noeud::getDirections() {
	return &directions;
}

void Noeud::afficher() {
	cout << "noeud : " << position << " borne : " << borneRecharge << " ";
	list<Direction>::iterator it;
	for (it = directions.begin(); it != directions.end(); it++) {
		it->afficher();
		cout << " , ";
	}
	cout << endl;
}

void Noeud::setGCost(int g) {
	gCost = g;
}

int Noeud::getGCost() {
	return gCost;
}

void Noeud::setParent(int index) {
	posParent = index;
}

int Noeud::getPositionParent() {
	return posParent;
}

void Noeud::setBateryLvl(int l) {
	bateryLvl = l;
}

double Noeud::getBateryLvl() {
	return bateryLvl;
}

bool Noeud::getBorneRecharge() {
	return borneRecharge;
}