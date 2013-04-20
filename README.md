NDriven CLI
===========

A command line tool for starting projects with the NDriven baseline.

Usage
-----
```
NDriven CLI 0.1.0
Usage: ndriven -s NewSolutionName -d /path/to/solution/location

  -s, --solution    Required. The solution name

  -d, --directory   The directory to extract the project to

  --help            Display this help screen
```

For now the command line tool will fetch the latest version of the NDriven baseline from Github, and
rename the solution file.

Building
--------
The easiest way to build and use for now is Visual Studio. Just build and move the resulting ndriven.exe
to a preferred location.

Todo
----
*Support renaming of core projects
*Support version specification
*Create NuGet package