using BepInEx;
using HarmonyLib;

namespace Enchantment
{
    [BepInPlugin("com.example.EnchantPlugin", "EnchantmentPlugin", "1.0.0.0")]
    public class Patcher
    {
        void Awake()
        {
            var harmony = new Harmony("com.example.patch");
            harmony.PatchAll();
        }
    }
}
