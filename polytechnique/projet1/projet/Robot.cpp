#include "Robot.h"
Robot::Robot() : led(LED_PORT, LED_LSP, LED_MSP),
                 rightWheel(RIGHT_WHEEL_PORT, RIGHT_WHEEL_PWM, RIGHT_WHEEL_DIR),
                 leftWheel(LEFT_WHEEL_PORT, LEFT_WHEEL_PWM, LEFT_WHEEL_DIR),
                 button(BUTTON_PORT, BUTTON_PIN),
                 rightSensor(RIGHT_SENSOR_PORT,RIGHT_SENSOR_PIN),
                 leftSensor(LEFT_SENSOR_PORT, LEFT_SENSOR_PIN),
                 buzzer(BUZZER_PORT, BUZZER_LSP, BUZZER_MSP)
{
    right.sensor = &rightSensor;
    right.wheel = &rightWheel;
    left.sensor = &leftSensor;
    left.wheel = &leftWheel;

    right.o_sensor = &leftSensor;
    right.o_wheel = &leftWheel;
    left.o_sensor = &rightSensor;
    left.o_wheel = &rightWheel;

    //initialisation
    cli();
    //SREG |= (1 << 7); //enabling global interrupts
    // enabling external interrupts
    EICRA |= (1 << INT0);
    sei();
    toggle_mode();
}
void Robot::toggle_mode()
{
    //lecture du mode en memoire 0x00
    memoire.lecture(MODE_BYTE_ADRESS, &mode);
    //Changer de mode a chaque RESET ou alimentation
    mode = (mode != 0x01);
    memoire.ecriture(MODE_BYTE_ADRESS, mode);
}

uint8_t Robot::stabilizeDistance(Side* s){
	uint8_t distance = s->sensor->getAverageDistance();
    uint8_t distance2 = s->sensor->getAverageDistance();
    distance2 -= distance;
    if (distance2 < 0){
		distance2 = -distance2;
	}
	return distance2;
}

uint8_t Robot::getDistance(Side* s){
	return s->sensor->getAverageDistance();
}

bool Robot::searchWall(Side* s)
{
    //if we detect something no far then 60 cm we verify if its a wall or not
    uint8_t distance = s->sensor->getAverageDistance();
    if (distance <= 40 && distance >= 5 && (stabilizeDistance(s) <= 1))
    {
		return true;
    }
    return false; //In this case -1 says there are no walls
}

Side* Robot::searchClosesthWall()
{
    bool leftWall = searchWall(&left);
    bool rightWall = searchWall(&right);
    if (rightWall && leftWall)
    {
        return (leftSensor.getAverageDistance() < rightSensor.getAverageDistance())? &left : &right;
    }
    else if (!rightWall && leftWall)
    {
        return &left;
    }
    else if (rightWall && !leftWall)
    { //we turn to search in other directions
        return &right;
    }
    return 0;
}

void Robot::follow(Side* s)
{
    uint8_t distance = s->sensor->getAverageDistance();
    uint8_t speed = ROTATION_SPEED;
    if (!isValid(distance, 10))
    {
        led.changeColor(RED);
        if (distance > 10)
        { //if getting far from the wall
            s->wheel->set(ROTATION_SPEED, FORWARD);
        }
        else
        { //if too close to wall
			if (s == &right){
				speed -= 10;
			}
            s->o_wheel->set(speed, FORWARD);
        }
    }
    else
    {
        led.changeColor(GREEN);
        move(NORMAL_SPEED, FORWARD); // continue
    }
}
void Robot::update()
{
    uart.send(ETAT_BOUTON, button.isPressed() ? 0 : 1);
    uart.send(DISTANCE_DROIT, rightSensor.getAverageDistance());
    uart.send(DISTANCE_GAUCHE, leftSensor.getAverageDistance());
}
void Robot::identify()
{
    char nomRobot[7] = "UTOPIA";
    char numeroEquipe[5] = "0911";
    uint8_t numeroSection = 0x01;
    char session[5] = "18-1";
    uint8_t couleurBase = 1;

    uart.send(NOM_ROBOT, nomRobot);
    uart.send(NUMERO_EQUIPE, numeroEquipe);
    uart.send(NUMERO_SECTION, numeroSection);
    uart.send(SESSION, session);
    uart.send(COULEUR_BASE, couleurBase);
}

bool Robot::isWall(Side* s)
{
    uint8_t distance2 = s->sensor->getAverageDistance();
    _delay_ms(800);
    uint8_t distance3 = s->sensor->getAverageDistance();
    if ((distance2 < distance3) && (!isValid(distance2, distance3)))
        return false;
    return true;
}

void Robot::setWheels(uint8_t speedL, uint8_t speedR){
	rightWheel.set(speedR, FORWARD);
	leftWheel.set(speedL, FORWARD);
}

void Robot::move(uint8_t speed, Direction dir)
{
    rightWheel.set(speed, dir);
    leftWheel.set(speed - 10, dir);
}

void Robot::turn(Side* s)
{
    s->wheel->set(0);    //0%
    s->o_wheel->set(70); //70%
}

// tourner sur place
void Robot::spin(Side* s, uint16_t angle)
{
    s->wheel->set(-65); //backward
    s->o_wheel->set(65); //forward
    for (int i = 0; i < (angle*_90_SPIN_MS/90.0) ; i++)
        _delay_ms(2);
    stop();
}

void Robot::stop()
{
    rightWheel.set(0, FORWARD);
    leftWheel.set(0, FORWARD);
}
