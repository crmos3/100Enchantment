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

    class MyEnchantDataList : SoEnchantDataList
    {
        public MyEnchantDataList(SoEnchantDataList from)
        {
            this.emptySource = from.emptySource;
            this.nonInheritables = from.nonInheritables;

            LoadSum(from);
            Load(from);
            Add();
        }

        public void LoadSum(SoEnchantDataList from)
        {
            float[] probs = AccessTools.FieldRefAccess<SoEnchantDataList, float[]>(from, "rarityChestProbSums");
            ref float[] mprobs = ref AccessTools.FieldRefAccess<SoEnchantDataList, float[]>(this, "rarityChestProbSums");
            mprobs = probs;
        }

        public void Load(SoEnchantDataList from)
        {
            for(int i = 1; i <= 12; i++) {
                float[] probs = AccessTools.FieldRefAccess<SoEnchantDataList, float[]>(
                    from, 
                    string.Format("rarity{0}ChestProbs", i.ToString())
                );
                ref float[] mprobs = ref AccessTools.FieldRefAccess<SoEnchantDataList, float[]>(
                    this,
                    string.Format("rarity{0}ChestProbs", i.ToString())
                );
                mprobs = probs;
            }
        }

        public void Add()
        {
            var temp = this.all.ToList();
            this.all = temp.ToArray();
        }
    }

    class MyEnchantment
    {
    }
}
