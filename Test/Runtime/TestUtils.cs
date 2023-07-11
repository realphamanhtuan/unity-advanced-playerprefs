using UnityEngine;
using System.Collections.Generic;
namespace UnityAdvancedPlayerPrefs.Test{
    public static class TestUtils{
        public static string RandomString(int length = 16){
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(chars[Random.Range(0, chars.Length)]);
            }

            return stringBuilder.ToString();
        }
        public static string[] GenerateStringTestValues(){
            return new string[] {
                string.Empty,
                "a",
                "abc",
                "__",
                "123",
                "!@#",
                ".",
                TestUtils.RandomString()
            };
        }
        public static bool[] GenerateBoolTestValues(){
            return new bool[] {
                true,
                false
            };
        }
        public static Color32[] GenerateColor32TestValues(){
            return new Color32[] {
                new Color32(0, 0, 0, 0),
                new Color32(255, 255, 255, 255),
                new Color32(100, 100, 100, 100),
                new Color32(0, 255, 255, 255),
                new Color32(0, 128, 128, 128),
                new Color32(128, 0, 128, 128),
                new Color32(255, 0, 255, 255),
                new Color32((byte) Random.Range(0, 256), (byte) Random.Range(0, 256), (byte) Random.Range(0, 256), (byte) Random.Range(0, 256))
            };
        }
        public static double[] GenerateDoubleTestValues(){
            return new double[]{
                -1.0,
                double.NegativeInfinity,
                double.PositiveInfinity,
                double.MaxValue,
                double.MinValue,
                0.0,
                1.0,
                -2147483649.5,
                2147483648.0
            };
        }
        public static float[] GenerateFloatTestValues(){
            return new float[]{
                -1f,
                float.NegativeInfinity,
                float.PositiveInfinity,
                float.MaxValue,
                float.MinValue,
                0f,
                1f,
                -2147483640f,
                214f,
                2738498f,
                Random.Range(-2147483647f, 2147483647f),
                Random.Range(float.MinValue, float.MaxValue)
            };
        }
        public static int[] GenerateIntTestValues(){
            return new int[]{
                0,
                -1,
                1,
                int.MaxValue,
                int.MinValue,
                -65537,
                65537,
                Random.Range(int.MinValue, int.MaxValue)
            };
        }
        public static long[] GenerateLongTestValues(){
            return new long[] {
                0L,
                1L,
                -1L,
                long.MaxValue,
                long.MinValue,
                2147483648,
                -2147483648,
            };
        }
        public static Texture2D[] GenerateTexture2DTestValues(){
            List<Texture2D> result = new List<Texture2D>();
            Texture2D temp;

            temp = new Texture2D(1024, 1024, TextureFormat.RGBA32, true);
            for (int i = 0; i < temp.width; ++i)
            for (int j = 0; j < temp.height; ++j)
                temp.SetPixel(i, j, Color.white);
            temp.Apply();
            result.Add(temp);

            temp = new Texture2D(1024, 1024, TextureFormat.RGBA32, true);
            for (int i = 0; i < temp.width; ++i)
            for (int j = 0; j < temp.height; ++j)
                temp.SetPixel(i, j, Color.black);
            temp.Apply();
            result.Add(temp);

            return result.ToArray();
        }
        public static long MeasureExecutionTime(System.Action action){
            var watch = System.Diagnostics.Stopwatch.StartNew();
            action?.Invoke();
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }
    }
}