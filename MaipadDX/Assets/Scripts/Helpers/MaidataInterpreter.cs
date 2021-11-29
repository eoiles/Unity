using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Structures;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

namespace Helpers
{
    public class MaidataInterpreter : MonoBehaviour
    {
        /*
         * &title=曲名
         * &freemsg=曲目作者信息
         * &wholebpm=整体bpm
         * &first=
         * &first_1=
         * Easy难度中,从音频开始到第一个note 打下去的时间。可精确到0.001 秒
         * &first_2=
         * Basic难度中,从音频开始到第一个note 打下去的时间。可精确到0.001 秒
         * &first_3=
         * Advanced难度中,从音频开始到第一个note 打下去的时间。可精确到0.001 秒
         * &first_4=
         * Expert难度中,从音频开始到第一个note 打下去的时间。可精确到0.001 秒
         * &first_5=
         * Master难度中,从音频开始到第一个note 打下去的时间。可精确到0.001 秒
         * &first_6=
         * ReMaster难度中,从音频开始到第一个note 打下去的时间。可精确到0.001 秒
         * &amsg_first=
         * &tap_ofs=
         * &hold_ofs=
         * &slide_ofs=
         * &break_ofs=
         * &lv_1=
         * Easy难度的等级
         * &des_1=
         * Easy难度的谱面制作人
         * &lv_2=
         * Basic难度的等级
         * &des_2=
         * Basic难度的谱面制作人
         * &lv_3=
         * Advanced难度的等级
         * &des_3=
         * Advanced难度的谱面制作人
         * &lv_4=
         * Expert难度的等级
         * &des_4=
         * Expert难度的谱面制作人
         * &lv_5=
         * Master难度的等级
         * &des_5=
         * Master难度的谱面制作人 
         * &lv_6=
         * ReMaster难度的等级 
         * &des_6=
         * ReMaster难度的谱面制作人
         * &inote_1=
         * 这里开始写Easy难度的谱面
         * &inote_2=
         * 这里开始写Basic难度的谱面
         * &inote_3=
         * 这里开始写Advanced难度的谱面
         * &inote_4=
         * 这里开始写Expert难度的谱面
         * &inote_5=
         * 这里开始写Master难度的谱面 
         * &inote_6=
         * 这里开始写ReMaster难度的谱面
         * &amsg_time=
         * &amsg_content= 
         */

        public static Maidata LoadSong(DirectoryInfo fullDirectoryName,
            out string musicPath)
        {
            var files = fullDirectoryName.GetFiles();

            var returnData = new Maidata();
            musicPath = "";

            foreach (var file in files)
            {
                if (file.Name.StartsWith("MaiData"))
                {
                    returnData = ReadMaiData(File.ReadAllText(file.FullName));
                }

                if (file.Name.StartsWith("Track"))
                {
                    musicPath = file.FullName;
                }
            }

            return returnData;
        }

        public static Maidata ReadMaiData(string rawData)
        {
            var parameters = rawData.Remove(0,
                    1)
                .Split('&');

            var returnData = new Maidata();

            foreach (var parameter in parameters)
            {
                var title = parameter.Substring(0,
                    parameter.IndexOf('='));
                var value = parameter.Substring(parameter.IndexOf('=') + 1);

                switch (title)
                {
                    case "title":
                        returnData.title = value;
                        break;

                    case "freemsg":
                    case "artist":
                        returnData.artist = value;
                        break;
                    case "wholebpm":
                        returnData.wholebpm = value;
                        break;

                    case "first":
                        returnData.first = float.Parse(value);
                        break;
                    case "first_1":
                        returnData.first1 = float.Parse(value);
                        break;
                    case "first_2":
                        returnData.first2 = float.Parse(value);
                        break;
                    case "first_3":
                        returnData.first3 = float.Parse(value);
                        break;
                    case "first_4":
                        returnData.first4 = float.Parse(value);
                        break;
                    case "first_5":
                        returnData.first5 = float.Parse(value);
                        break;
                    case "first_6":
                        returnData.first6 = float.Parse(value);
                        break;

                    case "amsg_first":
                        returnData.amsgFirst = value;
                        break;

                    case "tap_ofs":
                        returnData.tapofs = float.Parse(value);
                        break;
                    case "hold_ofs":
                        returnData.holdofs = float.Parse(value);
                        break;
                    case "slide_ofs":
                        returnData.slideofs = float.Parse(value);
                        break;
                    case "break_ofs":
                        returnData.breakofs = float.Parse(value);
                        break;

                    case "lv_1":
                        returnData.easy.level = value;
                        break;
                    case "des_1":
                        returnData.easy.description = value;
                        break;
                    case "lv_2":
                        returnData.basic.level = value;
                        break;
                    case "des_2":
                        returnData.basic.description = value;
                        break;
                    case "lv_3":
                        returnData.advanced.level = value;
                        break;
                    case "des_3":
                        returnData.advanced.description = value;
                        break;
                    case "lv_4":
                        returnData.expert.level = value;
                        break;
                    case "des_4":
                        returnData.expert.description = value;
                        break;
                    case "lv_5":
                        returnData.master.level = value;
                        break;
                    case "des_5":
                        returnData.master.description = value;
                        break;
                    case "lv_6":
                        returnData.remaster.level = value;
                        break;
                    case "des_6":
                        returnData.remaster.description = value;
                        break;

                    case "inote_1":
                        ProcessChart(value,
                            returnData.easy);
                        break;
                    case "inote_2":
                        ProcessChart(value,
                            returnData.basic);
                        break;
                    case "inote_3":
                        ProcessChart(value,
                            returnData.advanced);
                        break;
                    case "inote_4":
                        ProcessChart(value,
                            returnData.expert);
                        break;
                    case "inote_5":
                        ProcessChart(value,
                            returnData.master);
                        break;
                    case "inote_6":
                        ProcessChart(value,
                            returnData.remaster);
                        break;

                    case "amsg_time":
                        returnData.amsgTime = float.Parse(value);
                        break;
                    case "amsg_content":
                        returnData.amsgContent = value;
                        break;
                }
            }

            return returnData;
        }

