#pragma once
#include "Configuration.h"
#include <avr/io.h>
#include "Component.h"

enum Direction
{
    FORWARD,
    BACKWARD
};

/*
    PWM setup must be done by Engine
*/
class Wheel : public Component
{
  public:
    Wheel(volatile uint8_t &port, uint8_t speedPin, uint8_t dirPin);
    uint8_t getSpeedPin();
    uint8_t getDirPin();
    volatile uint8_t &getPort();
    void set(uint8_t speed, Direction dir);
    void set(int8_t percentage);  //-100, -75, -50, -25, 0, 25, 50, 75, 100: negative means move backwards
};