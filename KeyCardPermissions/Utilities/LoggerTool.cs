﻿using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastForScps.Utilities

{
    class LoggerTool
    {
        FileStream file_stream;
        static string static_default_path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\KeyCardPermissions.log";
        string default_path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\KeyCardPermissions.log";
        private const int DefaultBufferSize = 4096;
        private const FileOptions DefaultOptions = FileOptions.Asynchronous | FileOptions.SequentialScan;
        public LoggerTool()
        {
            if (File.Exists(default_path))
            {
                File.Delete(default_path);

            }
        }

        ~LoggerTool()
        {
            if (file_stream != null)
            {
                file_stream.Close();
            }
        }

        private void AddText(string value)
        {
            using (StreamWriter file_stream_ref = File.AppendText(default_path))
            {

                file_stream_ref.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") + " : " + value);

            }

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void StaticAddText(string value)
        {
            using (StreamWriter file_stream_ref = File.AppendText(static_default_path))
            {

                file_stream_ref.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") + " : " + value);

            }

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void log_msg_static(string msg)
        {
            StaticAddText(msg);
        }

        public void log_msg(string msg)
        {
            AddText(msg);
        }

        public async Task WriteAllTextAsync(string text)
        {
            byte[] encodedText = Encoding.Unicode.GetBytes(text);

            FileStream sourceStream = new FileStream(default_path, FileMode.Append, FileAccess.Write, FileShare.None,
                DefaultBufferSize, true);
            await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            sourceStream.Close();
        }
        internal void close_log()
        {
            if (file_stream != null)
            {
                file_stream.Close();
            }
        }
    }
}
