#include "Robot.h"
//Parcourt
enum PARCOURT{
    INITIAL,//search for closest wall
    FOLLOW,
    JUMPING,
    FOLLOW_LOCKED,
    TURN_AROUND
};
/******************* PROTOTYPES AND GLOBAL VARIABLES *******************/
//Mode parcourt
Robot robot;
bool wall = false;
uint8_t state = INITIAL;
Side* next = &robot.left; // current side
Side* current = &robot.right;    // the other side
bool changeSide();
/***********************************************************************/


/**************************** INTERRUPTS *******************************/

//instruction received
ISR(USART0_RX_vect)
{
    //USART reception complete
    uint8_t request = robot.uart.receive();
    //on éxécute la requete
    switch (request)
    {
    case REQUETE_ENVOI: //Transmission initiale
        robot.identify();
        break;
    case VITESSE_GAUCHE: //vitesse gauche
        robot.left.wheel->set(robot.uart.receive());
        break;
    case VITESSE_DROIT: //vitesse droite
        robot.right.wheel->set(robot.uart.receive());
        break;
    case COULEUR_DEL: //LED COLOR
        robot.led.changeColor(robot.uart.receive());
        break;
    }
}

//button state changed
ISR(INT0_vect)
{
    robot.button.update();

    //dans le mode parcourt on fait un virage à 180 degre
    if((robot.mode == PARCOURT) && robot.button.isPressed() && ((PORTB & 0b00000011) == 0b00000001)){
        robot.led.changeColor(RED);
        robot.spin(next,180);
        state = FOLLOW;
        
        Side* temp = current;
		current = next;
		next = temp;        
    }
}
/***********************************************************************/

int main()
{
    //mode logiciel
    robot.uart.enableReception(true);
    while (robot.mode == LOGICIEL)
    {
        robot.update();
         //delai pour stabiliser l'affichage
    }
    //mode parcourt
    char msg[20] = "Initial\n";
    char msgj[20] = "Jump\n";
    char msgf[20] = "follow\n";
    char msgt[20] = "turn\n";
    char msgl[20] = "follow Locked\n";
    robot.uart.enableReception(false); // We do not need to communicate with the computer
    while (robot.mode == PARCOURT)
    {
        // _delay_ms(300); //delai pour stabiliser l'affichage
        switch (state){
			case INITIAL:
				robot.led.changeColor(NONE);
				current = robot.searchClosesthWall();
				if (current == &robot.right){
					next = &robot.left;
				}
				else{
					next = &robot.right;
				}
				
				if (current != 0){
					robot.move(NORMAL_SPEED, FORWARD);
					state = FOLLOW;
				}		
			break;
			case FOLLOW:
				robot.follow(current);
				if (robot.searchWall(next) && robot.stabilizeDistance(next) <= 1){
					if (changeSide()){
						robot.led.changeColor(NONE);
						state = JUMPING;
						
						if (current == &robot.right){
						robot.setWheels(NORMAL_SPEED - 10, ROTATION_SPEED);
						}
						else {
							robot.setWheels(ROTATION_SPEED, NORMAL_SPEED);
						}
						_delay_ms(1000);
					}
				}
				if (!robot.searchWall(current)){
					state = TURN_AROUND;
				}
			break;
			case JUMPING:
				robot.led.changeColor(NONE);

				robot.setWheels(NORMAL_SPEED, NORMAL_SPEED);

				if (current->sensor->getAverageDistance() <= 25){
					if (current == &robot.right){
						robot.setWheels(ROTATION_SPEED, NORMAL_SPEED);
					}
					else {
						robot.setWheels(NORMAL_SPEED - 10, ROTATION_SPEED);
					}
					_delay_ms(1000);
					state = FOLLOW_LOCKED;
				}
			break;
			case FOLLOW_LOCKED:
				robot.follow(current);
				if (!robot.searchWall(next)){
					state = FOLLOW;
					_delay_ms(10);
				}
			break;
			case TURN_AROUND:
				if (current == &robot.right){
					current->o_wheel->set(210, FORWARD);
					current->wheel->set(100, FORWARD);
				}
				else {
					current->o_wheel->set(200, FORWARD);
					current->wheel->set(100, FORWARD);
				}
				while(current->sensor->getAverageDistance() >= 15){
				}
				state = FOLLOW;
			break;
		}
    }
    return 0;
}

bool changeSide(){
	if (robot.isWall(next)){
		Side* temp = current;
		current = next;
		next = temp;
		wall = true;
		return true;
	}
	
	else {
		for (uint8_t i = 0; i < 3; i++)
		{
			robot.buzzer.playNote(BUZZER_NOTE);
			_delay_ms(200);
            robot.buzzer.stop();
            _delay_ms(100);
		}
		return false;
	}
}
