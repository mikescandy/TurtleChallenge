using McMaster.Extensions.CommandLineUtils;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TurtleChallenge.App;
using TurtleChallenge.App.Enums;
using TurtleChallenge.App.Models;
using TurtleChallenge.App.Validators;

namespace TurtleChallenge
{
    class Program
    {
        [Required]
        [Argument(0, Description = "Required. Settings file")]
        private string SettingsFile { get; }

        [Required]
        [Argument(1, Description = "Required. Moves file")]
        private string MovesFile { get; }

        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        private void OnExecute()
        {
            if (!File.Exists(SettingsFile))
            {
                Console.WriteLine($"Can't find file {SettingsFile}");
                return;
            }

            if (!File.Exists(MovesFile))
            {
                Console.WriteLine($"Can't find file {MovesFile}");
                return;
            }

            var settingsText = File.ReadAllText(SettingsFile);
            var movesText = File.ReadAllLines(MovesFile);
         
            Settings settings = null;
            FluentValidation.Results.ValidationResult settingsValidationResult = null;
            
            try
            {
                var settingsValidator = new SettingsValidator();

                settings = JsonConvert.DeserializeObject<Settings>(settingsText);
                settingsValidationResult = settingsValidator.Validate(settings);

                if (!settingsValidationResult.IsValid)
                {
                    foreach (var error in settingsValidationResult.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }

                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine("The settings file contains an invalid configuration.");
                Console.WriteLine(ex.Message);
            }

            if (settingsValidationResult?.IsValid != true)
            {
                return;
            }

            var turtleFactory = new TurtleFactory(settings);

            ProcessMoves(turtleFactory, settings, movesText);
        }

        private void ProcessMoves(TurtleFactory turtleFactory, Settings settings, string[] movesText)
        {
            var sequenceCounter = 1;
            foreach (var line in movesText)
            {
                var turtle = turtleFactory.CreateTurtle();
                var logic = new Logic(settings, turtle);
                var moves = line.Split(',');

                foreach (var move in moves.Select(m => m.Trim().ToLowerInvariant()))
                {
                    switch (move)
                    {
                        case "m":
                            turtle.MoveForward();
                            break;
                        case "r":
                            turtle.RotateRight();
                            break;
                        default:
                            Debug.WriteLine("Turtle movement command not recognized");
                            break;
                    }

                    if (logic.Status < 0)
                    {
                        break;
                    }
                }

                switch (logic.Status)
                {
                    case Status.StillInDanger:
                        Console.WriteLine($"Sequence {sequenceCounter}: Still in danger!");
                        break;
                    case Status.OnAMine:
                        Console.WriteLine($"Sequence {sequenceCounter}: BOOM! The turtle exploded.");
                        break;
                    case Status.Exit:
                        Console.WriteLine($"Sequence {sequenceCounter}: The turtle found the exit and is safe!");
                        break;
                    case Status.OutOfBounds:
                        Console.WriteLine($"Sequence {sequenceCounter}: Uh oh, you took a wrong turn.");
                        break;
                }

                sequenceCounter++;
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
