﻿using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using PlantUmlClassDiagramGenerator.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassToUmlConverter.Library
{
    public class ClassDiagramGenerator
    {
        #region "Main"
        public async Task Run(string inputDir, string outputDir)
        {
            var args = new string[] { inputDir, outputDir, "public", "-createAssociation", "-allInOne" };
            Dictionary<string, string> parameters = MakeParameters(args);
            GeneratePlantUmlFromDir(parameters);
        }

        private bool GeneratePlantUmlFromFile(Dictionary<string, string> parameters)
        {
            var inputFileName = parameters["in"];
            if (!File.Exists(inputFileName))
            {
                OnError(new Exception($"\"{inputFileName}\" does not exist."));
                return false;
            }
            var outputFileName = "";
            if (parameters.ContainsKey("out"))
            {
                outputFileName = parameters["out"];
                try
                {
                    var outdir = Path.GetDirectoryName(outputFileName);
                    Directory.CreateDirectory(outdir);
                }
                catch (Exception e)
                {
                    OnError(e);
                    return false;
                }
            }
            else
            {
                outputFileName = CombinePath(Path.GetDirectoryName(inputFileName),
                    Path.GetFileNameWithoutExtension(inputFileName) + ".puml");
            }

            try
            {
                using (var stream = new FileStream(inputFileName, FileMode.Open, FileAccess.Read))
                {
                    var tree = CSharpSyntaxTree.ParseText(SourceText.From(stream));
                    var root = tree.GetRoot();
                    Accessibilities ignoreAcc = GetIgnoreAccessibilities(parameters);

                    using (var filestream = new FileStream(outputFileName, FileMode.Create, FileAccess.Write))
                    using (var writer = new StreamWriter(filestream))
                    {
                        var gen = new PlantUmlClassDiagramGenerator.Library.ClassDiagramGenerator(writer, "    ", ignoreAcc, parameters.ContainsKey("-createAssociation"));
                        gen.Generate(root);
                    }
                }
            }
            catch (Exception e)
            {
                OnError(e);
                return false;
            }
            return true;
        }

        private bool GeneratePlantUmlFromDir(Dictionary<string, string> parameters)
        {
            var inputRoot = parameters["in"];
            if (!Directory.Exists(inputRoot))
            {
                OnError(new Exception($"Directory \"{inputRoot}\" does not exist."));
                return false;
            }

            // Use GetFullPath to fully support relative paths.
            var outputRoot = Path.GetFullPath(inputRoot);
            if (parameters.ContainsKey("out"))
            {
                outputRoot = parameters["out"];
                try
                {
                    Directory.CreateDirectory(outputRoot);
                }
                catch (Exception e)
                {
                    OnError(e);
                    return false;
                }
            }

            var excludePaths = new List<string>();
            var pumlexclude = CombinePath(inputRoot, ".pumlexclude");
            if (File.Exists(pumlexclude))
            {
                excludePaths = File.ReadAllLines(pumlexclude).ToList();
            }
            if (parameters.ContainsKey("-excludePaths"))
            {
                excludePaths.AddRange(parameters["-excludePaths"].Split(','));
            }

            var files = Directory.EnumerateFiles(inputRoot, "*.cs", SearchOption.AllDirectories);

            var includeRefs = new StringBuilder();
            includeRefs.AppendLine("@startuml");

            var error = false;
            foreach (var inputFile in files)
            {
                if (excludePaths
                    .Select(p => CombinePath(inputRoot, p))
                    .Any(p => inputFile.StartsWith(p, StringComparison.InvariantCultureIgnoreCase)))
                {
                    OnProgress($"Skipped \"{inputFile}\"...");
                    continue;
                }

                OnProgress($"Create puml: \"{inputFile}\"...");
                try
                {
                    var outputDir = CombinePath(outputRoot, Path.GetDirectoryName(inputFile).Replace(inputRoot, ""));
                    Directory.CreateDirectory(outputDir);
                    var outputFile = CombinePath(outputDir,
                        Path.GetFileNameWithoutExtension(inputFile) + ".puml");

                    using (var stream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
                    {
                        var tree = CSharpSyntaxTree.ParseText(SourceText.From(stream));
                        var root = tree.GetRoot();
                        Accessibilities ignoreAcc = GetIgnoreAccessibilities(parameters);

                        using (var filestream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                        using (var writer = new StreamWriter(filestream))
                        {
                            var gen = new PlantUmlClassDiagramGenerator.Library.ClassDiagramGenerator(writer, "    ", ignoreAcc, parameters.ContainsKey("-createAssociation"));
                            gen.Generate(root);
                        }
                    }

                    if (parameters.ContainsKey("-allInOne"))
                    {
                        var lines = File.ReadAllLines(outputFile);
                        foreach (string line in lines.Skip(1).SkipLast(1))
                        {
                            includeRefs.AppendLine(line);
                        }
                    }
                    else
                    {
                        var newRoot = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? @".\" : @".";
                        includeRefs.AppendLine("!include " + outputFile.Replace(outputRoot, newRoot));
                    }
                }
                catch (Exception e)
                {
                    Exception ex = new Exception($"Error {inputFile}, {e.Message}", e);
                    OnError(ex);
                    error = true;
                }
            }
            includeRefs.AppendLine("@enduml");
            File.WriteAllText(CombinePath(outputRoot, "include.puml"), includeRefs.ToString());

            if (error)
            {
                OnError(new Exception("There were files that could not be processed."));
                return false;
            }
            return true;
        }
        #endregion

        #region "Helper"
        private Accessibilities GetIgnoreAccessibilities(Dictionary<string, string> parameters)
        {
            var ignoreAcc = Accessibilities.None;
            if (parameters.ContainsKey("-public"))
            {
                ignoreAcc = Accessibilities.Private | Accessibilities.Internal
                    | Accessibilities.Protected | Accessibilities.ProtectedInternal;
            }
            else if (parameters.ContainsKey("-ignore"))
            {
                var ignoreItems = parameters["-ignore"].Split(',');
                foreach (var item in ignoreItems)
                {
                    Accessibilities acc;
                    if (Enum.TryParse(item, true, out acc))
                    {
                        ignoreAcc |= acc;
                    }
                }
            }
            return ignoreAcc;
        }

        private Dictionary<string, string> MakeParameters(string[] args)
        {
            var currentKey = "";
            var parameters = new Dictionary<string, string>();

            foreach (var arg in args)
            {
                if (currentKey != string.Empty)
                {
                    parameters.Add(currentKey, arg);
                    currentKey = "";
                    continue;
                }

                if (options.ContainsKey(arg))
                {
                    if (options[arg] == OptionType.Value)
                    {
                        currentKey = arg;
                    }
                    else
                    {
                        parameters.Add(arg, string.Empty);
                    }
                }
                else if (!parameters.ContainsKey("in"))
                {
                    parameters.Add("in", arg);
                }
                else if (!parameters.ContainsKey("out"))
                {
                    parameters.Add("out", arg);
                }
            }
            return parameters;
        }

        private string CombinePath(string first, string second)
        {
            return first.TrimEnd(Path.DirectorySeparatorChar)
                + Path.DirectorySeparatorChar
                + second.TrimStart(Path.DirectorySeparatorChar);
        }

        enum OptionType
        {
            Value,
            Switch
        }

        readonly Dictionary<string, OptionType> options = new Dictionary<string, OptionType>()
        {
            ["-dir"] = OptionType.Switch,
            ["-public"] = OptionType.Switch,
            ["-ignore"] = OptionType.Value,
            ["-excludePaths"] = OptionType.Value,
            ["-createAssociation"] = OptionType.Switch,
            ["-allInOne"] = OptionType.Switch
        };
        #endregion

        #region "Events"
        public delegate void Error(Exception ex);
        public delegate void Progress(string message);

        public event Error ErrorEvent;
        public event Progress ProgressEvent;

        protected virtual void OnError(Exception ex)
        {
            ErrorEvent?.Invoke(ex);
        }

        protected virtual void OnProgress(string message)
        {
            ProgressEvent?.Invoke(message);
        }
        #endregion

    }
}
