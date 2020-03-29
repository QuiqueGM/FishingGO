using UnityEngine;
using UnityStandardAssets.ImageEffects;
using Ceto;

namespace VFG.Utils
{
	public class CameraEffectsController : MonoBehaviour 
	{
        [Header("Bloom Effect")]
        public BloomOptimized bloom;
        public float bloomFadeStart = 3;
        public float finalBloomValue = 0;

        [Header("Ocean waves")]
        public GameObject ocean;
        public float waveFadeStart = 3;
        public float finalWindEffect = 11.8f;
        public float finalWaveEffect = 2.07f;

        private float initBloomPos;
        private float currentIntensity;

        private Ocean o;
        private WaveSpectrum w;

        private float initWavePos;
        private float currentWind;
        private float currentWave;


        private void Awake()
        {
            o = ocean.GetComponent<Ocean>();
            w = ocean.GetComponent<WaveSpectrum>();

            initBloomPos = o.level - bloomFadeStart;
            currentIntensity = bloom.intensity;

            initWavePos = o.level - waveFadeStart;
            currentWind = w.windSpeed;
            currentWave = w.waveSpeed;
        }

        void Update ()
		{
            if (Input.GetKey(KeyCode.UpArrow))
                gameObject.transform.parent.transform.position += new Vector3(0, 0.01f, 0);
            else if (Input.GetKey(KeyCode.DownArrow))
                gameObject.transform.parent.transform.position -= new Vector3(0, 0.01f, 0);

            if (transform.position.y > initBloomPos)
            {
                bloom.intensity = GetValueFromIntervals(currentIntensity, finalBloomValue, bloomFadeStart, o.level, transform.position.y);
                if (bloom.intensity <= finalBloomValue) bloom.intensity = finalBloomValue;
                if (bloom.intensity >= currentIntensity) bloom.intensity = currentIntensity;
            }

            if (transform.position.y > initWavePos)
            {
                w.windSpeed = GetValueFromIntervals(currentWind, finalWindEffect, waveFadeStart, o.level, transform.position.y);
                if (w.windSpeed <= finalWindEffect) w.windSpeed = finalWindEffect;
                if (w.windSpeed >= currentWind) w.windSpeed = currentWind;

                w.waveSpeed = GetValueFromIntervals(currentWave, finalWaveEffect, waveFadeStart, o.level, transform.position.y);
                if (w.waveSpeed >= finalWaveEffect) w.waveSpeed = finalWaveEffect;
                if (w.waveSpeed <= currentWave) w.waveSpeed = currentWave;
            }
        }

        private float GetValueFromIntervals(float initValueEffect, float finalValueEffect, float initPosition, float finalPosition, float currentPosition)
        {
            return initValueEffect + (finalValueEffect-initValueEffect) * (currentPosition - initPosition) / (finalPosition - initPosition);
        }
	}
}
