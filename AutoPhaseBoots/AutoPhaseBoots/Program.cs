namespace AutoPhaseBoots
{
    using Ensage;

    class Program
    {
        #region Methods

        public static void Main()
        {
            Game.OnUpdate += AutoPhaseBoots.GameOnUpdate;
        }

        #endregion
    }
}