        public static void ProcessChart(string raw,
            Maidata.Difficulty diff)
        {
            raw = RemoveLineEndings(raw);
            diff.rawChart = raw;
            diff.chart = new PadderData();

            float time = 0;
            var readMode = ReadMode.None;
            var sb = new StringBuilder();

            var arr = raw.ToCharArray();

            var elements = new Dictionary<float, string>();

            foreach (var c in arr)
            {
                switch (c)
                {
                    case '(':
                        readMode = ReadMode.Tempo;
                        continue;
                    case '{':
                        readMode = ReadMode.Signature;
                        continue;
                    case ')' when readMode == ReadMode.Tempo:
                    {
                        diff.chart.tempoList.Add(time,
                            float.Parse(sb.ToString()));
                        sb.Clear();
                        readMode = ReadMode.None;
                        continue;
                    }
                    case '}' when readMode == ReadMode.Signature:
                    {
                        var spb = 60f /
                                  diff.chart.tempoList.Last()
                                      .Value;
                        var normalizedSignature = float.Parse(sb.ToString()) / 4f;
                        diff.chart.signatureList.Add(time,
                            spb / normalizedSignature);
                        sb.Clear();
                        readMode = ReadMode.None;
                        continue;
                    }
                    case ',':
                        if (sb.Length > 0)
                        {
                            elements.Add(time,
                                sb.ToString());
                            sb.Clear();
                        }

                        time += diff.chart.signatureList.Last()
                            .Value;
                        continue;
                }

                sb.Append(c);
            }

            foreach (var element in elements)
            {
                var queue = new Queue<char>(element.Value);

                if (element.Value.Count(char.IsNumber) > 1)
                {
                    for (int i = 0;
                        i < element.Value.Length;
                        i++)
                    {
                        var notation = queue.Dequeue();

                        if (!char.IsNumber(notation)) continue;

                        AppendNote(element.Key, notation, diff.chart, queue.Count > 0 ? queue.Peek() : ' ',
                            PadderData.Type.Each);
                    }
                }
                else if (element.Value.All(c => char.IsNumber(c) || c == 'b'))
                {
                    for (int i = 0;
                        i < element.Value.Length;
                        i++)
                    {
                        var notation = queue.Dequeue();

                        if (!char.IsNumber(notation)) continue;

                        AppendNote(element.Key, notation, diff.chart, queue.Count > 0 ? queue.Peek() : ' ');
                    }
                }
            }
        }

        private static void AppendNote(float time, char notation, PadderData chart,
            char peek = ' ', PadderData.Type forceType = PadderData.Type.Normal)
        {
            switch (notation)
            {
                case '1':
                    chart.notesBt1.Add(new PadderData.Tap(time,
                        peek == 'b'
                            ? PadderData.Type.Break
                            : forceType));
                    break;
                case '2':
                    chart.notesBt2.Add(new PadderData.Tap(time,
                        peek == 'b'
                            ? PadderData.Type.Break
                            : forceType));
                    break;
                case '3':
                    chart.notesBt3.Add(new PadderData.Tap(time,
                        peek == 'b'
                            ? PadderData.Type.Break
                            : forceType));
                    break;
                case '4':
                    chart.notesBt4.Add(new PadderData.Tap(time,
                        peek == 'b'
                            ? PadderData.Type.Break
                            : forceType));
                    break;
                case '5':
                    chart.notesBt5.Add(new PadderData.Tap(time,
                        peek == 'b'
                            ? PadderData.Type.Break
                            : forceType));
                    break;
                case '6':
                    chart.notesBt6.Add(new PadderData.Tap(time,
                        peek == 'b'
                            ? PadderData.Type.Break
                            : forceType));
                    break;
                case '7':
                    chart.notesBt7.Add(new PadderData.Tap(time,
                        peek == 'b'
                            ? PadderData.Type.Break
                            : forceType));
                    break;
                case '8':
                    chart.notesBt8.Add(new PadderData.Tap(time,
                        peek == 'b'
                            ? PadderData.Type.Break
                            : forceType));
                    break;
            }
        }

        private enum ReadMode
        {
            None,
            Tempo,
            Signature
        }

        private static string RemoveLineEndings(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return value;
            }

            string lineSeparator = ((char) 0x2028).ToString();
            string paragraphSeparator = ((char) 0x2029).ToString();

            return value.Replace("\r\n",
                    string.Empty)
                .Replace("\n",
                    string.Empty)
                .Replace("\r",
                    string.Empty)
                .Replace(lineSeparator,
                    string.Empty)
                .Replace(paragraphSeparator,
                    string.Empty);
        }
    }
}