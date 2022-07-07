using Parsing;
using Parsing.Jokes;
using Parsing.JacqueFresco;
using TypingTraining.TypingTexts;
using Newtonsoft.Json;

ParserWorker<TypingText[]> parser = new(new JacqueFrescoParser(), new JacqueFrescoParserSettings());
List<TypingText> texts = new();

parser.OnNewData += (o, t) =>
{
    texts.AddRange(t);
};

parser.OnCompleted += OnParsingCompleted;

parser.Start();

while (parser.IsActive)
{
    Console.SetCursorPosition(0, 0);
    Console.WriteLine("Loading.");
    Thread.Sleep(1000);
    Console.SetCursorPosition(0, 0);
    Console.WriteLine("Loading..");
    Thread.Sleep(1000);
    Console.SetCursorPosition(0, 0);
    Console.WriteLine("Loading...");
}

void OnParsingCompleted(object? obj)
{
    Random r = new();
    Console.WriteLine();
    Console.WriteLine(texts[r.Next(0, texts.Count - 1)].Content);
    string filePath = "../../../saved/[RU] Jacue Fresco collection.type";
    SaveTexts(filePath, texts.ToArray());

    Console.WriteLine("\nParsing completed. Press any key.");
    Console.ReadKey();
}

void SaveTexts(string filePath, TypingText[] texts)
{
    string json = JsonConvert.SerializeObject(texts, Formatting.Indented);
    Directory.CreateDirectory("../../../saved");
    File.WriteAllText(filePath, json);
}

