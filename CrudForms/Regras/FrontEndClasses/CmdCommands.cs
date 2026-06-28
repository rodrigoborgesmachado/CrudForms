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
                    return process.ExitCode == 0
                        && TryParseNodeVersion(output, out Version nodeVersion)
                        && IsSupportedViteNodeVersion(nodeVersion);
                }
            }
            catch
            {
                return false; // Node.js is not installed
            }
        }

        private static bool TryParseNodeVersion(string output, out Version version)
        {
            version = null;
            string normalized = output.Trim();

            if (normalized.StartsWith("v"))
            {
                normalized = normalized.Substring(1);
            }

            string[] parts = normalized.Split('.');
            if (parts.Length < 2)
            {
                return false;
            }

            if (!int.TryParse(parts[0], out int major) || !int.TryParse(parts[1], out int minor))
            {
                return false;
            }

            int patch = 0;
            if (parts.Length > 2)
            {
                int.TryParse(parts[2], out patch);
            }

            version = new Version(major, minor, patch);
            return true;
        }

        private static bool IsSupportedViteNodeVersion(Version version)
        {
            if (version.Major == 20)
            {
                return version.CompareTo(new Version(20, 19, 0)) >= 0;
            }

            if (version.Major == 22)
            {
                return version.CompareTo(new Version(22, 12, 0)) >= 0;
            }

            return version.Major > 22;
        }

        public static bool CreateReactAppAndFolders(string projectName, string directoryPath)
        {
            bool success = true;
            string projectPath = Path.Combine(directoryPath, projectName);
            try
            {
                success = RunCmd($"npm create vite@latest {QuoteArgument(projectName)} -- --template react", directoryPath);

                if (success && Directory.Exists(projectPath))
                {
                    Console.WriteLine("React project created successfully!");

                    // Remove everything inside src
                    string srcPath = Path.Combine(projectPath, "src");
                    if (Directory.Exists(srcPath))
                    {
                        Directory.Delete(srcPath, true);
                        Directory.CreateDirectory(srcPath); // Recreate src directory
                    }

                    // Ensure base dependencies are installed for Vite template
                    success &= RunNpmInstall(projectPath);

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
                        success &= InstallNpmPackage(package, projectPath);
                    }

                    // Recreate required directories
                    if (success)
                    {
                        CreateDirectories(projectPath);
                    }
                }
                else
                {
                    success = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                success = false;
            }

            return success;
        }

        private static bool InstallNpmPackage(string packageName, string workingDirectory)
        {
            return RunCmd($"npm install {packageName}", workingDirectory);
        }

        private static bool RunNpmInstall(string workingDirectory)
        {
            return RunCmd("npm install", workingDirectory);
        }

        private static bool RunCmd(string command, string workingDirectory)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c {command}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
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

                return process.ExitCode == 0;
            }
        }

        private static string QuoteArgument(string argument)
        {
            return $"\"{argument.Replace("\"", "\\\"")}\"";
        }

        private static void CreateDirectories(string projectPath)
        {
            foreach (FileType fileType in Enum.GetValues(typeof(FileType)))
            {
                string dirPath = GetDirectoryByType(projectPath, fileType);
                Directory.CreateDirectory(dirPath);
            }

            Console.WriteLine("Project directories created successfully.");
        }

    }
}
