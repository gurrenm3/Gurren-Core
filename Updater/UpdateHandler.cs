using MelonLoader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace Gurren_Core.Web
{
    /// <summary>
    /// Contains methods relating to checking for, getting, and installing updates
    /// </summary>
    public class UpdateHandler
    {
        #region Properties
        /// <summary>
        /// The url to the githubApi releases page, which contains info about the releases
        /// </summary>
        public string VersionURL { get; set; }

        /// <summary>
        /// The name of the project. Example: "BTDToolbox". Used for logging messages like: "Updating BTDToolbox"
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// The path to the project's exe. Example: "Environment.CurrentDirectory + "\\BTDToolbox.exe".
        /// Used to get the version number for the project
        /// </summary>
        public string ProjectExePath { get; set; }

        /// <summary>
        /// The EXE name of the updater. Example: "BTDToolbox Updater.exe". Used to launch the updater
        /// </summary>
        public string UpdaterExeName { get; set; }

        /// <summary>
        /// The name of the zip file that's downloaded from github releases if there is an update.
        /// Example: "BTDToolbox.zip". Used to delete the updater after updates
        /// </summary>
        public string UpdatedZipName { get; set; }

        /// <summary>
        /// The text read from VersionURL
        /// </summary>
        private string VersionTextFromWeb;

        /// <summary>
        /// The current version of your program. This is optional
        /// </summary>
        public string CurrentVersion { get; set; }

        public bool DownloadUpdate { get; set; } = false;

        public string InstallDirectory { get; set; } = Environment.CurrentDirectory;
        #endregion

        #region Constructors
        /// <summary>
        /// Check github releases for the latest release and install it if there is an update. 
        /// Need to manaully set properties with this constructor
        /// </summary>
        public UpdateHandler() { }

        /// <summary>
        /// Check github releases for the latest release and install it if there is an update.
        /// </summary>
        public UpdateHandler(string gitApiReleaseURL, string projectName, string projectExePath, string installDir, string updaterExeName, string updatedZipName)
        {
            VersionURL = gitApiReleaseURL;
            ProjectName = projectName;
            ProjectExePath = projectExePath;
            InstallDirectory = installDir;
            UpdaterExeName = updaterExeName;
            UpdatedZipName = updatedZipName;
        }
        #endregion

        /// <summary>
        /// Main updater method. Handles all update related functions for ease of use.
        /// </summary>
        public void HandleUpdates(bool hasUpdater = true, bool closeProgram = true)
        {
            /*if (hasUpdater)   //commented our for now
                DeleteUpdater();*/    //delete updater if found to keep directory clean and prevent using old updater


            GetGitApiText();
            if (String.IsNullOrEmpty(VersionTextFromWeb))
            {
                MelonLogger.Log("Failed to read release info for " + ProjectName);
                return;
            }

            if (String.IsNullOrEmpty(CurrentVersion))
            {
                if (String.IsNullOrEmpty(ProjectExePath))
                {
                    MelonLogger.Log("Can't check for version info because Current version and Project EXE Path were set to null");
                    return;
                }

                CurrentVersion = FileVersionInfo.GetVersionInfo(ProjectExePath).FileVersion;
            }


            bool isUpdate = false;
            if (VersionURL.Contains("api.github.com/repos")) //This is a github api url
                isUpdate = IsUpdate(CurrentVersion, GetLatestVersion(VersionTextFromWeb));
            else
                isUpdate = IsUpdate(CurrentVersion, VersionTextFromWeb);



            if (!isUpdate)
            {
                MelonLogger.Log(ProjectName + " is up to date!");
                return;
            }

            MelonLogger.Log("An update is available for " + ProjectName);

            if (!DownloadUpdate)
                return;

            MelonLogger.Log("Downloading latest version...");

            DownloadUpdates();
            ExtractUpdater();


            if (closeProgram)
                MelonLogger.Log("Closing " + ProjectName + "...");

            if (hasUpdater)
                LaunchUpdater();

            if (closeProgram)
                Environment.Exit(0);
        }

        /// <summary>
        /// Get the text from the VersionURL
        /// </summary>
        /// <returns>The text on the page from the url</returns>
        private string GetGitApiText()
        {
            var webReader = new WebReader();
            VersionTextFromWeb = webReader.ReadText_FromURL(VersionURL);
            return VersionTextFromWeb;
        }


        private static bool IsUpdate(string currentVersion, string latestVersion)
        {
            string currentProcessed = CleanVersionText(currentVersion);
            string latestProcessed = CleanVersionText(latestVersion);

            while (currentProcessed.Length != latestProcessed.Length)
            {
                if (currentProcessed.Length < latestProcessed.Length)
                    currentProcessed += "0";
                else
                    latestProcessed += "0";
            }

            int current = Int32.Parse(currentProcessed);
            int latest = Int32.Parse(latestProcessed);

            return latest > current;
        }


        /// <summary>
        /// Remove all non-numeric characters from version info
        /// </summary>
        /// <param name="uncleanedText"></param>
        /// <returns></returns>
        internal static string CleanVersionText(string uncleanedText)
        {
            string cleaned = "";
            foreach (var letter in uncleanedText)
            {
                if (Int32.TryParse(letter.ToString(), out int result))
                    cleaned += result.ToString();
            }

            return cleaned;
        }


        /// <summary>
        /// Parses gitApi text and gets the latest release version of the main executing program
        /// </summary>
        /// <returns>an int of the latest release version, as a whole number without decimals</returns>
        private string GetLatestVersion() => GetLatestVersion(VersionTextFromWeb);

        /// <summary>
        /// Parses gitApi text and gets the latest release version of the program the gitApi text is for
        /// </summary>
        /// <param name="aquiredGitText">The text that was successfully read from github</param>
        /// <returns>an int of the latest release version, as a whole number without decimals</returns>
        public static string GetLatestVersion(string aquiredGitText)
        {
            var gitApi = GithubReleaseConfig.FromJson(aquiredGitText);
            string latestRelease = gitApi[0].TagName;

            return latestRelease;
        }

        /// <summary>
        /// Reads gitApi text and gets all of the download urls associated with the latest release
        /// </summary>
        /// <returns>a list of download url strings</returns>
        private List<string> GetDownloadURLs()
        {
            List<string> downloads = new List<string>();
            var gitApi = GithubReleaseConfig.FromJson(VersionTextFromWeb);
            foreach (var a in gitApi[0].Assets)
                downloads.Add(a.BrowserDownloadUrl.ToString());

            return downloads;
        }

        /// <summary>
        /// Download all files aquired from the GetDownloadURLs method
        /// </summary>
        private void DownloadUpdates()
        {
            var downloader = new FileDownloader();
            List<string> downloads = GetDownloadURLs();

            foreach (string file in downloads)
                downloader.DownloadFile(file, InstallDirectory);
        }

        /// <summary>
        /// Extract the updater from the downloaded zip
        /// </summary>
        private void ExtractUpdater()
        {
            var files = Directory.GetFiles(InstallDirectory);
            foreach (var file in files)
            {
                if (!file.EndsWith(".zip") && !file.EndsWith(".rar") && !file.EndsWith(".7z"))
                    continue;

                using (ZipArchive archive = System.IO.Compression.ZipFile.OpenRead(file))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        if (!entry.FullName.ToLower().Contains("update"))
                            continue;

                        string destinationPath = Path.GetFullPath(Path.Combine(InstallDirectory, entry.FullName));
                        if (destinationPath.StartsWith(InstallDirectory, StringComparison.Ordinal))
                        {
                            if (File.Exists(destinationPath))
                                File.Delete(destinationPath);

                            entry.ExtractToFile(destinationPath);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Launch the updater exe so the update can continue.
        /// </summary>
        private void LaunchUpdater()
        {
            string updater = Environment.CurrentDirectory + "\\" + UpdaterExeName;

            if (!File.Exists(updater))
            {
                MelonLogger.Log("ERROR! Unable to find updater. You will need to close " + ProjectName +
                    " and manually extract " + UpdatedZipName);
                return;
            }

            Process.Start(updater, "launched_from_" + ProjectName);
            //Process.Start(updater);
        }

        /// <summary>
        /// Delete all files related to updater. Used to keep program directory clean
        /// </summary>
        private void DeleteUpdater()
        {
            if (File.Exists(Environment.CurrentDirectory + "\\" + UpdaterExeName))
                File.Delete(Environment.CurrentDirectory + "\\" + UpdaterExeName);

            var files = Directory.GetFiles(Environment.CurrentDirectory);
            foreach (var file in files)
            {
                FileInfo f = new FileInfo(file);
                if (f.Name.ToLower().Replace(".", "").Replace(" ", "") == UpdatedZipName.ToLower().Replace(".", "").Replace(" ", ""))
                {
                    File.Delete(file);
                    break;
                }
            }
        }
    }
}