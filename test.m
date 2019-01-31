#include <Foundation/Foundation.h>
#include <objc/objc.h>
#include <objc/message.h>

// NSDecimal get_NSNumber_decimalvalue (id number)
// {
// 	fprintf (stderr, "Number: %p\n", (void *) number);
// 	return [((NSNumber *) number) decimalValue];
// }

// typedef NSDecimal(*get_decimalvalue_func) (id number, SEL selector);

// NSDecimal get_decimalvalue (id number, SEL sel)
// {
// 	fprintf (stderr, "Number: %p SEL: %p=%s\n", (void *) number, (void *) sel, sel_getName (sel));
// 	get_decimalvalue_func func = (get_decimalvalue_func) objc_msgSend;
// 	return func (number, sel);
// }

NSDecimal get_number2 (int arg1, int arg2, int arg3, int arg4)
{
	fprintf (stderr, "get_number2 (%i, %i, %i, %i)\n", arg1, arg2, arg3, arg4);
	NSDecimal rv = { 0 };
	*((unsigned int *) &rv) = 12345678;
	for (int i = 0; i < 8; i++)
		rv._mantissa [i] = i + 10;
	return rv;
}