#pragma once
#include "Configuration.h"
#include <avr/io.h>
#include <util/delay.h>
#include "LED.h"
#include "Component.h"

class PushButton : public Component
{
  public:
    PushButton(volatile uint8_t &port, uint8_t pin);
    bool isPressed();
    //void set(bool pressed);
    void update();//updates the push button state

  private:
    bool pressed_;
};