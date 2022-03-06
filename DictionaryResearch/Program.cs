using System.Diagnostics;

namespace DictionaryResearch;

internal static class Program
{
	internal const int DictionaryLength = 500000;
	internal const int BucketLength = 672827;

	internal static void Main()
	{
		Helper.CheckBuckets(); // для 50к: 75431

		var defaultFilling = new Action(() =>
		{
			var count = Enumerable.Range(0, DictionaryLength).ToDictionary(i => i, i => "Value").Count;
		});

		// TODO Сделать Action, меделнно заполняющий Dictionary;

		var slowFilling = new Action(() =>
		{
			var count = Enumerable.Range(0, DictionaryLength).ToDictionary(i => BucketLength * i, i => "Value").Count;
		});

		Console.WriteLine(Helper.MeasureTime(defaultFilling));
		Console.WriteLine(Helper.MeasureTime(slowFilling));

		// Какое время на вашем ПК для обоих Action
		// Для defaultFilling = 34
		// Для slowFilling = 2667 примерно в 78 раз медленее
	}
}

internal static class Helper
{
	internal static void CheckBuckets()
	{
		var dictionary = Enumerable.Range(0, Program.DictionaryLength).ToDictionary(i => i, i => "Value");
		var count = dictionary.Count;
		Console.WriteLine(); // debug button
	}

	internal static long MeasureTime(Action action)
	{
		var time = new Stopwatch();
		GC.Collect();
		GC.WaitForPendingFinalizers();
		time.Restart();
		action.Invoke();
		time.Stop();
		return time.ElapsedMilliseconds;
	}
}