using UnityEngine;
using System.Collections.Generic;

public class EffectCaustics : MonoBehaviour
{
    public bool animationCaustics;
    public Texture firstTexture;
    public float speedInFPS = 60;

    [HideInInspector]
    public List<Texture> causticTex;
    private Projector projector;
    private float counter;

    private int nTex = 0;

	void Awake ()
    {
        if (animationCaustics)
        {
            projector = GetComponent<Projector>();
            counter = 0;
            for (int n = 1; n < 257; n++) causticTex.Add(Resources.Load("Caustics/_" + n.ToString("000")) as Texture);
        }
	}
	
	void Update ()
    {
        if (!animationCaustics) return;

        counter += Time.deltaTime * speedInFPS;

        if (counter >= 1)
        {
            nTex += (int)counter;
            if (nTex >= causticTex.Count) nTex = 0;
            projector.material.SetTexture("_ShadowTex", causticTex[nTex]);
            counter = 0;
            
        }
	}

    private void OnApplicationQuit()
    {
        GetComponent<Projector>().material.SetTexture("_ShadowTex", firstTexture);
    }
}
