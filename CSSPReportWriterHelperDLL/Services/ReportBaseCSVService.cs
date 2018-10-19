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
using CSSPDBDLL.Services;

namespace CSSPReportWriterHelperDLL.Services
{
    public partial class ReportBaseService
    {
        #region Functions public
        public string CheckNumberOfVariableOnFirstLineIsEqualToMaximumNumberOfVariablesCSV(StringBuilder sbFileText, List<ReportTag> reportTagList)
        {
            string FileText = sbFileText.ToString();
            string FirstLine = FileText.Substring(0, FileText.IndexOf("\r\n"));
            List<string> firstLineVariableList = FirstLine.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
            int VariableCount = firstLineVariableList.Count();

            int ResultCount = 0;
            for (int i = 0; i < 1000; i++)
            {
                if (!reportTagList.Where(c => c.Level == i).Any())
                    break;

                ResultCount += reportTagList.Where(c => c.Level == i).Select(c => c.ReportTreeNodeList.Count).Max();
            }

            if (VariableCount != ResultCount)
            {
                string ErrorText = "\r\n\r\n" + string.Format(ReportServiceRes.FirstLineVariableCount_DoesNotEqualToResultCount_, VariableCount, ResultCount) + "\r\n\r\n";
                sbFileText = sbFileText.Insert(0, ErrorText);
                return ErrorText;
            }

            return "";
        }
        public string CheckTagsAndContentOKCSV(StringBuilder sbFileText, List<ReportTag> reportTagList, int StartTVItemID, int Take)
        {
            string retStr = "";

            retStr = FirstLineMustNotHaveMarkersCSV(sbFileText);
            if (!string.IsNullOrWhiteSpace(retStr))
                return retStr;

            retStr = FindAllTagsCSV(sbFileText, reportTagList, StartTVItemID, Take);
            if (!string.IsNullOrWhiteSpace(retStr))
                return retStr;

            retStr = CheckTagsPositionOKCSV(sbFileText, reportTagList);
            if (!string.IsNullOrWhiteSpace(retStr))
                return retStr;

            retStr = CheckTagsFirstLineCSV(sbFileText, reportTagList);
            if (!string.IsNullOrWhiteSpace(retStr))
                return retStr;

            retStr = FillTagsVariablesCSV(sbFileText, reportTagList);
            if (!string.IsNullOrWhiteSpace(retStr))
                return retStr;

            retStr = FillTagsParentChildRelationshipCSV(sbFileText, reportTagList);
            if (!string.IsNullOrWhiteSpace(retStr))
                return retStr;

            foreach (ReportTag reportTag in reportTagList.Where(c => c.ReportTagParent == null))
            {
                retStr = FillTagsLevelsCSV(sbFileText, reportTag, 0);
                if (!string.IsNullOrWhiteSpace(retStr))
                    return retStr;
            }

            retStr = CheckNumberOfVariableOnFirstLineIsEqualToMaximumNumberOfVariablesCSV(sbFileText, reportTagList);
            if (!string.IsNullOrWhiteSpace(retStr))
                return retStr;

            return "";
        }
        public string CheckTagsFirstLineCSV(StringBuilder sbFileText, List<ReportTag> reportTagList)
        {
            int FirstLineCount = 3;
            string FileText = sbFileText.ToString();

            foreach (ReportTag reportTag in reportTagList)
            {
                string TagText = FileText.Substring(reportTag.StartRangeStartPos, reportTag.StartRangeEndPos - reportTag.StartRangeStartPos);
                if (TagText.IndexOf("\r") == -1)
                {
                    reportTag.Error = "\r\n\r\n" + ReportServiceRes.CouldNotFindFirstLineReturnsAreRequired + "\r\n\r\n";
                    sbFileText = sbFileText.Insert(reportTag.StartRangeStartPos, reportTag.Error);
                    return reportTag.Error;
                }

                string FirstLine = TagText.Substring(0, TagText.IndexOf("\r"));

                List<string> strList = FirstLine.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
                if (strList.Count != FirstLineCount)
                {
                    reportTag.Error = "\r\n\r\n" + string.Format(ReportServiceRes._DoesNotContain_Items, FirstLine, FirstLineCount.ToString()) + "\r\n\r\n" +
                        string.Format(ReportServiceRes.Example_, Marker + "Start Root en") + "\r\n\r\n";
                    sbFileText = sbFileText.Insert(reportTag.StartRangeStartPos, reportTag.Error);
                    return reportTag.Error;
                }

                reportTag.TagItem = strList[1];

                reportTag.ReportType = _ReportBase.GetReportType(reportTag.TagItem);
                if (reportTag.ReportType == null)
                {
                    reportTag.Error = "\r\n\r\n" + string.Format(ReportServiceRes.ItemName_NotAllowed, reportTag.TagItem) + "\r\n\r\n" +
                         string.Format(ReportServiceRes.AllowableValues_, _ReportBase.AllowableReportType()) + "\r\n\r\n";
                    sbFileText = sbFileText.Insert(reportTag.StartRangeStartPos, reportTag.Error);
                    return reportTag.Error;
                }

                reportTag.Language = strList[2];

                if (!(reportTag.Language == "en" || reportTag.Language == "fr"))
                {
                    reportTag.Error = "\r\n\r\n" + string.Format(ReportServiceRes.Language_NotAllowed, reportTag.Language) + "\r\n\r\n" +
                        string.Format(ReportServiceRes.AllowableLanguages_, "en, fr") + "\r\n\r\n";
                    sbFileText = sbFileText.Insert(reportTag.StartRangeStartPos, reportTag.Error);
                    return reportTag.Error;
                }
            }

            return "";
        }
        public string CheckTagsPositionOKCSV(StringBuilder sbFileText, List<ReportTag> reportTagList)
        {
            for (int i = 0, count = reportTagList.Count; i < count; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    if (reportTagList[i].RangeEndTagStartPos == reportTagList[j].RangeEndTagStartPos)
                    {
                        string ErrorText = "\r\n\r\n" + string.Format(ReportServiceRes.YouHaveATag_WhichIsNotClosedProperly, reportTagList[i].CSVTagText) + "\r\n\r\n";
                        sbFileText = sbFileText.Insert(reportTagList[i].StartRangeStartPos, ErrorText);
                        return ErrorText;
                    }
                }
            }

