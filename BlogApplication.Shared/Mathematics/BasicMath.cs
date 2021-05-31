namespace BlogApplication.Shared.Mathematics
{
    public static class BasicMath
    {
        public static int Clamp(int value, int min, int max)
        {
            if (value < min)
                value = min;
            else if (value > max) value = max;

            return value;
        }

        public static int ClampMax(int value, int max)
        {
            if (value > max) value = max;

            return value;
        }

        public static int ClampMin(int value, int min)
        {
            if (value < min) value = min;

            return value;
        }
    }
}