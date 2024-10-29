using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace ArachnophobiaMod
{
    [BepInPlugin("com.Rastilan.arachnophobiamod", "Arachnophobia Mod", "1.0.4")]
    public class ArachnophobiaPlugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Harmony harmony = new Harmony("com.Rastilan.arachnophobiamod");
            harmony.PatchAll();
            Debug.Log("Arachnophobia Mod Initialized");
        }

        [HarmonyPatch(typeof(SpawnManager), "Spawn")]
        class Spawn_Patch
        {
            static void Prefix(ref Spawnable.Type type, Vector3 pos, Quaternion rot)
            {
                Debug.Log($"Attempting to spawn: {type}");

                // Check if the spawnable type is a spider or ranged spider
                if (type == Spawnable.Type.Spider || type == Spawnable.Type.RangedSpider || type == Spawnable.Type.Boar)
                {
                    // Log and prevent the original spawn
                    Debug.Log($"Preventing original spawn of {type}.");
                    
                    // Spawn a capsule at the desired position and rotation
                    SpawnCapsule(pos, rot);
                    
                    // Prevent the original spawn by skipping further execution
                    return; // Exit the method early to avoid modifying 'type'
                }
            }

            private static void SpawnCapsule(Vector3 position, Quaternion rotation)
            {
                // Create a new GameObject
                GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                capsule.transform.position = position;
                capsule.transform.rotation = rotation;

                // Optional: Set the capsule's scale to fit your needs
                capsule.transform.localScale = new Vector3(1, 1, 1); // Adjust scale if needed
                
                // Log for debugging
                Debug.Log($"Capsule spawned at {position} with rotation {rotation}.");
            }
        }
    }
}
