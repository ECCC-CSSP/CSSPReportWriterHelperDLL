using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSSPReportWriterHelperDLL.Tests.SetupInfo;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using CSSPModelsDLL.Models;
using CSSPReportWriterHelperDLL.Services;
using System.IO;
using CSSPReportWriterHelperDLL.Services.Resources;
using CSSPEnumsDLL.Enums;
using System.Linq;
using System.Reflection;
using CSSPEnumsDLL.Services;

namespace CSSPReportWriterHelperDLL.Tests.Services
{
    /// <summary>
    /// Summary description for BaseServiceTest
    /// </summary>
    [TestClass]
    public class ReportBaseCSVServiceTest : SetupData
    {
        #region Variables
        private TestContext testContextInstance;
        private SetupData setupData;
        private string Marker = "|||";

        #endregion Variables

        #region Properties
        public ReportBaseService reportBaseService { get; set; }
        public TreeView treeViewCSSP { get; set; }
        public RichTextBox richTextBoxTreeViewSelectedStatus { get; set; }
        public ReportTreeNode reportTreeNodeRoot { get; set; }
        public BaseEnumService baseEnumService { get; set; }
        public ReportBase reportBase { get; set; }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        #endregion Properties

        #region Constructors
        public ReportBaseCSVServiceTest()
        {
            setupData = new SetupData();
        }
        #endregion Constructors

