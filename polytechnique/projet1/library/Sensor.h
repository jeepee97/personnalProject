#pragma once
#include "Configuration.h"
#include <avr/io.h>
#include "Component.h"

bool isValid(uint8_t distance_1, uint8_t distance_2);

class Sensor : public Component
{
  public:
    Sensor(volatile uint8_t &port, uint8_t pin);
    uint8_t getAverageDistance(); // read many values and return most accurate
    uint8_t getPin();
    uint8_t getDistance(); // read only one time
};
