#pragma once
#include "Configuration.h"
#include <avr/io.h>
#include <util/delay.h>

class Timer
{
  public:
    Timer();
    void start(uint16_t duree);

  private:
    bool set_;
};