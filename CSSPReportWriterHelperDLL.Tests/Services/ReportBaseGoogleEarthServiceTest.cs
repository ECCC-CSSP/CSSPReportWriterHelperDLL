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
    public class ReportBaseGoogleEarthServiceTest : SetupData
    {
        #region Variables
        private TestContext testContextInstance;
        private SetupData setupData;
        //private string Marker = "|||";
        public List<string> AllowableDateReportCondition = new List<string>() { "EQUAL", "BIGGER_THAN", "SMALLER_THAN", "BETWEEN", "NOT_EQUAL", "NOT_BIGGER_THAN", "NOT_SMALLER_THAN", "NOT_BETWEEN" };
        public List<string> AllowableDateVariables = new List<string>() { "YEAR", "MONTH", "DAY", "HOUR", "MINUTE" };
        public List<string> AllowableDateBetweenVariables = new List<string>() { "FROM_YEAR", "FROM_MONTH", "FROM_DAY", "FROM_HOUR", "FROM_MINUTE", "TO_YEAR", "TO_MONTH", "TO_DAY", "TO_HOUR", "TO_MINUTE" };
        public List<string> AllowableNumberReportCondition = new List<string>() { "EQUAL", "BIGGER_THAN", "SMALLER_THAN", "BETWEEN", "NOT_EQUAL", "NOT_BIGGER_THAN", "NOT_SMALLER_THAN", "NOT_BETWEEN" };
        public List<string> AllowableSortingReportCondition = new List<string>() { "ASCENDING", "DESCENDING" };
        public List<string> AllowableTextReportCondition = new List<string>() { "CONTAIN", "START_WITH", "END_WITH", "EQUAL", "NOT_CONTAIN", "NOT_START_WITH", "NOT_END_WITH", "NOT_EQUAL" };
        public List<string> AllowableTrueFalseReportCondition = new List<string>() { "TRUE", "FALSE" };

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
        public ReportBaseGoogleEarthServiceTest()
        {
            setupData = new SetupData();
        }
        #endregion Constructors

        #region Testing Functions public
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
