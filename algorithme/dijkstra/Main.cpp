#include <string>
#include <iostream>
#include <iomanip>
#include <list>
#include <fstream>
#include "Direction.h"
#include "Noeud.h"

using namespace std;

list<Noeud> noeuds;
list<Noeud> reponse;
bool NINH = true;

///////////////////fonction utile a la mise a jours de la map///////////////////////////////////////

//ajoute une direction a la liste de Direction que le noeud peut aller
void ajouterDirection(int n, Direction direction) {
	// trouve le noeud avec la position n
	list<Noeud>::iterator it;
	for (it = noeuds.begin(); it != noeuds.end(); it++) {
		if (it->getPosition() == n) {
			// ajoute la direction a la liste
			it->ajouterDirection(direction);
		}
	}
}

// creer le graphe composer de l'ensemble des noeuds, qui contiennent tous l'ensemble des
// directions dans lesquelles on peut se deplacer
void creerGraphe(string nomFichier) {
	noeuds = list<Noeud>();
	// ouverture du fichier
	ifstream fichier;
	fichier.open(nomFichier);
	if (!fichier.is_open()) {
		cout << "fichier introuvable" << endl;
		return;
	}

	string tampon;
	getline(fichier, tampon);
	// faire la liste des noeud tant qu'on est pas rendu a la seconde
	// partie du fichier text
	while (tampon != "") {
		string position, borne;
		int i = 0;
		while (tampon[i] != ',') {
			position += tampon[i];
			i++;
		}
		// pour ne pas lire ','
		i++;
		
		borne += tampon[i];
		noeuds.push_back(Noeud(stoi(position), bool(stoi(borne))));

		getline(fichier,tampon);
	}

	// faire la liste des arrets de chaque noeud
	getline(fichier, tampon);
	while (!fichier.eof()) {
		string pos1, pos2, price;
		int i = 0;

		// lecture de la position 1
		while (tampon[i] != ',') {
			pos1 += tampon[i];
			i++;
		}
		i++;
		// lecture de la position 2
		while (tampon[i] != ',') {
			pos2 += tampon[i];
			i++;
		}
		i++;
		// lecture de la distance entre les 2
		while (i < tampon.size()) {
			price += tampon[i];
			i++;
		}
		// ajouter pour les 2 deux le chemin les reliant a lautre
		ajouterDirection(stoi(pos1), Direction(stoi(pos2), stoi(price)));
		ajouterDirection(stoi(pos2), Direction(stoi(pos1), stoi(price)));

		getline(fichier, tampon);
	}
	cout << "Map mise a jour" << endl;
}

/////////////////////////////////// fonction affichage //////////////////////////////////////////////

// afficher l'ensemble du graphe une fois creer
void afficherGraphe(list<Noeud>& ensemble) {
	list<Noeud>::iterator it;
	for (it = ensemble.begin(); it != ensemble.end(); it++) {
		it->afficher();
	}
}

// afficher le chemin final une fois trouver
void extaireSousGraphe(list<Noeud>& ensemble) {
	list<Noeud>::iterator it;
	if (NINH) {
		cout << "vehicule NI-NH utiliser" << endl;
	}
	else {
		cout << "vehicule LI-ion" << endl;
	}
	for (it = ensemble.begin(); it != ensemble.end(); it) {

		int batery = it->getBateryLvl();
		cout << "(" << it->getPosition() << " (temps: " << it->getGCost() << "),(batery :" << batery << "))";

		// si le niveau de baterie est plus grand au prochain noeud, cest quon a recharger entre temps
		if (++it != ensemble.end() && batery < it->getBateryLvl()) {
			cout << " -> Recharge ";
		}

		//pour eviter davoir une fleche qui sert a rien a la fin de la liste
		if ((it) != ensemble.end()) {
			cout << " -> ";
		}
	}
}

///////////////////////////////// fonction utile au pathfinding ///////////////////////////////////////

// ajouter un noeud a la fin l'ensemble choisi
void ajouterNoeud(int index, list<Noeud>& ensemble) {
	list<Noeud>::iterator it;
	for (it = noeuds.begin(); it != noeuds.end(); it++) {
		if (it->getPosition() == index) {
			ensemble.push_back(*it);
		}
	}
}

// ajouter un noeud au debut de l'ensemble choisi
void ajouterNoeudFront(int index, list<Noeud>& ensemble) {
	list<Noeud>::iterator it;
	for (it = noeuds.begin(); it != noeuds.end(); it++) {
		if (it->getPosition() == index) {
			ensemble.push_front(*it);
		}
	}
}

// verifier si l'ensemble contient un noeud ayant l'index choisi
bool contientNoeud(int index, list<Noeud>& ensemble) {
	list<Noeud>::iterator it;
	if (ensemble.size() > 0) {
		for (it = ensemble.begin(); it != ensemble.end(); it++) {
			if (it->getPosition() == index) {
				return true;
			}
		}
	}
	return false;
}

// trouve le noeud ayant le plus faible gCost dans l'ensemble
Noeud trouverMeilleurNoeud(list<Noeud> ensemble) {
	list<Noeud>::iterator it;
	Noeud noeudChoisi = *ensemble.begin();
	for (it = ensemble.begin(); it != ensemble.end(); it++) {
		if (noeudChoisi.getGCost() > it->getGCost()) {
			noeudChoisi = *it;
		}
	}
	return noeudChoisi;
}

// trouve un noeud dans l'ensemble
Noeud trouverNoeud(int index, list<Noeud> ensemble) {
	list<Noeud>::iterator it;
	for (it = ensemble.begin(); it != ensemble.end(); it++) {
		if (it->getPosition() == index) {
			return *it;
		}
	}
}

