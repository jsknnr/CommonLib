using HarmonyLib;
using System.Linq;
using System.Reflection;
using Vintagestory.API.Client;
using Vintagestory.API.Common;

namespace CommonLib.Templates
{
    public abstract class HarmonySystem : ModSystem
    {
        protected readonly string PatchCode;
        protected Harmony HarmonyInstance = null!;
        protected ICoreAPI Api = null!;

        public HarmonySystem()
        {
            PatchCode = GetType().AssemblyQualifiedName!;
        }

        public HarmonySystem(string patchCode)
        {
            PatchCode = patchCode;
        }

        public override void StartPre(ICoreAPI api)
        {
            Api = api;
            if (api is ICoreClientAPI capi)
            {
                // Prevent double patches in singleplayer
                if (!capi.IsSinglePlayer)
                {
                    PatchAll(GetType().Assembly);
                }
            }
            else
            {
                PatchAll(GetType().Assembly);
            }
        }

        protected virtual void PatchAll(Assembly assembly)
        {
            HarmonyInstance = new Harmony(PatchCode);
            HarmonyInstance.PatchAll(assembly);
            var patchedMethods = HarmonyInstance.GetPatchedMethods();
            Mod.Logger.Notification($"Harmony patched:\n{string.Join("\n", patchedMethods.Select(x => x.FullDescription()))}");
        }

        public override void Dispose()
        {
            HarmonyInstance?.UnpatchAll(PatchCode);
        }
    }
}
