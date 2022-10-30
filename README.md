# RansomOfferCampaignBehaviorWorkaround
Catches exceptions on RansomOfferCampaignBehavior.ConsiderRansomPrisoner method execution.

This IS NOT the solution for the exception. This mod handles exceptions and ignores them instead of game crashes.

**Compatible Version:** 1.0.0 (Stable)

## Problem
If you're using multiple mods at the same time and some of them can break your game data with Unsynchronized datas. For example, captivity events use heroes that isn't included in native game. When you capture them as prisoners, this method is invoked and a ransom offer tries come to you. But thoese heroes doesn't have any clan or family. In this situation you'll get following exception:
```
at TaleWorlds.CampaignSystem.CampaignBehaviors.RansomOfferCampaignBehavior.ConsiderRansomPrisoner(Hero hero)
at TaleWorlds.CampaignSystem.CampaignBehaviors.RansomOfferCampaignBehavior.DailyTickHero(Hero hero)
at TaleWorlds.CampaignSystem.MbEvent`1.InvokeList(EventHandlerRec`1 list, T t)
at TaleWorlds.CampaignSystem.CampaignEvents.DailyTickHero(Hero hero)
at TaleWorlds.CampaignSystem.CampaignEventDispatcher.DailyTickHero(Hero hero)
at TaleWorlds.CampaignSystem.CampaignPeriodicEventManager.PeriodicTicker`1.PeriodicTickSome(Double timeUnitsElapsed)
at TaleWorlds.CampaignSystem.CampaignPeriodicEventManager.PeriodicDailyTick()
at TaleWorlds.CampaignSystem.CampaignPeriodicEventManager.TickPeriodicEvents()
at TaleWorlds.CampaignSystem.Campaign.Tick()
at TaleWorlds.CampaignSystem.GameState.MapState.OnMapModeTick(Single dt)
at TaleWorlds.CampaignSystem.GameState.MapState.OnTick(Single dt)
at TaleWorlds.Core.GameStateManager.OnTick(Single dt)
at TaleWorlds.Core.Game.OnTick(Single dt)
at TaleWorlds.Core.GameManagerBase.OnTick(Single dt)
at TaleWorlds.MountAndBlade.Module.OnApplicationTick_Patch1(Module this, Single dt)
```

## Installation
1. Install the mod.
2. Make sure it's loaded after Harmony and Native modules.

## Nexus Link
https://www.nexusmods.com/mountandblade2bannerlord/mods/4612/
