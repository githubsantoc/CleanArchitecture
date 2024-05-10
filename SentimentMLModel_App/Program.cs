﻿
// This file was auto-generated by ML.NET Model Builder. 

using SentimentMLModel_App;

// Create single instance of sample data from first line of dataset for model input
SentimentMLModel.ModelInput sampleData = new SentimentMLModel.ModelInput()
{
    Text = @"Sooo SAD I will miss you here in San Diego!!!",
};



Console.WriteLine("Using model to make single prediction -- Comparing actual Sentiment with predicted Sentiment from sample data...\n\n");


Console.WriteLine($"Text: {@"Sooo SAD I will miss you here in San Diego!!!"}");
Console.WriteLine($"Sentiment: {@"negative"}");


var sortedScoresWithLabel = SentimentMLModel.PredictAllLabels(sampleData);
Console.WriteLine($"{"Class",-40}{"Score",-20}");
Console.WriteLine($"{"-----",-40}{"-----",-20}");

foreach (var score in sortedScoresWithLabel)
{
    Console.WriteLine($"{score.Key,-40}{score.Value,-20}");
}

Console.WriteLine("=============== End of process, hit any key to finish ===============");
Console.ReadKey();
