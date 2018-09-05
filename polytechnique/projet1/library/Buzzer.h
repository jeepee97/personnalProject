#ifndef _BUZZER_H
#define _BUZZER_H
#include "Configuration.h"
#include <avr/io.h>
#include "Component.h"

class Buzzer : public Component
{
  public:
    Buzzer(volatile uint8_t &port, uint8_t LSP, uint8_t MSP);
    void playNote(uint8_t note);
    void stop();

  private:
    uint16_t prescaler;
};

#endif