# CravoGameLib
[![Build Status](https://travis-ci.org/cravo/CravoGameLib.svg?branch=master)](https://travis-ci.org/cravo/CravoGameLib)

A work-in-progress game library of commonly used classes for C#, primarily built around MonoGame.
This is currently in early and sporadic development, do not expect a full-featured game library any time soon.  See the current open issues for some idea of my plans for the bits which already exist, and a few ideas about what to add as and when I think about them.

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
