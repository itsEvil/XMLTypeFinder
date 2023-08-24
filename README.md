# XMLTypeFinder
A small program to find unused types

### Requirements

.NET 6/7/8 Runtime/SDK
Any C# Editor (VS/Rider)

### Info
It was built using .NET 8 and tested it on .NET 6/7 and it worked. 
Change the project to your version of .NET (Default on .NET 8)

1. Either add your XML files to the xmls folder in the project or when you build it.
2. Build/Run the project.
3. Ranges of usable ushort values get printed to console and saved to a file.

### How does it work?

The program reads all the XML Files and saves them to a dictionary.
Then loops through 0 - ushort.MaxValue checking if the value exists in the dictionary. 
Figures out the ranges and prints them to the console and makes a file.
