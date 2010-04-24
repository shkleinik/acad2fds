using System;
using System.IO;
using System.Text;

namespace Fds2AcadSetupActions.BLL
{
    public class AcadAutoLoadModifier
    {
        public static void AddCommandToAutocad2009StartUp(string commandName, string autoLoadFilePath)
        {
            var autoCadAutoLoadFilePath = string.Format(Constants.AutoCad2009AutoLoadFilePathPattern,
                                                        Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                                                        autoLoadFilePath,
                                                        Constants.AutoCad2009AutoLoadFileName);

            if (!File.Exists(autoCadAutoLoadFilePath))
                throw new FileNotFoundException(string.Format("File {0} was not found!", autoCadAutoLoadFilePath));

            var streamWriter = new StreamWriter(autoCadAutoLoadFilePath, true);

            streamWriter.WriteLine();
            streamWriter.WriteLine(String.Format(Constants.LispCommandCallPattern, commandName));
            streamWriter.Close();
        }

        public static void RemoveCommandToAutocad2009StartUp(string commandName, string autoLoadFilePath)
        {
            var autoCadAutoLoadFilePath = string.Format(Constants.AutoCad2009AutoLoadFilePathPattern,
                                            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                                            autoLoadFilePath,
                                            Constants.AutoCad2009AutoLoadFileName);

            if (!File.Exists(autoCadAutoLoadFilePath))
                throw new FileNotFoundException(string.Format("File {0} was not found!", autoCadAutoLoadFilePath));

            var commandToRemove = String.Format(Constants.LispCommandCallPattern, commandName);

            var streamReader = new StreamReader(autoCadAutoLoadFilePath);

            string line;
            var stringBuilder = new StringBuilder();

            // read existing file and remove redudant command
            while((line = streamReader.ReadLine()) != null)
            {
                if(String.Compare(line, commandToRemove) == 0)
                    continue;

                stringBuilder.AppendLine(line);
            }

            streamReader.Close();

            // rewrite file without redudant commands
            var streamWriter = new StreamWriter(autoCadAutoLoadFilePath, false);
            streamWriter.WriteLine(stringBuilder.ToString());
            streamWriter.Close();
        }
    }
}