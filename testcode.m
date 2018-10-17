#include <Foundation/Foundation.h>
#include <UIKit/UIKit.h>

@interface CallMe : NSObject
-(void) callMe: (NSObject *) obj;
@end

void
callSelector (CallMe *obj, NSObject **args, int counter)
{
	for (int i = 0; i < counter; i++)
		[obj callMe: args [i]];
}

void
createArguments (NSObject ***array, int counter)
{
	*array = calloc (sizeof (NSObject *), counter + 1);
	for (int i = 0; i < counter; i++)
		(*array) [i] = [[UIView alloc] init];
}

void
freeArguments (NSObject **array, int counter)
{
	for (int i = 0; i < counter; i++)
		[array [i] release];
	free (array);
}
