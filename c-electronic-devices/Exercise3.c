 #define F_CPU 8000000UL
 #include <avr/io.h>
 #include <util/delay.h>
 #include <avr/interrupt.h>
 
 //#define step 1562.5			// STK-500
 #define step 156.25			// PROTEUS
 //#define minValue 781.25		// STK-500
 #define minValue 78.125		// PROTEUS
 //#define maxValue 16406.25	// STK-500
 #define maxValue 1640.625	 	// PROTEUS
 
 volatile float current;
 
 volatile int direction = 0;
 volatile int cycles = 0;
 
 ISR (TIMER1_COMPA_vect)
 {
	 if(cycles == 2)
	 {
		 cycles = 0;
		 if(direction == 0)
		 {
			 direction = 1;
			 current = maxValue;
		 }
		 else
		 {
			 direction = 0;
			 current = minValue;
		 }
	 }
	 
	 if(direction == 0)
	 {
		 current += step;
		 if(current > maxValue)
		 {
			 current = minValue;
			 cycles++;
		 }
	 }
	 else
	 {
		 current -= step;
		 if(current < minValue)
		 {
			 current = maxValue;
			 cycles++;
		 }
	 }
	 
	 OCR1A = current;
	 PORTB = ~((PORTB ^ 0xFF) >> 1);
	 if(PORTB == 0xFF) PORTB = 0b01111111;
 }

 int main(void)
 {
	 DDRB = 0xFF;
	 PORTB = 0xFF;
	 
	 current = minValue;
	 OCR1A = current;
	 
	 TCCR1B = (1 << CS12)|(0 << CS11)|(1 << CS10)|(1 << WGM12);
	 TIMSK1 = (1 << OCIE1A);

	 sei();
	 while (1) { }
 }
