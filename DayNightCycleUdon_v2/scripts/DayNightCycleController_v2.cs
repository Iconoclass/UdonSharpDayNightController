using System;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
//using UdonToolkit;
using UnityEditor;
using UnityEngine.Serialization;

//[CustomName("Day/Night Cycle Controller v2")]
public class DayNightCycleController_v2 : UdonSharpBehaviour
{

    /*
#if UNITY_ANDROID
        private bool isQuest = true;
#else
    private bool isQuest = false;
#endif
    */

    [Header("UI components")]
    [Tooltip("Requires a canvas")]
    public Slider SpeedSlider;
    public Slider TimeSlider;
    public Toggle LocalToggle;

    //public GameObject FireFX;

    [Header("Time")]
    [UdonSynced]
    public float SetTime = 0.2f;
    [UdonSynced]
    public int syncid = 0;
    public int lastreceivedid = 0;
    [UdonSynced]
    public float SetSpeed = 1 / 600f;
    
    [Range(0, 1)]
    public float CurrentTimeOfDay = 0.2f;
    public float Speed = 1 / 600f;
    public float TimeMultiplier = 1f;

    
    [Header("SET Environment Lighting > Source TO Color IN LIGHTING WINDOW!")]
    [Tooltip("Color at set point in the cycle")] public Color AmbientColor1;
    [Tooltip("Color at set point in the cycle")] public Color AmbientColor2;
    [Tooltip("Color at set point in the cycle")] public Color AmbientColor3;
    public float AmbientPoint1 = 0.2f;
    public float AmbientPoint2 = 0.25f;
    //[HelpBox("SET ENVIORNMENT LIGHTING > SOURCE TO COLOR IN LIGHTING WINDOW!")] [UTEditor]
    public float AmbientPoint3 = 0.35f;
    
    
    //[SectionHeader("Scene object references")][UTEditor]
    [Header("Scene object references")] 
    public bool UseSun = true;
    [Tooltip("Directional Light")]
    public Light Sun;
    //[SectionHeader("Defines Color at set point in the cycle")][UTEditor]
    //[Header("Color at set point in the cycle")]
    [Tooltip("Color at set point in the cycle")] public Color SunColor1;
    [Tooltip("Color at set point in the cycle")] public Color SunColor2;
    public float SunPoint1 = 0.25f;
    public float SunPoint2 = 0.35f;
    
    //[Header("Light intensity at set points in the cycle")]
    public float SunIntensityPoint1 = 0.23f;
    public float SunIntensityPoint2 = 0.25f;
    
    [Space]
    [Header("Custom reflection probe for different times of day")]
    public bool UseReflectionProbe = true;
    [FormerlySerializedAs("Probe")] public ReflectionProbe RefProbe;
    public Cubemap DawnCubemap;
    public Cubemap DayCubemap;
    public Cubemap DuskCubemap;
    public Cubemap NightCubemap;
    
    [Space]
    [Header("Sky objects")] 
    public bool UseSky = true;
    [Tooltip("Should include the stars particle system and moon mesh gameobjects. Allows them to rotate in the sky")]
    public GameObject SkyObject;
    [Header("Stars")]
    [FormerlySerializedAs("Stars")] [SerializeField] private bool UseStars = true;
    public Material StarsMat;
    [Tooltip("A spherical particle system")]
    public GameObject StarsObject;
    
    //[Header("Color at set point in the cycle")]
    [Tooltip("Color at set point in the cycle")] public Color StarColor1;
    [Tooltip("Color at set point in the cycle")] public Color StarColor2;
    public float StarPoint1 = 0.2f;
    public float StarPoint2 = 0.25f;
    public float StarCutoff = 0.3f;
    
    [Header("Moon")]
    [SerializeField] private bool UseMoon = true;
    [FormerlySerializedAs("Moon")] public Material MoonMat;
    
    //[Header("Color at set point in the cycle")]
    [Tooltip("Color at set point in the cycle")] public Color MoonColor1;
    [Tooltip("Color at set point in the cycle")] public Color MoonColor2;
    public float MoonPoint1 = 0.2f;
    public float MoonPoint2 = 0.25f;
    
    
    [Space]
    [Header("For use with BFW clouds (no longer available on Unity Asset Store)")]
    [Header("Cloud Materials (Optional)")]
    [SerializeField] private bool UseClouds;
    public Material LowCloud;
    [Tooltip("BFW Clouds material")]
    public Material HighCloud;
    //[Header("Color at set point in the cycle")]
    [Tooltip("Color at set point in the cycle")] public Color CloudColor1;
    [Tooltip("Color at set point in the cycle")] public Color CloudColor2;
    [Tooltip("Color at set point in the cycle")] public Color CloudColor3;
    public float CloudPoint1 = 0.2f;
    public float CloudPoint2 = 0.25f;
    public float CloudPoint3 = 0.35f;
    
    /* Was used with REDSIM's Water Shaders
    public Color WaterFarColor1;
    public Color WaterFarColor2;
    public Color WaterFarColor3;
    public Color WaterCloseColor1;
    public Color WaterCloseColor2;
    public Color WaterCloseColor3;
    public float WaterPoint1 = 0.2f;
    public float WaterPoint2 = 0.25f;
    public float WaterPoint3 = 0.35f;
    */
    
