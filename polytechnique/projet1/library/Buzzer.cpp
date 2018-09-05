#include "Buzzer.h"
#include <math.h>

Buzzer::Buzzer(volatile uint8_t &port, uint8_t LSP, uint8_t MSP) : Component(port, LSP, MSP, OUT)
{
    TCCR0A |= (1 << WGM01) | (1 << COM0A0) | (1 << COM0B0); // setting CTC mode
    
    prescaler = 256;    //FCPU diviser
    TCCR0B |= (1 << CS02);  //setting prescaler

    //setting top to 0 for no sound
    OCR0A = 0;
}

void Buzzer::playNote(uint8_t note)
{
    // we ignore out of range notes
    if (note >= 45 && note <= 81)
    {
        /* find frequency from note. 
            reference: https://en.wikipedia.org/wiki/MIDI_tuning_standard
        */
        float freq = pow(2.0f, (note - 69.0f) / 12.0f) * 440.0f;

        /* calculate maximun value for counter
            reference:
                Atmel ATmega324PA [DATASHEET] page 137
        */
        int TOP = F_CPU / (2.0f * prescaler * freq) - 1.0f;
        OCR0A = TOP;
    }
}

void Buzzer::stop()
{
    OCR0A = 0;
}