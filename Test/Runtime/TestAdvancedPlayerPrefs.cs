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
            AdvancedPlayerPrefs prefs1 = new AdvancedPlayerPrefs(prefix, password);
            AdvancedPlayerPrefs prefs2 = new AdvancedPlayerPrefs(prefix, password);

            TestPrefix(prefs1.SetBool, prefix, TestUtils.GenerateBoolTestValues());
            TestPrefix(prefs1.SetColor32, prefix, TestUtils.GenerateColor32TestValues());
            TestPrefix(prefs1.SetDouble, prefix, TestUtils.GenerateDoubleTestValues());
            TestPrefix(prefs1.SetFloat, prefix, TestUtils.GenerateFloatTestValues());
            TestPrefix(prefs1.SetInt, prefix, TestUtils.GenerateIntTestValues());
            TestPrefix(prefs1.SetLong, prefix, TestUtils.GenerateLongTestValues());
            TestPrefix(prefs1.SetString, prefix, TestUtils.GenerateStringTestValues());

            TestConsistency(prefs1.SetBool, prefs2.GetBool, TestUtils.GenerateBoolTestValues());
            TestConsistency(prefs1.SetColor32, prefs2.GetColor32, TestUtils.GenerateColor32TestValues());
            TestConsistency(prefs1.SetDouble, prefs2.GetDouble, TestUtils.GenerateDoubleTestValues());
            TestConsistency(prefs1.SetFloat, prefs2.GetFloat, TestUtils.GenerateFloatTestValues());
            TestConsistency(prefs1.SetInt, prefs2.GetInt, TestUtils.GenerateIntTestValues());
            TestConsistency(prefs1.SetLong, prefs2.GetLong, TestUtils.GenerateLongTestValues());
            TestConsistency(prefs1.SetString, prefs2.GetString, TestUtils.GenerateStringTestValues());

            //Test empty key consistency
            prefix = TestUtils.RandomString();
            password = TestUtils.RandomString();
            prefs1 = new AdvancedPlayerPrefs("", password);
            TestConsistency(prefs1.SetBool, prefs1.GetBool, TestUtils.GenerateBoolTestValues());
            TestConsistency(prefs1.SetColor32, prefs1.GetColor32, TestUtils.GenerateColor32TestValues());
            TestConsistency(prefs1.SetDouble, prefs1.GetDouble, TestUtils.GenerateDoubleTestValues());
            TestConsistency(prefs1.SetFloat, prefs1.GetFloat, TestUtils.GenerateFloatTestValues());
            TestConsistency(prefs1.SetInt, prefs1.GetInt, TestUtils.GenerateIntTestValues());
            TestConsistency(prefs1.SetLong, prefs1.GetLong, TestUtils.GenerateLongTestValues());
            TestConsistency(prefs1.SetString, prefs1.GetString, TestUtils.GenerateStringTestValues());

            //Test empty password consistency
            prefix = TestUtils.RandomString();
            password = TestUtils.RandomString();
            prefs1 = new AdvancedPlayerPrefs(prefix, "");
            prefs2 = new AdvancedPlayerPrefs(prefix, "");
            TestConsistency(prefs1.SetBool, prefs2.GetBool, TestUtils.GenerateBoolTestValues());
            TestConsistency(prefs1.SetColor32, prefs2.GetColor32, TestUtils.GenerateColor32TestValues());
            TestConsistency(prefs1.SetDouble, prefs2.GetDouble, TestUtils.GenerateDoubleTestValues());
            TestConsistency(prefs1.SetFloat, prefs2.GetFloat, TestUtils.GenerateFloatTestValues());
            TestConsistency(prefs1.SetInt, prefs2.GetInt, TestUtils.GenerateIntTestValues());
            TestConsistency(prefs1.SetLong, prefs2.GetLong, TestUtils.GenerateLongTestValues());
            TestConsistency(prefs1.SetString, prefs2.GetString, TestUtils.GenerateStringTestValues());

            //test null all consistency
            prefs1 = new AdvancedPlayerPrefs(null, null);
            prefs2 = new AdvancedPlayerPrefs(null, null);
            TestConsistency(prefs1.SetBool, prefs2.GetBool, TestUtils.GenerateBoolTestValues());
            TestConsistency(prefs1.SetColor32, prefs2.GetColor32, TestUtils.GenerateColor32TestValues());
            TestConsistency(prefs1.SetDouble, prefs2.GetDouble, TestUtils.GenerateDoubleTestValues());
            TestConsistency(prefs1.SetFloat, prefs2.GetFloat, TestUtils.GenerateFloatTestValues());
            TestConsistency(prefs1.SetInt, prefs2.GetInt, TestUtils.GenerateIntTestValues());
            TestConsistency(prefs1.SetLong, prefs2.GetLong, TestUtils.GenerateLongTestValues());
            TestConsistency(prefs1.SetString, prefs2.GetString, TestUtils.GenerateStringTestValues());
            
            Debug.Log($"Test completed: {success} / {total} tests succeeded");

            //test execution time - int
            prefix = TestUtils.RandomString();
            password = TestUtils.RandomString();
            prefs1 = new AdvancedPlayerPrefs(prefix, "");
            long newIntExecTime = TestUtils.MeasureExecutionTime(() => {
                string key = TestUtils.RandomString();
                for (int i = 0; i < 1000000; ++i){
                    prefs1.SetInt(key, Random.Range(int.MinValue, int.MaxValue));
                    prefs1.GetInt(key, 0);
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
            prefs1 = new AdvancedPlayerPrefs(prefix, "");
            long newFloatExecTime = TestUtils.MeasureExecutionTime(() => {
                string key = TestUtils.RandomString();
                for (int i = 0; i < 1000000; ++i){
                    prefs1.SetFloat(key, Random.Range(float.MinValue, float.MaxValue));
                    prefs1.GetFloat(key, 0f);
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
            prefs1 = new AdvancedPlayerPrefs(prefix, "");
            long newStringExecTime = TestUtils.MeasureExecutionTime(() => {
                string key = TestUtils.RandomString();
                for (int i = 0; i < 1000000; ++i){
                    prefs1.SetString(key, TestUtils.RandomString());
                    prefs1.GetString(key, "");
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