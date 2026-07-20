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
                        "bootstrap",
                        "bootstrap-icons",
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
            string scriptPath = Path.Combine(Path.GetTempPath(), $"crudforms-frontend-{Guid.NewGuid():N}.cmd");
            File.WriteAllText(scriptPath, CreateCommandScript(command, workingDirectory));

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c \"{scriptPath}\"",
                UseShellExecute = true,
                CreateNoWindow = false,
                WorkingDirectory = workingDirectory
            };

            try
            {
                using (Process process = new Process { StartInfo = psi })
                {
                    process.Start();
                    process.WaitForExit();

                    return true;
                }
            }
            finally
            {
                try
                {
                    if (File.Exists(scriptPath))
                    {
                        File.Delete(scriptPath);
                    }
                }
                catch
                {
                    // The temp script is only diagnostic glue; failing to delete it should not break generation.
                }
            }
        }

        private static string CreateCommandScript(string command, string workingDirectory)
        {
            return
                "@echo off\r\n" +
                "title CrudForms - Criacao do front-end\r\n" +
                "echo.\r\n" +
                "echo ================================================\r\n" +
                "echo CrudForms - executando comando do front-end\r\n" +
                "echo ================================================\r\n" +
                $"echo Diretorio: {workingDirectory}\r\n" +
                $"echo Comando: {command}\r\n" +
                "echo.\r\n" +
                "set npm_config_yes=true\r\n" +
                $"call {command}\r\n" +
                "set EXIT_CODE=%ERRORLEVEL%\r\n" +
                "echo.\r\n" +
                "if not \"%EXIT_CODE%\"==\"0\" (\r\n" +
                "    echo ================================================\r\n" +
                "    echo Erro ao executar o comando. Codigo: %EXIT_CODE%\r\n" +
                "    echo Revise a mensagem acima antes de fechar esta janela.\r\n" +
                "    echo ================================================\r\n" +
                "    pause\r\n" +
                ")\r\n" +
                "exit /b %EXIT_CODE%\r\n";
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
