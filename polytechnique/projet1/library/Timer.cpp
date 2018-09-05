#include "Timer.h"
Timer::Timer() : set_(false)
{
    TCCR1A = 0;
    TCCR1B = (1 << CS12) | (1 << CS10) | (1 << WGM12);
    TCCR1C = 0;
    TIMSK1 = (1 << OCIE1A);
}
void Timer::start(uint16_t duree)
{
    TCNT1 = 0x0000;
    OCR1A = duree;
}