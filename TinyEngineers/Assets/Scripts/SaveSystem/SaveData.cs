using System;

namespace TinyEngineers.SaveSystem
{
    [Serializable]
    public class SaveData
    {
        public int PlayerLevel = 1;
        public int XP = 0;
        public int Coins = 0;
        public int Stars = 0;
        public int BestPizzaScore = 0;
        public bool IsFirstTime = true;
    }
}
