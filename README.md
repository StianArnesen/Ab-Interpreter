
# A-Flat-Interpreter

## A very basic interpreter made in C#. 

### Important things to know:
* All variables are global variables.
* if-statements should always be at the end of a function. 
	- This is because every instruction from a function is added to instruction stack when running a function.
	- This usually results in unintended behavior.
* Always use ';' at the end of each line.
* Curly brackets ( { } ) is not needed to wrap functions and will just be ignored.
* String/variable concatenation is not supported yet! ( "Stian" + lastName ) will not work.
* Only integers can be compared inside if-statements. ( if(a == b) -> trueFunc() : falseFunc(); )
* Comments are not supported.

### Currently Supports:

#### Functions/GOTO: 
Functions currently works as a GOTO statement where you can jump to a specified label. The program MUST contain a function named Main() since this works as the entry point for the application.

##### Syntax: 
<pre>
function Name();
run Name();
</pre>
#### Console:
Console print functionality with different text colors. (white, yellow, red) This also works with variables.

##### Syntax:
<pre>
Console.out.info("This text is white");

Console.out.warning("This text is yellow");

Console.out.error("This text is red");

// example of variable usage;
string: myVar = "This text is white";
Console.out.info(myVar);
</pre>

### Example program utilising all implemented functionality:

<pre>
// Adding the entry point.
function Main()
{
	run VariableSetup();
	run StartCounter();
}

function VariableSetup()
{
	int: ADD       = 1;
	int: Counter   = 0;
	int: MAX_COUNT = 3;
	
	string: testVar = "Enter a number: ";
	
	Console.out.warning(testVar);
	Console.input.integer( MAX_COUNT);
	
	Console.out.warning("Enter a string: ");
	Console.input.string(testVar);
}

function StartCounter()
{
	Console.out.warning("Starting counter!");
	run Count();
}

function Count()	
{
	Counter += ADD;
	Console.out.warning(Counter);
	if (MAX_COUNT > Counter) -> Count() : End();
	Console.out.warning("This should not be reached!");
}

function End()
{
	Console.out.error("End of application -- PROGRAM!");
}

</pre>
