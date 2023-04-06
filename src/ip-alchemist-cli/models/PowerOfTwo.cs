namespace ip_alchemist_cli.models
{
    public struct PowerOfTwo
    {
        public PowerOfTwo(int index, int power)
        {
            Index = index;
            Power = power;
        }

        public int Index { get; set; }
        public int Power { get; set; }
    }
}