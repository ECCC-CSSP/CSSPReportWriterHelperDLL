using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;
using CSSPReportWriterHelperDLL.Services;
using CSSPReportWriterHelperDLL.Services.Resources;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;
using CSSPModelsDLL.Models;
using CSSPEnumsDLL.Enums;
using System.Drawing;
using System.Collections.Specialized;
using System.Reflection;
using System.IO;
using System.Dynamic;
using CSSPWebToolsDBDLL.Services;
using CSSPEnumsDLL.Services;
using System.Security.Principal;

namespace CSSPReportWriterHelperDLL.Services
{
    public partial class ReportBaseService
    {
        #region Variables
        public string BaseURLEN = "http://wmon01dtchlebl2/csspwebtools/en-CA/Report/";
        public string BaseURLFR = "http://wmon01dtchlebl2/csspwebtools/fr-CA/Report/";
        //public string BaseURLEN = "http://localhost:11562/en-CA/Report/";
        //public string BaseURLFR = "http://localhost:11562/fr-CA/Report/";
        public List<string> AllowableBasicFilters = new List<string>() { "EQUAL", "BIGGER", "SMALLER" };
        public List<string> AllowableDateVariables = new List<string>() { "YEAR", "MONTH", "DAY", "HOUR", "MINUTE" };
        public List<string> AllowableEnumFilters = new List<string>() { "EQUAL" };
        public List<string> AllowableFormatingFilters = new List<string>() { "FORMAT" };
        public List<string> AllowableSortingFilters = new List<string>() { "ASCENDING", "DESCENDING" };
        public List<string> AllowableTextFilters = new List<string>() { "EQUAL", "BIGGER", "SMALLER", "CONTAIN", "START", "END" };
        public List<string> AllowableTrueFalseFilters = new List<string>() { "TRUE", "FALSE" };
        public string Marker = "|||";
        public string LastHref = "";
        public string LastCSSPTVText = "";
        public int Count = 0;
        public ReportFileTypeEnum ReportFileType = ReportFileTypeEnum.Error;
        //public ReportService _ReportService = null;
        public List<string> AllowableLanguages = new List<string>() { "en", "fr" };
        public IPrincipal _User = null;
        private List<Node> InterpolatedContourNodeList = new List<Node>();
        private Dictionary<String, Vector> ForwardVector = new Dictionary<String, Vector>();
        private Dictionary<String, Vector> BackwardVector = new Dictionary<String, Vector>();
        #endregion Variables

        #region Properties
        public LanguageEnum LanguageRequest { get; set; }
        public TreeView _TreeViewCSSP { get; set; }
        public ReportTreeNode _ReportTreeNodeRoot { get; set; }
        public List<string> _TreeViewTextList { get; set; }
        public CreateTreeViewService _CreateTreeViewService { get; set; }
        public ReportBase _ReportBase { get; set; }
        public BaseEnumService _BaseEnumService { get; set; }
        #endregion Properties

        #region Constructors
        public ReportBaseService(LanguageEnum LanguageRequest, TreeView treeViewCSSP)
        {
            this.LanguageRequest = LanguageRequest;
            Thread.CurrentThread.CurrentCulture = new CultureInfo(LanguageRequest + "-CA");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageRequest + "-CA");

            this._TreeViewCSSP = treeViewCSSP;
            this._TreeViewTextList = new List<string>();

            this._CreateTreeViewService = new CreateTreeViewService();

            this._ReportTreeNodeRoot = this._CreateTreeViewService.CreateReportTreeNodeItem("Root", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.TableSelectable, ReportFieldTypeEnum.Error);
            this._ReportTreeNodeRoot.ForeColor = Color.Green;
            this._TreeViewCSSP.Nodes.Add(_ReportTreeNodeRoot);
            this.LoadRecursiveTreeNode(_ReportTreeNodeRoot);
            this._ReportBase = new ReportBase();
            this._BaseEnumService = new BaseEnumService(LanguageRequest);
        }
        public ReportBaseService(LanguageEnum LanguageRequest, TreeView treeViewCSSP, IPrincipal User)
        {
            _User = User;
            this.LanguageRequest = LanguageRequest;
            Thread.CurrentThread.CurrentCulture = new CultureInfo(LanguageRequest + "-CA");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageRequest + "-CA");

            _TreeViewCSSP = treeViewCSSP;
            _TreeViewTextList = new List<string>();

            _CreateTreeViewService = new CreateTreeViewService();

