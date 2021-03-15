using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Net;
using System.Diagnostics;
using System.Reflection;

namespace WebTrashCheck
{
    public class LogWorker
    {
        const long MaxFileSize = 30000;
        const char indexDevider = '_';
        string _filePath = @"\\172.16.7.45\shu\Application Files\Logs\";//@"\\172.16.2.99\logs\Service-Helper\";
        string _fileMask;
        string _ip;

        public LogWorker()
        {
            DateTime date = DateTime.Now;
            _fileMask = string.Format("{0}{1}{2}", date.Year, date.Month, date.Day);
            _ip = GetIPAddress();

            if (!Directory.Exists(_filePath))
                //CreateDirectoriesToFile();

            if (IsNeedCreateNewFile())
                CreateLogFile();
        }

       public void TypeInLogFile(string text, StackTrace trace, string whoDid)
        {
            if (IsNeedCreateNewFile())
                CreateLogFile();
            string[] fileNames = Directory.GetFiles(_filePath, string.Format("{0}*", _fileMask));
            string lastFile = string.Format("{0}{1}{2}{3}.log", _filePath, _fileMask, indexDevider, GetLastIndexFile());
            
            string traceStr = GetStringOfFrames(trace.GetFrames());
            using (System.IO.StreamWriter sw = File.AppendText(lastFile))
            {
                DateTime date = DateTime.Now;
                sw.WriteLine(string.Format("{0} {5} {4}: {1}: {2} {3}", date.ToString("HH:mm:ss"), LogStatus.INFO, traceStr, text, whoDid, _ip));
            }
        }

        public void TypeInLogFile(string text, StackTrace trace, LogStatus status, string whoDid)
        {
            if (IsNeedCreateNewFile())
                CreateLogFile();
            string[] fileNames = Directory.GetFiles(_filePath, string.Format("{0}*", _fileMask));
            string lastFile = string.Format("{0}{1}{2}{3}.log", _filePath, _fileMask, indexDevider, GetLastIndexFile());

            string traceStr = GetStringOfFrames(trace.GetFrames());
            using (System.IO.StreamWriter sw = File.AppendText(lastFile))
            {
                DateTime date = DateTime.Now;
                sw.WriteLine(string.Format("{0} {5} {4}: {1}: {2} {3} ", date.ToString("HH:mm:ss"), status, traceStr, text, whoDid, _ip));
            }
        }

        public void TypeInLogFile(string text, LogStatus status, string whoDid)
        {
            if (IsNeedCreateNewFile())
                CreateLogFile();
            string[] fileNames = Directory.GetFiles(_filePath, string.Format("{0}*", _fileMask));
            string lastFile = string.Format("{0}{1}{2}{3}.log", _filePath, _fileMask, indexDevider, GetLastIndexFile());
            using (System.IO.StreamWriter sw = File.AppendText(lastFile))
            {
                DateTime date = DateTime.Now;
                sw.WriteLine(string.Format("{0} {4} {3}: {1}: {2} ", date.ToString("HH:mm:ss"), status, text, whoDid, _ip));
            }
        }

        public void TypeInLogFile(string text, string whoDid)
        {
            if (IsNeedCreateNewFile())
                CreateLogFile();
            string[] fileNames = Directory.GetFiles(_filePath, string.Format("{0}*", _fileMask));
            string lastFile = string.Format("{0}{1}{2}{3}.log", _filePath, _fileMask, indexDevider, GetLastIndexFile());
            using (System.IO.StreamWriter sw = File.AppendText(lastFile))
            {
                DateTime date = DateTime.Now;
                sw.WriteLine(string.Format("{0} {4} {3}: {1}: {2}", date.ToString("HH:mm:ss"), LogStatus.INFO, text, whoDid, _ip));
            }
        }

        private string GetStringOfFrames(StackFrame[] frames)
        {
            StringBuilder sb = new StringBuilder(string.Empty);
            sb.Append("[");
            sb.Append("Called class:");
            for (int frameIndex = 0; frameIndex < frames.Length; frameIndex++)
            {
                if (frameIndex == 0)
                {
                    MethodBase mb = frames[frameIndex].GetMethod();
                    sb.Append(mb.DeclaringType.FullName);
                    sb.Append(" Frames:");
                }
                sb.Append(string.Format(" {0}", frames[frameIndex].GetMethod().Name));
                if (frameIndex < frames.Length - 1)
                {
                    sb.Append(';');
                }
            }
            sb.Append("]");
            return sb.ToString();
        }

        /// <summary>
        /// Check is path correct
        /// </summary>
        /// <param name="filePath">Example: C:\Test\LogDirecotry\qwe.log</param>
        /// <returns></returns>
        private bool IsPathCorrect(string filePath)
        {
            string[] directories = filePath.Split('\\');
            string lastDir = directories[directories.Length - 1];
            if (lastDir.Contains('.'))
                return false;
            else if (filePath[filePath.Length - 1] != '\\')
                return false;
            return true;
        }

        private void CreateLogFile()
        {
            int fileIndex = GetLastIndexFile() + 1;
            DateTime date = DateTime.Now;
            string fileName = string.Format("{0}{1}", _filePath, _fileMask);
            fileName = string.Format("{0}{1}{2}.log", fileName, indexDevider, fileIndex);
            var myFile = File.Create(fileName);
            myFile.Close();
        }

        private int GetLastIndexFile()
        {
            int lastIndex = 0;
            int logIndex = 0;
            string[] fileNames = Directory.GetFiles(_filePath, string.Format("{0}*", _fileMask));

            for (int fileNameIndex = 0; fileNameIndex < fileNames.Length; fileNameIndex++)
            {
                logIndex = GetLogIndexFromName(fileNames[fileNameIndex]);
                if (lastIndex < logIndex)
                    lastIndex = logIndex;
            }
            return lastIndex;
        }

        private int GetLogIndexFromName(string fileName)
        {
            string[] directories = fileName.Split('\\');
            string file = directories[directories.Length - 1];
            string indexStr = file.Substring(_fileMask.Length + 1);

            string index = string.Empty;
            for (int i = 0; i < indexStr.Length; i++)
            {
                if (indexStr[i] == '.')
                {
                    break;
                }
                index += indexStr[i];
            }
            return int.Parse(index);
        }

        private bool IsNeedCreateNewFile()
        {
            bool isNeed = false;
            string[] fileNames = Directory.GetFiles(_filePath, string.Format("{0}*", _fileMask));
            Array.Sort(fileNames);

            if (fileNames.Length > 0)
            {
                string lastFile = string.Format("{0}{1}{2}{3}.log", _filePath, _fileMask, indexDevider, GetLastIndexFile());
                FileInfo fi = new FileInfo(lastFile);
                long lastFileSize = fi.Length;

                if (lastFileSize > MaxFileSize)
                    isNeed = true;
            }
            else if (fileNames.Length <= 0)
                isNeed = true;
            return isNeed;
        }

        private string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
    }
    public enum LogStatus
    {
        ERROR,
        WARNING,
        INFO
    }
}