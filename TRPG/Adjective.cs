using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPG
{
    public class Adjective
    {
        public string Text = "";
        public float Strength = 1.0f;
        public float Dexterity = 1.0f;
        public float Constitution = 1.0f;
        public float Intelligence = 1.0f;
        public float Wisdom = 1.0f;
        public float Charisma = 1.0f;
        public float Weight = 1.0f;
        public float Value = 1.0f;
        public float Defense = 1.0f;
        public float Accuracy = 1.0f;
        public float Health = 1.0f;
        public float Uses = 1.0f;
        public float Experience = 1.0f;

        public Adjective()
        { }

        public Adjective(string _text, float _str, float _dex, float _con,
            float _int, float _wis, float _cha, float _weight, float _val,
            float _def, float _acc, float _health, float _uses, float _xp)
        {
            Text = _text;
            Strength = _str;
            Dexterity = _dex;
            Constitution = _con;
            Intelligence = _int;
            Wisdom = _wis;

        }
    }
}