            _ReportTreeNodeRoot = _CreateTreeViewService.CreateReportTreeNodeItem("Root", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.TableSelectable, ReportFieldTypeEnum.Error);
            _ReportTreeNodeRoot.ForeColor = Color.Green;
            _TreeViewCSSP.Nodes.Add(_ReportTreeNodeRoot);
            LoadRecursiveTreeNode(_ReportTreeNodeRoot);
            _ReportBase = new ReportBase();
            _BaseEnumService = new BaseEnumService(LanguageRequest);
        }
        #endregion Constructors

        #region Events
        #endregion Events

        #region Generic Functions
        public string CheckFilterOfEnum(string FilterStr, ReportFieldTypeEnum reportFieldType)
        {
            List<string> FilterStrList = FilterStr.Split("*".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (string filter in FilterStrList)
            {
                bool Found = false;
                Type type = null;
                switch (reportFieldType)
                {
                    case ReportFieldTypeEnum.FilePurpose:
                        type = typeof(FilePurposeEnum);
                        break;
                    case ReportFieldTypeEnum.FileType:
                        type = typeof(FileTypeEnum);
                        break;
                    case ReportFieldTypeEnum.TranslationStatus:
                        type = typeof(TranslationStatusEnum);
                        break;
                    case ReportFieldTypeEnum.BoxModelResultType:
                        type = typeof(BoxModelResultTypeEnum);
                        break;
                    case ReportFieldTypeEnum.InfrastructureType:
                        type = typeof(InfrastructureTypeEnum);
                        break;
                    case ReportFieldTypeEnum.FacilityType:
                        type = typeof(FacilityTypeEnum);
                        break;
                    case ReportFieldTypeEnum.AerationType:
                        type = typeof(AerationTypeEnum);
                        break;
                    case ReportFieldTypeEnum.PreliminaryTreatmentType:
                        type = typeof(PreliminaryTreatmentTypeEnum);
                        break;
                    case ReportFieldTypeEnum.PrimaryTreatmentType:
                        type = typeof(PrimaryTreatmentTypeEnum);
                        break;
                    case ReportFieldTypeEnum.SecondaryTreatmentType:
                        type = typeof(SecondaryTreatmentTypeEnum);
                        break;
                    case ReportFieldTypeEnum.TertiaryTreatmentType:
                        type = typeof(TertiaryTreatmentTypeEnum);
                        break;
                    case ReportFieldTypeEnum.TreatmentType:
                        type = typeof(TreatmentTypeEnum);
                        break;
                    case ReportFieldTypeEnum.DisinfectionType:
                        type = typeof(DisinfectionTypeEnum);
                        break;
                    case ReportFieldTypeEnum.CollectionSystemType:
                        type = typeof(CollectionSystemTypeEnum);
                        break;
                    case ReportFieldTypeEnum.AlarmSystemType:
                        type = typeof(AlarmSystemTypeEnum);
                        break;
                    case ReportFieldTypeEnum.ScenarioStatus:
                        type = typeof(ScenarioStatusEnum);
                        break;
                    case ReportFieldTypeEnum.StorageDataType:
                        type = typeof(StorageDataTypeEnum);
                        break;
                    case ReportFieldTypeEnum.Language:
                        type = typeof(LanguageEnum);
                        break;
                    case ReportFieldTypeEnum.SampleType:
                        type = typeof(SampleTypeEnum);
                        break;
                    case ReportFieldTypeEnum.BeaufortScale:
                        type = typeof(BeaufortScaleEnum);
                        break;
                    case ReportFieldTypeEnum.AnalyzeMethod:
                        type = typeof(AnalyzeMethodEnum);
                        break;
                    case ReportFieldTypeEnum.SampleMatrix:
                        type = typeof(SampleMatrixEnum);
                        break;
                    case ReportFieldTypeEnum.Laboratory:
                        type = typeof(LaboratoryEnum);
                        break;
                    case ReportFieldTypeEnum.SampleStatus:
                        type = typeof(SampleStatusEnum);
                        break;
                    case ReportFieldTypeEnum.SamplingPlanType:
                        type = typeof(SamplingPlanTypeEnum);
                        break;
                    case ReportFieldTypeEnum.LabSheetSampleType:
                        type = typeof(SampleTypeEnum);
                        break;
                    case ReportFieldTypeEnum.LabSheetType:
                        type = typeof(LabSheetTypeEnum);
                        break;
                    case ReportFieldTypeEnum.LabSheetStatus:
                        type = typeof(LabSheetStatusEnum);
                        break;
                    case ReportFieldTypeEnum.PolSourceInactiveReason:
                        type = typeof(PolSourceInactiveReasonEnum);
                        break;
                    case ReportFieldTypeEnum.PolSourceObsInfo:
                        type = typeof(PolSourceObsInfoEnum);
                        break;
                    case ReportFieldTypeEnum.AddressType:
                        type = typeof(AddressTypeEnum);
                        break;
                    case ReportFieldTypeEnum.StreetType:
                        type = typeof(StreetTypeEnum);
                        break;
                    case ReportFieldTypeEnum.ContactTitle:
                        type = typeof(ContactTitleEnum);
                        break;
                    case ReportFieldTypeEnum.EmailType:
                        type = typeof(EmailTypeEnum);
                        break;
                    case ReportFieldTypeEnum.TelType:
                        type = typeof(TelTypeEnum);
                        break;
                    case ReportFieldTypeEnum.TideText:
                        type = typeof(TideTextEnum);
                        break;
                    case ReportFieldTypeEnum.TideDataType:
                        type = typeof(TideDataTypeEnum);
                        break;
                    case ReportFieldTypeEnum.SpecialTableType:
                        type = typeof(SpecialTableTypeEnum);
                        break;
                    case ReportFieldTypeEnum.MWQMSiteLatestClassification:
                        type = typeof(MWQMSiteLatestClassificationEnum);
                        break;
                    case ReportFieldTypeEnum.PolSourceIssueRisk:
                        type = typeof(PolSourceIssueRiskEnum);
                        break;
                    case ReportFieldTypeEnum.MikeScenarioSpecialResultKMLType:
                        type = typeof(MikeScenarioSpecialResultKMLTypeEnum);
                        break;
                    default:
                        break;
                }

                foreach (string enumStr in Enum.GetNames(type))
                {
                    if (filter == enumStr)
                    {
                        Found = true;
                        break;
                    }
                }

                if (!Found)
                    return string.Format(ReportServiceRes.Enum_NotFoundShouldBeOneOf_, filter, String.Join(",", Enum.GetNames(type)));
            }

            return "";
        }
        public T DownloadSerializedJsonData<T>(NameValueCollection nameValueCollection) where T : new()
        {
            using (WebClient webClient = new WebClient() { Encoding = Encoding.UTF8 })
            {
                WebProxy webProxy = new WebProxy();
                webClient.Proxy = webProxy;

                webClient.UseDefaultCredentials = true;

                var json_data = string.Empty;
                // attempt to download JSON data as a string
                try
                {
                    string url = (nameValueCollection["Language"] == "fr" ? BaseURLFR : BaseURLEN) + nameValueCollection["Name"];
                    byte[] responseBytes = webClient.UploadValues(url, "POST", nameValueCollection);
                    json_data = Encoding.UTF8.GetString(responseBytes);
                }
                catch (Exception ex)
                {
                    nameValueCollection["Error"] = ex.Message + (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : "");
                    return new T();
                }
                // if string with JSON data is not empty, deserialize it to class and return its instance 
                JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
                {
                    DateTimeZoneHandling = DateTimeZoneHandling.Local
                };
                return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data, jsonSerializerSettings) : new T();
            }
        }
        public dynamic GetDataDirectlyFromDB(string ReportTypeName, LanguageEnum Language, string Command, int TVItemID, string ParentTagItem, bool CountOnly, int Take)
        {
            switch (ReportTypeName)
            {
                case "ReportRootModel":
                    {
                        ReportServiceRoot reportServiceRoot = new ReportServiceRoot(Language, _User);
                        return (dynamic)reportServiceRoot.GetReportRootModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportCountryModel":
                    {
                        ReportServiceCountry reportServiceCountry = new ReportServiceCountry(Language, _User);
                        return (dynamic)reportServiceCountry.GetReportCountryModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportProvinceModel":
                    {
                        ReportServiceProvince reportServiceProvince = new ReportServiceProvince(Language, _User);
                        return (dynamic)reportServiceProvince.GetReportProvinceModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportAreaModel":
                    {
                        ReportServiceArea reportServiceArea = new ReportServiceArea(Language, _User);
                        return (dynamic)reportServiceArea.GetReportAreaModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportSectorModel":
                    {
                        ReportServiceSector reportServiceSector = new ReportServiceSector(Language, _User);
                        return (dynamic)reportServiceSector.GetReportSectorModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportSubsectorModel":
                    {
                        ReportServiceSubsector reportServiceSubsector = new ReportServiceSubsector(Language, _User);
                        return (dynamic)reportServiceSubsector.GetReportSubsectorModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportMWQM_SiteModel":
                    {
                        ReportServiceMWQM_Site reportServiceMWQM_Site = new ReportServiceMWQM_Site(Language, _User);
                        return (dynamic)reportServiceMWQM_Site.GetReportMWQM_SiteModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportMWQM_Site_SampleModel":
                    {
                        ReportServiceMWQM_Site_Sample reportServiceMWQM_Site_Sample = new ReportServiceMWQM_Site_Sample(Language, _User);
                        return (dynamic)reportServiceMWQM_Site_Sample.GetReportMWQM_Site_SampleModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportMWQM_Site_Start_And_End_DateModel":
                    {
                        ReportServiceMWQM_Site_Start_And_End_Date reportServiceMWQM_Site_Start_And_End_Date = new ReportServiceMWQM_Site_Start_And_End_Date(Language, _User);
                        return (dynamic)reportServiceMWQM_Site_Start_And_End_Date.GetReportMWQM_Site_Start_And_End_DateModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportMWQM_Site_FileModel":
                    {
                        ReportServiceMWQM_Site_File reportServiceMWQM_Site_File = new ReportServiceMWQM_Site_File(Language, _User);
                        return (dynamic)reportServiceMWQM_Site_File.GetReportMWQM_Site_FileModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportMWQM_RunModel":
                    {
                        ReportServiceMWQM_Run reportServiceMWQM_Run = new ReportServiceMWQM_Run(Language, _User);
                        return (dynamic)reportServiceMWQM_Run.GetReportMWQM_RunModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportMWQM_Run_SampleModel":
                    {
                        ReportServiceMWQM_Run_Sample reportServiceMWQM_Run_Sample = new ReportServiceMWQM_Run_Sample(Language, _User);
                        return (dynamic)reportServiceMWQM_Run_Sample.GetReportMWQM_Run_SampleModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportMWQM_Run_Lab_SheetModel":
                    {
                        ReportServiceMWQM_Run_Lab_Sheet reportServiceMWQM_Run_Lab_Sheet = new ReportServiceMWQM_Run_Lab_Sheet(Language, _User);
                        return (dynamic)reportServiceMWQM_Run_Lab_Sheet.GetReportMWQM_Run_Lab_SheetModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportMWQM_Run_Lab_Sheet_DetailModel":
                    {
                        ReportServiceMWQM_Run_Lab_Sheet_Detail reportServiceMWQM_Run_Lab_Sheet_Detail = new ReportServiceMWQM_Run_Lab_Sheet_Detail(Language, _User);
                        return (dynamic)reportServiceMWQM_Run_Lab_Sheet_Detail.GetReportMWQM_Run_Lab_Sheet_DetailModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportMWQM_Run_Lab_Sheet_Tube_And_MPN_DetailModel":
                    {
                        ReportServiceMWQM_Run_Lab_Sheet_Tube_And_MPN_Detail reportServiceMWQM_Run_Lab_Sheet_Tube_And_MPN_Detail = new ReportServiceMWQM_Run_Lab_Sheet_Tube_And_MPN_Detail(Language, _User);
                        return (dynamic)reportServiceMWQM_Run_Lab_Sheet_Tube_And_MPN_Detail.GetReportMWQM_Run_Lab_Sheet_Tube_And_MPN_DetailModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportMWQM_Run_FileModel":
                    {
                        ReportServiceMWQM_Run_File reportServiceMWQM_Run_File = new ReportServiceMWQM_Run_File(Language, _User);
                        return (dynamic)reportServiceMWQM_Run_File.GetReportMWQM_Run_FileModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportPol_Source_SiteModel":
                    {
                        ReportServicePol_Source_Site reportServicePol_Source_Site = new ReportServicePol_Source_Site(Language, _User);
                        return (dynamic)reportServicePol_Source_Site.GetReportPol_Source_SiteModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportPol_Source_Site_ObsModel":
                    {
                        ReportServicePol_Source_Site_Obs reportServicePol_Source_Site_Obs = new ReportServicePol_Source_Site_Obs(Language, _User);
                        return (dynamic)reportServicePol_Source_Site_Obs.GetReportPol_Source_Site_ObsModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportPol_Source_Site_Obs_IssueModel":
                    {
                        ReportServicePol_Source_Site_Obs_Issue reportServicePol_Source_Site_Obs_Issue = new ReportServicePol_Source_Site_Obs_Issue(Language, _User);
                        return (dynamic)reportServicePol_Source_Site_Obs_Issue.GetReportPol_Source_Site_Obs_IssueModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportPol_Source_Site_FileModel":
                    {
                        ReportServicePol_Source_Site_File reportServicePol_Source_Site_File = new ReportServicePol_Source_Site_File(Language, _User);
                        return (dynamic)reportServicePol_Source_Site_File.GetReportPol_Source_Site_FileModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportPol_Source_Site_AddressModel":
                    {
                        ReportServicePol_Source_Site_Address reportServicePol_Source_Site_Address = new ReportServicePol_Source_Site_Address(Language, _User);
                        return (dynamic)reportServicePol_Source_Site_Address.GetReportPol_Source_Site_AddressModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportMunicipalityModel":
                    {
                        ReportServiceMunicipality reportServiceMunicipality = new ReportServiceMunicipality(Language, _User);
                        return (dynamic)reportServiceMunicipality.GetReportMunicipalityModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportInfrastructureModel":
                    {
                        ReportServiceInfrastructure reportServiceInfrastructure = new ReportServiceInfrastructure(Language, _User);
                        return (dynamic)reportServiceInfrastructure.GetReportInfrastructureModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportBox_ModelModel":
                    {
                        ReportServiceBox_Model reportServiceBox_Model = new ReportServiceBox_Model(Language, _User);
                        return (dynamic)reportServiceBox_Model.GetReportBox_ModelModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportBox_Model_ResultModel":
                    {
                        ReportServiceBox_Model_Result reportServiceBox_Model_Result = new ReportServiceBox_Model_Result(Language, _User);
                        return (dynamic)reportServiceBox_Model_Result.GetReportBox_Model_ResultModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportVisual_Plumes_ScenarioModel":
                    {
                        ReportServiceVisual_Plumes_Scenario reportServiceVisual_Plumes_Scenario = new ReportServiceVisual_Plumes_Scenario(Language, _User);
                        return (dynamic)reportServiceVisual_Plumes_Scenario.GetReportVisual_Plumes_ScenarioModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportVisual_Plumes_Scenario_AmbientModel":
                    {
                        ReportServiceVisual_Plumes_Scenario_Ambient reportServiceVisual_Plumes_Scenario_Ambient = new ReportServiceVisual_Plumes_Scenario_Ambient(Language, _User);
                        return (dynamic)reportServiceVisual_Plumes_Scenario_Ambient.GetReportVisual_Plumes_Scenario_AmbientModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportVisual_Plumes_Scenario_ResultModel":
                    {
                        ReportServiceVisual_Plumes_Scenario_Result reportServiceVisual_Plumes_Scenario_Result = new ReportServiceVisual_Plumes_Scenario_Result(Language, _User);
                        return (dynamic)reportServiceVisual_Plumes_Scenario_Result.GetReportVisual_Plumes_Scenario_ResultModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportInfrastructure_AddressModel":
                    {
                        ReportServiceInfrastructure_Address reportServiceInfrastructure_Address = new ReportServiceInfrastructure_Address(Language, _User);
                        return (dynamic)reportServiceInfrastructure_Address.GetReportInfrastructure_AddressModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportInfrastructure_FileModel":
                    {
                        ReportServiceInfrastructure_File reportServiceInfrastructure_File = new ReportServiceInfrastructure_File(Language, _User);
                        return (dynamic)reportServiceInfrastructure_File.GetReportInfrastructure_FileModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportMike_ScenarioModel":
                    {
                        ReportServiceMike_Scenario reportServiceMike_Scenario = new ReportServiceMike_Scenario(Language, _User);
                        return (dynamic)reportServiceMike_Scenario.GetReportMike_ScenarioModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportMike_SourceModel":
                    {
                        ReportServiceMike_Source reportServiceMike_Source = new ReportServiceMike_Source(Language, _User);
                        return (dynamic)reportServiceMike_Source.GetReportMike_SourceModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportMike_Source_Start_EndModel":
                    {
                        ReportServiceMike_Source_Start_End reportServiceMike_Source_Start_End = new ReportServiceMike_Source_Start_End(Language, _User);
                        return (dynamic)reportServiceMike_Source_Start_End.GetReportMike_Source_Start_EndModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportMike_Boundary_ConditionModel":
                    {
                        ReportServiceMike_Boundary_Condition reportServiceMike_Boundary_Condition = new ReportServiceMike_Boundary_Condition(Language, _User);
                        return (dynamic)reportServiceMike_Boundary_Condition.GetReportMike_Boundary_ConditionModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportMike_Scenario_FileModel":
                    {
                        ReportServiceMike_Scenario_File reportServiceMike_Scenario_File = new ReportServiceMike_Scenario_File(Language, _User);
                        return (dynamic)reportServiceMike_Scenario_File.GetReportMike_Scenario_FileModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportMunicipality_ContactModel":
                    {
                        ReportServiceMunicipality_Contact reportServiceMunicipality_Contact = new ReportServiceMunicipality_Contact(Language, _User);
                        return (dynamic)reportServiceMunicipality_Contact.GetReportMunicipality_ContactModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportMunicipality_Contact_AddressModel":
                    {
                        ReportServiceMunicipality_Contact_Address reportServiceMunicipality_Contact_Address = new ReportServiceMunicipality_Contact_Address(Language, _User);
                        return (dynamic)reportServiceMunicipality_Contact_Address.GetReportMunicipality_Contact_AddressModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportMunicipality_Contact_TelModel":
                    {
                        ReportServiceMunicipality_Contact_Tel reportServiceMunicipality_Contact_Tel = new ReportServiceMunicipality_Contact_Tel(Language, _User);
                        return (dynamic)reportServiceMunicipality_Contact_Tel.GetReportMunicipality_Contact_TelModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportMunicipality_Contact_EmailModel":
                    {
                        ReportServiceMunicipality_Contact_Email reportServiceMunicipality_Contact_Email = new ReportServiceMunicipality_Contact_Email(Language, _User);
                        return (dynamic)reportServiceMunicipality_Contact_Email.GetReportMunicipality_Contact_EmailModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportMunicipality_FileModel":
                    {
                        ReportServiceMunicipality_File reportServiceMunicipality_File = new ReportServiceMunicipality_File(Language, _User);
                        return (dynamic)reportServiceMunicipality_File.GetReportMunicipality_FileModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportSubsector_Special_TableModel":
                    {
                        ReportServiceSubsector_Special_Table reportServiceSubsector_Special_Table = new ReportServiceSubsector_Special_Table(Language, _User);
                        return (dynamic)reportServiceSubsector_Special_Table.GetReportSubsector_Special_TableModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportSubsector_Climate_SiteModel":
                    {
                        ReportServiceSubsector_Climate_Site reportServiceSubsector_Climate_Site = new ReportServiceSubsector_Climate_Site(Language, _User);
                        return (dynamic)reportServiceSubsector_Climate_Site.GetReportSubsector_Climate_SiteModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportSubsector_Climate_Site_DataModel":
                    {
                        ReportServiceSubsector_Climate_Site_Data reportServiceSubsector_Climate_Site_Data = new ReportServiceSubsector_Climate_Site_Data(Language, _User);
                        return (dynamic)reportServiceSubsector_Climate_Site_Data.GetReportSubsector_Climate_Site_DataModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportSubsector_Hydrometric_SiteModel":
                    {
                        ReportServiceSubsector_Hydrometric_Site reportServiceSubsector_Hydrometric_Site = new ReportServiceSubsector_Hydrometric_Site(Language, _User);
                        return (dynamic)reportServiceSubsector_Hydrometric_Site.GetReportSubsector_Hydrometric_SiteModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportSubsector_Hydrometric_Site_DataModel":
                    {
                        ReportServiceSubsector_Hydrometric_Site_Data reportServiceSubsector_Hydrometric_Site_Data = new ReportServiceSubsector_Hydrometric_Site_Data(Language, _User);
                        return (dynamic)reportServiceSubsector_Hydrometric_Site_Data.GetReportSubsector_Hydrometric_Site_DataModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportSubsector_Hydrometric_Site_Rating_CurveModel":
                    {
                        ReportServiceSubsector_Hydrometric_Site_Rating_Curve reportServiceSubsector_Hydrometric_Site_Rating_Curve = new ReportServiceSubsector_Hydrometric_Site_Rating_Curve(Language, _User);
                        return (dynamic)reportServiceSubsector_Hydrometric_Site_Rating_Curve.GetReportSubsector_Hydrometric_Site_Rating_CurveModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportSubsector_Hydrometric_Site_Rating_Curve_ValueModel":
                    {
                        ReportServiceSubsector_Hydrometric_Site_Rating_Curve_Value reportServiceSubsector_Hydrometric_Site_Rating_Curve_Value = new ReportServiceSubsector_Hydrometric_Site_Rating_Curve_Value(Language, _User);
                        return (dynamic)reportServiceSubsector_Hydrometric_Site_Rating_Curve_Value.GetReportSubsector_Hydrometric_Site_Rating_Curve_ValueModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportSubsector_Tide_SiteModel":
                    {
                        ReportServiceSubsector_Tide_Site reportServiceSubsector_Tide_Site = new ReportServiceSubsector_Tide_Site(Language, _User);
                        return (dynamic)reportServiceSubsector_Tide_Site.GetReportSubsector_Tide_SiteModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportSubsector_Tide_Site_DataModel":
                    {
                        ReportServiceSubsector_Tide_Site_Data reportServiceSubsector_Tide_Site_Data = new ReportServiceSubsector_Tide_Site_Data(Language, _User);
                        return (dynamic)reportServiceSubsector_Tide_Site_Data.GetReportSubsector_Tide_Site_DataModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportSubsector_Lab_SheetModel":
                    {
                        ReportServiceSubsector_Lab_Sheet reportServiceSubsector_Lab_Sheet = new ReportServiceSubsector_Lab_Sheet(Language, _User);
                        return (dynamic)reportServiceSubsector_Lab_Sheet.GetReportSubsector_Lab_SheetModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportSubsector_Lab_Sheet_DetailModel":
                    {
                        ReportServiceSubsector_Lab_Sheet_Detail reportServiceSubsector_Lab_Sheet_Detail = new ReportServiceSubsector_Lab_Sheet_Detail(Language, _User);
                        return (dynamic)reportServiceSubsector_Lab_Sheet_Detail.GetReportSubsector_Lab_Sheet_DetailModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportSubsector_Lab_Sheet_Tube_And_MPN_DetailModel":
                    {
                        ReportServiceSubsector_Lab_Sheet_Tube_And_MPN_Detail reportServiceSubsector_Lab_Sheet_Tube_And_MPN_Detail = new ReportServiceSubsector_Lab_Sheet_Tube_And_MPN_Detail(Language, _User);
                        return (dynamic)reportServiceSubsector_Lab_Sheet_Tube_And_MPN_Detail.GetReportSubsector_Lab_Sheet_Tube_And_MPN_DetailModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportSubsector_FileModel":
                    {
                        ReportServiceSubsector_File reportServiceSubsector_File = new ReportServiceSubsector_File(Language, _User);
                        return (dynamic)reportServiceSubsector_File.GetReportSubsector_FileModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportSector_FileModel":
                    {
                        ReportServiceSector_File reportServiceSector_File = new ReportServiceSector_File(Language, _User);
                        return (dynamic)reportServiceSector_File.GetReportSector_FileModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportArea_FileModel":
                    {
                        ReportServiceArea_File reportServiceArea_File = new ReportServiceArea_File(Language, _User);
                        return (dynamic)reportServiceArea_File.GetReportArea_FileModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportSampling_PlanModel":
                    {
                        ReportServiceSampling_Plan reportServiceSampling_Plan = new ReportServiceSampling_Plan(Language, _User);
                        return (dynamic)reportServiceSampling_Plan.GetReportSampling_PlanModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportSampling_Plan_Lab_SheetModel":
                    {
                        ReportServiceSampling_Plan_Lab_Sheet reportServiceSampling_Plan_Lab_Sheet = new ReportServiceSampling_Plan_Lab_Sheet(Language, _User);
                        return (dynamic)reportServiceSampling_Plan_Lab_Sheet.GetReportSampling_Plan_Lab_SheetModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportSampling_Plan_Lab_Sheet_DetailModel":
                    {
                        ReportServiceSampling_Plan_Lab_Sheet_Detail reportServiceSampling_Plan_Lab_Sheet_Detail = new ReportServiceSampling_Plan_Lab_Sheet_Detail(Language, _User);
                        return (dynamic)reportServiceSampling_Plan_Lab_Sheet_Detail.GetReportSampling_Plan_Lab_Sheet_DetailModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportSampling_Plan_Lab_Sheet_Tube_And_MPN_DetailModel":
                    {
                        ReportServiceSampling_Plan_Lab_Sheet_Tube_And_MPN_Detail reportServiceSampling_Plan_Lab_Sheet_Tube_And_MPN_Detail = new ReportServiceSampling_Plan_Lab_Sheet_Tube_And_MPN_Detail(Language, _User);
                        return (dynamic)reportServiceSampling_Plan_Lab_Sheet_Tube_And_MPN_Detail.GetReportSampling_Plan_Lab_Sheet_Tube_And_MPN_DetailModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportSampling_Plan_SubsectorModel":
                    {
                        ReportServiceSampling_Plan_Subsector reportServiceSampling_Plan_Subsector = new ReportServiceSampling_Plan_Subsector(Language, _User);
                        return (dynamic)reportServiceSampling_Plan_Subsector.GetReportSampling_Plan_SubsectorModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportSampling_Plan_Subsector_SiteModel":
                    {
                        ReportServiceSampling_Plan_Subsector_Site reportServiceSampling_Plan_Subsector_Site = new ReportServiceSampling_Plan_Subsector_Site(Language, _User);
                        return (dynamic)reportServiceSampling_Plan_Subsector_Site.GetReportSampling_Plan_Subsector_SiteModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportClimate_SiteModel":
                    {
                        ReportServiceClimate_Site reportServiceClimate_Site = new ReportServiceClimate_Site(Language, _User);
                        return (dynamic)reportServiceClimate_Site.GetReportClimate_SiteModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportClimate_Site_DataModel":
                    {
                        ReportServiceClimate_Site_Data reportServiceClimate_Site_Data = new ReportServiceClimate_Site_Data(Language, _User);
                        return (dynamic)reportServiceClimate_Site_Data.GetReportClimate_Site_DataModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportHydrometric_SiteModel":
                    {
                        ReportServiceHydrometric_Site reportServiceHydrometric_Site = new ReportServiceHydrometric_Site(Language, _User);
                        return (dynamic)reportServiceHydrometric_Site.GetReportHydrometric_SiteModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportHydrometric_Site_DataModel":
                    {
                        ReportServiceHydrometric_Site_Data reportServiceHydrometric_Site_Data = new ReportServiceHydrometric_Site_Data(Language, _User);
                        return (dynamic)reportServiceHydrometric_Site_Data.GetReportHydrometric_Site_DataModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportHydrometric_Site_Rating_CurveModel":
                    {
                        ReportServiceHydrometric_Site_Rating_Curve reportServiceHydrometric_Site_Rating_Curve = new ReportServiceHydrometric_Site_Rating_Curve(Language, _User);
                        return (dynamic)reportServiceHydrometric_Site_Rating_Curve.GetReportHydrometric_Site_Rating_CurveModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportHydrometric_Site_Rating_Curve_ValueModel":
                    {
                        ReportServiceHydrometric_Site_Rating_Curve_Value reportServiceHydrometric_Site_Rating_Curve_Value = new ReportServiceHydrometric_Site_Rating_Curve_Value(Language, _User);
                        return (dynamic)reportServiceHydrometric_Site_Rating_Curve_Value.GetReportHydrometric_Site_Rating_Curve_ValueModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportProvince_FileModel":
                    {
                        ReportServiceProvince_File reportServiceProvince_File = new ReportServiceProvince_File(Language, _User);
                        return (dynamic)reportServiceProvince_File.GetReportProvince_FileModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportCountry_FileModel":
                    {
                        ReportServiceCountry_File reportServiceCountry_File = new ReportServiceCountry_File(Language, _User);
                        return (dynamic)reportServiceCountry_File.GetReportCountry_FileModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportMPN_LookupModel":
                    {
                        ReportServiceMPN_Lookup reportServiceMPN_Lookup = new ReportServiceMPN_Lookup(Language, _User);
                        return (dynamic)reportServiceMPN_Lookup.GetReportMPN_LookupModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                case "ReportRoot_FileModel":
                    {
                        ReportServiceRoot_File reportServiceRoot_File = new ReportServiceRoot_File(Language, _User);
                        return (dynamic)reportServiceRoot_File.GetReportRoot_FileModelListUnderTVItemIDDB(Language, Command, TVItemID, ParentTagItem, CountOnly, Take);
                    }
                default:
                    return null;
            }
        }
        public T GetReportModelJSON<T>(ReportTag reportTag) where T : new()
        {
            T reportModel = new T();
            NameValueCollection nameValueCollection = new NameValueCollection();
            if (string.IsNullOrWhiteSpace(reportTag.Language) || !AllowableLanguages.Contains(reportTag.Language))
            {
                reportTag.Error = string.Format(ReportServiceRes.AllowableLanguages_, "[en, fr]");
                return reportModel;
            }

            nameValueCollection["Language"] = reportTag.Language;
            string command = "";
            switch (reportTag.DocumentType)
            {
                case DocumentType.CSV:
                    {
                        if (string.IsNullOrWhiteSpace(reportTag.CSVTagText))
                        {
                            reportTag.Error = ReportServiceRes.CSVTagTextIsEmpty;
                            return reportModel;
                        }
                        command = reportTag.CSVTagText;
                    }
                    break;
                case DocumentType.Word:
                    {
                        if (string.IsNullOrWhiteSpace(reportTag.RangeStartTag.Text))
                        {
                            reportTag.Error = ReportServiceRes.RangeStartTagTextIsEmpty;
                            return reportModel;
                        }
                        command = (reportTag.RangeStartTag.Text.EndsWith("\r") ? reportTag.RangeStartTag.Text.Substring(0, reportTag.RangeStartTag.Text.Length - 1) : reportTag.RangeStartTag.Text);
                    }
                    break;
                case DocumentType.Excel:
                    {
                        bool IsTrue = true;
                        if (IsTrue)
                        {
                            reportTag.Error = string.Format(ReportServiceRes._NotImplementedIn_, "Excel", "GetReportModelJSON");
                            return reportModel;
                        }
                        command = "not implemented";
                    }
                    break;
                case DocumentType.KML:
                    {
                        if (string.IsNullOrWhiteSpace(reportTag.KMLTagText))
                        {
                            reportTag.Error = ReportServiceRes.KMLTagTextIsEmpty;
                            return reportModel;
                        }
                        command = reportTag.KMLTagText;
                    }
                    break;
                default:
                    {
                        reportTag.Error = string.Format(ReportServiceRes.AllowableDocumentTypes_,
                            "[" + DocumentType.CSV.ToString() + "," + DocumentType.Excel.ToString() +
                            "," + DocumentType.KML.ToString() + "," + DocumentType.Word.ToString() + "]");
                        return reportModel;
                    }
            }
            nameValueCollection["Command"] = command;
            if (reportTag.ReportType == null)
            {
                reportTag.Error = string.Format(ReportServiceRes._IsRequiredIn_, "ReportType", "GetReportModelJSON");
                return reportModel;
            }
            nameValueCollection["Name"] = "Get" + reportTag.ReportType.Name + "ListUnderTVItemID" + "JSON";
            nameValueCollection["UnderTVItemID"] = reportTag.UnderTVItemID.ToString();
            nameValueCollection["ParentTagItem"] = (reportTag.ReportTagParent == null ? "" : reportTag.ReportTagParent.TagItem);
            nameValueCollection["CountOnly"] = reportTag.CountOnly.ToString();
            nameValueCollection["Take"] = reportTag.Take.ToString();

            if (_User != null)
            {
                reportModel = (dynamic)GetDataDirectlyFromDB(reportTag.ReportType.Name, (reportTag.Language == "fr" ? LanguageEnum.fr : LanguageEnum.en), command, reportTag.UnderTVItemID, (reportTag.ReportTagParent == null ? "" : reportTag.ReportTagParent.TagItem), false, reportTag.Take);
            }
            else
            {
                reportModel = DownloadSerializedJsonData<T>(nameValueCollection);
            }

            if (!string.IsNullOrWhiteSpace(nameValueCollection["Error"]))
                reportTag.Error = reportTag.Error + " ServerError: " + nameValueCollection["Error"];


            return reportModel;
        }
        public string ReportGetDB<T>(ReportTag reportTag, ReportModelDynamic reportModelDynamic) where T : new()
        {
            List<T> reportModelList = GetReportModelJSON<List<T>>(reportTag);
            if (!string.IsNullOrWhiteSpace(reportTag.Error))
                return reportTag.Error;

            if (reportModelList.Count > 0)
            {
                foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                {
                    if (propertyInfo.Name == reportTag.TagItem + "_Error")
                    {
                        string errStr = (string)propertyInfo.GetValue(reportModelList.First());
                        if (!string.IsNullOrWhiteSpace(errStr))
                            return errStr;
                    }
                }

                foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                {
                    if (propertyInfo.Name == reportTag.TagItem + "_ID")
                    {
                        reportModelDynamic.TVItemID = (int)propertyInfo.GetValue(reportModelList.First());
                        break;
                    }
                }
            }

            reportModelDynamic.ReportModel = reportModelList;
            return "";
        }
        public string ReportGetDBOfType(ReportTag reportTag, ReportModelDynamic reportModelDynamic)
        {
            string retStr = "";
            switch (reportTag.TagItem)
            {
                case "Root":
                    retStr = ReportGetDB<ReportRootModel>(reportTag, reportModelDynamic);
                    break;
                case "Country":
                    retStr = ReportGetDB<ReportCountryModel>(reportTag, reportModelDynamic);
                    break;
                case "Province":
                    retStr = ReportGetDB<ReportProvinceModel>(reportTag, reportModelDynamic);
                    break;
                case "Area":
                    retStr = ReportGetDB<ReportAreaModel>(reportTag, reportModelDynamic);
                    break;
                case "Sector":
                    retStr = ReportGetDB<ReportSectorModel>(reportTag, reportModelDynamic);
                    break;
                case "Subsector":
                    retStr = ReportGetDB<ReportSubsectorModel>(reportTag, reportModelDynamic);
                    break;
                case "MWQM_Site":
                    retStr = ReportGetDB<ReportMWQM_SiteModel>(reportTag, reportModelDynamic);
                    break;
                case "MWQM_Site_Sample":
                    retStr = ReportGetDB<ReportMWQM_Site_SampleModel>(reportTag, reportModelDynamic);
                    break;
                case "MWQM_Site_Start_And_End_Date":
                    retStr = ReportGetDB<ReportMWQM_Site_Start_And_End_DateModel>(reportTag, reportModelDynamic);
                    break;
                case "MWQM_Site_File":
                    retStr = ReportGetDB<ReportMWQM_Site_FileModel>(reportTag, reportModelDynamic);
                    break;
                case "MWQM_Run":
                    retStr = ReportGetDB<ReportMWQM_RunModel>(reportTag, reportModelDynamic);
                    break;
                case "MWQM_Run_Sample":
                    retStr = ReportGetDB<ReportMWQM_Run_SampleModel>(reportTag, reportModelDynamic);
                    break;
                case "MWQM_Run_Lab_Sheet":
                    retStr = ReportGetDB<ReportMWQM_Run_Lab_SheetModel>(reportTag, reportModelDynamic);
                    break;
                case "MWQM_Run_Lab_Sheet_Detail":
                    retStr = ReportGetDB<ReportMWQM_Run_Lab_Sheet_DetailModel>(reportTag, reportModelDynamic);
                    break;
                case "MWQM_Run_Lab_Sheet_Tube_And_MPN_Detail":
                    retStr = ReportGetDB<ReportMWQM_Run_Lab_Sheet_Tube_And_MPN_DetailModel>(reportTag, reportModelDynamic);
                    break;
                case "MWQM_Run_File":
                    retStr = ReportGetDB<ReportMWQM_Run_FileModel>(reportTag, reportModelDynamic);
                    break;
                case "Pol_Source_Site":
                    retStr = ReportGetDB<ReportPol_Source_SiteModel>(reportTag, reportModelDynamic);
                    break;
                case "Pol_Source_Site_Obs":
                    retStr = ReportGetDB<ReportPol_Source_Site_ObsModel>(reportTag, reportModelDynamic);
                    break;
                case "Pol_Source_Site_Obs_Issue":
                    retStr = ReportGetDB<ReportPol_Source_Site_Obs_IssueModel>(reportTag, reportModelDynamic);
                    break;
                case "Pol_Source_Site_File":
                    retStr = ReportGetDB<ReportPol_Source_Site_FileModel>(reportTag, reportModelDynamic);
                    break;
                case "Pol_Source_Site_Address":
                    retStr = ReportGetDB<ReportPol_Source_Site_AddressModel>(reportTag, reportModelDynamic);
                    break;
                case "Municipality":
                    retStr = ReportGetDB<ReportMunicipalityModel>(reportTag, reportModelDynamic);
                    break;
                case "Infrastructure":
                    retStr = ReportGetDB<ReportInfrastructureModel>(reportTag, reportModelDynamic);
                    break;
                case "Box_Model":
                    retStr = ReportGetDB<ReportBox_ModelModel>(reportTag, reportModelDynamic);
                    break;
                case "Box_Model_Result":
                    retStr = ReportGetDB<ReportBox_Model_ResultModel>(reportTag, reportModelDynamic);
                    break;
                case "Visual_Plumes_Scenario":
                    retStr = ReportGetDB<ReportVisual_Plumes_ScenarioModel>(reportTag, reportModelDynamic);
                    break;
                case "Visual_Plumes_Scenario_Ambient":
                    retStr = ReportGetDB<ReportVisual_Plumes_Scenario_AmbientModel>(reportTag, reportModelDynamic);
                    break;
                case "Visual_Plumes_Scenario_Result":
                    retStr = ReportGetDB<ReportVisual_Plumes_Scenario_ResultModel>(reportTag, reportModelDynamic);
                    break;
                case "Infrastructure_Address":
                    retStr = ReportGetDB<ReportInfrastructure_AddressModel>(reportTag, reportModelDynamic);
                    break;
                case "Infrastructure_File":
                    retStr = ReportGetDB<ReportInfrastructure_FileModel>(reportTag, reportModelDynamic);
                    break;
                case "Mike_Scenario":
                    retStr = ReportGetDB<ReportMike_ScenarioModel>(reportTag, reportModelDynamic);
                    break;
                case "Mike_Scenario_Special_Result_KML":
                    retStr = ReportGetDB<ReportMike_Scenario_Special_Result_KMLModel>(reportTag, reportModelDynamic);
                    break;
                case "Mike_Source":
                    retStr = ReportGetDB<ReportMike_SourceModel>(reportTag, reportModelDynamic);
                    break;
                case "Mike_Source_Start_End":
                    retStr = ReportGetDB<ReportMike_Source_Start_EndModel>(reportTag, reportModelDynamic);
                    break;
                case "Mike_Boundary_Condition":
                    retStr = ReportGetDB<ReportMike_Boundary_ConditionModel>(reportTag, reportModelDynamic);
                    break;
                case "Mike_Scenario_File":
                    retStr = ReportGetDB<ReportMike_Scenario_FileModel>(reportTag, reportModelDynamic);
                    break;
                case "Municipality_Contact":
                    retStr = ReportGetDB<ReportMunicipality_ContactModel>(reportTag, reportModelDynamic);
                    break;
                case "Municipality_Contact_Address":
                    retStr = ReportGetDB<ReportMunicipality_Contact_AddressModel>(reportTag, reportModelDynamic);
                    break;
                case "Municipality_Contact_Tel":
                    retStr = ReportGetDB<ReportMunicipality_Contact_TelModel>(reportTag, reportModelDynamic);
                    break;
                case "Municipality_Contact_Email":
                    retStr = ReportGetDB<ReportMunicipality_Contact_EmailModel>(reportTag, reportModelDynamic);
                    break;
                case "Municipality_File":
                    retStr = ReportGetDB<ReportMunicipality_FileModel>(reportTag, reportModelDynamic);
                    break;
                case "Subsector_Special_Table":
                    retStr = ReportGetDB<ReportSubsector_Special_TableModel>(reportTag, reportModelDynamic);
                    break;
                case "Subsector_Climate_Site":
                    retStr = ReportGetDB<ReportSubsector_Climate_SiteModel>(reportTag, reportModelDynamic);
                    break;
                case "Subsector_Climate_Site_Data":
                    retStr = ReportGetDB<ReportSubsector_Climate_Site_DataModel>(reportTag, reportModelDynamic);
                    break;
                case "Subsector_Hydrometric_Site":
                    retStr = ReportGetDB<ReportSubsector_Hydrometric_SiteModel>(reportTag, reportModelDynamic);
                    break;
                case "Subsector_Hydrometric_Site_Data":
                    retStr = ReportGetDB<ReportSubsector_Hydrometric_Site_DataModel>(reportTag, reportModelDynamic);
                    break;
                case "Subsector_Hydrometric_Site_Rating_Curve":
                    retStr = ReportGetDB<ReportSubsector_Hydrometric_Site_Rating_CurveModel>(reportTag, reportModelDynamic);
                    break;
                case "Subsector_Hydrometric_Site_Rating_Curve_Value":
                    retStr = ReportGetDB<ReportSubsector_Hydrometric_Site_Rating_Curve_ValueModel>(reportTag, reportModelDynamic);
                    break;
                case "Subsector_Tide_Site":
                    retStr = ReportGetDB<ReportSubsector_Tide_SiteModel>(reportTag, reportModelDynamic);
                    break;
                case "Subsector_Tide_Site_Data":
                    retStr = ReportGetDB<ReportSubsector_Tide_Site_DataModel>(reportTag, reportModelDynamic);
                    break;
                case "Subsector_Lab_Sheet":
                    retStr = ReportGetDB<ReportSubsector_Lab_SheetModel>(reportTag, reportModelDynamic);
                    break;
                case "Subsector_Lab_Sheet_Detail":
                    retStr = ReportGetDB<ReportSubsector_Lab_Sheet_DetailModel>(reportTag, reportModelDynamic);
                    break;
                case "Subsector_Lab_Sheet_Tube_And_MPN_Detail":
                    retStr = ReportGetDB<ReportSubsector_Lab_Sheet_Tube_And_MPN_DetailModel>(reportTag, reportModelDynamic);
                    break;
                case "Subsector_File":
                    retStr = ReportGetDB<ReportSubsector_FileModel>(reportTag, reportModelDynamic);
                    break;
                case "Sector_File":
                    retStr = ReportGetDB<ReportSector_FileModel>(reportTag, reportModelDynamic);
                    break;
                case "Area_File":
                    retStr = ReportGetDB<ReportArea_FileModel>(reportTag, reportModelDynamic);
                    break;
                case "Sampling_Plan":
                    retStr = ReportGetDB<ReportSampling_PlanModel>(reportTag, reportModelDynamic);
                    break;
                case "Sampling_Plan_Lab_Sheet":
                    retStr = ReportGetDB<ReportSampling_Plan_Lab_SheetModel>(reportTag, reportModelDynamic);
                    break;
                case "Sampling_Plan_Lab_Sheet_Detail":
                    retStr = ReportGetDB<ReportSampling_Plan_Lab_Sheet_DetailModel>(reportTag, reportModelDynamic);
                    break;
                case "Sampling_Plan_Lab_Sheet_Tube_And_MPN_Detail":
                    retStr = ReportGetDB<ReportSampling_Plan_Lab_Sheet_Tube_And_MPN_DetailModel>(reportTag, reportModelDynamic);
                    break;
                case "Sampling_Plan_Subsector":
                    retStr = ReportGetDB<ReportSampling_Plan_SubsectorModel>(reportTag, reportModelDynamic);
                    break;
                case "Sampling_Plan_Subsector_Site":
                    retStr = ReportGetDB<ReportSampling_Plan_Subsector_SiteModel>(reportTag, reportModelDynamic);
                    break;
                case "Climate_Site":
                    retStr = ReportGetDB<ReportClimate_SiteModel>(reportTag, reportModelDynamic);
                    break;
                case "Climate_Site_Data":
                    retStr = ReportGetDB<ReportClimate_Site_DataModel>(reportTag, reportModelDynamic);
                    break;
                case "Hydrometric_Site":
                    retStr = ReportGetDB<ReportHydrometric_SiteModel>(reportTag, reportModelDynamic);
                    break;
                case "Hydrometric_Site_Data":
                    retStr = ReportGetDB<ReportHydrometric_Site_DataModel>(reportTag, reportModelDynamic);
                    break;
                case "Hydrometric_Site_Rating_Curve":
                    retStr = ReportGetDB<ReportHydrometric_Site_Rating_CurveModel>(reportTag, reportModelDynamic);
                    break;
                case "Hydrometric_Site_Rating_Curve_Value":
                    retStr = ReportGetDB<ReportHydrometric_Site_Rating_Curve_ValueModel>(reportTag, reportModelDynamic);
                    break;
                case "Province_File":
                    retStr = ReportGetDB<ReportProvince_FileModel>(reportTag, reportModelDynamic);
                    break;
                case "Country_File":
                    retStr = ReportGetDB<ReportCountry_FileModel>(reportTag, reportModelDynamic);
                    break;
                case "MPN_Lookup":
                    retStr = ReportGetDB<ReportMPN_LookupModel>(reportTag, reportModelDynamic);
                    break;
                case "Root_File":
                    retStr = ReportGetDB<ReportRoot_FileModel>(reportTag, reportModelDynamic);
                    break;
                default:
                    {
                        retStr = string.Format(ReportServiceRes._NotImplementedIn_, reportTag.TagItem, "ReportGetDBOfType");
                    }
                    break;
            }


            return retStr;
        }
        public object ReportGetFieldTextOrValue<T>(T reportModel, bool ConvertToText, PropertyInfo propertyInfo, string TagText, ReportTag reportTag, ReportTreeNode reportTreeNode) where T : new()
        {
            string FormatField = "";
            if (propertyInfo.PropertyType.FullName.Contains("System.DateTime"))
            {
                FormatField = reportTreeNode.dbFormatingField.DateFormating.Replace("*", " ");
            }
            else if (propertyInfo.PropertyType.FullName.Contains("System.Single"))
            {
                FormatField = reportTreeNode.dbFormatingField.NumberFormating;
            }

            if (!string.IsNullOrWhiteSpace(TagText))
            {
                TagText = TagText.Replace(Marker, "");
                if (string.IsNullOrWhiteSpace(TagText))
                    return "";

                if (TagText.Contains(Marker))
                    return "";

                ReportTreeNode reportTreeNodeTemp = new ReportTreeNode();
                List<string> strList = TagText.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

                bool IsDBFiltering = false;
                string retStr = GetReportTreeNodesForField(strList, reportTreeNodeTemp, reportTag.ReportType, IsDBFiltering, 999);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    reportTag.Error = retStr;
                    return "";
                }

                if (reportTreeNodeTemp.ReportFieldType == ReportFieldTypeEnum.DateAndTime)
                {
                    if (!string.IsNullOrWhiteSpace(reportTreeNodeTemp.dbFormatingField.DateFormating))
                    {
                        FormatField = reportTreeNodeTemp.dbFormatingField.DateFormating.Replace("*", " ");
                    }
                    if (!string.IsNullOrWhiteSpace(reportTreeNodeTemp.reportFormatingField.DateFormating))
                    {
                        FormatField = reportTreeNodeTemp.reportFormatingField.DateFormating.Replace("*", " ");
                    }
                }
                else if (reportTreeNodeTemp.ReportFieldType == ReportFieldTypeEnum.NumberWithDecimal || reportTreeNodeTemp.ReportFieldType == ReportFieldTypeEnum.NumberWhole)
                {
                    if (!string.IsNullOrWhiteSpace(reportTreeNodeTemp.dbFormatingField.NumberFormating))
                    {
                        FormatField = reportTreeNodeTemp.dbFormatingField.NumberFormating.Replace("*", " ");
                    }
                    if (!string.IsNullOrWhiteSpace(reportTreeNodeTemp.reportFormatingField.NumberFormating))
                    {
                        FormatField = reportTreeNodeTemp.reportFormatingField.NumberFormating.Replace("*", " ");
                    }
                }
                bool KeepShow = ReturnKeepShow(reportModel, typeof(T), reportTreeNodeTemp, IsDBFiltering);

                if (!string.IsNullOrWhiteSpace(reportTreeNodeTemp.Error))
                    return reportTreeNodeTemp.Error;

                if (!KeepShow)
                {
                    return "";
                }
            }


            if (propertyInfo.PropertyType.FullName.Contains("System.DateTime"))
            {
                DateTime TempVal = (DateTime)propertyInfo.GetValue(reportModel);
                if (ConvertToText)
                {
                    try
                    {
                        string valStr = (string)(!string.IsNullOrWhiteSpace(FormatField) ? TempVal.ToString(FormatField) : TempVal.ToString());
                        return valStr;
                    }
                    catch (Exception)
                    {
                        return FormatField;
                    }

                }

                return (DateTime)TempVal;
            }
            else if (propertyInfo.PropertyType.FullName.Contains("System.Boolean"))
            {
                bool TempVal = (bool)propertyInfo.GetValue(reportModel);
                if (ConvertToText)
                    return (string)TempVal.ToString();

                return (bool)TempVal;
            }
            else if (propertyInfo.PropertyType.FullName.Contains("System.String"))
            {
                return (string)propertyInfo.GetValue(reportModel);
            }
            else if (propertyInfo.PropertyType.FullName.Contains("System.Int32"))
            {
                int TempVal = (int)propertyInfo.GetValue(reportModel);
                if (ConvertToText)
                    try
                    {
                        string valStr = (string)(!string.IsNullOrWhiteSpace(FormatField) ? TempVal.ToString(FormatField) : TempVal.ToString());
                        return valStr;
                    }
                    catch (Exception)
                    {
                        return FormatField;
                    }

                return (int)TempVal;
            }
            else if (propertyInfo.PropertyType.FullName.Contains("System.Single"))
            {
                float TempVal = (float)propertyInfo.GetValue(reportModel);

                if (ConvertToText)
                {
                    try
                    {
                        string valStr = (string)(!string.IsNullOrWhiteSpace(FormatField) ? TempVal.ToString(FormatField).Replace(",", ".") : TempVal.ToString());
                        return valStr;
                    }
                    catch (Exception)
                    {
                        return FormatField;
                    }
                }

                return (float)TempVal;
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.FilePurposeEnum"))
            {
                return _BaseEnumService.GetEnumText_FilePurposeEnum((FilePurposeEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.FileTypeEnum"))
            {
                return _BaseEnumService.GetEnumText_FileTypeEnum((FileTypeEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.TranslationStatusEnum"))
            {
                return _BaseEnumService.GetEnumText_TranslationStatusEnum((TranslationStatusEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.BoxModelResultTypeEnum"))
            {
                return _BaseEnumService.GetEnumText_BoxModelResultTypeEnum((BoxModelResultTypeEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.InfrastructureTypeEnum"))
            {
                return _BaseEnumService.GetEnumText_InfrastructureTypeEnum((InfrastructureTypeEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.FacilityTypeEnum"))
            {
                return _BaseEnumService.GetEnumText_FacilityTypeEnum((FacilityTypeEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.AerationTypeEnum"))
            {
                return _BaseEnumService.GetEnumText_AerationTypeEnum((AerationTypeEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.PreliminaryTreatmentTypeEnum"))
            {
                return _BaseEnumService.GetEnumText_PreliminaryTreatmentTypeEnum((PreliminaryTreatmentTypeEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.PrimaryTreatmentTypeEnum"))
            {
                return _BaseEnumService.GetEnumText_PrimaryTreatmentTypeEnum((PrimaryTreatmentTypeEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.SecondaryTreatmentTypeEnum"))
            {
                return _BaseEnumService.GetEnumText_SecondaryTreatmentTypeEnum((SecondaryTreatmentTypeEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.TertiaryTreatmentTypeEnum"))
            {
                return _BaseEnumService.GetEnumText_TertiaryTreatmentTypeEnum((TertiaryTreatmentTypeEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.TreatmentTypeEnum"))
            {
                return _BaseEnumService.GetEnumText_TreatmentTypeEnum((TreatmentTypeEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.DisinfectionTypeEnum"))
            {
                return _BaseEnumService.GetEnumText_DisinfectionTypeEnum((DisinfectionTypeEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.CollectionSystemTypeEnum"))
            {
                return _BaseEnumService.GetEnumText_CollectionSystemTypeEnum((CollectionSystemTypeEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.AlarmSystemTypeEnum"))
            {
                return _BaseEnumService.GetEnumText_AlarmSystemTypeEnum((AlarmSystemTypeEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.ScenarioStatusEnum"))
            {
                return _BaseEnumService.GetEnumText_ScenarioStatusEnum((ScenarioStatusEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.StorageDataTypeEnum"))
            {
                return _BaseEnumService.GetEnumText_StorageDataTypeEnum((StorageDataTypeEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.LanguageEnum"))
            {
                return _BaseEnumService.GetEnumText_LanguageEnum((LanguageEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.SampleTypeEnum"))
            {
                //return _BaseEnumService.GetEnumText_SampleTypeEnum((SampleTypeEnum)propertyInfo.GetValue(reportModel));
                return propertyInfo.GetValue(reportModel);
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.BeaufortScaleEnum"))
            {
                return _BaseEnumService.GetEnumText_BeaufortScaleEnum((BeaufortScaleEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.AnalyzeMethodEnum"))
            {
                return _BaseEnumService.GetEnumText_AnalyzeMethodEnum((AnalyzeMethodEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.SampleMatrixEnum"))
            {
                return _BaseEnumService.GetEnumText_SampleMatrixEnum((SampleMatrixEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.LaboratoryEnum"))
            {
                return _BaseEnumService.GetEnumText_LaboratoryEnum((LaboratoryEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.SampleStatusEnum"))
            {
                return _BaseEnumService.GetEnumText_SampleStatusEnum((SampleStatusEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.SamplingPlanTypeEnum"))
            {
                return _BaseEnumService.GetEnumText_SamplingPlanTypeEnum((SamplingPlanTypeEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.LabSheetTypeEnum"))
            {
                return _BaseEnumService.GetEnumText_LabSheetTypeEnum((LabSheetTypeEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.LabSheetStatusEnum"))
            {
                return _BaseEnumService.GetEnumText_LabSheetStatusEnum((LabSheetStatusEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.PolSourceInactiveReasonEnum"))
            {
                return _BaseEnumService.GetEnumText_PolSourceInactiveReasonEnum((PolSourceInactiveReasonEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.PolSourceObsInfoEnum"))
            {
                //return _BaseEnumService.GetEnumText_PolSourceObsInfoEnum((PolSourceObsInfoEnum)propertyInfo.GetValue(reportModel));
                return propertyInfo.GetValue(reportModel);
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.AddressTypeEnum"))
            {
                return _BaseEnumService.GetEnumText_AddressTypeEnum((AddressTypeEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.StreetTypeEnum"))
            {
                return _BaseEnumService.GetEnumText_StreetTypeEnum((StreetTypeEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.ContactTitleEnum"))
            {
                return _BaseEnumService.GetEnumText_ContactTitleEnum((ContactTitleEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.EmailTypeEnum"))
            {
                return _BaseEnumService.GetEnumText_EmailTypeEnum((EmailTypeEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.TelTypeEnum"))
            {
                return _BaseEnumService.GetEnumText_TelTypeEnum((TelTypeEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.TideTextEnum"))
            {
                return _BaseEnumService.GetEnumText_TideTextEnum((TideTextEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.TideDataTypeEnum"))
            {
                return _BaseEnumService.GetEnumText_TideDataTypeEnum((TideDataTypeEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.SpecialTableTypeEnum"))
            {
                return _BaseEnumService.GetEnumText_SpecialTableTypeEnum((SpecialTableTypeEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.MWQMSiteLatestClassificationEnum"))
            {
                return _BaseEnumService.GetEnumText_MWQMSiteLatestClassificationEnum((MWQMSiteLatestClassificationEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.PolSourceIssueRiskEnum"))
            {
                return _BaseEnumService.GetEnumText_PolSourceIssueRiskEnum((PolSourceIssueRiskEnum)propertyInfo.GetValue(reportModel));
            }
            else if (propertyInfo.PropertyType.FullName.Contains("CSSPEnumsDLL.Enums.MikeScenarioSpecialResultKMLTypeEnum"))
            {
                return _BaseEnumService.GetEnumText_MikeScenarioSpecialResultKMLTypeEnum((MikeScenarioSpecialResultKMLTypeEnum)propertyInfo.GetValue(reportModel));
            }
            else
            {
                return string.Format(ReportServiceRes._NotImplementedIn_, propertyInfo.PropertyType.FullName, reportModel.GetType().ToString());
            }
        }
        #endregion Generic Functions

        #region Functions public
        public ReportTreeNode CreateReportTreeNodeItem(string Text)
        {
            ReportTreeNode reportTreeNode = new ReportTreeNode()
            {
                Text = Text,
            };
            reportTreeNode.ReportTreeNodeType = ReportTreeNodeTypeEnum.Error;
            reportTreeNode.ReportTreeNodeSubType = ReportTreeNodeSubTypeEnum.Error;
            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.Error;

            return reportTreeNode;
        }
        //public bool DoesVariableExist(ReportTag reportTag, string VariableName)
        //{
        //    bool WasFound = false;

        //    foreach (ReportTreeNode reportTreeNode in reportTag.ReportTreeNodeList)
        //    {
        //        if (reportTreeNode.Text == VariableName)
        //        {
        //            WasFound = true;
        //            break;
        //        }
        //    }
        //    if (!WasFound)
        //    {
        //        if (reportTag.ReportTagChildList.Count == 0)
        //            return WasFound;

        //        foreach (ReportTag reportTagChild in reportTag.ReportTagChildList)
        //        {
        //            WasFound = DoesVariableExist(reportTagChild, VariableName);
        //            if (WasFound)
        //                return WasFound;
        //        }
        //    }

        //    return WasFound;
        //}
        public List<string> GetAllowableParentTagItem(string TagItem)
        {
            ReportTreeNode reportTreeNode = FindReportTreeNode(_ReportTreeNodeRoot, TagItem);
            List<string> AllowableParentTagItemList = new List<string>();

            if (reportTreeNode != null)
            {
                while (reportTreeNode != null)
                {
                    if (reportTreeNode.Text != TagItem)
                    {
                        AllowableParentTagItemList.Insert(0, reportTreeNode.Text);
                    }

                    reportTreeNode = (ReportTreeNode)reportTreeNode.Parent;
                }
            }

            return AllowableParentTagItemList;
        }
        public string GetAllTheDateFilters(int Current, ReportTreeNode reportTreeNode, List<string> strList, bool IsDBFiltering, int LineCount)
        {
            while (true)
            {
                if (strList.Count > Current)
                {
                    ReportConditionDateField FilteringDateField = new ReportConditionDateField();
                    string retStr = SetReportTreeNodeDateCondition(Current, FilteringDateField, reportTreeNode, strList, LineCount);
                    if (!string.IsNullOrWhiteSpace(retStr))
                        return retStr;

                    if (IsDBFiltering)
                    {
                        reportTreeNode.dbFilteringDateFieldList.Add(FilteringDateField);
                    }
                    else
                    {
                        reportTreeNode.reportConditionDateFieldList.Add(FilteringDateField);
                    }
                }

                Current += 1;
                for (int i = Current, count = strList.Count(); i < count; i++)
                {
                    if (AllowableBasicFilters.Contains(strList[i]))
                    {
                        Current = i;
                        break;
                    }
                    Current = i + 1;
                }

                if (strList.Count < Current)
                    break;
            }

            return "";
        }
        public string GetAllTheEnumFilters(int Current, ReportTreeNode reportTreeNode, List<string> strList, bool IsDBFiltering, int LineCount)
        {
            while (true)
            {
                if (strList.Count > Current)
                {
                    ReportConditionEnumField FilteringEnumField = new ReportConditionEnumField();
                    string retStr = SetReportTreeNodeEnumCondition(Current, FilteringEnumField, reportTreeNode, strList, LineCount);
                    if (!string.IsNullOrWhiteSpace(retStr))
                        return retStr;

                    if (IsDBFiltering)
                    {
                        reportTreeNode.dbFilteringEnumFieldList.Add(FilteringEnumField);
                    }
                    else
                    {
                        reportTreeNode.reportConditionEnumFieldList.Add(FilteringEnumField);
                    }
                }
                Current += 2;

                if (strList.Count < Current)
                    break;
            }

            return "";
        }
        public string GetAllTheNumberFilters(int Current, ReportTreeNode reportTreeNode, List<string> strList, bool IsDBFiltering, int LineCount)
        {
            while (true)
            {
                ReportConditionNumberField FilteringNumberField = new ReportConditionNumberField();
                if (strList.Count > Current)
                {
                    FilteringNumberField = new ReportConditionNumberField();
                    string retStr = SetReportTreeNodeNumberCondition(Current, FilteringNumberField, reportTreeNode, strList, LineCount);
                    if (!string.IsNullOrWhiteSpace(retStr))
                    {
                        return retStr;
                    }
                    if (IsDBFiltering)
                    {
                        reportTreeNode.dbFilteringNumberFieldList.Add(FilteringNumberField);
                    }
                    else
                    {
                        reportTreeNode.reportConditionNumberFieldList.Add(FilteringNumberField);
                    }
                }

                Current += 2;
                if (strList.Count < Current)
                    break;
            }

            return "";
        }
        public string GetAllTheTextFilters(int Current, ReportTreeNode reportTreeNode, List<string> strList, bool IsDBFiltering, int LineCount)
        {
            while (true)
            {
                ReportConditionTextField FilteringTextField = new ReportConditionTextField();
                if (strList.Count > Current)
                {
                    FilteringTextField = new ReportConditionTextField();
                    string retStr = SetReportTreeNodeTextCondition(Current, FilteringTextField, reportTreeNode, strList, LineCount);
                    if (!string.IsNullOrWhiteSpace(retStr))
                    {
                        return retStr;
                    }
                    if (IsDBFiltering)
                    {
                        reportTreeNode.dbFilteringTextFieldList.Add(FilteringTextField);
                    }
                    else
                    {
                        reportTreeNode.reportConditionTextFieldList.Add(FilteringTextField);
                    }
                }
                Current += 2;

                if (strList.Count < Current)
                    break;
            }

            return "";
        }
        public string GetAllTheTrueFalseFilters(int Current, ReportTreeNode reportTreeNode, List<string> strList, bool IsDBFiltering, int LineCount)
        {
            while (true)
            {
                if (strList.Count > Current)
                {

                    ReportConditionTrueFalseField FilteringTrueFalseField = new ReportConditionTrueFalseField();
                    string retStr = SetReportTreeNodeTrueFalse(Current, FilteringTrueFalseField, reportTreeNode, strList, LineCount);
                    if (!string.IsNullOrWhiteSpace(retStr))
                    {
                        return retStr;
                    }
                    if (IsDBFiltering)
                    {
                        reportTreeNode.dbFilteringTrueFalseFieldList.Add(FilteringTrueFalseField);
                    }
                    else
                    {
                        reportTreeNode.reportConditionTrueFalseFieldList.Add(FilteringTrueFalseField);
                    }
                }
                Current += 1;

                if (strList.Count < Current)
                    break;
            }

            return "";
        }
        public string GetDateFormatText(ReportFormatingDateEnum? reportFormatingDate)
        {
            switch (reportFormatingDate)
            {
                case ReportFormatingDateEnum.ReportFormatingDateYearOnly:
                    return "yyyy";
                case ReportFormatingDateEnum.ReportFormatingDateMonthDecimalOnly:
                    return "MM";
                case ReportFormatingDateEnum.ReportFormatingDateMonthShortTextOnly:
                    return "MMM";
                case ReportFormatingDateEnum.ReportFormatingDateMonthFullTextOnly:
                    return "MMMM";
                case ReportFormatingDateEnum.ReportFormatingDateDayOnly:
                    return "dd";
                case ReportFormatingDateEnum.ReportFormatingDateHourOnly:
                    return "HH";
                case ReportFormatingDateEnum.ReportFormatingDateMinuteOnly:
                    return "mm";
                case ReportFormatingDateEnum.ReportFormatingDateYearMonthDecimalDay:
                    return "yyyy MM dd";
                case ReportFormatingDateEnum.ReportFormatingDateYearMonthShortTextDay:
                    return "yyyy MMM dd";
                case ReportFormatingDateEnum.ReportFormatingDateYearMonthFullTextDay:
                    return "yyyy MMMM dd";
                case ReportFormatingDateEnum.ReportFormatingDateYearMonthDecimalDayHourMinute:
                    return "yyyy MM dd HH:mm";
                case ReportFormatingDateEnum.ReportFormatingDateYearMonthShortTextDayHourMinute:
                    return "yyyy MMM dd HH:mm";
                case ReportFormatingDateEnum.ReportFormatingDateYearMonthFullTextDayHourMinute:
                    return "yyyy MMMM dd HH:mm";
                default:
                    return "";
            }
        }
        public void GetFieldDBFilterText(ReportTreeNode reportTreeNode, StringBuilder sb)
        {
            if (reportTreeNode == null)
                return;

            switch (reportTreeNode.ReportFieldType)
            {
                case ReportFieldTypeEnum.Error:
                    return;
                case ReportFieldTypeEnum.DateAndTime:
                    {
                        if (reportTreeNode.dbSortingField.ReportSorting != ReportSortingEnum.Error)
                        {
                            sb.Append(" " + GetReportSortingText(reportTreeNode.dbSortingField.ReportSorting) + " " + reportTreeNode.dbSortingField.Ordinal.ToString());
                        }
                        sb.Append(GetFieldDBFormatText(reportTreeNode));
                        foreach (ReportConditionDateField dbFilteringDateField in reportTreeNode.dbFilteringDateFieldList)
                        {
                            if (dbFilteringDateField.ReportCondition != ReportConditionEnum.Error)
                            {
                                sb.Append(" " + GetReportConditionText(dbFilteringDateField.ReportCondition));
                                if (dbFilteringDateField.DateTimeConditionYear != null && dbFilteringDateField.DateTimeConditionYear != 0)
                                {
                                    sb.Append(" YEAR " + dbFilteringDateField.DateTimeConditionYear.ToString());
                                }
                                if (dbFilteringDateField.DateTimeConditionMonth != null && dbFilteringDateField.DateTimeConditionMonth != 0)
                                {
                                    sb.Append(" MONTH " + dbFilteringDateField.DateTimeConditionMonth.ToString());
                                }
                                if (dbFilteringDateField.DateTimeConditionDay != null && dbFilteringDateField.DateTimeConditionDay != 0)
                                {
                                    sb.Append(" DAY " + dbFilteringDateField.DateTimeConditionDay.ToString());
                                }
                                if (dbFilteringDateField.DateTimeConditionHour != null && dbFilteringDateField.DateTimeConditionHour != 0)
                                {
                                    sb.Append(" HOUR " + dbFilteringDateField.DateTimeConditionHour.ToString());
                                }
                                if (dbFilteringDateField.DateTimeConditionMinute != null && dbFilteringDateField.DateTimeConditionMinute != 0)
                                {
                                    sb.Append(" MINUTE " + dbFilteringDateField.DateTimeConditionMinute.ToString());
                                }
                            }
                        }
                    }
                    break;
                case ReportFieldTypeEnum.NumberWhole:
                case ReportFieldTypeEnum.NumberWithDecimal:
                    {
                        if (reportTreeNode.dbSortingField.ReportSorting != ReportSortingEnum.Error)
                        {
                            sb.Append(" " + GetReportSortingText(reportTreeNode.dbSortingField.ReportSorting) + " " + reportTreeNode.dbSortingField.Ordinal.ToString());
                        }
                        sb.Append(GetFieldDBFormatText(reportTreeNode));
                        foreach (ReportConditionNumberField dbFilteringNumberField in reportTreeNode.dbFilteringNumberFieldList)
                        {
                            if (dbFilteringNumberField.ReportCondition != ReportConditionEnum.Error)
                            {
                                sb.Append(" " + GetReportConditionText(dbFilteringNumberField.ReportCondition));
                                if (dbFilteringNumberField.NumberCondition != 0)
                                {
                                    sb.Append(" " + (LanguageRequest == LanguageEnum.fr ? dbFilteringNumberField.NumberCondition.ToString().Replace(",", ".") : dbFilteringNumberField.NumberCondition.ToString()));
                                }
                            }
                        }
                    }
                    break;
                case ReportFieldTypeEnum.Text:
                    {
                        if (reportTreeNode.dbSortingField.ReportSorting != ReportSortingEnum.Error)
                        {
                            sb.Append(" " + GetReportSortingText(reportTreeNode.dbSortingField.ReportSorting) + " " + reportTreeNode.dbSortingField.Ordinal.ToString());
                        }
                        sb.Append(GetFieldDBFormatText(reportTreeNode));
                        foreach (ReportConditionTextField dbFilteringTextField in reportTreeNode.dbFilteringTextFieldList)
                        {
                            if (dbFilteringTextField.ReportCondition != ReportConditionEnum.Error)
                            {
                                sb.Append(" " + GetReportConditionText(dbFilteringTextField.ReportCondition));
                                if (!string.IsNullOrWhiteSpace(dbFilteringTextField.TextCondition))
                                {
                                    sb.Append(" " + dbFilteringTextField.TextCondition.Replace(" ", "*"));
                                }
                            }
                        }
                    }
                    break;
                case ReportFieldTypeEnum.TrueOrFalse:
                    {
                        if (reportTreeNode.dbSortingField.ReportSorting != ReportSortingEnum.Error)
                        {
                            sb.Append(" " + GetReportSortingText(reportTreeNode.dbSortingField.ReportSorting) + " " + reportTreeNode.dbSortingField.Ordinal.ToString());
                        }
                        sb.Append(GetFieldDBFormatText(reportTreeNode));
                        foreach (ReportConditionTrueFalseField dbFilteringTrueFalseField in reportTreeNode.dbFilteringTrueFalseFieldList)
                        {
                            if (dbFilteringTrueFalseField.ReportCondition != ReportConditionEnum.Error)
                            {
                                sb.Append(" " + GetReportConditionText(dbFilteringTrueFalseField.ReportCondition));
                            }
                        }
                    }
                    break;
                case ReportFieldTypeEnum.FilePurpose:
                case ReportFieldTypeEnum.FileType:
                case ReportFieldTypeEnum.TranslationStatus:
                case ReportFieldTypeEnum.BoxModelResultType:
                case ReportFieldTypeEnum.InfrastructureType:
                case ReportFieldTypeEnum.FacilityType:
                case ReportFieldTypeEnum.AerationType:
                case ReportFieldTypeEnum.PreliminaryTreatmentType:
                case ReportFieldTypeEnum.PrimaryTreatmentType:
                case ReportFieldTypeEnum.SecondaryTreatmentType:
                case ReportFieldTypeEnum.TertiaryTreatmentType:
                case ReportFieldTypeEnum.TreatmentType:
                case ReportFieldTypeEnum.DisinfectionType:
                case ReportFieldTypeEnum.CollectionSystemType:
                case ReportFieldTypeEnum.AlarmSystemType:
                case ReportFieldTypeEnum.ScenarioStatus:
                case ReportFieldTypeEnum.StorageDataType:
                case ReportFieldTypeEnum.Language:
                case ReportFieldTypeEnum.SampleType:
                case ReportFieldTypeEnum.BeaufortScale:
                case ReportFieldTypeEnum.AnalyzeMethod:
                case ReportFieldTypeEnum.SampleMatrix:
                case ReportFieldTypeEnum.Laboratory:
                case ReportFieldTypeEnum.SampleStatus:
                case ReportFieldTypeEnum.SamplingPlanType:
                case ReportFieldTypeEnum.LabSheetType:
                case ReportFieldTypeEnum.LabSheetStatus:
                case ReportFieldTypeEnum.PolSourceInactiveReason:
                case ReportFieldTypeEnum.PolSourceObsInfo:
                case ReportFieldTypeEnum.AddressType:
                case ReportFieldTypeEnum.StreetType:
                case ReportFieldTypeEnum.ContactTitle:
                case ReportFieldTypeEnum.EmailType:
                case ReportFieldTypeEnum.TelType:
                case ReportFieldTypeEnum.TideText:
                case ReportFieldTypeEnum.TideDataType:
                case ReportFieldTypeEnum.SpecialTableType:
                case ReportFieldTypeEnum.MWQMSiteLatestClassification:
                case ReportFieldTypeEnum.PolSourceIssueRisk:
                case ReportFieldTypeEnum.MikeScenarioSpecialResultKMLType:
                    {
                        if (reportTreeNode.dbSortingField.ReportSorting != ReportSortingEnum.Error)
                        {
                            sb.Append(" " + GetReportSortingText(reportTreeNode.dbSortingField.ReportSorting) + " " + reportTreeNode.dbSortingField.Ordinal.ToString());
                        }
                        sb.Append(GetFieldDBFormatText(reportTreeNode));
                        foreach (ReportConditionEnumField dbFilteringEnumField in reportTreeNode.dbFilteringEnumFieldList)
                        {
                            if (!string.IsNullOrWhiteSpace(dbFilteringEnumField.EnumConditionText))
                            {
                                sb.Append(" " + GetReportConditionText(dbFilteringEnumField.ReportCondition));
                                if (!string.IsNullOrWhiteSpace(dbFilteringEnumField.EnumConditionText))
                                {
                                    sb.Append(" " + dbFilteringEnumField.EnumConditionText);
                                }
                            }
                        }
                    }
                    break;
                default:
                    {
                        sb.Append(" Error" + reportTreeNode.Text);
                    }
                    break;
            }
        }
        public string GetFieldDBFormatText(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode == null)
                return "";

            switch (reportTreeNode.ReportFieldType)
            {
                case ReportFieldTypeEnum.Error:
                    return "";
                case ReportFieldTypeEnum.DateAndTime:
                    {
                        if (reportTreeNode.dbFormatingField.ReportFormatingDate != ReportFormatingDateEnum.Error)
                        {
                            return " FORMAT " + GetDateFormatText(reportTreeNode.dbFormatingField.ReportFormatingDate).Replace(" ", "*");
                        }
                    }
                    break;
                case ReportFieldTypeEnum.NumberWhole:
                    return "";
                case ReportFieldTypeEnum.NumberWithDecimal:
                    {
                        if (reportTreeNode.ReportFieldType == ReportFieldTypeEnum.NumberWithDecimal)
                        {
                            if (reportTreeNode.dbFormatingField.ReportFormatingNumber != ReportFormatingNumberEnum.Error)
                            {
                                return " FORMAT " + GetNumberFormatText(reportTreeNode.dbFormatingField.ReportFormatingNumber).Replace(" ", "*");
                            }
                        }
                        else
                        {
                            return "";
                        }
                    }
                    break;
                case ReportFieldTypeEnum.Text:
                    return "";
                case ReportFieldTypeEnum.TrueOrFalse:
                    return "";
                case ReportFieldTypeEnum.FilePurpose:
                    return "";
                case ReportFieldTypeEnum.FileType:
                    return "";
                case ReportFieldTypeEnum.TranslationStatus:
                    return "";
                case ReportFieldTypeEnum.BoxModelResultType:
                    return "";
                case ReportFieldTypeEnum.InfrastructureType:
                    return "";
                case ReportFieldTypeEnum.FacilityType:
                    return "";
                case ReportFieldTypeEnum.AerationType:
                    return "";
                case ReportFieldTypeEnum.PreliminaryTreatmentType:
                    return "";
                case ReportFieldTypeEnum.PrimaryTreatmentType:
                    return "";
                case ReportFieldTypeEnum.SecondaryTreatmentType:
                    return "";
                case ReportFieldTypeEnum.TertiaryTreatmentType:
                    return "";
                case ReportFieldTypeEnum.TreatmentType:
                    return "";
                case ReportFieldTypeEnum.DisinfectionType:
                    return "";
                case ReportFieldTypeEnum.CollectionSystemType:
                    return "";
                case ReportFieldTypeEnum.AlarmSystemType:
                    return "";
                case ReportFieldTypeEnum.ScenarioStatus:
                    return "";
                case ReportFieldTypeEnum.StorageDataType:
                    return "";
                case ReportFieldTypeEnum.Language:
                    return "";
                case ReportFieldTypeEnum.SampleType:
                    return "";
                case ReportFieldTypeEnum.BeaufortScale:
                    return "";
                case ReportFieldTypeEnum.AnalyzeMethod:
                    return "";
                case ReportFieldTypeEnum.SampleMatrix:
                    return "";
                case ReportFieldTypeEnum.Laboratory:
                    return "";
                case ReportFieldTypeEnum.SampleStatus:
                    return "";
                case ReportFieldTypeEnum.SamplingPlanType:
                    return "";
                case ReportFieldTypeEnum.LabSheetType:
                    return "";
                case ReportFieldTypeEnum.LabSheetStatus:
                    return "";
                case ReportFieldTypeEnum.PolSourceInactiveReason:
                    return "";
                case ReportFieldTypeEnum.PolSourceObsInfo:
                    return "";
                case ReportFieldTypeEnum.AddressType:
                    return "";
                case ReportFieldTypeEnum.StreetType:
                    return "";
                case ReportFieldTypeEnum.ContactTitle:
                    return "";
                case ReportFieldTypeEnum.EmailType:
                    return "";
                case ReportFieldTypeEnum.TelType:
                    return "";
                case ReportFieldTypeEnum.TideText:
                    return "";
                case ReportFieldTypeEnum.TideDataType:
                    return "";
                case ReportFieldTypeEnum.SpecialTableType:
                    return "";
                case ReportFieldTypeEnum.MWQMSiteLatestClassification:
                    return "";
                case ReportFieldTypeEnum.PolSourceIssueRisk:
                    return "";
                case ReportFieldTypeEnum.MikeScenarioSpecialResultKMLType:
                    return "";
                default:
                    return " Error" + reportTreeNode.Text;
            }

            return "";
        }
        public string GetFieldReportFormatText(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode == null)
                return "";

            switch (reportTreeNode.ReportFieldType)
            {
                case ReportFieldTypeEnum.Error:
                    return "";
                case ReportFieldTypeEnum.DateAndTime:
                    {
                        if (reportTreeNode.reportFormatingField.ReportFormatingDate != ReportFormatingDateEnum.Error)
                        {
                            return " FORMAT " + GetDateFormatText(reportTreeNode.reportFormatingField.ReportFormatingDate).Replace(" ", "*");
                        }
                    }
                    break;
                case ReportFieldTypeEnum.NumberWhole:
                    return "";
                case ReportFieldTypeEnum.NumberWithDecimal:
                    {
                        if (reportTreeNode.ReportFieldType == ReportFieldTypeEnum.NumberWithDecimal)
                        {
                            if (reportTreeNode.reportFormatingField.ReportFormatingNumber != ReportFormatingNumberEnum.Error)
                            {
                                return " FORMAT " + GetNumberFormatText(reportTreeNode.reportFormatingField.ReportFormatingNumber).Replace(" ", "*");
                            }
                        }
                        else
                        {
                            return "";
                        }
                    }
                    break;
                case ReportFieldTypeEnum.Text:
                    return "";
                case ReportFieldTypeEnum.TrueOrFalse:
                    return "";
                case ReportFieldTypeEnum.FilePurpose:
                    return "";
                case ReportFieldTypeEnum.FileType:
                    return "";
                case ReportFieldTypeEnum.TranslationStatus:
                    return "";
                case ReportFieldTypeEnum.BoxModelResultType:
                    return "";
                case ReportFieldTypeEnum.InfrastructureType:
                    return "";
                case ReportFieldTypeEnum.FacilityType:
                    return "";
                case ReportFieldTypeEnum.AerationType:
                    return "";
                case ReportFieldTypeEnum.PreliminaryTreatmentType:
                    return "";
                case ReportFieldTypeEnum.PrimaryTreatmentType:
                    return "";
                case ReportFieldTypeEnum.SecondaryTreatmentType:
                    return "";
                case ReportFieldTypeEnum.TertiaryTreatmentType:
                    return "";
                case ReportFieldTypeEnum.TreatmentType:
                    return "";
                case ReportFieldTypeEnum.DisinfectionType:
                    return "";
                case ReportFieldTypeEnum.CollectionSystemType:
                    return "";
                case ReportFieldTypeEnum.AlarmSystemType:
                    return "";
                case ReportFieldTypeEnum.ScenarioStatus:
                    return "";
                case ReportFieldTypeEnum.StorageDataType:
                    return "";
                case ReportFieldTypeEnum.Language:
                    return "";
                case ReportFieldTypeEnum.SampleType:
                    return "";
                case ReportFieldTypeEnum.BeaufortScale:
                    return "";
                case ReportFieldTypeEnum.AnalyzeMethod:
                    return "";
                case ReportFieldTypeEnum.SampleMatrix:
                    return "";
                case ReportFieldTypeEnum.Laboratory:
                    return "";
                case ReportFieldTypeEnum.SampleStatus:
                    return "";
                case ReportFieldTypeEnum.SamplingPlanType:
                    return "";
                case ReportFieldTypeEnum.LabSheetType:
                    return "";
                case ReportFieldTypeEnum.LabSheetStatus:
                    return "";
                case ReportFieldTypeEnum.PolSourceInactiveReason:
                    return "";
                case ReportFieldTypeEnum.PolSourceObsInfo:
                    return "";
                case ReportFieldTypeEnum.AddressType:
                    return "";
                case ReportFieldTypeEnum.StreetType:
                    return "";
                case ReportFieldTypeEnum.ContactTitle:
                    return "";
                case ReportFieldTypeEnum.EmailType:
                    return "";
                case ReportFieldTypeEnum.TelType:
                    return "";
                case ReportFieldTypeEnum.TideText:
                    return "";
                case ReportFieldTypeEnum.TideDataType:
                    return "";
                case ReportFieldTypeEnum.SpecialTableType:
                    return "";
                case ReportFieldTypeEnum.MWQMSiteLatestClassification:
                    return "";
                case ReportFieldTypeEnum.PolSourceIssueRisk:
                    return "";
                case ReportFieldTypeEnum.MikeScenarioSpecialResultKMLType:
                    return "";
                default:
                    return " Error" + reportTreeNode.Text;
            }

            return "";
        }
        public string GetFieldReportConditionText(ReportTreeNode reportTreeNode)
        {
            StringBuilder sb = new StringBuilder();
            if (reportTreeNode == null)
                return "";

            switch (reportTreeNode.ReportFieldType)
            {
                case ReportFieldTypeEnum.Error:
                    return "";
                case ReportFieldTypeEnum.DateAndTime:
                    {
                        foreach (ReportConditionDateField reportConditionDateField in reportTreeNode.reportConditionDateFieldList)
                        {
                            if (reportConditionDateField.ReportCondition != ReportConditionEnum.Error)
                            {
                                sb.Append(" " + GetReportConditionText(reportConditionDateField.ReportCondition));
                                if (reportConditionDateField.DateTimeConditionYear != null && reportConditionDateField.DateTimeConditionYear != 0)
                                {
                                    sb.Append(" YEAR " + reportConditionDateField.DateTimeConditionYear.ToString());
                                }
                                if (reportConditionDateField.DateTimeConditionMonth != null && reportConditionDateField.DateTimeConditionMonth != 0)
                                {
                                    sb.Append(" MONTH " + reportConditionDateField.DateTimeConditionMonth.ToString());
                                }
                                if (reportConditionDateField.DateTimeConditionDay != null && reportConditionDateField.DateTimeConditionDay != 0)
                                {
                                    sb.Append(" DAY " + reportConditionDateField.DateTimeConditionDay.ToString());
                                }
                                if (reportConditionDateField.DateTimeConditionHour != null && reportConditionDateField.DateTimeConditionHour != 0)
                                {
                                    sb.Append(" HOUR " + reportConditionDateField.DateTimeConditionHour.ToString());
                                }
                                if (reportConditionDateField.DateTimeConditionMinute != null && reportConditionDateField.DateTimeConditionMinute != 0)
                                {
                                    sb.Append(" MINUTE " + reportConditionDateField.DateTimeConditionMinute.ToString());
                                }
                            }
                        }
                    }
                    break;
                case ReportFieldTypeEnum.NumberWhole:
                case ReportFieldTypeEnum.NumberWithDecimal:
                    {
                        foreach (ReportConditionNumberField reportConditionNumberField in reportTreeNode.reportConditionNumberFieldList)
                        {
                            if (reportConditionNumberField.ReportCondition != ReportConditionEnum.Error)
                            {
                                sb.Append(" " + GetReportConditionText(reportConditionNumberField.ReportCondition)
                                    + " " + (LanguageRequest == LanguageEnum.fr
                                        ? reportConditionNumberField.NumberCondition.ToString().Replace(",", ".")
                                        : reportConditionNumberField.NumberCondition.ToString()));
                            }
                        }
                    }
                    break;
                case ReportFieldTypeEnum.Text:
                    {
                        foreach (ReportConditionTextField reportConditionTextField in reportTreeNode.reportConditionTextFieldList)
                        {
                            if (reportConditionTextField.ReportCondition != ReportConditionEnum.Error)
                            {
                                if (!string.IsNullOrWhiteSpace(reportConditionTextField.TextCondition))
                                {
                                    sb.Append(" " + GetReportConditionText(reportConditionTextField.ReportCondition) + " " + reportConditionTextField.TextCondition);
                                }
                            }
                        }
                    }
                    break;
                case ReportFieldTypeEnum.TrueOrFalse:
                    {
                        foreach (ReportConditionTrueFalseField reportConditionTrueFalseField in reportTreeNode.reportConditionTrueFalseFieldList)
                        {
                            if (reportConditionTrueFalseField.ReportCondition != ReportConditionEnum.Error)
                            {
                                sb.Append(" " + GetReportConditionText(reportConditionTrueFalseField.ReportCondition));
                            }
                        }
                    }
                    break;
                case ReportFieldTypeEnum.FilePurpose:
                case ReportFieldTypeEnum.FileType:
                case ReportFieldTypeEnum.TranslationStatus:
                case ReportFieldTypeEnum.BoxModelResultType:
                case ReportFieldTypeEnum.InfrastructureType:
                case ReportFieldTypeEnum.FacilityType:
                case ReportFieldTypeEnum.AerationType:
                case ReportFieldTypeEnum.PreliminaryTreatmentType:
                case ReportFieldTypeEnum.PrimaryTreatmentType:
                case ReportFieldTypeEnum.SecondaryTreatmentType:
                case ReportFieldTypeEnum.TertiaryTreatmentType:
                case ReportFieldTypeEnum.TreatmentType:
                case ReportFieldTypeEnum.DisinfectionType:
                case ReportFieldTypeEnum.CollectionSystemType:
                case ReportFieldTypeEnum.AlarmSystemType:
                case ReportFieldTypeEnum.ScenarioStatus:
                case ReportFieldTypeEnum.StorageDataType:
                case ReportFieldTypeEnum.Language:
                case ReportFieldTypeEnum.SampleType:
                case ReportFieldTypeEnum.BeaufortScale:
                case ReportFieldTypeEnum.AnalyzeMethod:
                case ReportFieldTypeEnum.SampleMatrix:
                case ReportFieldTypeEnum.Laboratory:
                case ReportFieldTypeEnum.SampleStatus:
                case ReportFieldTypeEnum.SamplingPlanType:
                case ReportFieldTypeEnum.LabSheetType:
                case ReportFieldTypeEnum.LabSheetStatus:
                case ReportFieldTypeEnum.PolSourceInactiveReason:
                case ReportFieldTypeEnum.PolSourceObsInfo:
                case ReportFieldTypeEnum.AddressType:
                case ReportFieldTypeEnum.StreetType:
                case ReportFieldTypeEnum.ContactTitle:
                case ReportFieldTypeEnum.EmailType:
                case ReportFieldTypeEnum.TelType:
                case ReportFieldTypeEnum.TideText:
                case ReportFieldTypeEnum.TideDataType:
                case ReportFieldTypeEnum.SpecialTableType:
                case ReportFieldTypeEnum.MWQMSiteLatestClassification:
                case ReportFieldTypeEnum.PolSourceIssueRisk:
                case ReportFieldTypeEnum.MikeScenarioSpecialResultKMLType:
                    {
                        foreach (ReportConditionEnumField reportConditionEnumField in reportTreeNode.reportConditionEnumFieldList)
                        {
                            if (reportConditionEnumField.ReportCondition == ReportConditionEnum.ReportConditionEqual)
                            {
                                sb.Append(reportTreeNode.Text);
                                if (!string.IsNullOrWhiteSpace(reportConditionEnumField.EnumConditionText))
                                {
                                    sb.Append(" " + GetReportConditionText(reportConditionEnumField.ReportCondition) + " " + reportConditionEnumField.EnumConditionText);
                                }
                            }
                        }
                    }
                    break;
                default:
                    {
                        sb.Append(" Error " + reportTreeNode.Text);
                    }
                    break;
            }

            return sb.ToString();
        }
        public Type GetFieldType(string FieldName, Type TagType)
        {
            foreach (PropertyInfo propInfo in TagType.GetProperties())
            {
                if (FieldName == propInfo.Name)
                {
                    return propInfo.PropertyType;
                }
            }

            return null;
        }
        public string GetFieldTypeStr(string FieldName, Type TagType)
        {
            foreach (PropertyInfo propInfo in TagType.GetProperties())
            {
                if (FieldName == propInfo.Name)
                {
                    string propTypeName = propInfo.PropertyType.ToString();
                    if (propTypeName.Contains("[["))
                    {
                        propTypeName = propTypeName.Substring(propTypeName.IndexOf("[[") + 2);
                        propTypeName = propTypeName.Substring(0, propTypeName.IndexOf("]]"));
                    }
                    else if (propTypeName.Contains("["))
                    {
                        propTypeName = propTypeName.Substring(propTypeName.IndexOf("[") + 1);
                        propTypeName = propTypeName.Substring(0, propTypeName.IndexOf("]"));
                    }
                    else
                    {
                        // nothing
                    }

                    return propTypeName;
                }
            }

            return string.Format(ReportServiceRes._DoesNotExistFor_, FieldName, TagType.ToString());
        }
        public string GetFormatDate(ReportFormatingDateEnum reportFormatingDate)
        {
            switch (reportFormatingDate)
            {
                case ReportFormatingDateEnum.ReportFormatingDateYearOnly:
                    return DateTime.Now.ToString("yyyy");
                case ReportFormatingDateEnum.ReportFormatingDateMonthDecimalOnly:
                    return DateTime.Now.ToString("MM");
                case ReportFormatingDateEnum.ReportFormatingDateMonthShortTextOnly:
                    return DateTime.Now.ToString("MMM");
                case ReportFormatingDateEnum.ReportFormatingDateMonthFullTextOnly:
                    return DateTime.Now.ToString("MMMM");
                case ReportFormatingDateEnum.ReportFormatingDateDayOnly:
                    return DateTime.Now.ToString("dd");
                case ReportFormatingDateEnum.ReportFormatingDateHourOnly:
                    return DateTime.Now.ToString("HH");
                case ReportFormatingDateEnum.ReportFormatingDateMinuteOnly:
                    return DateTime.Now.ToString("mm");
                case ReportFormatingDateEnum.ReportFormatingDateYearMonthDecimalDay:
                    return DateTime.Now.ToString("yyyy MM dd");
                case ReportFormatingDateEnum.ReportFormatingDateYearMonthShortTextDay:
                    return DateTime.Now.ToString("yyyy MMM dd");
                case ReportFormatingDateEnum.ReportFormatingDateYearMonthFullTextDay:
                    return DateTime.Now.ToString("yyyy MMMM dd");
                case ReportFormatingDateEnum.ReportFormatingDateYearMonthDecimalDayHourMinute:
                    return DateTime.Now.ToString("yyyy MM dd HH:mm");
                case ReportFormatingDateEnum.ReportFormatingDateYearMonthShortTextDayHourMinute:
                    return DateTime.Now.ToString("yyyy MMM dd HH:mm");
                case ReportFormatingDateEnum.ReportFormatingDateYearMonthFullTextDayHourMinute:
                    return DateTime.Now.ToString("yyyy MMMM dd HH:mm");
                default:
                    return "";
            }
        }
        public string GetFormatNumber(ReportFormatingNumberEnum reportFormatingNumber)
        {
            double TheNumber = 123456.123456D;

            switch (reportFormatingNumber)
            {
                case ReportFormatingNumberEnum.ReportFormatingNumber0Decimal:
                    return TheNumber.ToString("F0");
                case ReportFormatingNumberEnum.ReportFormatingNumber1Decimal:
                    return TheNumber.ToString("F1");
                case ReportFormatingNumberEnum.ReportFormatingNumber2Decimal:
                    return TheNumber.ToString("F2");
                case ReportFormatingNumberEnum.ReportFormatingNumber3Decimal:
                    return TheNumber.ToString("F3");
                case ReportFormatingNumberEnum.ReportFormatingNumber4Decimal:
                    return TheNumber.ToString("F4");
                case ReportFormatingNumberEnum.ReportFormatingNumber5Decimal:
                    return TheNumber.ToString("F5");
                case ReportFormatingNumberEnum.ReportFormatingNumber6Decimal:
                    return TheNumber.ToString("F6");
                case ReportFormatingNumberEnum.ReportFormatingNumberScientific0Decimal:
                    return TheNumber.ToString("e0");
                case ReportFormatingNumberEnum.ReportFormatingNumberScientific1Decimal:
                    return TheNumber.ToString("e1");
                case ReportFormatingNumberEnum.ReportFormatingNumberScientific2Decimal:
                    return TheNumber.ToString("e2");
                case ReportFormatingNumberEnum.ReportFormatingNumberScientific3Decimal:
                    return TheNumber.ToString("e3");
                case ReportFormatingNumberEnum.ReportFormatingNumberScientific4Decimal:
                    return TheNumber.ToString("e4");
                case ReportFormatingNumberEnum.ReportFormatingNumberScientific5Decimal:
                    return TheNumber.ToString("e5");
                case ReportFormatingNumberEnum.ReportFormatingNumberScientific6Decimal:
                    return TheNumber.ToString("e6");
                default:
                    return "";
            }
        }
        public string GetNumberFormatText(ReportFormatingNumberEnum reportFormatingNumber)
        {
            switch (reportFormatingNumber)
            {
                case ReportFormatingNumberEnum.ReportFormatingNumber0Decimal:
                    return "F0";
                case ReportFormatingNumberEnum.ReportFormatingNumber1Decimal:
                    return "F1";
                case ReportFormatingNumberEnum.ReportFormatingNumber2Decimal:
                    return "F2";
                case ReportFormatingNumberEnum.ReportFormatingNumber3Decimal:
                    return "F3";
                case ReportFormatingNumberEnum.ReportFormatingNumber4Decimal:
                    return "F4";
                case ReportFormatingNumberEnum.ReportFormatingNumber5Decimal:
                    return "F5";
                case ReportFormatingNumberEnum.ReportFormatingNumber6Decimal:
                    return "F6";
                case ReportFormatingNumberEnum.ReportFormatingNumberScientific0Decimal:
                    return "e0";
                case ReportFormatingNumberEnum.ReportFormatingNumberScientific1Decimal:
                    return "e1";
                case ReportFormatingNumberEnum.ReportFormatingNumberScientific2Decimal:
                    return "e2";
                case ReportFormatingNumberEnum.ReportFormatingNumberScientific3Decimal:
                    return "e3";
                case ReportFormatingNumberEnum.ReportFormatingNumberScientific4Decimal:
                    return "e4";
                case ReportFormatingNumberEnum.ReportFormatingNumberScientific5Decimal:
                    return "e5";
                case ReportFormatingNumberEnum.ReportFormatingNumberScientific6Decimal:
                    return "e6";
                default:
                    return "";
            }
        }
        public string GetReportConditionText(ReportConditionEnum reportCondition)
        {
            switch (reportCondition)
            {
                case ReportConditionEnum.Error:
                    return "";
                case ReportConditionEnum.ReportConditionTrue:
                    return "TRUE";
                case ReportConditionEnum.ReportConditionFalse:
                    return "FALSE";
                case ReportConditionEnum.ReportConditionContain:
                    return "CONTAIN";
                case ReportConditionEnum.ReportConditionStart:
                    return "START";
                case ReportConditionEnum.ReportConditionEnd:
                    return "END";
                case ReportConditionEnum.ReportConditionBigger:
                    return "BIGGER";
                case ReportConditionEnum.ReportConditionSmaller:
                    return "SMALLER";
                case ReportConditionEnum.ReportConditionEqual:
                    return "EQUAL";
                default:
                    return "";
            }
        }
        public string GetReportSortingText(ReportSortingEnum reportSorting)
        {
            switch (reportSorting)
            {
                case ReportSortingEnum.Error:
                    return "";
                case ReportSortingEnum.ReportSortingAscending:
                    return "ASCENDING";
                case ReportSortingEnum.ReportSortingDescending:
                    return "DESCENDING";
                default:
                    return "";
            }
        }
        public string GetReportTreeNodesForField(List<string> strList, ReportTreeNode reportTreeNode, Type TagType, bool IsDBFiltering, int LineCount)
        {
            reportTreeNode.Text = strList[0];
            string TypeName = GetFieldTypeStr(strList[0], TagType);
            switch (TypeName)
            {
                case "System.Boolean":
                    {
                        reportTreeNode.ReportFieldType = ReportFieldTypeEnum.TrueOrFalse;
                        if (strList.Count > 1)
                        {
                            int Current = 1;
                            string retStr = "";
                            if (!(AllowableSortingFilters.Contains(strList[Current]) || AllowableTrueFalseFilters.Contains(strList[Current])))
                                return string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, 2, String.Join(",", AllowableSortingFilters.Concat(AllowableTrueFalseFilters)));

                            if (AllowableSortingFilters.Contains(strList[Current]))
                            {
                                reportTreeNode.dbSortingField = new ReportSortingField();
                                retStr = SetReportTreeNodeSorting(Current, reportTreeNode.dbSortingField, reportTreeNode, strList, LineCount);
                                if (!string.IsNullOrWhiteSpace(retStr))
                                    return retStr;

                                Current += 2;
                            }

                            if (strList.Count > Current)
                            {
                                retStr = GetAllTheTrueFalseFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                                if (!string.IsNullOrWhiteSpace(retStr))
                                    return retStr;
                            }

                        }
                    }
                    break;
                case "System.DateTime":
                    reportTreeNode.ReportFieldType = ReportFieldTypeEnum.DateAndTime;
                    if (strList.Count > 1)
                    {
                        int Current = 1;
                        string retStr = "";
                        if (!(AllowableSortingFilters.Contains(strList[Current]) || AllowableBasicFilters.Contains(strList[Current]) || AllowableFormatingFilters.Contains(strList[Current])))
                            return string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, 2, String.Join(",", AllowableSortingFilters.Concat(AllowableFormatingFilters.Concat(AllowableBasicFilters))));

                        if (AllowableSortingFilters.Contains(strList[Current]))
                        {
                            reportTreeNode.dbSortingField = new ReportSortingField();
                            retStr = SetReportTreeNodeSorting(Current, reportTreeNode.dbSortingField, reportTreeNode, strList, LineCount);
                            if (!string.IsNullOrWhiteSpace(retStr))
                                return retStr;

                            Current += 2;
                        }

                        if (strList.Count > Current)
                        {
                            if (AllowableFormatingFilters.Contains(strList[Current]))
                            {
                                reportTreeNode.dbFormatingField = new ReportFormatingField();
                                retStr = SetReportTreeNodeFormating(Current, reportTreeNode.dbFormatingField, reportTreeNode, strList, LineCount);
                                if (!string.IsNullOrWhiteSpace(retStr))
                                {
                                    return retStr;
                                }

                                Current += 2;
                            }
                        }

                        if (strList.Count > Current)
                        {
                            retStr = GetAllTheDateFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                            if (!string.IsNullOrWhiteSpace(retStr))
                                return retStr;
                        }
                    }
                    break;
                case "System.Single":
                case "System.Int32":
                    {
                        if (TypeName == "System.Single")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.NumberWithDecimal;
                        }
                        else if (TypeName == "System.Int32")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.NumberWhole;
                        }
                        if (strList.Count > 1)
                        {
                            int Current = 1;
                            string retStr = "";
                            if (!(AllowableSortingFilters.Contains(strList[Current]) || AllowableBasicFilters.Contains(strList[Current]) || AllowableFormatingFilters.Contains(strList[Current])))
                                return string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, 2, String.Join(",", AllowableSortingFilters.Concat(AllowableFormatingFilters.Concat(AllowableBasicFilters))));

                            if (AllowableSortingFilters.Contains(strList[Current]))
                            {
                                reportTreeNode.dbSortingField = new ReportSortingField();
                                retStr = SetReportTreeNodeSorting(Current, reportTreeNode.dbSortingField, reportTreeNode, strList, LineCount);
                                if (!string.IsNullOrWhiteSpace(retStr))
                                    return retStr;

                                Current += 2;
                            }

                            if (strList.Count > Current)
                            {
                                if (AllowableFormatingFilters.Contains(strList[Current]))
                                {
                                    reportTreeNode.dbFormatingField = new ReportFormatingField();
                                    retStr = SetReportTreeNodeFormating(Current, reportTreeNode.dbFormatingField, reportTreeNode, strList, LineCount);
                                    if (!string.IsNullOrWhiteSpace(retStr))
                                    {
                                        return retStr;
                                    }

                                    Current += 2;
                                }
                            }

                            if (strList.Count > Current)
                            {
                                retStr = GetAllTheNumberFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                                if (!string.IsNullOrWhiteSpace(retStr))
                                    return retStr;

                            }
                        }
                    }
                    break;
                case "System.String":
                    {
                        reportTreeNode.ReportFieldType = ReportFieldTypeEnum.Text;
                        if (strList.Count > 1)
                        {
                            int Current = 1;
                            string retStr = "";
                            if (!(AllowableSortingFilters.Contains(strList[Current]) || AllowableTextFilters.Contains(strList[Current])))
                                return string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, 2, String.Join(",", AllowableSortingFilters.Concat(AllowableTextFilters)));

                            if (AllowableSortingFilters.Contains(strList[Current]))
                            {
                                reportTreeNode.dbSortingField = new ReportSortingField();
                                retStr = SetReportTreeNodeSorting(Current, reportTreeNode.dbSortingField, reportTreeNode, strList, LineCount);
                                if (!string.IsNullOrWhiteSpace(retStr))
                                    return retStr;

                                Current += 2;
                            }

                            if (strList.Count > Current)
                            {
                                retStr = GetAllTheTextFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                                if (!string.IsNullOrWhiteSpace(retStr))
                                    return retStr;
                            }
                        }
                    }
                    break;
                case "CSSPEnumsDLL.Enums.FilePurposeEnum":
                case "CSSPEnumsDLL.Enums.FileTypeEnum":
                case "CSSPEnumsDLL.Enums.TranslationStatusEnum":
                case "CSSPEnumsDLL.Enums.BoxModelResultTypeEnum":
                case "CSSPEnumsDLL.Enums.InfrastructureTypeEnum":
                case "CSSPEnumsDLL.Enums.FacilityTypeEnum":
                case "CSSPEnumsDLL.Enums.AerationTypeEnum":
                case "CSSPEnumsDLL.Enums.PreliminaryTreatmentTypeEnum":
                case "CSSPEnumsDLL.Enums.PrimaryTreatmentTypeEnum":
                case "CSSPEnumsDLL.Enums.SecondaryTreatmentTypeEnum":
                case "CSSPEnumsDLL.Enums.TertiaryTreatmentTypeEnum":
                case "CSSPEnumsDLL.Enums.TreatmentTypeEnum":
                case "CSSPEnumsDLL.Enums.DisinfectionTypeEnum":
                case "CSSPEnumsDLL.Enums.CollectionSystemTypeEnum":
                case "CSSPEnumsDLL.Enums.AlarmSystemTypeEnum":
                case "CSSPEnumsDLL.Enums.ScenarioStatusEnum":
                case "CSSPEnumsDLL.Enums.StorageDataTypeEnum":
                case "CSSPEnumsDLL.Enums.LanguageEnum":
                case "CSSPEnumsDLL.Enums.SampleTypeEnum":
                case "CSSPEnumsDLL.Enums.BeaufortScaleEnum":
                case "CSSPEnumsDLL.Enums.AnalyzeMethodEnum":
                case "CSSPEnumsDLL.Enums.SampleMatrixEnum":
                case "CSSPEnumsDLL.Enums.LaboratoryEnum":
                case "CSSPEnumsDLL.Enums.SampleStatusEnum":
                case "CSSPEnumsDLL.Enums.SamplingPlanTypeEnum":
                case "CSSPEnumsDLL.Enums.LabSheetTypeEnum":
                case "CSSPEnumsDLL.Enums.LabSheetStatusEnum":
                case "CSSPEnumsDLL.Enums.PolSourceInactiveReasonEnum":
                case "CSSPEnumsDLL.Enums.PolSourceObsInfoEnum":
                case "CSSPEnumsDLL.Enums.AddressTypeEnum":
                case "CSSPEnumsDLL.Enums.StreetTypeEnum":
                case "CSSPEnumsDLL.Enums.ContactTitleEnum":
                case "CSSPEnumsDLL.Enums.EmailTypeEnum":
                case "CSSPEnumsDLL.Enums.TelTypeEnum":
                case "CSSPEnumsDLL.Enums.TideTextEnum":
                case "CSSPEnumsDLL.Enums.TideDataTypeEnum":
                case "CSSPEnumsDLL.Enums.SpecialTableTypeEnum":
                case "CSSPEnumsDLL.Enums.MWQMSiteLatestClassificationEnum":
                case "CSSPEnumsDLL.Enums.PolSourceIssueRiskEnum":
                case "CSSPEnumsDLL.Enums.MikeScenarioSpecialResultKMLTypeEnum":
                    {
                        if (TypeName == "CSSPEnumsDLL.Enums.FilePurposeEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.FilePurpose;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.FileTypeEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.FileType;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.TranslationStatusEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.TranslationStatus;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.BoxModelResultTypeEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.BoxModelResultType;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.InfrastructureTypeEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.InfrastructureType;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.FacilityTypeEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.FacilityType;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.AerationTypeEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.AerationType;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.PreliminaryTreatmentTypeEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.PreliminaryTreatmentType;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.PrimaryTreatmentTypeEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.PrimaryTreatmentType;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.SecondaryTreatmentTypeEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.SecondaryTreatmentType;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.TertiaryTreatmentTypeEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.TertiaryTreatmentType;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.TreatmentTypeEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.TreatmentType;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.DisinfectionTypeEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.DisinfectionType;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.CollectionSystemTypeEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.CollectionSystemType;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.AlarmSystemTypeEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.AlarmSystemType;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.ScenarioStatusEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.ScenarioStatus;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.StorageDataTypeEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.StorageDataType;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.LanguageEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.Language;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.SampleTypeEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.SampleType;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.BeaufortScaleEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.BeaufortScale;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.AnalyzeMethodEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.AnalyzeMethod;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.SampleMatrixEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.SampleMatrix;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.LaboratoryEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.Laboratory;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.SampleStatusEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.SampleStatus;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.SamplingPlanTypeEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.SamplingPlanType;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.LabSheetTypeEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.LabSheetType;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.LabSheetStatusEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.LabSheetStatus;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.PolSourceInactiveReasonEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.PolSourceInactiveReason;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.PolSourceObsInfoEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.PolSourceObsInfo;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.AddressTypeEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.AddressType;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.StreetTypeEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.StreetType;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.ContactTitleEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.ContactTitle;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.EmailTypeEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.EmailType;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.TelTypeEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.TelType;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.TideTextEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.TideText;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.TideDataTypeEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.TideDataType;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.SpecialTableTypeEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.SpecialTableType;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.MWQMSiteLatestClassificationEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.MWQMSiteLatestClassification;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.PolSourceIssueRiskEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.PolSourceIssueRisk;
                        }
                        else if (TypeName == "CSSPEnumsDLL.Enums.MikeScenarioSpecialResultKMLTypeEnum")
                        {
                            reportTreeNode.ReportFieldType = ReportFieldTypeEnum.MikeScenarioSpecialResultKMLType;
                        }
                        else
                        {
                            return TypeName + " not Implemented in GetReportTreeNodesCSV";
                        }

                        if (strList.Count > 1)
                        {
                            int Current = 1;
                            string retStr = "";
                            if (!(AllowableSortingFilters.Contains(strList[Current]) || AllowableEnumFilters.Contains(strList[Current])))
                                return string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, 2, String.Join(",", AllowableSortingFilters.Concat(AllowableEnumFilters)));

                            if (AllowableSortingFilters.Contains(strList[Current]))
                            {
                                reportTreeNode.dbSortingField = new ReportSortingField();
                                retStr = SetReportTreeNodeSorting(Current, reportTreeNode.dbSortingField, reportTreeNode, strList, LineCount);
                                if (!string.IsNullOrWhiteSpace(retStr))
                                    return retStr;

                                Current += 2;
                            }

                            if (strList.Count > Current)
                            {
                                retStr = GetAllTheEnumFilters(Current, reportTreeNode, strList, IsDBFiltering, LineCount);
                                if (!string.IsNullOrWhiteSpace(retStr))
                                    return retStr;
                            }
                        }
                    }
                    break;
                default:
                    return TypeName;
            }

            return "";
        }
        public string GetReportTreeNodesFromTagText(string TagText, string TagItem, Type TagType, List<ReportTreeNode> ReportTreeNodeList, bool IsDBFiltering)
        {
            // Also used by CSSPWebToolsDBDLL

            // Most of the tag structure should have been verified in FillReportTag

            int LineCount = 0;
            int CharCount = 0;
            using (TextReader tr = new StringReader(TagText))
            {
                string LineStr = tr.ReadLine();
                string TagName = "";
                while (LineStr != "")
                {
                    CharCount += LineStr.Length;
                    LineCount += 1;
                    if (LineCount == 1)
                    {
                        List<string> strList = LineStr.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
                        if (strList.Count > 0)
                            TagName = strList[0].Replace(Marker, "");
                    }
                    else
                    {
                        List<string> strList = LineStr.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

                        if (strList[0] == Marker)
                            break;

                        if (!strList[0].StartsWith(TagItem))
                            return string.Format(ReportServiceRes.Line_ShouldStartWith_, LineCount, TagItem);

                        ReportTreeNode reportTreeNode = new ReportTreeNode();

                        string retStr = GetReportTreeNodesForField(strList, reportTreeNode, TagType, IsDBFiltering, LineCount);
                        if (!string.IsNullOrWhiteSpace(retStr))
                            return retStr;

                        ReportTreeNodeList.Add(reportTreeNode);
                    }
                    LineStr = tr.ReadLine();
                }
            }
            return "";
        }
        public bool GetSubFieldIsChecked(ReportTreeNode reportTreeNode)
        {
            bool CheckExist = false;
            foreach (ReportTreeNode RTNFieldsHolder in reportTreeNode.Nodes)
            {
                if (RTNFieldsHolder.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.FieldsHolder)
                {
                    foreach (ReportTreeNode RTN in RTNFieldsHolder.Nodes)
                    {
                        if (RTN.Checked)
                        {
                            CheckExist = true;
                        }
                    }
                }
            }

            return CheckExist;
        }
        public ReportTreeNode FindReportTreeNode(ReportTreeNode reportTreeNode, string TagItem)
        {
            if (reportTreeNode.Text == TagItem)
            {
                return reportTreeNode;
            }

            foreach (ReportTreeNode RTN in reportTreeNode.Nodes)
            {
                ReportTreeNode FoundReportTreeNode = FindReportTreeNode(RTN, TagItem);

                if (FoundReportTreeNode != null)
                {
                    return FoundReportTreeNode;
                }
            }

            return null;
        }
        public bool IsConditionIntTrue(ReportConditionNumberField reportConditionField, int Int32Value)
        {
            if (reportConditionField.ReportCondition == ReportConditionEnum.ReportConditionEqual)
            {
                if (Int32Value != reportConditionField.NumberCondition.Value)
                {
                    return false;
                }
            }
            else if (reportConditionField.ReportCondition == ReportConditionEnum.ReportConditionBigger)
            {
                if (Int32Value <= reportConditionField.NumberCondition.Value)
                {
                    return false;
                }
            }
            else if (reportConditionField.ReportCondition == ReportConditionEnum.ReportConditionSmaller)
            {
                if (Int32Value >= reportConditionField.NumberCondition.Value)
                {
                    return false;
                }
            }

            return true;
        }
        public bool IsConditionSingleTrue(ReportConditionNumberField reportConditionField, float SingleValue)
        {
            if (reportConditionField.ReportCondition == ReportConditionEnum.ReportConditionEqual)
            {
                if (SingleValue != reportConditionField.NumberCondition.Value)
                {
                    return false;
                }
            }
            else if (reportConditionField.ReportCondition == ReportConditionEnum.ReportConditionBigger)
            {
                if (SingleValue <= reportConditionField.NumberCondition.Value)
                {
                    return false;
                }
            }
            else if (reportConditionField.ReportCondition == ReportConditionEnum.ReportConditionSmaller)
            {
                if (SingleValue >= reportConditionField.NumberCondition.Value)
                {
                    return false;
                }
            }

            return true;
        }
        public bool IsConditionStringTrue(ReportConditionTextField reportConditionField, string stringValue)
        {
            if (reportConditionField.ReportCondition == ReportConditionEnum.ReportConditionContain)
            {
                if (!stringValue.ToLower().Contains(reportConditionField.TextCondition.ToLower()))
                {
                    return false;
                }
            }
            else if (reportConditionField.ReportCondition == ReportConditionEnum.ReportConditionStart)
            {
                if (!stringValue.ToLower().StartsWith(reportConditionField.TextCondition.ToLower()))
                {
                    return false;
                }
            }
            else if (reportConditionField.ReportCondition == ReportConditionEnum.ReportConditionEnd)
            {
                if (!stringValue.ToLower().EndsWith(reportConditionField.TextCondition.ToLower()))
                {
                    return false;
                }
            }
            else if (reportConditionField.ReportCondition == ReportConditionEnum.ReportConditionEqual)
            {
                if (!stringValue.ToLower().Equals(reportConditionField.TextCondition.ToLower()))
                {
                    return false;
                }
            }
            return true;
        }
        public string LoadRecursiveTreeNode(ReportTreeNode reportTreeNode)
        {
            try
            {
                List<ReportTreeNode> reportTreeNodeList = new List<ReportTreeNode>();
                reportTreeNodeList = _CreateTreeViewService.LoadChildrenTreeNodes(reportTreeNode);

                reportTreeNode.ChildrenTreeNodeList = reportTreeNodeList;

                if (reportTreeNodeList != null && reportTreeNodeList.Count > 0)
                {
                    foreach (ReportTreeNode RTN in reportTreeNodeList)
                    {
                        if (_TreeViewTextList.Where(c => c == RTN.Text).Any())
                        {
                            return RTN.Text + " is double\r\n";
                        }
                        _TreeViewTextList.Add(RTN.Text);
                        if (RTN.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable)
                        {
                            RTN.ForeColor = Color.Green;
                        }
                        if (RTN.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.Field)
                        {
                            RTN.ForeColor = Color.Blue;
                        }
                        reportTreeNode.Nodes.Add(RTN);
                        if (!(RTN.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.Field))
                        {
                            LoadRecursiveTreeNode(RTN);
                        }
                    }
                }

            }
            catch (Exception)
            {
            }

            return "";
        }
        public bool ReturnKeepShow<T>(T reportModel, Type type, ReportTreeNode reportTreeNode, bool IsDBFiltering) where T : new()
        {
            bool KeepShow = true;
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                if (propertyInfo.Name == reportTreeNode.Text)
                {
                    if (propertyInfo.GetValue(reportModel) == null)
                    {
                        KeepShow = false;
                    }
                    else
                    {
                        string TypeName = GetFieldTypeStr(propertyInfo.Name, typeof(T));
                        switch (TypeName)
                        {
                            case "System.Boolean":
                                {
                                    List<ReportConditionTrueFalseField> reportConditionTrueFalseFieldList = new List<ReportConditionTrueFalseField>();
                                    if (IsDBFiltering)
                                    {
                                        reportConditionTrueFalseFieldList = reportTreeNode.dbFilteringTrueFalseFieldList;
                                    }
                                    else
                                    {
                                        reportConditionTrueFalseFieldList = reportTreeNode.reportConditionTrueFalseFieldList;
                                    }

                                    bool val = (bool)propertyInfo.GetValue(reportModel);
                                    if (reportConditionTrueFalseFieldList.Count > 0)
                                    {
                                        foreach (ReportConditionTrueFalseField reportConditionTrueFalseField in reportConditionTrueFalseFieldList)
                                        {
                                            switch (reportConditionTrueFalseField.ReportCondition)
                                            {
                                                case ReportConditionEnum.ReportConditionTrue:
                                                    {
                                                        if (val == false)
                                                        {
                                                            KeepShow = false;
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                case ReportConditionEnum.ReportConditionFalse:
                                                    {
                                                        if (val == true)
                                                        {
                                                            KeepShow = false;
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                    }
                                }
                                break;
                            case "System.DateTime":
                                {
                                    List<ReportConditionDateField> reportConditionDateFieldList = new List<ReportConditionDateField>();
                                    if (IsDBFiltering)
                                    {
                                        reportConditionDateFieldList = reportTreeNode.dbFilteringDateFieldList;
                                    }
                                    else
                                    {
                                        reportConditionDateFieldList = reportTreeNode.reportConditionDateFieldList;
                                    }

                                    DateTime val = (DateTime)propertyInfo.GetValue(reportModel);
                                    if (reportConditionDateFieldList.Count > 0)
                                    {
                                        foreach (ReportConditionDateField reportConditionDateField in reportConditionDateFieldList)
                                        {
                                            switch (reportConditionDateField.ReportCondition)
                                            {
                                                case ReportConditionEnum.ReportConditionBigger:
                                                    {
                                                        if (reportConditionDateField.DateTimeConditionYear != null)
                                                        {
                                                            DateTime condDateTime = new DateTime((int)reportConditionDateField.DateTimeConditionYear,
                                                                (reportConditionDateField.DateTimeConditionMonth == null ? 1 : (int)reportConditionDateField.DateTimeConditionMonth),
                                                                (reportConditionDateField.DateTimeConditionDay == null ? 1 : (int)reportConditionDateField.DateTimeConditionDay),
                                                                (reportConditionDateField.DateTimeConditionHour == null ? 0 : (int)reportConditionDateField.DateTimeConditionHour),
                                                                (reportConditionDateField.DateTimeConditionMinute == null ? 0 : (int)reportConditionDateField.DateTimeConditionMinute), 0);

                                                            if (val <= condDateTime)
                                                            {
                                                                KeepShow = false;
                                                                break;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (reportConditionDateField.DateTimeConditionMonth != null)
                                                            {
                                                                if (val.Month < reportConditionDateField.DateTimeConditionMonth)
                                                                {
                                                                    KeepShow = false;
                                                                    break;
                                                                }
                                                                else if (val.Month == reportConditionDateField.DateTimeConditionMonth)
                                                                {
                                                                    if (reportConditionDateField.DateTimeConditionDay != null)
                                                                    {
                                                                        if (val.Day < reportConditionDateField.DateTimeConditionDay)
                                                                        {
                                                                            KeepShow = false;
                                                                            break;
                                                                        }
                                                                        else if (val.Day == reportConditionDateField.DateTimeConditionDay)
                                                                        {
                                                                            if (reportConditionDateField.DateTimeConditionHour != null)
                                                                            {
                                                                                if (val.Hour < reportConditionDateField.DateTimeConditionHour)
                                                                                {
                                                                                    KeepShow = false;
                                                                                    break;
                                                                                }
                                                                                else if (val.Hour == reportConditionDateField.DateTimeConditionHour)
                                                                                {
                                                                                    if (reportConditionDateField.DateTimeConditionMinute != null)
                                                                                    {
                                                                                        if (val.Minute < reportConditionDateField.DateTimeConditionMinute)
                                                                                        {
                                                                                            KeepShow = false;
                                                                                            break;
                                                                                        }
                                                                                        else if (val.Minute == reportConditionDateField.DateTimeConditionMinute)
                                                                                        {
                                                                                            KeepShow = false;
                                                                                            break;
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (reportConditionDateField.DateTimeConditionDay != null)
                                                                {
                                                                    if (val.Day < reportConditionDateField.DateTimeConditionDay)
                                                                    {
                                                                        KeepShow = false;
                                                                        break;
                                                                    }
                                                                    else if (val.Day == reportConditionDateField.DateTimeConditionDay)
                                                                    {
                                                                        if (reportConditionDateField.DateTimeConditionHour != null)
                                                                        {
                                                                            if (val.Hour < reportConditionDateField.DateTimeConditionHour)
                                                                            {
                                                                                KeepShow = false;
                                                                                break;
                                                                            }
                                                                            else if (val.Hour == reportConditionDateField.DateTimeConditionHour)
                                                                            {
                                                                                if (reportConditionDateField.DateTimeConditionMinute != null)
                                                                                {
                                                                                    if (val.Minute < reportConditionDateField.DateTimeConditionMinute)
                                                                                    {
                                                                                        KeepShow = false;
                                                                                        break;
                                                                                    }
                                                                                    else if (val.Minute == reportConditionDateField.DateTimeConditionMinute)
                                                                                    {
                                                                                        KeepShow = false;
                                                                                        break;
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (reportConditionDateField.DateTimeConditionHour != null)
                                                                    {
                                                                        if (val.Hour < reportConditionDateField.DateTimeConditionHour)
                                                                        {
                                                                            KeepShow = false;
                                                                            break;
                                                                        }
                                                                        else if (val.Hour == reportConditionDateField.DateTimeConditionHour)
                                                                        {
                                                                            if (reportConditionDateField.DateTimeConditionMinute != null)
                                                                            {
                                                                                if (val.Minute < reportConditionDateField.DateTimeConditionMinute)
                                                                                {
                                                                                    KeepShow = false;
                                                                                    break;
                                                                                }
                                                                                else if (val.Minute == reportConditionDateField.DateTimeConditionMinute)
                                                                                {
                                                                                    KeepShow = false;
                                                                                    break;
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (reportConditionDateField.DateTimeConditionMinute != null)
                                                                        {
                                                                            if (val.Minute < reportConditionDateField.DateTimeConditionMinute)
                                                                            {
                                                                                KeepShow = false;
                                                                                break;
                                                                            }
                                                                            else if (val.Minute == reportConditionDateField.DateTimeConditionMinute)
                                                                            {
                                                                                KeepShow = false;
                                                                                break;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    break;
                                                case ReportConditionEnum.ReportConditionSmaller:
                                                    {
                                                        if (reportConditionDateField.DateTimeConditionYear != null)
                                                        {
                                                            DateTime condDateTime = new DateTime((int)reportConditionDateField.DateTimeConditionYear,
                                                                (reportConditionDateField.DateTimeConditionMonth == null ? 1 : (int)reportConditionDateField.DateTimeConditionMonth),
                                                                (reportConditionDateField.DateTimeConditionDay == null ? 1 : (int)reportConditionDateField.DateTimeConditionDay),
                                                                (reportConditionDateField.DateTimeConditionHour == null ? 0 : (int)reportConditionDateField.DateTimeConditionHour),
                                                                (reportConditionDateField.DateTimeConditionMinute == null ? 0 : (int)reportConditionDateField.DateTimeConditionMinute), 0);

                                                            if (val >= condDateTime)
                                                            {
                                                                KeepShow = false;
                                                                break;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (reportConditionDateField.DateTimeConditionMonth != null)
                                                            {
                                                                if (val.Month > reportConditionDateField.DateTimeConditionMonth)
                                                                {
                                                                    KeepShow = false;
                                                                    break;
                                                                }
                                                                else if (val.Month == reportConditionDateField.DateTimeConditionMonth)
                                                                {
                                                                    if (reportConditionDateField.DateTimeConditionDay != null)
                                                                    {
                                                                        if (val.Day > reportConditionDateField.DateTimeConditionDay)
                                                                        {
                                                                            KeepShow = false;
                                                                            break;
                                                                        }
                                                                        else if (val.Day == reportConditionDateField.DateTimeConditionDay)
                                                                        {
                                                                            if (reportConditionDateField.DateTimeConditionHour != null)
                                                                            {
                                                                                if (val.Hour > reportConditionDateField.DateTimeConditionHour)
                                                                                {
                                                                                    KeepShow = false;
                                                                                    break;
                                                                                }
                                                                                else if (val.Hour == reportConditionDateField.DateTimeConditionHour)
                                                                                {
                                                                                    if (reportConditionDateField.DateTimeConditionMinute != null)
                                                                                    {
                                                                                        if (val.Minute > reportConditionDateField.DateTimeConditionMinute)
                                                                                        {
                                                                                            KeepShow = false;
                                                                                            break;
                                                                                        }
                                                                                        else if (val.Minute == reportConditionDateField.DateTimeConditionMinute)
                                                                                        {
                                                                                            KeepShow = false;
                                                                                            break;
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (reportConditionDateField.DateTimeConditionDay != null)
                                                                {
                                                                    if (val.Day > reportConditionDateField.DateTimeConditionDay)
                                                                    {
                                                                        KeepShow = false;
                                                                        break;
                                                                    }
                                                                    else if (val.Day == reportConditionDateField.DateTimeConditionDay)
                                                                    {
                                                                        if (reportConditionDateField.DateTimeConditionHour != null)
                                                                        {
                                                                            if (val.Hour > reportConditionDateField.DateTimeConditionHour)
                                                                            {
                                                                                KeepShow = false;
                                                                                break;
                                                                            }
                                                                            else if (val.Hour == reportConditionDateField.DateTimeConditionHour)
                                                                            {
                                                                                if (reportConditionDateField.DateTimeConditionMinute != null)
                                                                                {
                                                                                    if (val.Minute > reportConditionDateField.DateTimeConditionMinute)
                                                                                    {
                                                                                        KeepShow = false;
                                                                                        break;
                                                                                    }
                                                                                    else if (val.Minute == reportConditionDateField.DateTimeConditionMinute)
                                                                                    {
                                                                                        KeepShow = false;
                                                                                        break;
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (reportConditionDateField.DateTimeConditionHour != null)
                                                                    {
                                                                        if (val.Hour > reportConditionDateField.DateTimeConditionHour)
                                                                        {
                                                                            KeepShow = false;
                                                                            break;
                                                                        }
                                                                        else if (val.Hour == reportConditionDateField.DateTimeConditionHour)
                                                                        {
                                                                            if (reportConditionDateField.DateTimeConditionMinute != null)
                                                                            {
                                                                                if (val.Minute > reportConditionDateField.DateTimeConditionMinute)
                                                                                {
                                                                                    KeepShow = false;
                                                                                    break;
                                                                                }
                                                                                else if (val.Minute == reportConditionDateField.DateTimeConditionMinute)
                                                                                {
                                                                                    KeepShow = false;
                                                                                    break;
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (reportConditionDateField.DateTimeConditionMinute != null)
                                                                        {
                                                                            if (val.Minute > reportConditionDateField.DateTimeConditionMinute)
                                                                            {
                                                                                KeepShow = false;
                                                                                break;
                                                                            }
                                                                            else if (val.Minute == reportConditionDateField.DateTimeConditionMinute)
                                                                            {
                                                                                KeepShow = false;
                                                                                break;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    break;
                                                case ReportConditionEnum.ReportConditionEqual:
                                                    {
                                                        if (reportConditionDateField.DateTimeConditionYear != null)
                                                        {
                                                            if (val.Year != reportConditionDateField.DateTimeConditionYear)
                                                            {
                                                                KeepShow = false;
                                                                break;
                                                            }
                                                        }
                                                        if (reportConditionDateField.DateTimeConditionMonth != null)
                                                        {
                                                            if (val.Month != reportConditionDateField.DateTimeConditionMonth)
                                                            {
                                                                KeepShow = false;
                                                                break;
                                                            }
                                                        }
                                                        if (reportConditionDateField.DateTimeConditionDay != null)
                                                        {
                                                            if (val.Day != reportConditionDateField.DateTimeConditionDay)
                                                            {
                                                                KeepShow = false;
                                                                break;
                                                            }
                                                        }
                                                        if (reportConditionDateField.DateTimeConditionHour != null)
                                                        {
                                                            if (val.Hour != reportConditionDateField.DateTimeConditionHour)
                                                            {
                                                                KeepShow = false;
                                                                break;
                                                            }
                                                        }
                                                        if (reportConditionDateField.DateTimeConditionMinute != null)
                                                        {
                                                            if (val.Minute != reportConditionDateField.DateTimeConditionMinute)
                                                            {
                                                                KeepShow = false;
                                                                break;
                                                            }
                                                        }
                                                    }
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                    }
                                }
                                break;
                            case "System.Single":
                                {
                                    List<ReportConditionNumberField> reportConditionNumberFieldList = new List<ReportConditionNumberField>();
                                    if (IsDBFiltering)
                                    {
                                        reportConditionNumberFieldList = reportTreeNode.dbFilteringNumberFieldList;
                                    }
                                    else
                                    {
                                        reportConditionNumberFieldList = reportTreeNode.reportConditionNumberFieldList;
                                    }

                                    float val = (float)propertyInfo.GetValue(reportModel);
                                    if (reportConditionNumberFieldList.Count > 0)
                                    {
                                        foreach (ReportConditionNumberField reportConditionNumberField in reportConditionNumberFieldList)
                                        {
                                            switch (reportConditionNumberField.ReportCondition)
                                            {
                                                case ReportConditionEnum.ReportConditionBigger:
                                                    {
                                                        if (val <= reportConditionNumberField.NumberCondition)
                                                        {
                                                            KeepShow = false;
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                case ReportConditionEnum.ReportConditionSmaller:
                                                    {
                                                        if (val >= reportConditionNumberField.NumberCondition)
                                                        {
                                                            KeepShow = false;
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                case ReportConditionEnum.ReportConditionEqual:
                                                    {
                                                        if (val != reportConditionNumberField.NumberCondition)
                                                        {
                                                            KeepShow = false;
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                    }
                                }
                                break;
                            case "System.Int32":
                                {
                                    List<ReportConditionNumberField> reportConditionNumberFieldList = new List<ReportConditionNumberField>();
                                    if (IsDBFiltering)
                                    {
                                        reportConditionNumberFieldList = reportTreeNode.dbFilteringNumberFieldList;
                                    }
                                    else
                                    {
                                        reportConditionNumberFieldList = reportTreeNode.reportConditionNumberFieldList;
                                    }

                                    int val = (int)propertyInfo.GetValue(reportModel);
                                    if (reportConditionNumberFieldList.Count > 0)
                                    {
                                        foreach (ReportConditionNumberField reportConditionNumberField in reportConditionNumberFieldList)
                                        {
                                            switch (reportConditionNumberField.ReportCondition)
                                            {
                                                case ReportConditionEnum.ReportConditionBigger:
                                                    {
                                                        if (val <= reportConditionNumberField.NumberCondition)
                                                        {
                                                            KeepShow = false;
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                case ReportConditionEnum.ReportConditionSmaller:
                                                    {
                                                        if (val >= reportConditionNumberField.NumberCondition)
                                                        {
                                                            KeepShow = false;
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                case ReportConditionEnum.ReportConditionEqual:
                                                    {
                                                        if (val != reportConditionNumberField.NumberCondition)
                                                        {
                                                            KeepShow = false;
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                    }
                                }
                                break;
                            case "System.String":
                                {
                                    List<ReportConditionTextField> reportConditionTextFieldList = new List<ReportConditionTextField>();
                                    if (IsDBFiltering)
                                    {
                                        reportConditionTextFieldList = reportTreeNode.dbFilteringTextFieldList;
                                    }
                                    else
                                    {
                                        reportConditionTextFieldList = reportTreeNode.reportConditionTextFieldList;
                                    }

                                    string val = ((string)propertyInfo.GetValue(reportModel)).ToLower();
                                    if (reportConditionTextFieldList.Count > 0)
                                    {
                                        foreach (ReportConditionTextField reportConditionTextField in reportConditionTextFieldList)
                                        {
                                            switch (reportConditionTextField.ReportCondition)
                                            {
                                                case ReportConditionEnum.ReportConditionContain:
                                                    {
                                                        if (!val.Contains(reportConditionTextField.TextCondition.ToLower().Replace("*", " ")))
                                                        {
                                                            KeepShow = false;
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                case ReportConditionEnum.ReportConditionStart:
                                                    {
                                                        if (!val.StartsWith(reportConditionTextField.TextCondition.ToLower().Replace("*", " ")))
                                                        {
                                                            KeepShow = false;
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                case ReportConditionEnum.ReportConditionEnd:
                                                    {
                                                        if (!val.EndsWith(reportConditionTextField.TextCondition.ToLower().Replace("*", " ")))
                                                        {
                                                            KeepShow = false;
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                case ReportConditionEnum.ReportConditionBigger:
                                                    {
                                                        if (String.Compare(val, reportConditionTextField.TextCondition.ToLower().Replace("*", " ")) < 0)
                                                        {
                                                            KeepShow = false;
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                case ReportConditionEnum.ReportConditionSmaller:
                                                    {
                                                        if (String.Compare(val, reportConditionTextField.TextCondition.ToLower().Replace("*", " ")) > 0)
                                                        {
                                                            KeepShow = false;
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                case ReportConditionEnum.ReportConditionEqual:
                                                    {
                                                        if (!(val == reportConditionTextField.TextCondition.ToLower().Replace("*", " ")))
                                                        {
                                                            KeepShow = false;
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                    }
                                }
                                break;
                            case "CSSPEnumsDLL.Enums.FilePurposeEnum":
                            case "CSSPEnumsDLL.Enums.FileTypeEnum":
                            case "CSSPEnumsDLL.Enums.TranslationStatusEnum":
                            case "CSSPEnumsDLL.Enums.BoxModelResultTypeEnum":
                            case "CSSPEnumsDLL.Enums.InfrastructureTypeEnum":
                            case "CSSPEnumsDLL.Enums.FacilityTypeEnum":
                            case "CSSPEnumsDLL.Enums.AerationTypeEnum":
                            case "CSSPEnumsDLL.Enums.PreliminaryTreatmentTypeEnum":
                            case "CSSPEnumsDLL.Enums.PrimaryTreatmentTypeEnum":
                            case "CSSPEnumsDLL.Enums.SecondaryTreatmentTypeEnum":
                            case "CSSPEnumsDLL.Enums.TertiaryTreatmentTypeEnum":
                            case "CSSPEnumsDLL.Enums.TreatmentTypeEnum":
                            case "CSSPEnumsDLL.Enums.DisinfectionTypeEnum":
                            case "CSSPEnumsDLL.Enums.CollectionSystemTypeEnum":
                            case "CSSPEnumsDLL.Enums.AlarmSystemTypeEnum":
                            case "CSSPEnumsDLL.Enums.ScenarioStatusEnum":
                            case "CSSPEnumsDLL.Enums.StorageDataTypeEnum":
                            case "CSSPEnumsDLL.Enums.LanguageEnum":
                            case "CSSPEnumsDLL.Enums.BeaufortScaleEnum":
                            case "CSSPEnumsDLL.Enums.AnalyzeMethodEnum":
                            case "CSSPEnumsDLL.Enums.SampleMatrixEnum":
                            case "CSSPEnumsDLL.Enums.LaboratoryEnum":
                            case "CSSPEnumsDLL.Enums.SampleStatusEnum":
                            case "CSSPEnumsDLL.Enums.SamplingPlanTypeEnum":
                            case "CSSPEnumsDLL.Enums.LabSheetTypeEnum":
                            case "CSSPEnumsDLL.Enums.LabSheetStatusEnum":
                            case "CSSPEnumsDLL.Enums.PolSourceInactiveReasonEnum":
                            case "CSSPEnumsDLL.Enums.AddressTypeEnum":
                            case "CSSPEnumsDLL.Enums.StreetTypeEnum":
                            case "CSSPEnumsDLL.Enums.ContactTitleEnum":
                            case "CSSPEnumsDLL.Enums.EmailTypeEnum":
                            case "CSSPEnumsDLL.Enums.TelTypeEnum":
                            case "CSSPEnumsDLL.Enums.TideTextEnum":
                            case "CSSPEnumsDLL.Enums.TideDataTypeEnum":
                            case "CSSPEnumsDLL.Enums.SpecialTableTypeEnum":
                            case "CSSPEnumsDLL.Enums.MWQMSiteLatestClassificationEnum":
                            case "CSSPEnumsDLL.Enums.PolSourceIssueRiskEnum":
                            case "CSSPEnumsDLL.Enums.MikeScenarioSpecialResultKMLTypeEnum":
                                {
                                    List<ReportConditionEnumField> reportConditionEnumFieldList = new List<ReportConditionEnumField>();
                                    if (IsDBFiltering)
                                    {
                                        reportConditionEnumFieldList = reportTreeNode.dbFilteringEnumFieldList;
                                    }
                                    else
                                    {
                                        reportConditionEnumFieldList = reportTreeNode.reportConditionEnumFieldList;
                                    }

                                    var val = propertyInfo.GetValue(reportModel);
                                    if (reportConditionEnumFieldList.Count > 0)
                                    {
                                        foreach (ReportConditionEnumField reportConditionEnumField in reportConditionEnumFieldList)
                                        {
                                            List<string> strList = reportConditionEnumField.EnumConditionText.Split("*".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
                                            foreach (string s in strList)
                                            {
                                                switch (reportConditionEnumField.ReportCondition)
                                                {
                                                    case ReportConditionEnum.ReportConditionEqual:
                                                        {
                                                            if (val.ToString() != s)
                                                            {
                                                                KeepShow = false;
                                                                break;
                                                            }
                                                        }
                                                        break;
                                                    default:
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return KeepShow;
        }
        public void SetParentChecked(TreeViewEventArgs e) // used in CSSPReportWriterHelper windows app
        {
            ReportTreeNode reportTreeNode = (ReportTreeNode)e.Node;

            if (reportTreeNode != null)
            {
                if (reportTreeNode.Parent != null)
                {
                    if (reportTreeNode.Checked)
                    {
                        reportTreeNode.Parent.Checked = true;
                    }
                    else
                    {
                        bool AllSiblingUnchecked = true;
                        foreach (ReportTreeNode RTN in ((ReportTreeNode)reportTreeNode.Parent).Nodes)
                        {
                            if (RTN.Checked == true)
                            {
                                AllSiblingUnchecked = false;
                            }
                        }
                        if (AllSiblingUnchecked)
                        {
                            reportTreeNode.Parent.Checked = false;
                        }
                    }
                }
            }
        }
        public string SetReportTreeNodeDateCondition(int StartItem, ReportConditionDateField reportConditionDateField, ReportTreeNode reportTreeNode, List<string> strList, int LineCount)
        {
            int Value;

            if (!AllowableBasicFilters.Contains(strList[StartItem]))
                return string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, StartItem + 1, String.Join(",", AllowableBasicFilters));


            if (strList.Count < (StartItem + 3))
                return string.Format(ReportServiceRes.Condition_MissingValue, strList[StartItem]);

            for (int i = StartItem + 1, count = strList.Count - 1; i <= count; i += 2)
            {
                switch (strList[i])
                {
                    case "YEAR":
                        {
                            if (strList.Count < (i + 2))
                                return string.Format(ReportServiceRes.Condition_MissingValue, strList[i]);

                            if (strList[i + 1].Contains(".") || strList[i + 1].Contains(","))
                                return string.Format(ReportServiceRes.NumberWithoutDecimalIsRequiredAfter_, strList[i]);

                            if (!int.TryParse(strList[i + 1], out Value))
                                return string.Format(ReportServiceRes.ConditionTextMissingNumberAfter_, strList[i]);

                            reportConditionDateField.DateTimeConditionYear = Value;
                        }
                        break;
                    case "MONTH":
                        {
                            if (strList.Count < (i + 2))
                                return string.Format(ReportServiceRes.Condition_MissingValue, strList[i]);

                            if (strList[i + 1].Contains(".") || strList[i + 1].Contains(","))
                                return string.Format(ReportServiceRes.NumberWithoutDecimalIsRequiredAfter_, strList[i]);

                            if (!int.TryParse(strList[i + 1], out Value))
                                return string.Format(ReportServiceRes.ConditionTextMissingNumberAfter_, strList[i]);

                            reportConditionDateField.DateTimeConditionMonth = Value;
                        }
                        break;
                    case "DAY":
                        {
                            if (strList.Count < (i + 2))
                                return string.Format(ReportServiceRes.Condition_MissingValue, strList[i]);

                            if (strList[i + 1].Contains(".") || strList[i + 1].Contains(","))
                                return string.Format(ReportServiceRes.NumberWithoutDecimalIsRequiredAfter_, strList[i]);

                            if (!int.TryParse(strList[i + 1], out Value))
                                return string.Format(ReportServiceRes.ConditionTextMissingNumberAfter_, strList[i]);

                            reportConditionDateField.DateTimeConditionDay = Value;
                        }
                        break;
                    case "HOUR":
                        {
                            if (strList.Count < (i + 2))
                                return string.Format(ReportServiceRes.Condition_MissingValue, strList[i]);

                            if (strList[i + 1].Contains(".") || strList[i + 1].Contains(","))
                                return string.Format(ReportServiceRes.NumberWithoutDecimalIsRequiredAfter_, strList[i]);

                            if (!int.TryParse(strList[i + 1], out Value))
                                return string.Format(ReportServiceRes.ConditionTextMissingNumberAfter_, strList[i]);

                            reportConditionDateField.DateTimeConditionHour = Value;
                        }
                        break;
                    case "MINUTE":
                        {
                            if (strList.Count < (i + 2))
                                return string.Format(ReportServiceRes.Condition_MissingValue, strList[i]);

                            if (strList[i + 1].Contains(".") || strList[i + 1].Contains(","))
                                return string.Format(ReportServiceRes.NumberWithoutDecimalIsRequiredAfter_, strList[i]);

                            if (!int.TryParse(strList[i + 1], out Value))
                                return string.Format(ReportServiceRes.ConditionTextMissingNumberAfter_, strList[i]);

                            reportConditionDateField.DateTimeConditionMinute = Value;
                        }
                        break;
                    default:
                        break;
                }
            }

            if (strList[StartItem] == "EQUAL")
            {
                reportConditionDateField.ReportCondition = ReportConditionEnum.ReportConditionEqual;
            }
            else if (strList[StartItem] == "BIGGER")
            {
                reportConditionDateField.ReportCondition = ReportConditionEnum.ReportConditionBigger;
            }
            else if (strList[StartItem] == "SMALLER")
            {
                reportConditionDateField.ReportCondition = ReportConditionEnum.ReportConditionSmaller;
            }
            else
            {
                return string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, StartItem + 1, String.Join(",", AllowableBasicFilters));

            }

            return "";
        }
        public string SetReportTreeNodeEnumCondition(int StartItem, ReportConditionEnumField reportConditionEnumField, ReportTreeNode reportTreeNode, List<string> strList, int LineCount)
        {
            string retStr = "";

            if (!AllowableEnumFilters.Contains(strList[StartItem]))
                return string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, StartItem + 1, String.Join(",", AllowableEnumFilters));

            if (strList.Count <= (StartItem + 1))
                return string.Format(ReportServiceRes.Condition_MissingValue, strList[StartItem]);

            reportConditionEnumField.EnumConditionText = strList[StartItem + 1];

            if (strList[StartItem] == "EQUAL")
            {
                reportConditionEnumField.ReportCondition = ReportConditionEnum.ReportConditionEqual;
            }
            else
            {
                retStr = string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, StartItem + 1, String.Join(",", AllowableEnumFilters));
            }

            if (strList.Count > StartItem)
            {
                retStr = CheckFilterOfEnum(strList[StartItem + 1], (ReportFieldTypeEnum)reportTreeNode.ReportFieldType);
            }
            else
            {
                retStr = string.Format(ReportServiceRes.ConditionTextMissingTextAfter_, strList[StartItem]);
            }

            return retStr;
        }
        public string SetReportTreeNodeFormating(int StartItem, ReportFormatingField reportFormatingField, ReportTreeNode reportTreeNode, List<string> strList, int LineCount)
        {
            if (strList.Count <= (StartItem + 1))
                return string.Format(ReportServiceRes.Condition_MissingValue, strList[StartItem]);

            if (strList[StartItem] != "FORMAT")
                return string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, StartItem + 1, "FORMAT");

            if (reportTreeNode.ReportFieldType == ReportFieldTypeEnum.DateAndTime)
            {
                reportFormatingField.DateFormating = strList[StartItem + 1];
            }
            else if (reportTreeNode.ReportFieldType == ReportFieldTypeEnum.NumberWithDecimal)
            {
                reportFormatingField.NumberFormating = strList[StartItem + 1];
            }
            else
            {
                return string.Format(ReportServiceRes.ReportFieldTypeIs_ItHasToBeOneOf_, reportTreeNode.ReportFieldType.ToString(), ReportFieldTypeEnum.DateAndTime.ToString() + "," + ReportFieldTypeEnum.NumberWithDecimal.ToString());
            }

            return "";
        }
        public string SetReportTreeNodeNumberCondition(int StartItem, ReportConditionNumberField reportConditionNumberField, ReportTreeNode reportTreeNode, List<string> strList, int LineCount)
        {
            float ValueFloat;
            int ValueInt;

            if (!AllowableBasicFilters.Contains(strList[StartItem]))
                return string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, StartItem + 1, String.Join(",", AllowableBasicFilters));

            if (!(strList.Count > StartItem + 1))
                return string.Format(ReportServiceRes.Condition_MissingValue, strList[StartItem]);

            if (!float.TryParse((LanguageRequest == LanguageEnum.fr ? strList[StartItem + 1].Replace(".", ",") : strList[StartItem + 1]), out ValueFloat))
                return string.Format(ReportServiceRes.ConditionTextMissingNumberAfter_, strList[StartItem]);

            reportConditionNumberField.NumberCondition = ValueFloat;

            if (reportTreeNode.ReportFieldType == ReportFieldTypeEnum.NumberWhole)
            {
                if (strList[StartItem + 1].Contains(".") || strList[StartItem + 1].Contains(","))
                    return string.Format(ReportServiceRes.NumberWithoutDecimalIsRequiredAfter_, strList[StartItem]);

                if (!int.TryParse((LanguageRequest == LanguageEnum.fr ? strList[StartItem + 1].Replace(".", ",") : strList[StartItem + 1]), out ValueInt))
                    return string.Format(ReportServiceRes.ConditionTextMissingNumberAfter_, strList[StartItem]);

                reportConditionNumberField.NumberCondition = (float)ValueInt;
            }

            if (strList[StartItem] == "EQUAL")
            {
                reportConditionNumberField.ReportCondition = ReportConditionEnum.ReportConditionEqual;
            }
            else if (strList[StartItem] == "BIGGER")
            {
                reportConditionNumberField.ReportCondition = ReportConditionEnum.ReportConditionBigger;
            }
            else if (strList[StartItem] == "SMALLER")
            {
                reportConditionNumberField.ReportCondition = ReportConditionEnum.ReportConditionSmaller;
            }
            else
            {
                return string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, StartItem + 1, String.Join(",", AllowableBasicFilters));

            }

            return "";
        }
        public string SetReportTreeNodeSorting(int StartItem, ReportSortingField reportSortingField, ReportTreeNode reportTreeNode, List<string> strList, int LineCount)
        {
            int Ordinal;
            if (strList.Count <= (StartItem + 1))
                return string.Format(ReportServiceRes.Line_ShouldHave_Items, LineCount, "more than " + (StartItem + 1));

            if (strList[StartItem] == "ASCENDING")
            {
                reportSortingField.ReportSorting = ReportSortingEnum.ReportSortingAscending;
            }
            else if (strList[StartItem] == "DESCENDING")
            {
                reportSortingField.ReportSorting = ReportSortingEnum.ReportSortingDescending;
            }
            else
            {
                return string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, StartItem + 1, "ASCENDING, DESCENDING");
            }

            if (!int.TryParse(strList[StartItem + 1], out Ordinal))
                return string.Format(ReportServiceRes.Line_Item_ShouldBeANumber, LineCount, StartItem + 1);

            reportSortingField.Ordinal = Ordinal;

            return "";
        }
        public string SetReportTreeNodeTextCondition(int StartItem, ReportConditionTextField reportConditionTextField, ReportTreeNode reportTreeNode, List<string> strList, int LineCount)
        {
            if (!AllowableTextFilters.Contains(strList[StartItem]))
                return string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, StartItem + 1, String.Join(",", AllowableTextFilters));

            if (!(strList.Count > StartItem + 1))
                return string.Format(ReportServiceRes.Condition_MissingValue, strList[StartItem]);

            reportConditionTextField.TextCondition = strList[StartItem + 1];

            if (strList[StartItem] == "CONTAIN")
            {
                reportConditionTextField.ReportCondition = ReportConditionEnum.ReportConditionContain;
            }
            else if (strList[StartItem] == "START")
            {
                reportConditionTextField.ReportCondition = ReportConditionEnum.ReportConditionStart;
            }
            else if (strList[StartItem] == "END")
            {
                reportConditionTextField.ReportCondition = ReportConditionEnum.ReportConditionEnd;
            }
            else if (strList[StartItem] == "EQUAL")
            {
                reportConditionTextField.ReportCondition = ReportConditionEnum.ReportConditionEqual;
            }
            else if (strList[StartItem] == "BIGGER")
            {
                reportConditionTextField.ReportCondition = ReportConditionEnum.ReportConditionBigger;
            }
            else if (strList[StartItem] == "SMALLER")
            {
                reportConditionTextField.ReportCondition = ReportConditionEnum.ReportConditionSmaller;
            }
            else
            {
                return string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, StartItem + 1, String.Join(",", AllowableTextFilters));
            }

            return "";
        }
        public string SetReportTreeNodeTrueFalse(int StartItem, ReportConditionTrueFalseField reportConditionTrueFalseField, ReportTreeNode reportTreeNode, List<string> strList, int LineCount)
        {
            if (strList.Count <= StartItem)
                return string.Format(ReportServiceRes.Line_ShouldHave_Items, LineCount, "more than " + StartItem);

            if (strList[StartItem] == "TRUE")
            {
                reportConditionTrueFalseField.ReportCondition = ReportConditionEnum.ReportConditionTrue;
            }
            else if (strList[StartItem] == "FALSE")
            {
                reportConditionTrueFalseField.ReportCondition = ReportConditionEnum.ReportConditionFalse;
            }
            else
            {
                return string.Format(ReportServiceRes.Line_Item_ShouldBeOneOf_, LineCount, StartItem + 1, "TRUE,FALSE");

            }

            return "";
        }
        public float? StringIsANumber(string varText)
        {
            long numberInt = 0;
            bool canConvert = long.TryParse(varText, out numberInt);
            if (canConvert == true)
                return (float)numberInt;

            decimal numberDecimal = 0;
            canConvert = decimal.TryParse(varText, out numberDecimal);
            if (canConvert == true)
                return (float)numberDecimal;

            return null;
        }
        #endregion Functions public

        #region Functions private
        #endregion Functions private
    }

    public class ReportModelDynamic
    {
        public ReportModelDynamic()
        {
        }

        public Guid ReportTagGuid { get; set; }
        public int Ordinal { get; set; }
        public dynamic ReportModel { get; set; }
        public int TVItemID { get; set; }
        public int? ParentTVItemID { get; set; }
    }
    public class ReportTag
    {
        public ReportTag()
        {
            ReportTreeNodeList = new List<ReportTreeNode>();
            ReportTagChildList = new List<ReportTag>();
            Guid = Guid.NewGuid();
        }

        public string Error { get; set; }
        public string Command { get; set; }
        public bool CountOnly { get; set; }
        public Guid Guid { get; set; }
        public string Language { get; set; }
        public int Level { get; set; }
        public List<ReportTreeNode> ReportTreeNodeList { get; set; }
        //public List<int> ResultTVItemIDList { get; set; }
        public Type ReportType { get; set; }
        public string TagItem { get; set; }
        public string TagName { get; set; }
        //public int TVItemID { get; set; }
        public bool OnlyImmediateChildren { get; set; }
        public int UnderTVItemID { get; set; }
        public List<ReportTag> ReportTagChildList { get; set; }
        public ReportTag ReportTagParent { get; set; }
        public Microsoft.Office.Interop.Word.Application wordApp { get; set; }
        public Microsoft.Office.Interop.Word.Document doc { get; set; }
        public Microsoft.Office.Interop.Excel.Application excelApp { get; set; }
        public Microsoft.Office.Interop.Excel.Workbook workbook { get; set; }
        public Microsoft.Office.Interop.Word.Range StartRange { get; set; }
        public Microsoft.Office.Interop.Word.Range RangeEndTag { get; set; }
        public Microsoft.Office.Interop.Word.Range RangeInnerTag { get; set; }
        public Microsoft.Office.Interop.Word.Range RangeStartTag { get; set; }
        public int StartRangeStartPos { get; set; }
        public int StartRangeEndPos { get; set; }
        public int RangeEndTagStartPos { get; set; }
        public int RangeEndTagEndPos { get; set; }
        public int RangeInnerTagStartPos { get; set; }
        public int RangeInnerTagEndPos { get; set; }
        public int RangeStartTagStartPos { get; set; }
        public int RangeStartTagEndPos { get; set; }
        public DocumentType DocumentType { get; set; }
        public string CSVTagText { get; set; }
        public string KMLTagText { get; set; }
        public int Take { get; set; }
        public int AppTaskID { get; set; }
        public int DoingPart { get; set; }
        public int TotalPart { get; set; }
    }

    public enum DocumentType
    {
        Error = 0,
        CSV = 1,
        Word = 2,
        Excel = 3,
        KML = 4,
    }
}
