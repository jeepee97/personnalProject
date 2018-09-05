#include "Configuration.h"
void delay_loop(uint16_t time){
    for(uint16_t i =0;i<time;i++)
        _delay_ms(1);
}

uint8_t abs(int n){
    if(n<0)
    return -n;
    return n;
}