#define F_CPU 8000000
#include <avr/io.h>
#include <util/delay.h>

#define SW3 0x08
#define SW5 0x20

int main(void)
{
	DDRB = 0xFF;
	PORTB = 0x00;

	DDRD = ~(SW3|SW5);
	PORTD = SW3|SW5;

	char running = 1;

	while (1)
	{
		PORTB = 0b00000001;
		while(PINB != 0x00)
		{
			_delay_ms(20);
			if(running == 1) PORTB = PINB << 1;
			if(~PIND & SW3) running = 1;
			if(~PIND & SW5) running = 0;
		}
	}
}
