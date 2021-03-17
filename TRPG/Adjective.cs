namespace TRPG
{
    public class Adjective
    {
        public string Text = "";
        public double Strength = 1.0f;
        public double Dexterity = 1.0f;
        public double Constitution = 1.0f;
        public double Intelligence = 1.0f;
        public double Wisdom = 1.0f;
        public double Charisma = 1.0f;
        public double Weight = 1.0f;
        public double Value = 1.0f;
        public double Damage = 1.0f;
        public double Defense = 1.0f;
        public double Accuracy = 1.0f;
        public double Health = 1.0f;
        public double Uses = 1.0f;
        public double Experience = 1.0f;

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