using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSSPModelsDLL.Models;
using System.Reflection;
using System.Linq;
using CSSPReportWriterHelperDLL.Services;
using CSSPEnumsDLL.Enums;
using System.Windows.Forms;

namespace CSSPReportWriterHelperDLL.Tests.Services
{

    /// <summary>
    /// Summary description for CreateTreeViewServiceTest
    /// </summary>
    [TestClass]
    public class CreateTreeViewServiceTest
    {
        #region Variables
        private TestContext testContextInstance;
        #endregion Variables

        #region Properties
        public CreateTreeViewService createTreeViewService { get; set; }
        public ReportTreeNode _ReportTreeNodeRoot { get; set; }
        public TreeView _TreeViewCSSP { get; set; }

        #endregion Properties

        #region Constructors
        public CreateTreeViewServiceTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #endregion Constructors

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

        #region Testing Methods
        [TestMethod]
        public void CreateTreeViewService_LoadChildrenTreeNodes_Root_Test()
        {
            SetupTest();
            List<ReportTreeNode> reportTreeNodeList = createTreeViewService.LoadChildrenTreeNodes(_ReportTreeNodeRoot);
            Assert.AreEqual(4, reportTreeNodeList.Count);
            Assert.AreEqual("Root_Fields", reportTreeNodeList[0].Text);
            Assert.AreEqual("Country", reportTreeNodeList[1].Text);
            Assert.AreEqual("MPN_Lookup", reportTreeNodeList[2].Text);
            Assert.AreEqual("Root_File", reportTreeNodeList[3].Text);
        }
        [TestMethod]
        public void CreateTreeViewService_CreateReportTreeNodeItem_Area_Test()
        {
            SetupTest();
            ReportTreeNode reportTreeNodeArea = createTreeViewService.CreateReportTreeNodeItem("Area", ReportTreeNodeTypeEnum.ReportAreaType, ReportTreeNodeSubTypeEnum.TableSelectable, ReportFieldTypeEnum.Error);
            Assert.AreEqual("Area", reportTreeNodeArea.Text);
            Assert.AreEqual(ReportTreeNodeTypeEnum.ReportAreaType, reportTreeNodeArea.ReportTreeNodeType);
            Assert.AreEqual(ReportTreeNodeSubTypeEnum.TableSelectable, reportTreeNodeArea.ReportTreeNodeSubType);
            Assert.AreEqual(ReportFieldTypeEnum.Error, reportTreeNodeArea.ReportFieldType);
            Assert.AreEqual(ReportSortingEnum.Error, reportTreeNodeArea.dbSortingField.ReportSorting);
            Assert.AreEqual(0, reportTreeNodeArea.dbSortingField.Ordinal);
            Assert.AreEqual(ReportFormatingDateEnum.Error, reportTreeNodeArea.reportFormatingField.ReportFormatingDate);
            Assert.AreEqual(ReportFormatingNumberEnum.Error, reportTreeNodeArea.reportFormatingField.ReportFormatingNumber);
            Assert.AreEqual(0, reportTreeNodeArea.dbFilteringDateFieldList.Count);
            Assert.AreEqual(0, reportTreeNodeArea.dbFilteringNumberFieldList.Count);
            Assert.AreEqual(0, reportTreeNodeArea.dbFilteringTextFieldList.Count);
            Assert.AreEqual(0, reportTreeNodeArea.dbFilteringTrueFalseFieldList.Count);
            Assert.AreEqual(0, reportTreeNodeArea.reportConditionDateFieldList.Count);
            Assert.AreEqual(0, reportTreeNodeArea.reportConditionNumberFieldList.Count);
            Assert.AreEqual(0, reportTreeNodeArea.reportConditionTextFieldList.Count);
            Assert.AreEqual(0, reportTreeNodeArea.reportConditionTrueFalseFieldList.Count);
        }
        [TestMethod]
        public void CreateTreeViewService_CreateReportCountryTypeTreeNodeItem_Test()
        {
            SetupTest();
            List<ReportTreeNode> reportTreeNodeList = createTreeViewService.CreateReportCountryTypeTreeNodeItem(_ReportTreeNodeRoot);
            Assert.AreEqual(3, reportTreeNodeList.Count);
            Assert.AreEqual("Country_Fields", reportTreeNodeList[0].Text);
            Assert.AreEqual("Province", reportTreeNodeList[1].Text);
            Assert.AreEqual("Country_File", reportTreeNodeList[2].Text);
        }
        #endregion Testing Methods

        #region Functions
        public void SetupTest()
        {
            createTreeViewService = new CreateTreeViewService();
            _ReportTreeNodeRoot = createTreeViewService.CreateReportTreeNodeItem("Root", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.TableSelectable, ReportFieldTypeEnum.Error);
            _TreeViewCSSP = new TreeView();
            _TreeViewCSSP.Nodes.Add(_ReportTreeNodeRoot);
        }
        private void SetupShim()
        {
        }
        #endregion Functions

    }
}
