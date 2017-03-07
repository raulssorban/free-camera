# Free Camera
A pretty complex script, and fully customizable written in clean code.
This code is taken from a project I'm working on and use it as testing.

Features
--------
The script is split in two, one for free camera and another optional for orbit a target.
What does it actually do?

### Target

- **Use Target**: Use the target?
- **Distance**: Camera distance between target transform and camera itself.
- **Target Transform**: The target transform that would be orbited.

### Speeds
Controlled with the keys.

- **Normal Speed**: Normal speed (is being used when LeftShift (default) **is not** held).
- **Sprint Speed**: Sprint speed (is being used when LeftShift (default) **is** held).

### Lerp
Smoothing the camera rotation / position movement.

- **Use Lerp**: Use lerping?
- **Position Lerp**: Position lerping size.
- **Rotation Lerp**: Rotation lerping size.

### Keys
The keys to control.

- **Main Key**: This key allows the user to actually apply camera movement at all.
- **Field Of View Key**: This key being held with the main key will change the field of view of the camera.
- **Reset Field Of View Key**: This key resets the camera field of view (to 75.0f (default)).

Compatibility
-------------
Just as an obvious note, this is for Unity.

Inspector
---------
![screenshot](http://i.imgur.com/aqiBytP.png)

License
-------
This script is fully written by me.
