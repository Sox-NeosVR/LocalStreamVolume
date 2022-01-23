using HarmonyLib;
using NeosModLoader;
using FrooxEngine;

namespace LocalStreamVolume
{
    public class LocalStreamVolume : NeosMod
    {
        public override string Name => "LocalStreamVolume";
        public override string Author => "Sox";
        public override string Version => "1.0.0";
        public override string Link => "";
        public override void OnEngineInit()
        {
            Harmony harmony = new Harmony("net.Sox.LocalStreamVolume");
            harmony.PatchAll();
        }
		
        [HarmonyPatch(typeof(AudioStreamController), "BuildUI")]
        class LocalStreamVolumePatch
        {
            static void Postfix(AudioStreamController __instance, SyncRef<AudioOutput> ____audioOutput)
            {
             ValueUserOverride<float> valueoverride =__instance.Slot.AttachComponent<ValueUserOverride<float>>();
                valueoverride.CreateOverrideOnWrite.Value = true;
                valueoverride.Target.Target = ____audioOutput.Target.Volume;
                valueoverride.Default.Value = 1f;
            }
        }
    }
}