    /*
    [Header("Audio Source")]
    public AudioSource Audio;
    
    //[UTEditor]
    [Header("Defines when an audio source is played at set points in the cycle")]
    public float AudioPoint1 = 0.25f;
    public float AudioPoint2 = 0.35f;
    */

    float SunInitialIntensity;

    bool local;
    private bool lowCloudNotNull;
    private bool highCloudNotNull;

    private Color c;

    void Start()
    {
        SunInitialIntensity = Sun.intensity;
        //BirdsInitialVolume = Birds.volume;
        //CicadasInitialVolume = Cicadas.volume;
        UnityEngine.Random.InitState((int)Time.time);
        TimeSlider.value = CurrentTimeOfDay;
        SpeedSlider.value = Speed;
        
        // null checks
        lowCloudNotNull = LowCloud != null;
        highCloudNotNull = HighCloud != null;
        if (!lowCloudNotNull && !highCloudNotNull) UseClouds = false;
        
        if (UseStars) if (StarsMat == null || StarsObject == null) UseStars = false;
        if (!UseStars && StarsMat == null && StarsObject != null) StarsObject.SetActive(false);
        
        if (UseMoon) if (MoonMat == null) UseMoon = false;
        if (UseReflectionProbe) if (RefProbe == null) UseReflectionProbe = false;
        
        if (SkyObject == null) UseSky = false;
    }

    public void LocalUpdated()
    {
        local = LocalToggle.isOn;
        if (!local)
        {
            CurrentTimeOfDay = SetTime;
            Speed = SetSpeed;
        }
    }

    public void SliderUpdated()
    {
        if (!local)
        {
            //when disabled only master can control synced time, if enabled can cause syncing issues
            //Networking.SetOwner(Networking.LocalPlayer, this.gameObject);

            SetTime = TimeSlider.value;
            SetSpeed = SpeedSlider.value;
            syncid = GetID();
        }
        else
        {
            CurrentTimeOfDay = TimeSlider.value;
            Speed = SpeedSlider.value;
        }
    }

    private int GetID()
    {
        return UnityEngine.Random.Range(-2000000000, 2000000000);
    }

    void Update()
    {
        if (syncid != lastreceivedid)
        {
            lastreceivedid = syncid;
            if (!local)
            {
                CurrentTimeOfDay = SetTime;
                Speed = SetSpeed;
                TimeSlider.value = CurrentTimeOfDay;
                SpeedSlider.value = Speed;
            }
        }
        
        else if (TimeSlider.gameObject.activeInHierarchy && Time.time % 1 < 0.01f)
        {
            TimeSlider.value = CurrentTimeOfDay;
            SpeedSlider.value = Speed;
        }

        
        RenderSettings.ambientLight = ThreePoint(AmbientPoint1, AmbientPoint2, AmbientPoint3, 
            AmbientColor1, AmbientColor2, AmbientColor3);
        
        if (UseSun)
        {
            Sun.transform.localRotation = Quaternion.Euler((CurrentTimeOfDay * 360f) - 90, 140, 30);
            Sun.color = TwoPoint(SunPoint1, SunPoint2, SunColor1, SunColor2);
            float sunintensity = TwoPointFloat(SunIntensityPoint1, SunIntensityPoint2);
            Sun.intensity = (SunInitialIntensity * sunintensity) + 0.001f;
        }
        
        if (UseSky) SkyObject.transform.localRotation = Quaternion.Euler((CurrentTimeOfDay * 360f) - 90, 140, 30);
        
        if (UseClouds)
        {
            c = ThreePoint(CloudPoint1, CloudPoint2, CloudPoint3, 
                CloudColor1, CloudColor2, CloudColor3);
            if (lowCloudNotNull) LowCloud.SetColor("_CloudColor", c);
            if (highCloudNotNull) HighCloud.SetColor("_CloudColor", c);
        }

        /*
        c = ThreePoint(WaterPoint1, WaterPoint2, WaterPoint3, WaterFarColor1, WaterFarColor2, WaterFarColor3);
        Water.SetColor("_ColorFar", c);
        c = ThreePoint(WaterPoint1, WaterPoint2, WaterPoint3, WaterCloseColor1, WaterCloseColor2, WaterCloseColor3);
        Water.SetColor("_ColorClose", c);
        */

        #region Stars
        if (UseStars)
        {
            c = TwoPoint(StarPoint1, StarPoint2, StarColor1, StarColor2);
            StarsMat.SetColor("_EmissionColor", c);
            
            if (c.a <= StarCutoff)
            {
                if (StarsObject.activeInHierarchy)
                {
                    StarsObject.SetActive(false);
                }
            }
            else if (!StarsObject.activeInHierarchy)
            {
                StarsObject.SetActive(true);
            }
        }
        #endregion

        // Moon
        if(UseMoon) MoonMat.color = TwoPoint(MoonPoint1, MoonPoint2, MoonColor1, MoonColor2);

        /*
        if (CurrentTimeOfDay > FirePoint && CurrentTimeOfDay < 1 - FirePoint)
        {
            if (FireFX.activeSelf)
            {
                FireFX.SetActive(false);
            }
        }
        else
        {
            if (!FireFX.activeSelf)
            {
                FireFX.SetActive(true);
            }
        }
        */

        //float audiovolume = TwoPointFloat(AudioPoint1, AudioPoint2);

        //Birds.volume = BirdsInitialVolume * audiovolume;
        //Cicadas.volume = CicadasInitialVolume * Mathf.Clamp(1f - audiovolume, 0f, 1f);

        // Reflection Probe
        if (UseReflectionProbe) RefProbe.customBakedTexture = 
            CycleCubemap(SunPoint1, SunPoint2, NightCubemap, DawnCubemap, DayCubemap, DuskCubemap);

        
        #region Advance Time of Day
        CurrentTimeOfDay += (Time.deltaTime * Speed) * TimeMultiplier;

        // Ensures CurrentTimeOfDay stays between 0-1
        if (CurrentTimeOfDay >= 1)
        {
            CurrentTimeOfDay = 0;
        }
        #endregion
        
    }


