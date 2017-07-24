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
using System.Drawing;

namespace CSSPReportWriterHelperDLL.Tests.Services
{
    /// <summary>
    /// Summary description for BaseServiceTest
    /// </summary>
    [TestClass]
    public class ReportBaseGenerateCodeServiceTest : SetupData
    {
        #region Variables
        private TestContext testContextInstance;
        private SetupData setupData;
        //private string Marker = "|||";
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
        public ReportBaseGenerateCodeServiceTest()
        {
            setupData = new SetupData();
        }
        #endregion Constructors

        #region Testing Functions public
        [TestMethod]
        public void ReportBaseService_GenerateModel_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sb = new StringBuilder();
                reportBaseService._ReportTreeNodeRoot = reportBaseService._CreateTreeViewService.CreateReportTreeNodeItem("Root", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.TableSelectable, ReportFieldTypeEnum.Error);
                reportBaseService._ReportTreeNodeRoot.ForeColor = Color.Green;
                reportBaseService._TreeViewCSSP.Nodes.Add(reportBaseService._ReportTreeNodeRoot);
                reportBaseService.LoadRecursiveTreeNode(reportBaseService._ReportTreeNodeRoot);

                string retStr = reportBaseService.GenerateModel(reportBaseService._ReportTreeNodeRoot, sb);
                Assert.AreEqual("", retStr);
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
