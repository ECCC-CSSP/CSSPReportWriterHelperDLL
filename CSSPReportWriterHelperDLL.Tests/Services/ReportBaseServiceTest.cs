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
using CSSPWebToolsDBDLL.Services;
using System.Security.Principal;
using System.Collections.Specialized;
using CSSPWebToolsDBDLL;
using CSSPWebToolsDBDLL.Services.Resources;

namespace CSSPReportWriterHelperDLL.Tests.Services
{
    /// <summary>
    /// Summary description for BaseServiceTest
    /// </summary>
    [TestClass]
    public class ReportBaseServiceTest : SetupData
    {
        #region Variables
        private TestContext testContextInstance;
        private SetupData setupData;
        private string Marker = "|||";
        private List<string> AllowableBasicFilters = new List<string>() { "EQUAL", "BIGGER", "SMALLER" };
        private List<string> AllowableDateVariables = new List<string>() { "YEAR", "MONTH", "DAY", "HOUR", "MINUTE" };
        private List<string> AllowableEnumFilters = new List<string>() { "EQUAL" };
        private List<string> AllowableFormatingFilters = new List<string>() { "FORMAT" };
        private List<string> AllowableSortingFilters = new List<string>() { "ASCENDING", "DESCENDING" };
        private List<string> AllowableTextFilters = new List<string>() { "EQUAL", "BIGGER", "SMALLER", "CONTAIN", "START", "END" };
        private List<string> AllowableTrueFalseFilters = new List<string>() { "TRUE", "FALSE" };
        #endregion Variables

        #region Properties
        public ReportBaseService reportBaseService { get; set; }
        public TreeView treeViewCSSP { get; set; }
        public ReportTreeNode reportTreeNodeRoot { get; set; }
        public BaseEnumService baseEnumService { get; set; }
        public ReportBase reportBase { get; set; }
        public TVItemService tvItemService { get; set; }

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
        public ReportBaseServiceTest()
        {
            setupData = new SetupData();
        }
        #endregion Constructors

