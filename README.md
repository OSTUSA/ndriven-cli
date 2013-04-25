NDriven CLI
===========

A command line tool for starting projects with the [NDriven](https://github.com/OSTUSA/ndriven) baseline.

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
The easiest way to build and use is NuGet. From NuGet.exe the following command should be used:

```
nuget install ndriven-cli -Version 0.1.4
```

The resulting ndriven.exe can be found in src\Presentation.Console\bin\Release

Todo
----
* Support renaming of core projects
* Support version specification