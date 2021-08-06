using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace RunPrograms
{
	/*
	 * 15 var
	 * Find and print foreign sentences
	 */

	class CourseWork
	{
		public const int AmountOfFiles = 20;

		public static async Task Start()
		{
			var watchSync = new Stopwatch();
			Console.OutputEncoding = Encoding.Unicode;
			var fileNames = new List<string>();

			for (int i = 1; i <= AmountOfFiles; i++)
			{
				fileNames.Add($"File{i}.txt");
			}

			watchSync.Start();

			// SyncRun(fileNames);
			await ParallelRun(fileNames);


			watchSync.Stop();
			Console.WriteLine($"Total time: {watchSync.Elapsed}");
			// Console.ReadKey();
		}

		private static bool IsHasForeignWords(string str)
		{
			// const string pattern = "(?i).*(?![a-z])\pM*\pL.*";
			const string pattern = @"[^\x00-\x80]+";
			var rgx = new Regex(pattern);

			return rgx.IsMatch(str);
		}

		private static async Task<bool> ParallelRun(IEnumerable<string> fileNames)
		{
			var lines = ParallelFileProcesses(fileNames).Result;
			foreach (var sentences in lines)
			{
				sentences.ForEach(line => Console.WriteLine($"Has foreign words: {line}"));
				Console.WriteLine("////////////////////////////////////////////////////////////////");
			}

			return true;
		}

		private static bool SyncRun(IEnumerable<string> fileNames)
		{
			foreach (var name in fileNames)
			{
				var sentences = GetSentences("e:/" + name);
				sentences.ForEach(l => Console.WriteLine($"Has foreign words: {l}"));
			}

			Console.WriteLine("////////////////////////////////////////////////////////////////");

			return true;
		}

		private static async Task<List<List<string>>> ParallelFileProcesses(IEnumerable<string> fileNames)
		{
			var results = new List<List<string>>();
			// var tasks = new List<Task>();

			// foreach (var name in fileNames)
			//    tasks.Add(Task.Run(() => results.Add(GetSentences("e:/" + name))));
			foreach (var name in fileNames)
			{
				var fileLines = await GetFileLinesAsync("e:/" + name);
				results.Add(fileLines);
			}

			// tasks.Add(GetFileLinesAsync(name));

			// Task.WaitAll(tasks.ToArray());

			return results;
		}

		private static ValueTask<List<string>> AddLines(List<List<string>> results, string fileName)
		{
			return new(GetSentences("e:/" + fileName));
		}

		public static List<string> GetSentences(string fileName)
		{
			var sentences = GetFileLines(fileName);

			return sentences.Where(IsHasForeignWords).ToList();
		}

		public static List<string> GetFileLines(string fileName)
		{
			using var sr = new StreamReader(fileName);
			return File.ReadAllLines(fileName)
				.ToList();
		}

		public static async Task<List<string>> GetFileLinesAsync(string fileName)
		{
			using var sr = new StreamReader(fileName);
			return (await File.ReadAllLinesAsync(fileName))
				.ToList();
		}
	}
}