# AR-Collaboration
CS13: AR collaboration for PCB analysis with VR and Zed Mini
Team Members: Carson Pemble, Ryan Miura, Haozhe Li


### Required Equipment:
* VR Capable Computer
* HTC Vive VR headset
* Stereolab's [ZEDMini] stereo camera
* [ArUco] marker images (MarkerID's 0-4)
* PCB board 


### Set Up Instructions:
1. Clone [Repo] to your local machine.
2. Download [ZEDSDK].
3. Download and Set up [Unity] (2019.2.17f1).
4. Download [ZEDUnity] plugin and import into Unity.
5. Download and install [Steam] (for SteamVR).
6. Download and run [SteamVR].
7. Download and install [Visual Studio 2019].
8. Open Server.sln file in Visual Studio.
9. Start the Server using the Run button at the top of the window labeled Server.
10. Set up HTC [Vive] and base stations.
11. Attach the ZED Mini to Vive and then connect the [combined] hardware to the machine (usb 3.0).
12. Open the AR Collaboration project in Unity.
13. Select LoginScene (Project -> Assets -> Scenes -> LoginScene) and press Play.  
14. To view audio, build and run the project (File -> Build And Run).  
15. Select Main Scene (Project -> Assets -> Client -> Scenes -> Main) and press Play.  


### Troubleshooting:
Controller Input Not Working at all?
> Select the “Window” -> “SteamVR Input Live View” and you can see if the controller is registering the input. If it still doesn’t work > make sure you have “Steam” open and running on your system. Steam has constant updates and SteamVR must be up to date to work 
> properly.

If Laser ON/OFF is tracking the wrong hand?
> Click on the “Right Hand” object and switch the device to 4 (if it is currently 3) or 3 (if it is currently 4). Do the same for the 
> “Left Hand” object.

AR Image Marker isn't tracking correctly?
> It could be that the size of your image is incorrect; AR Images are set to the default size of  5cm. If you print off or use ArUco 
> images that are a different size, you can change this size within the project by opening the “ArUco Detection Manager” object and 
> adjust the “Marker Width Meters” from 0.05 to the correct size.

Virtual AR Image plane isn't the correct size?
> Currently Marker 0 (ArUco Marker ID 0) is set to the correct size for a Beagleboard XM. Marker 1 is set to a Beagleboard Black, and 
> Marker 2 is set to a Raspberry Pi 4. If you want to assign an image to a new board, you can adjust the size of the corresponding cube > in each Marker object. To add a new board select the “Marker 3” object and then select the “Cube” child object of  “Marker 3”. You can > then adjust the “Transform” and “Box Collider” components to fit the correct size and placement of your board.


Thank you for looking into our project.


[Repo]: <https://github.com/CS-Senior-Project/AR-Collaboration>
[ZEDMini]: <https://www.stereolabs.com/zed-mini/>
[ArUco]: <http://chev.me/arucogen/>
[ZEDSDK]: <https://www.stereolabs.com/developers/release/>
[ZEDUnity]: <https://github.com/stereolabs/zed-unity/releases/>
[Unity]: <https://unity3d.com/get-unity/download>
[Steam]: <https://store.steampowered.com/about/>
[SteamVR]: <https://store.steampowered.com/app/250820/SteamVR/>
[Vive]: <https://www.vive.com/us/setup/>
[combined]: <https://www.stereolabs.com/zed-mini/setup/vive/>
[Visual Studio 2019]: <https://visualstudio.microsoft.com/vs/>
