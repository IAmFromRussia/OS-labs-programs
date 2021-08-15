using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RunPrograms
{
	class Program
	{
		static async Task Main(string[] args)
		{
			Lab1.Start();
			// Lab2.Start();
			// await CourseWork.Start();

			// Console.WriteLine("Creating files...");
			// for (int i = 1; i <= CourseWork.AmountOfFiles; i++)
			// {
			// 	await CreateFile(i);
			// }
			// Console.WriteLine("Success");
		}

		private static async Task CreateFile(int fileIndex)
		{
			var path = $@"e:\File{fileIndex}.txt";

			await using var sw = File.CreateText(path);
			for (int i = 0; i < 3000; i++)
			{
				await sw.WriteLineAsync("This sentence will not visible");
				await sw.WriteLineAsync("This sentence будет видимым");
				await sw.WriteLineAsync("Это предложение будет видимым");
			}			
		}
	}
}