    public float TwoPointFloat(float p1, float p2)
    {
        float p3 = 1 - p2;
        float p4 = 1 - p1;

        float ret = 1f;

        if (CurrentTimeOfDay < p1)
        {
            ret = 0f;
        }
        else if (CurrentTimeOfDay < p2)
        {
            ret = (CurrentTimeOfDay - p1) / (p2 - p1);
        }
        else if (CurrentTimeOfDay < p3)
        {
            ret = 1f;
        }
        else if (CurrentTimeOfDay < p4)
        {
            ret = 1 - ((CurrentTimeOfDay - p3) / (p4 - p3));
        }
        else
        {
            ret = 0f;
        }

        return ret;
    }

    public Color TwoPoint(float p1, float p2, Color c1, Color c2)
    {
        Color ret = new Color(0f, 0f, 0f);

        float p3 = 1 - p2;
        float p4 = 1 - p1;

        if (CurrentTimeOfDay < p1)
        {
            ret = c1;
        }
        else if (CurrentTimeOfDay < p2)
        {
            float v = (CurrentTimeOfDay - p1) / (p2 - p1);
            ret = Color.Lerp(c1, c2, v);
        }
        else if (CurrentTimeOfDay < p3)
        {
            ret = c2;
        }
        else if (CurrentTimeOfDay < p4)
        {
            float v = (CurrentTimeOfDay - p3) / (p4 - p3);
            ret = Color.Lerp(c2, c1, v);
        }
        else
        {
            ret = c1;
        }

        return ret;
    }

    public Color ThreePoint(float p1, float p2, float p3, Color c1, Color c2, Color c3)
    {
        Color ret = new Color(1f, 1f, 1f);

        float p4 = 1 - p3;
        float p5 = 1 - p2;
        float p6 = 1 - p1;

        if (CurrentTimeOfDay < p1)
        {
            ret = c1;
        }
        else if (CurrentTimeOfDay < p2)
        {
            float v = (CurrentTimeOfDay - p1) / (p2 - p1);
            ret = Color.Lerp(c1, c2, v);
        }
        else if (CurrentTimeOfDay < p3)
        {
            float v = (CurrentTimeOfDay - p2) / (p3 - p2);
            ret = Color.Lerp(c2, c3, v);
        }
        else if (CurrentTimeOfDay < p4)
        {
            ret = c3;
        }
        else if (CurrentTimeOfDay < p5)
        {
            float v = (CurrentTimeOfDay - p4) / (p5 - p4);
            ret = Color.Lerp(c3, c2, v);
        }
        else if (CurrentTimeOfDay < p6)
        {
            float v = (CurrentTimeOfDay - p5) / (p6 - p5);
            ret = Color.Lerp(c2, c1, v);
        }
        else
        {
            ret = c1;
        }

        return ret;
    }
    
    public Cubemap CycleCubemap(float p1, float p2, Cubemap night, Cubemap dawn, Cubemap day, Cubemap dusk)
    {
        Cubemap cubemap = night;

        float p3 = 1 - p2,
            p4 = 1 - p1;

        p1 -= 0.05f;
        p4 += 0.05f;

        if (CurrentTimeOfDay < p1) {
            //ret = night;
        }
        else if (p1 < CurrentTimeOfDay && CurrentTimeOfDay < p2)
        {
            cubemap = dawn;
        }
        else if (p2 < CurrentTimeOfDay && CurrentTimeOfDay < p3)
        {
            cubemap = day;
        }
        else if (CurrentTimeOfDay < p4)
        {
            cubemap = dusk;
        }

        
        return cubemap;
    }

#if !COMPILER_UDONSHARP && UNITY_EDITOR
    public void TestChangeGUI(object value)
    {
        var casted = ((SerializedProperty)value)?.floatValue;
        var actualVal = Convert.ToSingle(casted);
    }
#endif
}
