using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj : MonoBehaviour {

    public float rate;
    public float interval;

    protected MeshRenderer m_renderer;
    protected Color defaultColor;
    protected float timer;

	// Use this for initialization
    protected void Start () {
        m_renderer = GetComponent<MeshRenderer>();
        m_renderer.material.EnableKeyword("_EmissionColor");
        defaultColor = m_renderer.material.GetColor("_EmissionColor");
        m_renderer.material.SetColor("_EmissionColor", Color.black);
        timer = 0;
	}
	
	// Update is called once per frame
	protected void Update () {
        if(timer >= interval) m_renderer.material.SetColor("_EmissionColor", Color.Lerp(Color.black, defaultColor, (timer-interval) / rate));
        timer += Time.deltaTime;
	}

    virtual public void Clear() { }
}
