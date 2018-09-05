#pragma once
#include "Configuration.h"
#include <avr/io.h>
#include <util/delay.h>
class UART
{
  public:
    UART();
    uint8_t receive();
    void send(uint8_t code, char *str);   // send 1 byte code followed by a string
    void send(uint8_t code, uint8_t data);// send 1 byte code followed by a 1 byte data
    bool isUnread(); // returns true when there is unread data in the recieve buffer
    void enableReception(bool enable);
    void send(uint8_t donnee);  // send 1 byte
    void send(char *str);       //send a string
    void send_char(uint8_t);
};
