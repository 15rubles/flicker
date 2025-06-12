namespace Controller
{
    public static class StateController
    {
        private static bool isMulliganStep = false;

        public static bool IsMulliganStep
        {
            get => isMulliganStep;
            set => isMulliganStep = value;
        }
    }
}