            foreach (ReportTag reportTagLoop in reportTagList.Where(c => c.TagName == "LoopStart"))
            {
                bool IsOutside = true;
                foreach (ReportTag reportTagStart in reportTagList.Where(c => c.TagName == "Start"))
                {
                    if (reportTagLoop.StartRangeStartPos >= reportTagStart.RangeInnerTagStartPos
                        && reportTagLoop.StartRangeEndPos <= reportTagStart.RangeInnerTagEndPos)
                    {
                        IsOutside = false;
                        continue;
                    }
                }

                if (IsOutside)
                {
                    string ErrorText = "\r\n\r\n" + ReportServiceRes.AllTagsMustBeWithinAStartTag + "\r\n\r\n";
                    sbFileText = sbFileText.Insert(reportTagLoop.StartRangeStartPos, ErrorText);
                    return ErrorText;
                }
            }

            return "";
        }
        public string FindAllTagsCSV(StringBuilder sbFileText, List<ReportTag> reportTagList, int StartTVItemID, int Take)
        {
            string tempTest = "";
            bool Found = true;
            string FileText = sbFileText.ToString();
            int MaxRange = 0;
            int RangeEndTagStartPos = 0;
            int RangeEndTagEndPos = 0;
            int RangeStartTagStartPos = -1;
            int RangeStartTagEndPos = 0;

            MaxRange = sbFileText.Length;

            foreach (string s in new List<string>() { "Start", "LoopStart" })
            {
                RangeStartTagEndPos = 0;
                Found = true;
                while (Found)
                {
                    RangeStartTagStartPos = FileText.IndexOf(Marker + s, RangeStartTagEndPos + 1);
                    if (RangeStartTagStartPos != -1)
                    {
                        if (FileText.Substring(RangeStartTagStartPos).StartsWith(Marker + "LoopEnd"))
                        {
                            RangeStartTagEndPos = RangeStartTagStartPos;
                            continue;
                        }

                        RangeStartTagEndPos = FileText.IndexOf(Marker, RangeStartTagStartPos + Marker.Length);
                        if (RangeStartTagEndPos < 0)
                        {
                            string ErrorText = "\r\n\r\n" + string.Format(ReportServiceRes.Tag_NotWellFormedItShouldHaveAStructureLike_, s, Marker + s + " Root en" + Marker) + "\r\n\r\n";
                            sbFileText = sbFileText.Insert(RangeStartTagStartPos, ErrorText);
                            return ErrorText;
                        }

                        RangeStartTagEndPos = RangeStartTagEndPos + Marker.Length;

                        if (MaxRange == RangeStartTagEndPos)
                        {
                            string ErrorText = "\r\n\r\n" + string.Format(ReportServiceRes._TagMustBeFollowedWithAReturn, Marker + s + "*" + Marker) + "\r\n\r\n";
                            sbFileText = sbFileText.Insert(RangeStartTagEndPos, ErrorText);
                            return ErrorText;
                        }

                        if (!FileText.Substring(RangeStartTagStartPos).StartsWith(Marker + s + " "))
                        {
                            string ErrorText = "\r\n\r\n" + string.Format(ReportServiceRes.Tag_NotWellFormedItShouldHaveAStructureLike_, s, Marker + s + " Root en" + Marker) + "\r\n\r\n";
                            sbFileText = sbFileText.Insert(RangeStartTagStartPos, ErrorText);
                            return ErrorText;
                        }

                        tempTest = FileText.Substring(RangeStartTagStartPos, RangeStartTagEndPos - RangeStartTagStartPos);

                        string TagItem = FileText.Substring(RangeStartTagStartPos + (Marker + s + " ").Length);
                        TagItem = TagItem.Substring(0, TagItem.IndexOf(" "));


                        ReportTag reportTag = new ReportTag();
                        reportTag.TagName = s;

                        if (reportTag.TagName == "Start")
                        {
                            reportTag.UnderTVItemID = StartTVItemID;
                        }

                        if (s == "Start")
                        {
                            RangeEndTagStartPos = FileText.IndexOf(Marker + "End " + TagItem + " ", RangeStartTagEndPos);
                        }
                        else // LoopStart tag name
                        {
                            RangeEndTagStartPos = FileText.IndexOf(Marker + "LoopEnd " + TagItem + " ", RangeStartTagEndPos);
                        }

                        if (RangeEndTagStartPos == -1)
                        {
                            string ErrorText = "\r\n\r\n" + string.Format(ReportServiceRes.CouldNotFindEndTag_Of_, Marker + (s == "Loop" ? "Loop" : "") + "End*" + Marker,
                                FileText.Substring(RangeStartTagStartPos, RangeStartTagEndPos - RangeStartTagStartPos)) + "\r\n\r\n";
                            sbFileText = sbFileText.Insert(RangeStartTagEndPos, ErrorText);
                            return ErrorText;
                        }

                        RangeEndTagEndPos = FileText.IndexOf(Marker, RangeEndTagStartPos + 1);
                        if (RangeEndTagEndPos < 0)
                        {
                            string ErrorText = "\r\n\r\n" + string.Format(ReportServiceRes.Tag_NotWellFormedItShouldHaveAStructureLike_, (s == "Loop" ? "Loop" : "") + "End", Marker + "End Root en" + Marker) + "\r\n\r\n";
                            sbFileText = sbFileText.Insert(RangeEndTagStartPos, ErrorText);
                            return ErrorText;
                        }

                        RangeEndTagEndPos = RangeEndTagEndPos + Marker.Length;

                        if (MaxRange == RangeEndTagEndPos)
                        {
                            string ErrorText = "\r\n\r\n" + string.Format(ReportServiceRes._TagMustBeFollowedWithAReturn, Marker + (s == "Loop" ? "Loop" : "") + "End*" + Marker) + "\r\n\r\n";
                            sbFileText = sbFileText.Insert(RangeEndTagStartPos, ErrorText);
                            return ErrorText;
                        }

                        tempTest = FileText.Substring(RangeEndTagStartPos, RangeEndTagEndPos - RangeEndTagStartPos);

                        reportTag.StartRangeStartPos = RangeStartTagStartPos;
                        reportTag.StartRangeEndPos = RangeEndTagEndPos;
                        reportTag.RangeStartTagStartPos = RangeStartTagStartPos;
                        reportTag.RangeStartTagEndPos = RangeStartTagEndPos;
                        reportTag.RangeEndTagStartPos = RangeEndTagStartPos;
                        reportTag.RangeEndTagEndPos = RangeEndTagEndPos;
                        reportTag.RangeInnerTagStartPos = RangeStartTagEndPos;
                        reportTag.RangeInnerTagEndPos = RangeEndTagStartPos;

                        reportTag.CSVTagText = FileText.Substring(reportTag.RangeInnerTagStartPos, reportTag.RangeInnerTagEndPos - reportTag.RangeInnerTagStartPos);

                        reportTag.Take = Take;

                        reportTagList.Add(reportTag);
                    }
                    else
                    {
                        Found = false;
                    }
                }
            }

            if (reportTagList.Count == 0)
            {
                string ErrorText = "\r\n\r\n" + ReportServiceRes.NoTagFoundInDocument + "\r\n\r\n";
                sbFileText = sbFileText.Insert(0, ErrorText);
                return ErrorText;
            }

            // First tag must be Start
            if (reportTagList[0].TagName != "Start")
            {
                string ErrorText = "\r\n\r\n" + ReportServiceRes.FirstTagInDocumentMustBeAStartTag + "\r\n\r\n";
                sbFileText = sbFileText.Insert(0, ErrorText);
                return ErrorText;
            }

            foreach (ReportTag reportTag in reportTagList)
            {
                reportTag.CSVTagText = FileText.Substring(reportTag.RangeStartTagStartPos, reportTag.RangeStartTagEndPos - reportTag.RangeStartTagStartPos);
                if (reportTag.CSVTagText.Substring(reportTag.CSVTagText.Length - 2) == "\r\n")
                {
                    reportTag.CSVTagText = reportTag.CSVTagText.Substring(0, reportTag.CSVTagText.Length - 2);
                }
                reportTag.DocumentType = DocumentType.CSV;
            }

            int StartTagCount = reportTagList.Where(c => c.TagName == "Start").Count();
            if (StartTagCount == 0)
            {
                string ErrorText = "\r\n\r\n" + ReportServiceRes.OneStartTagIsRequired + "\r\n\r\n";
                sbFileText = sbFileText.Insert(0, ErrorText);
                return ErrorText;
            }

            if (StartTagCount != 1)
            {
                string ErrorText = "\r\n\r\n" + ReportServiceRes.OnlyOneStartTagIsAllowedInCSVTemplate + "\r\n\r\n";
                sbFileText = sbFileText.Insert(0, ErrorText);
                return ErrorText;
            }


            return "";
        }
        public string FillTagsParentChildRelationshipCSV(StringBuilder sbFileText, List<ReportTag> reportTagList)
        {
            foreach (ReportTag reportTag in reportTagList)
            {
                // Getting Parent
                reportTag.ReportTagParent = (from c in reportTagList
                                             where c.Guid != reportTag.Guid
                                             && reportTag.RangeStartTagStartPos >= c.RangeStartTagEndPos
                                             && reportTag.RangeEndTagEndPos <= c.RangeEndTagStartPos
                                             orderby c.RangeStartTagEndPos descending
                                             select c).FirstOrDefault();


                var reList = (from c in reportTagList
                              where c.RangeStartTag != reportTag.RangeStartTag
                              && reportTag.RangeStartTagStartPos >= c.RangeStartTagEndPos
                              && reportTag.RangeEndTagEndPos <= c.RangeEndTagStartPos
                              orderby c.RangeStartTag.End descending
                              select c).ToList();
            }

            foreach (ReportTag reportTag in reportTagList)
            {
                // Getting Children
                reportTag.ReportTagChildList = (from c in reportTagList
                                                where c.Guid != reportTag.Guid
                                                && c.ReportTagParent == reportTag
                                                select c).ToList();

                if (reportTag.ReportTagChildList.Count > 1)
                {
                    string ErrorText = "\r\n\r\n" + string.Format(ReportServiceRes.Tag_HasMoreThan1ChildTagWhichIsNotAllowedInCSVTemplates, reportTag.TagName + reportTag.TagItem) + "\r\n\r\n";
                    sbFileText = sbFileText.Insert(reportTag.StartRangeStartPos, ErrorText);
                    return ErrorText;
                }
            }

            return "";
        }
        public string FillTagsLevelsCSV(StringBuilder sbFileText, ReportTag reportTag, int CurrentLevel)
        {
            reportTag.Level = CurrentLevel;

            foreach (ReportTag reportTagChild in reportTag.ReportTagChildList)
            {
                FillTagsLevelsCSV(sbFileText, reportTagChild, CurrentLevel + 1);
            }

            return "";
        }
        public string FillTagsVariablesCSV(StringBuilder sbFileText, List<ReportTag> reportTagList)
        {
            string retStr = "";
            foreach (ReportTag reportTag in reportTagList)
            {
                bool IsDBFiltering = true;
                retStr = GetReportTreeNodesFromTagText(reportTag.CSVTagText, reportTag.TagItem, reportTag.ReportType, reportTag.ReportTreeNodeList, IsDBFiltering);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    sbFileText.Insert(0, "\r\n\r\n" + retStr + "\r\n\r\n");
                    return retStr;
                }
            }

