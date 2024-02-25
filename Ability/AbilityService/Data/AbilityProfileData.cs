namespace Game.Code.Services.AbilityLoadout.Data
{
	using System;
	using System.Collections.Generic;

	[Serializable]
	public class AbilityProfileData
	{
		public List<int> inventory = new List<int>();
        
		public List<AbilitySlotData> abilities = new List<AbilitySlotData>();
	}
}