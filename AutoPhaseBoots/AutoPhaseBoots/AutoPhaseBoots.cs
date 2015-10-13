namespace AutoPhaseBoots
{
    using System;
    using System.Linq;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;

    internal static class AutoPhaseBoots
    {
        /// <summary>
        /// Maximum buff overlap allowed if phase boots are off cool down before buff ends.
        /// </summary>
        private const float MaximumBuffOverlap = 0.05f;

        /// <summary>
        /// Constant added to latency for sleep to prevent multiple casting of the phase boots.
        /// </summary>
        private const int SleepConstant = 50;

        /// <summary>
        /// The constant of the move network activity. Will be obsolete once the NetworkActivity enumeration is fixed.
        /// </summary>
        private const NetworkActivity MoveActivity = (NetworkActivity)1502;

        #region Public Methods and Operators

        public static void GameOnUpdate(EventArgs args)
        {
            if (!Utils.SleepCheck("phaseSleep"))
            {
                return;
            }

            var localHero = ObjectMgr.LocalHero;
            if (localHero == null)
            {
                return;
            }

            var phase = localHero.FindItem("item_phase_boots");
            if (phase == null || !phase.CanBeCasted())
            {
                return;
            }

            var phaseBuff = localHero.Modifiers.FirstOrDefault(modifier => modifier.Name == "modifier_item_phase_boots_active");
            if (phaseBuff != null && phaseBuff.RemainingTime > (Game.AvgPing + MaximumBuffOverlap))
            {
                return;
            }

            if (localHero.NetworkActivity != MoveActivity)
            {
                return;
            }

            phase.UseAbility();
            Utils.Sleep(Game.AvgPing + SleepConstant, "phaseSleep");
        }

        #endregion
    }
}