// retire un noeud de l'ensemble
void retirerNoeud(Noeud n, list<Noeud>& ensemble) {
	list<Noeud>::iterator it;
	for (it = ensemble.begin(); it != ensemble.end(); it++) {
		if (it->getPosition() == n.getPosition()) {
			it = ensemble.erase(it);
			return;
		}
	}
}

// fonction principale du pathfinding
void plusCourtChemin(int depart, int arriver, int risque, int vehicule) {
	NINH = true;
	reponse = list<Noeud>();
	// openSet = ensemble des noeuds a verifier
	list<Noeud> openSet = list<Noeud>();
	// closedSet = ensemble des noeuds deja verifier
	list<Noeud> closedSet = list<Noeud>();
	ajouterNoeud(depart, openSet);

	while (openSet.size() > 0) {
		Noeud noeudChoisi = trouverMeilleurNoeud(openSet);
		retirerNoeud(noeudChoisi, openSet);

		if (noeudChoisi.getPosition() == arriver) {
			list<Noeud>::iterator it;
			// -1 est la valeur par defaut
			while (noeudChoisi.getPositionParent() != -1) {
				reponse.push_front(noeudChoisi);
				noeudChoisi = trouverNoeud(noeudChoisi.getPositionParent(), closedSet);
			}
			ajouterNoeudFront(depart, reponse);
			return;
		}

		//trouver chacun des voisin du noeud choisi
		list<Direction>::iterator it;
		list<Direction>* tampon = new list<Direction>();
		tampon = noeudChoisi.getDirections();
		if (noeudChoisi.getBorneRecharge() && noeudChoisi.getBateryLvl() < 100) {
			tampon->push_back(Direction(noeudChoisi.getPosition(), 120));
		}
		for (it = tampon->begin(); it != tampon->end(); it++) {
			
			if ((!contientNoeud(it->getPosition(), openSet) && !contientNoeud(it->getPosition(), closedSet) && (noeudChoisi.getBateryLvl() >= (20 + it->getCout()))) || it->getPosition() == noeudChoisi.getPosition()) {
				ajouterNoeud(it->getPosition(), openSet);
				openSet.back().setGCost(it->getCout() + noeudChoisi.getGCost());
				openSet.back().setParent(noeudChoisi.getPosition());
				if (it->getPosition() == noeudChoisi.getPosition()) {
					openSet.back().setBateryLvl(100);
				}
				else {
					if (vehicule == 1) {
						if (risque == 1) {
							openSet.back().setBateryLvl(noeudChoisi.getBateryLvl() - (it->getCout() / 60.0) * 6);
						}
						else if (risque == 2) {
							openSet.back().setBateryLvl(noeudChoisi.getBateryLvl() - (it->getCout() / 60.0) * 12);
						}
						else {
							openSet.back().setBateryLvl(noeudChoisi.getBateryLvl() - (it->getCout() / 60.0) * 42);
						}
					}
					else {
						if (risque == 1) {
							openSet.back().setBateryLvl(noeudChoisi.getBateryLvl() - (it->getCout() / 60.0) * 5);
						}
						else if (risque == 2) {
							openSet.back().setBateryLvl(noeudChoisi.getBateryLvl() - (it->getCout() / 60.0) * 10);
						}
						else {
							openSet.back().setBateryLvl(noeudChoisi.getBateryLvl() - (it->getCout() / 60.0) * 30);
						}
					}
				}
			}
		}
		closedSet.push_back(noeudChoisi);
	}
	if (vehicule == 1) {
		cout << "chemin impossible pour les voitures NI-NH" << endl;
		NINH = false;
		plusCourtChemin(depart, arriver, risque, 2);
	}
	else {
		cout << "aucun chemin possible pour les deux type de voiture, nous devons refuser le passager" << endl;
		return;
	}
}

////////////////////////////////////// interface /////////////////////////////////////
void afficherInterface() {
	char choix;
	cout << "Menu" << endl;
	cout << "1 - Mettre a jour la carte" << endl;
	cout << "2 - Determiner le plus court chemin" << endl;
	cout << "3 - Extraire un sous-arbre" << endl;
	cout << "4 - Quiter" << endl;
	cin >> choix;
	if (choix == '1') {
		string nomFichier;
		cout << "quel est le nom du fichier a utiliser?" << endl;
		cin >> nomFichier;
		creerGraphe(nomFichier);
		afficherGraphe(noeuds);
	}
	else if (choix == '2') {
		int depart, arriver, risque;
		if (noeuds.size() > 0) {
			cout << "choisir un point de depart entre 1 et " << noeuds.size() << " : " << endl;
			cin >> depart;
			cout << "choisir un point d'arriver entre 1 et " << noeuds.size() << " : " << endl;
			cin >> arriver;
			cout << "quel est le type de risque (1) faible (2) moyen (3) elever?" << endl;
			cin >> risque;
			if (0 < depart && depart <= noeuds.size() && 0 < arriver && arriver <= noeuds.size() && 0 < risque && risque <= 3) {
				plusCourtChemin(depart, arriver, risque, 1);
			}
			else {
				cout << "commande invalide" << endl;
			}
		}
		else {
			cout << "aucune carte memorisee" << endl;
		}
	}
	else if (choix == '3') {
		if (reponse.size() == 0) {
			cout << "pas de chemin en memoire" << endl;
		}
		else {
			cout << "chemin le plus court trouver : " << endl;
			extaireSousGraphe(reponse);
		}
	}
	else if (choix == '4') {
		return;
	}
	else {
		cout << "choisir une option entre 1, 2, 3 ou 4" << endl;
	}
	cout << endl << endl;
	afficherInterface();
}

//main
int main() {
	afficherInterface();

	system("pause");
	return 0;
}