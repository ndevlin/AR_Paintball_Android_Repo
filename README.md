# AR_Paintball_Android_Repo
 
Implemented in Unity using C#

Cube Synchrony: Rectangle on phone and computer rotate together in synchrony according to phone's gyroscope inputs


Paint Ball:

Paint balls shoot forward when pressing the front shoot button on the phone. They are present on both the phone and computer screen. Paintballs shoot up when the user swipes on the screen. When the camera cannot see the target image, the phone's gyroscope is used to aim the paintballs. The paintballs successfully shoot towards the screen and splat on the target image.


Improvements:

To make the experience more interactive, I altered the speed of the balls according to the speed at which the user swipes. If they swipe very quickly, the ball is launched at a high velocity, whereas if they swipe slowly, the ball will launch at a low velocity.

In addition to the above, I polished the look and feel of the game by placing the paint splats appropriately according to when they are shot. The most recent paintballs shot splat on top of those below, modeling how this process would work in real life. This fixes the z-order issue present in the previous version.


Note: The main files of interest are in the Assets folder, such as Assets/Scripts. The Windows build is in Windows_Build, and the Android build is in the Android_Phone_Build



