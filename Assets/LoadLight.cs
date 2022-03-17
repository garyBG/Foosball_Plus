using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLight : MonoBehaviour {
    public PointLight ALight;
    
	void OnPreRender() {
        ALight = GameObject.Find("APointLight").GetComponent<PointLight>();
        ALight.LoadLightToShader();
    }

}
