# CravoGameLib
A game library of commonly used classes.
Primarily built around MonoGame.
See CravoGameLibTest for an example which demonstrates:
* Loading and rendering a Texture2D
* Loading and drawing a TileMap

##Current Features:

### Data
DataManager
* Capable of loading and processing data types
* You can add new DataProcessors to support new types
* Currently only supports loading Texture2D data

### Extensions
XmlElement
* TryGetString, TryGetInt, TryGetFloat to get attribute values and specify a default

### TileMap 
* Basic orthographic tile map rendering
* (Limited) Support for TileEd (http://www.mapeditor.org/) maps (orthographic, CSV only)
