#ifndef _LED_H
#define _LED_H
#include "Configuration.h"
#include <avr/io.h>
#include "Component.h"

enum Color
{
    NONE,
    GREEN,
    RED
};

class LED : public Component
{
  public:
    LED(volatile uint8_t &port, uint8_t LSP, uint8_t MSP);
    void changeColor(Color color);
    void changeColor(uint8_t color);
    void turnOff();
    void blink(uint8_t n = 1, Color c = RED);
};
#endif