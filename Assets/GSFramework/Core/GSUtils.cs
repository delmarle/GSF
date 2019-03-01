public static class GsUtils
{
	public static long FloatToLong(float valueToConvert)
	{
		return (long)(valueToConvert * 1000);
	}

	public static float LongToFloat(long valueToConvert)
	{
		return (float)valueToConvert / 1000;
	}
}
