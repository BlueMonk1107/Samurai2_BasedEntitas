using System;
using Const;
using UnityEngine;

namespace Data
{
    public class DataManager     
    {
        public static DifficultLevel DifficultLevel
        {
            set { PlayerPrefs.SetString(ConstValue.DIFFICULT_LEVEL,value.ToString()); }
            get
            {
                string value = PlayerPrefs.GetString(ConstValue.DIFFICULT_LEVEL, DifficultLevel.NONE.ToString());
                DifficultLevel level;
                if (!Enum.TryParse(value, out level))
                {
                    Debug.LogError("parse DifficultLevel type faile");
                    return DifficultLevel.NONE;
                }
                else
                {
                    return level;
                }  
            }
        }

        public static int LevelIndex
        {
            set { PlayerPrefs.SetInt(ConstValue.LEVEL_INDEX,value);}
            get { return PlayerPrefs.GetInt(ConstValue.LEVEL_INDEX, 1); }
        }

        public static int LevelPartIndex
        {
            set { PlayerPrefs.SetInt(ConstValue.LEVEL_PART_INDEX, value); }
            get { return PlayerPrefs.GetInt(ConstValue.LEVEL_PART_INDEX, 1); }
        }

        public static bool JudgeExistData()
        {
            return DifficultLevel != DifficultLevel.NONE;
        }
    }
}
