Gesture friendly non-blocking dialog box for Windows Phone 8
==========

I've developed this one to replace a standard `MessageBox` in my WP8 projects because standard dialog box is too boring.  
Popuppy box accepts drag events and has to be moved to the left to "accept" or to the right to "decline".  
It has some kinematics and inertia management so the dragging process feels quite natural.

<img src="https://raw.github.com/tone00001/PopuppyWP8/master/wp_ss_20131218_0001.png" alt="Dialog box is on" width="200" /> 
<img src="https://raw.github.com/tone00001/PopuppyWP8/master/wp_ss_20131218_0003.png" alt="User drags a box" width="200" />

Minimal code needed to start a dialog
==========

`Manager.Show` is a static method that takes text parameters object and two `Action` statements: one to be triggered if user accepts a proposition and another if user turns it down.

`````c#
var vars = new TextParams { Message = "Do you want to proceed?", Ok = "Yepp", Cancel = "Nope" };
Manager.Show(vars, (se, ev) => Debug.WriteLine("Yepp"), (se, ev) => Debug.WriteLine("Nope"));
`````
