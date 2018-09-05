#ifndef F_CPU
#define F_CPU 8000000
#endif
#include "PushButton.h"

PushButton::PushButton(volatile uint8_t &port, uint8_t pin) : Component(port, pin, IN), pressed_(false)
{
    // To make push button interrupts detectable
    EIMSK |= (1 << INT0);
}
bool PushButton::isPressed()
{
    return pressed_;
}
/*
void PushButton::set(bool pressed)
{
    pressed_ = pressed;
}
*/

//updates the push button state
void PushButton::update()
{
    bool state1 = PIND & 0x04; // read current value : 1-> button hold
    _delay_ms(15);              //wait some time
    bool state2 = PIND & 0x04;  // read value a second time
    pressed_ = state1 && state2; //if the 2 values are different we assume it's unhold 
}
