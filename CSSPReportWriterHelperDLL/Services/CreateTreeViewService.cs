using CSSPEnumsDLL.Enums;
using CSSPModelsDLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSSPReportWriterHelperDLL.Services
{
    public class CreateTreeViewService
    {
        #region Variables
        #endregion Variables

        #region Properties
        #endregion Properties

        #region Constructors
        public CreateTreeViewService()
        {
        }
        #endregion Constructors

        #region Functions public
        public List<ReportTreeNode> LoadChildrenTreeNodes(ReportTreeNode reportTreeNode)
        {
            List<ReportTreeNode> treeNodeList = new List<ReportTreeNode>();

            switch (reportTreeNode.ReportTreeNodeType)
            {
                case ReportTreeNodeTypeEnum.ReportAreaType:
                    return CreateReportAreaTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportAreaFileType:
                    return CreateReportAreaFileTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportBoxModelType:
                    return CreateReportBoxModelTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportBoxModelResultType:
                    return CreateReportBoxModelResultTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportClimateSiteType:
                    return CreateReportClimateSiteTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportClimateSiteDataType:
                    return CreateReportClimateSiteDataTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportCountryType:
                    return CreateReportCountryTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportCountryFileType:
                    return CreateReportCountryFileTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportHydrometricSiteType:
                    return CreateReportHydrometricSiteTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportHydrometricSiteDataType:
                    return CreateReportHydrometricSiteDataTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportHydrometricSiteRatingCurveType:
                    return CreateReportHydrometricSiteRatingCurveTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportHydrometricSiteRatingCurveValueType:
                    return CreateReportHydrometricSiteRatingCurveValueTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportInfrastructureType:
                    return CreateReportInfrastructureTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportInfrastructureAddressType:
                    return CreateReportInfrastructureAddressTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportInfrastructureFileType:
                    return CreateReportInfrastructureFileTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportMikeBoundaryConditionType:
                    return CreateReportMikeBoundaryConditionTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportMikeScenarioType:
                    return CreateReportMikeScenarioTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportMikeScenarioFileType:
                    return CreateReportMikeScenarioFileTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportMikeScenarioSpecialResultKMLType:
                    return CreateReportMikeScenarioSpecialResultKMLTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportMikeSourceType:
                    return CreateReportMikeSourceTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportMikeSourceStartEndType:
                    return CreateReportMikeSourceStartEndTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportMPNLookupType:
                    return CreateReportMPNLookupTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportSamplingPlanType:
                    return CreateReportSamplingPlanTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType:
                    return CreateReportSamplingPlanLabSheetTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType:
                    return CreateReportSamplingPlanLabSheetDetailTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetTubeMPNDetailType:
                    return CreateReportSamplingPlanLabSheetTubeAndMPNDetailTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportSamplingPlanSubsectorSiteType:
                    return CreateReportSamplingPlanSubsectorSiteTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportSamplingPlanSubsectorType:
                    return CreateReportSamplingPlanSubsectorTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportMWQMRunType:
                    return CreateReportMWQMRunTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportMWQMRunFileType:
                    return CreateReportMWQMRunFileTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType:
                    return CreateReportMWQMRunLabSheetTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType:
                    return CreateReportMWQMRunLabSheetDetailTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetTubeMPNDetailType:
                    return CreateReportMWQMRunLabSheetTubeMPNDetailTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportMWQMRunSampleType:
                    return CreateReportMWQMRunSampleTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportMWQMSiteType:
                    return CreateReportMWQMSiteTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportMWQMSiteFileType:
                    return CreateReportMWQMSiteFileTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportMWQMSiteSampleType:
                    return CreateReportMWQMSiteSampleTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportMWQMSiteStartAndEndType:
                    return CreateReportMWQMSiteStartAndEndDateTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportMunicipalityType:
                    return CreateReportMunicipalityTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportMunicipalityContactType:
                    return CreateReportMunicipalityContactTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportMunicipalityContactAddressType:
                    return CreateReportMunicipalityContactAddressTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportMunicipalityContactEmailType:
                    return CreateReportMunicipalityContactEmailTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportMunicipalityContactTelType:
                    return CreateReportMunicipalityContactTelTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportMunicipalityFileType:
                    return CreateReportMunicipalityFileTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportProvinceType:
                    return CreateReportProvinceTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportProvinceFileType:
                    return CreateReportProvinceFileTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportPolSourceSiteType:
                    return CreateReportPolSourceSiteTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportPolSourceSiteFileType:
                    return CreateReportPolSourceSiteFileTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportPolSourceSiteAddressType:
                    return CreateReportPolSourceSiteAddressTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportPolSourceSiteObsType:
                    return CreateReportPolSourceSiteObsTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportPolSourceSiteObsIssueType:
                    return CreateReportPolSourceSiteObsIssueTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportRootType:
                    return CreateReportRootTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportRootFileType:
                    return CreateReportRootFileTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportSectorType:
                    return CreateReportSectorTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportSectorFileType:
                    return CreateReportSectorFileTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportSubsectorType:
                    return CreateReportSubsectorTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportSubsectorSpecialTableType:
                    return CreateReportSubsectorSpecialTableTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType:
                    return CreateReportSubsectorClimateSiteTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteDataType:
                    return CreateReportSubsectorClimateSiteDataTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportSubsectorFileType:
                    return CreateReportSubsectorFileTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteType:
                    return CreateReportSubsectorHydrometricSiteTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteDataType:
                    return CreateReportSubsectorHydrometricSiteDataTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteRatingCurveType:
                    return CreateReportSubsectorHydrometricSiteRatingCurveTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteRatingCurveValueType:
                    return CreateReportSubsectorHydrometricSiteRatingCurveValueTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType:
                    return CreateReportSubsectorLabSheetTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType:
                    return CreateReportSubsectorLabSheetDetailTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportSubsectorLabSheetTubeMPNDetailType:
                    return CreateReportSubsectorLabSheetTubeMPNDetailTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportSubsectorTideSiteType:
                    return CreateReportSubsectorTideSiteTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportSubsectorTideSiteDataType:
                    return CreateReportSubsectorTideSiteDataTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioType:
                    return CreateReportVisualPlumesScenarioTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioAmbientType:
                    return CreateReportVisualPlumesScenarioAmbientTypeTreeNodeItem(reportTreeNode);
                case ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioResultType:
                    return CreateReportVisualPlumesScenarioResultTypeTreeNodeItem(reportTreeNode);
                default:
                    return null;
            }
        }

        public ReportTreeNode CreateReportTreeNodeItem(string Text, ReportTreeNodeTypeEnum reportTreeNodeType, ReportTreeNodeSubTypeEnum reportTreeNodeSubType, ReportFieldTypeEnum reportFieldType)
        {
            if (Text.Contains(" "))
                Text = Text + Text + Text + Text;

            ReportTreeNode reportTreeNode = new ReportTreeNode()
            {
                Text = Text,
                ReportTreeNodeType = reportTreeNodeType,
                ReportTreeNodeSubType = reportTreeNodeSubType,
                ReportFieldType = reportFieldType,
            };

            return reportTreeNode;
        }

        public List<ReportTreeNode> CreateReportAreaTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Area_Fields", ReportTreeNodeTypeEnum.ReportAreaType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Sector", ReportTreeNodeTypeEnum.ReportSectorType, ReportTreeNodeSubTypeEnum.TableSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Area_File", ReportTreeNodeTypeEnum.ReportAreaFileType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Area_Error", ReportTreeNodeTypeEnum.ReportAreaType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Area_Counter", ReportTreeNodeTypeEnum.ReportAreaType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Area_ID", ReportTreeNodeTypeEnum.ReportAreaType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Area_Name_Translation_Status", ReportTreeNodeTypeEnum.ReportAreaType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TranslationStatus),
                    CreateReportTreeNodeItem("Area_Name_Short", ReportTreeNodeTypeEnum.ReportAreaType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Area_Name_Long", ReportTreeNodeTypeEnum.ReportAreaType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Area_Is_Active", ReportTreeNodeTypeEnum.ReportAreaType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Area_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportAreaType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Area_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportAreaType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Area_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportAreaType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Area_Lat", ReportTreeNodeTypeEnum.ReportAreaType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Area_Lng", ReportTreeNodeTypeEnum.ReportAreaType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Area_Stat_Sector_Count", ReportTreeNodeTypeEnum.ReportAreaType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Area_Stat_Subsector_Count", ReportTreeNodeTypeEnum.ReportAreaType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Area_Stat_Municipality_Count", ReportTreeNodeTypeEnum.ReportAreaType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Area_Stat_Lift_Station_Count", ReportTreeNodeTypeEnum.ReportAreaType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Area_Stat_WWTP_Count", ReportTreeNodeTypeEnum.ReportAreaType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Area_Stat_MWQM_Sample_Count", ReportTreeNodeTypeEnum.ReportAreaType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Area_Stat_MWQM_Site_Count", ReportTreeNodeTypeEnum.ReportAreaType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Area_Stat_MWQM_Run_Count", ReportTreeNodeTypeEnum.ReportAreaType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Area_Stat_Pol_Source_Site_Count", ReportTreeNodeTypeEnum.ReportAreaType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Area_Stat_Mike_Scenario_Count", ReportTreeNodeTypeEnum.ReportAreaType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Area_Stat_Box_Model_Scenario_Count", ReportTreeNodeTypeEnum.ReportAreaType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Area_Stat_Visual_Plumes_Scenario_Count", ReportTreeNodeTypeEnum.ReportAreaType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
     };
            }
        }
        public List<ReportTreeNode> CreateReportAreaFileTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Area_File_Fields", ReportTreeNodeTypeEnum.ReportAreaFileType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Area_File_Error", ReportTreeNodeTypeEnum.ReportAreaFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Area_File_Counter", ReportTreeNodeTypeEnum.ReportAreaFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Area_File_ID", ReportTreeNodeTypeEnum.ReportAreaFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Area_File_Language", ReportTreeNodeTypeEnum.ReportAreaFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Language),
                    CreateReportTreeNodeItem("Area_File_Purpose", ReportTreeNodeTypeEnum.ReportAreaFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.FilePurpose),
                    CreateReportTreeNodeItem("Area_File_Type", ReportTreeNodeTypeEnum.ReportAreaFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.FileType),
                    CreateReportTreeNodeItem("Area_File_Description", ReportTreeNodeTypeEnum.ReportAreaFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Area_File_Size_kb", ReportTreeNodeTypeEnum.ReportAreaFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Area_File_Info", ReportTreeNodeTypeEnum.ReportAreaFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Area_File_Created_Date_UTC", ReportTreeNodeTypeEnum.ReportAreaFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Area_File_From_Water", ReportTreeNodeTypeEnum.ReportAreaFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Area_File_Server_File_Name", ReportTreeNodeTypeEnum.ReportAreaFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Area_File_Server_File_Path", ReportTreeNodeTypeEnum.ReportAreaFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Area_File_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportAreaFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Area_File_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportAreaFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Area_File_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportAreaFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportBoxModelTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Box_Model_Fields", ReportTreeNodeTypeEnum.ReportBoxModelType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Box_Model_Result", ReportTreeNodeTypeEnum.ReportBoxModelResultType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Box_Model_Error", ReportTreeNodeTypeEnum.ReportBoxModelType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Box_Model_Counter", ReportTreeNodeTypeEnum.ReportBoxModelType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Box_Model_ID", ReportTreeNodeTypeEnum.ReportBoxModelType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Box_Model_Scenario_Name_Translation_Status", ReportTreeNodeTypeEnum.ReportBoxModelType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TranslationStatus),
                    CreateReportTreeNodeItem("Box_Model_Scenario_Name", ReportTreeNodeTypeEnum.ReportBoxModelType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Box_Model_Flow_m3_day", ReportTreeNodeTypeEnum.ReportBoxModelType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Box_Model_Depth_m", ReportTreeNodeTypeEnum.ReportBoxModelType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Box_Model_Temperature_C", ReportTreeNodeTypeEnum.ReportBoxModelType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Box_Model_Dilution", ReportTreeNodeTypeEnum.ReportBoxModelType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Box_Model_Decay_Rate_per_day", ReportTreeNodeTypeEnum.ReportBoxModelType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Box_Model_FC_Untreated_MPN_100ml", ReportTreeNodeTypeEnum.ReportBoxModelType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Box_Model_FC_Pre_Disinfection_MPN_100_ml", ReportTreeNodeTypeEnum.ReportBoxModelType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Box_Model_Concentration_MPN_100_ml", ReportTreeNodeTypeEnum.ReportBoxModelType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Box_Model_T90_hour", ReportTreeNodeTypeEnum.ReportBoxModelType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Box_Model_Flow_Duration_hour", ReportTreeNodeTypeEnum.ReportBoxModelType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Box_Model_Last_Update_Date_UTC", ReportTreeNodeTypeEnum.ReportBoxModelType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Box_Model_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportBoxModelType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Box_Model_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportBoxModelType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportBoxModelResultTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Box_Model_Result_Fields", ReportTreeNodeTypeEnum.ReportBoxModelResultType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Box_Model_Result_Error", ReportTreeNodeTypeEnum.ReportBoxModelResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Box_Model_Result_Counter", ReportTreeNodeTypeEnum.ReportBoxModelResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Box_Model_Result_ID", ReportTreeNodeTypeEnum.ReportBoxModelResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Box_Model_Result_Result_Type", ReportTreeNodeTypeEnum.ReportBoxModelResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.BoxModelResultType),
                    CreateReportTreeNodeItem("Box_Model_Result_Volume_m3", ReportTreeNodeTypeEnum.ReportBoxModelResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Box_Model_Result_Surface_m2", ReportTreeNodeTypeEnum.ReportBoxModelResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Box_Model_Result_Radius_m", ReportTreeNodeTypeEnum.ReportBoxModelResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Box_Model_Result_Left_Side_Diameter_Line_Angle_deg", ReportTreeNodeTypeEnum.ReportBoxModelResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Box_Model_Result_Circle_Center_Latitude", ReportTreeNodeTypeEnum.ReportBoxModelResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Box_Model_Result_Circle_Center_Longitude", ReportTreeNodeTypeEnum.ReportBoxModelResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Box_Model_Result_Fix_Length", ReportTreeNodeTypeEnum.ReportBoxModelResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Box_Model_Result_Fix_Width", ReportTreeNodeTypeEnum.ReportBoxModelResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Box_Model_Result_Rect_Length_m", ReportTreeNodeTypeEnum.ReportBoxModelResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Box_Model_Result_Rect_Width_m", ReportTreeNodeTypeEnum.ReportBoxModelResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Box_Model_Result_Left_Side_Line_Angle_deg", ReportTreeNodeTypeEnum.ReportBoxModelResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Box_Model_Result_Left_Side_Line_Start_Latitude", ReportTreeNodeTypeEnum.ReportBoxModelResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Box_Model_Result_Left_Side_Line_Start_Longitude", ReportTreeNodeTypeEnum.ReportBoxModelResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Box_Model_Result_Last_Update_Date_UTC", ReportTreeNodeTypeEnum.ReportBoxModelResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Box_Model_Result_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportBoxModelResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Box_Model_Result_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportBoxModelResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportClimateSiteTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Climate_Site_Fields", ReportTreeNodeTypeEnum.ReportClimateSiteType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Climate_Site_Data", ReportTreeNodeTypeEnum.ReportClimateSiteDataType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Climate_Site_Error", ReportTreeNodeTypeEnum.ReportClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Climate_Site_Counter", ReportTreeNodeTypeEnum.ReportClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Climate_Site_ID", ReportTreeNodeTypeEnum.ReportClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Climate_Site_ECDBID", ReportTreeNodeTypeEnum.ReportClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Climate_Site_Name", ReportTreeNodeTypeEnum.ReportClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Climate_Site_Province", ReportTreeNodeTypeEnum.ReportClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Climate_Site_Elevation_m", ReportTreeNodeTypeEnum.ReportClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Climate_Site_Climate_ID", ReportTreeNodeTypeEnum.ReportClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Climate_Site_WMOID", ReportTreeNodeTypeEnum.ReportClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Climate_Site_TCID", ReportTreeNodeTypeEnum.ReportClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Climate_Site_Is_Provincial", ReportTreeNodeTypeEnum.ReportClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Climate_Site_Prov_Site_ID", ReportTreeNodeTypeEnum.ReportClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Climate_Site_Time_Offset_hour", ReportTreeNodeTypeEnum.ReportClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Climate_Site_File_desc", ReportTreeNodeTypeEnum.ReportClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Climate_Site_Hourly_Start_Date_Local", ReportTreeNodeTypeEnum.ReportClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Climate_Site_Hourly_End_Date_Local", ReportTreeNodeTypeEnum.ReportClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Climate_Site_Hourly_Now", ReportTreeNodeTypeEnum.ReportClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Climate_Site_Daily_Start_Date_Local", ReportTreeNodeTypeEnum.ReportClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Climate_Site_Daily_End_Date_Local", ReportTreeNodeTypeEnum.ReportClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Climate_Site_Daily_Now", ReportTreeNodeTypeEnum.ReportClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Climate_Site_Monthly_Start_Date_Local", ReportTreeNodeTypeEnum.ReportClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Climate_Site_Monthly_End_Date_Local", ReportTreeNodeTypeEnum.ReportClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Climate_Site_Monthly_Now", ReportTreeNodeTypeEnum.ReportClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Climate_Site_Last_Update_Date_UTC", ReportTreeNodeTypeEnum.ReportClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Climate_Site_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Climate_Site_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Climate_Site_Lat", ReportTreeNodeTypeEnum.ReportClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Climate_Site_Lng", ReportTreeNodeTypeEnum.ReportClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                };
            }
        }
        public List<ReportTreeNode> CreateReportClimateSiteDataTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Climate_Site_Data_Fields", ReportTreeNodeTypeEnum.ReportClimateSiteDataType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Climate_Site_Data_Error", ReportTreeNodeTypeEnum.ReportClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Climate_Site_Data_Counter", ReportTreeNodeTypeEnum.ReportClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Climate_Site_Data_ID", ReportTreeNodeTypeEnum.ReportClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Climate_Site_Data_Date_Time_Local", ReportTreeNodeTypeEnum.ReportClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Climate_Site_Data_Keep", ReportTreeNodeTypeEnum.ReportClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Climate_Site_Data_Storage_Data_Type", ReportTreeNodeTypeEnum.ReportClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.StorageDataType),
                    CreateReportTreeNodeItem("Climate_Site_Data_Snow_cm", ReportTreeNodeTypeEnum.ReportClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Climate_Site_Data_Rainfall_mm", ReportTreeNodeTypeEnum.ReportClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Climate_Site_Data_Rainfall_Entered_mm", ReportTreeNodeTypeEnum.ReportClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Climate_Site_Data_Total_Precip_mm_cm", ReportTreeNodeTypeEnum.ReportClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Climate_Site_Data_Max_Temp_C", ReportTreeNodeTypeEnum.ReportClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Climate_Site_Data_Min_Temp_C", ReportTreeNodeTypeEnum.ReportClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Climate_Site_Data_Heat_Deg_Days_C", ReportTreeNodeTypeEnum.ReportClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Climate_Site_Data_Cool_Deg_Days_C", ReportTreeNodeTypeEnum.ReportClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Climate_Site_Data_Snow_On_Ground_cm", ReportTreeNodeTypeEnum.ReportClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Climate_Site_Data_Dir_Max_Gust_0North", ReportTreeNodeTypeEnum.ReportClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Climate_Site_Data_Spd_Max_Gust_kmh", ReportTreeNodeTypeEnum.ReportClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Climate_Site_Data_Hourly_Values", ReportTreeNodeTypeEnum.ReportClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Climate_Site_Data_Last_Update_Date_UTC", ReportTreeNodeTypeEnum.ReportClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Climate_Site_Data_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Climate_Site_Data_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportCountryTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Country_Fields", ReportTreeNodeTypeEnum.ReportCountryType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Province", ReportTreeNodeTypeEnum.ReportProvinceType, ReportTreeNodeSubTypeEnum.TableSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Country_File", ReportTreeNodeTypeEnum.ReportCountryFileType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Country_Error", ReportTreeNodeTypeEnum.ReportCountryType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Country_Counter", ReportTreeNodeTypeEnum.ReportCountryType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Country_ID", ReportTreeNodeTypeEnum.ReportCountryType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Country_Name_Translation_Status", ReportTreeNodeTypeEnum.ReportCountryType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TranslationStatus),
                    CreateReportTreeNodeItem("Country_Name", ReportTreeNodeTypeEnum.ReportCountryType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Country_Initial", ReportTreeNodeTypeEnum.ReportCountryType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Country_Is_Active", ReportTreeNodeTypeEnum.ReportCountryType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Country_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportCountryType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Country_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportCountryType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Country_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportCountryType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Country_Lat", ReportTreeNodeTypeEnum.ReportCountryType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Country_Lng", ReportTreeNodeTypeEnum.ReportCountryType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Country_Stat_Province_Count", ReportTreeNodeTypeEnum.ReportCountryType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Country_Stat_Area_Count", ReportTreeNodeTypeEnum.ReportCountryType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Country_Stat_Sector_Count", ReportTreeNodeTypeEnum.ReportCountryType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Country_Stat_Subsector_Count", ReportTreeNodeTypeEnum.ReportCountryType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Country_Stat_Municipality_Count", ReportTreeNodeTypeEnum.ReportCountryType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Country_Stat_Lift_Station_Count", ReportTreeNodeTypeEnum.ReportCountryType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Country_Stat_WWTP_Count", ReportTreeNodeTypeEnum.ReportCountryType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Country_Stat_MWQM_Sample_Count", ReportTreeNodeTypeEnum.ReportCountryType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Country_Stat_MWQM_Site_Count", ReportTreeNodeTypeEnum.ReportCountryType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Country_Stat_MWQM_Run_Count", ReportTreeNodeTypeEnum.ReportCountryType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Country_Stat_Pol_Source_Site_Count", ReportTreeNodeTypeEnum.ReportCountryType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Country_Stat_Mike_Scenario_Count", ReportTreeNodeTypeEnum.ReportCountryType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Country_Stat_Box_Model_Scenario_Count", ReportTreeNodeTypeEnum.ReportCountryType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Country_Stat_Visual_Plumes_Scenario_Count", ReportTreeNodeTypeEnum.ReportCountryType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                };
            }
        }
        public List<ReportTreeNode> CreateReportCountryFileTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Country_File_Fields", ReportTreeNodeTypeEnum.ReportCountryFileType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Country_File_Error", ReportTreeNodeTypeEnum.ReportCountryFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Country_File_Counter", ReportTreeNodeTypeEnum.ReportCountryFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Country_File_ID", ReportTreeNodeTypeEnum.ReportCountryFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Country_File_Language", ReportTreeNodeTypeEnum.ReportCountryFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Language),
                    CreateReportTreeNodeItem("Country_File_Purpose", ReportTreeNodeTypeEnum.ReportCountryFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.FilePurpose),
                    CreateReportTreeNodeItem("Country_File_Type", ReportTreeNodeTypeEnum.ReportCountryFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.FileType),
                    CreateReportTreeNodeItem("Country_File_Description", ReportTreeNodeTypeEnum.ReportCountryFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Country_File_Size_kb", ReportTreeNodeTypeEnum.ReportCountryFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Country_File_Info", ReportTreeNodeTypeEnum.ReportCountryFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Country_File_Created_Date_UTC", ReportTreeNodeTypeEnum.ReportCountryFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Country_File_From_Water", ReportTreeNodeTypeEnum.ReportCountryFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Country_File_Server_File_Name", ReportTreeNodeTypeEnum.ReportCountryFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Country_File_Server_File_Path", ReportTreeNodeTypeEnum.ReportCountryFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Country_File_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportCountryFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Country_File_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportCountryFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Country_File_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportCountryFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportHydrometricSiteTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Hydrometric_Site_Fields", ReportTreeNodeTypeEnum.ReportHydrometricSiteType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Hydrometric_Site_Data", ReportTreeNodeTypeEnum.ReportHydrometricSiteDataType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Hydrometric_Site_Rating_Curve", ReportTreeNodeTypeEnum.ReportHydrometricSiteRatingCurveType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Hydrometric_Site_Error", ReportTreeNodeTypeEnum.ReportHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Hydrometric_Site_Counter", ReportTreeNodeTypeEnum.ReportHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Hydrometric_Site_ID", ReportTreeNodeTypeEnum.ReportHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Hydrometric_Site_Fed_Site_Number", ReportTreeNodeTypeEnum.ReportHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Hydrometric_Site_Quebec_Site_Number", ReportTreeNodeTypeEnum.ReportHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Hydrometric_Site_Name", ReportTreeNodeTypeEnum.ReportHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Hydrometric_Site_Description", ReportTreeNodeTypeEnum.ReportHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Hydrometric_Site_Province", ReportTreeNodeTypeEnum.ReportHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Hydrometric_Site_Elevation_m", ReportTreeNodeTypeEnum.ReportHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Hydrometric_Site_Start_Date_Local", ReportTreeNodeTypeEnum.ReportHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Hydrometric_Site_End_Date_Local", ReportTreeNodeTypeEnum.ReportHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Hydrometric_Site_Time_Offset_hour", ReportTreeNodeTypeEnum.ReportHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Hydrometric_Site_Drainage_Area_km2", ReportTreeNodeTypeEnum.ReportHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Hydrometric_Site_Is_Natural", ReportTreeNodeTypeEnum.ReportHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Hydrometric_Site_Is_Active", ReportTreeNodeTypeEnum.ReportHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Hydrometric_Site_Sediment", ReportTreeNodeTypeEnum.ReportHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Hydrometric_Site_RHBN", ReportTreeNodeTypeEnum.ReportHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Hydrometric_Site_Real_Time", ReportTreeNodeTypeEnum.ReportHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Hydrometric_Site_Has_Rating_Curve", ReportTreeNodeTypeEnum.ReportHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Hydrometric_Site_Last_Update_Date_UTC", ReportTreeNodeTypeEnum.ReportHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Hydrometric_Site_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Hydrometric_Site_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Hydrometric_Site_Lat", ReportTreeNodeTypeEnum.ReportHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Hydrometric_Site_Lng", ReportTreeNodeTypeEnum.ReportHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                };
            }
        }
        public List<ReportTreeNode> CreateReportHydrometricSiteDataTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Hydrometric_Site_Data_Fields", ReportTreeNodeTypeEnum.ReportHydrometricSiteDataType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Hydrometric_Site_Data_Error", ReportTreeNodeTypeEnum.ReportHydrometricSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Hydrometric_Site_Data_Counter", ReportTreeNodeTypeEnum.ReportHydrometricSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Hydrometric_Site_Data_ID", ReportTreeNodeTypeEnum.ReportHydrometricSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Hydrometric_Site_Data_Date_Time_Local", ReportTreeNodeTypeEnum.ReportHydrometricSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Hydrometric_Site_Data_Keep", ReportTreeNodeTypeEnum.ReportHydrometricSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Hydrometric_Site_Data_Storage_Data_Type", ReportTreeNodeTypeEnum.ReportHydrometricSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.StorageDataType),
                    CreateReportTreeNodeItem("Hydrometric_Site_Data_Flow_m3_s", ReportTreeNodeTypeEnum.ReportHydrometricSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Hydrometric_Site_Data_Hourly_Values", ReportTreeNodeTypeEnum.ReportHydrometricSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Hydrometric_Site_Data_Last_Update_Date_UTC", ReportTreeNodeTypeEnum.ReportHydrometricSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Hydrometric_Site_Data_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportHydrometricSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Hydrometric_Site_Data_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportHydrometricSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportHydrometricSiteRatingCurveTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Hydrometric_Site_Rating_Curve_Fields", ReportTreeNodeTypeEnum.ReportHydrometricSiteRatingCurveType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Hydrometric_Site_Rating_Curve_Value", ReportTreeNodeTypeEnum.ReportHydrometricSiteRatingCurveValueType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Hydrometric_Site_Rating_Curve_Error", ReportTreeNodeTypeEnum.ReportHydrometricSiteRatingCurveType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Hydrometric_Site_Rating_Curve_Counter", ReportTreeNodeTypeEnum.ReportHydrometricSiteRatingCurveType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Hydrometric_Site_Rating_Curve_ID", ReportTreeNodeTypeEnum.ReportHydrometricSiteRatingCurveType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Hydrometric_Site_Rating_Curve_Rating_Curve_Number", ReportTreeNodeTypeEnum.ReportHydrometricSiteRatingCurveType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Hydrometric_Site_Rating_Curve_Last_Update_Date_UTC", ReportTreeNodeTypeEnum.ReportHydrometricSiteRatingCurveType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Hydrometric_Site_Rating_Curve_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportHydrometricSiteRatingCurveType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Hydrometric_Site_Rating_Curve_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportHydrometricSiteRatingCurveType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportHydrometricSiteRatingCurveValueTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Hydrometric_Site_Rating_Curve_Value_Fields", ReportTreeNodeTypeEnum.ReportHydrometricSiteRatingCurveValueType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Hydrometric_Site_Rating_Curve_Value_Error", ReportTreeNodeTypeEnum.ReportHydrometricSiteRatingCurveValueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Hydrometric_Site_Rating_Curve_Value_Counter", ReportTreeNodeTypeEnum.ReportHydrometricSiteRatingCurveValueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Hydrometric_Site_Rating_Curve_Value_ID", ReportTreeNodeTypeEnum.ReportHydrometricSiteRatingCurveValueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Hydrometric_Site_Rating_Curve_Value_Stage_Value_m", ReportTreeNodeTypeEnum.ReportHydrometricSiteRatingCurveValueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Hydrometric_Site_Rating_Curve_Value_Discharge_Value_m3_s", ReportTreeNodeTypeEnum.ReportHydrometricSiteRatingCurveValueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Hydrometric_Site_Rating_Curve_Value_Last_Update_Date_UTC", ReportTreeNodeTypeEnum.ReportHydrometricSiteRatingCurveValueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Hydrometric_Site_Rating_Curve_Value_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportHydrometricSiteRatingCurveValueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Hydrometric_Site_Rating_Curve_Value_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportHydrometricSiteRatingCurveValueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportInfrastructureTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Infrastructure_Fields", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Box_Model", ReportTreeNodeTypeEnum.ReportBoxModelType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Infrastructure_Address", ReportTreeNodeTypeEnum.ReportInfrastructureAddressType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Infrastructure_File", ReportTreeNodeTypeEnum.ReportInfrastructureFileType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Infrastructure_Error", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Infrastructure_Counter", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Infrastructure_ID", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Infrastructure_Name", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Infrastructure_Name_Translation_Status", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TranslationStatus),
                    CreateReportTreeNodeItem("Infrastructure_Last_Update_Date_UTC", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Infrastructure_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Infrastructure_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Infrastructure_Is_Active", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Infrastructure_Comment_Translation_Status", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TranslationStatus),
                    CreateReportTreeNodeItem("Infrastructure_Comment_Last_Update_Date_UTC", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Infrastructure_Comment_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Infrastructure_Comment_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Infrastructure_Comment", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Infrastructure_Prism_ID", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Infrastructure_TPID", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Infrastructure_LSID", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Infrastructure_Site_ID", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Infrastructure_Site", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Infrastructure_Infrastructure_Category", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Infrastructure_Infrastructure_Type", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.InfrastructureType),
                    CreateReportTreeNodeItem("Infrastructure_Facility_Type", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.FacilityType),
                    CreateReportTreeNodeItem("Infrastructure_Is_Mechanically_Aerated", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Infrastructure_Number_Of_Cells", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Infrastructure_Number_Of_Aerated_Cells", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Infrastructure_Aeration_Type", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.AerationType),
                    CreateReportTreeNodeItem("Infrastructure_Preliminary_Treatment_Type", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.PreliminaryTreatmentType),
                    CreateReportTreeNodeItem("Infrastructure_Primary_Treatment_Type", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.PrimaryTreatmentType),
                    CreateReportTreeNodeItem("Infrastructure_Secondary_Treatment_Type", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.SecondaryTreatmentType),
                    CreateReportTreeNodeItem("Infrastructure_Tertiary_Treatment_Type", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TertiaryTreatmentType),
                    CreateReportTreeNodeItem("Infrastructure_Treatment_Type", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TreatmentType),
                    CreateReportTreeNodeItem("Infrastructure_Disinfection_Type", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DisinfectionType),
                    CreateReportTreeNodeItem("Infrastructure_Collection_System_Type", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.CollectionSystemType),
                    CreateReportTreeNodeItem("Infrastructure_Alarm_System_Type", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.AlarmSystemType),
                    CreateReportTreeNodeItem("Infrastructure_Design_Flow_m3_day", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Infrastructure_Average_Flow_m3_day", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Infrastructure_Peak_Flow_m3_day", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Infrastructure_Pop_Served", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Infrastructure_Can_Overflow", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Infrastructure_Perc_Flow_Of_Total", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Infrastructure_Time_Offset_hour", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Infrastructure_Temp_Catch_All_Remove_Later", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Infrastructure_Average_Depth_m", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Infrastructure_Number_Of_Ports", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Infrastructure_Port_Diameter_m", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Infrastructure_Port_Spacing_m", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Infrastructure_Port_Elevation_m", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Infrastructure_Vertical_Angle_deg", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Infrastructure_Horizontal_Angle_deg", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Infrastructure_Decay_Rate_per_day", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Infrastructure_Near_Field_Velocity_m_s", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Infrastructure_Far_Field_Velocity_m_s", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Infrastructure_Receiving_Water_Salinity_PSU", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Infrastructure_Receiving_Water_Temperature_C", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Infrastructure_Receiving_Water_MPN_per_100_ml", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Infrastructure_Distance_From_Shore_m", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Infrastructure_See_Other_ID", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Infrastructure_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Infrastructure_Lat", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Infrastructure_Lng", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Infrastructure_Outfall_Lat", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Infrastructure_Outfall_Lng", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Infrastructure_Civic_Address", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Infrastructure_Google_Address", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Infrastructure_Stat_Box_Model_Scenario_Count", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Infrastructure_Stat_Visual_Plumes_Scenario_Count", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                };
            }
        }
        public List<ReportTreeNode> CreateReportInfrastructureAddressTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Infrastructure_Address_Fields", ReportTreeNodeTypeEnum.ReportInfrastructureAddressType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Infrastructure_Address_Error", ReportTreeNodeTypeEnum.ReportInfrastructureAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Infrastructure_Address_Counter", ReportTreeNodeTypeEnum.ReportInfrastructureAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Infrastructure_Address_ID", ReportTreeNodeTypeEnum.ReportInfrastructureAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Infrastructure_Address_Type", ReportTreeNodeTypeEnum.ReportInfrastructureAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.AddressType),
                    CreateReportTreeNodeItem("Infrastructure_Address_Country", ReportTreeNodeTypeEnum.ReportInfrastructureAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Infrastructure_Address_Province", ReportTreeNodeTypeEnum.ReportInfrastructureAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Infrastructure_Address_Municipality", ReportTreeNodeTypeEnum.ReportInfrastructureAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Infrastructure_Address_Street_Name", ReportTreeNodeTypeEnum.ReportInfrastructureAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Infrastructure_Address_Street_Number", ReportTreeNodeTypeEnum.ReportInfrastructureAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Infrastructure_Address_Street_Type", ReportTreeNodeTypeEnum.ReportInfrastructureAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.StreetType),
                    CreateReportTreeNodeItem("Infrastructure_Address_Postal_Code", ReportTreeNodeTypeEnum.ReportInfrastructureAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Infrastructure_Address_Google_Address_Text", ReportTreeNodeTypeEnum.ReportInfrastructureAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Infrastructure_Address_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportInfrastructureAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Infrastructure_Address_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportInfrastructureAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Infrastructure_Address_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportInfrastructureAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportInfrastructureFileTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Infrastructure_File_Fields", ReportTreeNodeTypeEnum.ReportInfrastructureFileType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Infrastructure_File_Error", ReportTreeNodeTypeEnum.ReportInfrastructureFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Infrastructure_File_Counter", ReportTreeNodeTypeEnum.ReportInfrastructureFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Infrastructure_File_ID", ReportTreeNodeTypeEnum.ReportInfrastructureFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Infrastructure_File_Language", ReportTreeNodeTypeEnum.ReportInfrastructureFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Language),
                    CreateReportTreeNodeItem("Infrastructure_File_Purpose", ReportTreeNodeTypeEnum.ReportInfrastructureFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.FilePurpose),
                    CreateReportTreeNodeItem("Infrastructure_File_Type", ReportTreeNodeTypeEnum.ReportInfrastructureFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.FileType),
                    CreateReportTreeNodeItem("Infrastructure_File_Description", ReportTreeNodeTypeEnum.ReportInfrastructureFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Infrastructure_File_Size_kb", ReportTreeNodeTypeEnum.ReportInfrastructureFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Infrastructure_File_Info", ReportTreeNodeTypeEnum.ReportInfrastructureFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Infrastructure_File_Created_Date_UTC", ReportTreeNodeTypeEnum.ReportInfrastructureFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Infrastructure_File_From_Water", ReportTreeNodeTypeEnum.ReportInfrastructureFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Infrastructure_File_Server_File_Name", ReportTreeNodeTypeEnum.ReportInfrastructureFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Infrastructure_File_Server_File_Path", ReportTreeNodeTypeEnum.ReportInfrastructureFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Infrastructure_File_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportInfrastructureFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Infrastructure_File_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportInfrastructureFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Infrastructure_File_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportInfrastructureFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportMikeBoundaryConditionTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Mike_Boundary_Condition_Fields", ReportTreeNodeTypeEnum.ReportMikeBoundaryConditionType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Mike_Boundary_Condition_Error", ReportTreeNodeTypeEnum.ReportMikeBoundaryConditionType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Mike_Boundary_Condition_Counter", ReportTreeNodeTypeEnum.ReportMikeBoundaryConditionType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Mike_Boundary_Condition_ID", ReportTreeNodeTypeEnum.ReportMikeBoundaryConditionType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Mike_Boundary_Condition_Name", ReportTreeNodeTypeEnum.ReportMikeBoundaryConditionType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Mike_Boundary_Condition_Mike_Boundary_Condition_Code", ReportTreeNodeTypeEnum.ReportMikeBoundaryConditionType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Mike_Boundary_Condition_Mike_Boundary_Condition_Name", ReportTreeNodeTypeEnum.ReportMikeBoundaryConditionType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Mike_Boundary_Condition_Mike_Boundary_Condition_Length_m", ReportTreeNodeTypeEnum.ReportMikeBoundaryConditionType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Mike_Boundary_Condition_Mike_Boundary_Condition_Format", ReportTreeNodeTypeEnum.ReportMikeBoundaryConditionType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Mike_Boundary_Condition_Mike_Boundary_Condition_Level_Or_Velocity", ReportTreeNodeTypeEnum.ReportMikeBoundaryConditionType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Mike_Boundary_Condition_Web_Tide_Data_Set", ReportTreeNodeTypeEnum.ReportMikeBoundaryConditionType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Mike_Boundary_Condition_Number_Of_Web_Tide_Nodes", ReportTreeNodeTypeEnum.ReportMikeBoundaryConditionType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Mike_Boundary_Condition_Last_Update_Date_UTC", ReportTreeNodeTypeEnum.ReportMikeBoundaryConditionType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Mike_Boundary_Condition_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportMikeBoundaryConditionType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Mike_Boundary_Condition_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportMikeBoundaryConditionType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Mike_Boundary_Condition_Lat", ReportTreeNodeTypeEnum.ReportMikeBoundaryConditionType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Mike_Boundary_Condition_Lng", ReportTreeNodeTypeEnum.ReportMikeBoundaryConditionType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                };
            }
        }
        public List<ReportTreeNode> CreateReportMikeScenarioTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Mike_Scenario_Fields", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Mike_Scenario_Special_Result_KML", ReportTreeNodeTypeEnum.ReportMikeScenarioSpecialResultKMLType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Mike_Source", ReportTreeNodeTypeEnum.ReportMikeSourceType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Mike_Boundary_Condition", ReportTreeNodeTypeEnum.ReportMikeBoundaryConditionType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Mike_Scenario_File", ReportTreeNodeTypeEnum.ReportMikeScenarioFileType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Mike_Scenario_ErrorInfo", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Mike_Scenario_Error", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Mike_Scenario_Counter", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Mike_Scenario_ID", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Mike_Scenario_Name_Translation_Status", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TranslationStatus),
                    CreateReportTreeNodeItem("Mike_Scenario_Name", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Mike_Scenario_Is_Active", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Mike_Scenario_Status", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.ScenarioStatus),
                    CreateReportTreeNodeItem("Mike_Scenario_Start_Date_Time_Local", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Mike_Scenario_End_Date_Time_Local", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Mike_Scenario_Start_Execution_Date_Time_Local", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Mike_Scenario_Execution_Time_min", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Mike_Scenario_Wind_Speed_km_h", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Mike_Scenario_Wind_Direction_deg", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Mike_Scenario_Decay_Factor_per_day", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Mike_Scenario_Decay_Is_Constant", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Mike_Scenario_Decay_Factor_Amplitude", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Mike_Scenario_Result_Frequency_min", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Mike_Scenario_Ambient_Temperature_C", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Mike_Scenario_Ambient_Salinity_PSU", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Mike_Scenario_Manning_Number", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Mike_Scenario_Number_Of_Elements", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Mike_Scenario_Number_Of_Time_Steps", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Mike_Scenario_Number_Of_Sigma_Layers", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Mike_Scenario_Number_Of_Z_Layers", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Mike_Scenario_Number_Of_Hydro_Output_Parameters", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Mike_Scenario_Number_Of_Trans_Output_Parameters", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Mike_Scenario_Estimated_Hydro_File_Size", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Mike_Scenario_Estimated_Trans_File_Size", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Mike_Scenario_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Mike_Scenario_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Mike_Scenario_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportMikeScenarioFileTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Mike_Scenario_File_Fields", ReportTreeNodeTypeEnum.ReportMikeScenarioFileType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Mike_Scenario_File_Error", ReportTreeNodeTypeEnum.ReportMikeScenarioFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Mike_Scenario_File_Counter", ReportTreeNodeTypeEnum.ReportMikeScenarioFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Mike_Scenario_File_ID", ReportTreeNodeTypeEnum.ReportMikeScenarioFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Mike_Scenario_File_Language", ReportTreeNodeTypeEnum.ReportMikeScenarioFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Language),
                    CreateReportTreeNodeItem("Mike_Scenario_File_Purpose", ReportTreeNodeTypeEnum.ReportMikeScenarioFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.FilePurpose),
                    CreateReportTreeNodeItem("Mike_Scenario_File_Type", ReportTreeNodeTypeEnum.ReportMikeScenarioFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.FileType),
                    CreateReportTreeNodeItem("Mike_Scenario_File_Description", ReportTreeNodeTypeEnum.ReportMikeScenarioFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Mike_Scenario_File_Size_kb", ReportTreeNodeTypeEnum.ReportMikeScenarioFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Mike_Scenario_File_Info", ReportTreeNodeTypeEnum.ReportMikeScenarioFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Mike_Scenario_File_Created_Date_UTC", ReportTreeNodeTypeEnum.ReportMikeScenarioFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Mike_Scenario_File_From_Water", ReportTreeNodeTypeEnum.ReportMikeScenarioFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Mike_Scenario_File_Server_File_Name", ReportTreeNodeTypeEnum.ReportMikeScenarioFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Mike_Scenario_File_Server_File_Path", ReportTreeNodeTypeEnum.ReportMikeScenarioFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Mike_Scenario_File_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportMikeScenarioFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Mike_Scenario_File_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportMikeScenarioFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Mike_Scenario_File_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportMikeScenarioFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportMikeScenarioSpecialResultKMLTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Mike_Scenario_Special_Result_KML_Fields", ReportTreeNodeTypeEnum.ReportMikeScenarioSpecialResultKMLType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error)
                   };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Mike_Scenario_Special_Result_KML_Error", ReportTreeNodeTypeEnum.ReportMikeScenarioSpecialResultKMLType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Mike_Scenario_Special_Result_KML_Type", ReportTreeNodeTypeEnum.ReportMikeScenarioSpecialResultKMLType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.MikeScenarioSpecialResultKMLType),
                    CreateReportTreeNodeItem("Mike_Scenario_Special_Result_KML_Limit_Values", ReportTreeNodeTypeEnum.ReportMikeScenarioSpecialResultKMLType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportMikeSourceTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Mike_Source_Fields", ReportTreeNodeTypeEnum.ReportMikeSourceType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Mike_Source_Start_End", ReportTreeNodeTypeEnum.ReportMikeSourceStartEndType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Mike_Source_Error", ReportTreeNodeTypeEnum.ReportMikeSourceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Mike_Source_Counter", ReportTreeNodeTypeEnum.ReportMikeSourceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Mike_Source_ID", ReportTreeNodeTypeEnum.ReportMikeSourceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Mike_Source_Name_Translation_Status", ReportTreeNodeTypeEnum.ReportMikeSourceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TranslationStatus),
                    CreateReportTreeNodeItem("Mike_Source_Name", ReportTreeNodeTypeEnum.ReportMikeSourceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Mike_Source_Is_True", ReportTreeNodeTypeEnum.ReportMikeSourceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Mike_Source_Is_Continuous", ReportTreeNodeTypeEnum.ReportMikeSourceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Mike_Source_Include", ReportTreeNodeTypeEnum.ReportMikeSourceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Mike_Source_Is_River", ReportTreeNodeTypeEnum.ReportMikeSourceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Mike_Source_Source_Number_String", ReportTreeNodeTypeEnum.ReportMikeSourceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Mike_Source_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportMikeSourceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Mike_Source_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportMikeSourceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Mike_Source_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportMikeSourceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportMikeSourceStartEndTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Mike_Source_Start_End_Fields", ReportTreeNodeTypeEnum.ReportMikeSourceStartEndType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Mike_Source_Start_End_Error", ReportTreeNodeTypeEnum.ReportMikeSourceStartEndType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Mike_Source_Start_End_Counter", ReportTreeNodeTypeEnum.ReportMikeSourceStartEndType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Mike_Source_Start_End_ID", ReportTreeNodeTypeEnum.ReportMikeSourceStartEndType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Mike_Source_Start_End_Start_Date_And_Time_Local", ReportTreeNodeTypeEnum.ReportMikeSourceStartEndType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Mike_Source_Start_End_End_Date_And_Time_Local", ReportTreeNodeTypeEnum.ReportMikeSourceStartEndType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Mike_Source_Start_End_Source_Flow_Start_m3_day", ReportTreeNodeTypeEnum.ReportMikeSourceStartEndType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Mike_Source_Start_End_Source_Flow_End_m3_day", ReportTreeNodeTypeEnum.ReportMikeSourceStartEndType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Mike_Source_Start_End_Source_Pollution_Start_MPN_100ml", ReportTreeNodeTypeEnum.ReportMikeSourceStartEndType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Mike_Source_Start_End_Source_Pollution_End_MPN_100ml", ReportTreeNodeTypeEnum.ReportMikeSourceStartEndType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Mike_Source_Start_End_Source_Temperature_Start_C", ReportTreeNodeTypeEnum.ReportMikeSourceStartEndType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Mike_Source_Start_End_Source_Temperature_End_C", ReportTreeNodeTypeEnum.ReportMikeSourceStartEndType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Mike_Source_Start_End_Source_Salinity_Start_PSU", ReportTreeNodeTypeEnum.ReportMikeSourceStartEndType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Mike_Source_Start_End_Source_Salinity_End_PSU", ReportTreeNodeTypeEnum.ReportMikeSourceStartEndType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Mike_Source_Start_End_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportMikeSourceStartEndType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Mike_Source_Start_End_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportMikeSourceStartEndType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Mike_Source_Start_End_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportMikeSourceStartEndType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportMPNLookupTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("MPN_Lookup_Fields", ReportTreeNodeTypeEnum.ReportMPNLookupType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("MPN_Lookup_Error", ReportTreeNodeTypeEnum.ReportMPNLookupType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MPN_Lookup_Counter", ReportTreeNodeTypeEnum.ReportMPNLookupType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MPN_Lookup_ID", ReportTreeNodeTypeEnum.ReportMPNLookupType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MPN_Lookup_Tubes_10", ReportTreeNodeTypeEnum.ReportMPNLookupType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MPN_Lookup_Tubes_1_0", ReportTreeNodeTypeEnum.ReportMPNLookupType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MPN_Lookup_Tubes_0_1", ReportTreeNodeTypeEnum.ReportMPNLookupType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MPN_Lookup_MPN_100_ml", ReportTreeNodeTypeEnum.ReportMPNLookupType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MPN_Lookup_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportMPNLookupType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MPN_Lookup_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportMPNLookupType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MPN_Lookup_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportMPNLookupType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportSamplingPlanTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Sampling_Plan_Fields", ReportTreeNodeTypeEnum.ReportSamplingPlanType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Sampling_Plan_Subsector", ReportTreeNodeTypeEnum.ReportSamplingPlanSubsectorType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Sampling_Plan_Error", ReportTreeNodeTypeEnum.ReportSamplingPlanType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Counter", ReportTreeNodeTypeEnum.ReportSamplingPlanType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sampling_Plan_ID", ReportTreeNodeTypeEnum.ReportSamplingPlanType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sampling_Plan_Sampling_Plan_Name", ReportTreeNodeTypeEnum.ReportSamplingPlanType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_For_Group_Name", ReportTreeNodeTypeEnum.ReportSamplingPlanType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Sample_Type", ReportTreeNodeTypeEnum.ReportSamplingPlanType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.SampleType),
                    CreateReportTreeNodeItem("Sampling_Plan_Sampling_Plan_Type", ReportTreeNodeTypeEnum.ReportSamplingPlanType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.SamplingPlanType),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Type", ReportTreeNodeTypeEnum.ReportSamplingPlanType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.LabSheetType),
                    CreateReportTreeNodeItem("Sampling_Plan_Province", ReportTreeNodeTypeEnum.ReportSamplingPlanType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Creator_Name", ReportTreeNodeTypeEnum.ReportSamplingPlanType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Creator_Initial", ReportTreeNodeTypeEnum.ReportSamplingPlanType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Year", ReportTreeNodeTypeEnum.ReportSamplingPlanType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sampling_Plan_Access_Code", ReportTreeNodeTypeEnum.ReportSamplingPlanType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Daily_Duplicate_Precision_Criteria", ReportTreeNodeTypeEnum.ReportSamplingPlanType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Sampling_Plan_Intertech_Duplicate_Precision_Criteria", ReportTreeNodeTypeEnum.ReportSamplingPlanType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Sampling_Plan_Sampling_Plan_File", ReportTreeNodeTypeEnum.ReportSamplingPlanType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Last_Update_Date_UTC", ReportTreeNodeTypeEnum.ReportSamplingPlanType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Sampling_Plan_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportSamplingPlanType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportSamplingPlanType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportSamplingPlanLabSheetTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Fields", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Error", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Counter", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_ID", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Other_Server_Lab_Sheet_ID", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Sampling_Plan_Name", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Province", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_For_Group_Name", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Year", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Month", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Day", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Access_Code", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Daily_Duplicate_Precision_Criteria", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Intertech_Duplicate_Precision_Criteria", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Subsector_Name_Short", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Subsector_Name_Long", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Sampling_Plan_Type", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.SamplingPlanType),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Sample_Type", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.SampleType),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Lab_Sheet_Type", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.LabSheetType),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Status", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.LabSheetStatus),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_File_Name", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_File_Last_Modified_Date_Local", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_File_Content", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Accepted_Or_Rejected_By_Contact_Name", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Accepted_Or_Rejected_By_Contact_Initial", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Accepted_Or_Rejected_Date_Time", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Last_Update_Date_UTC", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportSamplingPlanLabSheetDetailTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Fields", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Tube_And_MPN_Detail", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Error", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Counter", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_ID", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Version", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Run_Date", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Tides", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Sample_Crew_Initials", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Incubation_Bath1_Start_Time", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Incubation_Bath2_Start_Time", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Incubation_Bath3_Start_Time", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Incubation_Bath1_End_Time", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Incubation_Bath2_End_Time", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Incubation_Bath3_End_Time", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Incubation_Bath1_Time_Calculated_minutes", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Incubation_Bath2_Time_Calculated_minutes", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Incubation_Bath3_Time_Calculated_minutes", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Water_Bath1", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Water_Bath2", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Water_Bath3", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_TC_Field_1", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_TC_Lab_1", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_TC_Field_2", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_TC_Lab_2", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_TC_First", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_TC_Average", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Control_Lot", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Positive_35", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Non_Target_35", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Negative_35", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Bath1_Positive_44_5", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Bath2_Positive_44_5", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Bath3_Positive_44_5", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Bath1_Non_Target_44_5", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Bath2_Non_Target_44_5", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Bath3_Non_Target_44_5", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Bath1_Negative_44_5", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Bath2_Negative_44_5", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Bath3_Negative_44_5", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Blank_35", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Bath1_Blank_44_5", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Bath2_Blank_44_5", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Bath3_Blank_44_5", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Lot_35", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Lot_44_5", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Run_Comment", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Run_Weather_Comment", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Sample_Bottle_Lot_Number", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Salinities_Read_By", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Salinities_Read_Date", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Results_Read_By", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Results_Read_Date", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Results_Recorded_By", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Results_Recorded_Date", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Daily_Duplicate_R_Log", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Daily_Duplicate_Precision_Criteria", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Daily_Duplicate_Acceptable", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Intertech_Duplicate_R_Log", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Intertech_Duplicate_Precision_Criteria", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Intertech_Duplicate_Acceptable", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Intertech_Read_Acceptable", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Last_Update_Date_UTC", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Detail_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportSamplingPlanLabSheetTubeAndMPNDetailTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Tube_And_MPN_Detail_Fields", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Tube_And_MPN_Detail_Error", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Tube_And_MPN_Detail_Counter", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Tube_And_MPN_Detail_ID", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Tube_And_MPN_Detail_Ordinal", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Tube_And_MPN_Detail_MWQM_Site", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Tube_And_MPN_Detail_Sample_Date_Time", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Tube_And_MPN_Detail_MPN", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Tube_And_MPN_Detail_Tube_10", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Tube_And_MPN_Detail_Tube_1_0", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Tube_And_MPN_Detail_Tube_0_1", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Tube_And_MPN_Detail_Salinity", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Tube_And_MPN_Detail_Temperature", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Tube_And_MPN_Detail_Processed_By", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Tube_And_MPN_Detail_Sample_Type", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.SampleType),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Tube_And_MPN_Detail_Site_Comment", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Tube_And_MPN_Detail_Last_Update_Date_UTC", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Tube_And_MPN_Detail_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Lab_Sheet_Tube_And_MPN_Detail_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportSamplingPlanLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportSamplingPlanSubsectorTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Sampling_Plan_Subsector_Fields", ReportTreeNodeTypeEnum.ReportSamplingPlanSubsectorType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Sampling_Plan_Subsector_Site", ReportTreeNodeTypeEnum.ReportSamplingPlanSubsectorSiteType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Sampling_Plan_Subsector_Error", ReportTreeNodeTypeEnum.ReportSamplingPlanSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Subsector_Counter", ReportTreeNodeTypeEnum.ReportSamplingPlanSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sampling_Plan_Subsector_ID", ReportTreeNodeTypeEnum.ReportSamplingPlanSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sampling_Plan_Subsector_Name_Short", ReportTreeNodeTypeEnum.ReportSamplingPlanSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Subsector_Name_Long", ReportTreeNodeTypeEnum.ReportSamplingPlanSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Subsector_Last_Update_Date_UTC", ReportTreeNodeTypeEnum.ReportSamplingPlanSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Sampling_Plan_Subsector_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportSamplingPlanSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Subsector_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportSamplingPlanSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Subsector_Lat", ReportTreeNodeTypeEnum.ReportSamplingPlanSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Sampling_Plan_Subsector_Lng", ReportTreeNodeTypeEnum.ReportSamplingPlanSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                };
            }
        }
        public List<ReportTreeNode> CreateReportSamplingPlanSubsectorSiteTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Sampling_Plan_Subsector_Site_Fields", ReportTreeNodeTypeEnum.ReportSamplingPlanSubsectorSiteType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Sampling_Plan_Subsector_Site_Error", ReportTreeNodeTypeEnum.ReportSamplingPlanSubsectorSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Subsector_Site_Counter", ReportTreeNodeTypeEnum.ReportSamplingPlanSubsectorSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sampling_Plan_Subsector_Site_ID", ReportTreeNodeTypeEnum.ReportSamplingPlanSubsectorSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sampling_Plan_Subsector_Site_MWQM_Site", ReportTreeNodeTypeEnum.ReportSamplingPlanSubsectorSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Subsector_Site_Is_Duplicate", ReportTreeNodeTypeEnum.ReportSamplingPlanSubsectorSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Sampling_Plan_Subsector_Site_Last_Update_Date_UTC", ReportTreeNodeTypeEnum.ReportSamplingPlanSubsectorSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Sampling_Plan_Subsector_Site_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportSamplingPlanSubsectorSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Subsector_Site_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportSamplingPlanSubsectorSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sampling_Plan_Subsector_Site_Lat", ReportTreeNodeTypeEnum.ReportSamplingPlanSubsectorSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Sampling_Plan_Subsector_Site_Lng", ReportTreeNodeTypeEnum.ReportSamplingPlanSubsectorSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                };
            }
        }
        public List<ReportTreeNode> CreateReportMWQMRunTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("MWQM_Run_Fields", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("MWQM_Run_Sample", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("MWQM_Run_File", ReportTreeNodeTypeEnum.ReportMWQMRunFileType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("MWQM_Run_Error", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Counter", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Run_ID", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Run_Name_Translation_Status", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TranslationStatus),
                    CreateReportTreeNodeItem("MWQM_Run_Name", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Is_Active", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("MWQM_Run_Date_Time_Local", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Run_Start_Date_Time_Local", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Run_End_Date_Time_Local", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Received_Date_Time_Local", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Run_Temperature_Control_1_C", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Temperature_Control_2_C", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Sea_State_At_Start_Beaufort_Scale", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.BeaufortScale),
                    CreateReportTreeNodeItem("MWQM_Run_Sea_State_At_End_Beaufort_Scale", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.BeaufortScale),
                    CreateReportTreeNodeItem("MWQM_Run_Water_Level_At_Brook_m", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Wave_Hight_At_Start_m", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Wave_Hight_At_End_m", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Crew_Initials", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Analyze_Method", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.AnalyzeMethod),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Matrix", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.SampleMatrix),
                    CreateReportTreeNodeItem("MWQM_Run_Laboratory", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Laboratory),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Status", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.SampleStatus),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sample_Approval_Contact_Name", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sample_Approval_Contact_Initial", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Analyze_Bath1_Incubation_Start_Date_Time_Local", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Analyze_Bath2_Incubation_Start_Date_Time_Local", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Analyze_Bath3_Incubation_Start_Date_Time_Local", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Run_Sample_Approval_Date_Time_Local", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Run_Rain_Day_0_mm", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Rain_Day_1_mm", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Rain_Day_2_mm", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Rain_Day_3_mm", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Rain_Day_4_mm", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Rain_Day_5_mm", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Rain_Day_6_mm", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Rain_Day_7_mm", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Rain_Day_8_mm", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Rain_Day_9_mm", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Rain_Day_10_mm", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Comment_Translation_Status", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TranslationStatus),
                    CreateReportTreeNodeItem("MWQM_Run_Comment", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Weather_Comment_Translation_Status", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TranslationStatus),
                    CreateReportTreeNodeItem("MWQM_Run_Weather_Comment", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Run_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Stat_MWQM_Site_Count", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Run_Stat_Sample_Count", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                };
            }
        }
        public List<ReportTreeNode> CreateReportMWQMRunFileTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("MWQM_Run_File_Fields", ReportTreeNodeTypeEnum.ReportMWQMRunFileType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("MWQM_Run_File_Error", ReportTreeNodeTypeEnum.ReportMWQMRunFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_File_Counter", ReportTreeNodeTypeEnum.ReportMWQMRunFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Run_File_ID", ReportTreeNodeTypeEnum.ReportMWQMRunFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Run_File_Language", ReportTreeNodeTypeEnum.ReportMWQMRunFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Language),
                    CreateReportTreeNodeItem("MWQM_Run_File_Purpose", ReportTreeNodeTypeEnum.ReportMWQMRunFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.FilePurpose),
                    CreateReportTreeNodeItem("MWQM_Run_File_Type", ReportTreeNodeTypeEnum.ReportMWQMRunFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.FileType),
                    CreateReportTreeNodeItem("MWQM_Run_File_Description", ReportTreeNodeTypeEnum.ReportMWQMRunFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_File_Size_kb", ReportTreeNodeTypeEnum.ReportMWQMRunFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Run_File_Info", ReportTreeNodeTypeEnum.ReportMWQMRunFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_File_Created_Date_UTC", ReportTreeNodeTypeEnum.ReportMWQMRunFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Run_File_From_Water", ReportTreeNodeTypeEnum.ReportMWQMRunFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("MWQM_Run_File_Server_File_Name", ReportTreeNodeTypeEnum.ReportMWQMRunFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_File_Server_File_Path", ReportTreeNodeTypeEnum.ReportMWQMRunFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_File_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportMWQMRunFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Run_File_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportMWQMRunFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_File_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportMWQMRunFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportMWQMRunLabSheetTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Fields", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Error", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Counter", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_ID", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Other_Server_Lab_Sheet_ID", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Sampling_Plan_Name", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Province", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_For_Group_Name", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Year", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Month", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Day", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Access_Code", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Daily_Duplicate_Precision_Criteria", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Intertech_Duplicate_Precision_Criteria", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Subsector_Name_Short", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Subsector_Name_Long", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_MWQM_Run_Name", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Sampling_Plan_Type", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.SamplingPlanType),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Sample_Type", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.SampleType),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Type", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.LabSheetType),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Status", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.LabSheetStatus),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_File_Name", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_File_Last_Modified_Date_Local", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_File_Content", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Accepted_Or_Rejected_By_Contact_Name", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Accepted_Or_Rejected_By_Contact_Initial", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Accepted_Or_Rejected_Date_Time", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Last_Update_Date_UTC", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportMWQMRunLabSheetDetailTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Fields", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Tube_And_MPN_Detail", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Error", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Counter", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_ID", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Version", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Run_Date", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Tides", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Sample_Crew_Initials", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Incubation_Bath1_Start_Time", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Incubation_Bath2_Start_Time", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Incubation_Bath3_Start_Time", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Incubation_Bath1_End_Time", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Incubation_Bath2_End_Time", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Incubation_Bath3_End_Time", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Incubation_Bath1_Time_Calculated_minutes", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Incubation_Bath2_Time_Calculated_minutes", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Incubation_Bath3_Time_Calculated_minutes", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Water_Bath1", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Water_Bath2", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Water_Bath3", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_TC_Field_1", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_TC_Lab_1", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_TC_Field_2", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_TC_Lab_2", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_TC_First", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_TC_Average", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Control_Lot", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Positive_35", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Non_Target_35", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Negative_35", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Bath1_Positive_44_5", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Bath2_Positive_44_5", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Bath3_Positive_44_5", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Bath1_Non_Target_44_5", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Bath2_Non_Target_44_5", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Bath3_Non_Target_44_5", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Bath1_Negative_44_5", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Bath2_Negative_44_5", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Bath3_Negative_44_5", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Blank_35", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Bath1_Blank_44_5", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Bath2_Blank_44_5", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Bath3_Blank_44_5", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Lot_35", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Lot_44_5", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Run_Comment", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Run_Weather_Comment", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Sample_Bottle_Lot_Number", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Salinities_Read_By", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Salinities_Read_Date", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Results_Read_By", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Results_Read_Date", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Results_Recorded_By", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Results_Recorded_Date", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Daily_Duplicate_R_Log", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Daily_Duplicate_Precision_Criteria", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Daily_Duplicate_Acceptable", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Intertech_Duplicate_R_Log", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Intertech_Duplicate_Precision_Criteria", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Intertech_Duplicate_Acceptable", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Intertech_Read_Acceptable", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Last_Update_Date_UTC", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Detail_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportMWQMRunLabSheetTubeMPNDetailTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Tube_And_MPN_Detail_Fields", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Tube_And_MPN_Detail_Error", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Tube_And_MPN_Detail_Counter", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Tube_And_MPN_Detail_ID", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Tube_And_MPN_Detail_Ordinal", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Tube_And_MPN_Detail_MWQM_Site", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Tube_And_MPN_Detail_Sample_Date_Time", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Tube_And_MPN_Detail_MPN", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Tube_And_MPN_Detail_Tube_10", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Tube_And_MPN_Detail_Tube_1_0", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Tube_And_MPN_Detail_Tube_0_1", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Tube_And_MPN_Detail_Salinity", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Tube_And_MPN_Detail_Temperature", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Tube_And_MPN_Detail_Processed_By", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Tube_And_MPN_Detail_Sample_Type", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.SampleType),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Tube_And_MPN_Detail_Site_Comment", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Tube_And_MPN_Detail_Last_Update_Date_UTC", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Tube_And_MPN_Detail_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Lab_Sheet_Tube_And_MPN_Detail_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportMWQMRunLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportMWQMRunSampleTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Fields", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Error", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Counter", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_ID", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Date_Time_Local", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_MWQM_Site", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Depth_m", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Fec_Col_MPN_100_ml", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Salinity_PPT", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Water_Temp_C", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_PH", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Types", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.SampleType),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Tube_10", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Tube_1_0", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Tube_0_1", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Processed_By", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Note_Translation_Status", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TranslationStatus),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Note", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Rain_Day_0_mm", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Rain_Day_1_mm", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Rain_Day_2_mm", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Rain_Day_3_mm", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Rain_Day_4_mm", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Rain_Day_5_mm", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Rain_Day_6_mm", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Rain_Day_7_mm", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Rain_Day_8_mm", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Rain_Day_9_mm", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Rain_Day_10_mm", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Tide_Start", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TideText),
                    CreateReportTreeNodeItem("MWQM_Run_Sample_Tide_End", ReportTreeNodeTypeEnum.ReportMWQMRunSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TideText),
                };
            }
        }
        public List<ReportTreeNode> CreateReportMWQMSiteTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("MWQM_Site_Fields", ReportTreeNodeTypeEnum.ReportMWQMSiteType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("MWQM_Site_Sample", ReportTreeNodeTypeEnum.ReportMWQMSiteSampleType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("MWQM_Site_Start_And_End_Date", ReportTreeNodeTypeEnum.ReportMWQMSiteStartAndEndType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("MWQM_Site_File", ReportTreeNodeTypeEnum.ReportMWQMSiteFileType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("MWQM_Site_Error", ReportTreeNodeTypeEnum.ReportMWQMSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Site_Counter", ReportTreeNodeTypeEnum.ReportMWQMSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Site_ID", ReportTreeNodeTypeEnum.ReportMWQMSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Site_Name_Translation_Status", ReportTreeNodeTypeEnum.ReportMWQMSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TranslationStatus),
                    CreateReportTreeNodeItem("MWQM_Site_Name", ReportTreeNodeTypeEnum.ReportMWQMSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Site_Is_Active", ReportTreeNodeTypeEnum.ReportMWQMSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("MWQM_Site_Number",ReportTreeNodeTypeEnum.ReportMWQMSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Site_Description", ReportTreeNodeTypeEnum.ReportMWQMSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Site_Latest_Classification", ReportTreeNodeTypeEnum.ReportMWQMSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.MWQMSiteLatestClassification),
                    CreateReportTreeNodeItem("MWQM_Site_Ordinal", ReportTreeNodeTypeEnum.ReportMWQMSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Site_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportMWQMSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Site_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportMWQMSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Site_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportMWQMSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Site_Lat", ReportTreeNodeTypeEnum.ReportMWQMSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Site_Lng", ReportTreeNodeTypeEnum.ReportMWQMSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Site_Stat_MWQM_Run_Count", ReportTreeNodeTypeEnum.ReportMWQMSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Site_Stat_MWQM_Sample_Count", ReportTreeNodeTypeEnum.ReportMWQMSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Site_Stat_X_Last_Samples", ReportTreeNodeTypeEnum.ReportMWQMSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Site_Stat_GM_X_Last_Samples", ReportTreeNodeTypeEnum.ReportMWQMSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Site_Stat_Median_X_Last_Samples", ReportTreeNodeTypeEnum.ReportMWQMSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Site_Stat_P90_X_Last_Samples", ReportTreeNodeTypeEnum.ReportMWQMSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Site_Stat_P90_Over_43_X_Last_Samples", ReportTreeNodeTypeEnum.ReportMWQMSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Site_Stat_P90_Over_260_X_Last_Samples", ReportTreeNodeTypeEnum.ReportMWQMSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Site_Stat_Min_FC_X_Last_Samples", ReportTreeNodeTypeEnum.ReportMWQMSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Site_Stat_Max_FC_X_Last_Samples", ReportTreeNodeTypeEnum.ReportMWQMSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Site_Stat_Min_Year_X_Last_Samples", ReportTreeNodeTypeEnum.ReportMWQMSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Site_Stat_Max_Year_X_Last_Samples", ReportTreeNodeTypeEnum.ReportMWQMSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Site_Stat_Sample_Count_X_Last_Samples", ReportTreeNodeTypeEnum.ReportMWQMSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                };
            }
        }
        public List<ReportTreeNode> CreateReportMWQMSiteFileTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("MWQM_Site_File_Fields", ReportTreeNodeTypeEnum.ReportMWQMSiteFileType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("MWQM_Site_File_Error", ReportTreeNodeTypeEnum.ReportMWQMSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Site_File_Counter", ReportTreeNodeTypeEnum.ReportMWQMSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Site_File_ID", ReportTreeNodeTypeEnum.ReportMWQMSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Site_File_Language", ReportTreeNodeTypeEnum.ReportMWQMSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Language),
                    CreateReportTreeNodeItem("MWQM_Site_File_Purpose", ReportTreeNodeTypeEnum.ReportMWQMSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.FilePurpose),
                    CreateReportTreeNodeItem("MWQM_Site_File_Type", ReportTreeNodeTypeEnum.ReportMWQMSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.FileType),
                    CreateReportTreeNodeItem("MWQM_Site_File_Description", ReportTreeNodeTypeEnum.ReportMWQMSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Site_File_Size_kb", ReportTreeNodeTypeEnum.ReportMWQMSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Site_File_Info", ReportTreeNodeTypeEnum.ReportMWQMSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Site_File_Created_Date_UTC", ReportTreeNodeTypeEnum.ReportMWQMSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Site_File_From_Water", ReportTreeNodeTypeEnum.ReportMWQMSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("MWQM_Site_File_Server_File_Name", ReportTreeNodeTypeEnum.ReportMWQMSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Site_File_Server_File_Path", ReportTreeNodeTypeEnum.ReportMWQMSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Site_File_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportMWQMSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Site_File_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportMWQMSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Site_File_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportMWQMSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportMWQMSiteSampleTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("MWQM_Site_Sample_Fields", ReportTreeNodeTypeEnum.ReportMWQMSiteSampleType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("MWQM_Site_Sample_Error", ReportTreeNodeTypeEnum.ReportMWQMSiteSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Site_Sample_Counter", ReportTreeNodeTypeEnum.ReportMWQMSiteSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Site_Sample_ID", ReportTreeNodeTypeEnum.ReportMWQMSiteSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Site_Sample_Date_Time_Local", ReportTreeNodeTypeEnum.ReportMWQMSiteSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Site_Sample_Depth_m", ReportTreeNodeTypeEnum.ReportMWQMSiteSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Site_Sample_Fec_Col_MPN_100_ml", ReportTreeNodeTypeEnum.ReportMWQMSiteSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Site_Sample_Salinity_PPT", ReportTreeNodeTypeEnum.ReportMWQMSiteSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Site_Sample_Water_Temp_C", ReportTreeNodeTypeEnum.ReportMWQMSiteSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Site_Sample_PH", ReportTreeNodeTypeEnum.ReportMWQMSiteSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("MWQM_Site_Sample_Types", ReportTreeNodeTypeEnum.ReportMWQMSiteSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.SampleType),
                    CreateReportTreeNodeItem("MWQM_Site_Sample_Tube_10", ReportTreeNodeTypeEnum.ReportMWQMSiteSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Site_Sample_Tube_1_0", ReportTreeNodeTypeEnum.ReportMWQMSiteSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Site_Sample_Tube_0_1", ReportTreeNodeTypeEnum.ReportMWQMSiteSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Site_Sample_Processed_By", ReportTreeNodeTypeEnum.ReportMWQMSiteSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Site_Sample_Note_Translation_Status", ReportTreeNodeTypeEnum.ReportMWQMSiteSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TranslationStatus),
                    CreateReportTreeNodeItem("MWQM_Site_Sample_Note", ReportTreeNodeTypeEnum.ReportMWQMSiteSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Site_Sample_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportMWQMSiteSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Site_Sample_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportMWQMSiteSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Site_Sample_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportMWQMSiteSampleType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportMWQMSiteStartAndEndDateTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("MWQM_Site_Start_And_End_Date_Fields", ReportTreeNodeTypeEnum.ReportMWQMSiteStartAndEndType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("MWQM_Site_Start_And_End_Date_Error", ReportTreeNodeTypeEnum.ReportMWQMSiteStartAndEndType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Site_Start_And_End_Date_Counter", ReportTreeNodeTypeEnum.ReportMWQMSiteStartAndEndType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Site_Start_And_End_Date_ID", ReportTreeNodeTypeEnum.ReportMWQMSiteStartAndEndType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("MWQM_Site_Start_And_End_Date_Start_Date", ReportTreeNodeTypeEnum.ReportMWQMSiteStartAndEndType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Site_Start_And_End_Date_End_Date", ReportTreeNodeTypeEnum.ReportMWQMSiteStartAndEndType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Site_Start_And_End_Date_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportMWQMSiteStartAndEndType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("MWQM_Site_Start_And_End_Date_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportMWQMSiteStartAndEndType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("MWQM_Site_Start_And_End_Date_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportMWQMSiteStartAndEndType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportMunicipalityTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Municipality_Fields", ReportTreeNodeTypeEnum.ReportMunicipalityType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Infrastructure", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.TableSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Mike_Scenario", ReportTreeNodeTypeEnum.ReportMikeScenarioType, ReportTreeNodeSubTypeEnum.TableSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Municipality_Contact", ReportTreeNodeTypeEnum.ReportMunicipalityContactType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Municipality_File", ReportTreeNodeTypeEnum.ReportMunicipalityFileType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Municipality_Error", ReportTreeNodeTypeEnum.ReportMunicipalityType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_Counter", ReportTreeNodeTypeEnum.ReportMunicipalityType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Municipality_ID", ReportTreeNodeTypeEnum.ReportMunicipalityType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Municipality_Name_Translation_Status", ReportTreeNodeTypeEnum.ReportMunicipalityType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TranslationStatus),
                    CreateReportTreeNodeItem("Municipality_Name", ReportTreeNodeTypeEnum.ReportMunicipalityType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_Is_Active", ReportTreeNodeTypeEnum.ReportMunicipalityType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Municipality_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportMunicipalityType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Municipality_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportMunicipalityType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportMunicipalityType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_Lat", ReportTreeNodeTypeEnum.ReportMunicipalityType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Municipality_Lng", ReportTreeNodeTypeEnum.ReportMunicipalityType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Municipality_Stat_Lift_Station_Count", ReportTreeNodeTypeEnum.ReportMunicipalityType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Municipality_Stat_WWTP_Count", ReportTreeNodeTypeEnum.ReportMunicipalityType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Municipality_Stat_Mike_Scenario_Count", ReportTreeNodeTypeEnum.ReportMunicipalityType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Municipality_Stat_Box_Model_Scenario_Count", ReportTreeNodeTypeEnum.ReportMunicipalityType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Municipality_Stat_Visual_Plumes_Scenario_Count", ReportTreeNodeTypeEnum.ReportMunicipalityType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                };
            }
        }
        public List<ReportTreeNode> CreateReportMunicipalityContactTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Municipality_Contact_Fields", ReportTreeNodeTypeEnum.ReportMunicipalityContactType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Municipality_Contact_Address", ReportTreeNodeTypeEnum.ReportMunicipalityContactAddressType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Municipality_Contact_Tel", ReportTreeNodeTypeEnum.ReportMunicipalityContactTelType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Municipality_Contact_Email", ReportTreeNodeTypeEnum.ReportMunicipalityContactEmailType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Municipality_Contact_Error", ReportTreeNodeTypeEnum.ReportMunicipalityContactType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_Contact_Counter", ReportTreeNodeTypeEnum.ReportMunicipalityContactType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Municipality_Contact_ID", ReportTreeNodeTypeEnum.ReportMunicipalityContactType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Municipality_Contact_First_Name", ReportTreeNodeTypeEnum.ReportMunicipalityContactType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_Contact_Initial", ReportTreeNodeTypeEnum.ReportMunicipalityContactType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_Contact_Last_Name", ReportTreeNodeTypeEnum.ReportMunicipalityContactType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_Contact_Full_Name", ReportTreeNodeTypeEnum.ReportMunicipalityContactType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_Contact_Title", ReportTreeNodeTypeEnum.ReportMunicipalityContactType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.ContactTitle),
                    CreateReportTreeNodeItem("Municipality_Contact_Tels", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_Contact_Emails", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_Contact_Civic_Address", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_Contact_Google_Address", ReportTreeNodeTypeEnum.ReportInfrastructureType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_Contact_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportMunicipalityContactType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Municipality_Contact_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportMunicipalityContactType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_Contact_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportMunicipalityContactType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportMunicipalityContactAddressTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Municipality_Contact_Address_Fields", ReportTreeNodeTypeEnum.ReportMunicipalityContactAddressType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Municipality_Contact_Address_Error", ReportTreeNodeTypeEnum.ReportMunicipalityContactAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_Contact_Address_Counter", ReportTreeNodeTypeEnum.ReportMunicipalityContactAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Municipality_Contact_Address_ID", ReportTreeNodeTypeEnum.ReportMunicipalityContactAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Municipality_Contact_Address_Type", ReportTreeNodeTypeEnum.ReportMunicipalityContactAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.AddressType),
                    CreateReportTreeNodeItem("Municipality_Contact_Address_Country", ReportTreeNodeTypeEnum.ReportMunicipalityContactAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_Contact_Address_Province", ReportTreeNodeTypeEnum.ReportMunicipalityContactAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_Contact_Address_Municipality", ReportTreeNodeTypeEnum.ReportMunicipalityContactAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_Contact_Address_Street_Name", ReportTreeNodeTypeEnum.ReportMunicipalityContactAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_Contact_Address_Street_Number", ReportTreeNodeTypeEnum.ReportMunicipalityContactAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_Contact_Address_Street_Type", ReportTreeNodeTypeEnum.ReportMunicipalityContactAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.StreetType),
                    CreateReportTreeNodeItem("Municipality_Contact_Address_Postal_Code", ReportTreeNodeTypeEnum.ReportMunicipalityContactAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_Contact_Address_Google_Address_Text", ReportTreeNodeTypeEnum.ReportMunicipalityContactAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_Contact_Address_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportMunicipalityContactAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Municipality_Contact_Address_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportMunicipalityContactAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_Contact_Address_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportMunicipalityContactAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportMunicipalityContactEmailTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Municipality_Contact_Email_Fields", ReportTreeNodeTypeEnum.ReportMunicipalityContactEmailType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Municipality_Contact_Email_Error", ReportTreeNodeTypeEnum.ReportMunicipalityContactEmailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_Contact_Email_Counter", ReportTreeNodeTypeEnum.ReportMunicipalityContactEmailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Municipality_Contact_Email_ID", ReportTreeNodeTypeEnum.ReportMunicipalityContactEmailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Municipality_Contact_Email_Type", ReportTreeNodeTypeEnum.ReportMunicipalityContactEmailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.EmailType),
                    CreateReportTreeNodeItem("Municipality_Contact_Email_Address", ReportTreeNodeTypeEnum.ReportMunicipalityContactEmailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_Contact_Email_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportMunicipalityContactEmailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Municipality_Contact_Email_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportMunicipalityContactEmailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_Contact_Email_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportMunicipalityContactEmailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportMunicipalityContactTelTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Municipality_Contact_Tel_Fields", ReportTreeNodeTypeEnum.ReportMunicipalityContactTelType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Municipality_Contact_Tel_Error", ReportTreeNodeTypeEnum.ReportMunicipalityContactTelType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_Contact_Tel_Counter", ReportTreeNodeTypeEnum.ReportMunicipalityContactTelType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Municipality_Contact_Tel_ID", ReportTreeNodeTypeEnum.ReportMunicipalityContactTelType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Municipality_Contact_Tel_Type", ReportTreeNodeTypeEnum.ReportMunicipalityContactTelType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TelType),
                    CreateReportTreeNodeItem("Municipality_Contact_Tel_Number", ReportTreeNodeTypeEnum.ReportMunicipalityContactTelType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_Contact_Tel_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportMunicipalityContactTelType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Municipality_Contact_Tel_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportMunicipalityContactTelType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_Contact_Tel_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportMunicipalityContactTelType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportMunicipalityFileTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Municipality_File_Fields", ReportTreeNodeTypeEnum.ReportMunicipalityFileType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Municipality_File_Error", ReportTreeNodeTypeEnum.ReportMunicipalityFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_File_Counter", ReportTreeNodeTypeEnum.ReportMunicipalityFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Municipality_File_ID", ReportTreeNodeTypeEnum.ReportMunicipalityFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Municipality_File_Language", ReportTreeNodeTypeEnum.ReportMunicipalityFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Language),
                    CreateReportTreeNodeItem("Municipality_File_Purpose", ReportTreeNodeTypeEnum.ReportMunicipalityFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.FilePurpose),
                    CreateReportTreeNodeItem("Municipality_File_Type", ReportTreeNodeTypeEnum.ReportMunicipalityFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.FileType),
                    CreateReportTreeNodeItem("Municipality_File_Description", ReportTreeNodeTypeEnum.ReportMunicipalityFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_File_Size_kb", ReportTreeNodeTypeEnum.ReportMunicipalityFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Municipality_File_Info", ReportTreeNodeTypeEnum.ReportMunicipalityFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_File_Created_Date_UTC", ReportTreeNodeTypeEnum.ReportMunicipalityFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Municipality_File_From_Water", ReportTreeNodeTypeEnum.ReportMunicipalityFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Municipality_File_Server_File_Name", ReportTreeNodeTypeEnum.ReportMunicipalityFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_File_Server_File_Path", ReportTreeNodeTypeEnum.ReportMunicipalityFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_File_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportMunicipalityFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Municipality_File_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportMunicipalityFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Municipality_File_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportMunicipalityFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportProvinceTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Province_Fields", ReportTreeNodeTypeEnum.ReportProvinceType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Area", ReportTreeNodeTypeEnum.ReportAreaType, ReportTreeNodeSubTypeEnum.TableSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Sampling_Plan", ReportTreeNodeTypeEnum.ReportSamplingPlanType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Climate_Site", ReportTreeNodeTypeEnum.ReportClimateSiteType, ReportTreeNodeSubTypeEnum.TableSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Hydrometric_Site", ReportTreeNodeTypeEnum.ReportHydrometricSiteType, ReportTreeNodeSubTypeEnum.TableSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Province_File", ReportTreeNodeTypeEnum.ReportProvinceFileType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Province_Error", ReportTreeNodeTypeEnum.ReportProvinceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Province_Counter", ReportTreeNodeTypeEnum.ReportProvinceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Province_ID", ReportTreeNodeTypeEnum.ReportProvinceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Province_Name_Translation_Status", ReportTreeNodeTypeEnum.ReportProvinceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TranslationStatus),
                    CreateReportTreeNodeItem("Province_Name", ReportTreeNodeTypeEnum.ReportProvinceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Province_Initial", ReportTreeNodeTypeEnum.ReportProvinceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Province_Is_Active", ReportTreeNodeTypeEnum.ReportProvinceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Province_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportProvinceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Province_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportProvinceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Province_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportProvinceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Province_Lat", ReportTreeNodeTypeEnum.ReportProvinceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Province_Lng", ReportTreeNodeTypeEnum.ReportProvinceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Province_Stat_Area_Count", ReportTreeNodeTypeEnum.ReportProvinceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Province_Stat_Sector_Count", ReportTreeNodeTypeEnum.ReportProvinceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Province_Stat_Subsector_Count", ReportTreeNodeTypeEnum.ReportProvinceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Province_Stat_Municipality_Count", ReportTreeNodeTypeEnum.ReportProvinceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Province_Stat_Lift_Station_Count", ReportTreeNodeTypeEnum.ReportProvinceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Province_Stat_WWTP_Count", ReportTreeNodeTypeEnum.ReportProvinceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Province_Stat_MWQM_Sample_Count", ReportTreeNodeTypeEnum.ReportProvinceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Province_Stat_MWQM_Site_Count", ReportTreeNodeTypeEnum.ReportProvinceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Province_Stat_MWQM_Run_Count", ReportTreeNodeTypeEnum.ReportProvinceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Province_Stat_Pol_Source_Site_Count", ReportTreeNodeTypeEnum.ReportProvinceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Province_Stat_Mike_Scenario_Count", ReportTreeNodeTypeEnum.ReportProvinceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Province_Stat_Box_Model_Scenario_Count", ReportTreeNodeTypeEnum.ReportProvinceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Province_Stat_Visual_Plumes_Scenario_Count", ReportTreeNodeTypeEnum.ReportProvinceType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                };
            }
        }
        public List<ReportTreeNode> CreateReportProvinceFileTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Province_File_Fields", ReportTreeNodeTypeEnum.ReportProvinceFileType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Province_File_Error", ReportTreeNodeTypeEnum.ReportProvinceFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Province_File_Counter", ReportTreeNodeTypeEnum.ReportProvinceFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Province_File_ID", ReportTreeNodeTypeEnum.ReportProvinceFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Province_File_Language", ReportTreeNodeTypeEnum.ReportProvinceFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Language),
                    CreateReportTreeNodeItem("Province_File_Purpose", ReportTreeNodeTypeEnum.ReportProvinceFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.FilePurpose),
                    CreateReportTreeNodeItem("Province_File_Type", ReportTreeNodeTypeEnum.ReportProvinceFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.FileType),
                    CreateReportTreeNodeItem("Province_File_Description", ReportTreeNodeTypeEnum.ReportProvinceFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Province_File_Size_kb", ReportTreeNodeTypeEnum.ReportProvinceFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Province_File_Info", ReportTreeNodeTypeEnum.ReportProvinceFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Province_File_Created_Date_UTC", ReportTreeNodeTypeEnum.ReportProvinceFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Province_File_From_Water", ReportTreeNodeTypeEnum.ReportProvinceFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Province_File_Server_File_Name", ReportTreeNodeTypeEnum.ReportProvinceFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Province_File_Server_File_Path", ReportTreeNodeTypeEnum.ReportProvinceFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Province_File_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportProvinceFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Province_File_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportProvinceFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Province_File_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportProvinceFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportPolSourceSiteTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Pol_Source_Site_Fields", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Pol_Source_Site_File", ReportTreeNodeTypeEnum.ReportPolSourceSiteFileType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Pol_Source_Site_Address", ReportTreeNodeTypeEnum.ReportPolSourceSiteAddressType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Pol_Source_Site_Error", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Counter", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Pol_Source_Site_ID", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Pol_Source_Site_Name_Translation_Status", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TranslationStatus),
                    CreateReportTreeNodeItem("Pol_Source_Site_Name", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Last_Obs_Issue_Item_Text", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Last_Obs_Issue_Item_Text_First_Initial", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Last_Obs_Issue_Item_Text_Second_Initial", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Last_Obs_Issue_Item_Text_Last_Initial", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Last_Obs_Issue_Item_Text_Start_Level", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Pol_Source_Site_Last_Obs_Issue_Item_Text_End_Level", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Pol_Source_Site_Last_Obs_Issue_Item_Risk_Text", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Last_Obs_Issue_Item_Risk_Text_Initial", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Last_Obs_Issue_Sentence", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Last_Obs_Issue_Filtering", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.PolSourceObsInfo),
                    CreateReportTreeNodeItem("Pol_Source_Site_Last_Obs_Issue_Filtering_Reverse_Equal", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Pol_Source_Site_Last_Obs_Issue_Risk", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.PolSourceIssueRisk),
                    CreateReportTreeNodeItem("Pol_Source_Site_Last_Obs_Issue_Item_Enum_ID_List", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsIssueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Is_Active", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Pol_Source_Site_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Pol_Source_Site_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Lat", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Pol_Source_Site_Lng", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Pol_Source_Site_Old_Site_Id", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Pol_Source_Site_Site_ID", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Pol_Source_Site_Site", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Pol_Source_Site_Is_Point_Source", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Pol_Source_Site_Inactive_Reason", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.PolSourceInactiveReason),
                    CreateReportTreeNodeItem("Pol_Source_Site_Civic_Address", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Google_Address", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportPolSourceSiteAddressTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Pol_Source_Site_Address_Fields", ReportTreeNodeTypeEnum.ReportPolSourceSiteAddressType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Pol_Source_Site_Address_Error", ReportTreeNodeTypeEnum.ReportPolSourceSiteAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Address_Counter", ReportTreeNodeTypeEnum.ReportPolSourceSiteAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Pol_Source_Site_Address_ID", ReportTreeNodeTypeEnum.ReportPolSourceSiteAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Pol_Source_Site_Address_Type", ReportTreeNodeTypeEnum.ReportPolSourceSiteAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.AddressType),
                    CreateReportTreeNodeItem("Pol_Source_Site_Address_Country", ReportTreeNodeTypeEnum.ReportPolSourceSiteAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Address_Province", ReportTreeNodeTypeEnum.ReportPolSourceSiteAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Address_Municipality", ReportTreeNodeTypeEnum.ReportPolSourceSiteAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Address_Street_Name", ReportTreeNodeTypeEnum.ReportPolSourceSiteAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Address_Street_Number", ReportTreeNodeTypeEnum.ReportPolSourceSiteAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Address_Street_Type", ReportTreeNodeTypeEnum.ReportPolSourceSiteAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.StreetType),
                    CreateReportTreeNodeItem("Pol_Source_Site_Address_Postal_Code", ReportTreeNodeTypeEnum.ReportPolSourceSiteAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Address_Google_Address_Text", ReportTreeNodeTypeEnum.ReportPolSourceSiteAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Address_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportPolSourceSiteAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Pol_Source_Site_Address_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportPolSourceSiteAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Address_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportPolSourceSiteAddressType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportPolSourceSiteFileTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Pol_Source_Site_File_Fields", ReportTreeNodeTypeEnum.ReportPolSourceSiteFileType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Pol_Source_Site_File_Error", ReportTreeNodeTypeEnum.ReportPolSourceSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_File_Counter", ReportTreeNodeTypeEnum.ReportPolSourceSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Pol_Source_Site_File_ID", ReportTreeNodeTypeEnum.ReportPolSourceSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Pol_Source_Site_File_Language", ReportTreeNodeTypeEnum.ReportPolSourceSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Language),
                    CreateReportTreeNodeItem("Pol_Source_Site_File_Purpose", ReportTreeNodeTypeEnum.ReportPolSourceSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.FilePurpose),
                    CreateReportTreeNodeItem("Pol_Source_Site_File_Type", ReportTreeNodeTypeEnum.ReportPolSourceSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.FileType),
                    CreateReportTreeNodeItem("Pol_Source_Site_File_Description", ReportTreeNodeTypeEnum.ReportPolSourceSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_File_Size_kb", ReportTreeNodeTypeEnum.ReportPolSourceSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Pol_Source_Site_File_Info", ReportTreeNodeTypeEnum.ReportPolSourceSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_File_Created_Date_UTC", ReportTreeNodeTypeEnum.ReportPolSourceSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Pol_Source_Site_File_From_Water", ReportTreeNodeTypeEnum.ReportPolSourceSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Pol_Source_Site_File_Server_File_Name", ReportTreeNodeTypeEnum.ReportPolSourceSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_File_Server_File_Path", ReportTreeNodeTypeEnum.ReportPolSourceSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_File_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportPolSourceSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Pol_Source_Site_File_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportPolSourceSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_File_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportPolSourceSiteFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportPolSourceSiteObsTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Fields", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Issue", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsIssueType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Error", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Counter", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_ID", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Observation_Date_Local", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Inspector_Name", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Inspector_Initial", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Observation_To_Be_Deleted", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Only_Last", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                };
            }
        }
        public List<ReportTreeNode> CreateReportPolSourceSiteObsIssueTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Issue_Fields", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsIssueType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Issue_Error", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsIssueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Issue_Counter", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsIssueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Issue_ID", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsIssueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Issue_Items_Text", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsIssueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Issue_Items_Text_First_Initial", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsIssueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Issue_Items_Text_Second_Initial", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsIssueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Issue_Items_Text_Last_Initial", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsIssueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Issue_Items_Text_Start_Level", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsIssueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Issue_Items_Text_End_Level", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsIssueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Issue_Items_Risk_Text", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsIssueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Issue_Items_Risk_Text_Initial", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsIssueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Issue_Sentence", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsIssueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Issue_Filtering", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsIssueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.PolSourceObsInfo),
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Issue_Filtering_Reverse_Equal", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Issue_Risk", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsIssueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.PolSourceIssueRisk),
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Issue_Enum_ID_List", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsIssueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Issue_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsIssueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Issue_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsIssueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Pol_Source_Site_Obs_Issue_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportPolSourceSiteObsIssueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportRootTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Root_Fields", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Country", ReportTreeNodeTypeEnum.ReportCountryType, ReportTreeNodeSubTypeEnum.TableSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("MPN_Lookup", ReportTreeNodeTypeEnum.ReportMPNLookupType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Root_File", ReportTreeNodeTypeEnum.ReportRootFileType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Root_Error", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Root_Counter", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Root_ID", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Root_Name_Translation_Status", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TranslationStatus),
                    CreateReportTreeNodeItem("Root_Name", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Root_Is_Active", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Root_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Root_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Root_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Root_Lat", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Root_Lng", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Root_Stat_Country_Count", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Root_Stat_Province_Count", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Root_Stat_Area_Count", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Root_Stat_Sector_Count", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Root_Stat_Subsector_Count", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Root_Stat_Municipality_Count", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Root_Stat_Lift_Station_Count", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Root_Stat_WWTP_Count", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Root_Stat_MWQM_Sample_Count", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Root_Stat_MWQM_Site_Count", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Root_Stat_MWQM_Run_Count", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Root_Stat_Pol_Source_Site_Count", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Root_Stat_Mike_Scenario_Count", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Root_Stat_Box_Model_Scenario_Count", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Root_Stat_Visual_Plumes_Scenario_Count", ReportTreeNodeTypeEnum.ReportRootType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                };
            }
        }
        public List<ReportTreeNode> CreateReportRootFileTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Root_File_Fields", ReportTreeNodeTypeEnum.ReportRootFileType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Root_File_Error", ReportTreeNodeTypeEnum.ReportRootFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Root_File_Counter", ReportTreeNodeTypeEnum.ReportRootFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Root_File_ID", ReportTreeNodeTypeEnum.ReportRootFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Root_File_Language", ReportTreeNodeTypeEnum.ReportRootFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Language),
                    CreateReportTreeNodeItem("Root_File_Purpose", ReportTreeNodeTypeEnum.ReportRootFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.FilePurpose),
                    CreateReportTreeNodeItem("Root_File_Type", ReportTreeNodeTypeEnum.ReportRootFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.FileType),
                    CreateReportTreeNodeItem("Root_File_Description", ReportTreeNodeTypeEnum.ReportRootFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Root_File_Size_kb", ReportTreeNodeTypeEnum.ReportRootFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Root_File_Info", ReportTreeNodeTypeEnum.ReportRootFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Root_File_Created_Date_UTC", ReportTreeNodeTypeEnum.ReportRootFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Root_File_From_Water", ReportTreeNodeTypeEnum.ReportRootFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Root_File_Server_File_Name", ReportTreeNodeTypeEnum.ReportRootFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Root_File_Server_File_Path", ReportTreeNodeTypeEnum.ReportRootFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Root_File_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportRootFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Root_File_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportRootFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Root_File_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportRootFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportSectorTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Sector_Fields", ReportTreeNodeTypeEnum.ReportSectorType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Subsector", ReportTreeNodeTypeEnum.ReportSubsectorType, ReportTreeNodeSubTypeEnum.TableSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Sector_File", ReportTreeNodeTypeEnum.ReportSectorFileType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Sector_Error", ReportTreeNodeTypeEnum.ReportSectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sector_Counter", ReportTreeNodeTypeEnum.ReportSectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sector_ID", ReportTreeNodeTypeEnum.ReportSectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sector_Name_Translation_Status", ReportTreeNodeTypeEnum.ReportSectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TranslationStatus),
                    CreateReportTreeNodeItem("Sector_Name_Short", ReportTreeNodeTypeEnum.ReportSectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sector_Name_Long", ReportTreeNodeTypeEnum.ReportSectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sector_Is_Active", ReportTreeNodeTypeEnum.ReportSectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Sector_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportSectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Sector_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportSectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sector_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportSectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sector_Lat", ReportTreeNodeTypeEnum.ReportSectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Sector_Lng", ReportTreeNodeTypeEnum.ReportSectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Sector_Stat_Subsector_Count", ReportTreeNodeTypeEnum.ReportSectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sector_Stat_Municipality_Count", ReportTreeNodeTypeEnum.ReportSectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sector_Stat_Lift_Station_Count", ReportTreeNodeTypeEnum.ReportSectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sector_Stat_WWTP_Count", ReportTreeNodeTypeEnum.ReportSectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sector_Stat_MWQM_Sample_Count", ReportTreeNodeTypeEnum.ReportSectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sector_Stat_MWQM_Site_Count", ReportTreeNodeTypeEnum.ReportSectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sector_Stat_MWQM_Run_Count", ReportTreeNodeTypeEnum.ReportSectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sector_Stat_Pol_Source_Site_Count", ReportTreeNodeTypeEnum.ReportSectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sector_Stat_Mike_Scenario_Count", ReportTreeNodeTypeEnum.ReportSectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sector_Stat_Box_Model_Scenario_Count", ReportTreeNodeTypeEnum.ReportSectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sector_Stat_Visual_Plumes_Scenario_Count", ReportTreeNodeTypeEnum.ReportSectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                };
            }
        }
        public List<ReportTreeNode> CreateReportSubsectorTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Subsector_Fields", ReportTreeNodeTypeEnum.ReportSubsectorType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("MWQM_Site", ReportTreeNodeTypeEnum.ReportMWQMSiteType, ReportTreeNodeSubTypeEnum.TableSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("MWQM_Run", ReportTreeNodeTypeEnum.ReportMWQMRunType, ReportTreeNodeSubTypeEnum.TableSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Pol_Source_Site", ReportTreeNodeTypeEnum.ReportPolSourceSiteType, ReportTreeNodeSubTypeEnum.TableSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Municipality", ReportTreeNodeTypeEnum.ReportMunicipalityType, ReportTreeNodeSubTypeEnum.TableSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Subsector_Climate_Site", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Subsector_Special_Table", ReportTreeNodeTypeEnum.ReportSubsectorSpecialTableType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Subsector_Tide_Site", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Subsector_File", ReportTreeNodeTypeEnum.ReportSubsectorFileType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Subsector_Error", ReportTreeNodeTypeEnum.ReportSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Counter", ReportTreeNodeTypeEnum.ReportSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_ID", ReportTreeNodeTypeEnum.ReportSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Name_Translation_Status", ReportTreeNodeTypeEnum.ReportSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TranslationStatus),
                    CreateReportTreeNodeItem("Subsector_Name_Short", ReportTreeNodeTypeEnum.ReportSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Name_Long", ReportTreeNodeTypeEnum.ReportSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Is_Active", ReportTreeNodeTypeEnum.ReportSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Subsector_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lat", ReportTreeNodeTypeEnum.ReportSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Lng", ReportTreeNodeTypeEnum.ReportSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Stat_Municipality_Count", ReportTreeNodeTypeEnum.ReportSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Stat_Lift_Station_Count", ReportTreeNodeTypeEnum.ReportSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Stat_WWTP_Count", ReportTreeNodeTypeEnum.ReportSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Stat_MWQM_Sample_Count", ReportTreeNodeTypeEnum.ReportSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Stat_MWQM_Site_Count", ReportTreeNodeTypeEnum.ReportSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Stat_MWQM_Run_Count", ReportTreeNodeTypeEnum.ReportSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Stat_Pol_Source_Site_Count", ReportTreeNodeTypeEnum.ReportSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Stat_Mike_Scenario_Count", ReportTreeNodeTypeEnum.ReportSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Stat_Box_Model_Scenario_Count", ReportTreeNodeTypeEnum.ReportSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Stat_Visual_Plumes_Scenario_Count", ReportTreeNodeTypeEnum.ReportSubsectorType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                };
            }
        }
        public List<ReportTreeNode> CreateReportSubsectorSpecialTableTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Subsector_Special_Table_Fields", ReportTreeNodeTypeEnum.ReportSubsectorSpecialTableType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Subsector_Special_Table_Error", ReportTreeNodeTypeEnum.ReportSubsectorSpecialTableType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Special_Table_Last_X_Runs", ReportTreeNodeTypeEnum.ReportSubsectorSpecialTableType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Special_Table_Type", ReportTreeNodeTypeEnum.ReportSubsectorSpecialTableType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.SpecialTableType),
                    CreateReportTreeNodeItem("Subsector_Special_Table_MWQM_Site_Is_Active", ReportTreeNodeTypeEnum.ReportSubsectorSpecialTableType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Subsector_Special_Table_Number_Of_Samples_For_Stat_Max", ReportTreeNodeTypeEnum.ReportSubsectorSpecialTableType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Special_Table_Number_Of_Samples_For_Stat_Min", ReportTreeNodeTypeEnum.ReportSubsectorSpecialTableType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Special_Table_Highlight_Above_Min_Number", ReportTreeNodeTypeEnum.ReportSubsectorSpecialTableType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Special_Table_Highlight_Below_Max_Number", ReportTreeNodeTypeEnum.ReportSubsectorSpecialTableType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Special_Table_Show_Number_Of_Days_Of_Precipitation", ReportTreeNodeTypeEnum.ReportSubsectorSpecialTableType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Special_Table_Max_Number_Of_Sites_Per_Table_Part", ReportTreeNodeTypeEnum.ReportSubsectorSpecialTableType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Special_Table_MWQM_Site_Name_List", ReportTreeNodeTypeEnum.ReportSubsectorSpecialTableType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text), // bar separated values
                    CreateReportTreeNodeItem("Subsector_Special_Table_Stat_Letter_List", ReportTreeNodeTypeEnum.ReportSubsectorSpecialTableType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text), // bar separated values
                    CreateReportTreeNodeItem("Subsector_Special_Table_MWQM_Run_Date_List", ReportTreeNodeTypeEnum.ReportSubsectorSpecialTableType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text), // bar separated values
                    CreateReportTreeNodeItem("Subsector_Special_Table_Parameter_Value_List", ReportTreeNodeTypeEnum.ReportSubsectorSpecialTableType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text), // bar separated values
                    CreateReportTreeNodeItem("Subsector_Special_Table_Tide_Value_List", ReportTreeNodeTypeEnum.ReportSubsectorSpecialTableType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text), // bar separated values
                    CreateReportTreeNodeItem("Subsector_Special_Table_Rain_Day_0_Value_List", ReportTreeNodeTypeEnum.ReportSubsectorSpecialTableType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text), // bar separated values
                    CreateReportTreeNodeItem("Subsector_Special_Table_Rain_Day_1_Value_List", ReportTreeNodeTypeEnum.ReportSubsectorSpecialTableType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text), // bar separated values
                    CreateReportTreeNodeItem("Subsector_Special_Table_Rain_Day_2_Value_List", ReportTreeNodeTypeEnum.ReportSubsectorSpecialTableType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text), // bar separated values
                    CreateReportTreeNodeItem("Subsector_Special_Table_Rain_Day_3_Value_List", ReportTreeNodeTypeEnum.ReportSubsectorSpecialTableType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text), // bar separated values
                    CreateReportTreeNodeItem("Subsector_Special_Table_Rain_Day_4_Value_List", ReportTreeNodeTypeEnum.ReportSubsectorSpecialTableType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text), // bar separated values
                    CreateReportTreeNodeItem("Subsector_Special_Table_Rain_Day_5_Value_List", ReportTreeNodeTypeEnum.ReportSubsectorSpecialTableType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text), // bar separated values
                    CreateReportTreeNodeItem("Subsector_Special_Table_Rain_Day_6_Value_List", ReportTreeNodeTypeEnum.ReportSubsectorSpecialTableType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text), // bar separated values
                    CreateReportTreeNodeItem("Subsector_Special_Table_Rain_Day_7_Value_List", ReportTreeNodeTypeEnum.ReportSubsectorSpecialTableType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text), // bar separated values
                    CreateReportTreeNodeItem("Subsector_Special_Table_Rain_Day_8_Value_List", ReportTreeNodeTypeEnum.ReportSubsectorSpecialTableType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text), // bar separated values
                    CreateReportTreeNodeItem("Subsector_Special_Table_Rain_Day_9_Value_List", ReportTreeNodeTypeEnum.ReportSubsectorSpecialTableType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text), // bar separated values
                    CreateReportTreeNodeItem("Subsector_Special_Table_Rain_Day_10_Value_List", ReportTreeNodeTypeEnum.ReportSubsectorSpecialTableType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text), // bar separated values
                };
            }
        }
        public List<ReportTreeNode> CreateReportSubsectorClimateSiteTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Fields", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Data", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteDataType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Error", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Counter", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_ID", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_ECDBID", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Name", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Province", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Elevation_m", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Climate_ID", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_WMOID", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_TCID", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Is_Provincial", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Prov_Site_ID", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Time_Offset_hour", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_File_desc", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Hourly_Start_Date_Local", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Hourly_End_Date_Local", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Hourly_Now", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Daily_Start_Date_Local", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Daily_End_Date_Local", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Daily_Now", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Monthly_Start_Date_Local", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Monthly_End_Date_Local", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Monthly_Now", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Last_Update_Date_UTC", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Lat", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Lng", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                };
            }
        }
        public List<ReportTreeNode> CreateReportSubsectorClimateSiteDataTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Data_Fields", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteDataType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Data_Error", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Data_Counter", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Data_ID", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Data_Date_Time_Local", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Data_Keep", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Data_Storage_Data_Type", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.StorageDataType),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Data_Snow_cm", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Data_Rainfall_mm", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Data_Rainfall_Entered_mm", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Data_Total_Precip_mm_cm", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Data_Max_Temp_C", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Data_Min_Temp_C", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Data_Heat_Deg_Days_C", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Data_Cool_Deg_Days_C", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Data_Snow_On_Ground_cm", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Data_Dir_Max_Gust_0North", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Data_Spd_Max_Gust_kmh", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Data_Hourly_Values", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Data_Last_Update_Date_UTC", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Data_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Climate_Site_Data_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportSubsectorClimateSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportSectorFileTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Sector_File_Fields", ReportTreeNodeTypeEnum.ReportSectorFileType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Sector_File_Error", ReportTreeNodeTypeEnum.ReportSectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sector_File_Counter", ReportTreeNodeTypeEnum.ReportSectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sector_File_ID", ReportTreeNodeTypeEnum.ReportSectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sector_File_Language", ReportTreeNodeTypeEnum.ReportSectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Language),
                    CreateReportTreeNodeItem("Sector_File_Purpose", ReportTreeNodeTypeEnum.ReportSectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.FilePurpose),
                    CreateReportTreeNodeItem("Sector_File_Type", ReportTreeNodeTypeEnum.ReportSectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.FileType),
                    CreateReportTreeNodeItem("Sector_File_Description", ReportTreeNodeTypeEnum.ReportSectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sector_File_Size_kb", ReportTreeNodeTypeEnum.ReportSectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Sector_File_Info", ReportTreeNodeTypeEnum.ReportSectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sector_File_Created_Date_UTC", ReportTreeNodeTypeEnum.ReportSectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Sector_File_From_Water", ReportTreeNodeTypeEnum.ReportSectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Sector_File_Server_File_Name", ReportTreeNodeTypeEnum.ReportSectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sector_File_Server_File_Path", ReportTreeNodeTypeEnum.ReportSectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sector_File_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportSectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Sector_File_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportSectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Sector_File_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportSectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportSubsectorFileTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Subsector_File_Fields", ReportTreeNodeTypeEnum.ReportSubsectorFileType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Subsector_File_Error", ReportTreeNodeTypeEnum.ReportSubsectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_File_Counter", ReportTreeNodeTypeEnum.ReportSubsectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_File_ID", ReportTreeNodeTypeEnum.ReportSubsectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_File_Language", ReportTreeNodeTypeEnum.ReportSubsectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Language),
                    CreateReportTreeNodeItem("Subsector_File_Purpose", ReportTreeNodeTypeEnum.ReportSubsectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.FilePurpose),
                    CreateReportTreeNodeItem("Subsector_File_Type", ReportTreeNodeTypeEnum.ReportSubsectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.FileType),
                    CreateReportTreeNodeItem("Subsector_File_Description", ReportTreeNodeTypeEnum.ReportSubsectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_File_Size_kb", ReportTreeNodeTypeEnum.ReportSubsectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_File_Info", ReportTreeNodeTypeEnum.ReportSubsectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_File_Created_Date_UTC", ReportTreeNodeTypeEnum.ReportSubsectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_File_From_Water", ReportTreeNodeTypeEnum.ReportSubsectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Subsector_File_Server_File_Name", ReportTreeNodeTypeEnum.ReportSubsectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_File_Server_File_Path", ReportTreeNodeTypeEnum.ReportSubsectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_File_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportSubsectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_File_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportSubsectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_File_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportSubsectorFileType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportSubsectorHydrometricSiteTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Fields", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Data", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteDataType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Rating_Curve", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteRatingCurveType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Error", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Counter", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_ID", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Fed_Site_Number", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Quebec_Site_Number", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Name", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Description", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Province", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Elevation_m", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Start_Date_Local", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_End_Date_Local", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Time_Offset_hour", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Drainage_Area_km2", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Is_Natural", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Is_Active", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Sediment", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_RHBN", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Real_Time", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Has_Rating_Curve", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Last_Update_Date_UTC", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Lat", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Lng", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                };
            }
        }
        public List<ReportTreeNode> CreateReportSubsectorHydrometricSiteDataTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Data_Fields", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteDataType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Data_Error", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Data_Counter", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Data_ID", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Data_Date_Time_Local", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Data_Keep", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Data_Storage_Data_Type", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.StorageDataType),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Data_Flow_m3_s", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Data_Hourly_Values", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Data_Last_Update_Date_UTC", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Data_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Data_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportSubsectorHydrometricSiteRatingCurveTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Rating_Curve_Fields", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteRatingCurveType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Rating_Curve_Value", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteRatingCurveValueType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Rating_Curve_Error", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteRatingCurveType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Rating_Curve_Counter", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteRatingCurveType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Rating_Curve_ID", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteRatingCurveType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Rating_Curve_Rating_Curve_Number", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteRatingCurveType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Rating_Curve_Last_Update_Date_UTC", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteRatingCurveType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Rating_Curve_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteRatingCurveType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Rating_Curve_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteRatingCurveType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportSubsectorHydrometricSiteRatingCurveValueTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Rating_Curve_Value_Fields", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteRatingCurveValueType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Rating_Curve_Value_Error", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteRatingCurveValueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Rating_Curve_Value_Counter", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteRatingCurveValueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Rating_Curve_Value_ID", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteRatingCurveValueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Rating_Curve_Value_Stage_Value_m", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteRatingCurveValueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Rating_Curve_Value_Discharge_Value_m3_s", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteRatingCurveValueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Rating_Curve_Value_Last_Update_Date_UTC", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteRatingCurveValueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Rating_Curve_Value_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteRatingCurveValueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Hydrometric_Site_Rating_Curve_Value_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportSubsectorHydrometricSiteRatingCurveValueType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportSubsectorLabSheetTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Fields", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Error", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Counter", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_ID", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Other_Server_Lab_Sheet_ID", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Sampling_Plan_Name", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Province", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_For_Group_Name", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Year", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Month", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Day", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Access_Code", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Daily_Duplicate_Precision_Criteria", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Intertech_Duplicate_Precision_Criteria", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Subsector_Name_Short", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Subsector_Name_Long", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Sampling_Plan_Type", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.SamplingPlanType),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Sample_Type", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.SampleType),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Type", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.LabSheetType),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Status", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.LabSheetStatus),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_File_Name", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_File_Last_Modified_Date_Local", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_File_Content", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Accepted_Or_Rejected_By_Contact_Name", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Accepted_Or_Rejected_By_Contact_Initial", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Accepted_Or_Rejected_Date_Time", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Last_Update_Date_UTC", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportSubsectorLabSheetDetailTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Fields", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Tube_And_MPN_Detail", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Error", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Counter", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_ID", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Version", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Run_Date", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Tides", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Sample_Crew_Initials", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Incubation_Bath1_Start_Time", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Incubation_Bath2_Start_Time", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Incubation_Bath3_Start_Time", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Incubation_Bath1_End_Time", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Incubation_Bath2_End_Time", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Incubation_Bath3_End_Time", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Incubation_Bath1_Time_Calculated_minutes", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Incubation_Bath2_Time_Calculated_minutes", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Incubation_Bath3_Time_Calculated_minutes", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Water_Bath1", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Water_Bath2", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Water_Bath3", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_TC_Field_1", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_TC_Lab_1", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_TC_Field_2", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_TC_Lab_2", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_TC_First", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_TC_Average", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Control_Lot", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Positive_35", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Non_Target_35", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Negative_35", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Bath1_Positive_44_5", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Bath2_Positive_44_5", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Bath3_Positive_44_5", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Bath1_Non_Target_44_5", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Bath2_Non_Target_44_5", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Bath3_Non_Target_44_5", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Bath1_Negative_44_5", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Bath2_Negative_44_5", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Bath3_Negative_44_5", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Blank_35", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Bath1_Blank_44_5", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Bath2_Blank_44_5", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Bath3_Blank_44_5", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Lot_35", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Lot_44_5", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Run_Comment", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Run_Weather_Comment", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Sample_Bottle_Lot_Number", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Salinities_Read_By", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Salinities_Read_Date", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Results_Read_By", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Results_Read_Date", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Results_Recorded_By", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Results_Recorded_Date", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Daily_Duplicate_R_Log", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Daily_Duplicate_Precision_Criteria", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Daily_Duplicate_Acceptable", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Intertech_Duplicate_R_Log", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Intertech_Duplicate_Precision_Criteria", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Intertech_Duplicate_Acceptable", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Intertech_Read_Acceptable", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Last_Update_Date_UTC", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Detail_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportSubsectorLabSheetTubeMPNDetailTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Tube_And_MPN_Detail_Fields", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Tube_And_MPN_Detail_Error", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Tube_And_MPN_Detail_Counter", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Tube_And_MPN_Detail_ID", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Tube_And_MPN_Detail_Ordinal", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Tube_And_MPN_Detail_MWQM_Site", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Tube_And_MPN_Detail_Sample_Date_Time", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Tube_And_MPN_Detail_MPN", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Tube_And_MPN_Detail_Tube_10", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Tube_And_MPN_Detail_Tube_1_0", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Tube_And_MPN_Detail_Tube_0_1", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Tube_And_MPN_Detail_Salinity", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Tube_And_MPN_Detail_Temperature", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Tube_And_MPN_Detail_Processed_By", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Tube_And_MPN_Detail_Sample_Type", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.SampleType),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Tube_And_MPN_Detail_Site_Comment", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Tube_And_MPN_Detail_Last_Update_Date_UTC", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Tube_And_MPN_Detail_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Lab_Sheet_Tube_And_MPN_Detail_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportSubsectorLabSheetTubeMPNDetailType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportSubsectorTideSiteTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Subsector_Tide_Site_Fields", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Subsector_Tide_Site_Data", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteDataType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Subsector_Tide_Site_Error", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Tide_Site_Counter", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Tide_Site_ID", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Tide_Site_Name_Translation_Status", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TranslationStatus),
                    CreateReportTreeNodeItem("Subsector_Tide_Site_Name", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Tide_Site_Is_Active", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Subsector_Tide_Site_Web_Tide_Model", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Tide_Site_Web_Tide_Datum_m", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Tide_Site_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Tide_Site_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Tide_Site_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Tide_Site_Lat", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Tide_Site_Lng", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                };
            }
        }
        public List<ReportTreeNode> CreateReportSubsectorTideSiteDataTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Subsector_Tide_Site_Data_Fields", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteDataType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Subsector_Tide_Site_Data_Error", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Tide_Site_Data_Counter", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Tide_Site_Data_ID", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Subsector_Tide_Site_Data_Date_Time_Local", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Tide_Site_Data_Keep", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Subsector_Tide_Site_Data_Tide_Data_Type", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TideDataType),
                    CreateReportTreeNodeItem("Subsector_Tide_Site_Data_Storage_Data_Type", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.StorageDataType),
                    CreateReportTreeNodeItem("Subsector_Tide_Site_Data_Depth_m", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Tide_Site_Data_U_Velocity_m_s", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Tide_Site_Data_V_Velocity_m_s", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Subsector_Tide_Site_Data_Tide_Start", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TideText),
                    CreateReportTreeNodeItem("Subsector_Tide_Site_Data_Tide_End", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TideText),
                    CreateReportTreeNodeItem("Subsector_Tide_Site_Data_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Subsector_Tide_Site_Data_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Subsector_Tide_Site_Data_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportSubsectorTideSiteDataType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportVisualPlumesScenarioTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Fields", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Ambient", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioAmbientType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Result", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioResultType, ReportTreeNodeSubTypeEnum.TableNotSelectable, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Error", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Counter", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_ID", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Name_Translation_Status", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TranslationStatus),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Name", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Status", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.ScenarioStatus),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Use_As_Best_Estimate", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.TrueOrFalse),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Effluent_Flow_m3_s", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Effluent_Concentration_MPN_100ml", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Froude_Number", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Port_Diameter_m", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Port_Depth_m", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Port_Elevation_m", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Vertical_Angle_deg", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Horizontal_Angle_deg", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Number_Of_Ports", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Port_Spacing_m", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Acute_Mix_Zone_m", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Chronic_Mix_Zone_m", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Effluent_Salinity_PSU", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Effluent_Temperature_C", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Effluent_Velocity_m_s", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Raw_Results", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Last_Update_Date_UTC", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportVisualPlumesScenarioAmbientTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Ambient_Fields", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioAmbientType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Ambient_Error", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioAmbientType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Ambient_Counter", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioAmbientType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Ambient_ID", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioAmbientType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Ambient_Row", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioAmbientType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Ambient_Measurement_Depth_m", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioAmbientType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Ambient_Current_Speed_m_s", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioAmbientType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Ambient_Current_Direction_deg", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioAmbientType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Ambient_Ambient_Salinity_PSU", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioAmbientType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Ambient_Ambient_Temperature_C", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioAmbientType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Ambient_Background_Concentration_MPN_100ml", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioAmbientType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Ambient_Pollutant_Decay_Rate_per_day", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioAmbientType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Ambient_Far_Field_Current_Speed_m_s", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioAmbientType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Ambient_Far_Field_Current_Direction_deg", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioAmbientType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Ambient_Far_Field_Diffusion_Coefficient", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioAmbientType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Ambient_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioAmbientType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Ambient_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioAmbientType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Ambient_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioAmbientType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }
        public List<ReportTreeNode> CreateReportVisualPlumesScenarioResultTypeTreeNodeItem(ReportTreeNode reportTreeNode)
        {
            if (reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableSelectable || reportTreeNode.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.TableNotSelectable)
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Result_Fields", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioResultType, ReportTreeNodeSubTypeEnum.FieldsHolder, ReportFieldTypeEnum.Error),
                };
            }
            else
            {
                return new List<ReportTreeNode>()
                {
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Result_Error", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Result_Counter", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Result_ID", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Result_Ordinal", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Result_Concentration_MPN_100ml", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWhole),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Result_Dilution", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Result_Far_Field_Width_m", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Result_Dispersion_Distance_m", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Result_Travel_Time_hour", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.NumberWithDecimal),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Result_Last_Update_Date_And_Time_UTC", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.DateAndTime),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Result_Last_Update_Contact_Name", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                    CreateReportTreeNodeItem("Visual_Plumes_Scenario_Result_Last_Update_Contact_Initial", ReportTreeNodeTypeEnum.ReportVisualPlumesScenarioResultType, ReportTreeNodeSubTypeEnum.Field, ReportFieldTypeEnum.Text),
                };
            }
        }

        #endregion Functions public
    }
}
