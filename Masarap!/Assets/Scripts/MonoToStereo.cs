using UnityEngine;
using UnityEditor;

public class MonoToStereo : AssetPostprocessor {
    void OnPreprocessAudio() {
        AudioImporter audioImporter = assetImporter as AudioImporter;

        if (assetPath.Contains("mono")) {
            audioImporter = assetImporter as AudioImporter;
            audioImporter.forceToMono = true;
        }
        
        if (assetPath.Contains("stereo")) {
            audioImporter.forceToMono = false;
        }
    }
}