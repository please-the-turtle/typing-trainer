using Parsing;
using Parsing.Jokes;
using Parsing.JacqueFresco;
using TypingTraining.TypingTexts;
using Newtonsoft.Json;

string savePath = "../../../saved/";

ParserWorker<TypingText[]> parserWorker;
List<TypingText> texts = new();
string currentSaveFileName;

var setups = GetParsingSetups();

foreach (var setup in setups)
{
    parserWorker = new(setup.Parser, setup.ParserSettings);
    currentSaveFileName = setup.FileName;
    texts.Clear();

    parserWorker.OnNewData += (o, t) =>
    {
        texts.AddRange(t);
    };

    parserWorker.OnCompleted += OnParsingCompleted;

    bool completed = false;
    parserWorker.OnCompleted += (o) => completed = true;

    parserWorker.Start();

    while (!completed)
    {
        Thread.Sleep(1000);
        Console.CursorLeft = 0;
        Console.Write($"File: {currentSaveFileName} - Parsing.  ");
        Thread.Sleep(1000);
        Console.CursorLeft = 0;
        Console.Write($"File: {currentSaveFileName} - Parsing.. ");
        Thread.Sleep(1000);
        Console.CursorLeft = 0;
        Console.Write($"File: {currentSaveFileName} - Parsing...");
    }

    Console.CursorLeft = 0;
    Console.WriteLine($"File: {currentSaveFileName} - Complete.  ");
}

List<TypingTextParsingSetup> GetParsingSetups()
{
    List<TypingTextParsingSetup> setups = new();

    TypingTextParsingSetup jokes = new(
        "[RU] Jokes.type",
        new JokesParser(),
        new JokesParserSettings());
    setups.Add(jokes);

    TypingTextParsingSetup fresco = new(
        "[RU] Jacue Fresco.type",
        new JacqueFrescoParser(),
        new JacqueFrescoParserSettings());
    setups.Add(fresco);

    return setups;
}

void OnParsingCompleted(object? obj)
{
    TrySaveTexts(currentSaveFileName, texts.ToArray());
}

bool TrySaveTexts(string fileName, TypingText[] texts)
{
    string json = JsonConvert.SerializeObject(texts, Formatting.Indented);

    try
    {
        Directory.CreateDirectory(savePath);
        File.WriteAllText(savePath + fileName, json);
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        return false;
    }

    return true;
}

