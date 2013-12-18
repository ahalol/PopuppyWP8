Gesture friendly non-blocking dialog box for Windows Phone 8
==========

I've developed this one to replace a standard `MessageBox` in my WP8 projects because standard dialog box is too boring.  
`Popuppy` box accepts drag events and has to be moved to the left to "accept" or to the right to "decline".  
It has some kinematics and inertia management so the dragging process feels quite natural.

![Dialog box is on](https://raw.github.com/tone00001/PopuppyWP8/master/wp_ss_20131218_0001.png "Dialog box is on")
![User drags a box](https://raw.github.com/tone00001/PopuppyWP8/master/wp_ss_20131218_0003.png "User drags a box")


Code needed to show `Popuppy` box
==========

`````c#
var vars = new TextParams { Message = "Do you want to proceed?", Ok = "Yepp", Cancel = "Nope" };
Manager.Show(vars, (se, ev) => Debug.WriteLine("Yepp"), (se, ev) => Debug.WriteLine("Nope"));
`````
