using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Tests.Helper
{
    public class DataLoader
    {
        public static string LoadFileText(string path, string testData = "")
        {
            var dataDir = Directory.GetCurrentDirectory() + rootPath;
            var filePath = dataDir + path + testData + ".txt";

            if (File.Exists(filePath))
            {
                var fileContents = File.ReadAllText(filePath);
                return fileContents;
            }
            else
            {
                var msg = $"File Data/{path}{testData}.txt doesn't exist";
                Console.WriteLine(msg);
                throw new System.Exception(msg);
            }
        }
        public static Dictionary<string, t> loadJsonDictonary<t>(string path, string testData = "")
        {
            var dataDir = Directory.GetCurrentDirectory() + rootPath;
            if (string.IsNullOrWhiteSpace(testData))
            {
                // set equal to t type
                testData = typeof(t).Name.ToString();
            }

            if (File.Exists(dataDir + path + testData + ".json"))
            {
                var fileContents = File.ReadAllText(dataDir + path + testData + ".json");
                var result = JsonConvert.DeserializeObject<Dictionary<string, t>>(fileContents);
                return result;
            }
            else
            {
                var msg = "File Data/" + path + testData + ".json doesn't exist";
                Console.WriteLine(msg);
                throw new System.Exception(msg);
            }
        }
        /// <summary>
        /// initialize a json list, helper method to create mock data quickly
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void initJson<T>(List<T> data = null, string path = "Repository/", string testData = "") where T : class, new()
        {
            var dataDir = Directory.GetCurrentDirectory() + rootPath;
            if (string.IsNullOrWhiteSpace(testData))
            {
                // set equal to t type
                testData = typeof(T).Name.ToString();
            }
            if (data == null)
            {
                data = new List<T>() { new T() { } };
            }
            if (!File.Exists(dataDir + path + testData + ".json"))
            {
                // should serialize without null
                File.WriteAllText(dataDir + path + testData + ".json",
                    JsonConvert.SerializeObject(data
                        // ,Newtonsoft.Json.Formatting.None,
                        //     new JsonSerializerSettings
                        //     {
                        //         NullValueHandling = NullValueHandling.Ignore
                        //     }
                        )
                );
            }
        }

        public const string rootPath = "/../../../Data/";

        /// <summary>
        /// used for skipped tests
        /// </summary>
        /// <param name="path"></param>
        /// <param name="testData"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string DataExists<T>(string path = "Repository/", string testData = "")
        {
            var dataDir = Directory.GetCurrentDirectory() + rootPath;
            if (string.IsNullOrWhiteSpace(testData))
            {
                // set equal to t type
                testData = typeof(T).Name.ToString();
            }

            var filePath = dataDir + path + testData + ".json";

            if (File.Exists(filePath))
            {
                return true.ToString();
            }
            return filePath;
        }

        /// <summary>
        /// parse through a json array into a c# list
        /// </summary>
        /// <param name="path"></param>
        /// <param name="testData"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> loadJsonArray<T>(string path = "Repository/", string testData = "") where T : class, new()
        {
            var dataDir = Directory.GetCurrentDirectory() + rootPath;
            if (string.IsNullOrWhiteSpace(testData))
            {
                // set equal to t type
                testData = typeof(T).Name.ToString();
            }

            if (File.Exists(dataDir + path + testData + ".json"))
            {
                var fileContents = File.ReadAllText(dataDir + path + testData + ".json");
                var result = JsonConvert.DeserializeObject<List<T>>(fileContents);
                return result;
            }
            else
            {
                // auto create a default
                initJson<T>(path: path, testData: testData);
                var msg = "File Data/" + path + testData + ".json doesn't exist";
                Console.WriteLine(msg);
                throw new System.Exception(msg);
            }
        }
    }
}