# AR-Collaboration
CS13: AR collaboration for PCB analysis with VR and Zed Mini
Team Members: Carson Pemble, Ryan Miura, Haozhe Li


Required Equipment:
HTC Vive VR headset
Stereolab's Zed Mini stereo camera
ArUco marker images 
PCB board 


Set Up Instructions:
1. Clone repo
2. Connect Hardware
3. Set up Unity (2019.2.17f1)
4. Load LoginScene and Run


Troubleshooting:

Controller Input Not Working at all?
Select the “Window” -> “SteamVR Input Live View” and you can see if the controller is registering the input. If it still doesn’t work make sure you have “Steam” open and running on your system. Steam has constant updates and SteamVR must be up to date to work properly.

If Laser ON/OFF is tracking the wrong hand?
Click on the “Right Hand” object and switch the device to 4 (if it is currently 3) or 3 (if it is currently 4). Do the same for the “Left Hand” object.

AR Image Marker isn't tracking correctly?
It could be that the size of your image is incorrect; AR Images are set to the default size of  5cm. If you print off or use ArUco images that are a different size, you can change this size within the project by opening the “ArUco Detection Manager” object and adjust the “Marker Width Meters” from 0.05 to the correct size.

Virtual AR Image plane isn't the correct size?
Currently Marker 0 (ArUco Marker ID 0) is set to the correct size for a Beagleboard XM. Marker 1 is set to a Beagleboard Black, and Marker 2 is set to a Raspberry Pi 4. If you want to assign an image to a new board, you can adjust the size of the corresponding cube in each Marker object. To add a new board select the “Marker 3” object and then select the “Cube” child object of  “Marker 3”. You can then adjust the “Transform” and “Box Collider” components to fit the correct size and placement of your board.


Thank you for looking into our project.
