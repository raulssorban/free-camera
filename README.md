# Free Camera
A pretty complex script, and fully customizable written in clean code.
This code is taken from a project I'm working on and use it as testing.

Features
--------
The script is split in two, one for free camera and another optional for orbit a target.
What does it actually do?

### Target

- **Target Mode**: Use the optional target orbit?
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
- **Field Of View Lerp**: Field of view's lerping size.

### Field Of View
Field of view related customizable settings.

- **Min Field Of View**: Minimum scale of field of view for clamping.
- **Max Field Of View**: Maximum scale of field of view for clamping.
- **Midlle**: Middle of 0.1f to 1.0f. It approximates the start zooming (default 0.55f - 75.0f fov).

Keys
----
### Mouse KeyCodes
The keys to control.

- **Main Key**: This key allows the user to actually apply camera movement at all.
- **Field Of View Key**: This key being held with the main key will change the field of view of the camera.
- **Reset Field Of View Key**: This key resets the camera field of view (to 75.0f (default)).

### Keyboard KeyCodes

- **Forward Key**: Move forward.
- **Backward Key**: Move backward.
- **Left Key**: Move to the left.
- **Right Key**: Move to the right.
- **Pan Up Key**: Panning up.
- **Pan Down Key**: Panning down.

Compatibility
-------------
Just as an obvious note, this is for Unity.

Inspector
---------
![screenshot](http://i.imgur.com/aqiBytP.png)

License
-------
This script is fully written by me.
