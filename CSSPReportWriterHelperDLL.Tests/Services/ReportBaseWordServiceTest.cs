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
using CSSPWebToolsDBDLL.Services.Resources;

namespace CSSPReportWriterHelperDLL.Tests.Services
{
    /// <summary>
    /// Summary description for BaseServiceTest
    /// </summary>
    [TestClass]
    public class ReportBaseWordServiceTest : SetupData
    {
        #region Variables
        private TestContext testContextInstance;
        private SetupData setupData;
        private string Marker = "|||";
        bool WordVisibleWhenTesting = true;
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
        public ReportBaseWordServiceTest()
        {
            setupData = new SetupData();
        }
        #endregion Constructors

        #region Testing Functions public
        [TestMethod]
        public void ReportBaseService_Constructor_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Assert.AreEqual(Thread.CurrentThread.CurrentCulture, culture);
                Assert.AreEqual(Thread.CurrentThread.CurrentUICulture, culture);

                Assert.IsNotNull(treeViewCSSP);
                Assert.IsNotNull(richTextBoxTreeViewSelectedStatus);
                Assert.IsNotNull(reportTreeNodeRoot);
                Assert.IsNotNull(reportBaseService);
                Assert.IsNotNull(baseEnumService);
                Assert.IsNotNull(reportBase);

            }
        }
        [TestMethod]
        public void ReportBaseService_CheckTagsFirstLineWord_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "ShowStart Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name CONTAIN All");
                sb.AppendLine(Marker);
                sb.AppendLine("This should be shown" + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "ShowEnd Root " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                    doc.Close();
                    wordApp.Quit();
                    Assert.IsTrue(false);
                }

                retStr = reportBaseService.CheckTagsFirstLineWord(reportTagList[0]);

                int commentsCount = doc.Comments.Count;
                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

                Assert.AreEqual(0, commentsCount);
            }
        }
        [TestMethod]
        public void ReportBaseService_CheckTagsFirstLineWord_CouldNotFindFirstLineReturnsAreRequired_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                for (int i = 0; i < 2; i++)
                {
                    Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                    Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                    Microsoft.Office.Interop.Word.Range range = doc.Range();

                    wordApp.Visible = WordVisibleWhenTesting;

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                    sb.AppendLine("Root_Name");
                    sb.AppendLine(Marker);
                    sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                    sb.AppendLine(Marker + "ShowStart Root " + culture.TwoLetterISOLanguageName);
                    sb.AppendLine("Root_Name CONTAIN All");
                    sb.AppendLine(Marker);
                    sb.AppendLine("This should be shown " + Marker + "Root_Name" + Marker);
                    sb.AppendLine(Marker + "ShowEnd Root " + culture.TwoLetterISOLanguageName + Marker);
                    sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                    doc.Range().Text = "";
                    doc.Range().InsertAfter(sb.ToString());

                    List<ReportTag> reportTagList = new List<ReportTag>();
                    ReportTag reportTagStart = new ReportTag()
                    {
                        wordApp = wordApp,
                        doc = doc,
                        OnlyImmediateChildren = false,
                        UnderTVItemID = 1,
                        Take = 2
                    };
                    string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);
                    if (!string.IsNullOrWhiteSpace(retStr))
                    {
                        doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                        doc.Close();
                        wordApp.Quit();
                        Assert.IsTrue(false);
                    }

                    reportTagList[i].RangeStartTag.Text = reportTagList[i].RangeStartTag.Text.Substring(0, reportTagList[i].RangeStartTag.Text.IndexOf("\r"));
                    retStr = reportBaseService.CheckTagsFirstLineWord(reportTagList[i]);

                    int commentsCount = doc.Comments.Count;
                    doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                    doc.Close();
                    wordApp.Quit();

                    Assert.AreEqual(1, commentsCount);
                    Assert.AreEqual(ReportServiceRes.CouldNotFindFirstLineReturnsAreRequired, retStr);
                }
            }
        }
        [TestMethod]
        public void ReportBaseService_CheckTagsFirstLineWord_DoesNotContain_Items_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                for (int i = 0; i < 2; i++)
                {
                    Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                    Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                    Microsoft.Office.Interop.Word.Range range = doc.Range();

                    wordApp.Visible = WordVisibleWhenTesting;

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                    sb.AppendLine("Root_Name");
                    sb.AppendLine(Marker);
                    sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                    sb.AppendLine(Marker + "ShowStart Root " + culture.TwoLetterISOLanguageName);
                    sb.AppendLine("Root_Name CONTAIN All");
                    sb.AppendLine(Marker);
                    sb.AppendLine("This should be shown " + Marker + "Root_Name" + Marker);
                    sb.AppendLine(Marker + "ShowEnd Root " + culture.TwoLetterISOLanguageName + Marker);
                    sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                    doc.Range().Text = "";
                    doc.Range().InsertAfter(sb.ToString());

                    List<ReportTag> reportTagList = new List<ReportTag>();
                    ReportTag reportTagStart = new ReportTag()
                    {
                        wordApp = wordApp,
                        doc = doc,
                        OnlyImmediateChildren = false,
                        UnderTVItemID = 1,
                        Take = 2
                    };
                    string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);
                    if (!string.IsNullOrWhiteSpace(retStr))
                    {
                        doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                        doc.Close();
                        wordApp.Quit();
                        Assert.IsTrue(false);
                    }

                    string tagName = "";
                    switch (i)
                    {
                        case 0:
                            {
                                tagName = "Start";
                            }
                            break;
                        case 1:
                            {
                                tagName = "ShowStart";
                            }
                            break;
                        default:
                            break;
                    }
                    reportTagList[i].RangeStartTag.Text = reportTagList[i].RangeStartTag.Text.Replace(Marker + tagName + " Root " + culture.TwoLetterISOLanguageName + "\r", Marker + tagName + " Root " + culture.TwoLetterISOLanguageName + " not\r");
                    retStr = reportBaseService.CheckTagsFirstLineWord(reportTagList[i]);

                    int commentsCount = doc.Comments.Count;
                    doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                    doc.Close();
                    wordApp.Quit();

                    int FirstLineCount = 3;
                    Assert.AreEqual(1, commentsCount);
                    Assert.AreEqual(string.Format(ReportServiceRes._DoesNotContain_Items, Marker + tagName + " Root " + culture.TwoLetterISOLanguageName + " not",
                        FirstLineCount.ToString()) + "\r\n\r\n" +
                            string.Format(ReportServiceRes.Example_, Marker + "Start Root en"), retStr);
                }
            }
        }
        [TestMethod]
        public void ReportBaseService_CheckTagsFirstLineWord_DoesNotContain_Items1_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                for (int i = 0; i < 2; i++)
                {
                    Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                    Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                    Microsoft.Office.Interop.Word.Range range = doc.Range();

                    wordApp.Visible = WordVisibleWhenTesting;

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                    sb.AppendLine("Root_Name");
                    sb.AppendLine(Marker);
                    sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                    sb.AppendLine(Marker + "ShowStart Root " + culture.TwoLetterISOLanguageName);
                    sb.AppendLine("Root_Name CONTAIN All");
                    sb.AppendLine(Marker);
                    sb.AppendLine(Marker + "Root_Name" + Marker);
                    sb.AppendLine(Marker + "ShowEnd Root " + culture.TwoLetterISOLanguageName + Marker);
                    sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                    doc.Range().Text = "";
                    doc.Range().InsertAfter(sb.ToString());

                    List<ReportTag> reportTagList = new List<ReportTag>();
                    ReportTag reportTagStart = new ReportTag()
                    {
                        wordApp = wordApp,
                        doc = doc,
                        OnlyImmediateChildren = false,
                        UnderTVItemID = 1,
                        Take = 2
                    };
                    string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);
                    if (!string.IsNullOrWhiteSpace(retStr))
                    {
                        doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                        doc.Close();
                        wordApp.Quit();
                        Assert.IsTrue(false);
                    }

                    string tagName = "";
                    switch (i)
                    {
                        case 0:
                            {
                                tagName = "Start";
                            }
                            break;
                        case 1:
                            {
                                tagName = "ShowStart";
                            }
                            break;
                        default:
                            break;
                    }
                    reportTagList[i].RangeStartTag.Text = reportTagList[i].RangeStartTag.Text.Replace(Marker + tagName + " Root " + culture.TwoLetterISOLanguageName + "\r", Marker + tagName + " Root " + "\r");
                    retStr = reportBaseService.CheckTagsFirstLineWord(reportTagList[i]);

                    int commentsCount = doc.Comments.Count;
                    doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                    doc.Close();
                    wordApp.Quit();

                    int FirstLineCount = 3;
                    Assert.AreEqual(1, commentsCount);
                    Assert.AreEqual(string.Format(ReportServiceRes._DoesNotContain_Items, Marker + tagName + " Root ",
                    FirstLineCount.ToString()) + "\r\n\r\n" +
                        string.Format(ReportServiceRes.Example_, Marker + "Start Root en"), retStr);
                }
            }
        }
        [TestMethod]
        public void ReportBaseService_CheckTagsFirstLineWord_ItemName_NotAllowed_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                for (int i = 0; i < 2; i++)
                {
                    Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                    Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                    Microsoft.Office.Interop.Word.Range range = doc.Range();

                    wordApp.Visible = WordVisibleWhenTesting;

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                    sb.AppendLine("Root_Name");
                    sb.AppendLine(Marker);
                    sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                    sb.AppendLine(Marker + "ShowStart Root " + culture.TwoLetterISOLanguageName);
                    sb.AppendLine("Root_Name CONTAIN All");
                    sb.AppendLine(Marker);
                    sb.AppendLine(Marker + "Root_Name" + Marker);
                    sb.AppendLine(Marker + "ShowEnd Root " + culture.TwoLetterISOLanguageName + Marker);
                    sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                    doc.Range().Text = "";
                    doc.Range().InsertAfter(sb.ToString());

                    List<ReportTag> reportTagList = new List<ReportTag>();
                    ReportTag reportTagStart = new ReportTag()
                    {
                        wordApp = wordApp,
                        doc = doc,
                        OnlyImmediateChildren = false,
                        UnderTVItemID = 1,
                        Take = 2
                    };
                    string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);
                    if (!string.IsNullOrWhiteSpace(retStr))
                    {
                        doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                        doc.Close();
                        wordApp.Quit();
                        Assert.IsTrue(false);
                    }

                    string tagName = "";
                    switch (i)
                    {
                        case 0:
                            {
                                tagName = "Start";
                            }
                            break;
                        case 1:
                            {
                                tagName = "ShowStart";
                            }
                            break;
                        default:
                            break;
                    }
                    reportTagList[i].RangeStartTag.Text = reportTagList[i].RangeStartTag.Text.Replace(Marker + tagName + " Root " + culture.TwoLetterISOLanguageName + "\r", Marker + tagName + " RootNot " + culture.TwoLetterISOLanguageName + "\r");
                    reportTagList[i].RangeEndTag.Text = reportTagList[i].RangeEndTag.Text.Replace(Marker + tagName + " Root " + culture.TwoLetterISOLanguageName + Marker, Marker + tagName + " RootNot " + culture.TwoLetterISOLanguageName + Marker);
                    retStr = reportBaseService.CheckTagsFirstLineWord(reportTagList[i]);

                    int commentsCount = doc.Comments.Count;
                    doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                    doc.Close();
                    wordApp.Quit();

                    Assert.AreEqual(1, commentsCount);
                    Assert.AreEqual(string.Format(ReportServiceRes.ItemName_NotAllowed, reportTagList[i].TagItem) + "\r\n\r\n" +
                           string.Format(ReportServiceRes.AllowableValues_, reportBase.AllowableReportType()), retStr);
                }
            }
        }
        [TestMethod]
        public void ReportBaseService_CheckTagsFirstLineWord_Language_NotAllowed_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                for (int i = 0; i < 2; i++)
                {
                    Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                    Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                    Microsoft.Office.Interop.Word.Range range = doc.Range();

                    wordApp.Visible = WordVisibleWhenTesting;

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                    sb.AppendLine("Root_Name");
                    sb.AppendLine(Marker);
                    sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                    sb.AppendLine(Marker + "ShowStart Root " + culture.TwoLetterISOLanguageName);
                    sb.AppendLine("Root_Name CONTAIN All");
                    sb.AppendLine(Marker);
                    sb.AppendLine(Marker + "Root_Name" + Marker);
                    sb.AppendLine(Marker + "ShowEnd Root " + culture.TwoLetterISOLanguageName + Marker);
                    sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                    doc.Range().Text = "";
                    doc.Range().InsertAfter(sb.ToString());

                    List<ReportTag> reportTagList = new List<ReportTag>();
                    ReportTag reportTagStart = new ReportTag()
                    {
                        wordApp = wordApp,
                        doc = doc,
                        OnlyImmediateChildren = false,
                        UnderTVItemID = 1,
                        Take = 2
                    };
                    string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);
                    if (!string.IsNullOrWhiteSpace(retStr))
                    {
                        doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                        doc.Close();
                        wordApp.Quit();
                        Assert.IsTrue(false);
                    }

                    string tagName = "";
                    switch (i)
                    {
                        case 0:
                            {
                                tagName = "Start";
                            }
                            break;
                        case 1:
                            {
                                tagName = "ShowStart";
                            }
                            break;
                        default:
                            break;
                    }
                    reportTagList[i].RangeStartTag.Text = reportTagList[i].RangeStartTag.Text.Replace(Marker + tagName + " Root " + culture.TwoLetterISOLanguageName + "\r", Marker + tagName + " Root " + culture.TwoLetterISOLanguageName + " not\r");
                    retStr = reportBaseService.CheckTagsFirstLineWord(reportTagList[i]);

                    int commentsCount = doc.Comments.Count;
                    doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                    doc.Close();
                    wordApp.Quit();

                    int FirstLineCount = 3;
                    Assert.AreEqual(1, commentsCount);
                    Assert.AreEqual(string.Format(ReportServiceRes._DoesNotContain_Items, Marker + tagName + " Root " + culture.TwoLetterISOLanguageName + " not",
                        FirstLineCount.ToString()) + "\r\n\r\n" +
                            string.Format(ReportServiceRes.Example_, Marker + "Start Root en"), retStr);
                }
            }
        }
        [TestMethod]
        public void ReportBaseService_FindTagsWord_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("bonjour " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "ShowStart Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name CONTAIN All");
                sb.AppendLine(Marker);
                sb.AppendLine("True section");
                sb.AppendLine(Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "ShowEnd Root " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

                Assert.AreEqual(0, commentsCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(2, reportTagList.Count);
                Assert.AreEqual("Start", reportTagList[0].TagName);
                Assert.AreEqual("Root", reportTagList[0].TagItem);
                Assert.AreEqual("ShowStart", reportTagList[1].TagName);
                Assert.AreEqual("Root", reportTagList[1].TagItem);
            }
        }
        [TestMethod]
        public void ReportBaseService_FindTagsWord_With_Table_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                doc.Range().Text = "";
                doc.Range().InsertAfter(Marker + "Start Root " + culture.TwoLetterISOLanguageName + "\r\n");
                doc.Range().InsertAfter("Root_Name" + "\r\n");
                doc.Range().InsertAfter(Marker + "\r\n");
                doc.Range().InsertAfter("Root_Name = " + Marker + "Root_Name" + Marker + "\r\n");

                range = doc.Range();
                range.Start = range.End;

                range.Tables.Add(Range: range, NumRows: 6, NumColumns: 5);
                doc.Tables[1].Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                doc.Tables[1].Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

                // doing row 2 column 1
                doc.Tables[1].Cell(1, 1).Merge(doc.Tables[1].Cell(1, 2));
                doc.Tables[1].Cell(1, 1).Merge(doc.Tables[1].Cell(1, 2));
                doc.Tables[1].Cell(1, 1).Merge(doc.Tables[1].Cell(1, 2));
                doc.Tables[1].Cell(1, 1).Merge(doc.Tables[1].Cell(1, 2));
                range = doc.Tables[1].Cell(1, 1).Range;
                range.Text = "Table Example";

                // doing row 2 column 1
                range = doc.Tables[1].Cell(2, 1).Range;
                range.Text = "Province";

                range = doc.Tables[1].Cell(2, 2).Range;
                range.Text = "Muni. Count";

                range = doc.Tables[1].Cell(2, 3).Range;
                range.Text = "Sample Count";

                range = doc.Tables[1].Cell(2, 4).Range;
                range.Text = "Lat";

                range = doc.Tables[1].Cell(2, 5).Range;
                range.Text = "Lng";


                // doing row 3 column 1
                range = doc.Tables[1].Cell(3, 1).Range;

                StringBuilder sbTable = new StringBuilder();
                sbTable.AppendLine(Marker + "LoopStart Province " + culture.TwoLetterISOLanguageName);
                sbTable.AppendLine("Province_Name ASCENDING 1");
                sbTable.AppendLine("Province_Stat_Municipality_Count");
                sbTable.AppendLine("Province_Stat_MWQM_Sample_Count");
                sbTable.AppendLine("Province_Lat");
                sbTable.AppendLine("Province_Lng");
                sbTable.AppendLine(Marker);
                range.Text = sbTable.ToString();

                // doing row 4 column 1
                range = doc.Tables[1].Cell(4, 1).Range;
                range.Text = Marker + "Province_Name" + Marker;

                range = doc.Tables[1].Cell(4, 2).Range;
                StringBuilder sbCond = new StringBuilder();
                sbCond.AppendLine(Marker + "Province_Stat_Municipality_Count" + Marker);
                sbCond.AppendLine(Marker + "ShowStart Province " + culture.TwoLetterISOLanguageName);
                sbCond.AppendLine("Province_Stat_Municipality_Count BIGGER 80");
                sbCond.AppendLine(Marker);
                sbCond.AppendLine("True section Muni count > 80 " + Marker + "Province_Stat_Municipality_Count" + Marker);
                sbCond.AppendLine(Marker + "ShowEnd Province " + culture.TwoLetterISOLanguageName + Marker);
                sbCond.AppendLine(Marker + "Province_Stat_Municipality_Count" + Marker);
                range.Text = sbCond.ToString();

                range = doc.Tables[1].Cell(4, 3).Range;

                range.Text = Marker + "Province_Stat_MWQM_Sample_Count" + Marker;
                sbCond = new StringBuilder();
                sbCond.AppendLine(Marker + "Province_Stat_MWQM_Sample_Count" + Marker);
                sbCond.AppendLine(Marker + "ShowStart Province " + culture.TwoLetterISOLanguageName);
                sbCond.AppendLine("Province_Stat_MWQM_Sample_Count BIGGER 80000");
                sbCond.AppendLine(Marker);
                sbCond.AppendLine("True section Muni count > 80 " + Marker + "Province_Stat_MWQM_Sample_Count" + Marker);
                sbCond.AppendLine(Marker + "ShowEnd Province " + culture.TwoLetterISOLanguageName + Marker);
                range.Text = sbCond.ToString();

                range = doc.Tables[1].Cell(4, 4).Range;
                range.Text = Marker + "Province_Lat F3" + Marker;

                range = doc.Tables[1].Cell(4, 5).Range;
                range.Text = Marker + "Province_Lng F2" + Marker;

                // doing row 5 column 1
                range = doc.Tables[1].Cell(5, 1).Range;
                doc.Tables[1].Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                doc.Tables[1].Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

                range.Text = Marker + "LoopEnd Province " + culture.TwoLetterISOLanguageName + Marker + "\r\n";

                // doing row 6 column 1
                range = doc.Tables[1].Cell(6, 1).Range;
                range.Text = "AAAA";

                range = doc.Tables[1].Cell(6, 2).Range;
                range.Text = "BBBBBB";

                range = doc.Tables[1].Cell(6, 3).Range;
                range.Text = "CCCCC";

                range = doc.Tables[1].Cell(6, 4).Range;
                range.Text = "DDDDD";

                range = doc.Tables[1].Cell(6, 5).Range;
                range.Text = "EEEEE";


                range = doc.Range();
                range.Start = range.End;

                range.InsertAfter(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker + "\r\n");

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

                Assert.AreEqual(0, commentsCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(4, reportTagList.Count);
                Assert.AreEqual("Start", reportTagList[0].TagName);
                Assert.AreEqual("Root", reportTagList[0].TagItem);
                Assert.AreEqual("LoopStart", reportTagList[1].TagName);
                Assert.AreEqual("Province", reportTagList[1].TagItem);
                Assert.AreEqual("ShowStart", reportTagList[2].TagName);
                Assert.AreEqual("Province", reportTagList[2].TagItem);
                Assert.AreEqual("ShowStart", reportTagList[3].TagName);
                Assert.AreEqual("Province", reportTagList[3].TagItem);
            }
        }
        [TestMethod]
        public void ReportBaseService_FindTagsWord_2Level_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("bonjour " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "ShowStart Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name CONTAIN All");
                sb.AppendLine(Marker);
                sb.AppendLine("True section");
                sb.AppendLine(Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "ShowEnd Root " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Country_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("bonjour " + Marker + "Country_Name" + Marker);
                sb.AppendLine(Marker + "LoopStart Province " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Province_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("bonjour " + Marker + "Province_Name" + Marker);
                sb.AppendLine(Marker + "LoopEnd Province " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

                Assert.AreEqual(0, commentsCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(4, reportTagList.Count);
                Assert.AreEqual("Start", reportTagList[0].TagName);
                Assert.AreEqual("Root", reportTagList[0].TagItem);
                Assert.AreEqual("ShowStart", reportTagList[1].TagName);
                Assert.AreEqual("Root", reportTagList[1].TagItem);
                Assert.AreEqual("LoopStart", reportTagList[2].TagName);
                Assert.AreEqual("Country", reportTagList[2].TagItem);
                Assert.AreEqual("LoopStart", reportTagList[3].TagName);
                Assert.AreEqual("Province", reportTagList[3].TagItem);

            }
        }
        [TestMethod]
        public void ReportBaseService_FindTagsWord__TagMustBeFollowedWithAReturn_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.Append(Marker);
                sb.AppendLine("bonjour " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "ShowStart Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name CONTAIN All");
                sb.AppendLine(Marker);
                sb.AppendLine("True section");
                sb.AppendLine(Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "ShowEnd Root " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes._TagMustBeFollowedWithAReturn, Marker + "Start" + "*" + Marker), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_FindTagsWord__TagMustBeFollowedWithAReturn2_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("bonjour " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "ShowStart Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name CONTAIN All");
                sb.Append(Marker);
                sb.AppendLine("True section");
                sb.AppendLine(Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "ShowEnd Root " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes._TagMustBeFollowedWithAReturn, Marker + "ShowStart" + "*" + Marker), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_FindTagsWord_Tag_NotWellFormedItShouldHaveAStructureLike__Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("bonjour " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "ShowStart Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name CONTAIN All");
                sb.AppendLine(Marker);
                sb.AppendLine("True section");
                sb.AppendLine(Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "ShowEnd Root " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "LoopStartCountry " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Country_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("bonjour " + Marker + "Country_Name" + Marker);
                sb.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Tag_NotWellFormedItShouldHaveAStructureLike_, "LoopStart", Marker + "LoopStart" + " Root en" + Marker), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_FindTagsWord_Tag_NotWellFormedItShouldHaveAStructureLike_2_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("bonjour " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "ShowStartRoot " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name CONTAIN All");
                sb.AppendLine(Marker);
                sb.AppendLine("True section");
                sb.AppendLine(Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "ShowEnd Root " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Tag_NotWellFormedItShouldHaveAStructureLike_, "ShowStart", Marker + "ShowStart" + " Root en" + Marker), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_FindTagsWord_CouldNotFindEndTag_Of_Start_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("bonjour " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "ShowStart Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name CONTAIN All");
                sb.AppendLine(Marker);
                sb.AppendLine("True section");
                sb.AppendLine(Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "ShowEnd Root " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "NotEnd Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                wordApp.Visible = WordVisibleWhenTesting;

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

                Assert.AreEqual(1, commentsCount);
                StringBuilder sbStartTag = new StringBuilder(Marker + "Start Root " + culture.TwoLetterISOLanguageName + "\r\n" +
                     "Root_Name" + "\r\n" +
                     Marker + "\r\n");
                Assert.AreEqual(string.Format(ReportServiceRes.CouldNotFindEndTag_Of_, Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker, sbStartTag.Replace("\n", "").ToString()), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_FindTagsWord_CouldNotFindEndTag_Of_LoopStart_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("bonjour " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "ShowStart Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name CONTAIN All");
                sb.AppendLine(Marker);
                sb.AppendLine("True section");
                sb.AppendLine(Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "ShowEnd Root " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Country_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("bonjour " + Marker + "Country_Name" + Marker);
                sb.AppendLine(Marker + "NotLoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                wordApp.Visible = WordVisibleWhenTesting;

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

                Assert.AreEqual(1, commentsCount);
                StringBuilder sbStartTag = new StringBuilder(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName + "\r\n" +
                     "Country_Name" + "\r\n" +
                     Marker + "\r\n");
                Assert.AreEqual(string.Format(ReportServiceRes.CouldNotFindEndTag_Of_, Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker, sbStartTag.Replace("\n", "").ToString()), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_FindTagsWord_CouldNotFindEndTag_Level2_Of_Loop_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("bonjour " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Country_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("bonjour " + Marker + "Country_Name" + Marker);
                sb.AppendLine(Marker + "ShowStart Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name CONTAIN All");
                sb.AppendLine(Marker);
                sb.AppendLine("True section");
                sb.AppendLine(Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "ShowEnd Root " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "LoopStart Province " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Province_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("bonjour " + Marker + "Province_Name" + Marker);
                sb.AppendLine(Marker + "NotLoopEnd Province " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                wordApp.Visible = WordVisibleWhenTesting;

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

                Assert.AreEqual(1, commentsCount);
                StringBuilder sbStartTag = new StringBuilder(Marker + "LoopStart Province " + culture.TwoLetterISOLanguageName + "\r\n" +
                     "Province_Name" + "\r\n" +
                     Marker + "\r\n");
                Assert.AreEqual(string.Format(ReportServiceRes.CouldNotFindEndTag_Of_, Marker + "LoopEnd Province " + culture.TwoLetterISOLanguageName + Marker, sbStartTag.Replace("\n", "").ToString()), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_FindTagsWord_CouldNotFindEndTag_Level2_2_Of_Loop_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("bonjour " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "ShowStart Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name CONTAIN All");
                sb.AppendLine(Marker);
                sb.AppendLine("True section");
                sb.AppendLine(Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "ShowEnd Root " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Country_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("bonjour " + Marker + "Country_Name" + Marker);
                sb.AppendLine(Marker + "LoopStart Province " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Province_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("bonjour " + Marker + "Province_Name" + Marker);
                //sb.AppendLine(Marker + "NotLoopEnd Province " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                wordApp.Visible = WordVisibleWhenTesting;

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

                Assert.AreEqual(1, commentsCount);
                StringBuilder sbStartTag = new StringBuilder(Marker + "LoopStart Province " + culture.TwoLetterISOLanguageName + "\r\n" +
                     "Province_Name" + "\r\n" +
                     Marker + "\r\n");
                Assert.AreEqual(string.Format(ReportServiceRes.CouldNotFindEndTag_Of_, Marker + "LoopEnd Province " + culture.TwoLetterISOLanguageName + Marker, sbStartTag.Replace("\n", "").ToString()), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_FindTagsWord_CouldNotFindEndTag_In_ShowStart_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("bonjour " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "ShowStart Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name CONTAIN All");
                sb.AppendLine(Marker);
                sb.AppendLine("True section");
                sb.AppendLine(Marker + "Root_Name" + Marker);
                //sb.AppendLine(Marker + "ShowEnd Root " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                wordApp.Visible = WordVisibleWhenTesting;

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

                Assert.AreEqual(1, commentsCount);
                StringBuilder sbStartTag = new StringBuilder(Marker + "ShowStart Root " + culture.TwoLetterISOLanguageName + "\r\n" +
                     "Root_Name CONTAIN All" + "\r\n" +
                     Marker + "\r\n");
                Assert.AreEqual(string.Format(ReportServiceRes.CouldNotFindEndTag_Of_, Marker + "ShowEnd Root " + culture.TwoLetterISOLanguageName + Marker, sbStartTag.Replace("\n", "").ToString()), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_FindTagsWord__TagMustBeFollowedWithAReturn_Start_End_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("bonjour " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "ShowStart Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name CONTAIN All");
                sb.AppendLine(Marker);
                sb.AppendLine("True section");
                sb.AppendLine(Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "ShowEnd Root " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker + "sleifj");

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                wordApp.Visible = WordVisibleWhenTesting;

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes._TagMustBeFollowedWithAReturn, Marker + ("Start" == "Loop" ? "Loop" : "") + "End Root " + culture.TwoLetterISOLanguageName + Marker), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_FindTagsWord__TagMustBeFollowedWithAReturn_Loop_End_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("bonjour " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Country_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("bonjour " + Marker + "Country_Name" + Marker);
                sb.AppendLine(Marker + "ShowStart Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name CONTAIN All");
                sb.AppendLine(Marker);
                sb.AppendLine("True section");
                sb.AppendLine(Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "ShowEnd Root " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker + "lseijf");
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                wordApp.Visible = WordVisibleWhenTesting;

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes._TagMustBeFollowedWithAReturn, Marker + ("Loop" == "Loop" ? "Loop" : "") + "End Country " + culture.TwoLetterISOLanguageName + Marker), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_FindTagsWord_FirstTagInDocumentMustBeAStartTag_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "LoopStart Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "LoopEnd Root " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(ReportServiceRes.FirstTagInDocumentMustBeAStartTag, retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_FindTagsWord_StartTagMustNotResideInATable_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Tables.Add(Range: doc.Range(), NumRows: 1, NumColumns: 1);

                range = doc.Tables[1].Cell(1, 1).Range;
                doc.Tables[1].Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                doc.Tables[1].Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                range.Text = sb.ToString();


                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

                Assert.AreEqual(1, commentsCount);
            }
        }
        [TestMethod]
        public void ReportBaseService_FindTagsWord_Children_Start_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Country_Name");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "LoopStart Province " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Province_Name");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "Province_Name" + Marker);
                sb.AppendLine(Marker + "LoopEnd Province " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "Country_Name" + Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = 1,
                    Take = 2
                };

                List<ReportTag> reportTagChildrenList = new List<ReportTag>();

                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagChildrenList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(0, commentsCount);

                range = doc.Range();
                Assert.AreEqual(1, reportTagChildrenList.Count);
                Assert.AreEqual("Start", reportTagChildrenList[0].TagName);
                Assert.AreEqual("Root", reportTagChildrenList[0].TagItem);
                Assert.AreEqual(2, reportTagChildrenList[0].Take);
                Assert.AreEqual(1, reportTagChildrenList[0].UnderTVItemID);
                Assert.IsNotNull(reportTagChildrenList[0].RangeStartTag);
                Assert.AreNotEqual(reportTagChildrenList[0].RangeStartTag.Start, reportTagChildrenList[0].RangeStartTag.End);
                Assert.IsNotNull(reportTagChildrenList[0].RangeEndTag);
                Assert.AreNotEqual(reportTagChildrenList[0].RangeEndTag.Start, reportTagChildrenList[0].RangeEndTag.End);
                Assert.IsNotNull(reportTagChildrenList[0].RangeInnerTag);
                Assert.AreNotEqual(reportTagChildrenList[0].RangeInnerTag.Start, reportTagChildrenList[0].RangeInnerTag.End);
                Assert.AreEqual("Root_Name", reportTagChildrenList[0].ReportTreeNodeList[0].Text);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

            }
        }
        [TestMethod]
        public void ReportBaseService_FindTagsWord_Children_LoopStart_2_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Country_Name");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "LoopStart Province " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Province_Name");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "Province_Name" + Marker);
                sb.AppendLine(Marker + "LoopEnd Province " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "Country_Name" + Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                    doc.Close();
                    wordApp.Quit();
                    Assert.IsTrue(false);
                }

                List<ReportTag> reportTagChildrenList = new List<ReportTag>();

                reportTagList[0].OnlyImmediateChildren = true;
                int Take = 2;
                retStr = reportBaseService.FindTagsWord(reportTagList[0], reportTagChildrenList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(0, commentsCount);

                range = doc.Range();
                Assert.AreEqual(1, reportTagChildrenList.Count);
                Assert.AreEqual("LoopStart", reportTagChildrenList[0].TagName);
                Assert.AreEqual("Country", reportTagChildrenList[0].TagItem);
                Assert.AreEqual(Take, reportTagChildrenList[0].Take);
                Assert.AreEqual(1, reportTagChildrenList[0].UnderTVItemID);
                Assert.IsNotNull(reportTagChildrenList[0].RangeStartTag);
                Assert.AreNotEqual(reportTagChildrenList[0].RangeStartTag.Start, reportTagChildrenList[0].RangeStartTag.End);
                Assert.IsNotNull(reportTagChildrenList[0].RangeEndTag);
                Assert.AreNotEqual(reportTagChildrenList[0].RangeEndTag.Start, reportTagChildrenList[0].RangeEndTag.End);
                Assert.IsNotNull(reportTagChildrenList[0].RangeInnerTag);
                Assert.AreNotEqual(reportTagChildrenList[0].RangeInnerTag.Start, reportTagChildrenList[0].RangeInnerTag.End);
                Assert.AreEqual("Country_Name", reportTagChildrenList[0].ReportTreeNodeList[0].Text);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

            }
        }
        [TestMethod]
        public void ReportBaseService_FindTagsWord_Children_LoopStart3_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Country_Name");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "LoopStart Province " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Province_Name");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "Province_Name" + Marker);
                sb.AppendLine(Marker + "LoopEnd Province " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "Country_Name" + Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                    doc.Close();
                    wordApp.Quit();
                    Assert.IsTrue(false);
                }

                List<ReportTag> reportTagChildrenList = new List<ReportTag>();

                reportTagList[1].OnlyImmediateChildren = true;
                int Take = 2;
                retStr = reportBaseService.FindTagsWord(reportTagList[1], reportTagChildrenList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(0, commentsCount);

                range = doc.Range();
                Assert.AreEqual(1, reportTagChildrenList.Count);
                Assert.AreEqual("LoopStart", reportTagChildrenList[0].TagName);
                Assert.AreEqual("Province", reportTagChildrenList[0].TagItem);
                Assert.AreEqual(Take, reportTagChildrenList[0].Take);
                Assert.AreEqual(1, reportTagChildrenList[0].UnderTVItemID);
                Assert.IsNotNull(reportTagChildrenList[0].RangeStartTag);
                Assert.AreNotEqual(reportTagChildrenList[0].RangeStartTag.Start, reportTagChildrenList[0].RangeStartTag.End);
                Assert.IsNotNull(reportTagChildrenList[0].RangeEndTag);
                Assert.AreNotEqual(reportTagChildrenList[0].RangeEndTag.Start, reportTagChildrenList[0].RangeEndTag.End);
                Assert.IsNotNull(reportTagChildrenList[0].RangeInnerTag);
                Assert.AreNotEqual(reportTagChildrenList[0].RangeInnerTag.Start, reportTagChildrenList[0].RangeInnerTag.End);
                Assert.AreEqual("Province_Name", reportTagChildrenList[0].ReportTreeNodeList[0].Text);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

            }
        }
        [TestMethod]
        public void ReportBaseService_FindTagsWord_Children_Start_With_Table_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                doc.Range().Text = "";
                doc.Range().InsertAfter(Marker + "Start Root " + culture.TwoLetterISOLanguageName + "\r\n");
                doc.Range().InsertAfter("Root_Name" + "\r\n");
                doc.Range().InsertAfter(Marker + "\r\n");
                doc.Range().InsertAfter("Root_Name = " + Marker + "Root_Name" + Marker + "\r\n");

                range = doc.Range();
                range.Start = range.End;

                range.Tables.Add(Range: range, NumRows: 3, NumColumns: 3);
                doc.Tables[1].Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                doc.Tables[1].Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

                // doing row 1 column 1
                range = doc.Tables[1].Cell(1, 1).Range;

                StringBuilder sbTable = new StringBuilder();
                sbTable.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sbTable.AppendLine("Country_Name");
                sbTable.AppendLine(Marker);
                range.Text = sbTable.ToString();

                // doing row 2 column 1
                range = doc.Tables[1].Cell(2, 1).Range;
                range.Text = "Country_Name = " + Marker + "Country_Name" + Marker;

                // doing row 3 column 1
                range = doc.Tables[1].Cell(3, 1).Range;
                range.Text = Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker + "\r\n";

                range = doc.Range();
                range.Start = range.End;

                range.InsertAfter(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker + "\r\n");

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = 1,
                    Take = 2
                };

                List<ReportTag> reportTagChildrenList = new List<ReportTag>();
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagChildrenList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(0, commentsCount);

                range = doc.Range();
                Assert.AreEqual(1, reportTagChildrenList.Count);
                Assert.AreEqual("Start", reportTagChildrenList[0].TagName);
                Assert.AreEqual("Root", reportTagChildrenList[0].TagItem);
                Assert.AreEqual(2, reportTagChildrenList[0].Take);
                Assert.AreEqual(1, reportTagChildrenList[0].UnderTVItemID);
                Assert.IsNotNull(reportTagChildrenList[0].RangeStartTag);
                Assert.AreNotEqual(reportTagChildrenList[0].RangeStartTag.Start, reportTagChildrenList[0].RangeStartTag.End);
                Assert.IsNotNull(reportTagChildrenList[0].RangeEndTag);
                Assert.AreNotEqual(reportTagChildrenList[0].RangeEndTag.Start, reportTagChildrenList[0].RangeEndTag.End);
                Assert.IsNotNull(reportTagChildrenList[0].RangeInnerTag);
                Assert.AreNotEqual(reportTagChildrenList[0].RangeInnerTag.Start, reportTagChildrenList[0].RangeInnerTag.End);
                Assert.AreEqual("Root_Name", reportTagChildrenList[0].ReportTreeNodeList[0].Text);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

            }
        }
        [TestMethod]
        public void ReportBaseService_FindTagsWord_Children_LoopStart_With_Table2_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                doc.Range().Text = "";
                doc.Range().InsertAfter(Marker + "Start Root " + culture.TwoLetterISOLanguageName + "\r\n");
                doc.Range().InsertAfter("Root_Name" + "\r\n");
                doc.Range().InsertAfter(Marker + "\r\n");
                doc.Range().InsertAfter("Root_Name = " + Marker + "Root_Name" + Marker + "\r\n");

                range = doc.Range();
                range.Start = range.End;

                range.Tables.Add(Range: range, NumRows: 3, NumColumns: 3);
                doc.Tables[1].Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                doc.Tables[1].Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

                // doing row 1 column 1
                range = doc.Tables[1].Cell(1, 1).Range;

                StringBuilder sbTable = new StringBuilder();
                sbTable.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sbTable.AppendLine("Country_Name");
                sbTable.AppendLine(Marker);
                range.Text = sbTable.ToString();

                // doing row 2 column 1
                range = doc.Tables[1].Cell(2, 1).Range;
                range.Text = "Country_Name = " + Marker + "Country_Name" + Marker;

                // doing row 3 column 1
                range = doc.Tables[1].Cell(3, 1).Range;
                range.Text = Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker + "\r\n";

                range = doc.Range();
                range.Start = range.End;

                range.InsertAfter(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker + "\r\n");

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                    doc.Close();
                    wordApp.Quit();
                    Assert.IsTrue(false);
                }

                foreach (ReportTag reportTag in reportTagList)
                {
                    reportTag.Take = 2;
                }

                List<ReportTag> reportTagChildrenList = new List<ReportTag>();

                reportTagList[0].OnlyImmediateChildren = true;
                int Take = 2;
                retStr = reportBaseService.FindTagsWord(reportTagList[0], reportTagChildrenList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(0, commentsCount);

                range = doc.Range();
                Assert.AreEqual(1, reportTagChildrenList.Count);
                Assert.AreEqual("LoopStart", reportTagChildrenList[0].TagName);
                Assert.AreEqual("Country", reportTagChildrenList[0].TagItem);
                Assert.AreEqual(Take, reportTagChildrenList[0].Take);
                Assert.AreEqual(1, reportTagChildrenList[0].UnderTVItemID);
                Assert.IsNotNull(reportTagChildrenList[0].RangeStartTag);
                Assert.AreNotEqual(reportTagChildrenList[0].RangeStartTag.Start, reportTagChildrenList[0].RangeStartTag.End);
                Assert.IsNotNull(reportTagChildrenList[0].RangeEndTag);
                Assert.AreNotEqual(reportTagChildrenList[0].RangeEndTag.Start, reportTagChildrenList[0].RangeEndTag.End);
                Assert.IsNotNull(reportTagChildrenList[0].RangeInnerTag);
                Assert.AreNotEqual(reportTagChildrenList[0].RangeInnerTag.Start, reportTagChildrenList[0].RangeInnerTag.End);
                Assert.AreEqual("Country_Name", reportTagChildrenList[0].ReportTreeNodeList[0].Text);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

            }
        }
        [TestMethod]
        public void ReportBaseService_FindTagsWord_Children_Start_Multiple_Start_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Country_Name");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "LoopStart Province " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Province_Name");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "Province_Name" + Marker);
                sb.AppendLine(Marker + "LoopEnd Province " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "Country_Name" + Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Country_Name");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "LoopStart Province " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Province_Name");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "Province_Name" + Marker);
                sb.AppendLine(Marker + "LoopEnd Province " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "Country_Name" + Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Country_Name");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "LoopStart Province " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Province_Name");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "Province_Name" + Marker);
                sb.AppendLine(Marker + "LoopEnd Province " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "Country_Name" + Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = 1,
                    Take = 2
                };

                List<ReportTag> reportTagChildrenList = new List<ReportTag>();
                int Take = 2;

                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagChildrenList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(0, commentsCount);

                range = doc.Range();
                Assert.AreEqual(3, reportTagChildrenList.Count);
                Assert.AreEqual("Start", reportTagChildrenList[0].TagName);
                Assert.AreEqual("Root", reportTagChildrenList[0].TagItem);
                Assert.AreEqual("Root", reportTagChildrenList[1].TagItem);
                Assert.AreEqual("Root", reportTagChildrenList[2].TagItem);
                Assert.AreEqual(Take, reportTagChildrenList[0].Take);
                Assert.AreEqual(1, reportTagChildrenList[0].UnderTVItemID);
                Assert.IsNotNull(reportTagChildrenList[0].RangeStartTag);
                Assert.AreNotEqual(reportTagChildrenList[0].RangeStartTag.Start, reportTagChildrenList[0].RangeStartTag.End);
                Assert.IsNotNull(reportTagChildrenList[0].RangeEndTag);
                Assert.AreNotEqual(reportTagChildrenList[0].RangeEndTag.Start, reportTagChildrenList[0].RangeEndTag.End);
                Assert.IsNotNull(reportTagChildrenList[0].RangeInnerTag);
                Assert.AreNotEqual(reportTagChildrenList[0].RangeInnerTag.Start, reportTagChildrenList[0].RangeInnerTag.End);
                Assert.AreEqual("Root_Name", reportTagChildrenList[0].ReportTreeNodeList[0].Text);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

            }
        }
        [TestMethod]
        public void ReportBaseService_FindTagsWord_Children_LoopStart_Multiple_LoopStart_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Country_Name");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "LoopStart Province " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Province_Name");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "Province_Name" + Marker);
                sb.AppendLine(Marker + "LoopEnd Province " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "Country_Name" + Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Country_Name");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "LoopStart Province " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Province_Name");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "Province_Name" + Marker);
                sb.AppendLine(Marker + "LoopEnd Province " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "Country_Name" + Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Country_Name");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "LoopStart Province " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Province_Name");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "Province_Name" + Marker);
                sb.AppendLine(Marker + "LoopEnd Province " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "Country_Name" + Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                    doc.Close();
                    wordApp.Quit();
                    Assert.IsTrue(false);
                }

                foreach (ReportTag reportTag in reportTagList)
                {
                    reportTag.Take = 2;
                }

                reportTagList[0].OnlyImmediateChildren = true;

                List<ReportTag> reportTagChildrenList = new List<ReportTag>();

                int Take = 2;
                retStr = reportBaseService.FindTagsWord(reportTagList[0], reportTagChildrenList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(0, commentsCount);

                range = doc.Range();
                Assert.AreEqual(3, reportTagChildrenList.Count);
                Assert.AreEqual("LoopStart", reportTagChildrenList[0].TagName);
                Assert.AreEqual("Country", reportTagChildrenList[0].TagItem);
                Assert.AreEqual("Country", reportTagChildrenList[1].TagItem);
                Assert.AreEqual("Country", reportTagChildrenList[2].TagItem);
                Assert.AreEqual(Take, reportTagChildrenList[0].Take);
                Assert.AreEqual(1, reportTagChildrenList[0].UnderTVItemID);
                Assert.IsNotNull(reportTagChildrenList[0].RangeStartTag);
                Assert.AreNotEqual(reportTagChildrenList[0].RangeStartTag.Start, reportTagChildrenList[0].RangeStartTag.End);
                Assert.IsNotNull(reportTagChildrenList[0].RangeEndTag);
                Assert.AreNotEqual(reportTagChildrenList[0].RangeEndTag.Start, reportTagChildrenList[0].RangeEndTag.End);
                Assert.IsNotNull(reportTagChildrenList[0].RangeInnerTag);
                Assert.AreNotEqual(reportTagChildrenList[0].RangeInnerTag.Start, reportTagChildrenList[0].RangeInnerTag.End);
                Assert.AreEqual("Country_Name", reportTagChildrenList[0].ReportTreeNodeList[0].Text);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

            }
        }
        [TestMethod]
        public void ReportBaseService_FindTagsWord_Children_ShowStart_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker + "");
                sb.AppendLine(Marker + "ShowStart Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name CONTAIN All");
                sb.AppendLine(Marker + "");
                sb.AppendLine("True section");
                sb.AppendLine(Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "ShowEnd Root " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                    doc.Close();
                    wordApp.Quit();
                    Assert.IsTrue(false);
                }

                foreach (ReportTag reportTag in reportTagList)
                {
                    reportTag.Take = 2;
                }

                List<ReportTag> reportTagShowStartList = new List<ReportTag>();

                reportTagList[0].OnlyImmediateChildren = true;

                int Take = 2;
                retStr = reportBaseService.FindTagsWord(reportTagList[0], reportTagShowStartList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(0, commentsCount);

                range = doc.Range();
                Assert.AreEqual(1, reportTagShowStartList.Count);
                Assert.AreEqual("ShowStart", reportTagShowStartList[0].TagName);
                Assert.AreEqual("Root", reportTagShowStartList[0].TagItem);
                Assert.AreEqual(Take, reportTagShowStartList[0].Take);
                Assert.AreEqual(1, reportTagShowStartList[0].UnderTVItemID);
                Assert.IsNotNull(reportTagShowStartList[0].RangeStartTag);
                Assert.AreNotEqual(reportTagShowStartList[0].RangeStartTag.Start, reportTagShowStartList[0].RangeStartTag.End);
                Assert.IsNotNull(reportTagShowStartList[0].RangeEndTag);
                Assert.AreNotEqual(reportTagShowStartList[0].RangeEndTag.Start, reportTagShowStartList[0].RangeEndTag.End);
                Assert.IsNotNull(reportTagShowStartList[0].RangeInnerTag);
                Assert.AreNotEqual(reportTagShowStartList[0].RangeInnerTag.Start, reportTagShowStartList[0].RangeInnerTag.End);
                Assert.AreEqual("Root_Name", reportTagShowStartList[0].ReportTreeNodeList[0].Text);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

            }
        }
        [TestMethod]
        public void ReportBaseService_FindTagsWord_Children_ShowStart_With_Table_Good2_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                doc.Range().Text = "";
                doc.Range().InsertAfter(Marker + "Start Root " + culture.TwoLetterISOLanguageName + "\r\n");
                doc.Range().InsertAfter("Root_Name" + "\r\n");
                doc.Range().InsertAfter(Marker + "\r\n");
                doc.Range().InsertAfter("Root_Name = " + Marker + "Root_Name" + Marker + "\r\n");

                range = doc.Range();
                range.Start = range.End;

                range.Tables.Add(Range: range, NumRows: 3, NumColumns: 3);
                doc.Tables[1].Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                doc.Tables[1].Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

                // doing row 1 column 1
                range = doc.Tables[1].Cell(1, 1).Range;

                StringBuilder sbTable = new StringBuilder();
                sbTable.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sbTable.AppendLine("Country_Name");
                sbTable.AppendLine(Marker);
                range.Text = sbTable.ToString();

                // doing row 2 column 1
                range = doc.Tables[1].Cell(2, 1).Range;
                StringBuilder sbCond = new StringBuilder();
                sbCond.AppendLine("Country_Name = " + Marker + "Country_Name" + Marker);
                sbCond.AppendLine(Marker + "ShowStart Country " + culture.TwoLetterISOLanguageName);
                sbCond.AppendLine("Country_Name CONTAIN All");
                sbCond.AppendLine(Marker + "");
                sbCond.AppendLine("True section");
                sbCond.AppendLine(Marker + "Country_Name" + Marker);
                sbCond.AppendLine(Marker + "ShowEnd Country " + culture.TwoLetterISOLanguageName + Marker);

                range.Text = sbCond.ToString();

                // doing row 3 column 1
                range = doc.Tables[1].Cell(3, 1).Range;
                range.Text = Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker + "\r\n";

                range = doc.Range();
                range.Start = range.End;

                range.InsertAfter(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker + "\r\n");

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                    doc.Close();
                    wordApp.Quit();
                    Assert.IsTrue(false);
                }

                foreach (ReportTag reportTag in reportTagList)
                {
                    reportTag.Take = 2;
                }

                List<ReportTag> reportTagShowStartList = new List<ReportTag>();

                reportTagList[1].OnlyImmediateChildren = true;

                int Take = 2;
                retStr = reportBaseService.FindTagsWord(reportTagList[1], reportTagShowStartList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(0, commentsCount);

                range = doc.Range();
                Assert.AreEqual(1, reportTagShowStartList.Count);
                Assert.AreEqual("ShowStart", reportTagShowStartList[0].TagName);
                Assert.AreEqual("Country", reportTagShowStartList[0].TagItem);
                Assert.AreEqual(Take, reportTagShowStartList[0].Take);
                Assert.AreEqual(1, reportTagShowStartList[0].UnderTVItemID);
                Assert.IsNotNull(reportTagShowStartList[0].RangeStartTag);
                Assert.AreNotEqual(reportTagShowStartList[0].RangeStartTag.Start, reportTagShowStartList[0].RangeStartTag.End);
                Assert.IsNotNull(reportTagShowStartList[0].RangeEndTag);
                Assert.AreNotEqual(reportTagShowStartList[0].RangeEndTag.Start, reportTagShowStartList[0].RangeEndTag.End);
                Assert.IsNotNull(reportTagShowStartList[0].RangeInnerTag);
                Assert.AreNotEqual(reportTagShowStartList[0].RangeInnerTag.Start, reportTagShowStartList[0].RangeInnerTag.End);
                Assert.AreEqual("Country_Name", reportTagShowStartList[0].ReportTreeNodeList[0].Text);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

            }
        }
        [TestMethod]
        public void ReportBaseService_FillTemplateWithDBInfoWord_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine("Root_Lat FORMAT F2");
                sb.AppendLine(Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine("Root_Lat = " + Marker + "Root_Lat" + Marker + "a");
                sb.AppendLine("Root_Lat = " + Marker + "Root_Lat FORMAT F4" + Marker + "b");
                sb.AppendLine("Root_LatAll = " + Marker + "Root_Name START All" + Marker + " hhhhh");
                sb.AppendLine("Root_LatTous = " + Marker + "Root_Name START Tous" + Marker + " mmmmm");
                sb.AppendLine(Marker + "ShowStart Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name START All");
                sb.AppendLine(Marker);
                sb.AppendLine("Root_NameTestTrueEN = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "ShowEnd Root " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "ShowStart Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name START Tous");
                sb.AppendLine(Marker);
                sb.AppendLine("Root_NameTestTrueFR = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "ShowEnd Root " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = 1,
                    TagItem = "Root",
                    Take = 2
                };
                string retStr = reportBaseService.FillTemplateWithDBInfoWord(reportTagStart);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(0, commentsCount);

                range = doc.Range();
                Assert.IsFalse(range.Text.Contains(Marker));
                Assert.IsTrue(range.Text.Contains("Root_Name = " + (culture.TwoLetterISOLanguageName == "fr" ? "Tous les endroits" : "All locations")));
                Assert.IsTrue(range.Text.Contains("Root_Lat = " + (culture.TwoLetterISOLanguageName == "fr" ? "50.00" : "50.00") + "a"));
                Assert.IsTrue(range.Text.Contains("Root_Lat = " + (culture.TwoLetterISOLanguageName == "fr" ? "50.0000" : "50.0000") + "b"));
                if (culture.TwoLetterISOLanguageName == "fr")
                {
                    Assert.IsTrue(range.Text.Contains("Root_NameTestTrueFR"));
                    Assert.IsTrue(range.Text.Contains("Root_LatTous = Tous les endroits"));

                    Assert.IsFalse(range.Text.Contains("Root_NameTestTrueEN"));
                    Assert.IsFalse(range.Text.Contains("Root_LatAll = All locations"));
                }
                else
                {
                    Assert.IsTrue(range.Text.Contains("Root_NameTestTrueEN"));
                    Assert.IsTrue(range.Text.Contains("Root_LatAll = All locations"));

                    Assert.IsFalse(range.Text.Contains("Root_NameTestTrueFR"));
                    Assert.IsFalse(range.Text.Contains("Root_LatTous = Tous les endroits"));
                }

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

            }
        }
        [TestMethod]
        public void ReportBaseService_FillTemplateWithDBInfoWord_Good_Item_Without_Variable_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Country_Name CONTAIN Can");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "Country_Name" + Marker);
                sb.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = 1,
                    TagItem = "Root",
                    Take = 2
                };
                string retStr = reportBaseService.FillTemplateWithDBInfoWord(reportTagStart);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(0, commentsCount);

                range = doc.Range();
                Assert.IsFalse(range.Text.Contains(Marker));
                Assert.IsTrue(range.Text.Contains("Canada"));

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

            }
        }
        [TestMethod]
        public void ReportBaseService_FillTemplateWithDBInfoWord2_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Country_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("\t" + Marker + "Country_Name" + Marker);
                sb.AppendLine(Marker + "LoopStart Province " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Province_Name ASCENDING 1");
                sb.AppendLine(Marker);
                sb.AppendLine("\t" + "\t" + Marker + "Province_Name" + Marker);
                sb.AppendLine(Marker + "LoopEnd Province " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = 1,
                    Take = 8
                };
                string retStr = reportBaseService.FillTemplateWithDBInfoWord(reportTagStart);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(0, commentsCount);

                range = doc.Range();
                Assert.IsFalse(range.Text.Contains(Marker));
                Assert.IsTrue(range.Text.Contains((culture.TwoLetterISOLanguageName == "fr" ? "Tous les endroits" : "All locations")));
                Assert.IsTrue(range.Text.Contains("Canada"));
                Assert.IsTrue(range.Text.Contains((culture.TwoLetterISOLanguageName == "fr" ? "Nouveau-Brunswick" : "New Brunswick")));

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

            }
        }
        [TestMethod]
        public void ReportBaseService_FillTemplateWithDBInfoWord3_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Country_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("\t" + Marker + "Country_Name" + Marker);
                sb.AppendLine(Marker + "LoopStart Province " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Province_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("\t" + "\t" + Marker + "Province_Name" + Marker);
                sb.AppendLine(Marker + "LoopStart Area " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Area_Name_Long");
                sb.AppendLine(Marker);
                sb.AppendLine("\t" + "\t" + "\t" + Marker + "Area_Name_Long" + Marker);
                sb.AppendLine(Marker + "LoopStart Sector " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Sector_Name_Long");
                sb.AppendLine(Marker);
                sb.AppendLine("\t" + "\t" + "\t" + "\t" + Marker + "Sector_Name_Long" + Marker);
                sb.AppendLine(Marker + "LoopStart Subsector " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Subsector_Name_Long");
                sb.AppendLine("Subsector_Stat_MWQM_Site_Count");
                sb.AppendLine(Marker);
                sb.AppendLine("\t" + "\t" + "\t" + "\t" + "\t" + Marker + "Subsector_Name_Long" + Marker + "\t" + Marker + "Subsector_Stat_MWQM_Site_Count" + Marker);
                sb.AppendLine(Marker + "LoopEnd Subsector " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "LoopEnd Sector " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "LoopEnd Area " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "LoopEnd Province " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FillTemplateWithDBInfoWord(reportTagStart);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(0, commentsCount);

                range = doc.Range();
                Assert.IsFalse(range.Text.Contains(Marker));
                Assert.IsTrue(range.Text.Contains((culture.TwoLetterISOLanguageName == "fr" ? "Tous les endroits" : "All locations")));
                Assert.IsTrue(range.Text.Contains("Canada"));
                Assert.IsTrue(range.Text.Contains((culture.TwoLetterISOLanguageName == "fr" ? "Nouveau-Brunswick" : "New Brunswick")));

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

            }
        }
        [TestMethod]
        public void ReportBaseService_FillTemplateWithDBInfoWord_with_report_ShowStart_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Country_Name");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "ShowStart Country " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Country_Name CONTAIN Cana");
                sb.AppendLine(Marker);
                sb.AppendLine("True section " + Marker + "Country_Name" + Marker);
                sb.AppendLine(Marker + "ShowEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FillTemplateWithDBInfoWord(reportTagStart);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(0, commentsCount);

                range = doc.Range();
                Assert.IsFalse(range.Text.Contains(Marker));
                Assert.IsTrue(range.Text.Contains("Root_Name = " + (culture.TwoLetterISOLanguageName == "fr" ? "Tous les endroits" : "All locations")));
                Assert.IsTrue(range.Text.Contains("True section Canada"));

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

            }
        }
        [TestMethod]
        public void ReportBaseService_FillTemplateWithDBInfoWord_with_2_Start_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("aaaaa = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("bbbbbbbbb = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FillTemplateWithDBInfoWord(reportTagStart);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(0, commentsCount);

                range = doc.Range();
                Assert.IsFalse(range.Text.Contains(Marker));
                Assert.IsTrue(range.Text.Contains("aaaaa = " + (culture.TwoLetterISOLanguageName == "fr" ? "Tous les endroits" : "All locations")));
                Assert.IsTrue(range.Text.Contains("bbbbbbbbb = " + (culture.TwoLetterISOLanguageName == "fr" ? "Tous les endroits" : "All locations")));

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

            }
        }
        [TestMethod]
        public void ReportBaseService_FillTemplateWithDBInfoWord_with_2_LoopStart_Under_One_Start_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("aaaaa = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Country_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("\tbbbbb = " + Marker + "Country_Name" + Marker);
                sb.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Country_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("\t\tccccc = " + Marker + "Country_Name" + Marker);
                sb.AppendLine(Marker + "LoopStart Province " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Province_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("\t\t\tddddd = " + Marker + "Province_Name" + Marker);
                sb.AppendLine(Marker + "LoopEnd Province " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FillTemplateWithDBInfoWord(reportTagStart);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(0, commentsCount);

                range = doc.Range();
                Assert.IsFalse(range.Text.Contains(Marker));
                Assert.IsTrue(range.Text.Contains("aaaaa = " + (culture.TwoLetterISOLanguageName == "fr" ? "Tous les endroits" : "All locations")));
                Assert.IsTrue(range.Text.Contains("bbbbb = " + "Canada"));
                Assert.IsTrue(range.Text.Contains("ccccc = " + "Canada"));
                Assert.IsTrue(range.Text.Contains("ddddd = " + (culture.TwoLetterISOLanguageName == "fr" ? "Nouveau-Brunswick" : "New Brunswick")));

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

            }
        }
        [TestMethod]
        public void ReportBaseService_FillTemplateWithDBInfoWord_with_3_LoopStart_Under_One_Start_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("aaaaa = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Country_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("\tbbbbb = " + Marker + "Country_Name" + Marker);
                sb.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Country_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("\t\tccccc = " + Marker + "Country_Name" + Marker);
                sb.AppendLine(Marker + "LoopStart Province " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Province_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("\t\t\tddddd = " + Marker + "Province_Name" + Marker);
                sb.AppendLine(Marker + "LoopStart Subsector " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Subsector_Name_Short");
                sb.AppendLine(Marker);
                sb.AppendLine("\t\t\t\teeee = " + Marker + "Subsector_Name_Short" + Marker);
                sb.AppendLine(Marker + "LoopStart MWQM_Site " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("MWQM_Site_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("\t\t\t\t\tfff = " + Marker + "MWQM_Site_Name" + Marker);
                sb.AppendLine(Marker + "LoopEnd MWQM_Site " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "LoopEnd Subsector " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "LoopEnd Province " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FillTemplateWithDBInfoWord(reportTagStart);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(0, commentsCount);

                range = doc.Range();
                Assert.IsFalse(range.Text.Contains(Marker));
                Assert.IsTrue(range.Text.Contains("aaaaa = " + (culture.TwoLetterISOLanguageName == "fr" ? "Tous les endroits" : "All locations")));
                Assert.IsTrue(range.Text.Contains("bbbbb = " + "Canada"));
                Assert.IsTrue(range.Text.Contains("ccccc = " + "Canada"));
                Assert.IsTrue(range.Text.Contains("ddddd = " + (culture.TwoLetterISOLanguageName == "fr" ? "Nouveau-Brunswick" : "New Brunswick")));

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

            }
        }
        [TestMethod]
        public void ReportBaseService_FillTemplateWithDBInfoWord_In_Table_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                doc.Range().Text = "";
                doc.Range().InsertAfter(Marker + "Start Root " + culture.TwoLetterISOLanguageName + "\r\n");
                doc.Range().InsertAfter("Root_Name" + "\r\n");
                doc.Range().InsertAfter(Marker + "\r\n");
                doc.Range().InsertAfter("Root_Name = " + Marker + "Root_Name" + Marker + "\r\n");

                range = doc.Range();
                range.Start = range.End;

                range.Tables.Add(Range: range, NumRows: 6, NumColumns: 5);
                doc.Tables[1].Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                doc.Tables[1].Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

                // doing row 2 column 1
                doc.Tables[1].Cell(1, 1).Merge(doc.Tables[1].Cell(1, 2));
                doc.Tables[1].Cell(1, 1).Merge(doc.Tables[1].Cell(1, 2));
                doc.Tables[1].Cell(1, 1).Merge(doc.Tables[1].Cell(1, 2));
                doc.Tables[1].Cell(1, 1).Merge(doc.Tables[1].Cell(1, 2));
                range = doc.Tables[1].Cell(1, 1).Range;
                range.Text = "Table Example";

                // doing row 2 column 1
                range = doc.Tables[1].Cell(2, 1).Range;
                range.Text = "Province";

                range = doc.Tables[1].Cell(2, 2).Range;
                range.Text = "Muni. Count";

                range = doc.Tables[1].Cell(2, 3).Range;
                range.Text = "Sample Count";

                range = doc.Tables[1].Cell(2, 4).Range;
                range.Text = "Lat";

                range = doc.Tables[1].Cell(2, 5).Range;
                range.Text = "Lng";


                // doing row 3 column 1
                range = doc.Tables[1].Cell(3, 1).Range;

                StringBuilder sbTable = new StringBuilder();
                sbTable.AppendLine(Marker + "LoopStart Province " + culture.TwoLetterISOLanguageName);
                sbTable.AppendLine("Province_Name ASCENDING 1");
                sbTable.AppendLine("Province_Stat_Municipality_Count");
                sbTable.AppendLine("Province_Stat_MWQM_Sample_Count");
                sbTable.AppendLine("Province_Lat");
                sbTable.AppendLine("Province_Lng");
                sbTable.AppendLine(Marker);
                range.Text = sbTable.ToString();

                // doing row 4 column 1
                range = doc.Tables[1].Cell(4, 1).Range;
                range.Text = Marker + "Province_Name" + Marker;

                range = doc.Tables[1].Cell(4, 2).Range;
                range.Text = Marker + "Province_Stat_Municipality_Count" + Marker;

                range = doc.Tables[1].Cell(4, 3).Range;
                range.Text = Marker + "Province_Stat_MWQM_Sample_Count" + Marker;

                range = doc.Tables[1].Cell(4, 4).Range;
                range.Text = Marker + "Province_Lat FORMAT F3" + Marker;

                range = doc.Tables[1].Cell(4, 5).Range;
                range.Text = Marker + "Province_Lng FORMAT F2" + Marker;

                // doing row 5 column 1
                range = doc.Tables[1].Cell(5, 1).Range;
                doc.Tables[1].Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                doc.Tables[1].Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

                range.Text = Marker + "LoopEnd Province " + culture.TwoLetterISOLanguageName + Marker + "\r\n";

                // doing row 6 column 1
                range = doc.Tables[1].Cell(6, 1).Range;
                range.Text = "AAAA";

                range = doc.Tables[1].Cell(6, 2).Range;
                range.Text = "BBBBBB";

                range = doc.Tables[1].Cell(6, 3).Range;
                range.Text = "CCCCC";

                range = doc.Tables[1].Cell(6, 4).Range;
                range.Text = "DDDDD";

                range = doc.Tables[1].Cell(6, 5).Range;
                range.Text = "EEEEE";


                range = doc.Range();
                range.Start = range.End;

                range.InsertAfter(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker + "\r\n");

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = 1,
                    Take = 20
                };
                string retStr = reportBaseService.FillTemplateWithDBInfoWord(reportTagStart);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(0, commentsCount);

                range = doc.Range();
                Assert.IsFalse(range.Text.Contains(Marker));
                Assert.IsTrue(range.Text.Contains("Root_Name = " + (culture.TwoLetterISOLanguageName == "fr" ? "Tous les endroits" : "All locations")));

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

            }
        }
        [TestMethod]
        public void ReportBaseService_FillTemplateWithDBInfoWord_StartTVItemID_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = 0,
                    Take = 2
                };
                string retStr = reportBaseService.FillTemplateWithDBInfoWord(reportTagStart);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(1, commentsCount);

                range = doc.Range();
                Assert.AreEqual(string.Format(ServiceRes.CouldNotFind_With_Equal_, ServiceRes.TVItem, ServiceRes.TVItemID, reportTagStart.UnderTVItemID.ToString()), retStr);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

            }
        }
        [TestMethod]
        public void ReportBaseService_FillTemplateWithDBInfoWord_Variable_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("lasijeflaisejfalsiefj alisejf ailsejf ilasejf ailsejf ilasfj lisjef laisejf laisefj lasiejf aslfjlasi efj lasifj ");
                sb.AppendLine("lasijeflaisejfalsiefj alisejf ailsejf ilasejf ailsejf ilasfj lisjef laisejf laisefj lasiejf aslfjlasi efj lasifj ");
                sb.AppendLine("lasijeflaisejfalsiefj alisejf ailsejf ilasejf ailsejf ilasfj lisjef laisejf laisefj lasiejf aslfjlasi efj lasifj ");
                sb.AppendLine("lasijeflaisejfalsiefj alisejf ailsejf ilasejf ailsejf ilasfj lisjef laisejf laisefj lasiejf aslfjlasi efj lasifj ");
                sb.AppendLine("lasijeflaisejfalsiefj alisejf ailsejf ilasejf ailsejf ilasfj lisjef laisejf laisefj lasiejf aslfjlasi efj lasifj ");
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_NameNot");
                sb.AppendLine(Marker);
                sb.AppendLine("Root_Name = " + Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FillTemplateWithDBInfoWord(reportTagStart);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(1, commentsCount);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

            }
        }
        [TestMethod]
        public void ReportBaseService_FillTemplateWithDBInfoWord_Special_Table_FCDensities_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Subsector en");
                sb.AppendLine("Subsector_Name_Long");
                sb.AppendLine(Marker + "");
                sb.AppendLine(Marker + "LoopStart Subsector_Special_Table en");
                sb.AppendLine("Subsector_Special_Table_Error");
                sb.AppendLine("Subsector_Special_Table_Last_X_Runs EQUAL 344");
                sb.AppendLine("Subsector_Special_Table_Type EQUAL FCDensitiesTable");
                sb.AppendLine("Subsector_Special_Table_MWQM_Site_Is_Active TRUE");
                sb.AppendLine("Subsector_Special_Table_Number_Of_Samples_For_Stat_Max EQUAL 30");
                sb.AppendLine("Subsector_Special_Table_Number_Of_Samples_For_Stat_Min EQUAL 10");
                sb.AppendLine("Subsector_Special_Table_Highlight_Above_Min_Number EQUAL 43");
                sb.AppendLine("Subsector_Special_Table_Highlight_Below_Max_Number EQUAL 30000");
                sb.AppendLine("Subsector_Special_Table_Show_Number_Of_Days_Of_Precipitation EQUAL 3");
                sb.AppendLine("Subsector_Special_Table_Max_Number_Of_Sites_Per_Table_Part EQUAL 500");
                sb.AppendLine("Subsector_Special_Table_MWQM_Site_Name_List");
                sb.AppendLine("Subsector_Special_Table_Stat_Letter_List");
                sb.AppendLine("Subsector_Special_Table_MWQM_Run_Date_List");
                sb.AppendLine("Subsector_Special_Table_Parameter_Value_List");
                sb.AppendLine("Subsector_Special_Table_Tide_Value_List");
                sb.AppendLine("Subsector_Special_Table_Rain_Day_0_Value_List");
                sb.AppendLine("Subsector_Special_Table_Rain_Day_1_Value_List");
                sb.AppendLine("Subsector_Special_Table_Rain_Day_2_Value_List");
                sb.AppendLine("Subsector_Special_Table_Rain_Day_3_Value_List");
                sb.AppendLine("Subsector_Special_Table_Rain_Day_4_Value_List");
                sb.AppendLine("Subsector_Special_Table_Rain_Day_5_Value_List");
                sb.AppendLine("Subsector_Special_Table_Rain_Day_6_Value_List");
                sb.AppendLine("Subsector_Special_Table_Rain_Day_7_Value_List");
                sb.AppendLine("Subsector_Special_Table_Rain_Day_8_Value_List");
                sb.AppendLine("Subsector_Special_Table_Rain_Day_9_Value_List");
                sb.AppendLine("Subsector_Special_Table_Rain_Day_10_Value_List");
                sb.AppendLine(Marker + "");
                sb.AppendLine("TABLE 2B-(|||PartCount||| of |||PartTotalCount|||) FECAL COLIFORM DENSITIES (MPN/100mL)");
                sb.AppendLine("|||Subsector_Name_Long|||");
                sb.AppendLine(Marker + "LoopEnd Subsector_Special_Table en" + Marker);
                sb.AppendLine(Marker + "End Subsector en" + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = 635,
                    Take = 2
                };
                string retStr = reportBaseService.FillTemplateWithDBInfoWord(reportTagStart);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(1, commentsCount);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();

            }
        }
        [TestMethod]
        public void ReportBaseService_GenerateReportFromTemplateWord_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Country_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("\t" + Marker + "Country_Name" + Marker);
                sb.AppendLine(Marker + "LoopStart Province " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Province_Name");
                sb.AppendLine(Marker);
                sb.AppendLine("\t" + "\t" + Marker + "Province_Name" + Marker);
                sb.AppendLine(Marker + "LoopStart Area " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Area_Name_Long");
                sb.AppendLine(Marker);
                sb.AppendLine("\t" + "\t" + "\t" + Marker + "Area_Name_Long" + Marker);
                sb.AppendLine(Marker + "LoopStart Sector " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Sector_Name_Long");
                sb.AppendLine(Marker);
                sb.AppendLine("\t" + "\t" + "\t" + "\t" + Marker + "Sector_Name_Long" + Marker);
                sb.AppendLine(Marker + "LoopStart Subsector " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Subsector_Name_Long");
                sb.AppendLine(Marker);
                sb.AppendLine("\t" + "\t" + "\t" + "\t" + "\t" + Marker + "Subsector_Name_Long" + Marker);
                sb.AppendLine(Marker + "LoopEnd Subsector " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "LoopEnd Sector " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "LoopEnd Area " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "LoopEnd Province " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\Template_test.docx");
                doc.Close();
                wordApp.Quit();

                FileInfo fiTemplate = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\Template_test.docx");
                Assert.IsTrue(fiTemplate.Exists);

                FileInfo fiDoc = new FileInfo(fiTemplate.FullName.Replace("Template_", ""));
                if (fiDoc.Exists)
                {
                    try
                    {
                        fiDoc.Delete();
                    }
                    catch (Exception)
                    {
                        Assert.IsTrue(false);
                    }
                }

                try
                {
                    File.Copy(fiTemplate.FullName, fiDoc.FullName);
                }
                catch (Exception)
                {
                    Assert.IsTrue(false);
                }

                fiDoc = new FileInfo(fiTemplate.FullName.Replace("Template_", ""));
                Assert.IsTrue(fiDoc.Exists);

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.GenerateReportFromTemplateWord(fiDoc, StartTVItemID, 2, 0);
                Assert.AreEqual("", retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_GenerateReportFromTemplateWord_Pol_Source_Obs_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Subsector " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Subsector_Name_Long");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "Subsector_Name_Long" + Marker);
                sb.AppendLine(Marker + "LoopStart Pol_Source_Site " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Pol_Source_Site_Name");
                sb.AppendLine("Pol_Source_Site_Is_Active TRUE");
                sb.AppendLine(Marker);
                sb.AppendLine("\t" + Marker + "Pol_Source_Site_Name" + Marker);
                sb.AppendLine("\t" + Marker + "Pol_Source_Site_Is_Active" + Marker);
                sb.AppendLine(Marker + "LoopStart Pol_Source_Site_Obs " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Pol_Source_Site_Obs_Only_Last TRUE");
                sb.AppendLine(Marker);
                sb.AppendLine("\t" + "\t" + Marker + "Pol_Source_Site_Obs_Only_Last" + Marker);
                sb.AppendLine(Marker + "LoopStart Pol_Source_Site_Obs_Issue " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Pol_Source_Site_Obs_Issue_Observation_Sentence");
                sb.AppendLine(Marker);
                sb.AppendLine("\t" + "\t" + "\t" + Marker + "Pol_Source_Site_Obs_Issue_Observation_Sentence" + Marker);
                sb.AppendLine(Marker + "LoopEnd Pol_Source_Site_Obs_Issue " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "LoopEnd Pol_Source_Site_Obs " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "LoopEnd Pol_Source_Site " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "End Subsector " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\Template_test.docx");
                doc.Close();
                wordApp.Quit();

                FileInfo fiTemplate = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\Template_test.docx");
                Assert.IsTrue(fiTemplate.Exists);

                FileInfo fiDoc = new FileInfo(fiTemplate.FullName.Replace("Template_", ""));
                if (fiDoc.Exists)
                {
                    try
                    {
                        fiDoc.Delete();
                    }
                    catch (Exception)
                    {
                        Assert.IsTrue(false);
                    }
                }

                try
                {
                    File.Copy(fiTemplate.FullName, fiDoc.FullName);
                }
                catch (Exception)
                {
                    Assert.IsTrue(false);
                }

                fiDoc = new FileInfo(fiTemplate.FullName.Replace("Template_", ""));
                Assert.IsTrue(fiDoc.Exists);

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 645; // shediac harbour subsector
                string retStr = reportBaseService.GenerateReportFromTemplateWord(fiDoc, StartTVItemID, 2, 0);
                Assert.AreEqual("", retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_GenerateReportFromTemplateWord_With_Error_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root en");
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "Root_Name" + Marker);
                sb.AppendLine(Marker + "LoopStart Country en");
                sb.AppendLine("Country_Name");
                sb.AppendLine("Country_Lat");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "Country_Name" + Marker + "LAT " + Marker + "Country_Lat FORMAT F4" + Marker);
                sb.AppendLine(Marker + "LoopStart Province en");
                sb.AppendLine("Province_Name");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "Province_Name" + Marker);
                sb.AppendLine(Marker + "LoopEnd Province en" + Marker);
                sb.AppendLine(Marker + "LoopEnd Country en" + Marker);
                sb.AppendLine(Marker + "End Root en" + Marker);


                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\Template_test.docx");
                doc.Close();
                wordApp.Quit();

                FileInfo fiTemplate = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\Template_test.docx");
                Assert.IsTrue(fiTemplate.Exists);

                FileInfo fiDoc = new FileInfo(fiTemplate.FullName.Replace("Template_", ""));
                if (fiDoc.Exists)
                {
                    try
                    {
                        fiDoc.Delete();
                    }
                    catch (Exception)
                    {
                        Assert.IsTrue(false);
                    }
                }

                try
                {
                    File.Copy(fiTemplate.FullName, fiDoc.FullName);
                }
                catch (Exception)
                {
                    Assert.IsTrue(false);
                }

                fiDoc = new FileInfo(fiTemplate.FullName.Replace("Template_", ""));
                Assert.IsTrue(fiDoc.Exists);

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 1;
                string retStr = reportBaseService.GenerateReportFromTemplateWord(fiDoc, StartTVItemID, 2, 0);
                Assert.AreEqual("", retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_GenerateReportFromTemplateWord_Special_Table_FCDensities_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Subsector en");
                sb.AppendLine("Subsector_Name_Short");
                sb.AppendLine(Marker + "");
                sb.AppendLine(Marker + "Subsector_Name_Short" + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                range = doc.Range();
                range.Start = range.End;

                range.Tables.Add(Range: range, NumRows: 3, NumColumns: 1);
                doc.Tables[1].Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                doc.Tables[1].Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

                sb.Clear();
                sb.AppendLine(Marker + "LoopStart Subsector_Special_Table en");
                sb.AppendLine("Subsector_Special_Table_Last_X_Runs EQUAL 30");
                sb.AppendLine("Subsector_Special_Table_Type EQUAL FCDensitiesTable");
                sb.AppendLine("Subsector_Special_Table_MWQM_Site_Is_Active TRUE");
                sb.AppendLine("Subsector_Special_Table_Number_Of_Sample_For_Stat_Max EQUAL 30");
                sb.AppendLine("Subsector_Special_Table_Number_Of_Sample_For_Stat_Min EQUAL 10");
                sb.AppendLine(Marker + "");

                range = doc.Range();
                range.Start = range.End;

                range = doc.Tables[1].Cell(1, 1).Range;
                range.Text = sb.ToString();

                range = doc.Tables[1].Cell(2, 1).Range;
                range.Text = "";

                sb.Clear();
                sb.AppendLine(Marker + "LoopEnd Subsector_Special_Table en" + Marker);
                range = doc.Tables[1].Cell(3, 1).Range;
                range.Text = sb.ToString();

                range = doc.Range();
                range.Start = range.End;

                sb.Clear();
                sb.AppendLine(Marker + "End Subsector en" + Marker);
                range.Text = sb.ToString();

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\Template_test.docx");
                doc.Close();
                wordApp.Quit();

                FileInfo fiTemplate = new FileInfo(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\Template_test.docx");
                Assert.IsTrue(fiTemplate.Exists);

                FileInfo fiDoc = new FileInfo(fiTemplate.FullName.Replace("Template_", ""));
                if (fiDoc.Exists)
                {
                    try
                    {
                        fiDoc.Delete();
                    }
                    catch (Exception)
                    {
                        Assert.IsTrue(false);
                    }
                }

                try
                {
                    File.Copy(fiTemplate.FullName, fiDoc.FullName);
                }
                catch (Exception)
                {
                    Assert.IsTrue(false);
                }

                fiDoc = new FileInfo(fiTemplate.FullName.Replace("Template_", ""));
                Assert.IsTrue(fiDoc.Exists);

                List<ReportTag> reportTagList = new List<ReportTag>();
                int StartTVItemID = 635;
                string retStr = reportBaseService.GenerateReportFromTemplateWord(fiDoc, StartTVItemID, 2, 0);
                Assert.AreEqual("", retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesWord_Start_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                List<string> FieldNameList = new List<string>() { "Root_Name", "Root_ID", "Root_Is_Active", "Root_Last_Update_Date_And_Time_UTC", "Root_Lat" };
                List<ReportFieldTypeEnum> FieldType = new List<ReportFieldTypeEnum>() { ReportFieldTypeEnum.Text, ReportFieldTypeEnum.NumberWhole, ReportFieldTypeEnum.TrueOrFalse, ReportFieldTypeEnum.DateAndTime, ReportFieldTypeEnum.NumberWithDecimal };

                for (int i = 0, count = FieldNameList.Count(); i < count; i++)
                {

                    Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                    Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                    Microsoft.Office.Interop.Word.Range range = doc.Range();

                    wordApp.Visible = WordVisibleWhenTesting;

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                    sb.AppendLine(FieldNameList[i]);
                    sb.AppendLine(Marker);
                    sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                    doc.Range().Text = "";
                    doc.Range().InsertAfter(sb.ToString());

                    List<ReportTag> reportTagList = new List<ReportTag>();
                    ReportTag reportTagStart = new ReportTag()
                    {
                        wordApp = wordApp,
                        doc = doc,
                        OnlyImmediateChildren = false,
                        UnderTVItemID = 1,
                        Take = 2
                    };
                    string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                    int commentsCount = doc.Comments.Count;
                    Assert.AreEqual(0, commentsCount);
                    Assert.AreEqual("", retStr);
                    Assert.AreEqual(1, reportTagList[0].ReportTreeNodeList.Count);
                    Assert.AreEqual(FieldNameList[i], reportTagList[0].ReportTreeNodeList[0].Text);
                    Assert.AreEqual(FieldType[i], reportTagList[0].ReportTreeNodeList[0].ReportFieldType);

                    doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                    doc.Close();
                    wordApp.Quit();
                }
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesWord_Start_Enum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                List<string> FieldNameList = new List<string>() { "Root_File_Purpose", "Root_File_Type" };
                List<ReportFieldTypeEnum> FieldType = new List<ReportFieldTypeEnum>() { ReportFieldTypeEnum.FilePurpose, ReportFieldTypeEnum.FileType };

                for (int i = 0, count = FieldNameList.Count(); i < count; i++)
                {

                    Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                    Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                    Microsoft.Office.Interop.Word.Range range = doc.Range();

                    wordApp.Visible = WordVisibleWhenTesting;

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(Marker + "Start Root_File " + culture.TwoLetterISOLanguageName);
                    sb.AppendLine(FieldNameList[i]);
                    sb.AppendLine(Marker);
                    sb.AppendLine(Marker + "End Root_File " + culture.TwoLetterISOLanguageName + Marker);

                    doc.Range().Text = "";
                    doc.Range().InsertAfter(sb.ToString());

                    List<ReportTag> reportTagList = new List<ReportTag>();
                    ReportTag reportTagStart = new ReportTag()
                    {
                        wordApp = wordApp,
                        doc = doc,
                        OnlyImmediateChildren = false,
                        UnderTVItemID = 1,
                        Take = 2
                    };
                    string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                    int commentsCount = doc.Comments.Count;
                    Assert.AreEqual(0, commentsCount);
                    Assert.AreEqual("", retStr);
                    Assert.AreEqual(1, reportTagList[0].ReportTreeNodeList.Count);
                    Assert.AreEqual(FieldNameList[i], reportTagList[0].ReportTreeNodeList[0].Text);
                    Assert.AreEqual(FieldType[i], reportTagList[0].ReportTreeNodeList[0].ReportFieldType);

                    doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                    doc.Close();
                    wordApp.Quit();
                }
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesWord_Loop_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                List<string> FieldNameList = new List<string>() { "Country_Name", "Country_ID", "Country_Is_Active", "Country_Last_Update_Date_And_Time_UTC", "Country_Lat" };
                List<ReportFieldTypeEnum> FieldType = new List<ReportFieldTypeEnum>() { ReportFieldTypeEnum.Text, ReportFieldTypeEnum.NumberWhole, ReportFieldTypeEnum.TrueOrFalse, ReportFieldTypeEnum.DateAndTime, ReportFieldTypeEnum.NumberWithDecimal };

                for (int i = 0, count = FieldNameList.Count(); i < count; i++)
                {

                    Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                    Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                    Microsoft.Office.Interop.Word.Range range = doc.Range();

                    wordApp.Visible = WordVisibleWhenTesting;

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                    sb.AppendLine(Marker);
                    sb.AppendLine(Marker + "LoopStart Country " + culture.TwoLetterISOLanguageName);
                    sb.AppendLine(FieldNameList[i]);
                    sb.AppendLine(Marker);
                    sb.AppendLine(Marker + "LoopEnd Country " + culture.TwoLetterISOLanguageName + Marker);
                    sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                    doc.Range().Text = "";
                    doc.Range().InsertAfter(sb.ToString());

                    List<ReportTag> reportTagList = new List<ReportTag>();
                    ReportTag reportTagStart = new ReportTag()
                    {
                        wordApp = wordApp,
                        doc = doc,
                        OnlyImmediateChildren = false,
                        UnderTVItemID = 1,
                        Take = 2
                    };
                    string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                    int commentsCount = doc.Comments.Count;
                    Assert.AreEqual(0, commentsCount);
                    Assert.AreEqual("", retStr);
                    Assert.AreEqual(1, reportTagList[1].ReportTreeNodeList.Count);
                    Assert.AreEqual(FieldNameList[i], reportTagList[1].ReportTreeNodeList[0].Text);
                    Assert.AreEqual(FieldType[i], reportTagList[1].ReportTreeNodeList[0].ReportFieldType);

                    doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                    doc.Close();
                    wordApp.Quit();
                }
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesWord_Loop_Enum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                List<string> FieldNameList = new List<string>() { "Country_File_Purpose", "Country_File_Type" };
                List<ReportFieldTypeEnum> FieldType = new List<ReportFieldTypeEnum>() { ReportFieldTypeEnum.FilePurpose, ReportFieldTypeEnum.FileType };

                for (int i = 0, count = FieldNameList.Count(); i < count; i++)
                {

                    Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                    Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                    Microsoft.Office.Interop.Word.Range range = doc.Range();

                    wordApp.Visible = WordVisibleWhenTesting;

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                    sb.AppendLine(Marker);
                    sb.AppendLine(Marker + "LoopStart Country_File " + culture.TwoLetterISOLanguageName);
                    sb.AppendLine(FieldNameList[i]);
                    sb.AppendLine(Marker);
                    sb.AppendLine(Marker + "LoopEnd Country_File " + culture.TwoLetterISOLanguageName + Marker);
                    sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                    doc.Range().Text = "";
                    doc.Range().InsertAfter(sb.ToString());

                    List<ReportTag> reportTagList = new List<ReportTag>();
                    ReportTag reportTagStart = new ReportTag()
                    {
                        wordApp = wordApp,
                        doc = doc,
                        OnlyImmediateChildren = false,
                        UnderTVItemID = 1,
                        Take = 2
                    };
                    string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                    int commentsCount = doc.Comments.Count;
                    Assert.AreEqual(0, commentsCount);
                    Assert.AreEqual("", retStr);
                    Assert.AreEqual(1, reportTagList[1].ReportTreeNodeList.Count);
                    Assert.AreEqual(FieldNameList[i], reportTagList[1].ReportTreeNodeList[0].Text);
                    Assert.AreEqual(FieldType[i], reportTagList[1].ReportTreeNodeList[0].ReportFieldType);

                    doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                    doc.Close();
                    wordApp.Quit();
                }
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesWord_ReportTag_TagItem_ShouldStartWith_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);


                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("NotRoot_Name");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_ShouldStartWith_, 2, "Root"), retStr);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesWord_System_Boolean_ShouldBeOneOf_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Is_Active TRUENot");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, 2, 2, String.Join(",", reportBaseService.AllowableSortingFilters.Concat(reportBaseService.AllowableTrueFalseFilters))), retStr);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesWord_System_Boolean_SetReportTreeNodeSorting_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Is_Active ASCENDING a");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeANumber, 2, 1 + 1), retStr);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesWord_System_Boolean_SetReportTreeNodeTrueFalse_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Is_Active ASCENDING 1 TRUENot");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, 2, 3 + 1, "TRUE,FALSE"), retStr);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesWord_System_Boolean_Line_Item_ShouldBeOneOf_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_ID EQUALNot");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, 2, 2, String.Join(",", reportBaseService.AllowableSortingFilters.Concat(reportBaseService.AllowableFormatingFilters.Concat(reportBaseService.AllowableBasicFilters)))), retStr);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesWord_System_DateTime_ShouldBeOneOf_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Last_Update_Date_And_Time_UTC Not");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, 2, 2, String.Join(",",
                    reportBaseService.AllowableSortingFilters.Concat(reportBaseService.AllowableFormatingFilters.Concat(reportBaseService.AllowableBasicFilters)))), retStr);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesWord_System_DateTime_SetReportTreeNodeSorting_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Last_Update_Date_And_Time_UTC ASCENDING a");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeANumber, 2, 1 + 1), retStr);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesWord_System_DateTime_SetReportTreeNodeFormating_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Last_Update_Date_And_Time_UTC FORMAT yyyy");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(0, commentsCount);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesWord_System_Single_Or_Int32_Line_Item_ShouldBeOneOf_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_ID EQUALNot");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, 2, 2, String.Join(",", reportBaseService.AllowableSortingFilters.Concat(reportBaseService.AllowableFormatingFilters.Concat(reportBaseService.AllowableBasicFilters)))), retStr);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesWord_System_Single_Or_Int32_SetReportTreeNodeSorting_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_ID ASCENDING a");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeANumber, 2, 1 + 1), retStr);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesWord_System_Single_Or_Int32_SetReportTreeNodeNumberCondition_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_ID ASCENDING 1 EQUALNot 3");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, 2, 3 + 1, String.Join(",", reportBaseService.AllowableBasicFilters)), retStr);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesWord_System_Single_Or_Int32_SetReportTreeNodeNumberCondition2_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_ID ASCENDING 1 EQUAL 3 EQUALNot 5");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, 2, 5 + 1, String.Join(",", reportBaseService.AllowableBasicFilters)), retStr);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesWord_System_Single_Or_Int32_SetReportTreeNodeNumberCondition3_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_ID EQUAL a");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes.ConditionTextMissingNumberAfter_, "EQUAL"), retStr);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesWord_System_Single_Or_Int32_SetReportTreeNodeNumberCondition4_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_ID EQUAL 1 EQUAL a");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes.ConditionTextMissingNumberAfter_, "EQUAL"), retStr);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesWord_System_String_Line_Item_ShouldBeOneOf__Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name ASCENDINGNot 1");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, 2, 2, String.Join(",", reportBaseService.AllowableSortingFilters.Concat(reportBaseService.AllowableTextFilters))), retStr);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesWord_System_String_SetReportTreeNodeSorting_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name ASCENDING a");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeANumber, 2, 1 + 1), retStr);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesWord_System_String_SetReportTreeNodeTextCondition_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name ASCENDING 1 CONTAINNot a");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, 2, 3 + 1, String.Join(",", reportBaseService.AllowableTextFilters)), retStr);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesWord_System_String_SetReportTreeNodeTextCondition2_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name ASCENDING 1 CONTAIN a CONTAINNot a");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, 2, 5 + 1, String.Join(",", reportBaseService.AllowableTextFilters)), retStr);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesWord_System_String_SetReportTreeNodeTextCondition3_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name ASCENDING 1 CONTAIN a CONTAIN b CONTAINNot c");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, 2, 7 + 1, String.Join(",", reportBaseService.AllowableTextFilters)), retStr);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesWord_System_String_SetReportTreeNodeTextCondition4_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name CONTAIN A CONTAINNot c");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, 2, 3 + 1, String.Join(",", reportBaseService.AllowableTextFilters)), retStr);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesWord_System_String_SetReportTreeNodeTextCondition5_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name CONTAIN A CONTAIN B CONTAINNot c");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, 2, 5 + 1, String.Join(",", reportBaseService.AllowableTextFilters)), retStr);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesWord_CSSPEnumsDLL_Enums_FilePurposeEnum_Line_Item_ShouldBeOneOf__Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root_File " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_File_Purpose ASCENDINGNot 1");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "End Root_File " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, 2, 2, String.Join(",", reportBaseService.AllowableSortingFilters.Concat(reportBaseService.AllowableEnumFilters))), retStr);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesWord_CSSPEnumsDLL_Enums_FilePurposeEnum_SetReportTreeNodeSorting_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root_File " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_File_Purpose ASCENDING a");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "End Root_File " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeANumber, 2, 1 + 1), retStr);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesWord_CSSPEnumsDLL_Enums_FilePurposeEnum_SetReportTreeNodeEnumCondition_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root_File " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_File_Purpose ASCENDING 1 EQUALNot allo");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "End Root_File " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, 2, 3 + 1, String.Join(",", reportBaseService.AllowableEnumFilters)), retStr);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesWord_CSSPEnumsDLL_Enums_FilePurposeEnum_SetReportTreeNodeEnumCondition2_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root_File " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_File_Purpose EQUAL");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "End Root_File " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Condition_MissingValue, "EQUAL"), retStr);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesWord_CSSPEnumsDLL_Enums_FilePurposeEnum_TypeName_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range();

                wordApp.Visible = WordVisibleWhenTesting;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Marker + "Start Root " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_Name");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "LoopStart Root_File " + culture.TwoLetterISOLanguageName);
                sb.AppendLine("Root_File_Purpose EQUAL a");
                sb.AppendLine(Marker);
                sb.AppendLine(Marker + "LoopEnd Root_File " + culture.TwoLetterISOLanguageName + Marker);
                sb.AppendLine(Marker + "End Root " + culture.TwoLetterISOLanguageName + Marker);

                doc.Range().Text = "";
                doc.Range().InsertAfter(sb.ToString());

                List<ReportTag> reportTagList = new List<ReportTag>();
                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = wordApp,
                    doc = doc,
                    OnlyImmediateChildren = false,
                    UnderTVItemID = 1,
                    Take = 2
                };
                string retStr = reportBaseService.FindTagsWord(reportTagStart, reportTagList);

                int commentsCount = doc.Comments.Count;
                Assert.AreEqual(1, commentsCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Enum_NotFoundShouldBeOneOf_, "a", String.Join(",", Enum.GetNames(typeof(FilePurposeEnum)))), retStr);

                doc.SaveAs2(@"C:\CSSP latest code\CSSPReportWriterHelperDLL\CSSPReportWriterHelperDLL.Tests\test.docx");
                doc.Close();
                wordApp.Quit();
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
