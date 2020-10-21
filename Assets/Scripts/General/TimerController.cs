using System.Diagnostics;

namespace Assets.Scripts.General
{
	internal static class TimerController
	{
		private static Stopwatch timer = new Stopwatch();

		internal static void StartTimer() => timer.Start();

		internal static void StopTimer() => timer.Stop();

		internal static void ResetTimer() => timer.Reset();

		internal static long GetTime() => timer.ElapsedMilliseconds;
	}
}