        #region Testing Methods Constructors
        [TestMethod]
        public void ReportBaseService_Constructors1_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                //string baseURLEN = "http://wmon01dtchlebl2/csspwebtools/en-CA/Report/";
                //string baseURLFR = "http://wmon01dtchlebl2/csspwebtools/fr-CA/Report/";
                string baseURLEN = "http://localhost:11562/en-CA/Report/";
                string baseURLFR = "http://localhost:11562/fr-CA/Report/";
                Assert.AreEqual(baseURLEN, reportBaseService.BaseURLEN);
                Assert.AreEqual(baseURLFR, reportBaseService.BaseURLFR);
                Assert.AreEqual(AllowableBasicFilters.Count, reportBaseService.AllowableBasicFilters.Count);
                for (int i = 0, count = AllowableBasicFilters.Count(); i < count; i++)
                {
                    Assert.AreEqual(AllowableBasicFilters[i], reportBaseService.AllowableBasicFilters[i]);
                }
                Assert.AreEqual(AllowableDateVariables.Count, reportBaseService.AllowableDateVariables.Count);
                for (int i = 0, count = AllowableDateVariables.Count(); i < count; i++)
                {
                    Assert.AreEqual(AllowableDateVariables[i], reportBaseService.AllowableDateVariables[i]);
                }
                Assert.AreEqual(AllowableEnumFilters.Count, reportBaseService.AllowableEnumFilters.Count);
                for (int i = 0, count = AllowableEnumFilters.Count(); i < count; i++)
                {
                    Assert.AreEqual(AllowableEnumFilters[i], reportBaseService.AllowableEnumFilters[i]);
                }
                Assert.AreEqual(AllowableFormatingFilters.Count, reportBaseService.AllowableEnumFilters.Count);
                for (int i = 0, count = AllowableFormatingFilters.Count(); i < count; i++)
                {
                    Assert.AreEqual(AllowableFormatingFilters[i], reportBaseService.AllowableFormatingFilters[i]);
                }
                Assert.AreEqual(AllowableBasicFilters.Count, reportBaseService.AllowableBasicFilters.Count);
                for (int i = 0, count = AllowableBasicFilters.Count(); i < count; i++)
                {
                    Assert.AreEqual(AllowableBasicFilters[i], reportBaseService.AllowableBasicFilters[i]);
                }
                Assert.AreEqual(AllowableSortingFilters.Count, reportBaseService.AllowableSortingFilters.Count);
                for (int i = 0, count = AllowableSortingFilters.Count(); i < count; i++)
                {
                    Assert.AreEqual(AllowableSortingFilters[i], reportBaseService.AllowableSortingFilters[i]);
                }
                Assert.AreEqual(AllowableTextFilters.Count, reportBaseService.AllowableTextFilters.Count);
                for (int i = 0, count = AllowableTextFilters.Count(); i < count; i++)
                {
                    Assert.AreEqual(AllowableTextFilters[i], reportBaseService.AllowableTextFilters[i]);
                }
                Assert.AreEqual(AllowableTrueFalseFilters.Count, reportBaseService.AllowableTrueFalseFilters.Count);
                for (int i = 0, count = AllowableTrueFalseFilters.Count(); i < count; i++)
                {
                    Assert.AreEqual(AllowableTrueFalseFilters[i], reportBaseService.AllowableTrueFalseFilters[i]);
                }
                Assert.AreEqual("|||", Marker);
                Assert.AreEqual("", reportBaseService.LastHref);
                Assert.AreEqual("", reportBaseService.LastCSSPTVText);
                Assert.AreEqual(0, reportBaseService.Count);
                Assert.AreEqual(ReportFileTypeEnum.Error, reportBaseService.ReportFileType);
                Assert.IsNull(reportBaseService._User);
                Assert.AreEqual((culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en), reportBaseService.LanguageRequest);
                Assert.IsNotNull(reportBaseService._TreeViewCSSP);
                Assert.IsNotNull(reportBaseService._ReportTreeNodeRoot);
                Assert.IsNotNull(reportBaseService._TreeViewTextList);
                Assert.IsNotNull(reportBaseService._CreateTreeViewService);
                Assert.IsNotNull(reportBaseService._ReportBase);
                Assert.AreEqual(reportBaseService.LanguageRequest + "-CA", Thread.CurrentThread.CurrentCulture.Name);
                Assert.AreEqual(reportBaseService.LanguageRequest + "-CA", Thread.CurrentThread.CurrentUICulture.Name);
                Assert.AreEqual("Root", reportBaseService._ReportTreeNodeRoot.Text);
            }
        }
        [TestMethod]
        public void ReportBaseService_Constructors2_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                IPrincipal user = new GenericPrincipal(new GenericIdentity("charles.leblanc2@canada.ca", "Forms"), null);

                reportBaseService = new ReportBaseService((culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en), treeViewCSSP, user);

                //string baseURLEN = "http://wmon01dtchlebl2/csspwebtools/en-CA/Report/";
                //string baseURLFR = "http://wmon01dtchlebl2/csspwebtools/fr-CA/Report/";
                string baseURLEN = "http://localhost:11562/en-CA/Report/";
                string baseURLFR = "http://localhost:11562/fr-CA/Report/";
                Assert.AreEqual(baseURLEN, reportBaseService.BaseURLEN);
                Assert.AreEqual(baseURLFR, reportBaseService.BaseURLFR);
                Assert.AreEqual(AllowableBasicFilters.Count, reportBaseService.AllowableBasicFilters.Count);
                for (int i = 0, count = AllowableBasicFilters.Count(); i < count; i++)
                {
                    Assert.AreEqual(AllowableBasicFilters[i], reportBaseService.AllowableBasicFilters[i]);
                }
                Assert.AreEqual(AllowableDateVariables.Count, reportBaseService.AllowableDateVariables.Count);
                for (int i = 0, count = AllowableDateVariables.Count(); i < count; i++)
                {
                    Assert.AreEqual(AllowableDateVariables[i], reportBaseService.AllowableDateVariables[i]);
                }
                Assert.AreEqual(AllowableEnumFilters.Count, reportBaseService.AllowableEnumFilters.Count);
                for (int i = 0, count = AllowableEnumFilters.Count(); i < count; i++)
                {
                    Assert.AreEqual(AllowableEnumFilters[i], reportBaseService.AllowableEnumFilters[i]);
                }
                Assert.AreEqual(AllowableFormatingFilters.Count, reportBaseService.AllowableEnumFilters.Count);
                for (int i = 0, count = AllowableFormatingFilters.Count(); i < count; i++)
                {
                    Assert.AreEqual(AllowableFormatingFilters[i], reportBaseService.AllowableFormatingFilters[i]);
                }
                Assert.AreEqual(AllowableBasicFilters.Count, reportBaseService.AllowableBasicFilters.Count);
                for (int i = 0, count = AllowableBasicFilters.Count(); i < count; i++)
                {
                    Assert.AreEqual(AllowableBasicFilters[i], reportBaseService.AllowableBasicFilters[i]);
                }
                Assert.AreEqual(AllowableSortingFilters.Count, reportBaseService.AllowableSortingFilters.Count);
                for (int i = 0, count = AllowableSortingFilters.Count(); i < count; i++)
                {
                    Assert.AreEqual(AllowableSortingFilters[i], reportBaseService.AllowableSortingFilters[i]);
                }
                Assert.AreEqual(AllowableTextFilters.Count, reportBaseService.AllowableTextFilters.Count);
                for (int i = 0, count = AllowableTextFilters.Count(); i < count; i++)
                {
                    Assert.AreEqual(AllowableTextFilters[i], reportBaseService.AllowableTextFilters[i]);
                }
                Assert.AreEqual(AllowableTrueFalseFilters.Count, reportBaseService.AllowableTrueFalseFilters.Count);
                for (int i = 0, count = AllowableTrueFalseFilters.Count(); i < count; i++)
                {
                    Assert.AreEqual(AllowableTrueFalseFilters[i], reportBaseService.AllowableTrueFalseFilters[i]);
                }
                Assert.AreEqual("|||", Marker);
                Assert.AreEqual("", reportBaseService.LastHref);
                Assert.AreEqual("", reportBaseService.LastCSSPTVText);
                Assert.AreEqual(0, reportBaseService.Count);
                Assert.AreEqual(ReportFileTypeEnum.Error, reportBaseService.ReportFileType);
                Assert.IsNotNull(reportBaseService._User);
                Assert.AreEqual((culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en), reportBaseService.LanguageRequest);
                Assert.IsNotNull(reportBaseService._TreeViewCSSP);
                Assert.IsNotNull(reportBaseService._ReportTreeNodeRoot);
                Assert.IsNotNull(reportBaseService._TreeViewTextList);
                Assert.IsNotNull(reportBaseService._CreateTreeViewService);
                Assert.IsNotNull(reportBaseService._ReportBase);
                Assert.AreEqual(reportBaseService.LanguageRequest + "-CA", Thread.CurrentThread.CurrentCulture.Name);
                Assert.AreEqual(reportBaseService.LanguageRequest + "-CA", Thread.CurrentThread.CurrentUICulture.Name);
                Assert.AreEqual("Root", reportBaseService._ReportTreeNodeRoot.Text);
            }
        }
        #endregion Testing Methods Constructors
        #region Testing Methods Generic Functions
        [TestMethod]
        public void ReportBaseService_CheckFilterOfEnum_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                List<Type> typeList = new List<Type>()
                {
                    typeof(FilePurposeEnum), typeof(FileTypeEnum), typeof(TranslationStatusEnum), typeof(BoxModelResultTypeEnum), typeof(InfrastructureTypeEnum),
                    typeof(FacilityTypeEnum), typeof(AerationTypeEnum), typeof(PreliminaryTreatmentTypeEnum), typeof(PrimaryTreatmentTypeEnum),
                    typeof(SecondaryTreatmentTypeEnum), typeof(TertiaryTreatmentTypeEnum), typeof(TreatmentTypeEnum), typeof(DisinfectionTypeEnum),
                    typeof(CollectionSystemTypeEnum), typeof(AlarmSystemTypeEnum), typeof(ScenarioStatusEnum),
                    typeof(StorageDataTypeEnum), typeof(LanguageEnum), typeof(SampleTypeEnum), typeof(BeaufortScaleEnum), typeof(AnalyzeMethodEnum),
                    typeof(SampleMatrixEnum), typeof(LaboratoryEnum), typeof(SampleStatusEnum), typeof(ConfigTypeEnum), typeof(LabSheetTypeEnum),
                    typeof(LabSheetStatusEnum), typeof(PolSourceInactiveReasonEnum), typeof(PolSourceObsInfoEnum), typeof(AddressTypeEnum),
                    typeof(StreetTypeEnum), typeof(ContactTitleEnum), typeof(EmailTypeEnum), typeof(TelTypeEnum), typeof(TideTextEnum), typeof(TideDataTypeEnum)
                };

                List<ReportFieldTypeEnum> reportFieldTypeList = new List<ReportFieldTypeEnum>()
                {
                     ReportFieldTypeEnum.FilePurpose, ReportFieldTypeEnum.FileType, ReportFieldTypeEnum.TranslationStatus, ReportFieldTypeEnum.BoxModelResultType, ReportFieldTypeEnum.InfrastructureType,
                    ReportFieldTypeEnum.FacilityType, ReportFieldTypeEnum.AerationType, ReportFieldTypeEnum.PreliminaryTreatmentType, ReportFieldTypeEnum.PrimaryTreatmentType,
                    ReportFieldTypeEnum.SecondaryTreatmentType, ReportFieldTypeEnum.TertiaryTreatmentType, ReportFieldTypeEnum.TreatmentType, ReportFieldTypeEnum.DisinfectionType,
                    ReportFieldTypeEnum.CollectionSystemType, ReportFieldTypeEnum.AlarmSystemType, ReportFieldTypeEnum.ScenarioStatus,
                    ReportFieldTypeEnum.StorageDataType, ReportFieldTypeEnum.Language, ReportFieldTypeEnum.SampleType, ReportFieldTypeEnum.BeaufortScale, ReportFieldTypeEnum.AnalyzeMethod,
                    ReportFieldTypeEnum.SampleMatrix, ReportFieldTypeEnum.Laboratory, ReportFieldTypeEnum.SampleStatus, ReportFieldTypeEnum.ConfigType, ReportFieldTypeEnum.LabSheetType,
                    ReportFieldTypeEnum.LabSheetStatus, ReportFieldTypeEnum.PolSourceInactiveReason, ReportFieldTypeEnum.PolSourceObsInfo, ReportFieldTypeEnum.AddressType,
                    ReportFieldTypeEnum.StreetType, ReportFieldTypeEnum.ContactTitle, ReportFieldTypeEnum.EmailType, ReportFieldTypeEnum.TelType, ReportFieldTypeEnum.TideText, ReportFieldTypeEnum.TideDataType

                };

                for (int i = 0, count = typeList.Count(); i < count; i++)
                {
                    foreach (string s in Enum.GetNames(typeList[i]))
                    {
                        string retStr = reportBaseService.CheckFilterOfEnum(s, reportFieldTypeList[i]);
                        Assert.AreEqual("", retStr);
                    }
                }
            }
        }
        [TestMethod]
        public void ReportBaseService_DownloadSerializedJsonData_Root_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Name");
                sbCommand.AppendLine("|||");

                string TagItem = "Root";
                int UnderTVItemID = 1;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                NameValueCollection nameValueCollection = CreateNameValueCollection(culture, sbCommand, TagItem, UnderTVItemID, ParentTagItem, CountOnly, Take);

                List<ReportRootModel> reportRootModelList = reportBaseService.DownloadSerializedJsonData<List<ReportRootModel>>(nameValueCollection);
                Assert.AreEqual(1, reportRootModelList.Count);
                Assert.AreEqual("", reportRootModelList[0].Root_Error);
                Assert.AreEqual((culture.TwoLetterISOLanguageName == "fr" ? "Tous les endroits" : "All locations"), reportRootModelList[0].Root_Name);
            }
        }
        [TestMethod]
        public void ReportBaseService_DownloadSerializedJsonData_Root_Good_Count_Only_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Name");
                sbCommand.AppendLine("|||");

                string TagItem = "Root";
                int UnderTVItemID = 1;
                string ParentTagItem = "";
                bool CountOnly = true;
                int Take = 2;

                NameValueCollection nameValueCollection = CreateNameValueCollection(culture, sbCommand, TagItem, UnderTVItemID, ParentTagItem, CountOnly, Take);

                Assert.IsTrue(string.IsNullOrWhiteSpace(nameValueCollection["Error"]));

                List<ReportRootModel> reportRootModelList = reportBaseService.DownloadSerializedJsonData<List<ReportRootModel>>(nameValueCollection);
                Assert.AreEqual(1, reportRootModelList.Count);
                Assert.AreEqual("", reportRootModelList[0].Root_Error);
                Assert.AreEqual(1, reportRootModelList[0].Root_Counter);
            }
        }
        [TestMethod]
        public void ReportBaseService_DownloadSerializedJsonData_Root_Error_Field_Name_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_NameNot");
                sbCommand.AppendLine("|||");

                string TagItem = "Root";
                int UnderTVItemID = 1;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                NameValueCollection nameValueCollection = CreateNameValueCollection(culture, sbCommand, TagItem, UnderTVItemID, ParentTagItem, CountOnly, Take);

                List<ReportRootModel> reportRootModelList = reportBaseService.DownloadSerializedJsonData<List<ReportRootModel>>(nameValueCollection);
                Assert.AreEqual(1, reportRootModelList.Count);
                Assert.IsFalse(string.IsNullOrWhiteSpace(reportRootModelList[0].Root_Error));
            }
        }
        [TestMethod]
        public void ReportBaseService_DownloadSerializedJsonData_Root_Error_URL_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Name");
                sbCommand.AppendLine("|||");

                string TagItem = "RootNot";
                int UnderTVItemID = 1;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                NameValueCollection nameValueCollection = CreateNameValueCollection(culture, sbCommand, TagItem, UnderTVItemID, ParentTagItem, CountOnly, Take);

                Assert.IsTrue(string.IsNullOrWhiteSpace(nameValueCollection["Error"]));

                List<ReportRootModel> reportRootModelList = reportBaseService.DownloadSerializedJsonData<List<ReportRootModel>>(nameValueCollection);
                Assert.AreEqual(0, reportRootModelList.Count);
                Assert.IsFalse(string.IsNullOrWhiteSpace(nameValueCollection["Error"]));
            }
        }
        [TestMethod]
        public void ReportBaseService_DownloadSerializedJsonData_Root_Error_UnderTVItemID_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Name");
                sbCommand.AppendLine("|||");

                string TagItem = "Root";
                int UnderTVItemID = 0;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                NameValueCollection nameValueCollection = CreateNameValueCollection(culture, sbCommand, TagItem, UnderTVItemID, ParentTagItem, CountOnly, Take);

                Assert.IsTrue(string.IsNullOrWhiteSpace(nameValueCollection["Error"]));

                List<ReportRootModel> reportRootModelList = reportBaseService.DownloadSerializedJsonData<List<ReportRootModel>>(nameValueCollection);
                Assert.AreEqual(1, reportRootModelList.Count);
                Assert.IsFalse(string.IsNullOrWhiteSpace(reportRootModelList[0].Root_Error));
            }
        }
        [TestMethod]
        public void ReportBaseService_DownloadSerializedJsonData_Root_Error_ParentTagItem_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Name");
                sbCommand.AppendLine("|||");

                string TagItem = "Root";
                int UnderTVItemID = 1;
                string ParentTagItem = "NotAParentTagItem";
                bool CountOnly = false;
                int Take = 2;

                NameValueCollection nameValueCollection = CreateNameValueCollection(culture, sbCommand, TagItem, UnderTVItemID, ParentTagItem, CountOnly, Take);

                Assert.IsTrue(string.IsNullOrWhiteSpace(nameValueCollection["Error"]));

                List<ReportRootModel> reportRootModelList = reportBaseService.DownloadSerializedJsonData<List<ReportRootModel>>(nameValueCollection);
                Assert.AreEqual(1, reportRootModelList.Count);
                Assert.IsFalse(string.IsNullOrWhiteSpace(reportRootModelList[0].Root_Error));

                ParentTagItem = "Area";

                nameValueCollection = CreateNameValueCollection(culture, sbCommand, TagItem, UnderTVItemID, ParentTagItem, CountOnly, Take);

                Assert.IsTrue(string.IsNullOrWhiteSpace(nameValueCollection["Error"]));

                reportRootModelList = reportBaseService.DownloadSerializedJsonData<List<ReportRootModel>>(nameValueCollection);
                Assert.AreEqual(1, reportRootModelList.Count);
                Assert.IsFalse(string.IsNullOrWhiteSpace(reportRootModelList[0].Root_Error));

            }
        }
        // Could be doing all the other urls with combination of Fields, UnderTVItemID, ParentTagName etc...
        [TestMethod]
        public void ReportBaseService_GetDataDirectlyFromDB_Root_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Name");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportRootModel";
                int UnderTVItemID = 1;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                List<ReportRootModel> reportRootModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.AreEqual(1, reportRootModelList.Count);
                Assert.AreEqual((culture.TwoLetterISOLanguageName == "fr" ? "Tous les endroits" : "All locations"), reportRootModelList[0].Root_Name);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetDataDirectlyFromDB_Root_Good_Count_Only_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Name");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportRootModel";
                int UnderTVItemID = 1;
                string ParentTagItem = "";
                bool CountOnly = true;
                int Take = 2;

                List<ReportRootModel> reportRootModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.AreEqual(1, reportRootModelList.Count);
                Assert.AreEqual(1, reportRootModelList[0].Root_Counter);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetDataDirectlyFromDB_Root_Error_Field_Name_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_NameNot");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportRootModel";
                int UnderTVItemID = 1;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                List<ReportRootModel> reportRootModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.AreEqual(1, reportRootModelList.Count);
                Assert.IsFalse(string.IsNullOrWhiteSpace(reportRootModelList[0].Root_Error));
            }
        }
        [TestMethod]
        public void ReportBaseService_GetDataDirectlyFromDB_Root_Error_UnderTVItemID_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Name");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportRootModel";
                int UnderTVItemID = 0;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                List<ReportRootModel> reportRootModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.AreEqual(1, reportRootModelList.Count);
                Assert.IsFalse(string.IsNullOrWhiteSpace(reportRootModelList[0].Root_Error));
            }
        }
        [TestMethod]
        public void ReportBaseService_GetDataDirectlyFromDB_Root_Error_ParentTagItem_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Name");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportRootModel";
                int UnderTVItemID = 1;
                string ParentTagItem = "NotAParentTagItem";
                bool CountOnly = false;
                int Take = 2;

                List<ReportRootModel> reportRootModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.AreEqual(1, reportRootModelList.Count);
                Assert.IsFalse(string.IsNullOrWhiteSpace(reportRootModelList[0].Root_Error));

                ParentTagItem = "Area";

                reportRootModelList = (dynamic)reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.AreEqual(1, reportRootModelList.Count);
                Assert.IsFalse(string.IsNullOrWhiteSpace(reportRootModelList[0].Root_Error));

            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportModelJSON_Good_Root_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Name");
                sbCommand.AppendLine("|||");

                ReportTag reportTag = new ReportTag();
                reportTag.Language = culture.TwoLetterISOLanguageName;
                reportTag.DocumentType = DocumentType.CSV;
                reportTag.CSVTagText = sbCommand.ToString();
                reportTag.ReportType = typeof(ReportRootModel);
                reportTag.ReportTagParent = null;
                reportTag.CountOnly = false;
                reportTag.Take = 2;
                reportTag.UnderTVItemID = 1;

                List<ReportRootModel> reportRootModelList = reportBaseService.GetReportModelJSON<List<ReportRootModel>>(reportTag);
                Assert.AreEqual(1, reportRootModelList.Count);
                Assert.AreEqual((culture.TwoLetterISOLanguageName == "fr" ? "Tous les endroits" : "All locations"), reportRootModelList[0].Root_Name);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportModelJSON_Error_Language_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Name");
                sbCommand.AppendLine("|||");

                ReportTag reportTag = new ReportTag();
                reportTag.Language = null;
                reportTag.DocumentType = DocumentType.CSV;
                reportTag.CSVTagText = sbCommand.ToString();
                reportTag.ReportType = typeof(ReportRootModel);
                reportTag.ReportTagParent = null;
                reportTag.CountOnly = false;
                reportTag.Take = 2;

                List<ReportRootModel> reportRootModelList = reportBaseService.GetReportModelJSON<List<ReportRootModel>>(reportTag);
                Assert.AreEqual(0, reportRootModelList.Count);
                Assert.AreEqual(string.Format(ReportServiceRes.AllowableLanguages_, "[en, fr]"), reportTag.Error);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportModelJSON_Error_DocumentType_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Name");
                sbCommand.AppendLine("|||");

                ReportTag reportTag = new ReportTag();
                reportTag.Language = culture.TwoLetterISOLanguageName;
                reportTag.DocumentType = (DocumentType)0; // will create error
                reportTag.CSVTagText = sbCommand.ToString();
                reportTag.ReportType = typeof(ReportRootModel);
                reportTag.ReportTagParent = null;
                reportTag.CountOnly = false;
                reportTag.Take = 2;

                List<ReportRootModel> reportRootModelList = reportBaseService.GetReportModelJSON<List<ReportRootModel>>(reportTag);
                Assert.AreEqual(0, reportRootModelList.Count);
                Assert.AreEqual(string.Format(ReportServiceRes.AllowableDocumentTypes_,
                    "[" + DocumentType.CSV.ToString() + "," + DocumentType.Excel.ToString() +
                    "," + DocumentType.GoogleEarth.ToString() + "," + DocumentType.Word.ToString() + "]"), reportTag.Error);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportModelJSON_Error_CSVTagText_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Name");
                sbCommand.AppendLine("|||");

                ReportTag reportTag = new ReportTag();
                reportTag.Language = culture.TwoLetterISOLanguageName;
                reportTag.DocumentType = DocumentType.CSV;
                reportTag.CSVTagText = ""; // sbCommand.ToString();
                reportTag.ReportType = typeof(ReportRootModel);
                reportTag.ReportTagParent = null;
                reportTag.CountOnly = false;
                reportTag.Take = 2;

                List<ReportRootModel> reportRootModelList = reportBaseService.GetReportModelJSON<List<ReportRootModel>>(reportTag);
                Assert.AreEqual(0, reportRootModelList.Count);
                Assert.AreEqual(ReportServiceRes.CSVTagTextIsEmpty, reportTag.Error);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportModelJSON_Error_Word_RangeStartTag_Text_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Name");
                sbCommand.AppendLine("|||");

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Range(); // should be empty

                ReportTag reportTag = new ReportTag();
                reportTag.Language = culture.TwoLetterISOLanguageName;
                reportTag.DocumentType = DocumentType.Word;
                reportTag.RangeStartTag = range;
                reportTag.CSVTagText = ""; // sbCommand.ToString();
                reportTag.ReportType = typeof(ReportRootModel);
                reportTag.ReportTagParent = null;
                reportTag.CountOnly = false;
                reportTag.Take = 2;

                List<ReportRootModel> reportRootModelList = reportBaseService.GetReportModelJSON<List<ReportRootModel>>(reportTag);
                Assert.AreEqual(0, reportRootModelList.Count);
                Assert.AreEqual(ReportServiceRes.RangeStartTagTextIsEmpty, reportTag.Error);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportModelJSON_Error_Excel_Not_Implemented_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Name");
                sbCommand.AppendLine("|||");

                ReportTag reportTag = new ReportTag();
                reportTag.Language = culture.TwoLetterISOLanguageName;
                reportTag.DocumentType = DocumentType.Excel;
                reportTag.CSVTagText = sbCommand.ToString();
                reportTag.ReportType = typeof(ReportRootModel);
                reportTag.ReportTagParent = null;
                reportTag.CountOnly = false;
                reportTag.Take = 2;

                List<ReportRootModel> reportRootModelList = reportBaseService.GetReportModelJSON<List<ReportRootModel>>(reportTag);
                Assert.AreEqual(0, reportRootModelList.Count);
                Assert.AreEqual(string.Format(ReportServiceRes._NotImplementedIn_, "Excel", "GetReportModelJSON"), reportTag.Error);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportModelJSON_Error_GoogleEarth_Not_Implemented_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Name");
                sbCommand.AppendLine("|||");

                ReportTag reportTag = new ReportTag();
                reportTag.Language = culture.TwoLetterISOLanguageName;
                reportTag.DocumentType = DocumentType.GoogleEarth;
                reportTag.CSVTagText = sbCommand.ToString();
                reportTag.ReportType = typeof(ReportRootModel);
                reportTag.ReportTagParent = null;
                reportTag.CountOnly = false;
                reportTag.Take = 2;

                List<ReportRootModel> reportRootModelList = reportBaseService.GetReportModelJSON<List<ReportRootModel>>(reportTag);
                Assert.AreEqual(0, reportRootModelList.Count);
                Assert.AreEqual(string.Format(ReportServiceRes._NotImplementedIn_, "GoogleEarth", "GetReportModelJSON"), reportTag.Error);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportModelJSON_Error_ReportType_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Name");
                sbCommand.AppendLine("|||");

                ReportTag reportTag = new ReportTag();
                reportTag.Language = culture.TwoLetterISOLanguageName;
                reportTag.DocumentType = DocumentType.CSV;
                reportTag.CSVTagText = sbCommand.ToString();
                reportTag.ReportType = null;
                reportTag.ReportTagParent = null;
                reportTag.CountOnly = false;
                reportTag.Take = 2;

                List<ReportRootModel> reportRootModelList = reportBaseService.GetReportModelJSON<List<ReportRootModel>>(reportTag);
                Assert.AreEqual(0, reportRootModelList.Count);
                Assert.AreEqual(string.Format(ReportServiceRes._IsRequiredIn_, "ReportType", "GetReportModelJSON"), reportTag.Error);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportModelJSON_Error_UnderTVItemID_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Name");
                sbCommand.AppendLine("|||");

                ReportTag reportTag = new ReportTag();
                reportTag.Language = culture.TwoLetterISOLanguageName;
                reportTag.DocumentType = DocumentType.CSV;
                reportTag.CSVTagText = sbCommand.ToString();
                reportTag.ReportType = typeof(ReportRootModel);
                reportTag.UnderTVItemID = 0; // 1;
                reportTag.ReportTagParent = null;
                reportTag.CountOnly = false;
                reportTag.Take = 2;

                List<ReportRootModel> reportRootModelList = reportBaseService.GetReportModelJSON<List<ReportRootModel>>(reportTag);
                Assert.AreEqual(1, reportRootModelList.Count);
                Assert.AreEqual(string.Format(ServiceRes.CouldNotFind_With_Equal_, ServiceRes.TVItem, ServiceRes.TVItemID, reportTag.UnderTVItemID.ToString()), reportRootModelList[0].Root_Error);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetDB_Good_Root_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Name");
                sbCommand.AppendLine("|||");

                ReportTag reportTag = new ReportTag();
                reportTag.Language = culture.TwoLetterISOLanguageName;
                reportTag.DocumentType = DocumentType.CSV;
                reportTag.CSVTagText = sbCommand.ToString();
                reportTag.ReportType = typeof(ReportRootModel);
                reportTag.UnderTVItemID = 1;
                reportTag.ReportTagParent = null;
                reportTag.CountOnly = false;
                reportTag.Take = 2;

                ReportModelDynamic reportModelDynamic = new ReportModelDynamic();

                string retStr = reportBaseService.ReportGetDB<ReportRootModel>(reportTag, reportModelDynamic);
                Assert.AreEqual("", retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetDB_Error_GetReportModelJSON_Start_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Name");
                sbCommand.AppendLine("|||");

                ReportTag reportTag = new ReportTag();
                reportTag.Language = culture.TwoLetterISOLanguageName;
                reportTag.DocumentType = DocumentType.CSV;
                reportTag.CSVTagText = sbCommand.ToString();
                reportTag.ReportType = typeof(ReportRootModel);
                reportTag.UnderTVItemID = 0; // will create error 1;
                reportTag.ReportTagParent = null;
                reportTag.CountOnly = false;
                reportTag.Take = 2;

                ReportModelDynamic reportModelDynamic = new ReportModelDynamic();

                string retStr = reportBaseService.ReportGetDB<ReportRootModel>(reportTag, reportModelDynamic);
                Assert.AreEqual("", retStr);
                Assert.IsNull(reportTag.Error);
                Assert.AreEqual(string.Format(ServiceRes.CouldNotFind_With_Equal_, ServiceRes.TVItem, ServiceRes.TVItemID, reportTag.UnderTVItemID.ToString()), ((List<ReportRootModel>)(reportModelDynamic.ReportModel)).FirstOrDefault().Root_Error);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetDB_Error_TagItem_Error_Exist_Start_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_NameNot"); // will create an error
                sbCommand.AppendLine("|||");

                ReportTag reportTag = new ReportTag();
                reportTag.Language = culture.TwoLetterISOLanguageName;
                reportTag.DocumentType = DocumentType.CSV;
                reportTag.CSVTagText = sbCommand.ToString();
                reportTag.ReportType = typeof(ReportRootModel);
                reportTag.ReportTagParent = null;
                reportTag.CountOnly = false;
                reportTag.Take = 2;
                reportTag.TagItem = "Root";

                ReportModelDynamic reportModelDynamic = new ReportModelDynamic();

                string retStr = reportBaseService.ReportGetDB<ReportRootModel>(reportTag, reportModelDynamic);
                Assert.IsFalse(string.IsNullOrWhiteSpace(retStr));
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetDB_Error_GetReportModelJSON_Loop_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Name");
                sbCommand.AppendLine("|||");

                ReportTag reportParentTag = new ReportTag();
                reportParentTag.Language = culture.TwoLetterISOLanguageName;
                reportParentTag.DocumentType = DocumentType.CSV;
                reportParentTag.CSVTagText = sbCommand.ToString();
                reportParentTag.ReportType = typeof(ReportRootModel);
                reportParentTag.UnderTVItemID = 1;
                reportParentTag.ReportTagParent = null;
                reportParentTag.CountOnly = false;
                reportParentTag.Take = 2;
                reportParentTag.TagName = "Root";
                reportParentTag.TagItem = "Root";
                reportParentTag.Guid = new Guid();

                ReportModelDynamic reportModelDynamic = new ReportModelDynamic();

                string retStr = reportBaseService.ReportGetDB<ReportRootModel>(reportParentTag, reportModelDynamic);
                Assert.IsTrue(string.IsNullOrWhiteSpace(retStr));
                Assert.AreEqual(1, ((List<ReportRootModel>)reportModelDynamic.ReportModel).Count());
                Assert.AreEqual((culture.TwoLetterISOLanguageName == "fr" ? "Tous les endroits" : "All locations"), ((List<ReportRootModel>)reportModelDynamic.ReportModel).Take(1).First().Root_Name);

                StringBuilder sbCommand2 = new StringBuilder();
                sbCommand2.AppendLine("|||Loop Country " + culture.TwoLetterISOLanguageName);
                sbCommand2.AppendLine("Country_Name");
                sbCommand2.AppendLine("|||");

                ReportTag reportTag = new ReportTag();
                reportTag.Language = culture.TwoLetterISOLanguageName;
                reportTag.DocumentType = DocumentType.CSV;
                reportTag.CSVTagText = sbCommand2.ToString();
                reportTag.ReportType = typeof(ReportCountryModel);
                reportTag.ReportTagParent = reportParentTag;
                reportTag.CountOnly = false;
                reportTag.Take = 2;
                reportTag.UnderTVItemID = 1;

                reportModelDynamic = new ReportModelDynamic();

                retStr = reportBaseService.ReportGetDB<ReportCountryModel>(reportTag, reportModelDynamic);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(2, ((List<ReportCountryModel>)reportModelDynamic.ReportModel).Count());
                Assert.AreEqual("Canada", ((List<ReportCountryModel>)reportModelDynamic.ReportModel).Take(1).First().Country_Name);
                Assert.AreEqual((culture.TwoLetterISOLanguageName == "fr" ? "Etats-Unis" : "United States"), ((List<ReportCountryModel>)reportModelDynamic.ReportModel).Skip(1).Take(1).First().Country_Name);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetDB_Error_TagItem_Error_Exist_Loop_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Name");
                sbCommand.AppendLine("|||");

                ReportTag reportParentTag = new ReportTag();
                reportParentTag.Language = culture.TwoLetterISOLanguageName;
                reportParentTag.DocumentType = DocumentType.CSV;
                reportParentTag.CSVTagText = sbCommand.ToString();
                reportParentTag.ReportType = typeof(ReportRootModel);
                reportParentTag.UnderTVItemID = 1;
                reportParentTag.ReportTagParent = null;
                reportParentTag.CountOnly = false;
                reportParentTag.Take = 2;
                reportParentTag.TagName = "Root";
                reportParentTag.TagItem = "Root";
                reportParentTag.Guid = new Guid();

                ReportModelDynamic reportModelDynamic = new ReportModelDynamic();

                string retStr = reportBaseService.ReportGetDB<ReportRootModel>(reportParentTag, reportModelDynamic);
                Assert.IsTrue(string.IsNullOrWhiteSpace(retStr));

                StringBuilder sbCommand2 = new StringBuilder();
                sbCommand2.AppendLine("|||Loop Country " + culture.TwoLetterISOLanguageName);
                sbCommand2.AppendLine("Country_NameNot");
                sbCommand2.AppendLine("|||");

                ReportTag reportTag = new ReportTag();
                reportTag.Language = culture.TwoLetterISOLanguageName;
                reportTag.DocumentType = DocumentType.CSV;
                reportTag.CSVTagText = sbCommand2.ToString();
                reportTag.ReportType = typeof(ReportCountryModel);
                reportTag.UnderTVItemID = 1;
                reportTag.ReportTagParent = reportParentTag;
                reportTag.CountOnly = false;
                reportTag.Take = 2;
                reportTag.TagItem = "Country";
                reportTag.TagName = "Country";

                retStr = reportBaseService.ReportGetDB<ReportCountryModel>(reportTag, reportModelDynamic);
                Assert.IsFalse(string.IsNullOrWhiteSpace(retStr));
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetDBOfType_Good_Root_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Name");
                sbCommand.AppendLine("|||");

                ReportTag reportTag = new ReportTag();
                reportTag.Language = culture.TwoLetterISOLanguageName;
                reportTag.DocumentType = DocumentType.CSV;
                reportTag.CSVTagText = sbCommand.ToString();
                reportTag.ReportType = typeof(ReportRootModel);
                reportTag.ReportTagParent = null;
                reportTag.CountOnly = false;
                reportTag.Take = 2;
                reportTag.TagItem = "Root";
                reportTag.UnderTVItemID = 1;

                ReportModelDynamic reportModelDynamic = new ReportModelDynamic();

                string retStr = reportBaseService.ReportGetDBOfType(reportTag, reportModelDynamic);
                Assert.AreEqual("", retStr);
                Assert.AreEqual((culture.TwoLetterISOLanguageName == "fr" ? "Tous les endroits" : "All locations"), ((List<ReportRootModel>)reportModelDynamic.ReportModel).First().Root_Name);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetDBOfType_Error_reportTag_TagItem_not_Implemented_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Name");
                sbCommand.AppendLine("|||");

                ReportTag reportTag = new ReportTag();
                reportTag.Language = culture.TwoLetterISOLanguageName;
                reportTag.DocumentType = DocumentType.CSV;
                reportTag.CSVTagText = sbCommand.ToString();
                reportTag.ReportType = typeof(ReportRootModel);
                reportTag.ReportTagParent = null;
                reportTag.CountOnly = false;
                reportTag.Take = 2;
                reportTag.TagItem = "RootNot";

                ReportModelDynamic reportModelDynamic = new ReportModelDynamic();

                string retStr = reportBaseService.ReportGetDBOfType(reportTag, reportModelDynamic);
                Assert.AreEqual(string.Format(ReportServiceRes._NotImplementedIn_, reportTag.TagItem, "ReportGetDBOfType"), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_System_DateTime_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Last_Update_Date_And_Time_UTC");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportRootModel";
                int UnderTVItemID = 1;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = 1,
                    TagItem = "Root",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Root_Last_Update_Date_And_Time_UTC" } },
                };

                List<ReportRootModel> reportRootModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.AreEqual(1, reportRootModelList.Count);
                Assert.AreEqual((culture.TwoLetterISOLanguageName == "fr" ? "Tous les endroits" : "All locations"), reportRootModelList[0].Root_Name);

                ReportRootModel reportRootModel = reportRootModelList[0];

                PropertyInfo propertyInfo = typeof(ReportRootModel).GetProperties().Where(c => c.Name == "Root_Last_Update_Date_And_Time_UTC").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                DateTime shouldReturnDateTime = new DateTime(2014, 12, 2, 16, 58, 16);
                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportRootModel>(reportRootModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(shouldReturnDateTime.Year, ((DateTime)obj).Year);
                Assert.AreEqual(shouldReturnDateTime.Month, ((DateTime)obj).Month);
                Assert.AreEqual(shouldReturnDateTime.Day, ((DateTime)obj).Day);
                Assert.AreEqual(shouldReturnDateTime.Hour, ((DateTime)obj).Hour);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_System_DateTime_With_Format_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Last_Update_Date_And_Time_UTC FORMAT yyyy-MMM");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportRootModel";
                int UnderTVItemID = 1;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = 1,
                    TagItem = "Root",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Root_Last_Update_Date_And_Time_UTC" } },
                };

                List<ReportRootModel> reportRootModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.AreEqual(1, reportRootModelList.Count);
                Assert.AreEqual((culture.TwoLetterISOLanguageName == "fr" ? "Tous les endroits" : "All locations"), reportRootModelList[0].Root_Name);

                ReportRootModel reportRootModel = reportRootModelList[0];

                PropertyInfo propertyInfo = typeof(ReportRootModel).GetProperties().Where(c => c.Name == "Root_Last_Update_Date_And_Time_UTC").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                reportTagStart.ReportTreeNodeList[0].dbFormatingField = new ReportFormatingField();
                reportTagStart.ReportTreeNodeList[0].dbFormatingField.DateFormating = "yyyy*MMM";

                //DateTime shouldReturnDateTime = new DateTime(2014, 12, 2, 16, 58, 16);
                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportRootModel>(reportRootModel, true, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                if (culture.TwoLetterISOLanguageName == "fr")
                {
                    Assert.AreEqual("2014 déc.", (string)obj);
                }
                else
                {
                    Assert.AreEqual("2014 Dec", (string)obj);
                }

                reportTagStart.ReportTreeNodeList[0].dbFormatingField.DateFormating = "nop";

                //DateTime shouldReturnDateTime = new DateTime(2014, 12, 2, 16, 58, 16);
                obj = reportBaseService.ReportGetFieldTextOrValue<ReportRootModel>(reportRootModel, true, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                if (culture.TwoLetterISOLanguageName == "fr")
                {
                    Assert.AreEqual("nop", (string)obj);
                }
                else
                {
                    Assert.AreEqual("nop", (string)obj);
                }
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_System_Boolean_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Is_Active");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportRootModel";
                int UnderTVItemID = 1;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = 1,
                    TagItem = "Root",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Root_Is_Active" } },
                };

                List<ReportRootModel> reportRootModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.AreEqual(1, reportRootModelList.Count);
                Assert.AreEqual((culture.TwoLetterISOLanguageName == "fr" ? "Tous les endroits" : "All locations"), reportRootModelList[0].Root_Name);

                ReportRootModel reportRootModel = reportRootModelList[0];

                PropertyInfo propertyInfo = typeof(ReportRootModel).GetProperties().Where(c => c.Name == "Root_Is_Active").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportRootModel>(reportRootModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(true, (bool)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_System_String_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Name");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportRootModel";
                int UnderTVItemID = 1;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = 1,
                    TagItem = "Root",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Root_Last_Update_Date_And_Time" } },
                };

                List<ReportRootModel> reportRootModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.AreEqual(1, reportRootModelList.Count);
                Assert.AreEqual((culture.TwoLetterISOLanguageName == "fr" ? "Tous les endroits" : "All locations"), reportRootModelList[0].Root_Name);

                ReportRootModel reportRootModel = reportRootModelList[0];

                PropertyInfo propertyInfo = typeof(ReportRootModel).GetProperties().Where(c => c.Name == "Root_Name").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportRootModel>(reportRootModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual((culture.TwoLetterISOLanguageName == "fr" ? "Tous les endroits" : "All locations"), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_System_Int32_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Stat_Country_Count");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportRootModel";
                int UnderTVItemID = 1;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = 1,
                    TagItem = "Root",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Root_Stat_Country_Count" } },
                };

                List<ReportRootModel> reportRootModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.AreEqual(1, reportRootModelList.Count);
                Assert.AreEqual((culture.TwoLetterISOLanguageName == "fr" ? "Tous les endroits" : "All locations"), reportRootModelList[0].Root_Name);

                ReportRootModel reportRootModel = reportRootModelList[0];

                PropertyInfo propertyInfo = typeof(ReportRootModel).GetProperties().Where(c => c.Name == "Root_Stat_Country_Count").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportRootModel>(reportRootModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(2, (Int32)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_System_Single_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Lat");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportRootModel";
                int UnderTVItemID = 1;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = 1,
                    TagItem = "Root",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Root_Lat" } },
                };

                List<ReportRootModel> reportRootModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.AreEqual(1, reportRootModelList.Count);
                Assert.AreEqual((culture.TwoLetterISOLanguageName == "fr" ? "Tous les endroits" : "All locations"), reportRootModelList[0].Root_Name);

                ReportRootModel reportRootModel = reportRootModelList[0];

                PropertyInfo propertyInfo = typeof(ReportRootModel).GetProperties().Where(c => c.Name == "Root_Lat").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportRootModel>(reportRootModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(50.0f, (float)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_System_Single_With_Format_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Lat");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportRootModel";
                int UnderTVItemID = 1;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = 1,
                    TagItem = "Root",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Root_Lat" } },
                };

                List<ReportRootModel> reportRootModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.AreEqual(1, reportRootModelList.Count);
                Assert.AreEqual((culture.TwoLetterISOLanguageName == "fr" ? "Tous les endroits" : "All locations"), reportRootModelList[0].Root_Name);

                ReportRootModel reportRootModel = reportRootModelList[0];

                PropertyInfo propertyInfo = typeof(ReportRootModel).GetProperties().Where(c => c.Name == "Root_Lat").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                reportTagStart.ReportTreeNodeList[0].dbFormatingField = new ReportFormatingField();
                reportTagStart.ReportTreeNodeList[0].dbFormatingField.NumberFormating = "F3";

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportRootModel>(reportRootModel, true, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual("50.000", (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_FilePurposeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Loop Root_File " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_File_File_Purpose");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportRoot_FileModel";
                int UnderTVItemID = 1;
                string ParentTagItem = "Root";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = 1,
                    TagItem = "Root",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Root_File_File_Purpose" } },
                };

                List<ReportRoot_FileModel> reportRoot_FileModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportRoot_FileModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportRoot_FileModelList[0].Root_File_Error));

                ReportRoot_FileModel reportRoot_FileModel = reportRoot_FileModelList[0];

                PropertyInfo propertyInfo = typeof(ReportRoot_FileModel).GetProperties().Where(c => c.Name == "Root_File_File_Purpose").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportRoot_FileModel>(reportRoot_FileModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_FilePurposeEnum(reportRoot_FileModel.Root_File_File_Purpose), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_FileTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Loop Root_File " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_File_File_Type");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportRoot_FileModel";
                int UnderTVItemID = 1;
                string ParentTagItem = "Root";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = 1,
                    TagItem = "Root",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Root_File_File_Type" } },
                };

                List<ReportRoot_FileModel> reportRoot_FileModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportRoot_FileModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportRoot_FileModelList[0].Root_File_Error));

                ReportRoot_FileModel reportRoot_FileModel = reportRoot_FileModelList[0];

                PropertyInfo propertyInfo = typeof(ReportRoot_FileModel).GetProperties().Where(c => c.Name == "Root_File_File_Type").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportRoot_FileModel>(reportRoot_FileModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_FileTypeEnum(reportRoot_FileModel.Root_File_File_Type), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_TranslationStatusEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Root " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_Name_Translation_Status");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportRootModel";
                int UnderTVItemID = 1;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = 1,
                    TagItem = "Root",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Root_Name_Translation_Status" } },
                };

                List<ReportRootModel> reportRootModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.AreEqual(1, reportRootModelList.Count);
                Assert.AreEqual((culture.TwoLetterISOLanguageName == "fr" ? "Tous les endroits" : "All locations"), reportRootModelList[0].Root_Name);

                ReportRootModel reportRootModel = reportRootModelList[0];

                PropertyInfo propertyInfo = typeof(ReportRootModel).GetProperties().Where(c => c.Name == "Root_Name_Translation_Status").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportRootModel>(reportRootModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_TranslationStatusEnum(reportRootModel.Root_Name_Translation_Status), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_BoxModelResultTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                BoxModelResult boxModelResult = (from c in tvItemService.db.BoxModelResults
                                                 where c.ResultType > 0
                                                 select c).FirstOrDefault();
                Assert.IsNotNull(boxModelResult);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Loop Box_Model_Result " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Box_Model_Result_Result_Type");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportBox_Model_ResultModel";
                int UnderTVItemID = boxModelResult.BoxModelID;
                string ParentTagItem = "Box_Model";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = boxModelResult.BoxModelID,
                    TagItem = "ReportBox_Model",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Box_Model_Result_Result_Type" } },
                };

                List<ReportBox_Model_ResultModel> reportBox_Model_ResultModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportBox_Model_ResultModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportBox_Model_ResultModelList[0].Box_Model_Result_Error));

                ReportBox_Model_ResultModel reportBox_Model_ResultModel = reportBox_Model_ResultModelList[0];

                PropertyInfo propertyInfo = typeof(ReportBox_Model_ResultModel).GetProperties().Where(c => c.Name == "Box_Model_Result_Result_Type").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportBox_Model_ResultModel>(reportBox_Model_ResultModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_BoxModelResultTypeEnum(reportBox_Model_ResultModel.Box_Model_Result_Result_Type), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_InfrastructureTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Infrastructure infrastructure = (from c in tvItemService.db.Infrastructures
                                                 where c.InfrastructureType > 0
                                                 select c).FirstOrDefault();
                Assert.IsNotNull(infrastructure);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Infrastructure " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Infrastructure_Infrastructure_Type");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportInfrastructureModel";
                int UnderTVItemID = infrastructure.InfrastructureTVItemID;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = infrastructure.InfrastructureTVItemID,
                    TagItem = "Infrastructure",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Infrastructure_Infrastructure_Type" } },
                };

                List<ReportInfrastructureModel> reportInfrastructureModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportInfrastructureModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportInfrastructureModelList[0].Infrastructure_Error));

                ReportInfrastructureModel reportInfrastructureModel = reportInfrastructureModelList[0];

                PropertyInfo propertyInfo = typeof(ReportInfrastructureModel).GetProperties().Where(c => c.Name == "Infrastructure_Infrastructure_Type").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportInfrastructureModel>(reportInfrastructureModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_InfrastructureTypeEnum(reportInfrastructureModel.Infrastructure_Infrastructure_Type), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_FacilityTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Infrastructure infrastructure = (from c in tvItemService.db.Infrastructures
                                                 where c.FacilityType > 0
                                                 select c).FirstOrDefault();
                Assert.IsNotNull(infrastructure);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Infrastructure " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Infrastructure_Facility_Type");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportInfrastructureModel";
                int UnderTVItemID = infrastructure.InfrastructureTVItemID;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = infrastructure.InfrastructureTVItemID,
                    TagItem = "Infrastructure",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Infrastructure_Facility_Type" } },
                };

                List<ReportInfrastructureModel> reportInfrastructureModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportInfrastructureModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportInfrastructureModelList[0].Infrastructure_Error));

                ReportInfrastructureModel reportInfrastructureModel = reportInfrastructureModelList[0];

                PropertyInfo propertyInfo = typeof(ReportInfrastructureModel).GetProperties().Where(c => c.Name == "Infrastructure_Facility_Type").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportInfrastructureModel>(reportInfrastructureModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_FacilityTypeEnum(reportInfrastructureModel.Infrastructure_Facility_Type), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_AerationTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Infrastructure infrastructure = (from c in tvItemService.db.Infrastructures
                                                 where c.AerationType > 0
                                                 select c).FirstOrDefault();
                Assert.IsNotNull(infrastructure);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Infrastructure " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Infrastructure_Aeration_Type");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportInfrastructureModel";
                int UnderTVItemID = infrastructure.InfrastructureTVItemID;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = infrastructure.InfrastructureTVItemID,
                    TagItem = "Infrastructure",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Infrastructure_Aeration_Type" } },
                };

                List<ReportInfrastructureModel> reportInfrastructureModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportInfrastructureModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportInfrastructureModelList[0].Infrastructure_Error));

                ReportInfrastructureModel reportInfrastructureModel = reportInfrastructureModelList[0];

                PropertyInfo propertyInfo = typeof(ReportInfrastructureModel).GetProperties().Where(c => c.Name == "Infrastructure_Aeration_Type").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportInfrastructureModel>(reportInfrastructureModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_AerationTypeEnum(reportInfrastructureModel.Infrastructure_Aeration_Type), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_PreliminaryTreatmentTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Infrastructure infrastructure = (from c in tvItemService.db.Infrastructures
                                                 where c.PreliminaryTreatmentType > 0
                                                 select c).FirstOrDefault();
                Assert.IsNotNull(infrastructure);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Infrastructure " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Infrastructure_Preliminary_Treatment_Type");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportInfrastructureModel";
                int UnderTVItemID = infrastructure.InfrastructureTVItemID;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = infrastructure.InfrastructureTVItemID,
                    TagItem = "Infrastructure",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Infrastructure_Preliminary_Treatment_Type" } },
                };

                List<ReportInfrastructureModel> reportInfrastructureModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportInfrastructureModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportInfrastructureModelList[0].Infrastructure_Error));

                ReportInfrastructureModel reportInfrastructureModel = reportInfrastructureModelList[0];

                PropertyInfo propertyInfo = typeof(ReportInfrastructureModel).GetProperties().Where(c => c.Name == "Infrastructure_Preliminary_Treatment_Type").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportInfrastructureModel>(reportInfrastructureModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_PreliminaryTreatmentTypeEnum(reportInfrastructureModel.Infrastructure_Preliminary_Treatment_Type), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_PrimaryTreatmentTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Infrastructure infrastructure = (from c in tvItemService.db.Infrastructures
                                                 where c.PrimaryTreatmentType > 0
                                                 select c).FirstOrDefault();
                Assert.IsNotNull(infrastructure);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Infrastructure " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Infrastructure_Primary_Treatment_Type");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportInfrastructureModel";
                int UnderTVItemID = infrastructure.InfrastructureTVItemID;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = infrastructure.InfrastructureTVItemID,
                    TagItem = "Infrastructure",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Infrastructure_Primary_Treatment_Type" } },
                };

                List<ReportInfrastructureModel> reportInfrastructureModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportInfrastructureModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportInfrastructureModelList[0].Infrastructure_Error));

                ReportInfrastructureModel reportInfrastructureModel = reportInfrastructureModelList[0];

                PropertyInfo propertyInfo = typeof(ReportInfrastructureModel).GetProperties().Where(c => c.Name == "Infrastructure_Primary_Treatment_Type").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportInfrastructureModel>(reportInfrastructureModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_PrimaryTreatmentTypeEnum(reportInfrastructureModel.Infrastructure_Primary_Treatment_Type), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_SecondaryTreatmentTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Infrastructure infrastructure = (from c in tvItemService.db.Infrastructures
                                                 where c.SecondaryTreatmentType > 0
                                                 select c).FirstOrDefault();
                Assert.IsNotNull(infrastructure);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Infrastructure " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Infrastructure_Secondary_Treatment_Type");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportInfrastructureModel";
                int UnderTVItemID = infrastructure.InfrastructureTVItemID;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = infrastructure.InfrastructureTVItemID,
                    TagItem = "Infrastructure",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Infrastructure_Secondary_Treatment_Type" } },
                };

                List<ReportInfrastructureModel> reportInfrastructureModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportInfrastructureModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportInfrastructureModelList[0].Infrastructure_Error));

                ReportInfrastructureModel reportInfrastructureModel = reportInfrastructureModelList[0];

                PropertyInfo propertyInfo = typeof(ReportInfrastructureModel).GetProperties().Where(c => c.Name == "Infrastructure_Secondary_Treatment_Type").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportInfrastructureModel>(reportInfrastructureModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_SecondaryTreatmentTypeEnum(reportInfrastructureModel.Infrastructure_Secondary_Treatment_Type), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_TertiaryTreatmentTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Infrastructure infrastructure = (from c in tvItemService.db.Infrastructures
                                                 where c.TertiaryTreatmentType > 0
                                                 select c).FirstOrDefault();
                Assert.IsNotNull(infrastructure);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Infrastructure " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Infrastructure_Tertiary_Treatment_Type");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportInfrastructureModel";
                int UnderTVItemID = infrastructure.InfrastructureTVItemID;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = infrastructure.InfrastructureTVItemID,
                    TagItem = "Infrastructure",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Infrastructure_Tertiary_Treatment_Type" } },
                };

                List<ReportInfrastructureModel> reportInfrastructureModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportInfrastructureModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportInfrastructureModelList[0].Infrastructure_Error));

                ReportInfrastructureModel reportInfrastructureModel = reportInfrastructureModelList[0];

                PropertyInfo propertyInfo = typeof(ReportInfrastructureModel).GetProperties().Where(c => c.Name == "Infrastructure_Tertiary_Treatment_Type").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportInfrastructureModel>(reportInfrastructureModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_TertiaryTreatmentTypeEnum(reportInfrastructureModel.Infrastructure_Tertiary_Treatment_Type), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_TreatmentTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Infrastructure infrastructure = (from c in tvItemService.db.Infrastructures
                                                 where c.TreatmentType > 0
                                                 select c).FirstOrDefault();
                Assert.IsNotNull(infrastructure);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Infrastructure " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Infrastructure_Treatment_Type");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportInfrastructureModel";
                int UnderTVItemID = infrastructure.InfrastructureTVItemID;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = infrastructure.InfrastructureTVItemID,
                    TagItem = "Infrastructure",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Infrastructure_Treatment_Type" } },
                };

                List<ReportInfrastructureModel> reportInfrastructureModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportInfrastructureModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportInfrastructureModelList[0].Infrastructure_Error));

                ReportInfrastructureModel reportInfrastructureModel = reportInfrastructureModelList[0];

                PropertyInfo propertyInfo = typeof(ReportInfrastructureModel).GetProperties().Where(c => c.Name == "Infrastructure_Treatment_Type").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportInfrastructureModel>(reportInfrastructureModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_TreatmentTypeEnum(reportInfrastructureModel.Infrastructure_Treatment_Type), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_DisinfectionTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Infrastructure infrastructure = (from c in tvItemService.db.Infrastructures
                                                 where c.DisinfectionType > 0
                                                 select c).FirstOrDefault();
                Assert.IsNotNull(infrastructure);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Infrastructure " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Infrastructure_Disinfection_Type");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportInfrastructureModel";
                int UnderTVItemID = infrastructure.InfrastructureTVItemID;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = infrastructure.InfrastructureTVItemID,
                    TagItem = "Infrastructure",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Infrastructure_Disinfection_Type" } },
                };

                List<ReportInfrastructureModel> reportInfrastructureModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportInfrastructureModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportInfrastructureModelList[0].Infrastructure_Error));

                ReportInfrastructureModel reportInfrastructureModel = reportInfrastructureModelList[0];

                PropertyInfo propertyInfo = typeof(ReportInfrastructureModel).GetProperties().Where(c => c.Name == "Infrastructure_Disinfection_Type").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportInfrastructureModel>(reportInfrastructureModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_DisinfectionTypeEnum(reportInfrastructureModel.Infrastructure_Disinfection_Type), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_CollectionSystemTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Infrastructure infrastructure = (from c in tvItemService.db.Infrastructures
                                                 where c.CollectionSystemType > 0
                                                 select c).FirstOrDefault();
                Assert.IsNotNull(infrastructure);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Infrastructure " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Infrastructure_Collection_System_Type");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportInfrastructureModel";
                int UnderTVItemID = infrastructure.InfrastructureTVItemID;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = infrastructure.InfrastructureTVItemID,
                    TagItem = "Infrastructure",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Infrastructure_Collection_System_Type" } },
                };

                List<ReportInfrastructureModel> reportInfrastructureModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportInfrastructureModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportInfrastructureModelList[0].Infrastructure_Error));

                ReportInfrastructureModel reportInfrastructureModel = reportInfrastructureModelList[0];

                PropertyInfo propertyInfo = typeof(ReportInfrastructureModel).GetProperties().Where(c => c.Name == "Infrastructure_Collection_System_Type").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportInfrastructureModel>(reportInfrastructureModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_CollectionSystemTypeEnum(reportInfrastructureModel.Infrastructure_Collection_System_Type), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_AlarmSystemTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Infrastructure infrastructure = (from c in tvItemService.db.Infrastructures
                                                 where c.AlarmSystemType > 0
                                                 select c).FirstOrDefault();
                Assert.IsNotNull(infrastructure);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Start Infrastructure " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Infrastructure_Alarm_System_Type");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportInfrastructureModel";
                int UnderTVItemID = infrastructure.InfrastructureTVItemID;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = infrastructure.InfrastructureTVItemID,
                    TagItem = "Infrastructure",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Infrastructure_Alarm_System_Type" } },
                };

                List<ReportInfrastructureModel> reportInfrastructureModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportInfrastructureModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportInfrastructureModelList[0].Infrastructure_Error));

                ReportInfrastructureModel reportInfrastructureModel = reportInfrastructureModelList[0];

                PropertyInfo propertyInfo = typeof(ReportInfrastructureModel).GetProperties().Where(c => c.Name == "Infrastructure_Alarm_System_Type").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportInfrastructureModel>(reportInfrastructureModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_AlarmSystemTypeEnum(reportInfrastructureModel.Infrastructure_Alarm_System_Type), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_ScenarioStatusEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                VPScenario vpScenario = (from c in tvItemService.db.VPScenarios
                                         where c.VPScenarioStatus > 0
                                         select c).FirstOrDefault();
                Assert.IsNotNull(vpScenario);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Loop Visual_Plumes_Scenario " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Visual_Plumes_Scenario_Status");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportVisual_Plumes_ScenarioModel";
                int UnderTVItemID = vpScenario.InfrastructureTVItemID;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = vpScenario.InfrastructureTVItemID,
                    TagItem = "Visual_Plumes_Scenario",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Visual_Plumes_Scenario_Status" } },
                };

                List<ReportVisual_Plumes_ScenarioModel> reportVisual_Plumes_ScenarioModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportVisual_Plumes_ScenarioModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportVisual_Plumes_ScenarioModelList[0].Visual_Plumes_Scenario_Error));

                ReportVisual_Plumes_ScenarioModel reportVisual_Plumes_ScenarioModel = reportVisual_Plumes_ScenarioModelList[0];

                PropertyInfo propertyInfo = typeof(ReportVisual_Plumes_ScenarioModel).GetProperties().Where(c => c.Name == "Visual_Plumes_Scenario_Status").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportVisual_Plumes_ScenarioModel>(reportVisual_Plumes_ScenarioModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_ScenarioStatusEnum(reportVisual_Plumes_ScenarioModel.Visual_Plumes_Scenario_Status), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_LanguageEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Loop Root_File " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Root_File_Language");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportRoot_FileModel";
                int UnderTVItemID = 1;
                string ParentTagItem = "Root";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = 1,
                    TagItem = "Root_File",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Root_File_Language" } },
                };

                List<ReportRoot_FileModel> reportRoot_FileModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportRoot_FileModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportRoot_FileModelList[0].Root_File_Error));

                ReportRoot_FileModel reportRoot_FileModel = reportRoot_FileModelList[0];

                PropertyInfo propertyInfo = typeof(ReportRoot_FileModel).GetProperties().Where(c => c.Name == "Root_File_Language").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportRoot_FileModel>(reportRoot_FileModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_LanguageEnum(reportRoot_FileModel.Root_File_Language), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_SampleTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                MWQMSample mwqmSample = (from c in tvItemService.db.MWQMSamples
                                         where c.SampleTypesText.Contains(((int)SampleTypeEnum.Routine).ToString())
                                         select c).FirstOrDefault();
                Assert.IsNotNull(mwqmSample);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Loop MWQM_Site_Sample " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("MWQM_Site_Sample_Types");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportMWQM_Site_SampleModel";
                int UnderTVItemID = mwqmSample.MWQMSiteTVItemID;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = mwqmSample.MWQMSiteTVItemID,
                    TagItem = "MWQM_Site",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "MWQM_Site_Sample_Types" } },
                };

                List<ReportMWQM_Site_SampleModel> reportMWQM_Site_SampleModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportMWQM_Site_SampleModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportMWQM_Site_SampleModelList[0].MWQM_Site_Sample_Error));

                ReportMWQM_Site_SampleModel reportMWQM_Site_SampleModel = reportMWQM_Site_SampleModelList[0];

                PropertyInfo propertyInfo = typeof(ReportMWQM_Site_SampleModel).GetProperties().Where(c => c.Name == "MWQM_Site_Sample_Types").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportMWQM_Site_SampleModel>(reportMWQM_Site_SampleModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(reportMWQM_Site_SampleModel.MWQM_Site_Sample_Types, (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_BeaufortScaleEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                MWQMRun mwqmRun = (from c in tvItemService.db.MWQMRuns
                                   where c.SeaStateAtStart_BeaufortScale > 0
                                   select c).FirstOrDefault();
                Assert.IsNotNull(mwqmRun);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Loop MWQM_Run " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("MWQM_Run_Sea_State_At_Start_Beaufort_Scale");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportMWQM_RunModel";
                int UnderTVItemID = mwqmRun.MWQMRunTVItemID;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = mwqmRun.MWQMRunTVItemID,
                    TagItem = "MWQM_Run",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "MWQM_Run_Sea_State_At_Start_Beaufort_Scale" } },
                };

                List<ReportMWQM_RunModel> reportMWQM_RunModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportMWQM_RunModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportMWQM_RunModelList[0].MWQM_Run_Error));

                ReportMWQM_RunModel reportMWQM_RunModel = reportMWQM_RunModelList[0];

                PropertyInfo propertyInfo = typeof(ReportMWQM_RunModel).GetProperties().Where(c => c.Name == "MWQM_Run_Sea_State_At_Start_Beaufort_Scale").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportMWQM_RunModel>(reportMWQM_RunModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_BeaufortScaleEnum(reportMWQM_RunModel.MWQM_Run_Sea_State_At_Start_Beaufort_Scale), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_AnalyzeMethodEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                MWQMRun mwqmRun = (from c in tvItemService.db.MWQMRuns
                                   where c.AnalyzeMethod > 0
                                   select c).FirstOrDefault();
                Assert.IsNotNull(mwqmRun);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Loop MWQM_Run " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("MWQM_Run_Analyze_Method");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportMWQM_RunModel";
                int UnderTVItemID = mwqmRun.MWQMRunTVItemID;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = mwqmRun.MWQMRunTVItemID,
                    TagItem = "MWQM_Run",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "MWQM_Run_Analyze_Method" } },
                };

                List<ReportMWQM_RunModel> reportMWQM_RunModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportMWQM_RunModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportMWQM_RunModelList[0].MWQM_Run_Error));

                ReportMWQM_RunModel reportMWQM_RunModel = reportMWQM_RunModelList[0];

                PropertyInfo propertyInfo = typeof(ReportMWQM_RunModel).GetProperties().Where(c => c.Name == "MWQM_Run_Analyze_Method").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportMWQM_RunModel>(reportMWQM_RunModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_AnalyzeMethodEnum(reportMWQM_RunModel.MWQM_Run_Analyze_Method), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_SampleMatrixEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                MWQMRun mwqmRun = (from c in tvItemService.db.MWQMRuns
                                   where c.SampleMatrix > 0
                                   select c).FirstOrDefault();
                Assert.IsNotNull(mwqmRun);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Loop MWQM_Run " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("MWQM_Run_Sample_Matrix");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportMWQM_RunModel";
                int UnderTVItemID = mwqmRun.MWQMRunTVItemID;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = mwqmRun.MWQMRunTVItemID,
                    TagItem = "MWQM_Run",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "MWQM_Run_Sample_Matrix" } },
                };

                List<ReportMWQM_RunModel> reportMWQM_RunModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportMWQM_RunModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportMWQM_RunModelList[0].MWQM_Run_Error));

                ReportMWQM_RunModel reportMWQM_RunModel = reportMWQM_RunModelList[0];

                PropertyInfo propertyInfo = typeof(ReportMWQM_RunModel).GetProperties().Where(c => c.Name == "MWQM_Run_Sample_Matrix").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportMWQM_RunModel>(reportMWQM_RunModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_SampleMatrixEnum(reportMWQM_RunModel.MWQM_Run_Sample_Matrix), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_LaboratoryEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                MWQMRun mwqmRun = (from c in tvItemService.db.MWQMRuns
                                   where c.Laboratory > 0
                                   select c).FirstOrDefault();
                Assert.IsNotNull(mwqmRun);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Loop MWQM_Run " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("MWQM_Run_Laboratory");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportMWQM_RunModel";
                int UnderTVItemID = mwqmRun.MWQMRunTVItemID;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = mwqmRun.MWQMRunTVItemID,
                    TagItem = "MWQM_Run",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "MWQM_Run_Laboratory" } },
                };

                List<ReportMWQM_RunModel> reportMWQM_RunModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportMWQM_RunModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportMWQM_RunModelList[0].MWQM_Run_Error));

                ReportMWQM_RunModel reportMWQM_RunModel = reportMWQM_RunModelList[0];

                PropertyInfo propertyInfo = typeof(ReportMWQM_RunModel).GetProperties().Where(c => c.Name == "MWQM_Run_Laboratory").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportMWQM_RunModel>(reportMWQM_RunModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_LaboratoryEnum(reportMWQM_RunModel.MWQM_Run_Laboratory), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_SampleStatusEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                MWQMRun mwqmRun = (from c in tvItemService.db.MWQMRuns
                                   where c.SampleStatus > 0
                                   select c).FirstOrDefault();
                Assert.IsNotNull(mwqmRun);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Loop MWQM_Run " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("MWQM_Run_Sample_Status");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportMWQM_RunModel";
                int UnderTVItemID = mwqmRun.MWQMRunTVItemID;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = mwqmRun.MWQMRunTVItemID,
                    TagItem = "MWQM_Run",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "MWQM_Run_Sample_Status" } },
                };

                List<ReportMWQM_RunModel> reportMWQM_RunModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportMWQM_RunModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportMWQM_RunModelList[0].MWQM_Run_Error));

                ReportMWQM_RunModel reportMWQM_RunModel = reportMWQM_RunModelList[0];

                PropertyInfo propertyInfo = typeof(ReportMWQM_RunModel).GetProperties().Where(c => c.Name == "MWQM_Run_Sample_Status").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportMWQM_RunModel>(reportMWQM_RunModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_SampleStatusEnum(reportMWQM_RunModel.MWQM_Run_Sample_Status), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_ConfigTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                LabSheet labSheet = (from c in tvItemService.db.LabSheets
                                     where c.ConfigType > 0
                                     && c.MWQMRunTVItemID > 0
                                     select c).FirstOrDefault();
                Assert.IsNotNull(labSheet);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Loop MWQM_Run_Lab_Sheet " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("MWQM_Run_Lab_Sheet_Config_Type");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportMWQM_Run_Lab_SheetModel";
                int UnderTVItemID = (int)labSheet.MWQMRunTVItemID;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = (int)labSheet.MWQMRunTVItemID,
                    TagItem = "MWQM_Run_Lab_Sheet",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "MWQM_Run_Lab_Sheet_Config_Type" } },
                };

                List<ReportMWQM_Run_Lab_SheetModel> reportMWQM_Run_Lab_SheetModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportMWQM_Run_Lab_SheetModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportMWQM_Run_Lab_SheetModelList[0].MWQM_Run_Lab_Sheet_Error));

                ReportMWQM_Run_Lab_SheetModel reportMWQM_Run_Lab_SheetModel = reportMWQM_Run_Lab_SheetModelList[0];

                PropertyInfo propertyInfo = typeof(ReportMWQM_Run_Lab_SheetModel).GetProperties().Where(c => c.Name == "MWQM_Run_Lab_Sheet_Config_Type").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportMWQM_Run_Lab_SheetModel>(reportMWQM_Run_Lab_SheetModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_ConfigTypeEnum(reportMWQM_Run_Lab_SheetModel.MWQM_Run_Lab_Sheet_Config_Type), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_LabSheetTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                LabSheet labSheet = (from c in tvItemService.db.LabSheets
                                     where c.LabSheetType > 0
                                     && c.MWQMRunTVItemID > 0
                                     select c).FirstOrDefault();
                Assert.IsNotNull(labSheet);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Loop MWQM_Run_Lab_Sheet " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("MWQM_Run_Lab_Sheet_Type");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportMWQM_Run_Lab_SheetModel";
                int UnderTVItemID = (int)labSheet.MWQMRunTVItemID;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = (int)labSheet.MWQMRunTVItemID,
                    TagItem = "MWQM_Run_Lab_Sheet",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "MWQM_Run_Lab_Sheet_Type" } },
                };

                List<ReportMWQM_Run_Lab_SheetModel> reportMWQM_Run_Lab_SheetModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportMWQM_Run_Lab_SheetModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportMWQM_Run_Lab_SheetModelList[0].MWQM_Run_Lab_Sheet_Error));

                ReportMWQM_Run_Lab_SheetModel reportMWQM_Run_Lab_SheetModel = reportMWQM_Run_Lab_SheetModelList[0];

                PropertyInfo propertyInfo = typeof(ReportMWQM_Run_Lab_SheetModel).GetProperties().Where(c => c.Name == "MWQM_Run_Lab_Sheet_Type").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportMWQM_Run_Lab_SheetModel>(reportMWQM_Run_Lab_SheetModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_LabSheetTypeEnum(reportMWQM_Run_Lab_SheetModel.MWQM_Run_Lab_Sheet_Type), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_LabSheetStatusEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                LabSheet labSheet = (from c in tvItemService.db.LabSheets
                                     where c.LabSheetStatus > 0
                                     && c.MWQMRunTVItemID > 0
                                     select c).FirstOrDefault();
                Assert.IsNotNull(labSheet);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Loop MWQM_Run_Lab_Sheet " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("MWQM_Run_Lab_Sheet_Status");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportMWQM_Run_Lab_SheetModel";
                int UnderTVItemID = (int)labSheet.MWQMRunTVItemID;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = (int)labSheet.MWQMRunTVItemID,
                    TagItem = "MWQM_Run_Lab_Sheet",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "MWQM_Run_Lab_Sheet_Status" } },
                };

                List<ReportMWQM_Run_Lab_SheetModel> reportMWQM_Run_Lab_SheetModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportMWQM_Run_Lab_SheetModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportMWQM_Run_Lab_SheetModelList[0].MWQM_Run_Lab_Sheet_Error));

                ReportMWQM_Run_Lab_SheetModel reportMWQM_Run_Lab_SheetModel = reportMWQM_Run_Lab_SheetModelList[0];

                PropertyInfo propertyInfo = typeof(ReportMWQM_Run_Lab_SheetModel).GetProperties().Where(c => c.Name == "MWQM_Run_Lab_Sheet_Status").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportMWQM_Run_Lab_SheetModel>(reportMWQM_Run_Lab_SheetModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_LabSheetStatusEnum(reportMWQM_Run_Lab_SheetModel.MWQM_Run_Lab_Sheet_Status), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_PolSourceInactiveReasonEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                PolSourceSite polSourceSite = (from c in tvItemService.db.PolSourceSites
                                               where c.InactiveReason > 0
                                               select c).FirstOrDefault();
                Assert.IsNotNull(polSourceSite);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Loop Pol_Source_Site " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Pol_Source_Site_Inactive_Reason");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportPol_Source_SiteModel";
                int UnderTVItemID = polSourceSite.PolSourceSiteTVItemID;
                string ParentTagItem = "";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = polSourceSite.PolSourceSiteTVItemID,
                    TagItem = "Pol_Source_Site",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Pol_Source_Site_Inactive_Reason" } },
                };

                List<ReportPol_Source_SiteModel> reportPol_Source_SiteModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportPol_Source_SiteModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportPol_Source_SiteModelList[0].Pol_Source_Site_Error));

                ReportPol_Source_SiteModel reportPol_Source_SiteModel = reportPol_Source_SiteModelList[0];

                PropertyInfo propertyInfo = typeof(ReportPol_Source_SiteModel).GetProperties().Where(c => c.Name == "Pol_Source_Site_Inactive_Reason").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportPol_Source_SiteModel>(reportPol_Source_SiteModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_PolSourceInactiveReasonEnum(reportPol_Source_SiteModel.Pol_Source_Site_Inactive_Reason), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_PolSourceObsInfoEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                PolSourceObservationIssue polSourceObservationIssue = (from c in tvItemService.db.PolSourceObservationIssues
                                                                       where c.ObservationInfo.Contains(((int)(PolSourceObsInfoEnum.FarmCommerical)).ToString())
                                                                       select c).FirstOrDefault();
                Assert.IsNotNull(polSourceObservationIssue);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Loop Pol_Source_Site_Obs_Issue " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Pol_Source_Site_Obs_Issue_Observation_Sentence");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportPol_Source_Site_Obs_IssueModel";
                int UnderTVItemID = polSourceObservationIssue.PolSourceObservationID;
                string ParentTagItem = "Pol_Source_Site_Obs";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = polSourceObservationIssue.PolSourceObservationID,
                    TagItem = "Pol_Source_Site_Obs_Issue",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Pol_Source_Site_Obs_Issue_Observation_Sentence" } },
                };

                List<ReportPol_Source_Site_Obs_IssueModel> reportPol_Source_Site_Obs_IssueModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportPol_Source_Site_Obs_IssueModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportPol_Source_Site_Obs_IssueModelList[0].Pol_Source_Site_Obs_Issue_Error));

                ReportPol_Source_Site_Obs_IssueModel reportPol_Source_Site_Obs_IssueModel = reportPol_Source_Site_Obs_IssueModelList[0];

                PropertyInfo propertyInfo = typeof(ReportPol_Source_Site_Obs_IssueModel).GetProperties().Where(c => c.Name == "Pol_Source_Site_Obs_Issue_Observation_Sentence").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportPol_Source_Site_Obs_IssueModel>(reportPol_Source_Site_Obs_IssueModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(reportPol_Source_Site_Obs_IssueModelList[0].Pol_Source_Site_Obs_Issue_Observation_Sentence, (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_AddressTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                PolSourceSite polSourceSite = (from c in tvItemService.db.PolSourceSites
                                               from a in tvItemService.db.Addresses
                                               where c.CivicAddressTVItemID == a.AddressTVItemID
                                               && c.CivicAddressTVItemID > 0
                                               && a.AddressType > 0
                                               select c).FirstOrDefault();
                Assert.IsNotNull(polSourceSite);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Loop Pol_Source_Site_Address " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Pol_Source_Site_Address_Type");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportPol_Source_Site_AddressModel";
                int UnderTVItemID = polSourceSite.PolSourceSiteTVItemID;
                string ParentTagItem = "Pol_Source_Site";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = polSourceSite.PolSourceSiteTVItemID,
                    TagItem = "Pol_Source_Site_Address",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Pol_Source_Site_Address_Type" } },
                };

                List<ReportPol_Source_Site_AddressModel> reportPol_Source_Site_AddressModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportPol_Source_Site_AddressModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportPol_Source_Site_AddressModelList[0].Pol_Source_Site_Address_Error));

                ReportPol_Source_Site_AddressModel reportPol_Source_Site_AddressModel = reportPol_Source_Site_AddressModelList[0];

                PropertyInfo propertyInfo = typeof(ReportPol_Source_Site_AddressModel).GetProperties().Where(c => c.Name == "Pol_Source_Site_Address_Type").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportPol_Source_Site_AddressModel>(reportPol_Source_Site_AddressModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_AddressTypeEnum(reportPol_Source_Site_AddressModel.Pol_Source_Site_Address_Type), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_StreetTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                PolSourceSite polSourceSite = (from c in tvItemService.db.PolSourceSites
                                               from a in tvItemService.db.Addresses
                                               where c.CivicAddressTVItemID == a.AddressTVItemID
                                               && c.CivicAddressTVItemID > 0
                                               && a.StreetType > 0
                                               select c).FirstOrDefault();
                Assert.IsNotNull(polSourceSite);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Loop Pol_Source_Site_Address " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Pol_Source_Site_Address_Street_Type");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportPol_Source_Site_AddressModel";
                int UnderTVItemID = polSourceSite.PolSourceSiteTVItemID;
                string ParentTagItem = "Pol_Source_Site";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = polSourceSite.PolSourceSiteTVItemID,
                    TagItem = "Pol_Source_Site_Address",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Pol_Source_Site_Address_Street_Type" } },
                };

                List<ReportPol_Source_Site_AddressModel> reportPol_Source_Site_AddressModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportPol_Source_Site_AddressModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportPol_Source_Site_AddressModelList[0].Pol_Source_Site_Address_Error));

                ReportPol_Source_Site_AddressModel reportPol_Source_Site_AddressModel = reportPol_Source_Site_AddressModelList[0];

                PropertyInfo propertyInfo = typeof(ReportPol_Source_Site_AddressModel).GetProperties().Where(c => c.Name == "Pol_Source_Site_Address_Street_Type").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportPol_Source_Site_AddressModel>(reportPol_Source_Site_AddressModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_StreetTypeEnum(reportPol_Source_Site_AddressModel.Pol_Source_Site_Address_Street_Type), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_ContactTitleEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                var contactMunicipality = (from t in tvItemService.db.TVItemLinks
                                           from c in tvItemService.db.Contacts
                                           where t.ToTVItemID == c.ContactTVItemID
                                           && t.FromTVType == (int)TVTypeEnum.Municipality
                                           && t.ToTVType == (int)TVTypeEnum.Contact
                                           && c.ContactTitle > 0
                                           select new { c, t }).FirstOrDefault();
                Assert.IsNotNull(contactMunicipality);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Loop Municipality_Contact " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Municipality_Contact_Title");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportMunicipality_ContactModel";
                int UnderTVItemID = contactMunicipality.t.FromTVItemID; // Municipality TVItemID
                string ParentTagItem = "Municipality";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = contactMunicipality.t.FromTVItemID,
                    TagItem = "Municipality_Contact",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Municipality_Contact_Title" } },
                };

                List<ReportMunicipality_ContactModel> reportMunicipality_ContactModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportMunicipality_ContactModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportMunicipality_ContactModelList[0].Municipality_Contact_Error));

                ReportMunicipality_ContactModel reportMunicipality_ContactModel = reportMunicipality_ContactModelList[0];

                PropertyInfo propertyInfo = typeof(ReportMunicipality_ContactModel).GetProperties().Where(c => c.Name == "Municipality_Contact_Title").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportMunicipality_ContactModel>(reportMunicipality_ContactModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_ContactTitleEnum(reportMunicipality_ContactModel.Municipality_Contact_Title), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_EmailTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                var contactEmail = (from t in tvItemService.db.TVItemLinks
                                    from e in tvItemService.db.Emails
                                    where t.ToTVItemID == e.EmailTVItemID
                                    && t.FromTVType == (int)TVTypeEnum.Contact
                                    && t.ToTVType == (int)TVTypeEnum.Email
                                    && e.EmailType > 0
                                    select new { e, t }).FirstOrDefault();
                Assert.IsNotNull(contactEmail);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Loop Municipality_Contact_Email " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Municipality_Contact_Email_Type");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportMunicipality_Contact_EmailModel";
                int UnderTVItemID = contactEmail.t.FromTVItemID; // Municipality TVItemID
                string ParentTagItem = "Municipality_Contact";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = contactEmail.t.FromTVItemID,
                    TagItem = "Municipality_Contact_Email",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Municipality_Contact_Email_Type" } },
                };

                List<ReportMunicipality_Contact_EmailModel> reportMunicipality_Contact_EmailModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportMunicipality_Contact_EmailModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportMunicipality_Contact_EmailModelList[0].Municipality_Contact_Email_Error));

                ReportMunicipality_Contact_EmailModel reportMunicipality_Contact_EmailModel = reportMunicipality_Contact_EmailModelList[0];

                PropertyInfo propertyInfo = typeof(ReportMunicipality_Contact_EmailModel).GetProperties().Where(c => c.Name == "Municipality_Contact_Email_Type").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportMunicipality_Contact_EmailModel>(reportMunicipality_Contact_EmailModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_EmailTypeEnum(reportMunicipality_Contact_EmailModel.Municipality_Contact_Email_Type), (string)obj);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReportGetFieldTextOrValue_Good_TelTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                var contactTel = (from t in tvItemService.db.TVItemLinks
                                  from e in tvItemService.db.Tels
                                  where t.ToTVItemID == e.TelTVItemID
                                  && t.FromTVType == (int)TVTypeEnum.Contact
                                  && t.ToTVType == (int)TVTypeEnum.Tel
                                  && e.TelType > 0
                                  select new { e, t }).FirstOrDefault();
                Assert.IsNotNull(contactTel);

                StringBuilder sbCommand = new StringBuilder();
                sbCommand.AppendLine("|||Loop Municipality_Contact_Tel " + culture.TwoLetterISOLanguageName);
                sbCommand.AppendLine("Municipality_Contact_Tel_Type");
                sbCommand.AppendLine("|||");

                LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
                string ReportTypeName = "ReportMunicipality_Contact_TelModel";
                int UnderTVItemID = contactTel.t.FromTVItemID; // Municipality TVItemID
                string ParentTagItem = "Municipality_Contact";
                bool CountOnly = false;
                int Take = 2;

                ReportTag reportTagStart = new ReportTag()
                {
                    wordApp = null,
                    doc = null,
                    OnlyImmediateChildren = true,
                    UnderTVItemID = contactTel.t.FromTVItemID,
                    TagItem = "Municipality_Contact_Tel",
                    Take = 2,
                    ReportType = typeof(ReportRootModel),
                    ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Municipality_Contact_Tel_Type" } },
                };

                List<ReportMunicipality_Contact_TelModel> reportMunicipality_Contact_TelModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
                Assert.IsTrue(reportMunicipality_Contact_TelModelList.Count > 0);
                Assert.IsTrue(string.IsNullOrWhiteSpace(reportMunicipality_Contact_TelModelList[0].Municipality_Contact_Tel_Error));

                ReportMunicipality_Contact_TelModel reportMunicipality_Contact_TelModel = reportMunicipality_Contact_TelModelList[0];

                PropertyInfo propertyInfo = typeof(ReportMunicipality_Contact_TelModel).GetProperties().Where(c => c.Name == "Municipality_Contact_Tel_Type").FirstOrDefault();
                Assert.IsNotNull(propertyInfo);

                object obj = reportBaseService.ReportGetFieldTextOrValue<ReportMunicipality_Contact_TelModel>(reportMunicipality_Contact_TelModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
                Assert.AreEqual(baseEnumService.GetEnumText_TelTypeEnum(reportMunicipality_Contact_TelModel.Municipality_Contact_Tel_Type), (string)obj);
            }
        }
        //[TestMethod]
        //public void ReportBaseService_ReportGetFieldTextOrValue_Good_TideTextEnum_Test()
        //{
        //    foreach (CultureInfo culture in setupData.cultureListGood)
        //    {
        //        SetupTest(culture);

        //        //Can't test it at this time. no data in db to test
        //        var subsectorTide = (from tdv in tvItemService.db.TideDataValues
        //                             from u in tvItemService.db.UseOfSites
        //                             where u.SiteTVItemID == tdv.TideSiteTVItemID
        //                             && u.SiteType == (int)SiteTypeEnum.Tide
        //                             && tdv.TideDataType > 0
        //                             select new { tdv, u }).FirstOrDefault();
        //        Assert.IsNotNull(subsectorTide);

        //        StringBuilder sbCommand = new StringBuilder();
        //        sbCommand.AppendLine("|||Loop Subsector_Tide_Site_Data " + culture.TwoLetterISOLanguageName);
        //        sbCommand.AppendLine("Subsector_Tide_Site_Data_Tide_Start");
        //        sbCommand.AppendLine("|||");

        //        LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
        //        string ReportTypeName = "ReportMunicipality_Contact_TelModel";
        //        int UnderTVItemID = subsectorTide.u.SubsectorTVItemID;
        //        string ParentTagItem = "Subsector_Tide_Site";
        //        bool CountOnly = false;
        //        int Take = 2;

        //        ReportTag reportTagStart = new ReportTag()
        //        {
        //            wordApp = null,
        //            doc = null,
        //            OnlyImmediateChildren = true,
        //            UnderTVItemID = subsectorTide.u.SubsectorTVItemID,
        //            TagItem = "Subsector_Tide_Site_Data",
        //            Take = 2,
        //            ReportType = typeof(ReportRootModel),
        //            ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Subsector_Tide_Site_Data_Tide_Start" } },
        //        };

        //        List<ReportSubsector_Tide_Site_DataModel> reportSubsector_Tide_Site_DataModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
        //        Assert.IsTrue(reportSubsector_Tide_Site_DataModelList.Count > 0);
        //        Assert.IsTrue(string.IsNullOrWhiteSpace(reportSubsector_Tide_Site_DataModelList[0].Subsector_Tide_Site_Data_Error));

        //        ReportSubsector_Tide_Site_DataModel reportSubsector_Tide_Site_DataModel = reportSubsector_Tide_Site_DataModelList[0];

        //        PropertyInfo propertyInfo = typeof(ReportSubsector_Tide_Site_DataModel).GetProperties().Where(c => c.Name == "Subsector_Tide_Site_Data_Tide_Start").FirstOrDefault();
        //        Assert.IsNotNull(propertyInfo);

        //        object obj = reportBaseService.ReportGetFieldTextOrValue<ReportSubsector_Tide_Site_DataModel>(reportSubsector_Tide_Site_DataModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
        //        Assert.AreEqual(baseEnumService.GetEnumText_TideTextEnum(reportSubsector_Tide_Site_DataModel.Subsector_Tide_Site_Data_Tide_Start), (string)obj);
        //    }
        //}
        //[TestMethod]
        //public void ReportBaseService_ReportGetFieldTextOrValue_Good_TideDataTypeEnum_Test()
        //{
        //    foreach (CultureInfo culture in setupData.cultureListGood)
        //    {
        //        SetupTest(culture);

        //        //Can't test it at this time. no data in db to test
        //        var subsectorTide = (from tdv in tvItemService.db.TideDataValues
        //                             from u in tvItemService.db.UseOfSites
        //                             where u.SiteTVItemID == tdv.TideSiteTVItemID
        //                             && u.SiteType == (int)SiteTypeEnum.Tide
        //                             && tdv.TideDataType > 0
        //                             select new { tdv, u }).FirstOrDefault();
        //        Assert.IsNotNull(subsectorTide);

        //        StringBuilder sbCommand = new StringBuilder();
        //        sbCommand.AppendLine("|||Loop Subsector_Tide_Site_Data " + culture.TwoLetterISOLanguageName);
        //        sbCommand.AppendLine("Subsector_Tide_Site_Data_Tide_Start");
        //        sbCommand.AppendLine("|||");

        //        LanguageEnum Language = (culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en);
        //        string ReportTypeName = "ReportMunicipality_Contact_TelModel";
        //        int UnderTVItemID = subsectorTide.u.SubsectorTVItemID;
        //        string ParentTagItem = "Subsector_Tide_Site";
        //        bool CountOnly = false;
        //        int Take = 2;

        //        ReportTag reportTagStart = new ReportTag()
        //        {
        //            wordApp = null,
        //            doc = null,
        //            OnlyImmediateChildren = true,
        //            UnderTVItemID = subsectorTide.u.SubsectorTVItemID,
        //            TagItem = "Subsector_Tide_Site_Data",
        //            Take = 2,
        //            ReportType = typeof(ReportRootModel),
        //            ReportTreeNodeList = new List<ReportTreeNode>() { new ReportTreeNode() { Text = "Subsector_Tide_Site_Data_Tide_Data_Type" } },
        //        };

        //        List<ReportSubsector_Tide_Site_DataModel> reportSubsector_Tide_Site_DataModelList = reportBaseService.GetDataDirectlyFromDB(ReportTypeName, Language, sbCommand.ToString(), UnderTVItemID, ParentTagItem, CountOnly, Take);
        //        Assert.IsTrue(reportSubsector_Tide_Site_DataModelList.Count > 0);
        //        Assert.IsTrue(string.IsNullOrWhiteSpace(reportSubsector_Tide_Site_DataModelList[0].Subsector_Tide_Site_Data_Error));

        //        ReportSubsector_Tide_Site_DataModel reportSubsector_Tide_Site_DataModel = reportSubsector_Tide_Site_DataModelList[0];

        //        PropertyInfo propertyInfo = typeof(ReportSubsector_Tide_Site_DataModel).GetProperties().Where(c => c.Name == "Subsector_Tide_Site_Data_Tide_Data_Type").FirstOrDefault();
        //        Assert.IsNotNull(propertyInfo);

        //        object obj = reportBaseService.ReportGetFieldTextOrValue<ReportSubsector_Tide_Site_DataModel>(reportSubsector_Tide_Site_DataModel, false, propertyInfo, "", reportTagStart, reportTagStart.ReportTreeNodeList[0]);
        //        Assert.AreEqual(baseEnumService.GetEnumText_TideDataTypeEnum(reportSubsector_Tide_Site_DataModel.Subsector_Tide_Site_Data_Tide_Data_Type), (string)obj);
        //    }
        //}
        #endregion Testing Methods Generic Functions
        #region Testing Methods Public
        [TestMethod]
        public void ReportBaseService_CreateReportTreeNodeItem_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string TheText = "Root_Is_Active";
                ReportTreeNode reportTreeNode = reportBaseService.CreateReportTreeNodeItem(TheText);
                Assert.AreEqual(TheText, reportTreeNode.Text);
                Assert.AreEqual(ReportTreeNodeTypeEnum.Error, reportTreeNode.ReportTreeNodeType);
                Assert.AreEqual(ReportTreeNodeSubTypeEnum.Error, reportTreeNode.ReportTreeNodeSubType);
                Assert.AreEqual(ReportFieldTypeEnum.Error, reportTreeNode.ReportFieldType);
                Assert.AreEqual(ReportSortingEnum.Error, reportTreeNode.dbSortingField.ReportSorting);
                Assert.AreEqual(0, reportTreeNode.dbSortingField.Ordinal);
                Assert.AreEqual(ReportFormatingDateEnum.Error, reportTreeNode.reportFormatingField.ReportFormatingDate);
                Assert.AreEqual(ReportFormatingNumberEnum.Error, reportTreeNode.reportFormatingField.ReportFormatingNumber);
                Assert.AreEqual(0, reportTreeNode.dbFilteringDateFieldList.Count);
                Assert.AreEqual(0, reportTreeNode.dbFilteringNumberFieldList.Count);
                Assert.AreEqual(0, reportTreeNode.dbFilteringTextFieldList.Count);
                Assert.AreEqual(0, reportTreeNode.dbFilteringTrueFalseFieldList.Count);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetAllowableParentTagItem_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string TagItem = "Root";
                List<string> TagItemList = reportBaseService.GetAllowableParentTagItem(TagItem);
                Assert.IsTrue(TagItemList.Count == 0);

                TagItem = "Country";
                TagItemList = reportBaseService.GetAllowableParentTagItem(TagItem);
                Assert.IsTrue(TagItemList.Count == 1);
                Assert.AreEqual("Root", TagItemList[0]);

                TagItem = "Province";
                TagItemList = reportBaseService.GetAllowableParentTagItem(TagItem);
                Assert.IsTrue(TagItemList.Count == 2);
                Assert.AreEqual("Root", TagItemList[0]);
                Assert.AreEqual("Country", TagItemList[1]);

                TagItem = "Area";
                TagItemList = reportBaseService.GetAllowableParentTagItem(TagItem);
                Assert.IsTrue(TagItemList.Count == 3);
                Assert.AreEqual("Root", TagItemList[0]);
                Assert.AreEqual("Country", TagItemList[1]);
                Assert.AreEqual("Province", TagItemList[2]);

                TagItem = "Sector";
                TagItemList = reportBaseService.GetAllowableParentTagItem(TagItem);
                Assert.IsTrue(TagItemList.Count == 4);
                Assert.AreEqual("Root", TagItemList[0]);
                Assert.AreEqual("Country", TagItemList[1]);
                Assert.AreEqual("Province", TagItemList[2]);
                Assert.AreEqual("Area", TagItemList[3]);

                TagItem = "Subsector";
                TagItemList = reportBaseService.GetAllowableParentTagItem(TagItem);
                Assert.IsTrue(TagItemList.Count == 5);
                Assert.AreEqual("Root", TagItemList[0]);
                Assert.AreEqual("Country", TagItemList[1]);
                Assert.AreEqual("Province", TagItemList[2]);
                Assert.AreEqual("Area", TagItemList[3]);
                Assert.AreEqual("Sector", TagItemList[4]);

                TagItem = "MWQM_Site";
                TagItemList = reportBaseService.GetAllowableParentTagItem(TagItem);
                Assert.IsTrue(TagItemList.Count == 6);
                Assert.AreEqual("Root", TagItemList[0]);
                Assert.AreEqual("Country", TagItemList[1]);
                Assert.AreEqual("Province", TagItemList[2]);
                Assert.AreEqual("Area", TagItemList[3]);
                Assert.AreEqual("Sector", TagItemList[4]);
                Assert.AreEqual("Subsector", TagItemList[5]);

                TagItem = "MWQM_Run";
                TagItemList = reportBaseService.GetAllowableParentTagItem(TagItem);
                Assert.IsTrue(TagItemList.Count == 6);
                Assert.AreEqual("Root", TagItemList[0]);
                Assert.AreEqual("Country", TagItemList[1]);
                Assert.AreEqual("Province", TagItemList[2]);
                Assert.AreEqual("Area", TagItemList[3]);
                Assert.AreEqual("Sector", TagItemList[4]);
                Assert.AreEqual("Subsector", TagItemList[5]);

                TagItem = "Pol_Source_Site";
                TagItemList = reportBaseService.GetAllowableParentTagItem(TagItem);
                Assert.IsTrue(TagItemList.Count == 6);
                Assert.AreEqual("Root", TagItemList[0]);
                Assert.AreEqual("Country", TagItemList[1]);
                Assert.AreEqual("Province", TagItemList[2]);
                Assert.AreEqual("Area", TagItemList[3]);
                Assert.AreEqual("Sector", TagItemList[4]);
                Assert.AreEqual("Subsector", TagItemList[5]);

                TagItem = "Municipality";
                TagItemList = reportBaseService.GetAllowableParentTagItem(TagItem);
                Assert.IsTrue(TagItemList.Count == 6);
                Assert.AreEqual("Root", TagItemList[0]);
                Assert.AreEqual("Country", TagItemList[1]);
                Assert.AreEqual("Province", TagItemList[2]);
                Assert.AreEqual("Area", TagItemList[3]);
                Assert.AreEqual("Sector", TagItemList[4]);
                Assert.AreEqual("Subsector", TagItemList[5]);

                TagItem = "Infrastructure";
                TagItemList = reportBaseService.GetAllowableParentTagItem(TagItem);
                Assert.IsTrue(TagItemList.Count == 7);
                Assert.AreEqual("Root", TagItemList[0]);
                Assert.AreEqual("Country", TagItemList[1]);
                Assert.AreEqual("Province", TagItemList[2]);
                Assert.AreEqual("Area", TagItemList[3]);
                Assert.AreEqual("Sector", TagItemList[4]);
                Assert.AreEqual("Subsector", TagItemList[5]);
                Assert.AreEqual("Municipality", TagItemList[6]);

                // Should continue in the future
            }
        }
        [TestMethod]
        public void ReportBaseService_GetAllTheDateFilters_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.DateAndTime;
                int Current = 1;
                bool IsDBFiltering = true;
                int LineCount = 2;
                List<string> strList = new List<string>()
                {
                    "Root_Last_Update_Date_And_Time_UTC"
                };
                string retStr = reportBaseService.GetAllTheDateFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(0, reportTreeNode.dbFilteringDateFieldList.Count);
                Assert.AreEqual(0, reportTreeNode.reportConditionDateFieldList.Count);

                strList = new List<string>()
                {
                    "Root_Last_Update_Date_And_Time_UTC", "EQUAL", "YEAR", "2010"
                };
                retStr = reportBaseService.GetAllTheDateFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(1, reportTreeNode.dbFilteringDateFieldList.Count);
                Assert.AreEqual(ReportConditionEnum.ReportConditionEqual, reportTreeNode.dbFilteringDateFieldList[0].ReportCondition);
                Assert.AreEqual(2010, reportTreeNode.dbFilteringDateFieldList[0].DateTimeConditionYear);
                Assert.AreEqual(0, reportTreeNode.reportConditionDateFieldList.Count);

                IsDBFiltering = false;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.DateAndTime;

                retStr = reportBaseService.GetAllTheDateFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(0, reportTreeNode.dbFilteringDateFieldList.Count);
                Assert.AreEqual(1, reportTreeNode.reportConditionDateFieldList.Count);
                Assert.AreEqual(ReportConditionEnum.ReportConditionEqual, reportTreeNode.reportConditionDateFieldList[0].ReportCondition);
                Assert.AreEqual(2010, reportTreeNode.reportConditionDateFieldList[0].DateTimeConditionYear);

                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.DateAndTime;

                strList = new List<string>()
                {
                    "Root_Last_Update_Date_And_Time_UTC", "EQUAL", "YEAR", "2010", "MONTH", "5"
                };
                retStr = reportBaseService.GetAllTheDateFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(1, reportTreeNode.dbFilteringDateFieldList.Count);
                Assert.AreEqual(ReportConditionEnum.ReportConditionEqual, reportTreeNode.dbFilteringDateFieldList[0].ReportCondition);
                Assert.AreEqual(2010, reportTreeNode.dbFilteringDateFieldList[0].DateTimeConditionYear);
                Assert.AreEqual(5, reportTreeNode.dbFilteringDateFieldList[0].DateTimeConditionMonth);
                Assert.AreEqual(0, reportTreeNode.reportConditionDateFieldList.Count);

                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.DateAndTime;

                strList = new List<string>()
                {
                    "Root_Last_Update_Date_And_Time_UTC", "BIGGER", "YEAR", "2010", "SMALLER", "YEAR", "2014"
                };
                retStr = reportBaseService.GetAllTheDateFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(2, reportTreeNode.dbFilteringDateFieldList.Count);
                Assert.AreEqual(ReportConditionEnum.ReportConditionBigger, reportTreeNode.dbFilteringDateFieldList[0].ReportCondition);
                Assert.AreEqual(2010, reportTreeNode.dbFilteringDateFieldList[0].DateTimeConditionYear);
                Assert.AreEqual(ReportConditionEnum.ReportConditionSmaller, reportTreeNode.dbFilteringDateFieldList[1].ReportCondition);
                Assert.AreEqual(2014, reportTreeNode.dbFilteringDateFieldList[1].DateTimeConditionYear);
                Assert.AreEqual(0, reportTreeNode.reportConditionDateFieldList.Count);

                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.DateAndTime;

                strList = new List<string>()
                {
                    "Root_Last_Update_Date_And_Time_UTC", "BIGGER", "YEAR", "2010", "SMALLER", "YEAR", "2014"
                };
                retStr = reportBaseService.GetAllTheDateFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(2, reportTreeNode.dbFilteringDateFieldList.Count);
                Assert.AreEqual(ReportConditionEnum.ReportConditionBigger, reportTreeNode.dbFilteringDateFieldList[0].ReportCondition);
                Assert.AreEqual(2010, reportTreeNode.dbFilteringDateFieldList[0].DateTimeConditionYear);
                Assert.AreEqual(ReportConditionEnum.ReportConditionSmaller, reportTreeNode.dbFilteringDateFieldList[1].ReportCondition);
                Assert.AreEqual(2014, reportTreeNode.dbFilteringDateFieldList[1].DateTimeConditionYear);
                Assert.AreEqual(0, reportTreeNode.reportConditionDateFieldList.Count);

                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.DateAndTime;

                strList = new List<string>()
                {
                    "Root_Last_Update_Date_And_Time_UTC", "BIGGER", "YEAR", "2010", "MONTH", "5", "SMALLER", "YEAR", "2014"
                };
                retStr = reportBaseService.GetAllTheDateFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(2, reportTreeNode.dbFilteringDateFieldList.Count);
                Assert.AreEqual(ReportConditionEnum.ReportConditionBigger, reportTreeNode.dbFilteringDateFieldList[0].ReportCondition);
                Assert.AreEqual(2010, reportTreeNode.dbFilteringDateFieldList[0].DateTimeConditionYear);
                Assert.AreEqual(ReportConditionEnum.ReportConditionSmaller, reportTreeNode.dbFilteringDateFieldList[1].ReportCondition);
                Assert.AreEqual(2014, reportTreeNode.dbFilteringDateFieldList[1].DateTimeConditionYear);
                Assert.AreEqual(0, reportTreeNode.reportConditionDateFieldList.Count);


                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.DateAndTime;

                strList = new List<string>()
                {
                    "Root_Last_Update_Date_And_Time_UTC", "EQUALNot", "2010"
                };
                retStr = reportBaseService.GetAllTheDateFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, Current + 1, String.Join(",", AllowableBasicFilters)), retStr);

                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.DateAndTime;

                strList = new List<string>()
                {
                    "Root_Last_Update_Date_And_Time_UTC", "EQUAL", "YEAR"
                };
                retStr = reportBaseService.GetAllTheDateFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Condition_MissingValue, strList[1]), retStr);

                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.DateAndTime;

                strList = new List<string>()
                {
                    "Root_Last_Update_Date_And_Time_UTC", "EQUAL", "YEAR", "2012", "MONTH"
                };
                retStr = reportBaseService.GetAllTheDateFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Condition_MissingValue, strList[4]), retStr);

                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.DateAndTime;

                strList = new List<string>()
                {
                    "Root_Last_Update_Date_And_Time_UTC", "EQUAL", "YEAR", "MONTH"
                };
                retStr = reportBaseService.GetAllTheDateFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.ConditionTextMissingNumberAfter_, strList[2]), retStr);

                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.DateAndTime;

                strList = new List<string>()
                {
                    "Root_Last_Update_Date_And_Time_UTC", "EQUAL", "YEAR", "a"
                };
                retStr = reportBaseService.GetAllTheDateFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.ConditionTextMissingNumberAfter_, strList[2]), retStr);

                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.DateAndTime;

                strList = new List<string>()
                {
                    "Root_Last_Update_Date_And_Time_UTC", "EQUAL", "YEAR", "3.3"
                };
                retStr = reportBaseService.GetAllTheDateFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.NumberWithoutDecimalIsRequiredAfter_, strList[2]), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetAllTheEnumFilters_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.FilePurpose;
                int Current = 1;
                bool IsDBFiltering = true;
                int LineCount = 2;
                List<string> strList = new List<string>()
                {
                    "Root_File_File_Purpose"
                };
                string retStr = reportBaseService.GetAllTheEnumFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(0, reportTreeNode.dbFilteringEnumFieldList.Count);
                Assert.AreEqual(0, reportTreeNode.reportConditionEnumFieldList.Count);

                strList = new List<string>()
                {
                    "Root_File_File_Purpose", "EQUAL", "MikeInput"
                };
                retStr = reportBaseService.GetAllTheEnumFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(1, reportTreeNode.dbFilteringEnumFieldList.Count);
                Assert.AreEqual(ReportConditionEnum.ReportConditionEqual, reportTreeNode.dbFilteringEnumFieldList[0].ReportCondition);
                Assert.AreEqual("MikeInput", reportTreeNode.dbFilteringEnumFieldList[0].EnumConditionText);
                Assert.AreEqual(0, reportTreeNode.reportConditionEnumFieldList.Count);

                IsDBFiltering = false;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.FilePurpose;

                retStr = reportBaseService.GetAllTheEnumFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(0, reportTreeNode.dbFilteringEnumFieldList.Count);
                Assert.AreEqual(1, reportTreeNode.reportConditionEnumFieldList.Count);
                Assert.AreEqual(ReportConditionEnum.ReportConditionEqual, reportTreeNode.reportConditionEnumFieldList[0].ReportCondition);
                Assert.AreEqual("MikeInput", reportTreeNode.reportConditionEnumFieldList[0].EnumConditionText);

                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.FilePurpose;

                strList = new List<string>()
                {
                    "Root_File_File_Purpose", "EQUAL", "MikeInput", "EQUAL", "MikeInputMDF"
                };
                retStr = reportBaseService.GetAllTheEnumFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(2, reportTreeNode.dbFilteringEnumFieldList.Count);
                Assert.AreEqual(ReportConditionEnum.ReportConditionEqual, reportTreeNode.dbFilteringEnumFieldList[0].ReportCondition);
                Assert.AreEqual("MikeInput", reportTreeNode.dbFilteringEnumFieldList[0].EnumConditionText);
                Assert.AreEqual(ReportConditionEnum.ReportConditionEqual, reportTreeNode.dbFilteringEnumFieldList[1].ReportCondition);
                Assert.AreEqual("MikeInputMDF", reportTreeNode.dbFilteringEnumFieldList[1].EnumConditionText);
                Assert.AreEqual(0, reportTreeNode.reportConditionEnumFieldList.Count);

                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.FilePurpose;

                strList = new List<string>()
                {
                    "Root_File_File_Purpose", "EQUALNot", "MikeInput"
                };
                retStr = reportBaseService.GetAllTheEnumFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, Current + 1, String.Join(",", AllowableEnumFilters)), retStr);

                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.FilePurpose;

                strList = new List<string>()
                {
                    "Root_File_File_Purpose", "EQUAL"
                };
                retStr = reportBaseService.GetAllTheEnumFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Condition_MissingValue, strList[1]), retStr);

                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.FilePurpose;

                strList = new List<string>()
                {
                    "Root_File_File_Purpose", "EQUAL", "MikeInput", "MikeInput"
                };
                retStr = reportBaseService.GetAllTheEnumFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, 4, String.Join(",", AllowableEnumFilters)), retStr);

                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.FilePurpose;

                strList = new List<string>()
                {
                    "Root_File_File_Purpose", "EQUAL", "MikeInput", "EQUAL"
                };
                retStr = reportBaseService.GetAllTheEnumFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Condition_MissingValue, strList[3]), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetAllTheNumberFilters_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.NumberWithDecimal;
                int Current = 1;
                bool IsDBFiltering = true;
                int LineCount = 2;

                IsDBFiltering = false;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.NumberWithDecimal;

                List<string> strList = new List<string>()
                {
                    "Root_Lat"
                };
                string retStr = reportBaseService.GetAllTheNumberFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(0, reportTreeNode.dbFilteringNumberFieldList.Count);
                Assert.AreEqual(0, reportTreeNode.reportConditionNumberFieldList.Count);

                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.NumberWithDecimal;

                strList = new List<string>()
                {
                    "Root_Lat", "EQUAL", "3.3"
                };
                retStr = reportBaseService.GetAllTheNumberFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(1, reportTreeNode.dbFilteringNumberFieldList.Count);
                Assert.AreEqual(ReportConditionEnum.ReportConditionEqual, reportTreeNode.dbFilteringNumberFieldList[0].ReportCondition);
                Assert.AreEqual(3.3f, reportTreeNode.dbFilteringNumberFieldList[0].NumberCondition);
                Assert.AreEqual(0, reportTreeNode.reportConditionNumberFieldList.Count);

                IsDBFiltering = false;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.NumberWithDecimal;

                retStr = reportBaseService.GetAllTheNumberFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(0, reportTreeNode.dbFilteringNumberFieldList.Count);
                Assert.AreEqual(1, reportTreeNode.reportConditionNumberFieldList.Count);
                Assert.AreEqual(ReportConditionEnum.ReportConditionEqual, reportTreeNode.reportConditionNumberFieldList[0].ReportCondition);
                Assert.AreEqual(3.3f, reportTreeNode.reportConditionNumberFieldList[0].NumberCondition);

                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.NumberWithDecimal;

                strList = new List<string>()
                {
                    "Root_Lat", "EQUAL", "3.3", "EQUAL", "4.4"
                };
                retStr = reportBaseService.GetAllTheNumberFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(2, reportTreeNode.dbFilteringNumberFieldList.Count);
                Assert.AreEqual(ReportConditionEnum.ReportConditionEqual, reportTreeNode.dbFilteringNumberFieldList[0].ReportCondition);
                Assert.AreEqual(3.3f, reportTreeNode.dbFilteringNumberFieldList[0].NumberCondition);
                Assert.AreEqual(ReportConditionEnum.ReportConditionEqual, reportTreeNode.dbFilteringNumberFieldList[1].ReportCondition);
                Assert.AreEqual(4.4f, reportTreeNode.dbFilteringNumberFieldList[1].NumberCondition);
                Assert.AreEqual(0, reportTreeNode.reportConditionNumberFieldList.Count);

                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.NumberWithDecimal;

                strList = new List<string>()
                {
                    "Root_Lat", "EQUALNot", "3.3"
                };
                retStr = reportBaseService.GetAllTheNumberFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, Current + 1, String.Join(",", AllowableBasicFilters)), retStr);

                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.NumberWithDecimal;

                strList = new List<string>()
                {
                    "Root_Lat", "EQUAL"
                };
                retStr = reportBaseService.GetAllTheNumberFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Condition_MissingValue, strList[1]), retStr);

                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.NumberWithDecimal;

                strList = new List<string>()
                {
                    "Root_Lat", "EQUAL", "3.3", "3.3"
                };
                retStr = reportBaseService.GetAllTheNumberFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, 4, String.Join(",", AllowableBasicFilters)), retStr);

                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.NumberWithDecimal;

                strList = new List<string>()
                {
                    "Root_Lat", "EQUAL", "3.3", "EQUAL"
                };
                retStr = reportBaseService.GetAllTheNumberFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Condition_MissingValue, strList[3]), retStr);

                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.NumberWithDecimal;

                strList = new List<string>()
                {
                    "Root_Lat", "EQUAL", "3.3", "EQUAL", "a"
                };
                retStr = reportBaseService.GetAllTheNumberFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.ConditionTextMissingNumberAfter_, strList[3]), retStr);


                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.NumberWhole;

                strList = new List<string>()
                {
                    "Root_Stat_Subsector_Count", "EQUAL", "3.3"
                };
                retStr = reportBaseService.GetAllTheNumberFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.NumberWithoutDecimalIsRequiredAfter_, strList[1]), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetAllTheTextFilters_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.Text;
                int Current = 1;
                bool IsDBFiltering = true;
                int LineCount = 2;

                IsDBFiltering = false;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.Text;

                List<string> strList = new List<string>()
                {
                    "Root_Name"
                };
                string retStr = reportBaseService.GetAllTheTextFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(0, reportTreeNode.dbFilteringTextFieldList.Count);
                Assert.AreEqual(0, reportTreeNode.reportConditionTextFieldList.Count);

                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.Text;

                strList = new List<string>()
                {
                    "Root_Name", "EQUAL", "All"
                };
                retStr = reportBaseService.GetAllTheTextFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(1, reportTreeNode.dbFilteringTextFieldList.Count);
                Assert.AreEqual(ReportConditionEnum.ReportConditionEqual, reportTreeNode.dbFilteringTextFieldList[0].ReportCondition);
                Assert.AreEqual("All", reportTreeNode.dbFilteringTextFieldList[0].TextCondition);
                Assert.AreEqual(0, reportTreeNode.reportConditionTextFieldList.Count);

                IsDBFiltering = false;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.Text;

                retStr = reportBaseService.GetAllTheTextFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(0, reportTreeNode.dbFilteringTextFieldList.Count);
                Assert.AreEqual(1, reportTreeNode.reportConditionTextFieldList.Count);
                Assert.AreEqual(ReportConditionEnum.ReportConditionEqual, reportTreeNode.reportConditionTextFieldList[0].ReportCondition);
                Assert.AreEqual("All", reportTreeNode.reportConditionTextFieldList[0].TextCondition);

                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.Text;

                strList = new List<string>()
                {
                    "Root_Name", "EQUAL", "All", "EQUAL", "All2"
                };
                retStr = reportBaseService.GetAllTheTextFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(2, reportTreeNode.dbFilteringTextFieldList.Count);
                Assert.AreEqual(ReportConditionEnum.ReportConditionEqual, reportTreeNode.dbFilteringTextFieldList[0].ReportCondition);
                Assert.AreEqual("All", reportTreeNode.dbFilteringTextFieldList[0].TextCondition);
                Assert.AreEqual(ReportConditionEnum.ReportConditionEqual, reportTreeNode.dbFilteringTextFieldList[1].ReportCondition);
                Assert.AreEqual("All2", reportTreeNode.dbFilteringTextFieldList[1].TextCondition);
                Assert.AreEqual(0, reportTreeNode.reportConditionTextFieldList.Count);

                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.Text;

                strList = new List<string>()
                {
                    "Root_Name", "EQUALNot", "All"
                };
                retStr = reportBaseService.GetAllTheTextFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, Current + 1, String.Join(",", AllowableTextFilters)), retStr);

                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.Text;

                strList = new List<string>()
                {
                    "Root_Name", "EQUAL"
                };
                retStr = reportBaseService.GetAllTheTextFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Condition_MissingValue, strList[1]), retStr);

                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.Text;

                strList = new List<string>()
                {
                    "Root_Name", "EQUAL", "All", "All2"
                };
                retStr = reportBaseService.GetAllTheTextFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, 4, String.Join(",", AllowableTextFilters)), retStr);

                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.Text;

                strList = new List<string>()
                {
                    "Root_Name", "EQUAL", "All", "EQUAL"
                };
                retStr = reportBaseService.GetAllTheTextFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Condition_MissingValue, strList[3]), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetAllTheTrueFalseFilters_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.TrueOrFalse;
                int Current = 1;
                bool IsDBFiltering = true;
                int LineCount = 2;

                IsDBFiltering = false;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.TrueOrFalse;

                List<string> strList = new List<string>()
                {
                    "Root_Is_Active"
                };
                string retStr = reportBaseService.GetAllTheTrueFalseFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(0, reportTreeNode.dbFilteringTrueFalseFieldList.Count);
                Assert.AreEqual(0, reportTreeNode.reportConditionTrueFalseFieldList.Count);

                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.TrueOrFalse;

                strList = new List<string>()
                {
                    "Root_Is_Active", "TRUE"
                };
                retStr = reportBaseService.GetAllTheTrueFalseFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(1, reportTreeNode.dbFilteringTrueFalseFieldList.Count);
                Assert.AreEqual(ReportConditionEnum.ReportConditionTrue, reportTreeNode.dbFilteringTrueFalseFieldList[0].ReportCondition);
                Assert.AreEqual(0, reportTreeNode.reportConditionTrueFalseFieldList.Count);

                IsDBFiltering = false;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.TrueOrFalse;

                retStr = reportBaseService.GetAllTheTrueFalseFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(0, reportTreeNode.dbFilteringTrueFalseFieldList.Count);
                Assert.AreEqual(1, reportTreeNode.reportConditionTrueFalseFieldList.Count);
                Assert.AreEqual(ReportConditionEnum.ReportConditionTrue, reportTreeNode.reportConditionTrueFalseFieldList[0].ReportCondition);

                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.TrueOrFalse;

                strList = new List<string>()
                {
                    "Root_Is_Active", "TRUE", "FALSE"
                };
                retStr = reportBaseService.GetAllTheTrueFalseFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(2, reportTreeNode.dbFilteringTrueFalseFieldList.Count);
                Assert.AreEqual(ReportConditionEnum.ReportConditionTrue, reportTreeNode.dbFilteringTrueFalseFieldList[0].ReportCondition);
                Assert.AreEqual(ReportConditionEnum.ReportConditionFalse, reportTreeNode.dbFilteringTrueFalseFieldList[1].ReportCondition);
                Assert.AreEqual(0, reportTreeNode.reportConditionTrueFalseFieldList.Count);

                IsDBFiltering = true;
                reportTreeNode = new ReportTreeNode();
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.TrueOrFalse;

                strList = new List<string>()
                {
                    "Root_Is_Active", "TrueNot"
                };
                retStr = reportBaseService.GetAllTheTrueFalseFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, Current + 1, String.Join(",", AllowableTrueFalseFilters)), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetDateFormatText_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                for (int i = 0, count = Enum.GetNames(typeof(ReportFormatingDateEnum)).Count(); i < count; i++)
                {
                    string retStr = reportBaseService.GetDateFormatText((ReportFormatingDateEnum)i);
                    switch ((ReportFormatingDateEnum)i)
                    {
                        case ReportFormatingDateEnum.ReportFormatingDateYearOnly:
                            Assert.AreEqual("yyyy", retStr);
                            break;
                        case ReportFormatingDateEnum.ReportFormatingDateMonthDecimalOnly:
                            Assert.AreEqual("MM", retStr);
                            break;
                        case ReportFormatingDateEnum.ReportFormatingDateMonthShortTextOnly:
                            Assert.AreEqual("MMM", retStr);
                            break;
                        case ReportFormatingDateEnum.ReportFormatingDateMonthFullTextOnly:
                            Assert.AreEqual("MMMM", retStr);
                            break;
                        case ReportFormatingDateEnum.ReportFormatingDateDayOnly:
                            Assert.AreEqual("dd", retStr);
                            break;
                        case ReportFormatingDateEnum.ReportFormatingDateHourOnly:
                            Assert.AreEqual("HH", retStr);
                            break;
                        case ReportFormatingDateEnum.ReportFormatingDateMinuteOnly:
                            Assert.AreEqual("mm", retStr);
                            break;
                        case ReportFormatingDateEnum.ReportFormatingDateYearMonthDecimalDay:
                            Assert.AreEqual("yyyy MM dd", retStr);
                            break;
                        case ReportFormatingDateEnum.ReportFormatingDateYearMonthShortTextDay:
                            Assert.AreEqual("yyyy MMM dd", retStr);
                            break;
                        case ReportFormatingDateEnum.ReportFormatingDateYearMonthFullTextDay:
                            Assert.AreEqual("yyyy MMMM dd", retStr);
                            break;
                        case ReportFormatingDateEnum.ReportFormatingDateYearMonthDecimalDayHourMinute:
                            Assert.AreEqual("yyyy MM dd HH:mm", retStr);
                            break;
                        case ReportFormatingDateEnum.ReportFormatingDateYearMonthShortTextDayHourMinute:
                            Assert.AreEqual("yyyy MMM dd HH:mm", retStr);
                            break;
                        case ReportFormatingDateEnum.ReportFormatingDateYearMonthFullTextDayHourMinute:
                            Assert.AreEqual("yyyy MMMM dd HH:mm", retStr);
                            break;
                        default:
                            Assert.AreEqual("Error", ((ReportFormatingDateEnum)i).ToString());
                            break;
                    }
                }
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldDBFilterText_DateAndTime_No_Filtering_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_Last_Update_Date_And_Time_UTC";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                StringBuilder sb = new StringBuilder();
                reportBaseService.GetFieldDBFilterText(reportTreeNode, sb);
                Assert.AreEqual("", sb.ToString());
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldDBFilterText_DateAndTime_With_Sorting_ASCENDING_1_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_Last_Update_Date_And_Time_UTC";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                reportTreeNode.dbSortingField = new ReportSortingField();

                reportTreeNode.dbSortingField.ReportSorting = ReportSortingEnum.ReportSortingAscending;
                reportTreeNode.dbSortingField.Ordinal = 1;

                StringBuilder sb = new StringBuilder();
                reportBaseService.GetFieldDBFilterText(reportTreeNode, sb);
                Assert.AreEqual(" ASCENDING 1", sb.ToString());
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldDBFilterText_DateAndTime_With_Filtering_BIGGER_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_Last_Update_Date_And_Time_UTC";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                reportTreeNode.dbFilteringDateFieldList.Add(new ReportConditionDateField());
                reportTreeNode.dbFilteringDateFieldList[0].ReportCondition = ReportConditionEnum.ReportConditionBigger;
                reportTreeNode.dbFilteringDateFieldList[0].DateTimeConditionYear = 1988;
                reportTreeNode.dbFilteringDateFieldList[0].DateTimeConditionMonth = 7;
                reportTreeNode.dbFilteringDateFieldList[0].DateTimeConditionDay = 8;
                reportTreeNode.dbFilteringDateFieldList[0].DateTimeConditionHour = 9;
                reportTreeNode.dbFilteringDateFieldList[0].DateTimeConditionMinute = 10;

                StringBuilder sb = new StringBuilder();
                reportBaseService.GetFieldDBFilterText(reportTreeNode, sb);
                Assert.AreEqual(" " + baseEnumService.GetEnumText_ReportConditionEnum(reportTreeNode.dbFilteringDateFieldList[0].ReportCondition) +
                    " YEAR 1988 MONTH 7 DAY 8 HOUR 9 MINUTE 10", sb.ToString());
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldDBFilterText_DateAndTime_With_Filtering_BIGGER_Partial_Date_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_Last_Update_Date_And_Time_UTC";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                reportTreeNode.dbFilteringDateFieldList.Add(new ReportConditionDateField());
                reportTreeNode.dbFilteringDateFieldList[0].ReportCondition = ReportConditionEnum.ReportConditionBigger;
                reportTreeNode.dbFilteringDateFieldList[0].DateTimeConditionYear = 1988;
                reportTreeNode.dbFilteringDateFieldList[0].DateTimeConditionDay = 8;
                reportTreeNode.dbFilteringDateFieldList[0].DateTimeConditionMinute = 10;

                StringBuilder sb = new StringBuilder();
                reportBaseService.GetFieldDBFilterText(reportTreeNode, sb);
                Assert.AreEqual(" " + baseEnumService.GetEnumText_ReportConditionEnum(reportTreeNode.dbFilteringDateFieldList[0].ReportCondition) +
                    " YEAR 1988 DAY 8 MINUTE 10", sb.ToString());
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldDBFilterText_Number_Whole_No_Filtering_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_ID";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                StringBuilder sb = new StringBuilder();
                reportBaseService.GetFieldDBFilterText(reportTreeNode, sb);
                Assert.AreEqual("", sb.ToString());
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldDBFilterText_Number_Whole_With_Sorting_ASCENDING_1_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_ID";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                reportTreeNode.dbSortingField = new ReportSortingField();

                reportTreeNode.dbSortingField.ReportSorting = ReportSortingEnum.ReportSortingAscending;
                reportTreeNode.dbSortingField.Ordinal = 1;

                StringBuilder sb = new StringBuilder();
                reportBaseService.GetFieldDBFilterText(reportTreeNode, sb);
                Assert.AreEqual(" ASCENDING 1", sb.ToString());
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldDBFilterText_Number_Whole_With_Filtering_BIGGER_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_ID";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                reportTreeNode.dbFilteringNumberFieldList.Add(new ReportConditionNumberField());
                reportTreeNode.dbFilteringNumberFieldList[0].ReportCondition = ReportConditionEnum.ReportConditionBigger;
                reportTreeNode.dbFilteringNumberFieldList[0].NumberCondition = 23;

                StringBuilder sb = new StringBuilder();
                reportBaseService.GetFieldDBFilterText(reportTreeNode, sb);
                Assert.AreEqual(" " + baseEnumService.GetEnumText_ReportConditionEnum(reportTreeNode.dbFilteringNumberFieldList[0].ReportCondition) +
                    " 23", sb.ToString());
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldDBFilterText_Number_Decimal_No_Filtering_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_Lat";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                StringBuilder sb = new StringBuilder();
                reportBaseService.GetFieldDBFilterText(reportTreeNode, sb);
                Assert.AreEqual("", sb.ToString());
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldDBFilterText_Number_Decimal_With_Sorting_ASCENDING_1_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_Lat";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                reportTreeNode.dbSortingField = new ReportSortingField();

                reportTreeNode.dbSortingField.ReportSorting = ReportSortingEnum.ReportSortingAscending;
                reportTreeNode.dbSortingField.Ordinal = 1;

                StringBuilder sb = new StringBuilder();
                reportBaseService.GetFieldDBFilterText(reportTreeNode, sb);
                Assert.AreEqual(" ASCENDING 1", sb.ToString());
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldDBFilterText_Number_Decimal_With_Filtering_BIGGER_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_Lat";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                reportTreeNode.dbFilteringNumberFieldList.Add(new ReportConditionNumberField());
                reportTreeNode.dbFilteringNumberFieldList[0].ReportCondition = ReportConditionEnum.ReportConditionBigger;
                reportTreeNode.dbFilteringNumberFieldList[0].NumberCondition = culture.TwoLetterISOLanguageName == "fr" ? float.Parse("23.3".Replace(".", ",")) : 23.3f;

                StringBuilder sb = new StringBuilder();
                reportBaseService.GetFieldDBFilterText(reportTreeNode, sb);
                Assert.AreEqual(" " + baseEnumService.GetEnumText_ReportConditionEnum(reportTreeNode.dbFilteringNumberFieldList[0].ReportCondition) +
                    " 23.3", sb.ToString());
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldDBFilterText_Text_No_Filtering_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_Name";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                StringBuilder sb = new StringBuilder();
                reportBaseService.GetFieldDBFilterText(reportTreeNode, sb);
                Assert.AreEqual("", sb.ToString());
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldDBFilterText_Text_Sorting_ASCENDING_1_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_Name";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                reportTreeNode.dbSortingField = new ReportSortingField();

                reportTreeNode.dbSortingField.ReportSorting = ReportSortingEnum.ReportSortingAscending;
                reportTreeNode.dbSortingField.Ordinal = 1;

                StringBuilder sb = new StringBuilder();
                reportBaseService.GetFieldDBFilterText(reportTreeNode, sb);
                Assert.AreEqual(" ASCENDING 1", sb.ToString());
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldDBFilterText_Text_1_Filtering_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_Name";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                reportTreeNode.dbFilteringTextFieldList.Add(new ReportConditionTextField());
                reportTreeNode.dbFilteringTextFieldList[0].ReportCondition = ReportConditionEnum.ReportConditionContain;
                reportTreeNode.dbFilteringTextFieldList[0].TextCondition = "r";

                StringBuilder sb = new StringBuilder();
                reportBaseService.GetFieldDBFilterText(reportTreeNode, sb);
                Assert.AreEqual(" " + baseEnumService.GetEnumText_ReportConditionEnum(reportTreeNode.dbFilteringTextFieldList[0].ReportCondition) +
                    " r", sb.ToString());
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldDBFilterText_Text_2_Filtering_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_Name";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                reportTreeNode.dbFilteringTextFieldList.Add(new ReportConditionTextField());
                reportTreeNode.dbFilteringTextFieldList[0].ReportCondition = ReportConditionEnum.ReportConditionContain;
                reportTreeNode.dbFilteringTextFieldList[0].TextCondition = "r";

                reportTreeNode.dbFilteringTextFieldList.Add(new ReportConditionTextField());
                reportTreeNode.dbFilteringTextFieldList[1].ReportCondition = ReportConditionEnum.ReportConditionContain;
                reportTreeNode.dbFilteringTextFieldList[1].TextCondition = "tet";

                StringBuilder sb = new StringBuilder();
                reportBaseService.GetFieldDBFilterText(reportTreeNode, sb);
                Assert.AreEqual(" " + baseEnumService.GetEnumText_ReportConditionEnum(reportTreeNode.dbFilteringTextFieldList[0].ReportCondition) +
                    " r" + " " + baseEnumService.GetEnumText_ReportConditionEnum(reportTreeNode.dbFilteringTextFieldList[1].ReportCondition) +
                    " tet", sb.ToString());
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldDBFilterText_Text_3_Filtering_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_Name";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                reportTreeNode.dbFilteringTextFieldList.Add(new ReportConditionTextField());
                reportTreeNode.dbFilteringTextFieldList[0].ReportCondition = ReportConditionEnum.ReportConditionContain;
                reportTreeNode.dbFilteringTextFieldList[0].TextCondition = "r";

                reportTreeNode.dbFilteringTextFieldList.Add(new ReportConditionTextField());
                reportTreeNode.dbFilteringTextFieldList[1].ReportCondition = ReportConditionEnum.ReportConditionContain;
                reportTreeNode.dbFilteringTextFieldList[1].TextCondition = "tet";

                reportTreeNode.dbFilteringTextFieldList.Add(new ReportConditionTextField());
                reportTreeNode.dbFilteringTextFieldList[2].ReportCondition = ReportConditionEnum.ReportConditionContain;
                reportTreeNode.dbFilteringTextFieldList[2].TextCondition = "rouge";

                StringBuilder sb = new StringBuilder();
                reportBaseService.GetFieldDBFilterText(reportTreeNode, sb);
                Assert.AreEqual(" " + baseEnumService.GetEnumText_ReportConditionEnum(reportTreeNode.dbFilteringTextFieldList[0].ReportCondition) +
                    " r" + " " + baseEnumService.GetEnumText_ReportConditionEnum(reportTreeNode.dbFilteringTextFieldList[1].ReportCondition) +
                    " tet" + " " + baseEnumService.GetEnumText_ReportConditionEnum(reportTreeNode.dbFilteringTextFieldList[1].ReportCondition) +
                    " rouge", sb.ToString());
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldDBFilterText_TrueOrFalse_No_Filtering_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_Is_Active";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                StringBuilder sb = new StringBuilder();
                reportBaseService.GetFieldDBFilterText(reportTreeNode, sb);
                Assert.AreEqual("", sb.ToString());
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldDBFilterText_TrueOrFalse_Sorting_ASCENDING_1_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_Is_Active";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                reportTreeNode.dbSortingField = new ReportSortingField();

                reportTreeNode.dbSortingField.ReportSorting = ReportSortingEnum.ReportSortingAscending;
                reportTreeNode.dbSortingField.Ordinal = 1;

                StringBuilder sb = new StringBuilder();
                reportBaseService.GetFieldDBFilterText(reportTreeNode, sb);
                Assert.AreEqual(" ASCENDING 1", sb.ToString());
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldDBFilterText_TrueOrFalse_1_Filtering_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_Is_Active";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                reportTreeNode.dbFilteringTrueFalseFieldList.Add(new ReportConditionTrueFalseField());
                reportTreeNode.dbFilteringTrueFalseFieldList[0].ReportCondition = ReportConditionEnum.ReportConditionTrue;

                StringBuilder sb = new StringBuilder();
                reportBaseService.GetFieldDBFilterText(reportTreeNode, sb);
                Assert.AreEqual(" " + baseEnumService.GetEnumText_ReportConditionEnum(reportTreeNode.dbFilteringTrueFalseFieldList[0].ReportCondition), sb.ToString());
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldDBFormatText_DateAndTime_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_Last_Update_Date_And_Time_UTC";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                reportTreeNode.dbFormatingField = new ReportFormatingField();
                reportTreeNode.dbFormatingField.ReportFormatingDate = ReportFormatingDateEnum.ReportFormatingDateMonthDecimalOnly;

                StringBuilder sb = new StringBuilder();
                string retStr = reportBaseService.GetFieldDBFormatText(reportTreeNode);
                Assert.AreEqual(" FORMAT " + reportBaseService.GetDateFormatText(ReportFormatingDateEnum.ReportFormatingDateMonthDecimalOnly), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldDBFormatText_Number_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_Lat";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                reportTreeNode.dbFormatingField = new ReportFormatingField();
                reportTreeNode.dbFormatingField.ReportFormatingNumber = ReportFormatingNumberEnum.ReportFormatingNumber4Decimal;

                StringBuilder sb = new StringBuilder();
                string retStr = reportBaseService.GetFieldDBFormatText(reportTreeNode);
                Assert.AreEqual(" FORMAT " + reportBaseService.GetNumberFormatText(ReportFormatingNumberEnum.ReportFormatingNumber4Decimal), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldReportFormatText_DateAndTime_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_Last_Update_Date_And_Time_UTC";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                reportTreeNode.reportFormatingField = new ReportFormatingField();
                reportTreeNode.reportFormatingField.ReportFormatingDate = ReportFormatingDateEnum.ReportFormatingDateMonthDecimalOnly;

                StringBuilder sb = new StringBuilder();
                string retStr = reportBaseService.GetFieldReportFormatText(reportTreeNode);
                Assert.AreEqual(" FORMAT " + reportBaseService.GetDateFormatText(ReportFormatingDateEnum.ReportFormatingDateMonthDecimalOnly), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldReportFormatText_Number_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_Lat";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                reportTreeNode.reportFormatingField = new ReportFormatingField();
                reportTreeNode.reportFormatingField.ReportFormatingNumber = ReportFormatingNumberEnum.ReportFormatingNumber4Decimal;

                StringBuilder sb = new StringBuilder();
                string retStr = reportBaseService.GetFieldReportFormatText(reportTreeNode);
                Assert.AreEqual(" FORMAT " + reportBaseService.GetNumberFormatText(ReportFormatingNumberEnum.ReportFormatingNumber4Decimal), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldReportConditionText_Date_BIGGER_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_Last_Update_Date_And_Time_UTC";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                reportTreeNode.reportConditionDateFieldList.Add(new ReportConditionDateField());
                reportTreeNode.reportConditionDateFieldList[0].ReportCondition = ReportConditionEnum.ReportConditionBigger;
                reportTreeNode.reportConditionDateFieldList[0].DateTimeConditionYear = 1989;
                reportTreeNode.reportConditionDateFieldList[0].DateTimeConditionMonth = 9;
                reportTreeNode.reportConditionDateFieldList[0].DateTimeConditionDay = 10;
                reportTreeNode.reportConditionDateFieldList[0].DateTimeConditionHour = 11;
                reportTreeNode.reportConditionDateFieldList[0].DateTimeConditionMinute = 12;

                StringBuilder sb = new StringBuilder();
                string retStr = reportBaseService.GetFieldReportConditionText(reportTreeNode);
                string ShouldRet = " " + reportBaseService.GetReportConditionText(ReportConditionEnum.ReportConditionBigger);
                Assert.AreEqual(ShouldRet, retStr.Substring(0, ShouldRet.Length));
                Assert.IsTrue(retStr.Contains(" YEAR 1989"));
                Assert.IsTrue(retStr.Contains(" MONTH 9"));
                Assert.IsTrue(retStr.Contains(" DAY 10"));
                Assert.IsTrue(retStr.Contains(" HOUR 11"));
                Assert.IsTrue(retStr.Contains(" MINUTE 12"));
                //Assert.IsTrue(retStr.Contains(reportTreeNode.Text));
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldReportConditionText_Number_Whole_BIGGER_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_ID";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                reportTreeNode.reportConditionNumberFieldList.Add(new ReportConditionNumberField());
                reportTreeNode.reportConditionNumberFieldList[0].ReportCondition = ReportConditionEnum.ReportConditionBigger;
                reportTreeNode.reportConditionNumberFieldList[0].NumberCondition = 19;

                StringBuilder sb = new StringBuilder();
                string retStr = reportBaseService.GetFieldReportConditionText(reportTreeNode);
                string ShouldRet = " " + reportBaseService.GetReportConditionText(ReportConditionEnum.ReportConditionBigger) + " 19";
                Assert.AreEqual(ShouldRet, retStr.Substring(0, ShouldRet.Length));
                //Assert.IsTrue(retStr.Contains(reportTreeNode.Text));
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldReportConditionText_Number_Whole_2_Conditions_BIGGER_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_ID";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                reportTreeNode.reportConditionNumberFieldList.Add(new ReportConditionNumberField());
                reportTreeNode.reportConditionNumberFieldList[0].ReportCondition = ReportConditionEnum.ReportConditionBigger;
                reportTreeNode.reportConditionNumberFieldList[0].NumberCondition = 19;

                reportTreeNode.reportConditionNumberFieldList.Add(new ReportConditionNumberField());
                reportTreeNode.reportConditionNumberFieldList[1].ReportCondition = ReportConditionEnum.ReportConditionBigger;
                reportTreeNode.reportConditionNumberFieldList[1].NumberCondition = 29;

                StringBuilder sb = new StringBuilder();
                string retStr = reportBaseService.GetFieldReportConditionText(reportTreeNode);
                string ShouldRet = " " + reportBaseService.GetReportConditionText(ReportConditionEnum.ReportConditionBigger) + " 19" +
                    " " + reportBaseService.GetReportConditionText(ReportConditionEnum.ReportConditionBigger) + " 29";
                Assert.AreEqual(ShouldRet, retStr.Substring(0, ShouldRet.Length));
                //Assert.IsTrue(retStr.Contains(reportTreeNode.Text));
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldReportConditionText_Number_Decimal_BIGGER_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_Lat";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                reportTreeNode.reportConditionNumberFieldList.Add(new ReportConditionNumberField());
                reportTreeNode.reportConditionNumberFieldList[0].ReportCondition = ReportConditionEnum.ReportConditionBigger;
                reportTreeNode.reportConditionNumberFieldList[0].NumberCondition = (culture.TwoLetterISOLanguageName == "fr" ? float.Parse("23,3") : 23.3f);

                StringBuilder sb = new StringBuilder();
                string retStr = reportBaseService.GetFieldReportConditionText(reportTreeNode);
                string ShouldRet = " " + reportBaseService.GetReportConditionText(ReportConditionEnum.ReportConditionBigger) + " 23.3";
                Assert.AreEqual(ShouldRet, retStr.Substring(0, ShouldRet.Length));
                //Assert.IsTrue(retStr.Contains(reportTreeNode.Text));
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldReportConditionText_Number_Decimal_2_Conditions_BIGGER_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_Lat";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                reportTreeNode.reportConditionNumberFieldList.Add(new ReportConditionNumberField());
                reportTreeNode.reportConditionNumberFieldList[0].ReportCondition = ReportConditionEnum.ReportConditionBigger;
                reportTreeNode.reportConditionNumberFieldList[0].NumberCondition = (culture.TwoLetterISOLanguageName == "fr" ? float.Parse("23,3") : 23.3f);

                reportTreeNode.reportConditionNumberFieldList.Add(new ReportConditionNumberField());
                reportTreeNode.reportConditionNumberFieldList[1].ReportCondition = ReportConditionEnum.ReportConditionBigger;
                reportTreeNode.reportConditionNumberFieldList[1].NumberCondition = (culture.TwoLetterISOLanguageName == "fr" ? float.Parse("53,3") : 53.3f);

                StringBuilder sb = new StringBuilder();
                string retStr = reportBaseService.GetFieldReportConditionText(reportTreeNode);
                string ShouldRet = " " +
                    reportBaseService.GetReportConditionText(ReportConditionEnum.ReportConditionBigger) + " 23.3" + " " +
                    reportBaseService.GetReportConditionText(ReportConditionEnum.ReportConditionBigger) + " 53.3";
                Assert.AreEqual(ShouldRet, retStr.Substring(0, ShouldRet.Length));
                //Assert.IsTrue(retStr.Contains(reportTreeNode.Text));
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldReportConditionText_Text_1_Condition_CONTAIN_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_Name";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                reportTreeNode.reportConditionTextFieldList.Add(new ReportConditionTextField());
                reportTreeNode.reportConditionTextFieldList[0].ReportCondition = ReportConditionEnum.ReportConditionContain;
                reportTreeNode.reportConditionTextFieldList[0].TextCondition = "allo";

                StringBuilder sb = new StringBuilder();
                string retStr = reportBaseService.GetFieldReportConditionText(reportTreeNode);
                string ShouldRet = " " +
                    reportBaseService.GetReportConditionText(ReportConditionEnum.ReportConditionContain) + " allo";
                Assert.AreEqual(ShouldRet, retStr.Substring(0, ShouldRet.Length));
                //Assert.IsTrue(retStr.Contains(reportTreeNode.Text));
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldReportConditionText_Text_2_Condition_CONTAIN_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_Name";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                reportTreeNode.reportConditionTextFieldList.Add(new ReportConditionTextField());
                reportTreeNode.reportConditionTextFieldList[0].ReportCondition = ReportConditionEnum.ReportConditionContain;
                reportTreeNode.reportConditionTextFieldList[0].TextCondition = "allo";

                reportTreeNode.reportConditionTextFieldList.Add(new ReportConditionTextField());
                reportTreeNode.reportConditionTextFieldList[1].ReportCondition = ReportConditionEnum.ReportConditionContain;
                reportTreeNode.reportConditionTextFieldList[1].TextCondition = "Rouge";

                StringBuilder sb = new StringBuilder();
                string retStr = reportBaseService.GetFieldReportConditionText(reportTreeNode);
                string ShouldRet = " " +
                    reportBaseService.GetReportConditionText(ReportConditionEnum.ReportConditionContain) + " allo" + " " +
                    reportBaseService.GetReportConditionText(ReportConditionEnum.ReportConditionContain) + " Rouge";
                Assert.AreEqual(ShouldRet, retStr.Substring(0, ShouldRet.Length));
                //Assert.IsTrue(retStr.Contains(reportTreeNode.Text));
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldReportConditionText_Text_3_Condition_CONTAIN_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_Name";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                reportTreeNode.reportConditionTextFieldList.Add(new ReportConditionTextField());
                reportTreeNode.reportConditionTextFieldList[0].ReportCondition = ReportConditionEnum.ReportConditionContain;
                reportTreeNode.reportConditionTextFieldList[0].TextCondition = "allo";

                reportTreeNode.reportConditionTextFieldList.Add(new ReportConditionTextField());
                reportTreeNode.reportConditionTextFieldList[1].ReportCondition = ReportConditionEnum.ReportConditionContain;
                reportTreeNode.reportConditionTextFieldList[1].TextCondition = "test";

                reportTreeNode.reportConditionTextFieldList.Add(new ReportConditionTextField());
                reportTreeNode.reportConditionTextFieldList[2].ReportCondition = ReportConditionEnum.ReportConditionContain;
                reportTreeNode.reportConditionTextFieldList[2].TextCondition = "rouge";

                StringBuilder sb = new StringBuilder();
                string retStr = reportBaseService.GetFieldReportConditionText(reportTreeNode);
                string ShouldRet = " " +
                    reportBaseService.GetReportConditionText(ReportConditionEnum.ReportConditionContain) + " allo" + " " +
                    reportBaseService.GetReportConditionText(ReportConditionEnum.ReportConditionContain) + " test" + " " +
                    reportBaseService.GetReportConditionText(ReportConditionEnum.ReportConditionContain) + " rouge";
                Assert.AreEqual(ShouldRet, retStr.Substring(0, ShouldRet.Length));
                //Assert.IsTrue(retStr.Contains(reportTreeNode.Text));
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldReportConditionText_TrueOrFalse_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string Text = "Root_Is_Active";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], Text);
                Assert.IsNotNull(reportTreeNode);
                Assert.AreEqual(Text, reportTreeNode.Text);

                reportTreeNode.reportConditionTrueFalseFieldList.Add(new ReportConditionTrueFalseField());
                reportTreeNode.reportConditionTrueFalseFieldList[0].ReportCondition = ReportConditionEnum.ReportConditionTrue;

                StringBuilder sb = new StringBuilder();
                string retStr = reportBaseService.GetFieldReportConditionText(reportTreeNode);
                string ShouldRet = " " +
                    reportBaseService.GetReportConditionText(ReportConditionEnum.ReportConditionTrue);
                Assert.AreEqual(ShouldRet, retStr.Substring(0, ShouldRet.Length));
                //Assert.IsTrue(retStr.Contains(reportTreeNode.Text));
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldType_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                Type type = reportBaseService.GetFieldType("Root_Is_Active", typeof(ReportRootModel));
                Assert.AreEqual("System.Nullable`1[System.Boolean]", type.ToString());

                type = reportBaseService.GetFieldType("Root_ID", typeof(ReportRootModel));
                Assert.AreEqual("System.Nullable`1[System.Int32]", type.ToString());

                type = reportBaseService.GetFieldType("Root_Name", typeof(ReportRootModel));
                Assert.AreEqual("System.String", type.ToString());

                type = reportBaseService.GetFieldType("Root_Last_Update_Date_And_Time_UTC", typeof(ReportRootModel));
                Assert.AreEqual("System.Nullable`1[System.DateTime]", type.ToString());

                type = reportBaseService.GetFieldType("Root_Lat", typeof(ReportRootModel));
                Assert.AreEqual("System.Nullable`1[System.Single]", type.ToString());

                type = reportBaseService.GetFieldType("Root_File_File_Purpose", typeof(ReportRoot_FileModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.FilePurposeEnum]", type.ToString());

                type = reportBaseService.GetFieldType("Root_File_File_Type", typeof(ReportRoot_FileModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.FileTypeEnum]", type.ToString());

                type = reportBaseService.GetFieldType("Root_Name_Translation_Status", typeof(ReportRootModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.TranslationStatusEnum]", type.ToString());

                type = reportBaseService.GetFieldType("Box_Model_Result_Result_Type", typeof(ReportBox_Model_ResultModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.BoxModelResultTypeEnum]", type.ToString());

                type = reportBaseService.GetFieldType("Infrastructure_Infrastructure_Type", typeof(ReportInfrastructureModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.InfrastructureTypeEnum]", type.ToString());

                type = reportBaseService.GetFieldType("Infrastructure_Facility_Type", typeof(ReportInfrastructureModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.FacilityTypeEnum]", type.ToString());

                type = reportBaseService.GetFieldType("Infrastructure_Aeration_Type", typeof(ReportInfrastructureModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.AerationTypeEnum]", type.ToString());

                type = reportBaseService.GetFieldType("Infrastructure_Preliminary_Treatment_Type", typeof(ReportInfrastructureModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.PreliminaryTreatmentTypeEnum]", type.ToString());

                type = reportBaseService.GetFieldType("Infrastructure_Primary_Treatment_Type", typeof(ReportInfrastructureModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.PrimaryTreatmentTypeEnum]", type.ToString());

                type = reportBaseService.GetFieldType("Infrastructure_Secondary_Treatment_Type", typeof(ReportInfrastructureModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.SecondaryTreatmentTypeEnum]", type.ToString());

                type = reportBaseService.GetFieldType("Infrastructure_Tertiary_Treatment_Type", typeof(ReportInfrastructureModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.TertiaryTreatmentTypeEnum]", type.ToString());

                type = reportBaseService.GetFieldType("Infrastructure_Treatment_Type", typeof(ReportInfrastructureModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.TreatmentTypeEnum]", type.ToString());

                type = reportBaseService.GetFieldType("Infrastructure_Disinfection_Type", typeof(ReportInfrastructureModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.DisinfectionTypeEnum]", type.ToString());

                type = reportBaseService.GetFieldType("Infrastructure_Collection_System_Type", typeof(ReportInfrastructureModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.CollectionSystemTypeEnum]", type.ToString());

                type = reportBaseService.GetFieldType("Infrastructure_Alarm_System_Type", typeof(ReportInfrastructureModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.AlarmSystemTypeEnum]", type.ToString());

                type = reportBaseService.GetFieldType("Visual_Plumes_Scenario_Status", typeof(ReportVisual_Plumes_ScenarioModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.ScenarioStatusEnum]", type.ToString());

                type = reportBaseService.GetFieldType("Climate_Site_Data_Storage_Data_Type", typeof(ReportClimate_Site_DataModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.StorageDataTypeEnum]", type.ToString());

                type = reportBaseService.GetFieldType("Root_File_Language", typeof(ReportRoot_FileModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.LanguageEnum]", type.ToString());

                type = reportBaseService.GetFieldType("MWQM_Site_Sample_Types", typeof(ReportMWQM_Site_SampleModel));
                Assert.AreEqual("System.String", type.ToString());

                type = reportBaseService.GetFieldType("MWQM_Run_Sea_State_At_Start_Beaufort_Scale", typeof(ReportMWQM_RunModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.BeaufortScaleEnum]", type.ToString());

                type = reportBaseService.GetFieldType("MWQM_Run_Analyze_Method", typeof(ReportMWQM_RunModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.AnalyzeMethodEnum]", type.ToString());

                type = reportBaseService.GetFieldType("MWQM_Run_Sample_Matrix", typeof(ReportMWQM_RunModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.SampleMatrixEnum]", type.ToString());

                type = reportBaseService.GetFieldType("MWQM_Run_Laboratory", typeof(ReportMWQM_RunModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.LaboratoryEnum]", type.ToString());

                type = reportBaseService.GetFieldType("MWQM_Run_Sample_Status", typeof(ReportMWQM_RunModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.SampleStatusEnum]", type.ToString());

                type = reportBaseService.GetFieldType("MWQM_Run_Lab_Sheet_Config_Type", typeof(ReportMWQM_Run_Lab_SheetModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.ConfigTypeEnum]", type.ToString());

                type = reportBaseService.GetFieldType("MWQM_Run_Lab_Sheet_Type", typeof(ReportMWQM_Run_Lab_SheetModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.LabSheetTypeEnum]", type.ToString());

                type = reportBaseService.GetFieldType("MWQM_Run_Lab_Sheet_Status", typeof(ReportMWQM_Run_Lab_SheetModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.LabSheetStatusEnum]", type.ToString());

                type = reportBaseService.GetFieldType("Pol_Source_Site_Inactive_Reason", typeof(ReportPol_Source_SiteModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.PolSourceInactiveReasonEnum]", type.ToString());

                type = reportBaseService.GetFieldType("Pol_Source_Site_Obs_Issue_Observation_Sentence", typeof(ReportPol_Source_Site_Obs_IssueModel));
                Assert.AreEqual("System.String", type.ToString());

                type = reportBaseService.GetFieldType("Pol_Source_Site_Address_Type", typeof(ReportPol_Source_Site_AddressModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.AddressTypeEnum]", type.ToString());

                type = reportBaseService.GetFieldType("Pol_Source_Site_Address_Street_Type", typeof(ReportPol_Source_Site_AddressModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.StreetTypeEnum]", type.ToString());

                type = reportBaseService.GetFieldType("Municipality_Contact_Title", typeof(ReportMunicipality_ContactModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.ContactTitleEnum]", type.ToString());

                type = reportBaseService.GetFieldType("Municipality_Contact_Email_Type", typeof(ReportMunicipality_Contact_EmailModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.EmailTypeEnum]", type.ToString());

                type = reportBaseService.GetFieldType("Municipality_Contact_Tel_Type", typeof(ReportMunicipality_Contact_TelModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.TelTypeEnum]", type.ToString());

                type = reportBaseService.GetFieldType("Subsector_Tide_Site_Data_Tide_Start", typeof(ReportSubsector_Tide_Site_DataModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.TideTextEnum]", type.ToString());

                type = reportBaseService.GetFieldType("Subsector_Tide_Site_Data_Tide_Data_Type", typeof(ReportSubsector_Tide_Site_DataModel));
                Assert.AreEqual("System.Nullable`1[CSSPEnumsDLL.Enums.TideDataTypeEnum]", type.ToString());

                type = reportBaseService.GetFieldType("ErrRoot_Lat", typeof(ReportRootModel));
                Assert.IsNull(type);

            }
        }
        [TestMethod]
        public void ReportBaseService_GetFieldTypeStr_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string retStr = reportBaseService.GetFieldTypeStr("Root_Is_Active", typeof(ReportRootModel));
                Assert.AreEqual("System.Boolean", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Root_ID", typeof(ReportRootModel));
                Assert.AreEqual("System.Int32", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Root_Name", typeof(ReportRootModel));
                Assert.AreEqual("System.String", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Root_Last_Update_Date_And_Time_UTC", typeof(ReportRootModel));
                Assert.AreEqual("System.DateTime", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Root_Lat", typeof(ReportRootModel));
                Assert.AreEqual("System.Single", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Root_File_File_Purpose", typeof(ReportRoot_FileModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.FilePurposeEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Root_File_File_Type", typeof(ReportRoot_FileModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.FileTypeEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Root_Name_Translation_Status", typeof(ReportRootModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.TranslationStatusEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Box_Model_Result_Result_Type", typeof(ReportBox_Model_ResultModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.BoxModelResultTypeEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Infrastructure_Infrastructure_Type", typeof(ReportInfrastructureModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.InfrastructureTypeEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Infrastructure_Facility_Type", typeof(ReportInfrastructureModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.FacilityTypeEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Infrastructure_Aeration_Type", typeof(ReportInfrastructureModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.AerationTypeEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Infrastructure_Preliminary_Treatment_Type", typeof(ReportInfrastructureModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.PreliminaryTreatmentTypeEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Infrastructure_Primary_Treatment_Type", typeof(ReportInfrastructureModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.PrimaryTreatmentTypeEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Infrastructure_Secondary_Treatment_Type", typeof(ReportInfrastructureModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.SecondaryTreatmentTypeEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Infrastructure_Tertiary_Treatment_Type", typeof(ReportInfrastructureModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.TertiaryTreatmentTypeEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Infrastructure_Treatment_Type", typeof(ReportInfrastructureModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.TreatmentTypeEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Infrastructure_Disinfection_Type", typeof(ReportInfrastructureModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.DisinfectionTypeEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Infrastructure_Collection_System_Type", typeof(ReportInfrastructureModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.CollectionSystemTypeEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Infrastructure_Alarm_System_Type", typeof(ReportInfrastructureModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.AlarmSystemTypeEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Visual_Plumes_Scenario_Status", typeof(ReportVisual_Plumes_ScenarioModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.ScenarioStatusEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Climate_Site_Data_Storage_Data_Type", typeof(ReportClimate_Site_DataModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.StorageDataTypeEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Root_File_Language", typeof(ReportRoot_FileModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.LanguageEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("MWQM_Site_Sample_Types", typeof(ReportMWQM_Site_SampleModel));
                Assert.AreEqual("System.String", retStr);

                retStr = reportBaseService.GetFieldTypeStr("MWQM_Run_Sea_State_At_Start_Beaufort_Scale", typeof(ReportMWQM_RunModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.BeaufortScaleEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("MWQM_Run_Analyze_Method", typeof(ReportMWQM_RunModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.AnalyzeMethodEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("MWQM_Run_Sample_Matrix", typeof(ReportMWQM_RunModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.SampleMatrixEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("MWQM_Run_Laboratory", typeof(ReportMWQM_RunModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.LaboratoryEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("MWQM_Run_Sample_Status", typeof(ReportMWQM_RunModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.SampleStatusEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("MWQM_Run_Lab_Sheet_Config_Type", typeof(ReportMWQM_Run_Lab_SheetModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.ConfigTypeEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("MWQM_Run_Lab_Sheet_Type", typeof(ReportMWQM_Run_Lab_SheetModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.LabSheetTypeEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("MWQM_Run_Lab_Sheet_Status", typeof(ReportMWQM_Run_Lab_SheetModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.LabSheetStatusEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Pol_Source_Site_Inactive_Reason", typeof(ReportPol_Source_SiteModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.PolSourceInactiveReasonEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Pol_Source_Site_Obs_Issue_Observation_Sentence", typeof(ReportPol_Source_Site_Obs_IssueModel));
                Assert.AreEqual("System.String", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Pol_Source_Site_Address_Type", typeof(ReportPol_Source_Site_AddressModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.AddressTypeEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Pol_Source_Site_Address_Street_Type", typeof(ReportPol_Source_Site_AddressModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.StreetTypeEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Municipality_Contact_Title", typeof(ReportMunicipality_ContactModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.ContactTitleEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Municipality_Contact_Email_Type", typeof(ReportMunicipality_Contact_EmailModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.EmailTypeEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Municipality_Contact_Tel_Type", typeof(ReportMunicipality_Contact_TelModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.TelTypeEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Subsector_Tide_Site_Data_Tide_Start", typeof(ReportSubsector_Tide_Site_DataModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.TideTextEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("Subsector_Tide_Site_Data_Tide_Data_Type", typeof(ReportSubsector_Tide_Site_DataModel));
                Assert.AreEqual("CSSPEnumsDLL.Enums.TideDataTypeEnum", retStr);

                retStr = reportBaseService.GetFieldTypeStr("ErrRoot_Lat", typeof(ReportRootModel));
                Assert.AreEqual(string.Format(ReportServiceRes._DoesNotExistFor_, "ErrRoot_Lat", typeof(ReportRootModel).ToString()), retStr);

            }
        }
        [TestMethod]
        public void ReportBaseService_GetFormatDate_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                for (int i = 0, count = Enum.GetNames(typeof(ReportFormatingDateEnum)).Count(); i < count; i++)
                {
                    string retStr = reportBaseService.GetFormatDate((ReportFormatingDateEnum)i);
                    switch ((ReportFormatingDateEnum)i)
                    {
                        case ReportFormatingDateEnum.ReportFormatingDateYearOnly:
                            Assert.AreEqual(DateTime.Now.ToString("yyyy"), retStr);
                            break;
                        case ReportFormatingDateEnum.ReportFormatingDateMonthDecimalOnly:
                            Assert.AreEqual(DateTime.Now.ToString("MM"), retStr);
                            break;
                        case ReportFormatingDateEnum.ReportFormatingDateMonthShortTextOnly:
                            Assert.AreEqual(DateTime.Now.ToString("MMM"), retStr);
                            break;
                        case ReportFormatingDateEnum.ReportFormatingDateMonthFullTextOnly:
                            Assert.AreEqual(DateTime.Now.ToString("MMMM"), retStr);
                            break;
                        case ReportFormatingDateEnum.ReportFormatingDateDayOnly:
                            Assert.AreEqual(DateTime.Now.ToString("dd"), retStr);
                            break;
                        case ReportFormatingDateEnum.ReportFormatingDateHourOnly:
                            Assert.AreEqual(DateTime.Now.ToString("HH"), retStr);
                            break;
                        case ReportFormatingDateEnum.ReportFormatingDateMinuteOnly:
                            Assert.AreEqual(DateTime.Now.ToString("mm"), retStr);
                            break;
                        case ReportFormatingDateEnum.ReportFormatingDateYearMonthDecimalDay:
                            Assert.AreEqual(DateTime.Now.ToString("yyyy MM dd"), retStr);
                            break;
                        case ReportFormatingDateEnum.ReportFormatingDateYearMonthShortTextDay:
                            Assert.AreEqual(DateTime.Now.ToString("yyyy MMM dd"), retStr);
                            break;
                        case ReportFormatingDateEnum.ReportFormatingDateYearMonthFullTextDay:
                            Assert.AreEqual(DateTime.Now.ToString("yyyy MMMM dd"), retStr);
                            break;
                        case ReportFormatingDateEnum.ReportFormatingDateYearMonthDecimalDayHourMinute:
                            Assert.AreEqual(DateTime.Now.ToString("yyyy MM dd HH:mm"), retStr);
                            break;
                        case ReportFormatingDateEnum.ReportFormatingDateYearMonthShortTextDayHourMinute:
                            Assert.AreEqual(DateTime.Now.ToString("yyyy MMM dd HH:mm"), retStr);
                            break;
                        case ReportFormatingDateEnum.ReportFormatingDateYearMonthFullTextDayHourMinute:
                            Assert.AreEqual(DateTime.Now.ToString("yyyy MMMM dd HH:mm"), retStr);
                            break;
                        default:
                            Assert.AreEqual("Error", ((ReportFormatingDateEnum)i).ToString());
                            break;
                    }
                }
            }
        }
        [TestMethod]
        public void ReportBaseService_GetFormatNumber_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                double TheNumber = 123456.123456D;

                for (int i = 0, count = Enum.GetNames(typeof(ReportFormatingNumberEnum)).Count(); i < count; i++)
                {
                    string retStr = reportBaseService.GetFormatNumber((ReportFormatingNumberEnum)i);
                    switch ((ReportFormatingNumberEnum)i)
                    {
                        case ReportFormatingNumberEnum.ReportFormatingNumber0Decimal:
                            Assert.AreEqual(TheNumber.ToString("F0"), retStr);
                            break;
                        case ReportFormatingNumberEnum.ReportFormatingNumber1Decimal:
                            Assert.AreEqual(TheNumber.ToString("F1"), retStr);
                            break;
                        case ReportFormatingNumberEnum.ReportFormatingNumber2Decimal:
                            Assert.AreEqual(TheNumber.ToString("F2"), retStr);
                            break;
                        case ReportFormatingNumberEnum.ReportFormatingNumber3Decimal:
                            Assert.AreEqual(TheNumber.ToString("F3"), retStr);
                            break;
                        case ReportFormatingNumberEnum.ReportFormatingNumber4Decimal:
                            Assert.AreEqual(TheNumber.ToString("F4"), retStr);
                            break;
                        case ReportFormatingNumberEnum.ReportFormatingNumber5Decimal:
                            Assert.AreEqual(TheNumber.ToString("F5"), retStr);
                            break;
                        case ReportFormatingNumberEnum.ReportFormatingNumber6Decimal:
                            Assert.AreEqual(TheNumber.ToString("F6"), retStr);
                            break;
                        case ReportFormatingNumberEnum.ReportFormatingNumberScientific0Decimal:
                            Assert.AreEqual(TheNumber.ToString("e0"), retStr);
                            break;
                        case ReportFormatingNumberEnum.ReportFormatingNumberScientific1Decimal:
                            Assert.AreEqual(TheNumber.ToString("e1"), retStr);
                            break;
                        case ReportFormatingNumberEnum.ReportFormatingNumberScientific2Decimal:
                            Assert.AreEqual(TheNumber.ToString("e2"), retStr);
                            break;
                        case ReportFormatingNumberEnum.ReportFormatingNumberScientific3Decimal:
                            Assert.AreEqual(TheNumber.ToString("e3"), retStr);
                            break;
                        case ReportFormatingNumberEnum.ReportFormatingNumberScientific4Decimal:
                            Assert.AreEqual(TheNumber.ToString("e4"), retStr);
                            break;
                        case ReportFormatingNumberEnum.ReportFormatingNumberScientific5Decimal:
                            Assert.AreEqual(TheNumber.ToString("e5"), retStr);
                            break;
                        case ReportFormatingNumberEnum.ReportFormatingNumberScientific6Decimal:
                            Assert.AreEqual(TheNumber.ToString("e6"), retStr);
                            break;
                        default:
                            Assert.AreEqual("Error", ((ReportFormatingDateEnum)i).ToString());
                            break;
                    }
                }
            }
        }
        [TestMethod]
        public void ReportBaseService_GetNumberFormatText_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                for (int i = 0, count = Enum.GetNames(typeof(ReportFormatingNumberEnum)).Count(); i < count; i++)
                {
                    string retStr = reportBaseService.GetNumberFormatText((ReportFormatingNumberEnum)i);
                    switch ((ReportFormatingNumberEnum)i)
                    {
                        case ReportFormatingNumberEnum.ReportFormatingNumber0Decimal:
                            Assert.AreEqual("F0", retStr);
                            break;
                        case ReportFormatingNumberEnum.ReportFormatingNumber1Decimal:
                            Assert.AreEqual("F1", retStr);
                            break;
                        case ReportFormatingNumberEnum.ReportFormatingNumber2Decimal:
                            Assert.AreEqual("F2", retStr);
                            break;
                        case ReportFormatingNumberEnum.ReportFormatingNumber3Decimal:
                            Assert.AreEqual("F3", retStr);
                            break;
                        case ReportFormatingNumberEnum.ReportFormatingNumber4Decimal:
                            Assert.AreEqual("F4", retStr);
                            break;
                        case ReportFormatingNumberEnum.ReportFormatingNumber5Decimal:
                            Assert.AreEqual("F5", retStr);
                            break;
                        case ReportFormatingNumberEnum.ReportFormatingNumber6Decimal:
                            Assert.AreEqual("F6", retStr);
                            break;
                        case ReportFormatingNumberEnum.ReportFormatingNumberScientific0Decimal:
                            Assert.AreEqual("e0", retStr);
                            break;
                        case ReportFormatingNumberEnum.ReportFormatingNumberScientific1Decimal:
                            Assert.AreEqual("e1", retStr);
                            break;
                        case ReportFormatingNumberEnum.ReportFormatingNumberScientific2Decimal:
                            Assert.AreEqual("e2", retStr);
                            break;
                        case ReportFormatingNumberEnum.ReportFormatingNumberScientific3Decimal:
                            Assert.AreEqual("e3", retStr);
                            break;
                        case ReportFormatingNumberEnum.ReportFormatingNumberScientific4Decimal:
                            Assert.AreEqual("e4", retStr);
                            break;
                        case ReportFormatingNumberEnum.ReportFormatingNumberScientific5Decimal:
                            Assert.AreEqual("e5", retStr);
                            break;
                        case ReportFormatingNumberEnum.ReportFormatingNumberScientific6Decimal:
                            Assert.AreEqual("e6", retStr);
                            break;
                        default:
                            Assert.AreEqual("Error", ((ReportFormatingDateEnum)i).ToString());
                            break;
                    }
                }
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportConditionText_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                for (int i = 0, count = Enum.GetNames(typeof(ReportConditionEnum)).Count(); i < count; i++)
                {
                    string retStr = reportBaseService.GetReportConditionText((ReportConditionEnum)i);
                    switch ((ReportConditionEnum)i)
                    {
                        case ReportConditionEnum.Error:
                            Assert.AreEqual("", retStr);
                            break;
                        case ReportConditionEnum.ReportConditionTrue:
                            Assert.AreEqual("TRUE", retStr);
                            break;
                        case ReportConditionEnum.ReportConditionFalse:
                            Assert.AreEqual("FALSE", retStr);
                            break;
                        case ReportConditionEnum.ReportConditionContain:
                            Assert.AreEqual("CONTAIN", retStr);
                            break;
                        case ReportConditionEnum.ReportConditionStart:
                            Assert.AreEqual("START", retStr);
                            break;
                        case ReportConditionEnum.ReportConditionEnd:
                            Assert.AreEqual("END", retStr);
                            break;
                        case ReportConditionEnum.ReportConditionBigger:
                            Assert.AreEqual("BIGGER", retStr);
                            break;
                        case ReportConditionEnum.ReportConditionSmaller:
                            Assert.AreEqual("SMALLER", retStr);
                            break;
                        case ReportConditionEnum.ReportConditionEqual:
                            Assert.AreEqual("EQUAL", retStr);
                            break;
                        default:
                            Assert.AreEqual("", retStr);
                            break;
                    }
                }
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportSortingText_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                for (int i = 0, count = Enum.GetNames(typeof(ReportSortingEnum)).Count(); i < count; i++)
                {
                    string retStr = reportBaseService.GetReportSortingText((ReportSortingEnum)i);
                    switch ((ReportSortingEnum)i)
                    {
                        case ReportSortingEnum.Error:
                            Assert.AreEqual("", retStr);
                            break;
                        case ReportSortingEnum.ReportSortingAscending:
                            Assert.AreEqual("ASCENDING", retStr);
                            break;
                        case ReportSortingEnum.ReportSortingDescending:
                            Assert.AreEqual("DESCENDING", retStr);
                            break;
                        default:
                            Assert.AreEqual("", retStr);
                            break;
                    }
                }
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesFromTagText_Good_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("|||Start Root en");
                sb.AppendLine("Root_Name");
                sb.AppendLine("Root_Lat");
                sb.AppendLine("|||");

                string TagItem = "Root";
                Type TagType = typeof(ReportRootModel);

                List<ReportTreeNode> reportTreeNodeList = new List<ReportTreeNode>();

                bool IsDBFiltering = true;
                string retStr = reportBaseService.GetReportTreeNodesFromTagText(sb.ToString(), TagItem, TagType, reportTreeNodeList, IsDBFiltering);
                Assert.AreEqual("", retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesFromTagText_Error_Start_With_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("|||Start Root en");
                sb.AppendLine("NotRoot_Name");
                sb.AppendLine("Root_Lat");
                sb.AppendLine("|||");

                string TagItem = "Root";
                Type TagType = typeof(ReportRootModel);
                int LineCount = 2;

                List<ReportTreeNode> reportTreeNodeList = new List<ReportTreeNode>();

                bool IsDBFiltering = true;
                string retStr = reportBaseService.GetReportTreeNodesFromTagText(sb.ToString(), TagItem, TagType, reportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_ShouldStartWith_, LineCount, TagItem), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesFromTagText_Error_Tag_Does_Not_Exist_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("|||Start Root en");
                sb.AppendLine("Root_NameNot");
                sb.AppendLine("Root_Lat");
                sb.AppendLine("|||");

                string TagItem = "Root";
                Type TagType = typeof(ReportRootModel);

                List<ReportTreeNode> reportTreeNodeList = new List<ReportTreeNode>();

                bool IsDBFiltering = true;
                string retStr = reportBaseService.GetReportTreeNodesFromTagText(sb.ToString(), TagItem, TagType, reportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes._DoesNotExistFor_, "Root_NameNot", TagType.ToString()), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesFromTagText_Error_Bool_ShouldBeOneOf_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("|||Start Root en");
                sb.AppendLine("Root_Is_Active ASCENDINGNot");
                sb.AppendLine("|||");

                string TagItem = "Root";
                Type TagType = typeof(ReportRootModel);
                int LineCount = 2;

                List<ReportTreeNode> reportTreeNodeList = new List<ReportTreeNode>();

                bool IsDBFiltering = true;
                string retStr = reportBaseService.GetReportTreeNodesFromTagText(sb.ToString(), TagItem, TagType, reportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, 2, String.Join(",", AllowableSortingFilters.Concat(AllowableTrueFalseFilters))), retStr);

                sb = new StringBuilder();
                sb.AppendLine("|||Start Root en");
                sb.AppendLine("Root_Is_Active TRUENot");
                sb.AppendLine("|||");

                TagItem = "Root";
                TagType = typeof(ReportRootModel);
                LineCount = 2;

                reportTreeNodeList = new List<ReportTreeNode>();

                retStr = reportBaseService.GetReportTreeNodesFromTagText(sb.ToString(), TagItem, TagType, reportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, 2, String.Join(",", AllowableSortingFilters.Concat(AllowableTrueFalseFilters))), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesFromTagText_Error_Bool_Sorting_Ordering_Missing_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("|||Start Root en");
                sb.AppendLine("Root_Is_Active ASCENDING");
                sb.AppendLine("|||");

                string TagItem = "Root";
                Type TagType = typeof(ReportRootModel);
                int LineCount = 2;
                int StartItem = 1;

                List<ReportTreeNode> reportTreeNodeList = new List<ReportTreeNode>();

                bool IsDBFiltering = true;
                string retStr = reportBaseService.GetReportTreeNodesFromTagText(sb.ToString(), TagItem, TagType, reportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_ShouldHave_Items, LineCount, "more than " + (StartItem + 1)), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesFromTagText_Error_Bool_Not_True_Or_False_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("|||Start Root en");
                sb.AppendLine("Root_Is_Active ASCENDING 1 TRUENot");
                sb.AppendLine("|||");

                string TagItem = "Root";
                Type TagType = typeof(ReportRootModel);
                int LineCount = 2;
                int StartItem = 3;

                List<ReportTreeNode> reportTreeNodeList = new List<ReportTreeNode>();

                bool IsDBFiltering = true;
                string retStr = reportBaseService.GetReportTreeNodesFromTagText(sb.ToString(), TagItem, TagType, reportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, StartItem + 1, "TRUE,FALSE"), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesFromTagText_Error_Single_Or_Int32_ShouldBeOneOf_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("|||Start Root en");
                sb.AppendLine("Root_Lat ASCENDINGNot");
                sb.AppendLine("|||");

                string TagItem = "Root";
                Type TagType = typeof(ReportRootModel);
                int LineCount = 2;

                List<ReportTreeNode> reportTreeNodeList = new List<ReportTreeNode>();

                bool IsDBFiltering = true;
                string retStr = reportBaseService.GetReportTreeNodesFromTagText(sb.ToString(), TagItem, TagType, reportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, 2, String.Join(",", AllowableSortingFilters.Concat(AllowableFormatingFilters.Concat(AllowableBasicFilters)))), retStr);

                sb = new StringBuilder();
                sb.AppendLine("|||Start Root en");
                sb.AppendLine("Root_Lat BIGGERNot");
                sb.AppendLine("|||");

                TagItem = "Root";
                TagType = typeof(ReportRootModel);
                LineCount = 2;

                reportTreeNodeList = new List<ReportTreeNode>();

                retStr = reportBaseService.GetReportTreeNodesFromTagText(sb.ToString(), TagItem, TagType, reportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, 2, String.Join(",", AllowableSortingFilters.Concat(AllowableFormatingFilters.Concat(AllowableBasicFilters)))), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesFromTagText_Error_Single_Or_Int32_Sorting_Ordering_Missing_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("|||Start Root en");
                sb.AppendLine("Root_Lat ASCENDING");
                sb.AppendLine("|||");

                string TagItem = "Root";
                Type TagType = typeof(ReportRootModel);
                int LineCount = 2;
                int StartItem = 1;

                List<ReportTreeNode> reportTreeNodeList = new List<ReportTreeNode>();

                bool IsDBFiltering = true;
                string retStr = reportBaseService.GetReportTreeNodesFromTagText(sb.ToString(), TagItem, TagType, reportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_ShouldHave_Items, LineCount, "more than " + (StartItem + 1)), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesFromTagText_Error_Single_Or_Int32_ShouldBeANumber_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("|||Start Root en");
                sb.AppendLine("Root_Lat ASCENDING 1 BIGGER a");
                sb.AppendLine("|||");

                string TagItem = "Root";
                Type TagType = typeof(ReportRootModel);

                List<ReportTreeNode> reportTreeNodeList = new List<ReportTreeNode>();

                bool IsDBFiltering = true;
                string retStr = reportBaseService.GetReportTreeNodesFromTagText(sb.ToString(), TagItem, TagType, reportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes.ConditionTextMissingNumberAfter_, "BIGGER"), retStr);

                sb = new StringBuilder();
                sb.AppendLine("|||Start Root en");
                sb.AppendLine("Root_Lat BIGGER a");
                sb.AppendLine("|||");

                TagItem = "Root";
                TagType = typeof(ReportRootModel);

                reportTreeNodeList = new List<ReportTreeNode>();

                retStr = reportBaseService.GetReportTreeNodesFromTagText(sb.ToString(), TagItem, TagType, reportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes.ConditionTextMissingNumberAfter_, "BIGGER"), retStr);

            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesFromTagText_Error_String_ShouldBeOneOf_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("|||Start Root en");
                sb.AppendLine("Root_Name ASCENDINGNot");
                sb.AppendLine("|||");

                string TagItem = "Root";
                Type TagType = typeof(ReportRootModel);
                int LineCount = 2;

                List<ReportTreeNode> reportTreeNodeList = new List<ReportTreeNode>();

                bool IsDBFiltering = true;
                string retStr = reportBaseService.GetReportTreeNodesFromTagText(sb.ToString(), TagItem, TagType, reportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, 2, String.Join(",", AllowableSortingFilters.Concat(AllowableTextFilters))), retStr);

                sb = new StringBuilder();
                sb.AppendLine("|||Start Root en");
                sb.AppendLine("Root_Name EQUALNot");
                sb.AppendLine("|||");

                TagItem = "Root";
                TagType = typeof(ReportRootModel);
                LineCount = 2;

                reportTreeNodeList = new List<ReportTreeNode>();

                retStr = reportBaseService.GetReportTreeNodesFromTagText(sb.ToString(), TagItem, TagType, reportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, 2, String.Join(",", AllowableSortingFilters.Concat(AllowableTextFilters))), retStr);

                sb = new StringBuilder();
                sb.AppendLine("|||Start Root en");
                sb.AppendLine("Root_Name EQUAL START allo siefj");
                sb.AppendLine("|||");

                TagItem = "Root";
                TagType = typeof(ReportRootModel);
                LineCount = 2;
                int Item = 4;

                reportTreeNodeList = new List<ReportTreeNode>();

                retStr = reportBaseService.GetReportTreeNodesFromTagText(sb.ToString(), TagItem, TagType, reportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, Item, String.Join(",", AllowableTextFilters)), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesFromTagText_Error_String_Sorting_Ordering_Missing_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("|||Start Root en");
                sb.AppendLine("Root_Name ASCENDING");
                sb.AppendLine("|||");

                string TagItem = "Root";
                Type TagType = typeof(ReportRootModel);
                int LineCount = 2;
                int StartItem = 1;

                List<ReportTreeNode> reportTreeNodeList = new List<ReportTreeNode>();

                bool IsDBFiltering = true;
                string retStr = reportBaseService.GetReportTreeNodesFromTagText(sb.ToString(), TagItem, TagType, reportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_ShouldHave_Items, LineCount, "more than " + (StartItem + 1)), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesFromTagText_Error_String_Missing_Parameter_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("|||Start Root en");
                sb.AppendLine("Root_Name EQUAL");
                sb.AppendLine("|||");

                string TagItem = "Root";
                Type TagType = typeof(ReportRootModel);

                List<ReportTreeNode> reportTreeNodeList = new List<ReportTreeNode>();

                bool IsDBFiltering = true;
                string retStr = reportBaseService.GetReportTreeNodesFromTagText(sb.ToString(), TagItem, TagType, reportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes.Condition_MissingValue, "EQUAL"), retStr);

                sb = new StringBuilder();
                sb.AppendLine("|||Start Root en");
                sb.AppendLine("Root_Name EQUAL bonj START");
                sb.AppendLine("|||");

                TagItem = "Root";
                TagType = typeof(ReportRootModel);

                reportTreeNodeList = new List<ReportTreeNode>();

                retStr = reportBaseService.GetReportTreeNodesFromTagText(sb.ToString(), TagItem, TagType, reportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes.Condition_MissingValue, "START"), retStr);

                sb = new StringBuilder();
                sb.AppendLine("|||Start Root en");
                sb.AppendLine("Root_Name EQUAL bonj START A END");
                sb.AppendLine("|||");

                TagItem = "Root";
                TagType = typeof(ReportRootModel);

                reportTreeNodeList = new List<ReportTreeNode>();

                retStr = reportBaseService.GetReportTreeNodesFromTagText(sb.ToString(), TagItem, TagType, reportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes.Condition_MissingValue, "END"), retStr);

                sb = new StringBuilder();
                sb.AppendLine("|||Start Root en");
                sb.AppendLine("Root_Name ASCENDING 1 EQUAL");
                sb.AppendLine("|||");

                TagItem = "Root";
                TagType = typeof(ReportRootModel);

                reportTreeNodeList = new List<ReportTreeNode>();

                retStr = reportBaseService.GetReportTreeNodesFromTagText(sb.ToString(), TagItem, TagType, reportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes.Condition_MissingValue, "EQUAL"), retStr);

                sb = new StringBuilder();
                sb.AppendLine("|||Start Root en");
                sb.AppendLine("Root_Name ASCENDING 1 EQUAL bonj START");
                sb.AppendLine("|||");

                TagItem = "Root";
                TagType = typeof(ReportRootModel);

                reportTreeNodeList = new List<ReportTreeNode>();

                retStr = reportBaseService.GetReportTreeNodesFromTagText(sb.ToString(), TagItem, TagType, reportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes.Condition_MissingValue, "START"), retStr);

                sb = new StringBuilder();
                sb.AppendLine("|||Start Root en");
                sb.AppendLine("Root_Name ASCENDING 1 EQUAL bonj START a END");
                sb.AppendLine("|||");

                TagItem = "Root";
                TagType = typeof(ReportRootModel);

                reportTreeNodeList = new List<ReportTreeNode>();

                retStr = reportBaseService.GetReportTreeNodesFromTagText(sb.ToString(), TagItem, TagType, reportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes.Condition_MissingValue, "END"), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesFromTagText_Error_FilePurpose_ShouldBeOneOf_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("|||Start Root_File en");
                sb.AppendLine("Root_File_File_Purpose ASCENDINGNot");
                sb.AppendLine("|||");

                string TagItem = "Root_File";
                Type TagType = typeof(ReportRoot_FileModel);
                int LineCount = 2;
                int Item = 2;

                List<ReportTreeNode> reportTreeNodeList = new List<ReportTreeNode>();

                bool IsDBFiltering = true;
                string retStr = reportBaseService.GetReportTreeNodesFromTagText(sb.ToString(), TagItem, TagType, reportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, Item, String.Join(",", AllowableSortingFilters.Concat(AllowableEnumFilters))), retStr);

                sb = new StringBuilder();
                sb.AppendLine("|||Start Root_File en");
                sb.AppendLine("Root_File_File_Purpose EQUALNot");
                sb.AppendLine("|||");

                TagItem = "Root_File";
                TagType = typeof(ReportRoot_FileModel);
                LineCount = 2;
                Item = 2;

                reportTreeNodeList = new List<ReportTreeNode>();

                retStr = reportBaseService.GetReportTreeNodesFromTagText(sb.ToString(), TagItem, TagType, reportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, Item, String.Join(",", AllowableSortingFilters.Concat(AllowableEnumFilters))), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesFromTagText_Error_File_Purpose_Sorting_Ordering_Missing_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("|||Start Root_File en");
                sb.AppendLine("Root_File_File_Purpose ASCENDING");
                sb.AppendLine("|||");

                string TagItem = "Root_File";
                Type TagType = typeof(ReportRoot_FileModel);
                int LineCount = 2;
                int StartItem = 1;

                List<ReportTreeNode> reportTreeNodeList = new List<ReportTreeNode>();

                bool IsDBFiltering = true;
                string retStr = reportBaseService.GetReportTreeNodesFromTagText(sb.ToString(), TagItem, TagType, reportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_ShouldHave_Items, LineCount, "more than " + (StartItem + 1)), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetReportTreeNodesFromTagText_Error_File_Purpose_Missing_Parameter_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("|||Start Root_File en");
                sb.AppendLine("Root_File_File_Purpose EQUAL");
                sb.AppendLine("|||");

                string TagItem = "Root_File";
                Type TagType = typeof(ReportRoot_FileModel);

                List<ReportTreeNode> reportTreeNodeList = new List<ReportTreeNode>();

                bool IsDBFiltering = true;
                string retStr = reportBaseService.GetReportTreeNodesFromTagText(sb.ToString(), TagItem, TagType, reportTreeNodeList, IsDBFiltering);
                Assert.AreEqual(string.Format(ReportServiceRes.Condition_MissingValue, "EQUAL"), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetSelectedFieldsAndProperties_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], "Root");
                Assert.IsNotNull(reportTreeNode);

                string fieldName = "Root_ID";
                ReportTreeNode reportTreeNodeField = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], fieldName);
                Assert.IsNotNull(reportTreeNodeField);
                reportTreeNodeField.Checked = true;
                reportTreeNodeField.dbFilteringNumberFieldList.Add(new ReportConditionNumberField());
                reportTreeNodeField.dbFilteringNumberFieldList[0].ReportCondition = ReportConditionEnum.ReportConditionBigger;
                reportTreeNodeField.dbFilteringNumberFieldList[0].NumberCondition = 45;

                StringBuilder sb = new StringBuilder();

                reportBaseService.GetSelectedFieldsAndProperties(reportTreeNode, sb);
                Assert.AreEqual(fieldName + " " +
                    reportBaseService.GetReportConditionText(ReportConditionEnum.ReportConditionBigger) + " 45\r\n", sb.ToString());

                reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], "Root_File");
                Assert.IsNotNull(reportTreeNode);

                fieldName = "Root_File_File_Purpose";
                reportTreeNodeField = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], fieldName);
                Assert.IsNotNull(reportTreeNodeField);
                reportTreeNodeField.Checked = true;
                reportTreeNodeField.dbFilteringEnumFieldList.Add(new ReportConditionEnumField());
                reportTreeNodeField.dbFilteringEnumFieldList[0].ReportCondition = ReportConditionEnum.ReportConditionEqual;
                reportTreeNodeField.dbFilteringEnumFieldList[0].EnumConditionText = "Picture";

                sb = new StringBuilder();

                reportBaseService.GetSelectedFieldsAndProperties(reportTreeNode, sb);
                Assert.AreEqual(fieldName + " " +
                    reportBaseService.GetReportConditionText(ReportConditionEnum.ReportConditionEqual) + " Picture\r\n", sb.ToString());
            }
        }
        [TestMethod]
        public void ReportBaseService_GetSelectedFieldsContainer_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], "Root");
                Assert.IsNotNull(reportTreeNode);

                string fieldName = "Root_Lat";
                ReportTreeNode reportTreeNodeField = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], fieldName);
                Assert.IsNotNull(reportTreeNodeField);

                reportTreeNodeField.Checked = true;
                reportTreeNodeField.reportFormatingField = new ReportFormatingField();
                reportTreeNodeField.reportFormatingField.ReportFormatingNumber = ReportFormatingNumberEnum.ReportFormatingNumber1Decimal;

                StringBuilder sb = new StringBuilder();

                reportBaseService.GetSelectedFieldsContainer(reportTreeNode, sb);
                Assert.AreEqual(Marker + fieldName + " FORMAT " +
                    reportBaseService.GetNumberFormatText(ReportFormatingNumberEnum.ReportFormatingNumber1Decimal) +
                    Marker + " \r\n", sb.ToString());
            }
        }
        [TestMethod]
        public void ReportBaseService_GetSelectedFieldsFirstLine_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], "Root");
                Assert.IsNotNull(reportTreeNode);

                string fieldName = "Root_Lat";
                ReportTreeNode reportTreeNodeField = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], fieldName);
                Assert.IsNotNull(reportTreeNodeField);
                reportTreeNodeField.Checked = true;
                reportTreeNodeField.reportFormatingField = new ReportFormatingField();
                reportTreeNodeField.reportFormatingField.ReportFormatingNumber = ReportFormatingNumberEnum.ReportFormatingNumber1Decimal;

                StringBuilder sb = new StringBuilder();

                reportBaseService.GetSelectedFieldsFirstLine(reportTreeNode, sb);
                Assert.AreEqual(fieldName + ",", sb.ToString());

                string fieldName2 = "Root_Lng";
                reportTreeNodeField = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], fieldName2);
                Assert.IsNotNull(reportTreeNodeField);
                reportTreeNodeField.Checked = true;
                reportTreeNodeField.reportFormatingField = new ReportFormatingField();
                reportTreeNodeField.reportFormatingField.ReportFormatingNumber = ReportFormatingNumberEnum.ReportFormatingNumber1Decimal;

                sb = new StringBuilder();

                reportBaseService.GetSelectedFieldsFirstLine(reportTreeNode, sb);
                Assert.AreEqual(fieldName + "," + fieldName2 + ",", sb.ToString());

            }
        }
        [TestMethod]
        public void ReportBaseService_GetSubFieldIsChecked_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], "Root");
                Assert.IsNotNull(reportTreeNode);

                string fieldName = "Root_Lat";
                ReportTreeNode reportTreeNodeField = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], fieldName);
                Assert.IsNotNull(reportTreeNodeField);

                bool retBool = reportBaseService.GetSubFieldIsChecked(reportTreeNode);
                Assert.IsFalse(retBool);

                reportTreeNodeField.Checked = true;
                reportTreeNodeField.reportFormatingField = new ReportFormatingField();
                reportTreeNodeField.reportFormatingField.ReportFormatingNumber = ReportFormatingNumberEnum.ReportFormatingNumber1Decimal;

                StringBuilder sb = new StringBuilder();

                retBool = reportBaseService.GetSubFieldIsChecked(reportTreeNode);
                Assert.IsTrue(retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_GetTreeViewSelectedStatus_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                string TableName = "Root";
                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], TableName);
                Assert.IsNotNull(reportTreeNode);

                string fieldName = "Root_Lat";
                ReportTreeNode reportTreeNodeField = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], fieldName);
                Assert.IsNotNull(reportTreeNodeField);
                reportTreeNodeField.Checked = true;

                StringBuilder sb = new StringBuilder();
                StringBuilder sbFirstLine = new StringBuilder();

                reportBaseService.ReportFileType = ReportFileTypeEnum.Word;

                int Level = 0;
                reportBaseService.GetTreeViewSelectedStatus(reportTreeNode, sb, sbFirstLine, Level);
                Assert.AreEqual(Marker + "Start " + TableName + " " + culture.TwoLetterISOLanguageName + "\r\n" +
                    fieldName + "\r\n" + Marker + "\r\n" +
                    Marker + fieldName + Marker + " \r\n" +
                    Marker + "End " + TableName + " " + culture.TwoLetterISOLanguageName +
                    Marker + "\r\n", sb.ToString());
            }
        }
        [TestMethod]
        public void ReportBaseService_FindReportTreeNode_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], "Root");
                Assert.IsNotNull(reportTreeNode);

                string fieldName = "Root_Lat";
                ReportTreeNode reportTreeNodeField = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], fieldName);
                Assert.IsNotNull(reportTreeNodeField);

                ReportTreeNode reportTreeNodeRet = reportBaseService.FindReportTreeNode(reportTreeNode, fieldName);
                Assert.AreEqual("Root_Lat", reportTreeNodeRet.Text);
            }
        }
        [TestMethod]
        public void ReportBaseService_IsConditionIntTrue_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], "Root");
                Assert.IsNotNull(reportTreeNode);

                string fieldName = "Root_Lat";
                ReportTreeNode reportTreeNodeField = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], fieldName);
                Assert.IsNotNull(reportTreeNodeField);

                reportTreeNodeField.Checked = true;
                reportTreeNodeField.reportConditionNumberFieldList.Add(new ReportConditionNumberField());
                reportTreeNodeField.reportConditionNumberFieldList[0].NumberCondition = 23;

                bool retBool = reportBaseService.IsConditionIntTrue(reportTreeNodeField.reportConditionNumberFieldList[0], 23);
                Assert.IsTrue(retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_IsConditionSingleTrue_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], "Root");
                Assert.IsNotNull(reportTreeNode);

                string fieldName = "Root_Lat";
                ReportTreeNode reportTreeNodeField = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], fieldName);
                Assert.IsNotNull(reportTreeNodeField);

                reportTreeNodeField.Checked = true;
                reportTreeNodeField.reportConditionNumberFieldList.Add(new ReportConditionNumberField());
                reportTreeNodeField.reportConditionNumberFieldList[0].NumberCondition = 23;

                bool retBool = reportBaseService.IsConditionSingleTrue(reportTreeNodeField.reportConditionNumberFieldList[0], 23);
                Assert.IsTrue(retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_IsConditionStringTrue_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], "Root");
                Assert.IsNotNull(reportTreeNode);

                string fieldName = "Root_Name";
                ReportTreeNode reportTreeNodeField = GetReportTreeNodeWithText((ReportTreeNode)treeViewCSSP.Nodes[0], fieldName);
                Assert.IsNotNull(reportTreeNodeField);

                reportTreeNodeField.Checked = true;
                reportTreeNodeField.reportConditionTextFieldList.Add(new ReportConditionTextField());
                reportTreeNodeField.reportConditionTextFieldList[0].TextCondition = "Allo";

                bool retBool = reportBaseService.IsConditionStringTrue(reportTreeNodeField.reportConditionTextFieldList[0], "Allo");
                Assert.IsTrue(retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_LoadRecursiveTreeNode_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                reportBaseService.LoadRecursiveTreeNode((ReportTreeNode)treeViewCSSP.Nodes[0]);
                Assert.AreEqual("Root", treeViewCSSP.Nodes[0].Text);
                Assert.AreEqual("Root_Fields", treeViewCSSP.Nodes[0].Nodes[0].Text);
                Assert.AreEqual("Root_Error", treeViewCSSP.Nodes[0].Nodes[0].Nodes[0].Text);
                Assert.AreEqual("Country", treeViewCSSP.Nodes[0].Nodes[1].Text);
                Assert.AreEqual("Country_Fields", treeViewCSSP.Nodes[0].Nodes[1].Nodes[0].Text);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_System_Boolean_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Root_Is_Active",
                };
                ReportConditionTrueFalseField dbFilteringTrueFalseField = new ReportConditionTrueFalseField()
                {
                    ReportCondition = ReportConditionEnum.ReportConditionTrue
                };
                reportTreeNode.dbFilteringTrueFalseFieldList.Add(dbFilteringTrueFalseField);

                ReportRootModel reportModel = new ReportRootModel()
                {
                    Root_Is_Active = true,
                };

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                reportModel.Root_Is_Active = false;

                IsDBFiltering = true;
                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringTrueFalseField.ReportCondition = ReportConditionEnum.ReportConditionFalse;

                reportModel.Root_Is_Active = true;

                IsDBFiltering = true;
                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                reportModel.Root_Is_Active = false;

                IsDBFiltering = true;
                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                ReportConditionTrueFalseField reportConditionTrueFalseField = new ReportConditionTrueFalseField()
                {
                    ReportCondition = ReportConditionEnum.ReportConditionTrue
                };
                reportTreeNode.reportConditionTrueFalseFieldList.Add(reportConditionTrueFalseField);

                reportModel.Root_Is_Active = true;

                IsDBFiltering = false;
                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                reportModel.Root_Is_Active = false;

                IsDBFiltering = false;
                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                reportConditionTrueFalseField.ReportCondition = ReportConditionEnum.ReportConditionFalse;

                reportModel.Root_Is_Active = true;

                IsDBFiltering = false;
                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                reportModel.Root_Is_Active = false;

                IsDBFiltering = false;
                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_System_DateTime_BIGGER_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Root_Last_Update_Date_And_Time_UTC",
                };

                ReportRootModel reportModel = new ReportRootModel()
                {
                    Root_Last_Update_Date_And_Time_UTC = new DateTime(2010, 10, 9, 8, 7, 6)
                };

                ReportConditionDateField dbFilteringDateField = new ReportConditionDateField();
                dbFilteringDateField.ReportCondition = ReportConditionEnum.ReportConditionBigger;
                reportTreeNode.dbFilteringDateFieldList.Add(dbFilteringDateField);

                dbFilteringDateField.DateTimeConditionYear = 2010;

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionYear = 2011;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringDateField.DateTimeConditionYear = null;
                dbFilteringDateField.DateTimeConditionMonth = 10;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionMonth = 11;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringDateField.DateTimeConditionMonth = null;
                dbFilteringDateField.DateTimeConditionDay = 9;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionDay = 10;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringDateField.DateTimeConditionDay = null;
                dbFilteringDateField.DateTimeConditionHour = 8;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionHour = 9;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringDateField.DateTimeConditionHour = null;
                dbFilteringDateField.DateTimeConditionMinute = 7;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringDateField.DateTimeConditionMinute = 8;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringDateField.DateTimeConditionMinute = null;
                dbFilteringDateField.DateTimeConditionYear = 2010;
                dbFilteringDateField.DateTimeConditionMonth = 10;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionYear = 2011;
                dbFilteringDateField.DateTimeConditionMonth = 11;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringDateField.DateTimeConditionYear = 2010;
                dbFilteringDateField.DateTimeConditionMonth = 11;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringDateField.DateTimeConditionYear = 2011;
                dbFilteringDateField.DateTimeConditionMonth = 10;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringDateField.DateTimeConditionYear = 2010;
                dbFilteringDateField.DateTimeConditionMonth = 10;
                dbFilteringDateField.DateTimeConditionDay = 9;
                dbFilteringDateField.DateTimeConditionHour = 8;
                dbFilteringDateField.DateTimeConditionMinute = 8;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringDateField.DateTimeConditionMinute = 6;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionMinute = 7;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionYear = null;
                dbFilteringDateField.DateTimeConditionMonth = 10;
                dbFilteringDateField.DateTimeConditionDay = 9;
                dbFilteringDateField.DateTimeConditionHour = 8;
                dbFilteringDateField.DateTimeConditionMinute = 6;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionMinute = 8;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringDateField.DateTimeConditionYear = null;
                dbFilteringDateField.DateTimeConditionMonth = null;
                dbFilteringDateField.DateTimeConditionDay = 9;
                dbFilteringDateField.DateTimeConditionHour = 8;
                dbFilteringDateField.DateTimeConditionMinute = 6;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionMinute = 8;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringDateField.DateTimeConditionYear = null;
                dbFilteringDateField.DateTimeConditionMonth = null;
                dbFilteringDateField.DateTimeConditionDay = null;
                dbFilteringDateField.DateTimeConditionHour = 8;
                dbFilteringDateField.DateTimeConditionMinute = 6;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionMinute = 8;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringDateField.DateTimeConditionYear = null;
                dbFilteringDateField.DateTimeConditionMonth = null;
                dbFilteringDateField.DateTimeConditionDay = null;
                dbFilteringDateField.DateTimeConditionHour = null;
                dbFilteringDateField.DateTimeConditionMinute = 6;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionMinute = 8;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_System_DateTime_EQUAL_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Root_Last_Update_Date_And_Time_UTC",
                };

                ReportRootModel reportModel = new ReportRootModel()
                {
                    Root_Last_Update_Date_And_Time_UTC = new DateTime(2010, 10, 9, 8, 7, 6)
                };

                ReportConditionDateField dbFilteringDateField = new ReportConditionDateField();
                dbFilteringDateField.ReportCondition = ReportConditionEnum.ReportConditionEqual;
                dbFilteringDateField.DateTimeConditionYear = 2010;

                reportTreeNode.dbFilteringDateFieldList.Add(dbFilteringDateField);

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionYear = 2011;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringDateField.DateTimeConditionYear = null;
                dbFilteringDateField.DateTimeConditionMonth = 10;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionMonth = 11;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringDateField.DateTimeConditionMonth = null;
                dbFilteringDateField.DateTimeConditionDay = 9;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionDay = 10;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringDateField.DateTimeConditionDay = null;
                dbFilteringDateField.DateTimeConditionHour = 8;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionHour = 9;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringDateField.DateTimeConditionHour = null;
                dbFilteringDateField.DateTimeConditionMinute = 7;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionMinute = 8;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringDateField.DateTimeConditionMinute = null;
                dbFilteringDateField.DateTimeConditionYear = 2010;
                dbFilteringDateField.DateTimeConditionMonth = 10;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionYear = 2011;
                dbFilteringDateField.DateTimeConditionMonth = 11;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringDateField.DateTimeConditionYear = 2010;
                dbFilteringDateField.DateTimeConditionMonth = 11;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringDateField.DateTimeConditionYear = 2011;
                dbFilteringDateField.DateTimeConditionMonth = 10;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_System_DateTime_SMALLER_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Root_Last_Update_Date_And_Time_UTC",
                };

                ReportRootModel reportModel = new ReportRootModel()
                {
                    Root_Last_Update_Date_And_Time_UTC = new DateTime(2010, 10, 9, 8, 7, 6)
                };

                ReportConditionDateField dbFilteringDateField = new ReportConditionDateField();
                dbFilteringDateField.ReportCondition = ReportConditionEnum.ReportConditionSmaller;
                reportTreeNode.dbFilteringDateFieldList.Add(dbFilteringDateField);

                dbFilteringDateField.DateTimeConditionYear = 2010;

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringDateField.DateTimeConditionYear = 2011;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionYear = null;
                dbFilteringDateField.DateTimeConditionMonth = 10;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionMonth = 11;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionMonth = null;
                dbFilteringDateField.DateTimeConditionDay = 9;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionDay = 10;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionDay = null;
                dbFilteringDateField.DateTimeConditionHour = 8;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionHour = 9;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionHour = null;
                dbFilteringDateField.DateTimeConditionMinute = 7;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringDateField.DateTimeConditionMinute = 8;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionMinute = null;
                dbFilteringDateField.DateTimeConditionYear = 2010;
                dbFilteringDateField.DateTimeConditionMonth = 10;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringDateField.DateTimeConditionYear = 2011;
                dbFilteringDateField.DateTimeConditionMonth = 11;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionYear = 2010;
                dbFilteringDateField.DateTimeConditionMonth = 11;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionYear = 2011;
                dbFilteringDateField.DateTimeConditionMonth = 10;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionYear = 2010;
                dbFilteringDateField.DateTimeConditionMonth = 10;
                dbFilteringDateField.DateTimeConditionDay = 9;
                dbFilteringDateField.DateTimeConditionHour = 8;
                dbFilteringDateField.DateTimeConditionMinute = 8;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionMinute = 6;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringDateField.DateTimeConditionMinute = 7;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringDateField.DateTimeConditionYear = null;
                dbFilteringDateField.DateTimeConditionMonth = 10;
                dbFilteringDateField.DateTimeConditionDay = 9;
                dbFilteringDateField.DateTimeConditionHour = 8;
                dbFilteringDateField.DateTimeConditionMinute = 6;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringDateField.DateTimeConditionMinute = 8;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionYear = null;
                dbFilteringDateField.DateTimeConditionMonth = null;
                dbFilteringDateField.DateTimeConditionDay = 9;
                dbFilteringDateField.DateTimeConditionHour = 8;
                dbFilteringDateField.DateTimeConditionMinute = 6;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringDateField.DateTimeConditionMinute = 8;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionYear = null;
                dbFilteringDateField.DateTimeConditionMonth = null;
                dbFilteringDateField.DateTimeConditionDay = null;
                dbFilteringDateField.DateTimeConditionHour = 8;
                dbFilteringDateField.DateTimeConditionMinute = 6;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringDateField.DateTimeConditionMinute = 8;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringDateField.DateTimeConditionYear = null;
                dbFilteringDateField.DateTimeConditionMonth = null;
                dbFilteringDateField.DateTimeConditionDay = null;
                dbFilteringDateField.DateTimeConditionHour = null;
                dbFilteringDateField.DateTimeConditionMinute = 6;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringDateField.DateTimeConditionMinute = 8;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_System_Single_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Root_Lat",
                };

                ReportRootModel reportModel = new ReportRootModel()
                {
                    Root_Lat = 45.3f
                };

                ReportConditionNumberField dbFilteringNumberField = new ReportConditionNumberField();
                reportTreeNode.dbFilteringNumberFieldList.Add(dbFilteringNumberField);

                // BIGGER
                dbFilteringNumberField.ReportCondition = ReportConditionEnum.ReportConditionBigger;

                dbFilteringNumberField.NumberCondition = 45.2999f;

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringNumberField.NumberCondition = 45.31f;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                // SMALLER
                dbFilteringNumberField.ReportCondition = ReportConditionEnum.ReportConditionSmaller;

                dbFilteringNumberField.NumberCondition = 45.31f;

                IsDBFiltering = true;
                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringNumberField.NumberCondition = 45.2999f;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                // EQUAL
                dbFilteringNumberField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringNumberField.NumberCondition = 45.3f;

                IsDBFiltering = true;
                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringNumberField.NumberCondition = 45.2999f;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_System_Int32_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Root_Stat_Subsector_Count",
                };

                ReportRootModel reportModel = new ReportRootModel()
                {
                    Root_Stat_Subsector_Count = 45
                };

                ReportConditionNumberField dbFilteringNumberField = new ReportConditionNumberField();
                reportTreeNode.dbFilteringNumberFieldList.Add(dbFilteringNumberField);

                // BIGGER
                dbFilteringNumberField.ReportCondition = ReportConditionEnum.ReportConditionBigger;

                dbFilteringNumberField.NumberCondition = 44f;

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringNumberField.NumberCondition = 46f;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                // SMALLER
                dbFilteringNumberField.ReportCondition = ReportConditionEnum.ReportConditionSmaller;

                dbFilteringNumberField.NumberCondition = 46f;

                IsDBFiltering = true;
                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringNumberField.NumberCondition = 44f;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                // EQUAL
                dbFilteringNumberField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringNumberField.NumberCondition = 45f;

                IsDBFiltering = true;
                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringNumberField.NumberCondition = 449f;

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_System_String_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Root_Name",
                };

                ReportRootModel reportModel = new ReportRootModel()
                {
                    Root_Name = "All locations"
                };

                ReportConditionTextField dbFilteringTextField = new ReportConditionTextField();
                reportTreeNode.dbFilteringTextFieldList.Add(dbFilteringTextField);

                // CONTAIN
                dbFilteringTextField.ReportCondition = ReportConditionEnum.ReportConditionContain;

                dbFilteringTextField.TextCondition = "All";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringTextField.TextCondition = "Am";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                // START
                dbFilteringTextField.ReportCondition = ReportConditionEnum.ReportConditionStart;

                dbFilteringTextField.TextCondition = "All";

                IsDBFiltering = true;
                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringTextField.TextCondition = "Am";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                // END
                dbFilteringTextField.ReportCondition = ReportConditionEnum.ReportConditionEnd;

                dbFilteringTextField.TextCondition = "locations";

                IsDBFiltering = true;
                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringTextField.TextCondition = "nottions";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                // BIGGER
                dbFilteringTextField.ReportCondition = ReportConditionEnum.ReportConditionBigger;

                dbFilteringTextField.TextCondition = "Ak";

                IsDBFiltering = true;
                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringTextField.TextCondition = "Am";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                // SMALLER
                dbFilteringTextField.ReportCondition = ReportConditionEnum.ReportConditionSmaller;

                dbFilteringTextField.TextCondition = "Am";

                IsDBFiltering = true;
                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringTextField.TextCondition = "Ak";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                // EQUAL
                dbFilteringTextField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringTextField.TextCondition = "All locations";

                IsDBFiltering = true;
                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringTextField.TextCondition = "Ak";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_FilePurposeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Root_File_File_Purpose",
                };

                ReportRoot_FileModel reportModel = new ReportRoot_FileModel()
                {
                    Root_File_File_Purpose = FilePurposeEnum.MikeInput
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "MikeInput";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRoot_FileModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "MikeInputNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRoot_FileModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "MikeInputMDF-MikeResultKMZ-Image";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRoot_FileModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_FileTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Root_File_File_Type",
                };

                ReportRoot_FileModel reportModel = new ReportRoot_FileModel()
                {
                    Root_File_File_Type = FileTypeEnum.GIF
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "GIF";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRoot_FileModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "GIFNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRoot_FileModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "DFS1-DFSU-LOG-M3FM";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRoot_FileModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_TranslationStatusEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Root_Name_Translation_Status",
                };

                ReportRootModel reportModel = new ReportRootModel()
                {
                    Root_Name_Translation_Status = TranslationStatusEnum.Translated
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "Translated";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "TranslatedNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "NotTranslated-ElectronicallyTranslated";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_BoxModelResultTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Box_Model_Result_Result_Type",
                };

                ReportBox_Model_ResultModel reportModel = new ReportBox_Model_ResultModel()
                {
                    Box_Model_Result_Result_Type = BoxModelResultTypeEnum.Dilution
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "Dilution";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "DilutionNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "NoDecayUntreated-DecayUntreated";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_InfrastructureTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Infrastructure_Infrastructure_Type",
                };

                ReportInfrastructureModel reportModel = new ReportInfrastructureModel()
                {
                    Infrastructure_Infrastructure_Type = InfrastructureTypeEnum.WWTP
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "WWTP";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "WWTPNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "LiftStation-SeeOther";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_FacilityTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Infrastructure_Facility_Type",
                };

                ReportInfrastructureModel reportModel = new ReportInfrastructureModel()
                {
                    Infrastructure_Facility_Type = FacilityTypeEnum.Lagoon
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "Lagoon";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "LagoonNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "Plant-Error";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_AerationTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Infrastructure_Aeration_Type",
                };

                ReportInfrastructureModel reportModel = new ReportInfrastructureModel()
                {
                    Infrastructure_Aeration_Type = AerationTypeEnum.Diffuser
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "Diffuser";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "DiffuserNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "Surface-Error";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_PreliminaryTreatmentTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Infrastructure_Preliminary_Treatment_Type",
                };

                ReportInfrastructureModel reportModel = new ReportInfrastructureModel()
                {
                    Infrastructure_Preliminary_Treatment_Type = PreliminaryTreatmentTypeEnum.BarScreen
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "BarScreen";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "BarScreenNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "NotApplicable-Grinder";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_PrimaryTreatmentTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Infrastructure_Primary_Treatment_Type",
                };

                ReportInfrastructureModel reportModel = new ReportInfrastructureModel()
                {
                    Infrastructure_Primary_Treatment_Type = PrimaryTreatmentTypeEnum.Filtration
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "Filtration";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "FiltrationNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "Sedimentation-ChemicalCoagulation";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_SecondaryTreatmentTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Infrastructure_Secondary_Treatment_Type",
                };

                ReportInfrastructureModel reportModel = new ReportInfrastructureModel()
                {
                    Infrastructure_Secondary_Treatment_Type = SecondaryTreatmentTypeEnum.OxidationDitch
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "OxidationDitch";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "OxidationDitchNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "RotatingBiologicalContactor-TricklingFilters";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_TertiaryTreatmentTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Infrastructure_Tertiary_Treatment_Type",
                };

                ReportInfrastructureModel reportModel = new ReportInfrastructureModel()
                {
                    Infrastructure_Tertiary_Treatment_Type = TertiaryTreatmentTypeEnum.Adsorption
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "Adsorption";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "AdsorptionNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "MembraneFiltration-ReverseOsmosis";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_TreatmentTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Infrastructure_Treatment_Type",
                };

                ReportInfrastructureModel reportModel = new ReportInfrastructureModel()
                {
                    Infrastructure_Treatment_Type = TreatmentTypeEnum.BioDisks
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "BioDisks";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "BioDisksNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "OxidationDitchOnly-TricklingFilter-TrashRackRakeOnly-SepticTank";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_DisinfectionTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Infrastructure_Disinfection_Type",
                };

                ReportInfrastructureModel reportModel = new ReportInfrastructureModel()
                {
                    Infrastructure_Disinfection_Type = DisinfectionTypeEnum.ChlorinationNoDechlorination
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "ChlorinationNoDechlorination";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "ChlorinationNoDechlorinationNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "ChlorinationWithDechlorination-UVSeasonal-ChlorinationWithDechlorinationSeasonal";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_CollectionSystemTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Infrastructure_Collection_System_Type",
                };

                ReportInfrastructureModel reportModel = new ReportInfrastructureModel()
                {
                    Infrastructure_Collection_System_Type = CollectionSystemTypeEnum.Combined30Separated70
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "Combined30Separated70";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "Combined30Separated70Not";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "Combined90Separated10-Combined80Separated20-Combined60Separated40";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_AlarmSystemTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Infrastructure_Alarm_System_Type",
                };

                ReportInfrastructureModel reportModel = new ReportInfrastructureModel()
                {
                    Infrastructure_Alarm_System_Type = AlarmSystemTypeEnum.PagerAndLight
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "PagerAndLight";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "PagerAndLightNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "OnlyVisualLight-SCADAAndLight";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_ScenarioStatusEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Visual_Plumes_Scenario_Status",
                };

                ReportVisual_Plumes_ScenarioModel reportModel = new ReportVisual_Plumes_ScenarioModel()
                {
                    Visual_Plumes_Scenario_Status = ScenarioStatusEnum.Changed
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "Changed";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "ChangedNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "Copying-Changing-AskToRun";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_StorageDataTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Climate_Site_Data_Storage_Data_Type",
                };

                ReportClimate_Site_DataModel reportModel = new ReportClimate_Site_DataModel()
                {
                    Climate_Site_Data_Storage_Data_Type = StorageDataTypeEnum.Forcasted
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "Forcasted";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "ForcastedNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "Archived-Observed";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_LanguageEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Root_File_Language",
                };

                ReportRoot_FileModel reportModel = new ReportRoot_FileModel()
                {
                    Root_File_Language = LanguageEnum.en
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "en";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "enNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "fr-enAndfr";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_BeaufortScaleEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "MWQM_Run_Sea_State_At_End_Beaufort_Scale",
                };

                ReportMWQM_RunModel reportModel = new ReportMWQM_RunModel()
                {
                    MWQM_Run_Sea_State_At_End_Beaufort_Scale = BeaufortScaleEnum.FreshBreeze
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "FreshBreeze";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "FreshBreezeNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "GentleBreeze-FreshBreeze-StrongBreeze";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_AnalyzeMethodEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "MWQM_Run_Analyze_Method",
                };

                ReportMWQM_RunModel reportModel = new ReportMWQM_RunModel()
                {
                    MWQM_Run_Analyze_Method = AnalyzeMethodEnum.MPN
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "MPN";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "MPNNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "AnalyzeMethod9-AnalyzeMethod10-AnalyzeMethod11";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_SampleMatrixEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "MWQM_Run_Sample_Matrix",
                };

                ReportMWQM_RunModel reportModel = new ReportMWQM_RunModel()
                {
                    MWQM_Run_Sample_Matrix = SampleMatrixEnum.Water
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "Water";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "WaterNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "S-SampleMatrix5-SampleMatrix6";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_LaboratoryEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "MWQM_Run_Laboratory",
                };

                ReportMWQM_RunModel reportModel = new ReportMWQM_RunModel()
                {
                    MWQM_Run_Laboratory = LaboratoryEnum.MonctonEnvironmentCanada
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "MonctonEnvironmentCanada";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "MonctonEnvironmentCanadaNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "_14BC-_15BC-_17BC";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_SampleStatusEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "MWQM_Run_Sample_Status",
                };

                ReportMWQM_RunModel reportModel = new ReportMWQM_RunModel()
                {
                    MWQM_Run_Sample_Status = SampleStatusEnum.Active
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "Active";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "ActiveNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "SampleStatus3-SampleStatus4-SampleStatus5";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_ConfigTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "MWQM_Plan_Config_Type",
                };

                ReportMWQM_PlanModel reportModel = new ReportMWQM_PlanModel()
                {
                    MWQM_Plan_Config_Type = ConfigTypeEnum.Subsector
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "Subsector";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "SubsectorNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "Municipality-Error";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_LabSheetTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Subsector_Lab_Sheet_Type",
                };

                ReportSubsector_Lab_SheetModel reportModel = new ReportSubsector_Lab_SheetModel()
                {
                    Subsector_Lab_Sheet_Type = LabSheetTypeEnum.A1
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "A1";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "A1Not";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "LTB-EC";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_LabSheetStatusEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Subsector_Lab_Sheet_Status",
                };

                ReportSubsector_Lab_SheetModel reportModel = new ReportSubsector_Lab_SheetModel()
                {
                    Subsector_Lab_Sheet_Status = LabSheetStatusEnum.Created
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "Created";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "CreatedNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "Transferred-Approved";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_PolSourceInactiveReasonEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Pol_Source_Site_Inactive_Reason",
                };

                ReportPol_Source_SiteModel reportModel = new ReportPol_Source_SiteModel()
                {
                    Pol_Source_Site_Inactive_Reason = PolSourceInactiveReasonEnum.Abandoned
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "Abandoned";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "AbandonedNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "Closed-Removed";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_AddressTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Municipality_Contact_Address_Type",
                };

                ReportMunicipality_Contact_AddressModel reportModel = new ReportMunicipality_Contact_AddressModel()
                {
                    Municipality_Contact_Address_Type = AddressTypeEnum.Civic
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "Civic";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "CivicNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "Mailing-Shipping";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_StreetTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Municipality_Contact_Address_Street_Type",
                };

                ReportMunicipality_Contact_AddressModel reportModel = new ReportMunicipality_Contact_AddressModel()
                {
                    Municipality_Contact_Address_Street_Type = StreetTypeEnum.Road
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "Road";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "RoadNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "Avenue-Crescent-Alley";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_ContactTitleEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Municipality_Contact_Title",
                };

                ReportMunicipality_ContactModel reportModel = new ReportMunicipality_ContactModel()
                {
                    Municipality_Contact_Title = ContactTitleEnum.Engineer
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "Engineer";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "EngineerNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "DirectorPublicWorks-Foreman-FacilitiesManager";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_EmailTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Municipality_Contact_Email_Type",
                };

                ReportMunicipality_Contact_EmailModel reportModel = new ReportMunicipality_Contact_EmailModel()
                {
                    Municipality_Contact_Email_Type = EmailTypeEnum.Personal
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "Personal";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "PersonalNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "Personal2-Work2";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_TelTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Municipality_Contact_Tel_Type",
                };

                ReportMunicipality_Contact_TelModel reportModel = new ReportMunicipality_Contact_TelModel()
                {
                    Municipality_Contact_Tel_Type = TelTypeEnum.Personal
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "Personal";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "PersonalNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "Personal2-Work2";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_TideTextEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Subsector_Tide_Site_Data_Tide_Start",
                };

                ReportSubsector_Tide_Site_DataModel reportModel = new ReportSubsector_Tide_Site_DataModel()
                {
                    Subsector_Tide_Site_Data_Tide_Start = TideTextEnum.HighTide
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "HighTide";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "HighTideNot";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "LowTideFalling-MidTide-MidTideRising";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_TideDataTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Subsector_Tide_Site_Data_Tide_Data_Type",
                };

                ReportSubsector_Tide_Site_DataModel reportModel = new ReportSubsector_Tide_Site_DataModel()
                {
                    Subsector_Tide_Site_Data_Tide_Data_Type = TideDataTypeEnum.Min60
                };

                ReportConditionEnumField dbFilteringEnumField = new ReportConditionEnumField();
                reportTreeNode.dbFilteringEnumFieldList.Add(dbFilteringEnumField);

                // EQUAL
                dbFilteringEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringEnumField.EnumConditionText = "Min60";

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringEnumField.EnumConditionText = "Min60Not";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringEnumField.EnumConditionText = "Min15-Error";

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_SampleTypeEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "MWQM_Site_Sample_Types",
                };

                ReportMWQM_Site_SampleModel reportModel = new ReportMWQM_Site_SampleModel()
                {
                    MWQM_Site_Sample_Types = ((int)SampleTypeEnum.Routine).ToString()
                };

                ReportConditionTextField dbFilteringTextField = new ReportConditionTextField();
                reportTreeNode.dbFilteringTextFieldList.Add(dbFilteringTextField);

                // EQUAL
                dbFilteringTextField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringTextField.TextCondition = ((int)SampleTypeEnum.Routine).ToString();

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringTextField.TextCondition = (1200).ToString();

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringTextField.TextCondition = ((int)SampleTypeEnum.RainRun).ToString() + "," + ((int)SampleTypeEnum.RainCMPRoutine).ToString();

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_ReturnKeepShow_CSSPEnumsDLL_Enums_PolSourceObsInfoEnum_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                ReportTreeNode reportTreeNode = new ReportTreeNode()
                {
                    Text = "Pol_Source_Site_Obs_Issue_Observation_Sentence",
                };

                ReportPol_Source_Site_Obs_IssueModel reportModel = new ReportPol_Source_Site_Obs_IssueModel()
                {
                    Pol_Source_Site_Obs_Issue_Observation_Sentence = ((int)PolSourceObsInfoEnum.WaterAreaSizeSmall).ToString()
                };

                ReportConditionTextField dbFilteringTextField = new ReportConditionTextField();
                reportTreeNode.dbFilteringTextFieldList.Add(dbFilteringTextField);

                // EQUAL
                dbFilteringTextField.ReportCondition = ReportConditionEnum.ReportConditionEqual;

                dbFilteringTextField.TextCondition = ((int)PolSourceObsInfoEnum.WaterAreaSizeSmall).ToString();

                bool IsDBFiltering = true;
                bool retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(true, retBool);

                dbFilteringTextField.TextCondition = (1200).ToString();

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);

                dbFilteringTextField.TextCondition = ((int)PolSourceObsInfoEnum.WidthInMetersBetween5And25).ToString() + "," + ((int)PolSourceObsInfoEnum.ManurePileLarge).ToString();

                retBool = reportBaseService.ReturnKeepShow(reportModel, typeof(ReportRootModel), reportTreeNode, IsDBFiltering);
                Assert.AreEqual(false, retBool);
            }
        }
        [TestMethod]
        public void ReportBaseService_SetParentChecked_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                // nothing for now
            }
        }
        [TestMethod]
        public void ReportBaseService_SetReportTreeNodeDateCondition_dbFiltering_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                int StartItem = 1;
                ReportTreeNode reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_Last_Update_Date_And_Time");
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.DateAndTime;

                List<string> strList = new List<string>() { "Root_Last_Update_Date_And_Time", "EQUAL", "YEAR", "2000" };
                int LineCount = 2;

                reportTreeNode.dbFilteringDateFieldList.Add(new ReportConditionDateField());

                string retStr = reportBaseService.SetReportTreeNodeDateCondition(StartItem, reportTreeNode.dbFilteringDateFieldList[0], reportTreeNode, strList, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(ReportConditionEnum.ReportConditionEqual, reportTreeNode.dbFilteringDateFieldList[0].ReportCondition);

                reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_Last_Update_Date_And_Time");

                reportTreeNode.dbFilteringDateFieldList.Add(new ReportConditionDateField());

                strList = new List<string>() { "Root_Last_Update_Date_And_Time", "EQUALNot", "YEAR", "2000" };
                LineCount = 2;

                retStr = reportBaseService.SetReportTreeNodeDateCondition(StartItem, reportTreeNode.dbFilteringDateFieldList[0], reportTreeNode, strList, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, 1 + 1, String.Join(",", AllowableBasicFilters)), retStr);
                Assert.AreEqual(ReportConditionEnum.Error, reportTreeNode.dbFilteringDateFieldList[0].ReportCondition);
            }
        }
        [TestMethod]
        public void ReportBaseService_SetReportTreeNodeDateCondition_reportCondition_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                int StartItem = 1;
                ReportTreeNode reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_Last_Update_Date_And_Time");
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.DateAndTime;

                List<string> strList = new List<string>() { "Root_Last_Update_Date_And_Time", "EQUAL", "YEAR", "2000" };
                int LineCount = 2;

                reportTreeNode.reportConditionDateFieldList.Add(new ReportConditionDateField());

                string retStr = reportBaseService.SetReportTreeNodeDateCondition(StartItem, reportTreeNode.reportConditionDateFieldList[0], reportTreeNode, strList, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(ReportConditionEnum.ReportConditionEqual, reportTreeNode.reportConditionDateFieldList[0].ReportCondition);

                reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_Last_Update_Date_And_Time");

                reportTreeNode.reportConditionDateFieldList.Add(new ReportConditionDateField());

                strList = new List<string>() { "Root_Last_Update_Date_And_Time", "EQUALNot", "YEAR", "2000" };
                LineCount = 2;

                retStr = reportBaseService.SetReportTreeNodeDateCondition(StartItem, reportTreeNode.reportConditionDateFieldList[0], reportTreeNode, strList, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, 1 + 1, String.Join(",", AllowableBasicFilters)), retStr);
                Assert.AreEqual(ReportConditionEnum.Error, reportTreeNode.reportConditionDateFieldList[0].ReportCondition);
            }
        }
        [TestMethod]
        public void ReportBaseService_SetReportTreeNodeEnumCondition_dbFiltering_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                int StartItem = 1;
                ReportTreeNode reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_File_File_Purpose");
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.FilePurpose;

                List<string> strList = new List<string>() { "Root_File_File_Purpose", "EQUAL", "MikeInput" };
                int LineCount = 2;

                reportTreeNode.dbFilteringEnumFieldList.Add(new ReportConditionEnumField());

                string retStr = reportBaseService.SetReportTreeNodeEnumCondition(StartItem, reportTreeNode.dbFilteringEnumFieldList[0], reportTreeNode, strList, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(ReportConditionEnum.ReportConditionEqual, reportTreeNode.dbFilteringEnumFieldList[0].ReportCondition);

                reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_File_File_Purpose");

                reportTreeNode.dbFilteringEnumFieldList.Add(new ReportConditionEnumField());

                strList = new List<string>() { "Root_File_File_Purpose", "EQUALNot", "MikeInput" };
                LineCount = 2;

                retStr = reportBaseService.SetReportTreeNodeEnumCondition(StartItem, reportTreeNode.dbFilteringEnumFieldList[0], reportTreeNode, strList, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, StartItem + 1, "EQUAL"), retStr);
                Assert.AreEqual(ReportConditionEnum.Error, reportTreeNode.dbFilteringEnumFieldList[0].ReportCondition);
            }
        }
        [TestMethod]
        public void ReportBaseService_SetReportTreeNodeEnumCondition_reportCondition_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                int StartItem = 1;
                ReportTreeNode reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_File_File_Purpose");
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.FilePurpose;

                List<string> strList = new List<string>() { "Root_File_File_Purpose", "EQUAL", "MikeInput" };
                int LineCount = 2;

                reportTreeNode.reportConditionEnumFieldList.Add(new ReportConditionEnumField());

                string retStr = reportBaseService.SetReportTreeNodeEnumCondition(StartItem, reportTreeNode.reportConditionEnumFieldList[0], reportTreeNode, strList, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(ReportConditionEnum.ReportConditionEqual, reportTreeNode.reportConditionEnumFieldList[0].ReportCondition);

                reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_File_File_Purpose");

                reportTreeNode.reportConditionEnumFieldList.Add(new ReportConditionEnumField());

                strList = new List<string>() { "Root_File_File_Purpose", "EQUALNot", "MikeInput" };
                LineCount = 2;

                retStr = reportBaseService.SetReportTreeNodeEnumCondition(StartItem, reportTreeNode.reportConditionEnumFieldList[0], reportTreeNode, strList, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, StartItem + 1, "EQUAL"), retStr);
                Assert.AreEqual(ReportConditionEnum.Error, reportTreeNode.reportConditionEnumFieldList[0].ReportCondition);
            }
        }
        [TestMethod]
        public void ReportBaseService_SetReportTreeNodeFormating_dbFormating_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                int StartItem = 1;
                ReportTreeNode reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_Lat");
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.NumberWithDecimal;

                List<string> strList = new List<string>() { "Root_Lat", "FORMAT", "F2" };
                int LineCount = 2;

                string retStr = reportBaseService.SetReportTreeNodeFormating(StartItem, reportTreeNode.dbFormatingField, reportTreeNode, strList, LineCount);
                Assert.AreEqual("", retStr);

                reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_Lat");

                strList = new List<string>() { "Root_Lat", "EQUALNot", "F2" };
                LineCount = 2;

                retStr = reportBaseService.SetReportTreeNodeFormating(StartItem, reportTreeNode.dbFormatingField, reportTreeNode, strList, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, StartItem + 1, "FORMAT"), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_SetReportTreeNodeFormating_reportFormating_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                int StartItem = 1;
                ReportTreeNode reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_Lat");
                reportTreeNode.ReportFieldType = ReportFieldTypeEnum.NumberWithDecimal;

                List<string> strList = new List<string>() { "Root_Lat", "FORMAT", "F2" };
                int LineCount = 2;

                string retStr = reportBaseService.SetReportTreeNodeFormating(StartItem, reportTreeNode.reportFormatingField, reportTreeNode, strList, LineCount);
                Assert.AreEqual("", retStr);

                reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_Lat");

                strList = new List<string>() { "Root_Lat", "EQUALNot", "F2" };
                LineCount = 2;

                retStr = reportBaseService.SetReportTreeNodeFormating(StartItem, reportTreeNode.reportFormatingField, reportTreeNode, strList, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, StartItem + 1, "FORMAT"), retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_SetReportTreeNodeNumberCondition_dbFiltering_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                int StartItem = 1;
                ReportTreeNode reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_Name");

                string FieldName = "Root_Name";
                List<string> strList = new List<string>();
                string retStr = "";
                int LineCount = 2;
                int Number = 34;

                foreach (string Condition1 in reportBaseService.AllowableBasicFilters)
                {
                    reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_Name");

                    reportTreeNode.dbFilteringNumberFieldList.Add(new ReportConditionNumberField());
                    reportTreeNode.dbFilteringNumberFieldList.Add(new ReportConditionNumberField());

                    strList = new List<string>() { FieldName, Condition1 };
                    retStr = reportBaseService.SetReportTreeNodeNumberCondition(StartItem, reportTreeNode.dbFilteringNumberFieldList[0], reportTreeNode, strList, LineCount);
                    Assert.AreEqual(string.Format(ReportServiceRes.Condition_MissingValue, strList[StartItem]), retStr);
                    Assert.AreEqual(ReportConditionEnum.Error, reportTreeNode.dbFilteringNumberFieldList[0].ReportCondition);
                }

                for (int i = 0; i < 2; i++)
                {
                    foreach (string Condition1 in reportBaseService.AllowableBasicFilters)
                    {
                        reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_Name");

                        reportTreeNode.dbFilteringNumberFieldList.Add(new ReportConditionNumberField());
                        reportTreeNode.dbFilteringNumberFieldList.Add(new ReportConditionNumberField());

                        if (i == 0)
                        {
                            strList = new List<string>() { FieldName, Condition1, Number.ToString() };
                            retStr = reportBaseService.SetReportTreeNodeNumberCondition(StartItem, reportTreeNode.dbFilteringNumberFieldList[0], reportTreeNode, strList, LineCount);
                            Assert.AreEqual("", retStr);
                        }
                        else if (i == 1)
                        {
                            strList = new List<string>() { FieldName, Condition1, Number.ToString() };
                            strList.Add(Condition1);
                            strList.Add((Number + 10).ToString());
                            retStr = reportBaseService.SetReportTreeNodeNumberCondition(StartItem, reportTreeNode.dbFilteringNumberFieldList[0], reportTreeNode, strList, LineCount);
                            Assert.AreEqual("", retStr);
                                retStr = reportBaseService.SetReportTreeNodeNumberCondition(StartItem + 2, reportTreeNode.dbFilteringNumberFieldList[1], reportTreeNode, strList, LineCount);
                            Assert.AreEqual("", retStr);
                        }

                        if (i >= 0)
                        {
                            switch (strList[StartItem + 1])
                            {
                                case "EQUAL":
                                    Assert.AreEqual(ReportConditionEnum.ReportConditionEqual, reportTreeNode.dbFilteringNumberFieldList[0].ReportCondition);
                                    break;
                                case "BIGGER":
                                    Assert.AreEqual(ReportConditionEnum.ReportConditionBigger, reportTreeNode.dbFilteringNumberFieldList[0].ReportCondition);
                                    break;
                                case "SMALLER":
                                    Assert.AreEqual(ReportConditionEnum.ReportConditionSmaller, reportTreeNode.dbFilteringNumberFieldList[0].ReportCondition);
                                    break;
                                default:
                                    break;
                            }
                            Assert.AreEqual(Number, reportTreeNode.dbFilteringNumberFieldList[0].NumberCondition);
                        }
                        if (i >= 1)
                        {
                            switch (strList[StartItem + 3])
                            {
                                case "EQUAL":
                                    Assert.AreEqual(ReportConditionEnum.ReportConditionEqual, reportTreeNode.dbFilteringNumberFieldList[1].ReportCondition);
                                    break;
                                case "BIGGER":
                                    Assert.AreEqual(ReportConditionEnum.ReportConditionBigger, reportTreeNode.dbFilteringNumberFieldList[1].ReportCondition);
                                    break;
                                case "SMALLER":
                                    Assert.AreEqual(ReportConditionEnum.ReportConditionSmaller, reportTreeNode.dbFilteringNumberFieldList[1].ReportCondition);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
        }
        [TestMethod]
        public void ReportBaseService_SetReportTreeNodeNumberCondition_reportCondition_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                int StartItem = 1;
                ReportTreeNode reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_Name");

                string FieldName = "Root_Name";
                List<string> strList = new List<string>();
                string retStr = "";
                int LineCount = 2;
                int Number1 = 34;
                int Number2 = 44;

                foreach (string Condition1 in reportBaseService.AllowableBasicFilters)
                {
                    reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_Name");

                    reportTreeNode.reportConditionNumberFieldList.Add(new ReportConditionNumberField());
                    reportTreeNode.reportConditionNumberFieldList.Add(new ReportConditionNumberField());

                    strList = new List<string>() { FieldName, Condition1 };
                    retStr = reportBaseService.SetReportTreeNodeNumberCondition(StartItem, reportTreeNode.reportConditionNumberFieldList[0], reportTreeNode, strList, LineCount);
                    Assert.AreEqual(string.Format(ReportServiceRes.Condition_MissingValue, strList[StartItem]), retStr);
                    Assert.AreEqual(ReportConditionEnum.Error, reportTreeNode.reportConditionNumberFieldList[0].ReportCondition);
                }

                for (int i = 0; i < 2; i++)
                {
                    foreach (string Condition1 in reportBaseService.AllowableBasicFilters)
                    {
                        reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_Name");

                        reportTreeNode.reportConditionNumberFieldList.Add(new ReportConditionNumberField());
                        reportTreeNode.reportConditionNumberFieldList.Add(new ReportConditionNumberField());

                        if (i == 0)
                        {
                            strList = new List<string>() { FieldName, Condition1, Number1.ToString() };
                            retStr = reportBaseService.SetReportTreeNodeNumberCondition(StartItem, reportTreeNode.reportConditionNumberFieldList[0], reportTreeNode, strList, LineCount);
                            Assert.AreEqual("", retStr);
                        }
                        else if (i == 1)
                        {
                            strList = new List<string>() { FieldName, Condition1, Number1.ToString() };
                            strList.Add(Condition1);
                            strList.Add((Number1 + 10).ToString());
                            retStr = reportBaseService.SetReportTreeNodeNumberCondition(StartItem, reportTreeNode.reportConditionNumberFieldList[0], reportTreeNode, strList, LineCount);
                            Assert.AreEqual("", retStr);
                                retStr = reportBaseService.SetReportTreeNodeNumberCondition(StartItem + 2, reportTreeNode.reportConditionNumberFieldList[1], reportTreeNode, strList, LineCount);
                            Assert.AreEqual("", retStr);
                        }

                        if (i >= 0)
                        {
                            switch (strList[StartItem + 1])
                            {
                                case "EQUAL":
                                    Assert.AreEqual(ReportConditionEnum.ReportConditionEqual, reportTreeNode.reportConditionNumberFieldList[0].ReportCondition);
                                    break;
                                case "BIGGER":
                                    Assert.AreEqual(ReportConditionEnum.ReportConditionBigger, reportTreeNode.reportConditionNumberFieldList[0].ReportCondition);
                                    break;
                                case "SMALLER":
                                    Assert.AreEqual(ReportConditionEnum.ReportConditionSmaller, reportTreeNode.reportConditionNumberFieldList[0].ReportCondition);
                                    break;
                                default:
                                    break;
                            }
                            Assert.AreEqual(Number1, reportTreeNode.reportConditionNumberFieldList[0].NumberCondition);
                        }
                        if (i >= 1)
                        {
                            switch (strList[StartItem + 3])
                            {
                                case "EQUAL":
                                    Assert.AreEqual(ReportConditionEnum.ReportConditionEqual, reportTreeNode.reportConditionNumberFieldList[1].ReportCondition);
                                    break;
                                case "BIGGER":
                                    Assert.AreEqual(ReportConditionEnum.ReportConditionBigger, reportTreeNode.reportConditionNumberFieldList[1].ReportCondition);
                                    break;
                                case "SMALLER":
                                    Assert.AreEqual(ReportConditionEnum.ReportConditionSmaller, reportTreeNode.reportConditionNumberFieldList[1].ReportCondition);
                                    break;
                                default:
                                    break;
                            }
                            Assert.AreEqual(Number2, reportTreeNode.reportConditionNumberFieldList[1].NumberCondition);
                        }
                    }
                }
            }
        }
        [TestMethod]
        public void ReportBaseService_SetReportTreeNodeSorting_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                int StartItem = 1;
                ReportTreeNode reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_Is_Active");

                List<string> strList = new List<string>() { "Root_Is_Active", "ASCENDING", "1" };
                int LineCount = 2;

                string retStr = reportBaseService.SetReportTreeNodeSorting(StartItem, reportTreeNode.dbSortingField, reportTreeNode, strList, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(ReportSortingEnum.ReportSortingAscending, reportTreeNode.dbSortingField.ReportSorting);
                Assert.AreEqual(1, reportTreeNode.dbSortingField.Ordinal);

                reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_Is_Active");

                strList = new List<string>() { "Root_Is_Active", "DESCENDING", "1" };
                LineCount = 2;

                retStr = reportBaseService.SetReportTreeNodeSorting(StartItem, reportTreeNode.dbSortingField, reportTreeNode, strList, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(ReportSortingEnum.ReportSortingDescending, reportTreeNode.dbSortingField.ReportSorting);
                Assert.AreEqual(1, reportTreeNode.dbSortingField.Ordinal);

                reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_Is_Active");

                strList = new List<string>() { "Root_Is_Active", "NotWoking", "1" };
                LineCount = 2;

                retStr = reportBaseService.SetReportTreeNodeSorting(StartItem, reportTreeNode.dbSortingField, reportTreeNode, strList, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, StartItem + 1, "ASCENDING, DESCENDING"), retStr);
                Assert.AreEqual(ReportSortingEnum.Error, reportTreeNode.dbSortingField.ReportSorting);
                Assert.AreEqual(0, reportTreeNode.dbSortingField.Ordinal);

                reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_Is_Active");

                strList = new List<string>() { "Root_Is_Active" };
                StartItem = 1;

                retStr = reportBaseService.SetReportTreeNodeSorting(StartItem, reportTreeNode.dbSortingField, reportTreeNode, strList, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_ShouldHave_Items, LineCount, "more than " + (StartItem + 1)), retStr);
                Assert.AreEqual(ReportSortingEnum.Error, reportTreeNode.dbSortingField.ReportSorting);
                Assert.AreEqual(0, reportTreeNode.dbSortingField.Ordinal);

                strList = new List<string>() { "Root_Is_Active", "DESCENDING" };
                StartItem = 1;

                retStr = reportBaseService.SetReportTreeNodeSorting(StartItem, reportTreeNode.dbSortingField, reportTreeNode, strList, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_ShouldHave_Items, LineCount, "more than " + (StartItem + 1)), retStr);
                Assert.AreEqual(ReportSortingEnum.Error, reportTreeNode.dbSortingField.ReportSorting);
                Assert.AreEqual(0, reportTreeNode.dbSortingField.Ordinal);
            }
        }
        [TestMethod]
        public void ReportBaseService_SetReportTreeNodeTextCondition_dbFilteringText_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                int StartItem = 1;
                ReportTreeNode reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_Name");

                string FieldName = "Root_Name";
                List<string> strList = new List<string>();
                string retStr = "";
                int LineCount = 2;
                string Text = "Allo";

                strList = new List<string>() { FieldName, "CONTAIN" };
                reportTreeNode.dbFilteringTextFieldList.Add(new ReportConditionTextField());
                reportTreeNode.dbFilteringTextFieldList.Add(new ReportConditionTextField());
                reportTreeNode.dbFilteringTextFieldList.Add(new ReportConditionTextField());
                retStr = reportBaseService.SetReportTreeNodeTextCondition(StartItem, reportTreeNode.dbFilteringTextFieldList[0], reportTreeNode, strList, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Condition_MissingValue, strList[StartItem]), retStr);
                Assert.AreEqual(ReportConditionEnum.Error, reportTreeNode.dbFilteringTextFieldList[0].ReportCondition);

                strList = new List<string>() { FieldName, "NotWorking", Text };
                retStr = reportBaseService.SetReportTreeNodeTextCondition(StartItem, reportTreeNode.dbFilteringTextFieldList[0], reportTreeNode, strList, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, StartItem + 1, String.Join(",", reportBaseService.AllowableTextFilters)), retStr);
                Assert.AreEqual(ReportConditionEnum.Error, reportTreeNode.dbFilteringTextFieldList[0].ReportCondition);

                strList = new List<string>() { FieldName, "EQUAL", "All" };
                retStr = reportBaseService.SetReportTreeNodeTextCondition(StartItem, reportTreeNode.dbFilteringTextFieldList[0], reportTreeNode, strList, LineCount);
                Assert.AreEqual("", retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_SetReportTreeNodeTextCondition_reportConditionText_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                int StartItem = 1;
                ReportTreeNode reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_Name");

                string FieldName = "Root_Name";
                List<string> strList = new List<string>();
                string retStr = "";
                int LineCount = 2;
                string Text = "Allo";

                strList = new List<string>() { FieldName, "CONTAIN" };
                reportTreeNode.reportConditionTextFieldList.Add(new ReportConditionTextField());
                reportTreeNode.reportConditionTextFieldList.Add(new ReportConditionTextField());
                reportTreeNode.reportConditionTextFieldList.Add(new ReportConditionTextField());
                retStr = reportBaseService.SetReportTreeNodeTextCondition(StartItem, reportTreeNode.reportConditionTextFieldList[0], reportTreeNode, strList, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Condition_MissingValue, strList[StartItem]), retStr);
                Assert.AreEqual(ReportConditionEnum.Error, reportTreeNode.reportConditionTextFieldList[0].ReportCondition);

                strList = new List<string>() { FieldName, "NotWorking", Text };
                retStr = reportBaseService.SetReportTreeNodeTextCondition(StartItem, reportTreeNode.reportConditionTextFieldList[0], reportTreeNode, strList, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, StartItem + 1, String.Join(",", reportBaseService.AllowableTextFilters)), retStr);
                Assert.AreEqual(ReportConditionEnum.Error, reportTreeNode.reportConditionTextFieldList[0].ReportCondition);

                strList = new List<string>() { FieldName, "EQUAL", "All" };
                retStr = reportBaseService.SetReportTreeNodeTextCondition(StartItem, reportTreeNode.reportConditionTextFieldList[0], reportTreeNode, strList, LineCount);
                Assert.AreEqual("", retStr);
            }
        }
        [TestMethod]
        public void ReportBaseService_SetReportTreeNodeTrueFalse_dbFilteringTrueFalseField_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                int StartItem = 1;
                ReportTreeNode reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_Is_Active");

                List<string> strList = new List<string>() { "Root_Is_Active", "TRUE" };
                int LineCount = 2;

                reportTreeNode.dbFilteringTrueFalseFieldList.Add(new ReportConditionTrueFalseField());

                string retStr = reportBaseService.SetReportTreeNodeTrueFalse(StartItem, reportTreeNode.dbFilteringTrueFalseFieldList[0], reportTreeNode, strList, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(ReportConditionEnum.ReportConditionTrue, reportTreeNode.dbFilteringTrueFalseFieldList[0].ReportCondition);

                reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_Is_Active");

                strList = new List<string>() { "Root_Is_Active", "FALSE" };
                LineCount = 2;

                reportTreeNode.dbFilteringTrueFalseFieldList.Clear();
                reportTreeNode.dbFilteringTrueFalseFieldList.Add(new ReportConditionTrueFalseField());

                retStr = reportBaseService.SetReportTreeNodeTrueFalse(StartItem, reportTreeNode.dbFilteringTrueFalseFieldList[0], reportTreeNode, strList, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(ReportConditionEnum.ReportConditionFalse, reportTreeNode.dbFilteringTrueFalseFieldList[0].ReportCondition);

                reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_Is_Active");

                strList = new List<string>() { "Root_Is_Active", "NotWoking" };
                LineCount = 2;

                reportTreeNode.dbFilteringTrueFalseFieldList.Clear();
                reportTreeNode.dbFilteringTrueFalseFieldList.Add(new ReportConditionTrueFalseField());

                retStr = reportBaseService.SetReportTreeNodeTrueFalse(StartItem, reportTreeNode.dbFilteringTrueFalseFieldList[0], reportTreeNode, strList, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, StartItem + 1, "TRUE,FALSE"), retStr);
                Assert.AreEqual(ReportConditionEnum.Error, reportTreeNode.dbFilteringTrueFalseFieldList[0].ReportCondition);

                reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_Is_Active");

                strList = new List<string>() { "Root_Is_Active" };
                LineCount = 2;

                reportTreeNode.dbFilteringTrueFalseFieldList.Clear();
                reportTreeNode.dbFilteringTrueFalseFieldList.Add(new ReportConditionTrueFalseField());

                retStr = reportBaseService.SetReportTreeNodeTrueFalse(StartItem, reportTreeNode.dbFilteringTrueFalseFieldList[0], reportTreeNode, strList, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_ShouldHave_Items, LineCount, "more than " + StartItem), retStr);
                Assert.AreEqual(ReportConditionEnum.Error, reportTreeNode.dbFilteringTrueFalseFieldList[0].ReportCondition);

            }
        }
        [TestMethod]
        public void ReportBaseService_SetReportTreeNodeTrueFalse_reportConditionTrueFalseField_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                int StartItem = 1;
                ReportTreeNode reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_Is_Active");

                List<string> strList = new List<string>() { "Root_Is_Active", "TRUE" };
                int LineCount = 2;

                reportTreeNode.reportConditionTrueFalseFieldList.Add(new ReportConditionTrueFalseField());

                string retStr = reportBaseService.SetReportTreeNodeTrueFalse(StartItem, reportTreeNode.reportConditionTrueFalseFieldList[0], reportTreeNode, strList, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(ReportConditionEnum.ReportConditionTrue, reportTreeNode.reportConditionTrueFalseFieldList[0].ReportCondition);

                reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_Is_Active");

                strList = new List<string>() { "Root_Is_Active", "FALSE" };
                LineCount = 2;

                reportTreeNode.reportConditionTrueFalseFieldList.Add(new ReportConditionTrueFalseField());

                retStr = reportBaseService.SetReportTreeNodeTrueFalse(StartItem, reportTreeNode.reportConditionTrueFalseFieldList[0], reportTreeNode, strList, LineCount);
                Assert.AreEqual("", retStr);
                Assert.AreEqual(ReportConditionEnum.ReportConditionFalse, reportTreeNode.reportConditionTrueFalseFieldList[0].ReportCondition);

                reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_Is_Active");

                strList = new List<string>() { "Root_Is_Active", "NotWoking" };
                LineCount = 2;

                reportTreeNode.reportConditionTrueFalseFieldList.Add(new ReportConditionTrueFalseField());

                retStr = reportBaseService.SetReportTreeNodeTrueFalse(StartItem, reportTreeNode.reportConditionTrueFalseFieldList[0], reportTreeNode, strList, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, StartItem + 1, "TRUE,FALSE"), retStr);
                Assert.AreEqual(ReportConditionEnum.Error, reportTreeNode.reportConditionTrueFalseFieldList[0].ReportCondition);

                reportTreeNode = reportBaseService.CreateReportTreeNodeItem("Root_Is_Active");

                strList = new List<string>() { "Root_Is_Active" };
                LineCount = 2;

                reportTreeNode.reportConditionTrueFalseFieldList.Add(new ReportConditionTrueFalseField());

                retStr = reportBaseService.SetReportTreeNodeTrueFalse(StartItem, reportTreeNode.reportConditionTrueFalseFieldList[0], reportTreeNode, strList, LineCount);
                Assert.AreEqual(string.Format(ReportServiceRes.Line_ShouldHave_Items, LineCount, "more than " + StartItem), retStr);
                Assert.AreEqual(ReportConditionEnum.Error, reportTreeNode.reportConditionTrueFalseFieldList[0].ReportCondition);

            }
        }
        [TestMethod]
        public void ReportBaseService_StringIsANumber_Test()
        {
            foreach (CultureInfo culture in setupData.cultureListGood)
            {
                SetupTest(culture);

                float? retFloat = reportBaseService.StringIsANumber("23");
                Assert.AreEqual(23, retFloat);

                retFloat = reportBaseService.StringIsANumber("23a");
                Assert.IsNull(retFloat);

                retFloat = reportBaseService.StringIsANumber("23a");
                Assert.IsNull(retFloat);

                retFloat = reportBaseService.StringIsANumber("23 a");
                Assert.IsNull(retFloat);

                retFloat = reportBaseService.StringIsANumber("23 23a");
                Assert.IsNull(retFloat);

                retFloat = reportBaseService.StringIsANumber("23.34a");
                Assert.IsNull(retFloat);

                retFloat = reportBaseService.StringIsANumber((culture.TwoLetterISOLanguageName == "fr" ? "23,45" : "23.45"));
                Assert.AreEqual(23.45f, retFloat);
            }
        }
        #endregion Testing Methods public

        #region Functions
        public void SetupTest(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            treeViewCSSP = new TreeView();
            reportTreeNodeRoot = new ReportTreeNode();

            IPrincipal user = new GenericPrincipal(new GenericIdentity("charles.leblanc2@canada.ca", "Forms"), null);

            reportBaseService = new ReportBaseService((culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en), treeViewCSSP);
            baseEnumService = new BaseEnumService((culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en));
            tvItemService = new TVItemService((culture.TwoLetterISOLanguageName == "fr" ? LanguageEnum.fr : LanguageEnum.en), user);
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
        private NameValueCollection CreateNameValueCollection(CultureInfo culture, StringBuilder sbCommand, string TagItem, int UnderTVItemID, string ParentTagItem, bool CountOnly, int Take)
        {
            NameValueCollection nameValueCollection = new NameValueCollection();
            nameValueCollection["Language"] = culture.TwoLetterISOLanguageName;
            nameValueCollection["Command"] = sbCommand.ToString();
            nameValueCollection["Name"] = "GetReport" + TagItem + "ModelListUnderTVItemIDJSON";
            nameValueCollection["UnderTVItemID"] = UnderTVItemID.ToString();
            nameValueCollection["ParentTagItem"] = ParentTagItem;
            nameValueCollection["CountOnly"] = CountOnly.ToString();
            nameValueCollection["Take"] = Take.ToString();

            return nameValueCollection;
        }
        #endregion Functions

    }
}
