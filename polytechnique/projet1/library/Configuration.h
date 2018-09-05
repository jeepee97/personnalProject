#ifndef F_CPU
#define F_CPU 8000000
#endif

#include <util/delay.h>
#include <avr/interrupt.h>
#include <avr/io.h>
/********************* PORTS *********************/

//led
#define LED_PORT PORTB
#define LED_MSP 2 //+
#define LED_LSP 1 //-

//buzzer
#define BUZZER_PORT PORTB
#define BUZZER_MSP 4
#define BUZZER_LSP 3

//right wheel
#define RIGHT_WHEEL_PORT PORTD
#define RIGHT_WHEEL_DIR 8
#define RIGHT_WHEEL_PWM 6

//left wheel
#define LEFT_WHEEL_PORT PORTD
#define LEFT_WHEEL_DIR 7
#define LEFT_WHEEL_PWM 5

//push button
#define BUTTON_PORT PORTD
#define BUTTON_PIN 3

//right sensor
#define RIGHT_SENSOR_PORT PORTA
#define RIGHT_SENSOR_PIN 8

//left sensor
#define LEFT_SENSOR_PORT PORTA
#define LEFT_SENSOR_PIN 7
/*****************************************************/

/******************* CONFIGURATION *******************/
#define SENSOR_REPETITION 25
#define DISTANCE_TOLERANCE 2 // in cm
#define DELAY_BLINK 100 // in ms
#define _90_SPIN_MS 1000 // duration in s
#define NORMAL_SPEED 150
#define ROTATION_SPEED 100
#define BUZZER_NOTE 80
#define MODE_BYTE_ADRESS 0x0000
#define MAXIMUM_SPEED 255
/*****************************************************/

/********************* OCR1n *********************/
#define OCR1A_PIN 5 //6 on board
#define OCR1B_PIN 4 //5 on board
/*************************************************/

void delay_loop(uint16_t time);
uint8_t abs(int n);
