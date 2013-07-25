# CodeBuilder

A simple Class for building up C# code using functions...

## License
MIT, Do No Evil...

## Usage

The simplest 'Hello World' example is such:

    var programBuilder = new ClassRenderer(namespaceRef: "HelloWorldNamespace", className: "HelloWorldProgram");
    // Add the [directives]
    programBuilder.AddDirective("System")
        .AddDirective("System.Linq");
            
    // Add class interfaces if there are any
    programBuilder.AddInterface("ISomeInterface");

    // Add fields
    programBuilder.AddField("String", "_greetings");


    // Build an empty constructor
    var constructorMethod = programBuilder.AddConstructor();
    constructorMethod.AddLine("_greetings = \"Greetings!\";");

    // Build another constructors
    var greetingConstructor = programBuilder.AddConstructor( new []{"string greeting"} );
    greetingConstructor.AddLine("_greetings = greeting;");

    // Add a public method
    // same for AddProtected, AddPrivate
    var pubMethod = programBuilder.AddPublicMethod(
                "Greet", // name of the method
                "string", // return type of the method
                new[] {"string name"} // an string array of inputs
    
    // build the internals of the public method
    pubMethod.AddLine("return String.Format(\"{0}, Its nice to meet you, {1}\", _greetings, name);");
            
    // Build the class
    var programString = programBuilder.Emit();

This should produce a program that looks like this:

    using System;
    using System.Linq;

    namespace HelloWorldNamespace {
        class HelloWorldProgram : ISomeInterface {
            private String _greetings;
            public HelloWorldProgram() {
                _greetings = "Hello";
            }

            public HelloWorldProgram(string greeting) {
                _greetings = greeting;
            }

            public string Greet(string name){
                return String.Format("{0}, Its nice to meet you, {1}", _greetings, name);
            }
        }
    }

There are other API documented in the NUnit tests.

## Author
Evin Grano
- twitter: @etgryphon
- github: @etgryphon
- email: etgryphon@icloud.com