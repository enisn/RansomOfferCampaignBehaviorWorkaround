using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace RansomOfferCampaignBehaviorWorkaround
{
    public class RansomOfferCampaignBehaviorWorkaroundSubModule : MBSubModuleBase
    {
        private Harmony _harmony;
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            this._harmony = new Harmony("com.enisn.bannerlord.ransomoffercampaignbehaviorworkaround");
        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            this._harmony.PatchAll();
            InformationManager.DisplayMessage(new InformationMessage("RansomOfferCampaignBehavior Workaround patched", Colors.Blue));
        }
    }
}
