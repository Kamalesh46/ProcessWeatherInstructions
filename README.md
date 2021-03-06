# ProcessWeatherInstructions

# ProcessWeatherInstructions

Problem:

You are in your house wearing pajamas. You must be appropriately dressed for the temperature before leaving your house.

Your challenge is to programmatically process a list of commands for getting ready, enforce related rules, and display appropriate output.

Inputs:

Temperature Type (one of the following)
HOT
COLD
Comma separated list of numeric commands
Command

Description

					      HOT Response    COLD Response InstructionID == >Priority

Put on footwear 	“sandals” 		“boots” 1  ==> 3

Put on headwear		“sun visor” 	“hat” 	2  ==> 3

Put on socks 		fail 			“socks” 3  ==> 2

Put on shirt 		“t-shirt” 		“shirt” 4  ==> 2

Put on jacket		fail 			“jacket” 5 ==> 3

Put on pants		“shorts” 		“pants”	6  ==> 2

Leave house 		“leaving house” “leaving house” 7 ==> 4

Take off pajamas 	“Removing PJs”  “Removing PJs” 8 ==> 1

Rules:

Initial state is in your house with your pajamas on
Pajamas must be taken off before anything else can be put on
Only 1 piece of each type of clothing may be put on
You cannot put on socks when it is hot
You cannot put on a jacket when it is hot
Socks must be put on before shoes
Pants must be put on before shoes
The shirt must be put on before the headwear or jacket
You cannot leave the house until all items of clothing are on (except socks and a jacket when it’s hot)
If an invalid command is issued, respond with “fail” and stop processing commands
Examples

Success

Input: HOT 8, 6, 4, 2, 1, 7
Output: Removing PJs, shorts, t-shirt, sun visor, sandals, leaving house

Input: COLD 8, 6, 3, 4, 2, 5, 1, 7
Output: Removing PJs, pants, socks, shirt, hat, jacket, boots, leaving house

Failure

Input: HOT 8, 6, 6
Output: Removing PJs, shorts, fail

Input: HOT 8, 6, 3
Output: Removing PJs, shorts, fail

Input: COLD 8, 6, 3, 4, 2, 5, 7
Output: Removing PJs, pants, socks, shirt, hat, jacket, fail

Input: COLD 6
Output: fail
________________________________________________________________________________________________
Running the program : 

It is buildable in VS2013, 2015.

Input Format : HOT|COLD Instructions seperated by spaces and ','
Eg:HOT 1, 2, 3, 4, 5 ...

Output : Weather suggestions seperated by ','
Removing PJ's, socks, boots

For exiting from the program, type "exit" and press enter.
