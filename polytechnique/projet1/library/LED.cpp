#include "LED.h"
#include <util/delay.h>

LED::LED(volatile uint8_t &port, uint8_t LSP, uint8_t MSP) : Component(port, LSP, MSP, OUT)
{
}

void LED::changeColor(Color c)
{
    port_ = c << LSP_;
}

void LED::changeColor(uint8_t c)
{
    changeColor(static_cast<Color>(c));
}

void LED::turnOff()
{
    changeColor(NONE);
}

void LED::blink(uint8_t n, Color c)
{
    volatile uint8_t oldColor = port_;
    for (uint8_t i = 0; i < n; i++)
    {
        changeColor(NONE);
        _delay_ms(DELAY_BLINK);
        changeColor(c);
        _delay_ms(DELAY_BLINK);
    }
    changeColor((Color)oldColor);
}