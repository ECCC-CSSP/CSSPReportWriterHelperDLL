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

namespace CSSPReportWriterHelperDLL.Services
{
    public partial class ReportBaseService
    {
        public string GenerateModel(ReportTreeNode reportTreeNode, StringBuilder sb)
        {
            foreach (ReportTreeNode RTN in reportTreeNode.Nodes)
            {
                if (RTN.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.FieldsHolder)
                {
                    sb.AppendLine(@"    public class Report" + reportTreeNode.Text + "Model");
                    sb.AppendLine(@"    {");
                    sb.AppendLine(@"        public Report" + reportTreeNode.Text + "Model()");
                    sb.AppendLine(@"        {");
                    sb.AppendLine(@"            " + reportTreeNode.Text + @"_Error = """";");
                    sb.AppendLine(@"        }");
                    sb.AppendLine(@"    ");
                    foreach (ReportTreeNode RTNField in RTN.Nodes)
                    {
                        switch (RTNField.ReportFieldType)
                        {
                            case ReportFieldTypeEnum.DateAndTime:
                                sb.AppendLine(@"        public Nullable<DateTime> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.NumberWhole:
                                sb.AppendLine(@"        public Nullable<int> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.NumberWithDecimal:
                                sb.AppendLine(@"        public Nullable<float> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.Text:
                                sb.AppendLine(@"        public string " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.TrueOrFalse:
                                sb.AppendLine(@"        public Nullable<bool> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.FilePurpose:
                                sb.AppendLine(@"        public Nullable<FilePurposeEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.FileType:
                                sb.AppendLine(@"        public Nullable<FileTypeEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.TranslationStatus:
                                sb.AppendLine(@"        public Nullable<TranslationStatusEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.BoxModelResultType:
                                sb.AppendLine(@"        public Nullable<BoxModelResultTypeEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.InfrastructureType:
                                sb.AppendLine(@"        public Nullable<InfrastructureTypeEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.FacilityType:
                                sb.AppendLine(@"        public Nullable<FacilityTypeEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.AerationType:
                                sb.AppendLine(@"        public Nullable<AerationTypeEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.PreliminaryTreatmentType:
                                sb.AppendLine(@"        public Nullable<PreliminaryTreatmentTypeEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.PrimaryTreatmentType:
                                sb.AppendLine(@"        public Nullable<PrimaryTreatmentTypeEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.SecondaryTreatmentType:
                                sb.AppendLine(@"        public Nullable<SecondaryTreatmentTypeEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.TertiaryTreatmentType:
                                sb.AppendLine(@"        public Nullable<TertiaryTreatmentTypeEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.TreatmentType:
                                sb.AppendLine(@"        public Nullable<TreatmentTypeEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.DisinfectionType:
                                sb.AppendLine(@"        public Nullable<DisinfectionTypeEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.CollectionSystemType:
                                sb.AppendLine(@"        public Nullable<CollectionSystemTypeEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.AlarmSystemType:
                                sb.AppendLine(@"        public Nullable<AlarmSystemTypeEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.ScenarioStatus:
                                sb.AppendLine(@"        public Nullable<ScenarioStatusEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.StorageDataType:
                                sb.AppendLine(@"        public Nullable<StorageDataTypeEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.Language:
                                sb.AppendLine(@"        public Nullable<LanguageEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.SampleType:
                                sb.AppendLine(@"        public string " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.BeaufortScale:
                                sb.AppendLine(@"        public Nullable<BeaufortScaleEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.AnalyzeMethod:
                                sb.AppendLine(@"        public Nullable<AnalyzeMethodEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.SampleMatrix:
                                sb.AppendLine(@"        public Nullable<SampleMatrixEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.Laboratory:
                                sb.AppendLine(@"        public Nullable<LaboratoryEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.SampleStatus:
                                sb.AppendLine(@"        public Nullable<SampleStatusEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.SamplingPlanType:
                                sb.AppendLine(@"        public Nullable<SamplingPlanTypeEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.LabSheetType:
                                sb.AppendLine(@"        public Nullable<LabSheetTypeEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.LabSheetStatus:
                                sb.AppendLine(@"        public Nullable<LabSheetStatusEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.PolSourceInactiveReason:
                                sb.AppendLine(@"        public Nullable<PolSourceInactiveReasonEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.PolSourceObsInfo:
                                sb.AppendLine(@"        public string " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.AddressType:
                                sb.AppendLine(@"        public Nullable<AddressTypeEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.StreetType:
                                sb.AppendLine(@"        public Nullable<StreetTypeEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.ContactTitle:
                                sb.AppendLine(@"        public Nullable<ContactTitleEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.EmailType:
                                sb.AppendLine(@"        public Nullable<EmailTypeEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.TelType:
                                sb.AppendLine(@"        public Nullable<TelTypeEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.TideText:
                                sb.AppendLine(@"        public Nullable<TideTextEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.TideDataType:
                                sb.AppendLine(@"        public Nullable<TideDataTypeEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.SpecialTableType:
                                sb.AppendLine(@"        public Nullable<SpecialTableTypeEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.MWQMSiteLatestClassification:
                                sb.AppendLine(@"        public Nullable<MWQMSiteLatestClassificationEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.PolSourceIssueRisk:
                                sb.AppendLine(@"        public Nullable<PolSourceIssueRiskEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            case ReportFieldTypeEnum.MikeScenarioSpecialResultKMLType:
                                sb.AppendLine(@"        public Nullable<MikeScenarioSpecialResultKMLTypeEnum> " + RTNField.Text + " { get; set; }");
                                break;
                            default:
                                sb.AppendLine(@"        public eeeeee " + RTNField.Text + " { get; set; }");
                                break;
                        }
                    }
                    sb.AppendLine(@"    }");
                }
            }

            return "";
        }
    }
}
