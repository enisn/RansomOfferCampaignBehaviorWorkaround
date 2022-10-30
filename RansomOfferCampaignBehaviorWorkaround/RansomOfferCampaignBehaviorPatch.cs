using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.BarterSystem.Barterables;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.Core;
using TaleWorlds.Library;
using System.Reflection;
using Helpers;
using TaleWorlds.CampaignSystem.MapNotificationTypes;

namespace RansomOfferCampaignBehaviorWorkaround
{

    [HarmonyPatch(typeof(RansomOfferCampaignBehavior))]
    internal static class RansomOfferCampaignBehaviorPatch
    {
        [HarmonyPatch("ConsiderRansomPrisoner")]
        [HarmonyPrefix]
        private static bool ConsiderRansomPrisoner(Hero hero, List<Hero> ____heroesWithDeclinedRansomOffers)
        {
            try
            {
                Clan captorClanOfPrisoner = GetCaptorClanOfPrisoner_Original(hero);
                if (captorClanOfPrisoner == null)
                {
                    return false;
                }

                Hero hero2 = ((hero.Clan.Leader != hero) ? hero.Clan.Leader : hero.Clan.Lords.Where((Hero t) => t != hero.Clan.Leader).GetRandomElementInefficiently());
                if (hero2 == Hero.MainHero && hero2.IsPrisoner)
                {
                    return false;
                }

                if (captorClanOfPrisoner == Clan.PlayerClan || hero.Clan == Clan.PlayerClan)
                {
                    if (____heroesWithDeclinedRansomOffers != null)
                    {
                        return false;
                    }

                    float num = ((!____heroesWithDeclinedRansomOffers.Contains(hero)) ? 0.2f : 0.12f);
                    if (MBRandom.RandomFloat < num)
                    {
                        float num2 = (float)new SetPrisonerFreeBarterable(hero, captorClanOfPrisoner.Leader, hero.PartyBelongedToAsPrisoner, hero2).GetUnitValueForFaction(hero.Clan) * 1.1f;
                        _ = MBRandom.RandomFloat < num && (float)(hero2.Gold + 1000) >= num2;
                        //if (MBRandom.RandomFloat < num && (float)(hero2.Gold + 1000) >= num2)
                        {
                            //SetCurrentRansomHero(hero, hero2);
                            //StringHelpers.SetCharacterProperties("CAPTIVE_HERO", hero.CharacterObject, RansomOfferDescriptionText);
                            //Campaign.Current.CampaignInformationManager.NewMapNoticeAdded(new RansomOfferMapNotification(hero, RansomOfferDescriptionText));
                            InformationManager.DisplayMessage(new InformationMessage("RansomOfferCampaignBehavior.ConsiderRansomPrisoner Execution Passed to original method.", Colors.Green));

                            return true;
                        }
                    }
                }
                else if (MBRandom.RandomFloat < 0.1f)
                {
                    SetPrisonerFreeBarterable setPrisonerFreeBarterable = new SetPrisonerFreeBarterable(hero, captorClanOfPrisoner.Leader, hero.PartyBelongedToAsPrisoner, hero2);
                    if (setPrisonerFreeBarterable.GetValueForFaction(captorClanOfPrisoner) + setPrisonerFreeBarterable.GetValueForFaction(hero.Clan) > 0)
                    {
                        Campaign.Current.BarterManager.ExecuteAiBarter(captorClanOfPrisoner, hero.Clan, captorClanOfPrisoner.Leader, hero2, setPrisonerFreeBarterable);
                    }
                }
                
                return false;
            }
            catch (NullReferenceException ex)
            {
                return false;
            }
        }

        private static Clan GetCaptorClanOfPrisoner_Original(Hero hero)
        {
            Clan clan = null;
            if (hero.PartyBelongedToAsPrisoner.IsMobile)
            {
                if ((hero.PartyBelongedToAsPrisoner.MobileParty.IsMilitia || hero.PartyBelongedToAsPrisoner.MobileParty.IsGarrison || hero.PartyBelongedToAsPrisoner.MobileParty.IsCaravan || hero.PartyBelongedToAsPrisoner.MobileParty.IsVillager) && hero.PartyBelongedToAsPrisoner.Owner != null)
                {
                    if (hero.PartyBelongedToAsPrisoner.Owner.IsNotable)
                    {
                        return hero.PartyBelongedToAsPrisoner.Owner.CurrentSettlement.OwnerClan;
                    }

                    return hero.PartyBelongedToAsPrisoner.Owner.Clan;
                }

                return hero.PartyBelongedToAsPrisoner.MobileParty.ActualClan;
            }

            return hero.PartyBelongedToAsPrisoner.Settlement.OwnerClan;
        }
    }
}
