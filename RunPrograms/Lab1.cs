using ConsoleTables;
using Console = System.Console;
using ConsoleKey = System.ConsoleKey;
using ConsoleKeyInfo = System.ConsoleKeyInfo;
using Stopwatch = System.Diagnostics.Stopwatch;
using Thread = System.Threading.Thread;

namespace RunPrograms
{
	/*
	 * 15 var
	 * Find and print foreign sentences
	 */
	public class Lab1
	{
		private static string Current { get; set; } = "1234567890";

		public static void Start()
		{
			ConsoleKeyInfo cki;


			var watch = new Stopwatch();
			var programTime = new Stopwatch();

			var table = new ConsoleTable(
				"Current string",
				"Numbers string",
				"Backspace string",
				"Thread Name",
				"Priority",
				"State",
				"ID",
				"Key code",
				"Was executed",
				"Start",
				"End",
				"Delay");

			programTime.Start();


			Console.WriteLine("Press the Escape (Esc) key to quit: \n");
			Console.WriteLine("Current string: {0}", Current);

			Console.Write("Set a delay (milliseconds) —> ");
			var delay = int.Parse(Console.ReadLine() ?? string.Empty);

			do
			{
				cki = Console.ReadKey();
				object stringFromBackspace = null;
				object stringFromNumbers = null;

				var backspaceThread = new Thread(() =>
						stringFromBackspace = BackspaceCallback(cki, watch))
					{ Name = "Backspace" };

				var numbersThread = new Thread(() =>
						stringFromNumbers = NumberCallback(cki, watch))
					{ Name = "Numbers" };

				backspaceThread.Start();
				DisplayThreadInformation(backspaceThread, cki, watch, table, programTime, delay, stringFromBackspace,
					stringFromNumbers);

				Thread.Sleep(delay);

				numbersThread.Start();
				DisplayThreadInformation(numbersThread, cki, watch, table, programTime, delay, stringFromBackspace,
					stringFromNumbers);
			} while (cki.Key != ConsoleKey.Escape);

			programTime.Stop();
		}

		private static string NumberCallback(ConsoleKeyInfo cki, Stopwatch watch)
		{
			watch.Start();
			var threadString = NumberKeysOperations(cki);
			// Current = NumberKeysOperations(cki);
			watch.Stop();
			return threadString;
		}

		private static string BackspaceCallback(ConsoleKeyInfo cki, Stopwatch watch)
		{
			watch.Start();
			var threadString = BackSpaceOperation(cki);
			// Current = BackSpaceOperation(cki);
			watch.Stop();
			return threadString;
		}

		private static void ShowString()
		{
			Console.WriteLine($"Current string: {Current}");
		}

		private static void DisplayThreadInformation(Thread thread, ConsoleKeyInfo cki,
			Stopwatch watch, ConsoleTable table, Stopwatch programTime, int delay, object backspaceStringValue,
			object numbersStringValue)
		{
			backspaceStringValue ??= "empty";
			numbersStringValue ??= "empty";

			table.AddRow(
				Current,
				numbersStringValue.ToString(),
				backspaceStringValue.ToString(),
				thread.Name,
				thread.Priority,
				thread.ThreadState,
				thread.ManagedThreadId,
				cki.Key,
				watch.ElapsedMilliseconds,
				programTime.Elapsed,
				programTime.Elapsed + watch.Elapsed,
				delay);

			table.Write();
		}

		private static string NumberKeysOperations(ConsoleKeyInfo currentKey)
		{
			switch (currentKey.Key)
			{
				case ConsoleKey.D0:
					Current += "0";
					return Current;
				case ConsoleKey.D1:
					Current += "1";
					return Current;
				case ConsoleKey.D2:
					Current += "2";
					return Current;
				case ConsoleKey.D3:
					Current += "3";
					return Current;
				case ConsoleKey.D4:
					Current += "4";
					return Current;
				case ConsoleKey.D5:
					Current += "5";
					return Current;
				case ConsoleKey.D6:
					Current += "6";
					return Current;
				case ConsoleKey.D7:
					Current += "7";
					return Current;
				case ConsoleKey.D8:
					Current += "8";
					return Current;
				case ConsoleKey.D9:
					Current += "9";
					return Current;
				default:
					return Current;
			}
		}


		private static string BackSpaceOperation(ConsoleKeyInfo currentKey)
		{
			if (currentKey.Key != ConsoleKey.Backspace) return Current;

			if (string.IsNullOrEmpty(Current))
			{
				Console.WriteLine("string is empty! Add symbols");
				return Current;
			}

			if (Current.Length < 5)
			{
				var warning = "\nWARNING! Please, add Symbols!\n" +
				              "Current string: " + Current + "\n" +
				              "Amount of symbols: " + Current.Length;
				Console.WriteLine(warning);
				return Current;
			}

			Current = Current.Remove(Current.Length - 5);

			return Current;
		}
	}
}