#include "Direction.h"

Direction::Direction(int position_, int price_) {
	position = position_;
	price = price_;
}

int Direction::getPosition() {
	return position;
}
int Direction::getCout() {
	return price;
}

void Direction::afficher() {
	cout << "(direction : " << position << " cout : " << price << ")";
}