        #region Testing Functions public
        [TestMethod]
        public void ReportBaseService_CheckNumberOfVariableOnFirstLineIsEqualToMaximumNumberOfVariablesCSV_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Name,Lat,Lng");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine("Root_Lat");
                sbFileText.AppendLine("Root_Lng");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.CheckTagsAndContentOKCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                Assert.AreEqual(sbFileText.Length, InitialLength);
            }
        }
        [TestMethod]
        public void ReportBaseService_CheckNumberOfVariableOnFirstLineIsEqualToMaximumNumberOfVariablesCSV_2_levels_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Name,Lat,Lng,Country,Lat2,Lng2");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine("Root_Lat");
                sbFileText.AppendLine("Root_Lng");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Country_Name");
                sbFileText.AppendLine("Country_Lat");
                sbFileText.AppendLine("Country_Lng");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.CheckTagsAndContentOKCSV(sbFileText, reportTagList, StartTVItemID, 10000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                Assert.AreEqual(sbFileText.Length, InitialLength);
            }
        }
        [TestMethod]
        public void ReportBaseService_CheckNumberOfVariableOnFirstLineIsEqualToMaximumNumberOfVariablesCSV_3_levels_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Name,Lat,Lng,Country,Lat2,Lng2,Province,Lat3,Lng3");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine("Root_Lat");
                sbFileText.AppendLine("Root_Lng");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Country_Name");
                sbFileText.AppendLine("Country_Lat");
                sbFileText.AppendLine("Country_Lng");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopStart Province " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Province_Name");
                sbFileText.AppendLine("Province_Lat");
                sbFileText.AppendLine("Province_Lng");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopEnd Province " + culture.TwoLetterISOLanguageName + Marker);
                sbFileText.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.CheckTagsAndContentOKCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                Assert.AreEqual(sbFileText.Length, InitialLength);
            }
        }
        [TestMethod]
        public void ReportBaseService_CheckNumberOfVariableOnFirstLineIsEqualToMaximumNumberOfVariablesCSV_4_levels_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Name,Lat,Lng,Country,Lat2,Lng2,Province,Lat3,Lng3,Area,Lat4,Lng4");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine("Root_Lat");
                sbFileText.AppendLine("Root_Lng");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Country_Name");
                sbFileText.AppendLine("Country_Lat");
                sbFileText.AppendLine("Country_Lng");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopStart Province " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Province_Name");
                sbFileText.AppendLine("Province_Lat");
                sbFileText.AppendLine("Province_Lng");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopStart Area " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Area_Name_Short");
                sbFileText.AppendLine("Area_Lat");
                sbFileText.AppendLine("Area_Lng");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopEnd Area " + culture.TwoLetterISOLanguageName + Marker);
                sbFileText.AppendLine(Marker + "LoopEnd Province " + culture.TwoLetterISOLanguageName + Marker);
                sbFileText.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.CheckTagsAndContentOKCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                Assert.AreEqual(sbFileText.Length, InitialLength);
            }
        }
        [TestMethod]
        public void ReportBaseService_CheckNumberOfVariableOnFirstLineIsEqualToMaximumNumberOfVariablesCSV_FirstLineMissingVariable_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Name,Lat");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine("Root_Lat");
                sbFileText.AppendLine("Root_Lng");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.CheckTagsAndContentOKCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(sbFileText.ToString().Contains("\r\n\r\n" + string.Format(ReportServiceRes.FirstLineVariableCount_DoesNotEqualToResultCount_, 2, 3) + "\r\n\r\n"));
                }

                Assert.AreEqual(sbFileText.Length, InitialLength + ("\r\n\r\n" + string.Format(ReportServiceRes.FirstLineVariableCount_DoesNotEqualToResultCount_, 2, 3) + "\r\n\r\n").Length);
            }
        }
        [TestMethod]
        public void ReportBaseService_CheckNumberOfVariableOnFirstLineIsEqualToMaximumNumberOfVariablesCSV_FirstLineMissingVariable_level_2_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Name,Lat,Lng,Country,Lat2");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine("Root_Lat");
                sbFileText.AppendLine("Root_Lng");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Country_Name");
                sbFileText.AppendLine("Country_Lat");
                sbFileText.AppendLine("Country_Lng");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.CheckTagsAndContentOKCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(sbFileText.ToString().Contains("\r\n\r\n" + string.Format(ReportServiceRes.FirstLineVariableCount_DoesNotEqualToResultCount_, 5, 6) + "\r\n\r\n"));
                }

                Assert.AreEqual(sbFileText.Length, InitialLength + ("\r\n\r\n" + string.Format(ReportServiceRes.FirstLineVariableCount_DoesNotEqualToResultCount_, 5, 6) + "\r\n\r\n").Length);
            }
        }
        [TestMethod]
        public void ReportBaseService_CheckNumberOfVariableOnFirstLineIsEqualToMaximumNumberOfVariablesCSV_FirstLineTooManyVariable_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Name,Lat,Lng,Country");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine("Root_Lat");
                sbFileText.AppendLine("Root_Lng");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.CheckTagsAndContentOKCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(sbFileText.ToString().Contains("\r\n\r\n" + string.Format(ReportServiceRes.FirstLineVariableCount_DoesNotEqualToResultCount_, 4, 3) + "\r\n\r\n"));
                }

                Assert.AreEqual(sbFileText.Length, InitialLength + ("\r\n\r\n" + string.Format(ReportServiceRes.FirstLineVariableCount_DoesNotEqualToResultCount_, 4, 3) + "\r\n\r\n").Length);
            }
        }
        [TestMethod]
        public void ReportBaseService_CheckNumberOfVariableOnFirstLineIsEqualToMaximumNumberOfVariablesCSV_FirstLineTooManyVariable_level_2_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Name,Lat,Lng,Country,Lat2,Lng2,Not");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine("Root_Lat");
                sbFileText.AppendLine("Root_Lng");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Country_Name");
                sbFileText.AppendLine("Country_Lat");
                sbFileText.AppendLine("Country_Lng");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.CheckTagsAndContentOKCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(sbFileText.ToString().Contains("\r\n\r\n" + string.Format(ReportServiceRes.FirstLineVariableCount_DoesNotEqualToResultCount_, 7, 6) + "\r\n\r\n"));
                }

                Assert.AreEqual(sbFileText.Length, InitialLength + ("\r\n\r\n" + string.Format(ReportServiceRes.FirstLineVariableCount_DoesNotEqualToResultCount_, 7, 6) + "\r\n\r\n").Length);
            }
        }
        [TestMethod]
        public void ReportBaseService_CheckTagsAndContentOKCSV_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Name,Lat,Lng");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine("Root_Lat");
                sbFileText.AppendLine("Root_Lng");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.CheckTagsAndContentOKCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                Assert.AreEqual(sbFileText.Length, InitialLength);
            }
        }
        [TestMethod]
        public void ReportBaseService_CheckTagsFirstLineCSV_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Name,Lat,Lng");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine("Root_Lat");
                sbFileText.AppendLine("Root_Lng");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                retStr = reportBaseService.CheckTagsFirstLineCSV(sbFileText, reportTagList);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                Assert.AreEqual(sbFileText.Length, InitialLength);
            }
        }
        [TestMethod]
        public void ReportBaseService_CheckTagsFirstLineCSV_CouldNotFindFirstLineReturn_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Name,Lat,Lng");
                sbFileText.Append(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.Append("Root_Name");
                sbFileText.Append("Root_Lat");
                sbFileText.Append("Root_Lng");
                sbFileText.Append(Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                retStr = reportBaseService.CheckTagsFirstLineCSV(sbFileText, reportTagList);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(retStr.Contains("\r\n\r\n" + ReportServiceRes.CouldNotFindFirstLineReturnsAreRequired + "\r\n\r\n"));
                }

                Assert.AreEqual(sbFileText.Length, InitialLength + ("\r\n\r\n" + ReportServiceRes.CouldNotFindFirstLineReturnsAreRequired + "\r\n\r\n").Length);
            }
        }
        [TestMethod]
        public void ReportBaseService_CheckTagsFirstLineCSV_DoesNotContain3Items_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Name,Lat,Lng");
                sbFileText.AppendLine(Marker + "Start Root "); // + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine("Root_Lat");
                sbFileText.AppendLine("Root_Lng");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                retStr = reportBaseService.CheckTagsFirstLineCSV(sbFileText, reportTagList);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(sbFileText.ToString().Contains("\r\n\r\n" + string.Format(ReportServiceRes._DoesNotContain_Items, Marker + "Start Root ", 3.ToString()) + "\r\n\r\n" +
                        string.Format(ReportServiceRes.Example_, Marker + "Start Root en") + "\r\n\r\n"));
                }

                Assert.AreEqual(sbFileText.Length, InitialLength + ("\r\n\r\n" + string.Format(ReportServiceRes._DoesNotContain_Items, Marker + "Start Root ", 3.ToString()) + "\r\n\r\n" +
                        string.Format(ReportServiceRes.Example_, Marker + "Start Root en") + "\r\n\r\n").Length);
            }
        }
        [TestMethod]
        public void ReportBaseService_CheckTagsFirstLineCSV_DoesNotContain3Items2_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Name,Lat,Lng");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName + " Bon");
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine("Root_Lat");
                sbFileText.AppendLine("Root_Lng");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                retStr = reportBaseService.CheckTagsFirstLineCSV(sbFileText, reportTagList);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(sbFileText.ToString().Contains("\r\n\r\n" + string.Format(ReportServiceRes._DoesNotContain_Items, Marker + "Start Root " + culture.TwoLetterISOLanguageName + " Bon", 3.ToString()) + "\r\n\r\n" +
                        string.Format(ReportServiceRes.Example_, Marker + "Start Root en") + "\r\n\r\n"));
                }

                Assert.AreEqual(sbFileText.Length, InitialLength + ("\r\n\r\n" + string.Format(ReportServiceRes._DoesNotContain_Items, Marker + "Start Root " + culture.TwoLetterISOLanguageName + " Bon", 3.ToString()) + "\r\n\r\n" +
                        string.Format(ReportServiceRes.Example_, Marker + "Start Root en") + "\r\n\r\n").Length);
            }
        }
        [TestMethod]
        public void ReportBaseService_CheckTagsFirstLineCSV_ItemName_NotAllowed_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Name,Lat,Lng");
                sbFileText.AppendLine(Marker + "Start NotRoot " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine("Root_Lat");
                sbFileText.AppendLine("Root_Lng");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "End NotRoot " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                retStr = reportBaseService.CheckTagsFirstLineCSV(sbFileText, reportTagList);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(sbFileText.ToString().Contains("\r\n\r\n" + string.Format(ReportServiceRes.ItemName_NotAllowed, "NotRoot") + "\r\n\r\n" +
                         string.Format(ReportServiceRes.AllowableValues_, reportBase.AllowableReportType()) + "\r\n\r\n"));
                }

                Assert.AreEqual(sbFileText.Length, InitialLength + ("\r\n\r\n" + string.Format(ReportServiceRes.ItemName_NotAllowed, "NotRoot") + "\r\n\r\n" +
                         string.Format(ReportServiceRes.AllowableValues_, reportBase.AllowableReportType()) + "\r\n\r\n").Length);
            }
        }
        [TestMethod]
        public void ReportBaseService_CheckTagsFirstLineCSV_Language_NotAllowed_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Name,Lat,Lng");
                sbFileText.AppendLine(Marker + "Start Root Not" + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine("Root_Lat");
                sbFileText.AppendLine("Root_Lng");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "End Root Not" + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                retStr = reportBaseService.CheckTagsFirstLineCSV(sbFileText, reportTagList);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(sbFileText.ToString().Contains("\r\n\r\n" + string.Format(ReportServiceRes.Language_NotAllowed, "Not" + culture.TwoLetterISOLanguageName) + "\r\n\r\n" +
                        string.Format(ReportServiceRes.AllowableLanguages_, "en, fr") + "\r\n\r\n"));
                }

                Assert.AreEqual(sbFileText.Length, InitialLength + ("\r\n\r\n" + string.Format(ReportServiceRes.Language_NotAllowed, "Not" + culture.TwoLetterISOLanguageName) + "\r\n\r\n" +
                        string.Format(ReportServiceRes.AllowableLanguages_, "en, fr") + "\r\n\r\n").Length);
            }
        }
        [TestMethod]
        public void ReportBaseService_CheckTagsPositionOKCSV_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Root_Name,Country_Name");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Country_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                retStr = reportBaseService.CheckTagsPositionOKCSV(sbFileText, reportTagList);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                Assert.AreEqual(sbFileText.Length, InitialLength);
            }
        }
        [TestMethod]
        public void ReportBaseService_CheckTagsPositionOKCSV_NotProperlyClosed_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Root_Name,Country_Name");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Country_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Country_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                retStr = reportBaseService.CheckTagsPositionOKCSV(sbFileText, reportTagList);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(sbFileText.ToString().Contains("\r\n\r\n" + string.Format(ReportServiceRes.YouHaveATag_WhichIsNotClosedProperly, reportTagList[1].CSVTagText) + "\r\n\r\n"));
                }

                Assert.AreEqual(sbFileText.Length, InitialLength + ("\r\n\r\n" + string.Format(ReportServiceRes.YouHaveATag_WhichIsNotClosedProperly, reportTagList[1].CSVTagText) + "\r\n\r\n").Length);
            }
        }
        [TestMethod]
        public void ReportBaseService_CheckTagsPositionOKCSV_LoopTagOutside_StartTag_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Root_Name,Country_Name");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);
                sbFileText.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Country_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                retStr = reportBaseService.CheckTagsPositionOKCSV(sbFileText, reportTagList);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(sbFileText.ToString().Contains("\r\n\r\n" + ReportServiceRes.AllTagsMustBeWithinAStartTag + "\r\n\r\n"));
                }

                Assert.AreEqual(sbFileText.Length, InitialLength + ("\r\n\r\n" + ReportServiceRes.AllTagsMustBeWithinAStartTag + "\r\n\r\n").Length);
            }
        }
        [TestMethod]
        public void ReportBaseService_FindAllStartAndLoopTagsCSV_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("name");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                Assert.AreEqual(sbFileText.Length, InitialLength);
            }
        }
        [TestMethod]
        public void ReportBaseService_FindAllStartAndLoopTagsCSV_2Level_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("name");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Country_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                Assert.AreEqual(sbFileText.Length, InitialLength);
            }
        }
        [TestMethod]
        public void ReportBaseService_FindAllStartAndLoopTagsCSV_3Level_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("name");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Country_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopStart Province " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Province_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopEnd Province " + culture.TwoLetterISOLanguageName + Marker);
                sbFileText.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                Assert.AreEqual(sbFileText.Length, InitialLength);
            }
        }
        [TestMethod]
        public void ReportBaseService_FindAllStartAndLoopTagsCSV_NoTag_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder("No tag document\r\n");

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(sbFileText.ToString().StartsWith("\r\n\r\n" + ReportServiceRes.NoTagFoundInDocument + "\r\n\r\n"));
                }

                Assert.AreEqual(sbFileText.Length, InitialLength + ("\r\n\r\n" + ReportServiceRes.NoTagFoundInDocument + "\r\n\r\n").Length);
            }
        }
        [TestMethod]
        public void ReportBaseService_FindAllStartAndLoopTagsCSV_First_Tag_Not_Start_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("name");
                sbFileText.AppendLine(Marker + "LoopStart Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopEnd Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(sbFileText.ToString().StartsWith("\r\n\r\n" + ReportServiceRes.FirstTagInDocumentMustBeAStartTag + "\r\n\r\n"));
                }

                Assert.AreEqual(sbFileText.Length, InitialLength + ("\r\n\r\n" + ReportServiceRes.FirstTagInDocumentMustBeAStartTag + "\r\n\r\n").Length);
            }
        }
        [TestMethod]
        public void ReportBaseService_FindAllStartAndLoopTagsCSV_StartTagNotClosed_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("name");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                //sbFileText.AppendLine(Marker);
                //sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(sbFileText.ToString().Contains("\r\n\r\n" + string.Format(ReportServiceRes.Tag_NotWellFormedItShouldHaveAStructureLike_, "Start", Marker + "Start" + " Root en" + Marker) + "\r\n\r\n"));
                }

                Assert.AreEqual(sbFileText.Length, InitialLength + ("\r\n\r\n" + string.Format(ReportServiceRes.Tag_NotWellFormedItShouldHaveAStructureLike_, "Start", Marker + "Start" + " Root en" + Marker) + "\r\n\r\n").Length);
            }
        }
        [TestMethod]
        public void ReportBaseService_FindAllStartAndLoopTagsCSV_StartTagMustBeFollowedWithAReturn_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("name");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.Append(Marker);
                //sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(sbFileText.ToString().Contains("\r\n\r\n" + string.Format(ReportServiceRes._TagMustBeFollowedWithAReturn, Marker + "Start" + "*" + Marker) + "\r\n\r\n"));
                }

                Assert.AreEqual(sbFileText.Length, InitialLength + ("\r\n\r\n" + string.Format(ReportServiceRes._TagMustBeFollowedWithAReturn, Marker + "Start" + "*" + Marker) + "\r\n\r\n").Length);
            }
        }
        [TestMethod]
        public void ReportBaseService_FindAllStartAndLoopTagsCSV_Tag_NotWellFormed_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("name");
                sbFileText.AppendLine(Marker + "StartRoot " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(sbFileText.ToString().Contains("\r\n\r\n" + string.Format(ReportServiceRes.Tag_NotWellFormedItShouldHaveAStructureLike_, "Start", Marker + "Start" + " Root en" + Marker) + "\r\n\r\n"));
                }

                Assert.AreEqual(sbFileText.Length, InitialLength + ("\r\n\r\n" + string.Format(ReportServiceRes.Tag_NotWellFormedItShouldHaveAStructureLike_, "Start", Marker + "Start" + " Root en" + Marker) + "\r\n\r\n").Length);
            }
        }
        [TestMethod]
        public void ReportBaseService_FindAllStartAndLoopTagsCSV_CouldNotFindEndTag_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("name");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "End RootNot " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(sbFileText.ToString().Contains("\r\n\r\n" +
                        string.Format(ReportServiceRes.CouldNotFindEndTag_Of_, Marker + ("Start" == "Loop" ? "Loop" : "") + "End*" + Marker,
                        Marker + "Start Root " + culture.TwoLetterISOLanguageName + "\r\n" +
                        "Root_Name" + "\r\n" +
                        Marker) + "\r\n\r\n"));
                }

                Assert.AreEqual(sbFileText.Length, InitialLength + ("\r\n\r\n" +
                        string.Format(ReportServiceRes.CouldNotFindEndTag_Of_, Marker + ("Start" == "Loop" ? "Loop" : "") + "End*" + Marker,
                        Marker + "Start Root " + culture.TwoLetterISOLanguageName + "\r\n" +
                        "Root_Name" + "\r\n" +
                        Marker) + "\r\n\r\n").Length);
            }
        }
        [TestMethod]
        public void ReportBaseService_FindAllStartAndLoopTagsCSV_Tag_NotWellFormed_EndTag_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("name");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(sbFileText.ToString().Contains("\r\n\r\n" +
                        string.Format(ReportServiceRes.Tag_NotWellFormedItShouldHaveAStructureLike_, "End", Marker + "End Root en" + Marker) + "\r\n\r\n"));
                }

                Assert.AreEqual(sbFileText.Length, InitialLength + ("\r\n\r\n" +
                        string.Format(ReportServiceRes.Tag_NotWellFormedItShouldHaveAStructureLike_, "End", Marker + "End Root en" + Marker) + "\r\n\r\n").Length);
            }
        }
        [TestMethod]
        public void ReportBaseService_FindAllStartAndLoopTagsCSV__TagMustBeFollowedWithAReturn_EndTag_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("name");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.Append(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(sbFileText.ToString().Contains("\r\n\r\n" + string.Format(ReportServiceRes._TagMustBeFollowedWithAReturn, Marker + ("Start" == "Loop" ? "Loop" : "") + "End*" + Marker) + "\r\n\r\n"));
                }

                Assert.AreEqual(sbFileText.Length, InitialLength + ("\r\n\r\n" + string.Format(ReportServiceRes._TagMustBeFollowedWithAReturn, Marker + ("Start" == "Loop" ? "Loop" : "") + "End*" + Marker) + "\r\n\r\n").Length);
            }
        }
        [TestMethod]
        public void ReportBaseService_FindAllStartAndLoopTagsCSV_OnlyOneStartTagIsAllowedInCSVTemplate_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("name");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(sbFileText.ToString().Contains("\r\n\r\n" + ReportServiceRes.OnlyOneStartTagIsAllowedInCSVTemplate + "\r\n\r\n"));
                }

                Assert.AreEqual(sbFileText.Length, InitialLength + ("\r\n\r\n" + ReportServiceRes.OnlyOneStartTagIsAllowedInCSVTemplate + "\r\n\r\n").Length);
            }
        }
        [TestMethod]
        public void ReportBaseService_FillTagsParentChildRelationshipCSV_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("name");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Country_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                retStr = reportBaseService.CheckTagsFirstLineCSV(sbFileText, reportTagList);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                retStr = reportBaseService.FillTagsVariablesCSV(sbFileText, reportTagList);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                retStr = reportBaseService.FillTagsParentChildRelationshipCSV(sbFileText, reportTagList);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                Assert.AreEqual(sbFileText.Length, InitialLength);

                Assert.AreEqual(reportTagList[0], reportTagList[1].ReportTagParent);
                Assert.AreEqual(1, reportTagList[0].ReportTagChildList.Count);
                Assert.AreEqual(reportTagList[0].ReportTagChildList[0], reportTagList[1]);
            }
        }
        [TestMethod]
        public void ReportBaseService_FillTagsParentChildRelationshipCSV_XLevel_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Root_Name,Country_Name,Province_Name,Area_Name");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Country_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopStart Province " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Province_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopStart Area " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Area_Name_Short");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopEnd Area " + culture.TwoLetterISOLanguageName + Marker);
                sbFileText.AppendLine(Marker + "LoopEnd Province " + culture.TwoLetterISOLanguageName + Marker);
                sbFileText.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                retStr = reportBaseService.CheckTagsFirstLineCSV(sbFileText, reportTagList);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                retStr = reportBaseService.FillTagsVariablesCSV(sbFileText, reportTagList);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                retStr = reportBaseService.FillTagsParentChildRelationshipCSV(sbFileText, reportTagList);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                Assert.AreEqual(sbFileText.Length, InitialLength);

                Assert.AreEqual(reportTagList[0], reportTagList[1].ReportTagParent);
                Assert.AreEqual(reportTagList[1], reportTagList[2].ReportTagParent);
                Assert.AreEqual(reportTagList[2], reportTagList[3].ReportTagParent);
                Assert.AreEqual(1, reportTagList[0].ReportTagChildList.Count);
                Assert.AreEqual(1, reportTagList[1].ReportTagChildList.Count);
                Assert.AreEqual(1, reportTagList[2].ReportTagChildList.Count);
                Assert.AreEqual(reportTagList[0].ReportTagChildList[0], reportTagList[1]);
                Assert.AreEqual(reportTagList[1].ReportTagChildList[0], reportTagList[2]);
                Assert.AreEqual(reportTagList[2].ReportTagChildList[0], reportTagList[3]);
            }
        }
        [TestMethod]
        public void ReportBaseService_FillTagsLevelsCSV_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Root_Name,Country_Name,Province_Name,Area_Name");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Country_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopStart Province " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Province_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopStart Area " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Area_Name_Short");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopEnd Area " + culture.TwoLetterISOLanguageName + Marker);
                sbFileText.AppendLine(Marker + "LoopEnd Province " + culture.TwoLetterISOLanguageName + Marker);
                sbFileText.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.CheckTagsAndContentOKCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                Assert.AreEqual(sbFileText.Length, InitialLength);

                Assert.AreEqual(0, reportTagList[0].Level);
                Assert.AreEqual(1, reportTagList[1].Level);
                Assert.AreEqual(2, reportTagList[2].Level);
                Assert.AreEqual(3, reportTagList[3].Level);
            }
        }
        [TestMethod]
        public void ReportBaseService_FillTagsVariablesCSV_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Root_Name,Country_Name,Province_Name,Area_Name");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Country_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopStart Province " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Province_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopStart Area " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Area_Name_Short");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopEnd Area " + culture.TwoLetterISOLanguageName + Marker);
                sbFileText.AppendLine(Marker + "LoopEnd Province " + culture.TwoLetterISOLanguageName + Marker);
                sbFileText.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.CheckTagsAndContentOKCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                Assert.AreEqual(sbFileText.Length, InitialLength);

                Assert.AreEqual("Root_Name", reportTagList[0].ReportTreeNodeList[0].Text);
                Assert.AreEqual("Country_Name", reportTagList[1].ReportTreeNodeList[0].Text);
                Assert.AreEqual("Province_Name", reportTagList[2].ReportTreeNodeList[0].Text);
                Assert.AreEqual("Area_Name_Short", reportTagList[3].ReportTreeNodeList[0].Text);
            }
        }
        [TestMethod]
        public void ReportBaseService_FillTemplateWithDBInfoCSV_Good_Level_4_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Root_Name,Country_Name,Province_Name,Area_Name");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Country_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopStart Province " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Province_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopStart Area " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Area_Name_Short");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopEnd Area " + culture.TwoLetterISOLanguageName + Marker);
                sbFileText.AppendLine(Marker + "LoopEnd Province " + culture.TwoLetterISOLanguageName + Marker);
                sbFileText.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.CheckTagsAndContentOKCSV(sbFileText, reportTagList, StartTVItemID, 2);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }
                reportTagList[0].UnderTVItemID = StartTVItemID;

                retStr = reportBaseService.FillTemplateWithDBInfoCSV(sbFileText, reportTagList[0]);
                Assert.AreEqual("", retStr);
                Assert.IsTrue(sbFileText.ToString().Contains((culture.TwoLetterISOLanguageName == "fr" ? "Nouveau-Brunswick" : "New Brunswick")));
                Assert.IsTrue(sbFileText.ToString().Contains("Canada"));
                Assert.IsTrue(sbFileText.ToString().Contains("NB-02"));
                Assert.IsTrue(sbFileText.ToString().Contains("NS-01"));
            }
        }
        [TestMethod]
        public void ReportBaseService_FillTemplateWithDBInfoCSV_Condition_String_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                List<string> condStrList = new List<string>()
                {
                    " BIGGER al SMALLER cou", " EQUAL All*locations",
                    " CONTAIN all", " START all", " END tions"
                };

                foreach (string condStr in condStrList)
                {
                    StringBuilder sbFileText = new StringBuilder();
                    sbFileText.AppendLine("Root_Name");
                    sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                    sbFileText.AppendLine("Root_Name" + condStr);
                    sbFileText.AppendLine(Marker);
                    sbFileText.AppendLine(Marker + "|||Root_Name|||" + Marker);
                    sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                    int InitialLength = sbFileText.Length;

                    FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    StreamWriter sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();

                    List<ReportTag> reportTagList = new List<ReportTag>();
                    int StartTVItemID = 1;
                    string retStr = reportBaseService.CheckTagsAndContentOKCSV(sbFileText, reportTagList, StartTVItemID, 2);
                    if (!string.IsNullOrWhiteSpace(retStr))
                    {
                        fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                        sw = fi.CreateText();
                        sw.Write(sbFileText);
                        sw.Flush();
                        sw.Close();
                        Assert.IsTrue(false);
                    }
                    reportTagList[0].UnderTVItemID = StartTVItemID;

                    retStr = reportBaseService.FillTemplateWithDBInfoCSV(sbFileText, reportTagList[0]);
                    Assert.AreEqual("", retStr);
                    if (culture.TwoLetterISOLanguageName == "fr")
                    {
                        Assert.IsFalse(sbFileText.ToString().Contains("Tous les endroits"));
                    }
                    else
                    {
                        Assert.IsTrue(sbFileText.ToString().Contains("All locations"));
                    }
                }
            }
        }
        [TestMethod]
        public void ReportBaseService_FillTemplateWithDBInfoCSV_Condition_Number_TRUE_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                List<string> condStrTrueList = new List<string>()
                {
                    " EQUAL 50", " BIGGER 49.0", " SMALLER 50.1"
                };

                foreach (string condStr in condStrTrueList)
                {
                    StringBuilder sbFileText = new StringBuilder();
                    sbFileText.AppendLine("Root_Lat");
                    sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                    sbFileText.AppendLine("Root_Lat" + condStr);
                    sbFileText.AppendLine(Marker);
                    sbFileText.AppendLine(Marker + "|||Root_Lat|||" + Marker);
                    sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                    int InitialLength = sbFileText.Length;

                    FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    StreamWriter sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();

                    List<ReportTag> reportTagList = new List<ReportTag>();
                    int StartTVItemID = 1;
                    string retStr = reportBaseService.CheckTagsAndContentOKCSV(sbFileText, reportTagList, StartTVItemID, 2);
                    if (!string.IsNullOrWhiteSpace(retStr))
                    {
                        fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                        sw = fi.CreateText();
                        sw.Write(sbFileText);
                        sw.Flush();
                        sw.Close();
                        Assert.IsTrue(false);
                    }
                    reportTagList[0].UnderTVItemID = StartTVItemID;

                    retStr = reportBaseService.FillTemplateWithDBInfoCSV(sbFileText, reportTagList[0]);
                    Assert.AreEqual("", retStr);
                    Assert.IsTrue(sbFileText.ToString().Contains("50"));
                }
            }
        }
        [TestMethod]
        public void ReportBaseService_FillTemplateWithDBInfoCSV_Condition_Number_FALSE_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                List<string> condStrTrueList = new List<string>()
                {
                    " EQUAL 50.1", " BIGGER 55.0", " SMALLER 45.1"
                };

                foreach (string condStr in condStrTrueList)
                {
                    StringBuilder sbFileText = new StringBuilder();
                    sbFileText.AppendLine("Root_Lat");
                    sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                    sbFileText.AppendLine("Root_Lat" + condStr);
                    sbFileText.AppendLine(Marker);
                    sbFileText.AppendLine(Marker + "|||Root_Lat|||" + Marker);
                    sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                    int InitialLength = sbFileText.Length;

                    FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    StreamWriter sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();

                    List<ReportTag> reportTagList = new List<ReportTag>();
                    int StartTVItemID = 1;
                    string retStr = reportBaseService.CheckTagsAndContentOKCSV(sbFileText, reportTagList, StartTVItemID, 2);
                    if (!string.IsNullOrWhiteSpace(retStr))
                    {
                        fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                        sw = fi.CreateText();
                        sw.Write(sbFileText);
                        sw.Flush();
                        sw.Close();
                        Assert.IsTrue(false);
                    }
                    reportTagList[0].UnderTVItemID = StartTVItemID;

                    retStr = reportBaseService.FillTemplateWithDBInfoCSV(sbFileText, reportTagList[0]);
                    Assert.AreEqual("", retStr);
                    Assert.IsFalse(sbFileText.ToString().Contains("50"));
                }
            }
        }
        [TestMethod]
        public void ReportBaseService_FillTemplateWithDBInfoCSV_Condition_Date_EQUAL_TRUE_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                List<string> condStrTrueList = new List<string>()
                {
                    " EQUAL MINUTE 58",
                    " EQUAL HOUR 16 MINUTE 58",
                    " EQUAL HOUR 16",
                    " EQUAL DAY 2 HOUR 16 MINUTE 58",
                    " EQUAL DAY 2 HOUR 16",
                    " EQUAL DAY 2",
                    " EQUAL MONTH 12 DAY 2 HOUR 16 MINUTE 58",
                    " EQUAL MONTH 12 DAY 2 HOUR 16",
                    " EQUAL MONTH 12 DAY 2",
                    " EQUAL MONTH 12",
                    " EQUAL YEAR 2014 MONTH 12 DAY 2 HOUR 16 MINUTE 58",
                    " EQUAL YEAR 2014 MONTH 12 DAY 2 HOUR 16",
                    " EQUAL YEAR 2014 MONTH 12 DAY 2",
                    " EQUAL YEAR 2014 MONTH 12",
                    " EQUAL YEAR 2014"
                };

                //02/12/2014 4:58:16 PM
                //2014-12-02 16:58:16
                foreach (string condStr in condStrTrueList)
                {
                    StringBuilder sbFileText = new StringBuilder();
                    sbFileText.AppendLine("Root_Lat");
                    sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                    sbFileText.AppendLine("Root_Last_Update_Date_And_Time_UTC" + condStr);
                    sbFileText.AppendLine(Marker);
                    sbFileText.AppendLine(Marker + "|||Root_Last_Update_Date_And_Time|||" + Marker);
                    sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                    int InitialLength = sbFileText.Length;

                    FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    StreamWriter sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();

                    List<ReportTag> reportTagList = new List<ReportTag>();
                    int StartTVItemID = 1;
                    string retStr = reportBaseService.CheckTagsAndContentOKCSV(sbFileText, reportTagList, StartTVItemID, 2);
                    if (!string.IsNullOrWhiteSpace(retStr))
                    {
                        fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                        sw = fi.CreateText();
                        sw.Write(sbFileText);
                        sw.Flush();
                        sw.Close();
                        Assert.IsTrue(false);
                    }
                    reportTagList[0].UnderTVItemID = StartTVItemID;

                    retStr = reportBaseService.FillTemplateWithDBInfoCSV(sbFileText, reportTagList[0]);
                    Assert.AreEqual("", retStr);
                    if (culture.TwoLetterISOLanguageName == "fr")
                    {
                        Assert.IsTrue(sbFileText.ToString().Contains("2014-12-02 16:58:16"));
                    }
                    else
                    {
                        Assert.IsTrue(sbFileText.ToString().Contains("02/12/2014 4:58:16 PM"));
                    }
                }
            }
        }
        [TestMethod]
        public void ReportBaseService_FillTemplateWithDBInfoCSV_Condition_Date_EQUAL_FALSE_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                List<string> condStrTrueList = new List<string>()
                {
                    " EQUAL MINUTE 57",
                    " EQUAL HOUR 16 MINUTE 57",
                    " EQUAL HOUR 15",
                    " EQUAL DAY 2 HOUR 16 MINUTE 57",
                    " EQUAL DAY 2 HOUR 15",
                    " EQUAL DAY 1",
                    " EQUAL MONTH 12 DAY 2 HOUR 16 MINUTE 57",
                    " EQUAL MONTH 12 DAY 2 HOUR 15",
                    " EQUAL MONTH 12 DAY 1",
                    " EQUAL MONTH 11",
                    " EQUAL YEAR 2014 MONTH 12 DAY 2 HOUR 16 MINUTE 57",
                    " EQUAL YEAR 2014 MONTH 12 DAY 2 HOUR 15",
                    " EQUAL YEAR 2014 MONTH 12 DAY 1",
                    " EQUAL YEAR 2014 MONTH 11",
                    " EQUAL YEAR 2013"
                };

                //02/12/2014 4:58:16 PM
                //2014-12-02 16:58:16
                foreach (string condStr in condStrTrueList)
                {
                    StringBuilder sbFileText = new StringBuilder();
                    sbFileText.AppendLine("Root_Lat");
                    sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                    sbFileText.AppendLine("Root_Last_Update_Date_And_Time_UTC" + condStr);
                    sbFileText.AppendLine(Marker);
                    sbFileText.AppendLine(Marker + "|||Root_Last_Update_Date_And_Time|||" + Marker);
                    sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                    int InitialLength = sbFileText.Length;

                    FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    StreamWriter sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();

                    List<ReportTag> reportTagList = new List<ReportTag>();
                    int StartTVItemID = 1;
                    string retStr = reportBaseService.CheckTagsAndContentOKCSV(sbFileText, reportTagList, StartTVItemID, 2);
                    if (!string.IsNullOrWhiteSpace(retStr))
                    {
                        fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                        sw = fi.CreateText();
                        sw.Write(sbFileText);
                        sw.Flush();
                        sw.Close();
                        Assert.IsTrue(false);
                    }
                    reportTagList[0].UnderTVItemID = StartTVItemID;

                    retStr = reportBaseService.FillTemplateWithDBInfoCSV(sbFileText, reportTagList[0]);
                    Assert.AreEqual("", retStr);
                    if (culture.TwoLetterISOLanguageName == "fr")
                    {
                        Assert.IsFalse(sbFileText.ToString().Contains("2014-12-02 16:58:16"));
                    }
                    else
                    {
                        Assert.IsFalse(sbFileText.ToString().Contains("02/12/2014 4:58:16 PM"));
                    }
                }
            }
        }
        [TestMethod]
        public void ReportBaseService_FillTemplateWithDBInfoCSV_Condition_Date_BIGGER_TRUE_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                List<string> condStrTrueList = new List<string>()
                {
                    " BIGGER MINUTE 57",
                    " BIGGER HOUR 16 MINUTE 57",
                    " BIGGER HOUR 15",
                    " BIGGER DAY 2 HOUR 16 MINUTE 57",
                    " BIGGER DAY 2 HOUR 15",
                    " BIGGER DAY 1",
                    " BIGGER MONTH 12 DAY 2 HOUR 16 MINUTE 57",
                    " BIGGER MONTH 12 DAY 2 HOUR 15",
                    " BIGGER MONTH 12 DAY 1",
                    " BIGGER MONTH 11",
                    " BIGGER YEAR 2014 MONTH 12 DAY 2 HOUR 16 MINUTE 57",
                    " BIGGER YEAR 2014 MONTH 12 DAY 2 HOUR 15",
                    " BIGGER YEAR 2014 MONTH 12 DAY 1",
                    " BIGGER YEAR 2014 MONTH 11",
                    " BIGGER YEAR 2013"
                };

                //02/12/2014 4:58:16 PM
                //2014-12-02 16:58:16
                foreach (string condStr in condStrTrueList)
                {
                    StringBuilder sbFileText = new StringBuilder();
                    sbFileText.AppendLine("Root_Lat");
                    sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                    sbFileText.AppendLine("Root_Last_Update_Date_And_Time_UTC" + condStr);
                    sbFileText.AppendLine(Marker);
                    sbFileText.AppendLine(Marker + "|||Root_Last_Update_Date_And_Time|||" + Marker);
                    sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                    int InitialLength = sbFileText.Length;

                    FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    StreamWriter sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();

                    List<ReportTag> reportTagList = new List<ReportTag>();
                    int StartTVItemID = 1;
                    string retStr = reportBaseService.CheckTagsAndContentOKCSV(sbFileText, reportTagList, StartTVItemID, 2);
                    if (!string.IsNullOrWhiteSpace(retStr))
                    {
                        fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                        sw = fi.CreateText();
                        sw.Write(sbFileText);
                        sw.Flush();
                        sw.Close();
                        Assert.IsTrue(false);
                    }
                    reportTagList[0].UnderTVItemID = StartTVItemID;

                    retStr = reportBaseService.FillTemplateWithDBInfoCSV(sbFileText, reportTagList[0]);
                    Assert.AreEqual("", retStr);
                    if (culture.TwoLetterISOLanguageName == "fr")
                    {
                        Assert.IsTrue(sbFileText.ToString().Contains("2014-12-02 16:58:16"));
                    }
                    else
                    {
                        Assert.IsTrue(sbFileText.ToString().Contains("02/12/2014 4:58:16 PM"));
                    }
                }
            }
        }
        [TestMethod]
        public void ReportBaseService_FillTemplateWithDBInfoCSV_Condition_Date_BIGGER_FALSE_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                List<string> condStrTrueList = new List<string>()
                {
                    " BIGGER MINUTE 59",
                    " BIGGER HOUR 16 MINUTE 59",
                    " BIGGER HOUR 17",
                    " BIGGER DAY 2 HOUR 16 MINUTE 59",
                    " BIGGER DAY 2 HOUR 17",
                    " BIGGER DAY 3",
                    " BIGGER MONTH 12 DAY 2 HOUR 16 MINUTE 59",
                    " BIGGER MONTH 12 DAY 2 HOUR 17",
                    " BIGGER MONTH 12 DAY 3",
                    " BIGGER MONTH 13",
                    " BIGGER YEAR 2014 MONTH 12 DAY 2 HOUR 16 MINUTE 59",
                    " BIGGER YEAR 2014 MONTH 12 DAY 2 HOUR 17",
                    " BIGGER YEAR 2014 MONTH 12 DAY 3",
                    " BIGGER YEAR 2014 MONTH 13",
                    " BIGGER YEAR 2015"
                };

                //02/12/2014 4:58:16 PM
                //2014-12-02 16:58:16
                foreach (string condStr in condStrTrueList)
                {
                    StringBuilder sbFileText = new StringBuilder();
                    sbFileText.AppendLine("Root_Lat");
                    sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                    sbFileText.AppendLine("Root_Last_Update_Date_And_Time_UTC" + condStr);
                    sbFileText.AppendLine(Marker);
                    sbFileText.AppendLine(Marker + "|||Root_Last_Update_Date_And_Time|||" + Marker);
                    sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                    int InitialLength = sbFileText.Length;

                    FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    StreamWriter sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();

                    List<ReportTag> reportTagList = new List<ReportTag>();
                    int StartTVItemID = 1;
                    string retStr = reportBaseService.CheckTagsAndContentOKCSV(sbFileText, reportTagList, StartTVItemID, 2);
                    if (!string.IsNullOrWhiteSpace(retStr))
                    {
                        fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                        sw = fi.CreateText();
                        sw.Write(sbFileText);
                        sw.Flush();
                        sw.Close();
                        Assert.IsTrue(false);
                    }
                    reportTagList[0].UnderTVItemID = StartTVItemID;

                    retStr = reportBaseService.FillTemplateWithDBInfoCSV(sbFileText, reportTagList[0]);
                    Assert.AreEqual("", retStr);
                    if (culture.TwoLetterISOLanguageName == "fr")
                    {
                        Assert.IsFalse(sbFileText.ToString().Contains("2014-12-02 16:58:16"));
                    }
                    else
                    {
                        Assert.IsFalse(sbFileText.ToString().Contains("02/12/2014 4:58:16 PM"));
                    }
                }
            }
        }
        [TestMethod]
        public void ReportBaseService_FillTemplateWithDBInfoCSV_Condition_Date_SMALLER_TRUE_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                List<string> condStrTrueList = new List<string>()
                {
                    " SMALLER MINUTE 59",
                    " SMALLER HOUR 16 MINUTE 59",
                    " SMALLER HOUR 17",
                    " SMALLER DAY 2 HOUR 16 MINUTE 59",
                    " SMALLER DAY 2 HOUR 17",
                    " SMALLER DAY 3",
                    " SMALLER MONTH 12 DAY 2 HOUR 16 MINUTE 59",
                    " SMALLER MONTH 12 DAY 2 HOUR 17",
                    " SMALLER MONTH 12 DAY 3",
                    " SMALLER MONTH 13",
                    " SMALLER YEAR 2014 MONTH 12 DAY 2 HOUR 16 MINUTE 59",
                    " SMALLER YEAR 2014 MONTH 12 DAY 2 HOUR 17",
                    " SMALLER YEAR 2014 MONTH 12 DAY 3",
                    " SMALLER YEAR 2014 MONTH 13",
                    " SMALLER YEAR 2015"
              };

                //02/12/2014 4:58:16 PM
                //2014-12-02 16:58:16
                foreach (string condStr in condStrTrueList)
                {
                    StringBuilder sbFileText = new StringBuilder();
                    sbFileText.AppendLine("Root_Lat");
                    sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                    sbFileText.AppendLine("Root_Last_Update_Date_And_Time_UTC" + condStr);
                    sbFileText.AppendLine(Marker);
                    sbFileText.AppendLine(Marker + "|||Root_Last_Update_Date_And_Time|||" + Marker);
                    sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                    int InitialLength = sbFileText.Length;

                    FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    StreamWriter sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();

                    List<ReportTag> reportTagList = new List<ReportTag>();
                    int StartTVItemID = 1;
                    string retStr = reportBaseService.CheckTagsAndContentOKCSV(sbFileText, reportTagList, StartTVItemID, 2);
                    if (!string.IsNullOrWhiteSpace(retStr))
                    {
                        fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                        sw = fi.CreateText();
                        sw.Write(sbFileText);
                        sw.Flush();
                        sw.Close();
                        Assert.IsTrue(false);
                    }
                    reportTagList[0].UnderTVItemID = StartTVItemID;

                    retStr = reportBaseService.FillTemplateWithDBInfoCSV(sbFileText, reportTagList[0]);
                    Assert.AreEqual("", retStr);
                    if (culture.TwoLetterISOLanguageName == "fr")
                    {
                        Assert.IsTrue(sbFileText.ToString().Contains("2014-12-02 16:58:16"));
                    }
                    else
                    {
                        Assert.IsTrue(sbFileText.ToString().Contains("02/12/2014 4:58:16 PM"));
                    }
                }
            }
        }
        [TestMethod]
        public void ReportBaseService_FillTemplateWithDBInfoCSV_Condition_Date_SMALLER_FALSE_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                List<string> condStrTrueList = new List<string>()
                {
                    " SMALLER MINUTE 57",
                    " SMALLER HOUR 16 MINUTE 57",
                    " SMALLER HOUR 15",
                    " SMALLER DAY 2 HOUR 16 MINUTE 57",
                    " SMALLER DAY 2 HOUR 15",
                    " SMALLER DAY 1",
                    " SMALLER MONTH 12 DAY 2 HOUR 16 MINUTE 57",
                    " SMALLER MONTH 12 DAY 2 HOUR 15",
                    " SMALLER MONTH 12 DAY 1",
                    " SMALLER MONTH 11",
                    " SMALLER YEAR 2014 MONTH 12 DAY 2 HOUR 16 MINUTE 57",
                    " SMALLER YEAR 2014 MONTH 12 DAY 2 HOUR 15",
                    " SMALLER YEAR 2014 MONTH 12 DAY 1",
                    " SMALLER YEAR 2014 MONTH 11",
                    " SMALLER YEAR 2013"
                };

                //02/12/2014 4:58:16 PM
                //2014-12-02 16:58:16
                foreach (string condStr in condStrTrueList)
                {
                    StringBuilder sbFileText = new StringBuilder();
                    sbFileText.AppendLine("Root_Lat");
                    sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                    sbFileText.AppendLine("Root_Last_Update_Date_And_Time_UTC" + condStr);
                    sbFileText.AppendLine(Marker);
                    sbFileText.AppendLine(Marker + "|||Root_Last_Update_Date_And_Time|||" + Marker);
                    sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                    int InitialLength = sbFileText.Length;

                    FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    StreamWriter sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();

                    List<ReportTag> reportTagList = new List<ReportTag>();
                    int StartTVItemID = 1;
                    string retStr = reportBaseService.CheckTagsAndContentOKCSV(sbFileText, reportTagList, StartTVItemID, 2);
                    if (!string.IsNullOrWhiteSpace(retStr))
                    {
                        fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                        sw = fi.CreateText();
                        sw.Write(sbFileText);
                        sw.Flush();
                        sw.Close();
                        Assert.IsTrue(false);
                    }
                    reportTagList[0].UnderTVItemID = StartTVItemID;

                    retStr = reportBaseService.FillTemplateWithDBInfoCSV(sbFileText, reportTagList[0]);
                    Assert.AreEqual("", retStr);
                    if (culture.TwoLetterISOLanguageName == "fr")
                    {
                        Assert.IsFalse(sbFileText.ToString().Contains("2014-12-02 16:58:16"));
                    }
                    else
                    {
                        Assert.IsFalse(sbFileText.ToString().Contains("02/12/2014 4:58:16 PM"));
                    }
                }
            }
        }
        [TestMethod]
        public void ReportBaseService_FillTemplateWithDBInfoCSV_Condition_Root_Name_CONTAIN_Tous_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name CONTAIN Tous");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "|||Root_Name|||" + Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.CheckTagsAndContentOKCSV(sbFileText, reportTagList, StartTVItemID, 2);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }
                reportTagList[0].UnderTVItemID = StartTVItemID;

                retStr = reportBaseService.FillTemplateWithDBInfoCSV(sbFileText, reportTagList[0]);
                Assert.AreEqual("", retStr);
                if (culture.TwoLetterISOLanguageName == "fr")
                {
                    Assert.IsTrue(sbFileText.ToString().Contains("Tous les endroits"));
                }
                else
                {
                    Assert.IsFalse(sbFileText.ToString().Contains("All locations"));
                }

            }
        }
        [TestMethod]
        public void ReportBaseService_FillTemplateWithDBInfoCSV_Condition_Root_Last_Update_Date_And_Time_ASCENDING_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Root_Name,Date");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine("Root_Last_Update_Date_And_Time_UTC ASCENDING 1");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "|||Root_Name|||" + Marker);
                sbFileText.AppendLine(Marker + "|||Root_Last_Update_Date_And_Time|||" + Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.CheckTagsAndContentOKCSV(sbFileText, reportTagList, StartTVItemID, 2);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }
                reportTagList[0].UnderTVItemID = StartTVItemID;

                retStr = reportBaseService.FillTemplateWithDBInfoCSV(sbFileText, reportTagList[0]);
                Assert.AreEqual("", retStr);
                if (culture.TwoLetterISOLanguageName == "fr")
                {
                    Assert.IsTrue(sbFileText.ToString().Contains("Tous les endroits"));
                }
                else
                {
                    Assert.IsTrue(sbFileText.ToString().Contains("All locations"));
                }
                Assert.IsTrue(sbFileText.ToString().Contains("2014"));

            }
        }
        [TestMethod]
        public void ReportBaseService_FirstLineMustNotHaveMarkersCSV_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Name,Lat,Lng");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine("Root_Lat");
                sbFileText.AppendLine("Root_Lnt");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                string retStr = reportBaseService.FirstLineMustNotHaveMarkersCSV(sbFileText);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                Assert.AreEqual(sbFileText.Length, InitialLength);
            }
        }
        [TestMethod]
        public void ReportBaseService_FirstLineMustNotHaveMarkersCSV_Has_Marker_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Name,Lat,Lng" + Marker);
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine("Root_Lat");
                sbFileText.AppendLine("Root_Lnt");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                string retStr = reportBaseService.FirstLineMustNotHaveMarkersCSV(sbFileText);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(sbFileText.ToString().Contains("\r\n\r\n" + string.Format(ReportServiceRes.FirstLineOfCSVTemplateShouldNotContain_, Marker) + "\r\n\r\n"));
                }

                Assert.AreEqual(sbFileText.Length, InitialLength + ("\r\n\r\n" + string.Format(ReportServiceRes.FirstLineOfCSVTemplateShouldNotContain_, Marker) + "\r\n\r\n").Length);
            }
        }
        [TestMethod]
        public void ReportBaseService_GenerateForReportTagCSV_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Root_Name,Country_Name");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Country_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.CheckTagsAndContentOKCSV(sbFileText, reportTagList, StartTVItemID, 2);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                retStr = reportBaseService.GenerateNextReportTagCSV("", sbFileText, reportTagList[0]);
                Assert.AreEqual("", retStr);
                Assert.IsTrue(sbFileText.ToString().Contains("Canada"));
            }
        }
        [TestMethod]
        public void ReportBaseService_GenerateReportFromTemplateCSV_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Root_Name,Country_Name");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Country_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fiTemplate = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\Template_test.CSV");
                StreamWriter sw = fiTemplate.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                FileInfo fiCSV = new FileInfo(fiTemplate.FullName.Replace("Template_", ""));

                if (fiCSV.Exists)
                {
                    try
                    {
                        fiCSV.Delete();
                    }
                    catch (Exception)
                    {
                        Assert.IsTrue(false);
                    }
                }

                try
                {
                    File.Copy(fiTemplate.FullName, fiCSV.FullName);
                }
                catch (Exception)
                {
                    Assert.IsTrue(false);
                }

                fiCSV = new FileInfo(fiCSV.FullName);
                Assert.IsTrue(fiCSV.Exists);

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.GenerateReportFromTemplateCSV(fiCSV, StartTVItemID, 2, 0);

                Assert.AreEqual("", retStr);

                string FileText = "";
                StreamReader sr = fiCSV.OpenText();
                FileText = sr.ReadToEnd();
                sr.Close();

                Assert.IsTrue(FileText.Contains("Canada"));
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodes_Tag_Start_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                List<string> FieldNameList = new List<string>() { "Root_Name", "Root_ID", "Root_Is_Active", "Root_Last_Update_Date_And_Time_UTC", "Root_Lat" };
                List<ReportFieldTypeEnum> FieldType = new List<ReportFieldTypeEnum>() { ReportFieldTypeEnum.Text, ReportFieldTypeEnum.NumberWhole, ReportFieldTypeEnum.TrueOrFalse, ReportFieldTypeEnum.DateAndTime, ReportFieldTypeEnum.NumberWithDecimal };

                for (int i = 0, count = FieldNameList.Count(); i < count; i++)
                {
                    StringBuilder sbFileText = new StringBuilder();
                    sbFileText.AppendLine(FieldNameList[i]);
                    sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                    sbFileText.AppendLine(FieldNameList[i]);
                    sbFileText.AppendLine(Marker);
                    sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                    int InitialLength = sbFileText.Length;

                    FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    StreamWriter sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();

                    List<ReportTag> reportTagList = new List<ReportTag>();
                    int StartTVItemID = 1;
                    string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                    if (!string.IsNullOrWhiteSpace(retStr))
                    {
                        fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                        sw = fi.CreateText();
                        sw.Write(sbFileText);
                        sw.Flush();
                        sw.Close();
                        Assert.IsTrue(false);
                    }

                    retStr = reportBaseService.CheckTagsFirstLineCSV(sbFileText, reportTagList);
                    if (!string.IsNullOrWhiteSpace(retStr))
                    {
                        fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                        sw = fi.CreateText();
                        sw.Write(sbFileText);
                        sw.Flush();
                        sw.Close();
                        Assert.IsTrue(false);
                    }

                    bool IsDBFiltering = true;
                    string TagText = sbFileText.ToString();
                    TagText = TagText.Substring(TagText.IndexOf("\r\n") + 2);
                    retStr = reportBaseService.GetReportTreeNodesFromTagText(TagText, "Root", typeof(ReportRootModel), reportTagList[0].ReportTreeNodeList, IsDBFiltering);
                    if (!string.IsNullOrWhiteSpace(retStr))
                    {
                        fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                        sw = fi.CreateText();
                        sw.Write(sbFileText);
                        sw.Flush();
                        sw.Close();
                        Assert.IsTrue(false);
                    }

                    Assert.AreEqual(InitialLength, sbFileText.Length);
                }
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodes_Tag_Loop_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                List<string> FieldNameList = new List<string>() { "Country_Name", "Country_ID", "Country_Is_Active", "Country_Last_Update_Date_And_Time", "Country_Lat" };
                List<ReportFieldTypeEnum> FieldType = new List<ReportFieldTypeEnum>() { ReportFieldTypeEnum.Text, ReportFieldTypeEnum.NumberWhole, ReportFieldTypeEnum.TrueOrFalse, ReportFieldTypeEnum.DateAndTime, ReportFieldTypeEnum.NumberWithDecimal };

                for (int i = 0, count = FieldNameList.Count(); i < count; i++)
                {
                    StringBuilder sbFileText = new StringBuilder();
                    sbFileText.AppendLine("Root_Name," + FieldNameList[i]);
                    sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                    sbFileText.AppendLine("Root_Name");
                    sbFileText.AppendLine(Marker);
                    sbFileText.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                    sbFileText.AppendLine(FieldNameList[i]);
                    sbFileText.AppendLine(Marker);
                    sbFileText.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                    sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                    int InitialLength = sbFileText.Length;

                    FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    StreamWriter sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();

                    List<ReportTag> reportTagList = new List<ReportTag>();
                    int StartTVItemID = 1;
                    string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                    if (!string.IsNullOrWhiteSpace(retStr))
                    {
                        fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                        sw = fi.CreateText();
                        sw.Write(sbFileText);
                        sw.Flush();
                        sw.Close();
                        Assert.IsTrue(false);
                    }

                    retStr = reportBaseService.CheckTagsFirstLineCSV(sbFileText, reportTagList);
                    if (!string.IsNullOrWhiteSpace(retStr))
                    {
                        fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                        sw = fi.CreateText();
                        sw.Write(sbFileText);
                        sw.Flush();
                        sw.Close();
                        Assert.IsTrue(false);
                    }

                    bool IsDBFiltering = true;
                    string TagText = sbFileText.ToString();
                    TagText = TagText.Substring(TagText.IndexOf("\r\n") + 2);
                    retStr = reportBaseService.GetReportTreeNodesFromTagText(TagText, "Root", typeof(ReportRootModel), reportTagList[0].ReportTreeNodeList, IsDBFiltering);
                    if (!string.IsNullOrWhiteSpace(retStr))
                    {
                        fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                        sw = fi.CreateText();
                        sw.Write(sbFileText);
                        sw.Flush();
                        sw.Close();
                        Assert.IsTrue(false);
                    }

                    Assert.AreEqual(InitialLength, sbFileText.Length);
                }
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodes_LineShouldStartWith_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("NotRoot_Name");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                retStr = reportBaseService.CheckTagsFirstLineCSV(sbFileText, reportTagList);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                bool IsDBFiltering = true;
                string TagText = sbFileText.ToString();
                TagText = TagText.Substring(TagText.IndexOf("\r\n") + 2);
                retStr = reportBaseService.GetReportTreeNodesFromTagText(TagText, "Root", typeof(ReportRootModel), reportTagList[0].ReportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_ShouldStartWith_, 2, "Root"), retStr);

                fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodes_LineShouldBeOneOf_Bool_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Root_Is_Active");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Is_Active NotTRUE");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                retStr = reportBaseService.CheckTagsFirstLineCSV(sbFileText, reportTagList);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                bool IsDBFiltering = true;
                string TagText = sbFileText.ToString();
                TagText = TagText.Substring(TagText.IndexOf("\r\n") + 2);
                retStr = reportBaseService.GetReportTreeNodesFromTagText(TagText, "Root", typeof(ReportRootModel), reportTagList[0].ReportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, 2, 2, String.Join(",", reportBaseService.AllowableSortingFilters.Concat(reportBaseService.AllowableTrueFalseFilters))), retStr);

                fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodes_LineShouldBeOneOf_Date_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Root_Last_Update_Date_And_Time_UTC");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Last_Update_Date_And_Time_UTC NotAllowable");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                retStr = reportBaseService.CheckTagsFirstLineCSV(sbFileText, reportTagList);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                bool IsDBFiltering = true;
                string TagText = sbFileText.ToString();
                TagText = TagText.Substring(TagText.IndexOf("\r\n") + 2);
                retStr = reportBaseService.GetReportTreeNodesFromTagText(TagText, "Root", typeof(ReportRootModel), reportTagList[0].ReportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, 2, 2, String.Join(",", reportBaseService.AllowableSortingFilters.Concat(reportBaseService.AllowableFormatingFilters.Concat(reportBaseService.AllowableBasicFilters)))), retStr);

                fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodes_LineShouldBeOneOf_Int_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Root_ID");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_ID NotAllowable");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                retStr = reportBaseService.CheckTagsFirstLineCSV(sbFileText, reportTagList);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                bool IsDBFiltering = true;
                string TagText = sbFileText.ToString();
                TagText = TagText.Substring(TagText.IndexOf("\r\n") + 2);
                retStr = reportBaseService.GetReportTreeNodesFromTagText(TagText, "Root", typeof(ReportRootModel), reportTagList[0].ReportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, 2, 2, String.Join(",", reportBaseService.AllowableSortingFilters.Concat(reportBaseService.AllowableFormatingFilters.Concat(reportBaseService.AllowableBasicFilters)))), retStr);

                fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodes_LineShouldBeOneOf_Single_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Root_Lat");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Lat NotAllowable");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                retStr = reportBaseService.CheckTagsFirstLineCSV(sbFileText, reportTagList);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                bool IsDBFiltering = true;
                string TagText = sbFileText.ToString();
                TagText = TagText.Substring(TagText.IndexOf("\r\n") + 2);
                retStr = reportBaseService.GetReportTreeNodesFromTagText(TagText, "Root", typeof(ReportRootModel), reportTagList[0].ReportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, 2, 2, String.Join(",", reportBaseService.AllowableSortingFilters.Concat(reportBaseService.AllowableFormatingFilters.Concat(reportBaseService.AllowableBasicFilters)))), retStr);

                fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodes_LineShouldBeOneOf_String_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name NotAllowable");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                StreamWriter sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, 1000000);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                retStr = reportBaseService.CheckTagsFirstLineCSV(sbFileText, reportTagList);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                    sw = fi.CreateText();
                    sw.Write(sbFileText);
                    sw.Flush();
                    sw.Close();
                    Assert.IsTrue(false);
                }

                bool IsDBFiltering = true;
                string TagText = sbFileText.ToString();
                TagText = TagText.Substring(TagText.IndexOf("\r\n") + 2);
                retStr = reportBaseService.GetReportTreeNodesFromTagText(TagText, "Root", typeof(ReportRootModel), reportTagList[0].ReportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, 2, 2, String.Join(",", reportBaseService.AllowableSortingFilters.Concat(reportBaseService.AllowableTextFilters))), retStr);

                fi = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                sw = fi.CreateText();
                sw.Write(sbFileText);
                sw.Flush();
                sw.Close();

            }
        }
        [TestMethod]
        public void ReportBaseService_SaveFileCSV_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbFileText = new StringBuilder();
                sbFileText.AppendLine("Root_Name");
                sbFileText.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sbFileText.AppendLine("Root_Name NotAllowable");
                sbFileText.AppendLine(Marker);
                sbFileText.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                int InitialLength = sbFileText.Length;

                FileInfo fiCSV = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");

                if (fiCSV.Exists)
                {
                    try
                    {
                        fiCSV.Delete();
                    }
                    catch (Exception)
                    {
                        Assert.IsTrue(false);
                    }
                }

                reportBaseService.SaveFileCSV(sbFileText, fiCSV);

                fiCSV = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.CSV");
                Assert.IsTrue(fiCSV.Exists);


            }
        }
        #endregion Functions public

        #region Functions
        public void SetupTest(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            treeViewCSSP = new TreeView();
            richTextBoxTreeViewSelectedStatus = new RichTextBox();
            reportTreeNodeRoot = new ReportTreeNode();

            reportBaseService = new ReportBaseService((culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en), treeViewCSSP);
            baseEnumService = new BaseEnumService((culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en));
            reportBase = new ReportBase();
        }
        private void SetupShim()
        {
            //shimClimateSiteService = new ShimClimateSiteService(climateSiteService);
        }
        private ReportTreeNode GetReportTreeNodeWithText(ReportTreeNode reportTreeNode, string ReportTreeNodeText)
        {
            if (reportTreeNode.Text == ReportTreeNodeText)
                return reportTreeNode;

            foreach (ReportTreeNode RTN in reportTreeNode.Nodes)
            {
                ReportTreeNode RTNRes = GetReportTreeNodeWithText(RTN, ReportTreeNodeText);
                if (RTNRes != null)
                    return RTNRes;
            }

            return null;
        }
        #endregion Functions

    }
}
