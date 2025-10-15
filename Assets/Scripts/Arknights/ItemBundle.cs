// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:51

using System;
using Newtonsoft.Json;

namespace RhodeIsland.Arknights
{
	[Serializable]
	public class ItemBundle : ISharedItemModel
	{
		[JsonConstructor]
		public ItemBundle(string itemId, ItemType itemType, int count)
		{
		}

		private void _Init(string itemId_, ItemType itemType_, int count_)
		{
		}

		public override int GetHashCode()
		{
			return default(int);
		}

		public bool IsSameItem(ItemBundle other)
		{
			return default(bool);
		}

		public override bool Equals(object obj)
		{
			return default(bool);
		}

		[JsonIgnore]
		public bool isEmpty
		{
			get
			{
				return default(bool);
			}
		}

		public ItemType GetItemType()
		{
			return ItemType.NONE;
		}

		public string GetItemId()
		{
			return null;
		}

		public int GetItemCount()
		{
			return default(int);
		}

		public void SetItemCount(int count_)
		{
		}

		public string id;

		public int count;

		public ItemType type;
	}
}
