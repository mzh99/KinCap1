# KinCap1
Kinect for Windows MoCap Test App

**Purpose:** 
Capture body motion-capture data for major joints that can be passed to 3D animation tools like Maya.

**Requirements:**
- Install the Kinect SDK 2.0 following the Getting Started Guide at: https://docs.microsoft.com/en-us/previous-versions/windows/kinect/dn782035(v=ieb.10)
- Plug in the Kinect hardware to install driver and reboot as needed.
- Run the SDK Browser app (installed with SDK).
- Run the Kinect Config Verifier tool. Look for any errors. Warnings on USB 3.0 ports may or may not be a problem as long as the other items in the checklist are green.
- Try the samples to validate the hardware is working.

**Notes:**
This project is fairly old (2014). The hardware and SDK has been discontinued by Microsoft but it's still a nice, capable piece of hardware to do poor man's motion-capture.
Updated in 2020 to use *System.Numerics* for Matrix and Vector math. A third-party library was used in the original version.
