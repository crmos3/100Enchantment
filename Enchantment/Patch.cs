using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Oc;
using Oc.Item;

namespace Enchantment
{

    [HarmonyPatch(typeof(OcItemDataMng))]
    [HarmonyPatch("CraftItem")]
    class Patch
    {
        static bool Prefix(OcItemDataMng __instance, int itemId, int level, List<OcItem> materialList, List<int> materialCount, ref OcItem __result)
        {
            if (materialList.IsNullOrEmpty<OcItem>())
            {
                __result = __instance.CreateItem(itemId, level);
                return false;
            }

            List<int> list = new List<int>();
            SoEnchantment[] nonInheritables = OcResidentData.EnchantDataList.nonInheritables;

            for (int i = 0; i < materialList.Count; i++)
            {
                OcItem ocItem = materialList[i];
                for (int j = 0; j < ocItem.EnchantNum; j++)
                {
                    list.Add(ocItem.GetEnchant(j).Source.ID);
                }
            }

            int[] selectEnchantment = new int[4];
            for(int i = 0; i < Math.Min(list.Count, 4); i++)
            {
                var randomIndex = UnityEngine.Random.Range(0, list.Count - 1);
                selectEnchantment[i] = list[randomIndex];
                list.RemoveAt(randomIndex);
            }
            selectEnchantment = (from a in selectEnchantment
                                 orderby Guid.NewGuid()
                                 select a).ToArray<int>();
            __result = __instance.CreateItem(itemId, level, selectEnchantment);
            return false;
        }

    }
}
