using System.Diagnostics;
using System;
using System.IO;
using static Regras.FrontEndClasses.NamesHandler;

namespace Regras.FrontEndClasses
{
    public static class CmdCommands
    {
        public static bool IsNodeInstalled()
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c node -v",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(psi))
                {
                    process.WaitForExit();
                    string output = process.StandardOutput.ReadToEnd();
                    return !string.IsNullOrEmpty(output);
                }
            }
            catch
            {
                return false; // Node.js is not installed
            }
        }

        public static bool CreateReactAppAndFolders(string projectName, string directoryPath)
        {
            bool success = true;
            string projectPath = Path.Combine(directoryPath, projectName);
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c npx create-react-app {projectName}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = false,
                    WorkingDirectory = directoryPath
                };

                using (Process process = Process.Start(psi))
                {
                    process.WaitForExit();
                    string error = process.StandardError.ReadToEnd();
                    //success = string.IsNullOrEmpty(error);
                }

                if (success)
                {
                    Console.WriteLine("React project created successfully!");

                    // Remove everything inside src
                    string srcPath = Path.Combine(projectPath, "src");
                    if (Directory.Exists(srcPath))
                    {
                        Directory.Delete(srcPath, true);
                        Directory.CreateDirectory(srcPath); // Recreate src directory
                    }

                    string[] packages = new string[]
                    {
                        "react-router-dom",
                        "react-redux",
                        "react-toastify",
                        "axios",
                        "@reduxjs/toolkit",
                        "file-saver",
                        "date-fns"
                    };

                    foreach (var package in packages)
                    {
                        InstallNpmPackage(package, projectPath);
                    }

                    // Recreate required directories
                    CreateDirectories(projectPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                success = false;
            }

            return success;
        }

        static void InstallNpmPackage(string packageName, string workingDirectory)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c npm install {packageName}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = false,
                WorkingDirectory = workingDirectory
            };

            using (Process process = new Process { StartInfo = psi })
            {
                process.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
                process.ErrorDataReceived += (sender, args) => Console.WriteLine(args.Data);

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();
            }
        }

        private static void CreateDirectories(string projectPath)
        {
            foreach (FileType fileType in Enum.GetValues(typeof(FileType)))
            {
                string dirPath = Path.Combine(projectPath, GetDirectoryByType(projectPath, fileType));
                Directory.CreateDirectory(dirPath);
            }

            Console.WriteLine("Project directories created successfully.");
        }

    }
}
