# ---Added by Iconoclass---
## Setup
[![video preview](https://img.youtube.com/vi/hRjFOVZbyNs/0.jpg)](https://www.youtube.com/watch?v=hRjFOVZbyNs)

WATCH THE VIDEO TUTORIAL ON HOW TO SET UP THIS PREFAB ON YOUTUBE!
https://www.youtube.com/channel/UC3aKsENcL8o2C1Wz5N0sIXg/videos

**IMPORT THE FOLLOWING TO USE THIS PREFAB:**
* VRCHAT CREATOR COMPANION			https://vrchat.com/home/download
* BFW SIMPLE DYNAMIC CLOUDS	(Optional, now not avaliable on Unity Asset Store)		https://assetstore.unity.com/packages/tools/particles-effects/bfw-simple-dynamic-clouds-85665

OPEN THE LIGHTING TAB (Window>Rendering>Lighting Settings)
SET THE Enviornment Lighting TO COLOR

Drag the main Daynight Prefab v2 into the scene. Don't forget to unpack it! Udon does not play well with prefabs!

The sun, moon, stars, reflection probe and clouds are all optional toggles within the script. You don't have to fill in all the slots.

**To create the cubemaps:**
* set the reflection probe to custom
* play in the Unity editor
* set the time of day to one of the 4 options: dawn, midday, dusk, or night (or make your own options by editing the script!)
* click bake in the reflection probe and save
* assign the cubemaps in the day/night controller script

(FOR REALTIME GI SET STATIC OBJECTS AS STATIC, TICK THE BOX FOR REALTIME GLOBAL ILLIUMINATION IN THE LIGHTING SETTINGS, AND THEN CLICK GENERATE LIGHTING)

If importing BFW CLOUDS: In the daynight controller script, under cloud materials, set the low cloud materials and high cloud materials.

## Other
I added headers, tooltips and time of day cubemaps (a reflection probe) to the script.
I also commented out parts of the script specific to Nova_Max's world not wildly applicable.
Feel free to modify the script to suit your needs, or make and premote a fork (especially if it is more optimized and adaptable through the inspector).

## My worlds featuring the day night cycle
Serene Seaside: https://vrchat.com/home/launch?worldId=wrld_8b709af3-4e49-4dec-9cbb-132b2234fe7f 
Snowy Valley Forest: https://vrchat.com/home/launch?worldId=wrld_fe8835b1-0e18-4db8-bf52-035fc0910e74

## Potential Future Updates
* Color palettes stored as scriptable objects
* Sun Seasons
* Modular object toggles based on time of day

--- End of edit --- 

# **Day Night Cycle Island**
https://vrchat.com/home/world/wrld_6c4f1572-a918-4e6d-b383-6b056111768f
## **Features:**
* Day night cycle
* Global time syncing
* Performance optimized real time lighting and shadows
* Quest support
* Time of day and speed controls (accessible by everyone)
* Performance options
* Mirror
* Seats
* Custom water shader
* Moving grass and trees
* Audio dynamically changes with time of day
* Campfire lights dring the night
* Leaf particles
* Procedural Clouds
* Procedural Stars

My goal was to create a comfy place to hang out with the additional feature of the day night cycle to make the world feal more immersive.
I wanted everyone to be able to have fun with the day night cycle so I made the controls accessible for every player and not just the master. On the other hand the time of day and cycle speed are synced globally so everyone can enjoy the sunset at the same time or watch the stars together. 
I spent a lot of time optimizing the lighting and shadows so the world is able to run on all systems including quest. For that reason I was able to simplify the menu by removing most performance settings. The only settings that are still in the menu are for controlling the cycle and to toggle some of the visuals and background music. 

## **Assets/Tools used:**
* Udon Sharp
* Amplify Shader Editor
* Unity Default Skybox
* BFW Simple Dynamic Clouds
* Stylized Forest Environment

You can download the day night cycle controller script here. It will definitely work with the default skybox and should also work with any procedural skybox.

![Values](/Values.png)

## **Future Updates:**
* Moon cycle
* Seasons
* Weather
* More dynamic objects
* Videoplayer (as soon as Udon supports that)
