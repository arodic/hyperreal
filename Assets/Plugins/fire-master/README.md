fire
====

A fire effect for Unity.

**[video](http://robert.cupisz.eu/post/76961991149/fire)**

![fire and embers](https://dl.dropboxusercontent.com/u/2264982/FireAndEmbers/fireandembers.png)

In this repo I haven't included any meshes or textures, so you get just the stripped down fire effect:
![fire](https://dl.dropboxusercontent.com/u/2264982/FireAndEmbers/fire.png)


Download
--------
Check out this repo into a subfolder of your Unity project (visible meta files), e.g. `Assets/fire/`

Fire should work in the free version of Unity 4.5, and was tested on Windows and OSX.

Version 1.


Notes
-----
I didn't include the shader for the embers, but it's easy to reuse the FBM() noise function from the fire shader to animate the emissive channel of your embers shader.

Take a look at the settings on the fire material: toggle depth aware on/off and change the detail level (number of noise octaves).

You can affect the look of the effect a lot by scaling the fire object non-uniformly (firewall, stove top, etc.) and by painting a differently shaped fire texture.

License
-------
Public domain.
