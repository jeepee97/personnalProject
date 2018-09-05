#include "Component.h"

void setBit(volatile uint8_t &byte, uint8_t index, bool val)
{
    byte ^= (-val ^ byte) & (1 << index);
}


volatile uint8_t &getDDR(volatile uint8_t &port)
{
    if (&port == &PORTA)
        return DDRA;
    else if (&port == &PORTB)
        return DDRB;
    else if (&port == &PORTC)
        return DDRC;
    return DDRD;
}



Component::Component(volatile uint8_t &port, uint8_t LSP, uint8_t MSP, IO io) : port_(port), LSP_(LSP - 1), MSP_(MSP - 1)
{
    volatile uint8_t &ddr = getDDR(port_);

    //LSP and MSP are the pins we want to connect our component (1 to 8 on board)
    setBit(ddr, LSP_, io);
    setBit(ddr, MSP_, io);
}

Component::Component(volatile uint8_t &port, uint8_t PIN, IO io) : port_(port), LSP_(PIN - 1)
{
    volatile uint8_t &ddr = getDDR(port_);

    //LSP and MSP are the pins we want to connect our component (1 to 8 on board)
    setBit(ddr, LSP_, io);
}