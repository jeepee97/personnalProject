#ifndef _COMPONENT_H
#define _COMPONENT_H
#include "Configuration.h"
#include <avr/io.h>

enum IO {IN, OUT};

//returns the DDR of the given PORT
volatile uint8_t &getDDR(volatile uint8_t &port);

//sets the bit at the position index (0-->7) to val (0 or 1)
void setBit(volatile uint8_t &byte, uint8_t index, bool val);

/*
    This class is to be inherited by:
        - LED
        - Sensor
        - Buzzer
        - PushButton
        - Wheel
    It sets the corresponding ports to in or out and saves the port and pins
*/
class Component
{
    public:
        Component(volatile uint8_t& port, uint8_t LSP, uint8_t MSP, IO io); 
        Component(volatile uint8_t& port, uint8_t PIN, IO io);  
    protected:
        volatile uint8_t& port_;
        uint8_t LSP_;   //LSP: least significant pin
        uint8_t MSP_;   //MSP: least significant pin
};

#endif
