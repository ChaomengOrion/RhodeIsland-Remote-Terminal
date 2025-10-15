using System;
using System.Collections.Generic;
using UnityEngine;
using NPinyin;

public class NameStruct : IComparable<NameStruct>
{
    public string strChinese = default;
    public List<char> chineseList = default;
    public List<string> pinyinList = default;

    public NameStruct() { }

    public NameStruct(string chinese, List<char> chList, List<string> pyList)
    {
        strChinese = chinese;
        chineseList = chList;
        pinyinList = pyList;
    }

    int IComparable<NameStruct>.CompareTo(NameStruct other)
    {
        int size1 = Mathf.Min(chineseList.Count, other.chineseList.Count);
        int size2 = Mathf.Min(pinyinList.Count, other.pinyinList.Count);

        for (int i = 0; i < size1; i++)
        {
            if (pinyinList[i].CompareTo(other.pinyinList[i]) == 0)
            {
                if (i == size1 - 1)
                {
                    for (int j = 0; j < size2; ++j)
                    {
                        if (chineseList[j].CompareTo(other.chineseList[j]) != 0)
                        {
                            return chineseList[j].CompareTo(other.chineseList[j]);
                        }
                    }
                }
            }
            else
            {
                return pinyinList[i].CompareTo(other.pinyinList[i]);
            }
        }

        if (chineseList.Count > other.chineseList.Count)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }
}

public class PinYinComparer : IComparer<string>
{
    int IComparer<string>.Compare(string x, string y)
    {
        string pinYinX = Pinyin.GetPinyin(x);
        string pinYinY = Pinyin.GetPinyin(y);
        return pinYinX.CompareTo(pinYinY);
    }
}