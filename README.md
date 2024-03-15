
# A-Flat-Interpreter

## A very basic interpreter made in C#. 

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
    string: WelcomeText = "Welcome to Ab!";
    int: myNumber = 5;

    Console.out.info(WelcomeText);
    Console.out.warning(myNumber);
    Console.out.info("Going to another function!");

    run AnotherFunction();
}

function AnotherFunction()
{
    Console.out.error("End of application!");
}

</pre>
