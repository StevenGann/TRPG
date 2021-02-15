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
        public float Damage = 1.0f;
        public float Defense = 1.0f;
        public float Accuracy = 1.0f;
        public float Health = 1.0f;
        public float Uses = 1.0f;
        public float Experience = 1.0f;

        public Adjective()
        { }

        public Adjective(string _text, float _str, float _dex, float _con,
            float _int, float _wis, float _cha, float _weight, float _val,
            float _dam, float _def, float _acc, float _health, float _uses,
            float _xp)
        {
            Text = _text;
            Strength = _str;
            Dexterity = _dex;
            Constitution = _con;
            Intelligence = _int;
            Wisdom = _wis;
            Charisma = _cha;
            Weight = _weight;
            Value = _val;
            Damage = _dam;
            Defense = _def;
            Accuracy = _acc;
            Health = _health;
            Uses = _uses;
            Experience = _xp;
        }

        public static Adjective operator +(Adjective a, Adjective b)
        {
            Adjective result = new Adjective
            {
                Text = a.Text,

                Strength = a.Strength * b.Strength,
                Dexterity = a.Dexterity * b.Dexterity,
                Constitution = a.Constitution * b.Constitution,
                Intelligence = a.Intelligence * b.Intelligence,
                Wisdom = a.Wisdom * b.Wisdom,
                Charisma = a.Charisma * b.Charisma,
                Weight = a.Weight * b.Weight,
                Value = a.Value * b.Value,
                Damage = a.Damage * b.Damage,
                Defense = a.Defense * b.Defense,
                Accuracy = a.Accuracy * b.Accuracy,
                Health = a.Health * b.Health,
                Uses = a.Uses * b.Uses,
                Experience = a.Experience * b.Experience
            };

            return result;
        }
    }
}