            return "";
        }
        public string FillTemplateWithDBInfoCSV(StringBuilder sbFileText, ReportTag reportTag)
        {
            StringBuilder sbNewFileText = new StringBuilder();
            string FileText = sbFileText.ToString();
            string FirstLine = FileText.Substring(0, FileText.IndexOf("\r\n"));

            sbNewFileText.AppendLine(FirstLine);

            string retStr = GenerateNextReportTagCSV("", sbNewFileText, reportTag);
            if (!string.IsNullOrWhiteSpace(retStr))
            {
                sbNewFileText.AppendLine("");
                sbNewFileText.AppendLine(retStr);
            }

            sbFileText.Clear();
            sbFileText.Append(sbNewFileText.ToString());

            return "";
        }
        public string FirstLineMustNotHaveMarkersCSV(StringBuilder sbFileText)
        {
            string FileText = sbFileText.ToString();
            int pos = FileText.IndexOf("\r\n");
            if (pos == -1)
            {
                string ErrorText = "\r\n\r\n" + string.Format(ReportServiceRes.CouldNotFindFirstLineReturnsAreRequired) + "\r\n\r\n";
                sbFileText = sbFileText.Insert(0, ErrorText);
                return ErrorText;
            }
            string FirstLine = FileText.Substring(0, pos);
            if (FirstLine.Contains(Marker))
            {
                string ErrorText = "\r\n\r\n" + string.Format(ReportServiceRes.FirstLineOfCSVTemplateShouldNotContain_, Marker) + "\r\n\r\n";
                sbFileText = sbFileText.Insert(0, ErrorText);
                return ErrorText;
            }

            return "";
        }
        public string GenerateForReportTagCSV<T>(string PreTextOfLine, StringBuilder sbNewFileText, ReportTag reportTag) where T : new()
        {
            ReportModelDynamic reportModelDynamic = new ReportModelDynamic();
            string retStr = ReportGetDBOfType(reportTag, reportModelDynamic);
            if (!string.IsNullOrWhiteSpace(retStr))
                return retStr;

            if (!string.IsNullOrWhiteSpace(reportTag.Error))
                return reportTag.Error;

            List<T> reportModelList = reportModelDynamic.ReportModel;


            foreach (T reportModel in reportModelList)
            {
                string PreTextOfLineAtLevel = PreTextOfLine;

                foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                {
                    if (propertyInfo.Name == reportTag.TagItem + "_Error")
                    {
                        string errStr = (string)propertyInfo.GetValue(reportModel);
                        if (!string.IsNullOrWhiteSpace(errStr))
                        {
                            PreTextOfLineAtLevel += "ERROR: " + errStr;
                            sbNewFileText.Append("ERROR: " + errStr);
                            return PreTextOfLineAtLevel;
                        }
                    }
                }

                for (int i = 0, count = reportTag.ReportTreeNodeList.Count(); i < count; i++)
                {
                    foreach (PropertyInfo propertyInfo in reportTag.ReportType.GetProperties())
                    {
                        if (propertyInfo.Name == reportTag.ReportTreeNodeList[i].Text)
                        {
                            if (propertyInfo.GetValue(reportModel) == null)
                            {
                                PreTextOfLineAtLevel += "empty" + ",";
                            }
                            else
                            {
                                PreTextOfLineAtLevel += ((string)ReportGetFieldTextOrValue<T>(reportModel, true, propertyInfo, "", reportTag, reportTag.ReportTreeNodeList[i])).Replace(",", " ").Replace("\t", " ").Replace("\r\n", " ") + ",";
                                if (!string.IsNullOrWhiteSpace(reportTag.Error))
                                    return reportTag.Error;
                                
                                //PreTextOfLineAtLevel += propertyInfo.GetValue(reportModel).ToString().Replace(",", " ").Replace("\t", " ").Replace("\r\n", " ") + ",";
                            }
                        }
                    }
                }

                if (reportTag.ReportTagChildList.Count > 0)
                {
                    foreach (ReportTag reportTagChild in reportTag.ReportTagChildList)
                    {
                        foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                        {
                            if (propertyInfo.Name == reportTag.TagItem + "_ID")
                            {
                                reportTagChild.UnderTVItemID = (int)propertyInfo.GetValue(reportModel);
                                break;
                            }
                        }

                        //reportTagChild.UnderTVItemID = reportModel.TVItemID;
                        retStr = GenerateNextReportTagCSV(PreTextOfLineAtLevel, sbNewFileText, reportTagChild);
                        if (!string.IsNullOrWhiteSpace(retStr))
                            return retStr;
                    }
                }
                else
                {
                    //PreTextOfLine = PreTextOfLineAtLevel;
                    sbNewFileText.AppendLine(PreTextOfLineAtLevel.Substring(0, PreTextOfLineAtLevel.Length - 1));
                }
            }

            return "";

        }
        public string GenerateNextReportTagCSV(string PreTextOfLine, StringBuilder sbNewFileText, ReportTag ReportTag)
        {
            string retStr = "";
            switch (ReportTag.TagItem)
            {
                case "Root":
                    retStr = GenerateForReportTagCSV<ReportRootModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Country":
                    retStr = GenerateForReportTagCSV<ReportCountryModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Province":
                    retStr = GenerateForReportTagCSV<ReportProvinceModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Area":
                    retStr = GenerateForReportTagCSV<ReportAreaModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Sector":
                    retStr = GenerateForReportTagCSV<ReportSectorModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Subsector":
                    retStr = GenerateForReportTagCSV<ReportSubsectorModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "MWQM_Site":
                    retStr = GenerateForReportTagCSV<ReportMWQM_SiteModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "MWQM_Site_Sample":
                    retStr = GenerateForReportTagCSV<ReportMWQM_Site_SampleModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "MWQM_Site_Start_And_End_Date":
                    retStr = GenerateForReportTagCSV<ReportMWQM_Site_Start_And_End_DateModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "MWQM_Site_File":
                    retStr = GenerateForReportTagCSV<ReportMWQM_Site_FileModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "MWQM_Run":
                    retStr = GenerateForReportTagCSV<ReportMWQM_RunModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "MWQM_Run_Sample":
                    retStr = GenerateForReportTagCSV<ReportMWQM_Run_SampleModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "MWQM_Run_Lab_Sheet":
                    retStr = GenerateForReportTagCSV<ReportMWQM_Run_Lab_SheetModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "MWQM_Run_Lab_Sheet_Detail":
                    retStr = GenerateForReportTagCSV<ReportMWQM_Run_Lab_Sheet_DetailModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "MWQM_Run_Lab_Sheet_Tube_And_MPN_Detail":
                    retStr = GenerateForReportTagCSV<ReportMWQM_Run_Lab_Sheet_Tube_And_MPN_DetailModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "MWQM_Run_File":
                    retStr = GenerateForReportTagCSV<ReportMWQM_Run_FileModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Pol_Source_Site":
                    retStr = GenerateForReportTagCSV<ReportPol_Source_SiteModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Pol_Source_Site_Obs":
                    retStr = GenerateForReportTagCSV<ReportPol_Source_Site_ObsModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Pol_Source_Site_Obs_Issue":
                    retStr = GenerateForReportTagCSV<ReportPol_Source_Site_Obs_IssueModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Pol_Source_Site_File":
                    retStr = GenerateForReportTagCSV<ReportPol_Source_Site_FileModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Pol_Source_Site_Address":
                    retStr = GenerateForReportTagCSV<ReportPol_Source_Site_AddressModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Municipality":
                    retStr = GenerateForReportTagCSV<ReportMunicipalityModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Infrastructure":
                    retStr = GenerateForReportTagCSV<ReportInfrastructureModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Box_Model":
                    retStr = GenerateForReportTagCSV<ReportBox_ModelModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Box_Model_Result":
                    retStr = GenerateForReportTagCSV<ReportBox_Model_ResultModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Visual_Plumes_Scenario":
                    retStr = GenerateForReportTagCSV<ReportVisual_Plumes_ScenarioModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Visual_Plumes_Scenario_Ambient":
                    retStr = GenerateForReportTagCSV<ReportVisual_Plumes_Scenario_AmbientModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Visual_Plumes_Scenario_Result":
                    retStr = GenerateForReportTagCSV<ReportVisual_Plumes_Scenario_ResultModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Infrastructure_Address":
                    retStr = GenerateForReportTagCSV<ReportInfrastructure_AddressModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Infrastructure_File":
                    retStr = GenerateForReportTagCSV<ReportInfrastructure_FileModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Mike_Scenario":
                    retStr = GenerateForReportTagCSV<ReportMike_ScenarioModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Mike_Source":
                    retStr = GenerateForReportTagCSV<ReportMike_SourceModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Mike_Source_Start_End":
                    retStr = GenerateForReportTagCSV<ReportMike_Source_Start_EndModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Mike_Boundary_Condition":
                    retStr = GenerateForReportTagCSV<ReportMike_Boundary_ConditionModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Mike_Scenario_File":
                    retStr = GenerateForReportTagCSV<ReportMike_Scenario_FileModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Municipality_Contact":
                    retStr = GenerateForReportTagCSV<ReportMunicipality_ContactModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Municipality_Contact_Address":
                    retStr = GenerateForReportTagCSV<ReportMunicipality_Contact_AddressModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Municipality_Contact_Tel":
                    retStr = GenerateForReportTagCSV<ReportMunicipality_Contact_TelModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Municipality_Contact_Email":
                    retStr = GenerateForReportTagCSV<ReportMunicipality_Contact_EmailModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Municipality_File":
                    retStr = GenerateForReportTagCSV<ReportMunicipality_FileModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Subsector_Special_Table":
                    retStr = GenerateForReportTagCSV<ReportSubsector_Special_TableModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Subsector_Climate_Site":
                    retStr = GenerateForReportTagCSV<ReportSubsector_Climate_SiteModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Subsector_Climate_Site_Data":
                    retStr = GenerateForReportTagCSV<ReportSubsector_Climate_Site_DataModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Subsector_Hydrometric_Site":
                    retStr = GenerateForReportTagCSV<ReportSubsector_Hydrometric_SiteModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Subsector_Hydrometric_Site_Data":
                    retStr = GenerateForReportTagCSV<ReportSubsector_Hydrometric_Site_DataModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Subsector_Hydrometric_Site_Rating_Curve":
                    retStr = GenerateForReportTagCSV<ReportSubsector_Hydrometric_Site_Rating_CurveModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Subsector_Hydrometric_Site_Rating_Curve_Value":
                    retStr = GenerateForReportTagCSV<ReportSubsector_Hydrometric_Site_Rating_Curve_ValueModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Subsector_Tide_Site":
                    retStr = GenerateForReportTagCSV<ReportSubsector_Tide_SiteModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Subsector_Tide_Site_Data":
                    retStr = GenerateForReportTagCSV<ReportSubsector_Tide_Site_DataModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Subsector_Lab_Sheet":
                    retStr = GenerateForReportTagCSV<ReportSubsector_Lab_SheetModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Subsector_Lab_Sheet_Detail":
                    retStr = GenerateForReportTagCSV<ReportSubsector_Lab_Sheet_DetailModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Subsector_Lab_Sheet_Tube_And_MPN_Detail":
                    retStr = GenerateForReportTagCSV<ReportSubsector_Lab_Sheet_Tube_And_MPN_DetailModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Subsector_File":
                    retStr = GenerateForReportTagCSV<ReportSubsector_FileModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Sector_File":
                    retStr = GenerateForReportTagCSV<ReportSector_FileModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Area_File":
                    retStr = GenerateForReportTagCSV<ReportArea_FileModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Sampling_Plan":
                    retStr = GenerateForReportTagCSV<ReportSampling_PlanModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Sampling_Plan_Lab_Sheet":
                    retStr = GenerateForReportTagCSV<ReportSampling_Plan_Lab_SheetModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Sampling_Plan_Lab_Sheet_Detail":
                    retStr = GenerateForReportTagCSV<ReportSampling_Plan_Lab_Sheet_DetailModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Sampling_Plan_Lab_Sheet_Tube_And_MPN_Detail":
                    retStr = GenerateForReportTagCSV<ReportSampling_Plan_Lab_Sheet_Tube_And_MPN_DetailModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Sampling_Plan_Subsector":
                    retStr = GenerateForReportTagCSV<ReportSampling_Plan_SubsectorModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Sampling_Plan_Subsector_Site":
                    retStr = GenerateForReportTagCSV<ReportSampling_Plan_Subsector_SiteModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Climate_Site":
                    retStr = GenerateForReportTagCSV<ReportClimate_SiteModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Climate_Site_Data":
                    retStr = GenerateForReportTagCSV<ReportClimate_Site_DataModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Hydrometric_Site":
                    retStr = GenerateForReportTagCSV<ReportHydrometric_SiteModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Hydrometric_Site_Data":
                    retStr = GenerateForReportTagCSV<ReportHydrometric_Site_DataModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Hydrometric_Site_Rating_Curve":
                    retStr = GenerateForReportTagCSV<ReportHydrometric_Site_Rating_CurveModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Hydrometric_Site_Rating_Curve_Value":
                    retStr = GenerateForReportTagCSV<ReportHydrometric_Site_Rating_Curve_ValueModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Province_File":
                    retStr = GenerateForReportTagCSV<ReportProvince_FileModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Country_File":
                    retStr = GenerateForReportTagCSV<ReportCountry_FileModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "MPN_Lookup":
                    retStr = GenerateForReportTagCSV<ReportMPN_LookupModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                case "Root_File":
                    retStr = GenerateForReportTagCSV<ReportRoot_FileModel>(PreTextOfLine, sbNewFileText, ReportTag);
                    break;
                default:
                    {
                        retStr = string.Format(ReportServiceRes._NotImplementedIn_, ReportTag.TagItem, "ReportGetDBOfType");
                    }
                    break;
            }


            return retStr;
        }
        public string GenerateReportFromTemplateCSV(FileInfo fiCSV, int StartTVItemID, int Take, int AppTaskID)
        {
            string retStr = "";

            if (!fiCSV.Exists)
                return string.Format(ReportServiceRes.FileDoesNotExist_, fiCSV.FullName);

            StringBuilder sbFileText = new StringBuilder();
            StreamReader sr = fiCSV.OpenText();
            sbFileText.Append(sr.ReadToEnd());
            sr.Close();

            List<ReportTag> reportTagList = new List<Services.ReportTag>();
            retStr = CheckTagsAndContentOKCSV(sbFileText, reportTagList, StartTVItemID, Take);
            if (!string.IsNullOrWhiteSpace(retStr))
            {
                SaveFileCSV(sbFileText, fiCSV);
                return retStr;
            }

            ReportModelDynamic reportModelDynamic = new ReportModelDynamic();

            if (reportTagList.Count > 0)
            {
                foreach (ReportTag reportTag in reportTagList.Where(c => c.ReportTagParent == null))
                {
                    retStr = FillTemplateWithDBInfoCSV(sbFileText, reportTag);
                    if (!string.IsNullOrWhiteSpace(retStr))
                    {
                        sbFileText.Insert(0, "\r\n\r\n" + retStr + "\r\n\r\n");

                        SaveFileCSV(sbFileText, fiCSV);
                        return retStr;
                    }
                }
            }

            sbFileText = new StringBuilder(sbFileText.ToString().Trim());
            SaveFileCSV(sbFileText, fiCSV);

            return "";
        }
        public string GetInfoFromDBCSV(StringBuilder sbFileText, List<ReportTag> reportTagList, ReportModelDynamic reportModelDynamic)
        {
            string retStr = "";

            foreach (ReportTag reportTag in reportTagList.Where(c => c.Level == 0))
            {
                retStr = ReportGetDBOfType(reportTag, reportModelDynamic);
                if (!string.IsNullOrWhiteSpace(retStr))
                    return retStr;
            }

            return "";
        }
        public void GetSelectedFieldsAndPropertiesCSV(ReportTreeNode reportTreeNodeTable, StringBuilder sb)
        {
            foreach (ReportTreeNode RTNFieldsHolder in reportTreeNodeTable.Nodes)
            {
                if (RTNFieldsHolder.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.FieldsHolder)
                {
                    foreach (ReportTreeNode RTN in RTNFieldsHolder.Nodes)
                    {
                        if (RTN.Checked)
                        {
                            switch (RTN.ReportFieldType)
                            {
                                case ReportFieldTypeEnum.Error:
                                    {
                                        sb.AppendLine("Error: " + RTN.Text);
                                    }
                                    break;
                                case ReportFieldTypeEnum.DateAndTime:
                                case ReportFieldTypeEnum.NumberWhole:
                                case ReportFieldTypeEnum.NumberWithDecimal:
                                case ReportFieldTypeEnum.Text:
                                case ReportFieldTypeEnum.TrueOrFalse:
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
                                        sb.Append(RTN.Text);
                                        StringBuilder sbFilter = new StringBuilder();
                                        //sb.AppendLine(RTN.Text + GetFieldFormatText(RTN) + GetFieldReportConditionText(RTN))
                                        GetFieldDBFilterText(RTN, sbFilter);
                                        sb.AppendLine(sbFilter.ToString());
                                    }
                                    break;
                                default:
                                    {
                                        sb.AppendLine("NotImplemented: " + RTN.Text);
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
        }
        public void GetSelectedFieldsContainerCSV(ReportTreeNode reportTreeNodeTable, StringBuilder sb)
        {
            foreach (ReportTreeNode RTNFieldsHolder in reportTreeNodeTable.Nodes)
            {
                if (RTNFieldsHolder.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.FieldsHolder)
                {
                    StringBuilder sbCondition = new StringBuilder();
                    List<string> fieldList = new List<string>();
                    foreach (ReportTreeNode RTN in RTNFieldsHolder.Nodes)
                    {
                        if (RTN.Checked)
                        {
                            switch (RTN.ReportFieldType)
                            {
                                case ReportFieldTypeEnum.Error:
                                    {
                                        sb.AppendLine("Error: " + RTN.Text);
                                    }
                                    break;
                                case ReportFieldTypeEnum.DateAndTime:
                                case ReportFieldTypeEnum.NumberWhole:
                                case ReportFieldTypeEnum.NumberWithDecimal:
                                case ReportFieldTypeEnum.Text:
                                case ReportFieldTypeEnum.TrueOrFalse:
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
                                        sb.Append(Marker + RTN.Text + GetFieldReportFormatText(RTN) + GetFieldReportConditionText(RTN) + Marker + " ");
                                        fieldList.Add(Marker + RTN.Text + GetFieldReportFormatText(RTN) + Marker);
                                        string Condition = GetFieldReportConditionText(RTN);
                                        if (!string.IsNullOrWhiteSpace(Condition))
                                        {
                                            sbCondition.AppendLine(RTN.Text + GetFieldReportFormatText(RTN) + Condition);
                                        }
                                    }
                                    break;
                                default:
                                    {
                                        sb.AppendLine("NotImplemented: " + RTN.Text);
                                    }
                                    break;
                            }
                        }
                    }
                    if (sbCondition.Length != 0)
                    {
                        sbCondition.Insert(0, "\r\n" + Marker + "ShowStart " + reportTreeNodeTable.Text + " " + LanguageRequest.ToString() + "\r\n");
                        sbCondition.AppendLine(Marker);
                        foreach (string s in fieldList)
                        {
                            sbCondition.Append(s + " ");
                        }
                        sbCondition.AppendLine("");
                        sbCondition.AppendLine(Marker + "ShowEnd " + reportTreeNodeTable.Text + " " + LanguageRequest.ToString() + Marker);

                        sb.AppendLine(sbCondition.ToString());
                    }
                    else
                    {
                        if (fieldList.Count > 0)
                        {
                            sb.AppendLine("");
                        }
                    }
                }
            }
        }
        public void GetSelectedFieldsFirstLineCSV(ReportTreeNode reportTreeNodeTable, StringBuilder sb)
        {
            foreach (ReportTreeNode RTNFieldsHolder in reportTreeNodeTable.Nodes)
            {
                if (RTNFieldsHolder.ReportTreeNodeSubType == ReportTreeNodeSubTypeEnum.FieldsHolder)
                {
                    foreach (ReportTreeNode RTN in RTNFieldsHolder.Nodes)
                    {
                        if (RTN.Checked)
                        {
                            switch (RTN.ReportFieldType)
                            {
                                case ReportFieldTypeEnum.Error:
                                    {
                                        sb.AppendLine("Error: " + RTN.Text);
                                    }
                                    break;
                                case ReportFieldTypeEnum.DateAndTime:
                                case ReportFieldTypeEnum.NumberWhole:
                                case ReportFieldTypeEnum.NumberWithDecimal:
                                case ReportFieldTypeEnum.Text:
                                case ReportFieldTypeEnum.TrueOrFalse:
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
                                        sb.Append(RTN.Text + ",");
                                    }
                                    break;
                                default:
                                    {
                                        sb.AppendLine("NotImplemented: " + RTN.Text);
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
        }
        public void GetTreeViewSelectedStatusCSV(ReportTreeNode reportTreeNode, StringBuilder sb, StringBuilder sbFirstLine, int Level)
        {
            if (reportTreeNode == null)
                return;

            switch (reportTreeNode.ReportTreeNodeSubType)
            {
                case ReportTreeNodeSubTypeEnum.Error:
                    {
                        sb.AppendLine("Error occured. Please select an item on the left tree view.");
                    }
                    break;
                case ReportTreeNodeSubTypeEnum.TableNotSelectable:
                case ReportTreeNodeSubTypeEnum.TableSelectable:
                    {
                        if (Level == 0)
                        {
                            GetSelectedFieldsFirstLineCSV(reportTreeNode, sbFirstLine);
                            sb.AppendLine(Marker + "Start " + reportTreeNode.Text + " " + LanguageRequest.ToString());
                            GetSelectedFieldsAndPropertiesCSV(reportTreeNode, sb);
                            sb.AppendLine(Marker);
                            GetSelectedFieldsContainerCSV(reportTreeNode, sb);
                        }
                        else
                        {
                            GetSelectedFieldsFirstLineCSV(reportTreeNode, sbFirstLine);
                            bool CheckExist = GetSubFieldIsChecked(reportTreeNode);
                            if (CheckExist)
                            {
                                sb.AppendLine(Marker + "LoopStart " + reportTreeNode.Text + " " + LanguageRequest.ToString());
                                GetSelectedFieldsAndPropertiesCSV(reportTreeNode, sb);
                                sb.AppendLine(Marker);
                                GetSelectedFieldsContainerCSV(reportTreeNode, sb);
                            }
                        }
                        foreach (ReportTreeNode RTN in reportTreeNode.Nodes)
                        {
                            if (RTN.Checked)
                            {
                                GetTreeViewSelectedStatusCSV(RTN, sb, sbFirstLine, Level + 1);
                            }
                        }
                        if (Level == 0)
                        {
                            sb.AppendLine(Marker + "End " + reportTreeNode.Text + " " + LanguageRequest.ToString() + Marker);
                        }
                        else
                        {
                            bool CheckExist = GetSubFieldIsChecked(reportTreeNode);
                            if (CheckExist)
                            {
                                sb.AppendLine(Marker + "LoopEnd " + reportTreeNode.Text + " " + LanguageRequest.ToString() + Marker);
                            }
                        }
                    }
                    break;
                case ReportTreeNodeSubTypeEnum.Field:
                    {
                        // Nothing for now
                    }
                    break;
                case ReportTreeNodeSubTypeEnum.FieldsHolder:
                    {
                        // Nothing for now

                    }
                    break;
                default:
                    {
                        sb.AppendLine(Marker + "NotImplemented " + reportTreeNode.Text + Marker);
                    }
                    break;
            }
        }
        public string SaveFileCSV(StringBuilder sbFileText, FileInfo fiCSV)
        {
            System.Text.Encoding encDefault = System.Text.Encoding.GetEncoding(0);
            System.IO.FileStream fs = new System.IO.FileStream(fiCSV.FullName, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite);
            System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
            try
            {
                bw.Write(encDefault.GetBytes(sbFileText.ToString()));
            }
            finally
            {
                bw.Close();
                fs.Close();
            }

            return "";
        }
        #endregion Functions public
    }
}
