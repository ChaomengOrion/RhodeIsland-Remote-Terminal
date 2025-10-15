// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:51

namespace RhodeIsland.Arknights
{
	public interface ISharedItemModel
	{
		ItemType GetItemType();

		string GetItemId();

		int GetItemCount();

		void SetItemCount(int count);
	}
}
