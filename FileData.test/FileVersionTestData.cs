using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FileData.test
{
    public class FileVersionTestData
    {

        public static IEnumerable FileDetailsTestCases { get {
                yield return new TestCaseData(@"-v", @"c:/test.txt");
                yield return new TestCaseData(@"--v", @"c:/test.txt");
                yield return new TestCaseData(@"/v", @"c:/test.txt");
                yield return new TestCaseData(@"--version", @"c:/test.txt");
                yield return new TestCaseData(@"-s", @"c:/test.txt");
                yield return new TestCaseData(@"--s", @"c:/test.txt");
                yield return new TestCaseData(@"/s", @"c:/test.txt");
                yield return new TestCaseData(@"--size", @"c:/test.txt");
 
            }
        }

        public static IEnumerable FileVersionCommandNullTestCases
        {
            get
            {
                yield return new TestCaseData(null, @"c:/test.txt");
                yield return new TestCaseData(string.Empty, @"c:/test.txt");
                yield return new TestCaseData(string.Empty, string.Empty);
            }
        }

        public static IEnumerable FileVersionFileLocationNullTestCases
        {
            get
            {
                yield return new TestCaseData(@"-v", null);
                yield return new TestCaseData(@"-v", string.Empty);
            }
        }

    }
}
