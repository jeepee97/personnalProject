#ifndef F_CPU
#define F_CPU 8000000
#endif

#include <avr/io.h>
#include <util/delay.h>
#include <avr/interrupt.h>
#include "UART.h"

UART::UART()
{
    // initialisation du UART.
    /*	les ports UCSR0A et UCSR0B permettent la reception 
    *  et la transmission par le UART0
    * 
    *  le port UCSR0C set le format des trames, dans notre cas
    *  c'est sur 8 bit avec 1 stop bit, none parity
    */
    UBRR0H = 0;
    UBRR0L = 0xCF;

    UCSR0A = 0;
    UCSR0B = (1 << RXEN0) | (1 << TXEN0);
    UCSR0C = (1 << USBS0) | (3 << UCSZ00);
}


void UART::enableReception(bool enable)
{
    if (enable)
    {
        UCSR0B |= (1 << RXCIE0); //RXCIE0 is set to enable interrupt when data is recieved
    }
    else
    {
        UCSR0B &= ~(1 << RXCIE0); //RXCIE0 is set to enable interrupt when data is recieved
    }
}

// USART_Receive
/* appeler la fonction permet de lire 1 octet qui a ete transmit vers le
 * robot
 */
uint8_t UART::receive()
{
    while (!isUnread())
    {
        //wait for data
    }
    return UDR0;
}

bool UART::isUnread()
{
    // returns true when there is unread data in the recieve buffer
    return UCSR0A & (1 << RXC0);
}

// send 1 byte
void UART::send(uint8_t donnee)
{
    while (!(UCSR0A & (1 << UDRE0)))
    {
        //wait the buffer to be empty
    }
    UDR0 = donnee;
}

//send a string
void UART::send(char *str)
{
    // stop when '\0' character found
    for (uint8_t i = 0; str[i] != '\0'; i++)
    {
        send(str[i]);
    }
}

// send 1 byte code followed by a string
void UART::send(uint8_t code, char *str)
{
    send(code);
    send(str);
}
// send 1 byte code followed by a 1 byte data
void UART::send(uint8_t code, uint8_t data)
{
    send(code);
    send(data);
}

void UART::send_char(uint8_t n){
    uint8_t p =100;
    for(uint8_t i =0;i<3;i++){
        send((n/p)%10+48);
        p = p/10;
    }
}