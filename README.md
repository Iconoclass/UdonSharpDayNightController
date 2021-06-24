# ---Added by Iconoclass---
## **IMPORT THE FOLLOWING TO USE THIS PREFAB:**
* VRCHAT SDK3 WORLD			https://vrchat.com/home/download
* UDON SHARP				https://github.com/MerlinVR/UdonSharp/releases
* UDON TOOLKIT				https://github.com/orels1/UdonToolkit/releases
* BFW SIMPLE DYNAMIC CLOUDS			https://assetstore.unity.com/packages/tools/particles-effects/bfw-simple-dynamic-clouds-85665

- Open your `Project Settings` -> `UdonSharp` and in the `Default Behaviour Editor` select `UdonToolkit Editor`

I added tooltips and an UdonToolkit custom inspector to the script.
I also commented out parts of the script specific to Nova_Max's world.

Serene Seaside: https://vrchat.com/home/launch?worldId=wrld_8b709af3-4e49-4dec-9cbb-132b2234fe7f 
(My world featuring the day night cycle)

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
