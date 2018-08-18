#define F_CPU 8000000
#include <avr/io.h>
#include <util/delay.h>
#include <avr/interrupt.h>

#define SW4 0x01
#define SW6 0x02

char counter = 0;

ISR(PCINT1_vect)
{
	if(~PINC & SW4 && ~PINC & SW6)
	{
		PORTD = ~counter;
		_delay_ms(8000);
		PORTD = 0x00;
	}
}

int main(void)
{
	DDRD = 0xFF; // LEDs as output
	PORTD = 0xFF; // Turn off LEDs

	DDRC = ~(SW4|SW6); // BTNs input
	PORTC = SW4|SW6; // Pull up buttons

	PCICR = 0b00000010; // PORT C interrupts
	PCMSK1 = 0b00000011; // PORT C - 1; 2

	sei(); // Turn on interrupts

	while (1)
	{
		PORTD = 0b01111111;
		while(PORTD != 0xFF)
		{
			_delay_ms(20);
			PORTD = ~((PORTD ^ 0xFF) >> 1);
		}
		counter++;
	}
}
