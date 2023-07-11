using UnityEngine;
namespace UnityAdvancedPlayerPrefs.Test{
    public class TestAdvancedPlayerPrefs{
        int total = 0, success = 0;
        void Assert<T>(string testName, T actual, T expected, bool expectedEqual = true){
            ++total;
            if (actual.Equals(expected) == expectedEqual){
                Debug.Log($"Test \"{testName}\" yields good result. Expected " + (expectedEqual ? "" : "not ") + $"{expected}. Got {actual}");
                ++success;
            } else {
                Debug.LogWarning($"Test \"{testName}\" yields bad result. Expected " + (expectedEqual ? "" : "not ") + $"{expected}. Got {actual}");
                throw new System.Exception("Test failed.");
            }
        }
        public void TestAll(){
            //test random prefix and password
            string prefix = TestUtils.RandomString(), password = TestUtils.RandomString();
            //Debug.Log(prefix);
            AdvancedPlayerPrefs gsm1 = new AdvancedPlayerPrefs(prefix, password);
            AdvancedPlayerPrefs gsm2 = new AdvancedPlayerPrefs(prefix, password);

            TestPrefix(gsm1.SetBool, prefix, TestUtils.GenerateBoolTestValues());
            TestPrefix(gsm1.SetColor32, prefix, TestUtils.GenerateColor32TestValues());
            TestPrefix(gsm1.SetDouble, prefix, TestUtils.GenerateDoubleTestValues());
            TestPrefix(gsm1.SetFloat, prefix, TestUtils.GenerateFloatTestValues());
            TestPrefix(gsm1.SetInt, prefix, TestUtils.GenerateIntTestValues());
            TestPrefix(gsm1.SetLong, prefix, TestUtils.GenerateLongTestValues());
            TestPrefix(gsm1.SetString, prefix, TestUtils.GenerateStringTestValues());

            TestConsistency(gsm1.SetBool, gsm2.GetBool, TestUtils.GenerateBoolTestValues());
            TestConsistency(gsm1.SetColor32, gsm2.GetColor32, TestUtils.GenerateColor32TestValues());
            TestConsistency(gsm1.SetDouble, gsm2.GetDouble, TestUtils.GenerateDoubleTestValues());
            TestConsistency(gsm1.SetFloat, gsm2.GetFloat, TestUtils.GenerateFloatTestValues());
            TestConsistency(gsm1.SetInt, gsm2.GetInt, TestUtils.GenerateIntTestValues());
            TestConsistency(gsm1.SetLong, gsm2.GetLong, TestUtils.GenerateLongTestValues());
            TestConsistency(gsm1.SetString, gsm2.GetString, TestUtils.GenerateStringTestValues());

            //Test empty key consistency
            prefix = TestUtils.RandomString();
            password = TestUtils.RandomString();
            gsm1 = new AdvancedPlayerPrefs("", password);
            TestConsistency(gsm1.SetBool, gsm1.GetBool, TestUtils.GenerateBoolTestValues());
            TestConsistency(gsm1.SetColor32, gsm1.GetColor32, TestUtils.GenerateColor32TestValues());
            TestConsistency(gsm1.SetDouble, gsm1.GetDouble, TestUtils.GenerateDoubleTestValues());
            TestConsistency(gsm1.SetFloat, gsm1.GetFloat, TestUtils.GenerateFloatTestValues());
            TestConsistency(gsm1.SetInt, gsm1.GetInt, TestUtils.GenerateIntTestValues());
            TestConsistency(gsm1.SetLong, gsm1.GetLong, TestUtils.GenerateLongTestValues());
            TestConsistency(gsm1.SetString, gsm1.GetString, TestUtils.GenerateStringTestValues());

            //Test empty password consistency
            prefix = TestUtils.RandomString();
            password = TestUtils.RandomString();
            gsm1 = new AdvancedPlayerPrefs(prefix, "");
            gsm2 = new AdvancedPlayerPrefs(prefix, "");
            TestConsistency(gsm1.SetBool, gsm2.GetBool, TestUtils.GenerateBoolTestValues());
            TestConsistency(gsm1.SetColor32, gsm2.GetColor32, TestUtils.GenerateColor32TestValues());
            TestConsistency(gsm1.SetDouble, gsm2.GetDouble, TestUtils.GenerateDoubleTestValues());
            TestConsistency(gsm1.SetFloat, gsm2.GetFloat, TestUtils.GenerateFloatTestValues());
            TestConsistency(gsm1.SetInt, gsm2.GetInt, TestUtils.GenerateIntTestValues());
            TestConsistency(gsm1.SetLong, gsm2.GetLong, TestUtils.GenerateLongTestValues());
            TestConsistency(gsm1.SetString, gsm2.GetString, TestUtils.GenerateStringTestValues());

            //test null all consistency
            gsm1 = new AdvancedPlayerPrefs(null, null);
            gsm2 = new AdvancedPlayerPrefs(null, null);
            TestConsistency(gsm1.SetBool, gsm2.GetBool, TestUtils.GenerateBoolTestValues());
            TestConsistency(gsm1.SetColor32, gsm2.GetColor32, TestUtils.GenerateColor32TestValues());
            TestConsistency(gsm1.SetDouble, gsm2.GetDouble, TestUtils.GenerateDoubleTestValues());
            TestConsistency(gsm1.SetFloat, gsm2.GetFloat, TestUtils.GenerateFloatTestValues());
            TestConsistency(gsm1.SetInt, gsm2.GetInt, TestUtils.GenerateIntTestValues());
            TestConsistency(gsm1.SetLong, gsm2.GetLong, TestUtils.GenerateLongTestValues());
            TestConsistency(gsm1.SetString, gsm2.GetString, TestUtils.GenerateStringTestValues());
            
            Debug.Log($"Test completed: {success} / {total} tests succeeded");

            //test execution time - int
            prefix = TestUtils.RandomString();
            password = TestUtils.RandomString();
            gsm1 = new AdvancedPlayerPrefs(prefix, "");
            long newIntExecTime = TestUtils.MeasureExecutionTime(() => {
                string key = TestUtils.RandomString();
                for (int i = 0; i < 1000000; ++i){
                    gsm1.SetInt(key, Random.Range(int.MinValue, int.MaxValue));
                    gsm1.GetInt(key, 0);
                }
            });
            long oldIntExecTime = TestUtils.MeasureExecutionTime(() => {
                string key = TestUtils.RandomString();
                for (int i = 0; i < 1000000; ++i){
                    PlayerPrefs.SetInt(key, Random.Range(int.MinValue, int.MaxValue));
                    PlayerPrefs.GetInt(key, 0);
                }
            });

            Debug.Log($"Int Execution Time: UnityEngine.PlayerPrefs = {oldIntExecTime}; AdvancedPlayerPrefs = {newIntExecTime}");

            //test execution time - float
            prefix = TestUtils.RandomString();
            password = TestUtils.RandomString();
            gsm1 = new AdvancedPlayerPrefs(prefix, "");
            long newFloatExecTime = TestUtils.MeasureExecutionTime(() => {
                string key = TestUtils.RandomString();
                for (int i = 0; i < 1000000; ++i){
                    gsm1.SetFloat(key, Random.Range(float.MinValue, float.MaxValue));
                    gsm1.GetFloat(key, 0f);
                }
            });
            long oldFloatExecTime = TestUtils.MeasureExecutionTime(() => {
                string key = TestUtils.RandomString();
                for (int i = 0; i < 1000000; ++i){
                    PlayerPrefs.SetFloat(key, Random.Range(float.MinValue, float.MaxValue));
                    PlayerPrefs.GetFloat(key, 0f);
                }
            });

            Debug.Log($"Float Execution Time: UnityEngine.PlayerPrefs = {oldFloatExecTime}; AdvancedPlayerPrefs = {newFloatExecTime}");

            //test execution time - string
            prefix = TestUtils.RandomString();
            password = TestUtils.RandomString();
            gsm1 = new AdvancedPlayerPrefs(prefix, "");
            long newStringExecTime = TestUtils.MeasureExecutionTime(() => {
                string key = TestUtils.RandomString();
                for (int i = 0; i < 1000000; ++i){
                    gsm1.SetString(key, TestUtils.RandomString());
                    gsm1.GetString(key, "");
                }
            });
            long oldStringExecTime = TestUtils.MeasureExecutionTime(() => {
                string key = TestUtils.RandomString();
                for (int i = 0; i < 1000000; ++i){
                    PlayerPrefs.SetString(key, TestUtils.RandomString());
                    PlayerPrefs.GetString(key, "");
                }
            });

            Debug.Log($"String Execution Time: UnityEngine.PlayerPrefs = {oldStringExecTime}; AdvancedPlayerPrefs = {newStringExecTime}");
        }
        
        delegate void SetFunction<T> (string key, T value);
        delegate T GetFunction<T> (string key, T defaultValue);
        void TestConsistency<T>(SetFunction<T> sf, GetFunction<T> gf, T[] testValues){
            string[] testKeys = TestUtils.GenerateStringTestValues();
            foreach (string key in testKeys)
            foreach (T value in testValues){
                sf.Invoke(key, value);
                Assert($"Test consistency {key} - {value}", gf.Invoke(key, default(T)), value);
            }
        }
        void TestPrefix<T>(SetFunction<T> sf, string prefix, T[] testValues){
            string[] testKeys = TestUtils.GenerateStringTestValues();
            foreach (string key in testKeys)
            foreach (T value in testValues){
                sf.Invoke(key, value);
                Assert($"Test prefix {key} - {value}", PlayerPrefs.HasKey($"{prefix}_{key}_value") && PlayerPrefs.HasKey($"{prefix}_{key}_salt") , true);
            }
        }
    }
}