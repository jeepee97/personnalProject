#include "Wheel.h"
#include <avr/io.h>

Wheel::Wheel(volatile uint8_t &port, uint8_t speedPin, uint8_t dirPin) : Component(port, speedPin, dirPin, OUT)
{
    //Initialize PWM
    TCCR1A |= (1 << COM1B1) | (1 << COM1A1) | (1 << WGM10);
    TCCR1B |= (1 << CS11);
    TCCR1C = 0;
}
uint8_t Wheel::getSpeedPin()
{
    return LSP_;
}
uint8_t Wheel::getDirPin()
{
    return MSP_;
}
volatile uint8_t &Wheel::getPort()
{
    return port_;
}
void Wheel::set(uint8_t speed, Direction dir)
{
    //depends on connection of left and right wheel
    if (getSpeedPin() == OCR1A_PIN)
        OCR1A = speed;
    else if (getSpeedPin() == OCR1B_PIN)
        OCR1B = speed;

    //setting direction pin
    setBit(getPort(), getDirPin(), dir);
}
void Wheel::set(int8_t percentage)
{
    uint8_t speed = abs(percentage) * MAXIMUM_SPEED / 100.0f;
    set(speed, percentage > 0 ? FORWARD : BACKWARD);
}
