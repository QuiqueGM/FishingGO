using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VFG.Core;
using UnityStandardAssets.ImageEffects;

namespace VFG.Utils
{
    public class SetCameraEffects : MonoBehaviour
    {
        private BloomOptimized _bloom;
        private HBAO _oclusion;
        private HBAO_Core.AOSettings _aoSettings = new HBAO_Core.AOSettings();

        public void SetEffects()
        {
            _bloom = GetComponent<BloomOptimized>();
            _oclusion = GetComponent<HBAO>();

            if (GameState.AdvancedLighting == (int)GameState.Toggle.Off && GameState.AdvancedShadows == (int)GameState.Toggle.Off)
            {
                _bloom.enabled = false;
                _oclusion.enabled = false;
            }
            else if (GameState.AdvancedLighting == (int)GameState.Toggle.On && GameState.AdvancedShadows == (int)GameState.Toggle.Off)
            {
                _oclusion.enabled = false;
                SetBloomParameters();
            }
            else if (GameState.AdvancedLighting == (int)GameState.Toggle.Off && GameState.AdvancedShadows == (int)GameState.Toggle.On)
            {
                _bloom.enabled = false;
                SetOclusionParameters();
            }
            else
            {
                SetBloomAndOclusionParameters();
            }
        }

        private void SetBloomParameters()
        {
            _bloom.enabled = true;
            _bloom.threshold = 0.3f;
            _bloom.intensity = 0.4f;
            _bloom.blurSize = 0.5f;
            _bloom.blurIterations = 1;
        }

        private void SetOclusionParameters()
        {
			_oclusion.enabled = true;
			_oclusion.ApplyPreset (HBAO_Core.Preset.WithoutLight);
        }

        private void SetBloomAndOclusionParameters()
        {
            _bloom.enabled = true;
            _bloom.threshold = 0.3f;
            _bloom.intensity = 1f;
            _bloom.blurSize = 0.75f;
            _bloom.blurIterations = 3;
            
            _oclusion.enabled = true;
			_oclusion.ApplyPreset (HBAO_Core.Preset.FastestPerformance);
        }
    }
}