﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Timespinner.GameAbstractions.Gameplay;
using TsRandomizer.IntermediateObjects;

namespace TsRandomizer.Extensions
{
	public static class ScriptActionQueueExtensions
	{
		public static readonly Vector4 ReplacedArguments = new Vector4(1, 3, 3, 7);

		internal static void UpdateRelicOrbGetToastToItem(this Queue<ScriptAction> scripts, Level level, ItemInfo itemInfo)
		{
			var giveOrbScript = scripts.Single(s => s.AsDynamic().ScriptType == EScriptType.RelicOrbGetToast);
			var reflectedScript = giveOrbScript.AsDynamic();

			switch (itemInfo.LootType)
			{
				case LootType.ConstUseItem:
					reflectedScript.ScriptType = EScriptType.GiveItem;
					break;

				case LootType.ConstStat:
					reflectedScript.ScriptType = EScriptType.Delegate;
					reflectedScript.Delegate = (Action)(() => level.RequestToastPopupForStats(itemInfo));
					break;
			}

			reflectedScript.ItemToGiveType = itemInfo.LootType.ToEInventoryCategoryType();
			reflectedScript.ItemToGive = itemInfo.ItemId;
			reflectedScript.OrbSlot = itemInfo.OrbSlot;
			reflectedScript.Arguments = ReplacedArguments;
		}
	}
}