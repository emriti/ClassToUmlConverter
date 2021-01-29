using PlantUml.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ClassToUmlConverter.Library
{
    public class ClassToUmlConverterService
    {
        const string PUML_FOLDER = "puml";
        ClassDiagramGenerator gen = new ClassDiagramGenerator();

        public ClassToUmlConverterService()
        {
            gen.ErrorEvent += Gen_ErrorEvent;
            gen.ProgressEvent += Gen_ProgressEvent;
        }

        public async Task Run(string inputFolder, string outFolder)
        {
            try
            {
                var dirs = Directory.EnumerateDirectories(inputFolder);
                foreach (var dir in dirs)
                {
                    string[] folderArr = dir.Split("\\");
                    if (folderArr.Length == 0)
                    {
                        folderArr = dir.Split("/");
                    }

                    string folderName = folderArr[folderArr.Length - 1];
                    string pumlFolder = $"{outFolder}\\{folderName}\\{PUML_FOLDER}";
                    await gen.Run(dir, pumlFolder);

                    var factory = new RendererFactory();
                    var localSetting = new PlantUmlSettings()
                    {
                        LocalPlantUmlPath = "",
                        LocalGraphvizDotPath = "C:\\Program Files\\Graphviz\\bin\\dot.exe",
                        RenderingMode = RenderingMode.Local
                    };
                    var renderer = factory.CreateRenderer(localSetting);

                    var files = Directory.EnumerateFiles(pumlFolder, "include.puml", SearchOption.AllDirectories);
                    var outputFormat = OutputFormat.Svg;
                    var outputExt = "svg";

                    foreach (var file in files)
                    {
                        var data = "";
                        using (StreamReader sr = new StreamReader(file))
                        {
                            data = sr.ReadToEnd();
                        }

                        var bytes = await renderer.RenderAsync(data, outputFormat);
                        var filePath = $"{outFolder}\\{folderName}\\{folderName}.{outputExt}";
                        File.WriteAllBytes(filePath, bytes);

                        OnProgress($"Rendering svg {folderName} ...");
                    }
                }
            }
            catch (Exception ex)
            {
                OnError(ex);
            }
        }

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

        private void Gen_ProgressEvent(string message)
        {
            OnProgress(message);
        }

        private void Gen_ErrorEvent(Exception ex)
        {
            OnError(ex);
        }

        #endregion
    }
}
