# Robot Spiders

## Solution

The solution has a console application **RobotSpiders** that can be used to run the process which is coordinated by the **RobotSpiders.Process** project, which uses functionality from the following projects:

- **RobotSpiders.Core:** Contains domain objects and mappers.
- **RobotSpiders.InputReaders:** Contains functionality to read the input data from a text file, other file formats could be added in the future by implementing the IFileInputReader interface (e.g. csv, xml, json).
- **RobotSpiders.Exporters:** Contains functionality to export the spiders position data to a text file, other file formats could be added in the future by implementing the IFileOutputWriter interface(e.g. csv, xml, json).

## Sample Data

The sample data provided for the exercise can be found in the **samples** folder. 

## Running the Application

The application can be run directly from **Visual Studio**, the path to the file to be process would need to be added to **Application arguments** for the **RobotSpiders** project (e.g. C:\Ingenie\RobotSpiders\samples\InputData.txt).

Alternatively, an executable can be published using the following command:

**dotnet publish -c Release -r win10-x64**

The executable file can then be found under **.\Ingenie\RobotSpiders\src\RobotSpiders\bin\Release\netcoreapp2.2\win10-x64**, below is an example on how to run it.

**.\RobotSpiders.exe 'C:\Ingenie\RobotSpiders\samples\InputData.txt'**

## Output File

The output file will be exported to the same folder as the input file with the file format **SpiderPositions_{Date.Now.Ticks}.txt**. 
