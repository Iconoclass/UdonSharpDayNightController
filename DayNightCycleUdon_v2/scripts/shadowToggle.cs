using UdonSharp;
using UnityEngine;

public class shadowToggle : UdonSharpBehaviour
{
    public Light light;
    
    public void ShadowToggle()
    {
        if(light.shadows == LightShadows.None)
        {
            light.shadows = LightShadows.Soft;
        }
        else
        {
            light.shadows = LightShadows.None;
        }
    }
}
