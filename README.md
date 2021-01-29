# ClassToUmlConverter
This small application can help you converting C# classes into class diagrams. This project implementing 
[PlantUmlClassDiagramGenerator.Library](https://github.com/pierre3/PlantUmlClassDiagramGenerator) and [PlantUml.Net](https://www.nuget.org/packages/PlantUml.Net).

## Input project structure
- InputRootFolder
  - SubFolder1 (ex. Name of module/services) 
    - File1.cs
    - File2.cs
  - SubFolder2
    - File3.cs
    - File4.cs

## Output project structure
- OutputRootFolder
  - SubFolder1
    - puml
      - File1.puml
      - File2.puml
    - SubFolder1.svg
  - SubFolder2
    - puml
      - File3.puml
      - File4.puml
    - SubFolder2.svg
    
## Prerequisites
### PlantUml
Download [PlantUml](http://plantuml.com/download). By default PlantUml.Net looks for plantuml.jar in the woring directory; you can specify an alternate location in PlantUmlSettings.

### GraphViz Dot
Install [GraphViz Dot](https://graphviz.gitlab.io/download/) You may need to set the GRAPHVIZ_DOT environment variable
see the [PlantUml documentation](http://plantuml.com/graphviz-dot) for more detailed instructions.

### Configuration
Pointing configuration in **ClassToUmlConverterService.cs**
```
LocalPlantUmlPath = "<your path>\\plantuml.jar",
LocalGraphvizDotPath = "<your path>\\dot.exe",
```

## How to use the app
![Form](https://github.com/emriti/ClassToUmlConverter/blob/master/Assets/Form.png)
Pick input folder for **InputRootFolder** and output folder for **OutputRootFolder** and click **start**

## Sample Result
![Sample](https://github.com/emriti/ClassToUmlConverter/blob/master/Assets/Sample.PNG)
