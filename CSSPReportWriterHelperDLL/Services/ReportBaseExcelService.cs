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
        //public string CheckTagsFirstLineExcel(ReportTag reportTag)
        //{
        //    int FirstLineCount = 3;
        //    Microsoft.Office.Interop.Excel.Range tempRangeBeginning = reportTag.doc.Range().Duplicate;
        //    tempRangeBeginning.End = reportTag.doc.Range().Start;

        //    if (reportTag.RangeStartTag.Text.IndexOf("\r") == -1)
        //    {
        //        reportTag.Error = ReportServiceRes.CouldNotFindFirstLineReturnsAreRequired;
        //        tempRangeBeginning.Comments.Add(tempRangeBeginning, reportTag.Error);
        //        return reportTag.Error;
        //    }

        //    string FirstLine = reportTag.RangeStartTag.Text.Substring(0, reportTag.RangeStartTag.Text.IndexOf("\r"));

        //    List<string> strList = FirstLine.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

        //    if (strList.Count != FirstLineCount)
        //    {
        //        reportTag.Error = string.Format(ReportServiceRes._DoesNotContain_Items, FirstLine, FirstLineCount.ToString()) + "\r\n\r\n" +
        //            string.Format(ReportServiceRes.Example_, Marker + "Start Root en");
        //        tempRangeBeginning.Comments.Add(tempRangeBeginning, reportTag.Error);
        //        return reportTag.Error;
        //    }

        //    reportTag.TagItem = strList[1];

        //    reportTag.ReportType = _ReportBase.GetReportType(reportTag.TagItem);
        //    if (reportTag.ReportType == null)
        //    {
        //        reportTag.Error = string.Format(ReportServiceRes.ItemName_NotAllowed, reportTag.TagItem) + "\r\n\r\n" +
        //             string.Format(ReportServiceRes.AllowableValues_, _ReportBase.AllowableReportType());
        //        tempRangeBeginning.Comments.Add(tempRangeBeginning, reportTag.Error);
        //        return reportTag.Error;
        //    }

        //    reportTag.Language = strList[2];

        //    if (!(reportTag.Language == "en" || reportTag.Language == "fr"))
        //    {
        //        reportTag.Error = string.Format(ReportServiceRes.Language_NotAllowed, reportTag.Language) + "\r\n\r\n" +
        //            string.Format(ReportServiceRes.AllowableLanguages_, "en, fr");
        //        tempRangeBeginning.Comments.Add(tempRangeBeginning, reportTag.Error);
        //        return reportTag.Error;
        //    }

        //    return "";
        //}
        //public string FindTagsExcel(ReportTag reportTag, List<ReportTag> reportTagList)
        //{
        //    bool TopTag = reportTag.RangeInnerTag == null ? true : false;
        //    int MinRange = reportTag.RangeInnerTag == null ? reportTag.doc.Range().Start : reportTag.RangeInnerTag.Start;
        //    int MaxRange = reportTag.RangeInnerTag == null ? reportTag.doc.Range().End : reportTag.RangeInnerTag.End;
        //    string retStr = "";

        //    Microsoft.Office.Interop.Excel.Range tempRange = reportTag.doc.Range().Duplicate;

        //    List<string> tagNameList = new List<string>() { "Start", "LoopStart", "ShowStart" };

        //    tempRange = reportTag.doc.Range().Duplicate;
        //    tempRange.Start = MinRange;
        //    tempRange.End = MaxRange;

        //    int TagNumber = -1;

        //    bool Found = true;
        //    while (Found)
        //    {
        //        int oldStart = tempRange.Start;

        //        tempRange.Find.ClearFormatting();
        //        tempRange.Find.MatchWildcards = true;
        //        string FindText = Marker + "*" + Marker;

        //        tempRange.Find.Execute(FindText: FindText, Forward: true);

        //        if (tempRange.Find.Found)
        //        {
        //            tempRange.Select();
        //            if (tempRange.Start >= MaxRange)
        //                break;

        //            if (oldStart > tempRange.Start)
        //            {
        //                if (tempRange.Tables.Count > 0)
        //                {
        //                    tempRange.Start = oldStart;
        //                    tempRange.End = tempRange.Start;
        //                    tempRange.End = tempRange.Cells[1].Range.End - 1;

        //                    tempRange.Find.ClearFormatting();
        //                    tempRange.Find.MatchWildcards = true;
        //                    tempRange.Find.Execute(FindText: FindText, Forward: true);
        //                }
        //            }

        //            if (tempRange.Text == null || !(tempRange.Text.StartsWith(Marker + "Start")
        //                || tempRange.Text.StartsWith(Marker + "LoopStart")
        //                || tempRange.Text.StartsWith(Marker + "ShowStart")))
        //            {
        //                continue;
        //            }
        //            else
        //            {
        //                if (tempRange.Text.StartsWith(Marker + "Start"))
        //                {
        //                    TagNumber = 0;
        //                }
        //                else if (tempRange.Text.StartsWith(Marker + "LoopStart"))
        //                {
        //                    TagNumber = 1;
        //                }
        //                else if (tempRange.Text.StartsWith(Marker + "ShowStart"))
        //                {
        //                    TagNumber = 2;
        //                }
        //                else
        //                {
        //                    // nothing
        //                }
        //            }

        //            tempRange.End = tempRange.End + 1;

        //            if (!tempRange.Text.EndsWith("\r"))
        //            {
        //                string ErrorText = string.Format(ReportServiceRes._TagMustBeFollowedWithAReturn, Marker + tagNameList[TagNumber] + "*" + Marker);
        //                tempRange.Comments.Add(tempRange, ErrorText);
        //                return ErrorText;
        //            }

        //            if (!tempRange.Text.StartsWith(Marker + tagNameList[TagNumber] + " "))
        //            {
        //                string ErrorText = string.Format(ReportServiceRes.Tag_NotWellFormedItShouldHaveAStructureLike_, tagNameList[TagNumber], Marker + tagNameList[TagNumber] + " Root en" + Marker);
        //                tempRange.Comments.Add(tempRange, ErrorText);
        //                return ErrorText;
        //            }

        //            string TagItem = tempRange.Text.Substring((Marker + tagNameList[TagNumber] + " ").Length);
        //            TagItem = TagItem.Substring(0, TagItem.IndexOf(" "));

        //            string Language = tempRange.Text.Substring((Marker + tagNameList[TagNumber] + " " + TagItem + " ").Length);
        //            if (string.IsNullOrWhiteSpace(Language) || Language.Length < 3)
        //            {
        //                string ErrorText = string.Format(ReportServiceRes.Tag_NotWellFormedItShouldHaveAStructureLike_, tagNameList[TagNumber], Marker + tagNameList[TagNumber] + " Root en" + Marker);
        //                tempRange.Comments.Add(tempRange, ErrorText);
        //                return ErrorText;
        //            }

        //            Language = Language.Trim().Substring(0, 2);
        //            if (!(Language == "en" || Language == "fr"))
        //            {
        //                string ErrorText = string.Format(ReportServiceRes.Tag_NotWellFormedItShouldHaveAStructureLike_, tagNameList[TagNumber], Marker + tagNameList[TagNumber] + " Root en" + Marker);
        //                tempRange.Comments.Add(tempRange, ErrorText);
        //                return ErrorText;
        //            }

        //            FindText = Marker + tagNameList[TagNumber].Replace("Start", "End") + " " + TagItem + " " + Language + Marker;

        //            ReportTag reportTagChild = new ReportTag();
        //            reportTagChild.wordApp = reportTag.wordApp;
        //            reportTagChild.doc = reportTag.doc;
        //            reportTagChild.RangeStartTag = reportTag.doc.Range().Duplicate;
        //            reportTagChild.RangeStartTag.Start = tempRange.Start;
        //            reportTagChild.RangeStartTag.End = tempRange.End;
        //            reportTagChild.DocumentType = DocumentType.Excel;
        //            reportTagChild.OnlyImmediateChildren = reportTag.OnlyImmediateChildren;

        //            if (tempRange.End < MaxRange)
        //            {
        //                tempRange.Start = tempRange.End;
        //                tempRange.End = MaxRange;
        //            }

        //            reportTagChild.TagName = tagNameList[TagNumber];
        //            reportTagChild.TagItem = TagItem;
        //            reportTagChild.Take = reportTag.Take;
        //            reportTagChild.UnderTVItemID = reportTag.UnderTVItemID;
        //            if (TopTag)
        //            {
        //                reportTagChild.ReportTagParent = null;
        //                reportTagChild.Level = 0;
        //            }
        //            else
        //            {
        //                reportTagChild.ReportTagParent = reportTag;
        //                reportTagChild.Level = reportTag.Level + 1;
        //            }

        //            tempRange.Find.Execute(FindText: FindText, Forward: true);

        //            if (!tempRange.Find.Found)
        //            {
        //                string ErrorText = string.Format(ReportServiceRes.CouldNotFindEndTag_Of_, FindText, reportTagChild.RangeStartTag.Text);
        //                reportTagChild.RangeStartTag.Comments.Add(reportTagChild.RangeStartTag, ErrorText);
        //                return ErrorText;
        //            }

        //            tempRange.End = tempRange.End + 1;

        //            if (!tempRange.Text.EndsWith("\r"))
        //            {
        //                string ErrorText = string.Format(ReportServiceRes._TagMustBeFollowedWithAReturn, FindText);
        //                tempRange.Comments.Add(tempRange, ErrorText);
        //                return ErrorText;
        //            }

        //            reportTagChild.RangeEndTag = reportTag.doc.Range().Duplicate;
        //            reportTagChild.RangeEndTag.Start = tempRange.Start;
        //            reportTagChild.RangeEndTag.End = tempRange.End;

        //            reportTagChild.RangeInnerTag = reportTag.doc.Range().Duplicate;
        //            if (reportTagChild.RangeStartTag.Tables.Count > 0)
        //            {
        //                if (reportTagChild.TagName == "Start")
        //                {
        //                    string ErrorText = ReportServiceRes.StartTagMustNotResideInATable;
        //                    reportTagChild.RangeStartTag.Comments.Add(reportTagChild.RangeStartTag, ErrorText);
        //                    return ErrorText;
        //                }
        //                else if (reportTagChild.TagName == "LoopStart")
        //                {
        //                    reportTagChild.RangeInnerTag = reportTagChild.RangeStartTag.Tables[1].Rows[reportTagChild.RangeStartTag.Rows[1].Index + 1].Range;
        //                }
        //                else
        //                {
        //                    reportTagChild.RangeInnerTag.Start = reportTagChild.RangeStartTag.End;
        //                    reportTagChild.RangeInnerTag.End = reportTagChild.RangeEndTag.Start;
        //                }
        //            }
        //            else
        //            {
        //                reportTagChild.RangeInnerTag.Start = reportTagChild.RangeStartTag.End;
        //                reportTagChild.RangeInnerTag.End = reportTagChild.RangeEndTag.Start;
        //            }

        //            retStr = CheckTagsFirstLineExcel(reportTagChild);
        //            if (!string.IsNullOrWhiteSpace(retStr))
        //                return retStr;

        //            bool IsDBFiltering = reportTagChild.TagName == "ShowStart" ? false : true;
        //            retStr = GetReportTreeNodesFromTagText(reportTagChild.RangeStartTag.Text, reportTagChild.TagItem, reportTagChild.ReportType, reportTagChild.ReportTreeNodeList, IsDBFiltering);
        //            if (!string.IsNullOrWhiteSpace(retStr))
        //            {
        //                reportTagChild.RangeStartTag.Comments.Add(reportTagChild.RangeStartTag, retStr);
        //                return retStr;
        //            }

        //            //retStr = CheckTagsVariableBeingUsedExcel(reportTagChild);
        //            //if (!string.IsNullOrWhiteSpace(retStr))
        //            //    return retStr;

        //            //retStr = CheckTagsVariableNotDeclaredExcel(reportTagChild);
        //            //if (!string.IsNullOrWhiteSpace(retStr))
        //            //    return retStr;


        //            if (reportTagList.Count == 0 && !reportTag.OnlyImmediateChildren)
        //            {
        //                if (reportTagChild.TagName != tagNameList[0])
        //                {
        //                    string ErrorText = ReportServiceRes.FirstTagInDocumentMustBeAStartTag;
        //                    tempRange.Comments.Add(tempRange, ErrorText);
        //                    return ErrorText;
        //                }
        //            }

        //            reportTagList.Add(reportTagChild);

        //            if (!reportTag.OnlyImmediateChildren)
        //            {
        //                retStr = FindTagsExcel(reportTagChild, reportTagList);
        //                if (!string.IsNullOrWhiteSpace(retStr))
        //                    return retStr;

        //                tempRange.Start = reportTagChild.RangeEndTag.End;
        //                tempRange.End = MaxRange;
        //            }
        //            else
        //            {
        //                tempRange.Start = reportTagChild.RangeEndTag.End;
        //                tempRange.End = MaxRange;
        //            }
        //        }
        //        else
        //        {
        //            Found = false;
        //        }
        //    }

        //    return "";
        //}
        //public string FillTemplateWithDBInfoExcel(ReportTag reportTag)
        //{
        //    Microsoft.Office.Interop.Excel.Range nextTagRange = reportTag.doc.Range();

        //    List<ReportTag> reportTagList = new List<ReportTag>();

        //    string retStr = FindTagsExcel(reportTag, reportTagList);
        //    if (!string.IsNullOrWhiteSpace(retStr))
        //        return retStr;

        //    foreach (ReportTag reportTagStart in reportTagList.Where(c => c.ReportTagParent == null).OrderByDescending(c => c.RangeStartTag.Start))
        //    {
        //        retStr = GenerateNextReportTagExcel(reportTagStart);
        //        if (!string.IsNullOrWhiteSpace(retStr))
        //            return retStr;
        //    }

        //    nextTagRange = reportTag.doc.Range();
        //    nextTagRange.Start = 0;
        //    nextTagRange.End = 0;
        //    nextTagRange.Select();

        //    return "";
        //}
        //public string GenerateForReportTagExcel<T>(ReportTag reportTag) where T : new()
        //{
        //    Microsoft.Office.Interop.Excel.Range tempRange = reportTag.doc.Range();
        //    Microsoft.Office.Interop.Excel.Range CurrentCopyInnerTagRange = reportTag.doc.Range();
        //    Microsoft.Office.Interop.Excel.Range midRowRange = reportTag.doc.Range();
        //    Microsoft.Office.Interop.Excel.Range newRowRange = reportTag.doc.Range();

        //    Microsoft.Office.Interop.Excel.Table table = null;
        //    Microsoft.Office.Interop.Excel.Row rowTemplate = null;
        //    Microsoft.Office.Interop.Excel.Row beforeRow = null;

        //    try
        //    {
        //        table = reportTag.doc.Tables[1];
        //        rowTemplate = reportTag.doc.Tables[1].Rows[1];
        //        beforeRow = reportTag.doc.Tables[1].Rows[1];
        //    }
        //    catch (Exception)
        //    {
        //        // nothing
        //    }

        //    ReportModelDynamic reportModelDynamic = new ReportModelDynamic();
        //    string retStr = ReportGetDBOfType(reportTag, reportModelDynamic);
        //    if (!string.IsNullOrWhiteSpace(retStr))
        //    {
        //        CurrentCopyInnerTagRange.Comments.Add(CurrentCopyInnerTagRange, retStr);
        //        return retStr;
        //    }

        //    List<T> reportModelList = reportModelDynamic.ReportModel;

        //    for (int rmCount = reportModelList.Count - 1; rmCount > -1; rmCount--)
        //    {
        //        CurrentCopyInnerTagRange.Start = reportTag.RangeInnerTag.Start;
        //        CurrentCopyInnerTagRange.End = reportTag.RangeInnerTag.End;

        //        CurrentCopyInnerTagRange.Select();

        //        foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
        //        {
        //            if (propertyInfo.Name == reportTag.TagItem + "_Error")
        //            {
        //                string errStr = (string)propertyInfo.GetValue(reportModelList[rmCount]);
        //                if (!string.IsNullOrWhiteSpace(errStr))
        //                {
        //                    reportTag.RangeInnerTag.Comments.Add(reportTag.RangeStartTag, "ERROR: " + errStr);
        //                    return "ERROR: " + errStr;
        //                }
        //            }
        //        }

        //        if (reportTag.TagName != "Start")
        //        {
        //            if (reportTag.RangeInnerTag.Tables.Count > 0)
        //            {
        //                midRowRange.Start = (int)((reportTag.RangeInnerTag.Start + reportTag.RangeInnerTag.End) / 2);
        //                midRowRange.End = midRowRange.Start + 1;
        //                rowTemplate = midRowRange.Rows[1];
        //                rowTemplate.Select();
        //                table = midRowRange.Tables[1];
        //                beforeRow = table.Rows[rowTemplate.Index + 1];
        //                newRowRange = rowTemplate.Range;
        //                newRowRange.Start = rowTemplate.Range.End;
        //                newRowRange.End = rowTemplate.Range.End;
        //                newRowRange.Select();
        //                newRowRange.FormattedText = rowTemplate.Range.FormattedText;
        //                newRowRange.Select();
        //                CurrentCopyInnerTagRange.Start = newRowRange.Start;
        //                CurrentCopyInnerTagRange.End = newRowRange.End;
        //            }
        //            else
        //            {
        //                CurrentCopyInnerTagRange.Start = reportTag.RangeEndTag.End;
        //                CurrentCopyInnerTagRange.End = CurrentCopyInnerTagRange.Start;
        //                CurrentCopyInnerTagRange.FormattedText = reportTag.RangeInnerTag.FormattedText;
        //                CurrentCopyInnerTagRange.End = CurrentCopyInnerTagRange.Start + reportTag.RangeInnerTag.FormattedText.Text.Length;
        //            }

        //            CurrentCopyInnerTagRange.Select();
        //        }
        //        else
        //        {
        //            reportTag.RangeStartTag.Text = "";
        //            reportTag.RangeEndTag.Text = "";
        //        }

        //        for (int i = 0, count = reportTag.ReportTreeNodeList.Count(); i < count; i++)
        //        {
        //            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
        //            {
        //                if (propertyInfo.Name == reportTag.ReportTreeNodeList[i].Text)
        //                {
        //                    if (propertyInfo.GetValue(reportModelList[rmCount]) == null)
        //                    {

        //                        bool Found = true;
        //                        while (Found)
        //                        {
        //                            tempRange.Start = CurrentCopyInnerTagRange.Start;
        //                            tempRange.End = CurrentCopyInnerTagRange.End;

        //                            tempRange.Select();

        //                            tempRange.Find.ClearFormatting();
        //                            tempRange.Find.MatchWildcards = true;
        //                            string FindText = Marker + propertyInfo.Name + "*" + Marker;
        //                            tempRange.Find.Execute(FindText: FindText, Forward: true); // + "\r");

        //                            if (!tempRange.Find.Found)
        //                                break;

        //                            tempRange.Select();
        //                            tempRange.Text = "empty";
        //                        }
        //                    }
        //                    else
        //                    {
        //                        bool Found = true;
        //                        while (Found)
        //                        {
        //                            tempRange.Start = CurrentCopyInnerTagRange.Start;
        //                            tempRange.End = CurrentCopyInnerTagRange.End;

        //                            tempRange.Select();

        //                            tempRange.Find.ClearFormatting();
        //                            tempRange.Find.MatchWildcards = true;
        //                            string FindText = Marker + propertyInfo.Name + "*" + Marker;
        //                            tempRange.Find.Execute(FindText: FindText, Forward: true); // + "\r");

        //                            if (!tempRange.Find.Found)
        //                                break;

        //                            tempRange.Select();
        //                            //string InnerText = tempRange.Text.Substring((Marker + propertyInfo.Name).Length);
        //                            //InnerText = InnerText.Substring(0, InnerText.Length - Marker.Length);

        //                            retStr = (string)ReportGetFieldTextOrValue<T>(reportModelList[rmCount], true, propertyInfo, tempRange.Text, reportTag, reportTag.ReportTreeNodeList[i]);
        //                            if (!string.IsNullOrWhiteSpace(reportTag.Error))
        //                            {
        //                                tempRange.Comments.Add(tempRange, reportTag.Error);
        //                                return reportTag.Error;
        //                            }

        //                            tempRange.Text = retStr;
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
        //        {
        //            if (propertyInfo.Name == reportTag.TagItem + "_ID")
        //            {
        //                reportTag.UnderTVItemID = (int)propertyInfo.GetValue(reportModelList[rmCount]);
        //                break;
        //            }
        //        }

        //        CurrentCopyInnerTagRange.Select();

        //        List<ReportTag> reportTagChildrenList = new List<ReportTag>();

        //        ReportTag reportTagInner = new ReportTag()
        //        {
        //            wordApp = reportTag.wordApp,
        //            doc = reportTag.doc,
        //            OnlyImmediateChildren = true,
        //            UnderTVItemID = reportTag.UnderTVItemID,
        //            Take = reportTag.Take,
        //            RangeInnerTag = CurrentCopyInnerTagRange,
        //        };

        //        retStr = FindTagsExcel(reportTagInner, reportTagChildrenList);
        //        if (!string.IsNullOrWhiteSpace(retStr))
        //            return retStr;

        //        for (int tagCount = reportTagChildrenList.Count - 1; tagCount > -1; tagCount--)
        //        {
        //            reportTagChildrenList[tagCount].ReportTagParent = reportTag;
        //            reportTagChildrenList[tagCount].RangeInnerTag.Select();

        //            if (reportTagChildrenList[tagCount].TagName == "ShowStart")
        //            {
        //                bool IsDBFiltering = false;
        //                bool KeepShow = true;
        //                for (int j = 0, countShow = reportTagChildrenList[tagCount].ReportTreeNodeList.Count(); j < countShow; j++)
        //                {
        //                    KeepShow = ReturnKeepShow(reportModelList[rmCount], typeof(T), reportTagChildrenList[tagCount].ReportTreeNodeList[j], IsDBFiltering);

        //                    if (!string.IsNullOrWhiteSpace(reportTagChildrenList[tagCount].ReportTreeNodeList[j].Error))
        //                        return reportTagChildrenList[tagCount].ReportTreeNodeList[j].Error;
        //                }

        //                if (KeepShow)
        //                {
        //                    reportTagChildrenList[tagCount].RangeEndTag.Text = "";
        //                    reportTagChildrenList[tagCount].RangeStartTag.Text = "";
        //                }
        //                else
        //                {
        //                    reportTagChildrenList[tagCount].RangeEndTag.Start = reportTagChildrenList[tagCount].RangeStartTag.Start;
        //                    reportTagChildrenList[tagCount].RangeEndTag.Text = "";
        //                }
        //            }
        //            else
        //            {
        //                retStr = GenerateNextReportTagExcel(reportTagChildrenList[tagCount]);
        //                if (!string.IsNullOrWhiteSpace(retStr))
        //                    return retStr;
        //            }
        //        }
        //    }

        //    if (!(reportTag.TagName == "Start"))
        //    {
        //        if (reportTag.RangeStartTag.Tables.Count > 0)
        //        {
        //            reportTag.RangeStartTag.Rows[1].Delete();
        //            reportTag.RangeEndTag.Start = reportTag.RangeEndTag.End - 1;
        //            reportTag.RangeEndTag.Rows[1].Delete();
        //            reportTag.RangeInnerTag.Start = reportTag.RangeInnerTag.End - 1;
        //            reportTag.RangeInnerTag.Rows[1].Delete();
        //        }
        //        else
        //        {
        //            reportTag.RangeStartTag.Text = "";
        //            reportTag.RangeEndTag.Text = "";
        //            reportTag.RangeInnerTag.Text = "";
        //        }
        //    }

        //    return "";

        //}
        //public string GenerateNextReportTagExcel(ReportTag ReportTag)
        //{
        //    string retStr = "";
        //    switch (ReportTag.TagItem)
        //    {
        //        case "Root":
        //            retStr = GenerateForReportTagExcel<ReportRootModel>(ReportTag);
        //            break;
        //        case "Country":
        //            retStr = GenerateForReportTagExcel<ReportCountryModel>(ReportTag);
        //            break;
        //        case "Province":
        //            retStr = GenerateForReportTagExcel<ReportProvinceModel>(ReportTag);
        //            break;
        //        case "Area":
        //            retStr = GenerateForReportTagExcel<ReportAreaModel>(ReportTag);
        //            break;
        //        case "Sector":
        //            retStr = GenerateForReportTagExcel<ReportSectorModel>(ReportTag);
        //            break;
        //        case "Subsector":
        //            retStr = GenerateForReportTagExcel<ReportSubsectorModel>(ReportTag);
        //            break;
        //        case "MWQM_Site":
        //            retStr = GenerateForReportTagExcel<ReportMWQM_SiteModel>(ReportTag);
        //            break;
        //        case "MWQM_Site_Sample":
        //            retStr = GenerateForReportTagExcel<ReportMWQM_Site_SampleModel>(ReportTag);
        //            break;
        //        case "MWQM_Site_Start_And_End_Date":
        //            retStr = GenerateForReportTagExcel<ReportMWQM_Site_Start_And_End_DateModel>(ReportTag);
        //            break;
        //        case "MWQM_Site_File":
        //            retStr = GenerateForReportTagExcel<ReportMWQM_Site_FileModel>(ReportTag);
        //            break;
        //        case "MWQM_Run":
        //            retStr = GenerateForReportTagExcel<ReportMWQM_RunModel>(ReportTag);
        //            break;
        //        case "MWQM_Run_Sample":
        //            retStr = GenerateForReportTagExcel<ReportMWQM_Run_SampleModel>(ReportTag);
        //            break;
        //        case "MWQM_Run_Lab_Sheet":
        //            retStr = GenerateForReportTagExcel<ReportMWQM_Run_Lab_SheetModel>(ReportTag);
        //            break;
        //        case "MWQM_Run_Lab_Sheet_Detail":
        //            retStr = GenerateForReportTagExcel<ReportMWQM_Run_Lab_Sheet_DetailModel>(ReportTag);
        //            break;
        //        case "MWQM_Run_Lab_Sheet_Tube_And_MPN_Detail":
        //            retStr = GenerateForReportTagExcel<ReportMWQM_Run_Lab_Sheet_Tube_And_MPN_DetailModel>(ReportTag);
        //            break;
        //        case "MWQM_Run_File":
        //            retStr = GenerateForReportTagExcel<ReportMWQM_Run_FileModel>(ReportTag);
        //            break;
        //        case "Pol_Source_Site":
        //            retStr = GenerateForReportTagExcel<ReportPol_Source_SiteModel>(ReportTag);
        //            break;
        //        case "Pol_Source_Site_Obs":
        //            retStr = GenerateForReportTagExcel<ReportPol_Source_Site_ObsModel>(ReportTag);
        //            break;
        //        case "Pol_Source_Site_Obs_Issue":
        //            retStr = GenerateForReportTagExcel<ReportPol_Source_Site_Obs_IssueModel>(ReportTag);
        //            break;
        //        case "Pol_Source_Site_File":
        //            retStr = GenerateForReportTagExcel<ReportPol_Source_Site_FileModel>(ReportTag);
        //            break;
        //        case "Pol_Source_Site_Address":
        //            retStr = GenerateForReportTagExcel<ReportPol_Source_Site_AddressModel>(ReportTag);
        //            break;
        //        case "Municipality":
        //            retStr = GenerateForReportTagExcel<ReportMunicipalityModel>(ReportTag);
        //            break;
        //        case "Infrastructure":
        //            retStr = GenerateForReportTagExcel<ReportInfrastructureModel>(ReportTag);
        //            break;
        //        case "Box_Model":
        //            retStr = GenerateForReportTagExcel<ReportBox_ModelModel>(ReportTag);
        //            break;
        //        case "Box_Model_Result":
        //            retStr = GenerateForReportTagExcel<ReportBox_Model_ResultModel>(ReportTag);
        //            break;
        //        case "Visual_Plumes_Scenario":
        //            retStr = GenerateForReportTagExcel<ReportVisual_Plumes_ScenarioModel>(ReportTag);
        //            break;
        //        case "Visual_Plumes_Scenario_Ambient":
        //            retStr = GenerateForReportTagExcel<ReportVisual_Plumes_Scenario_AmbientModel>(ReportTag);
        //            break;
        //        case "Visual_Plumes_Scenario_Result":
        //            retStr = GenerateForReportTagExcel<ReportVisual_Plumes_Scenario_ResultModel>(ReportTag);
        //            break;
        //        case "Infrastructure_Address":
        //            retStr = GenerateForReportTagExcel<ReportInfrastructure_AddressModel>(ReportTag);
        //            break;
        //        case "Infrastructure_File":
        //            retStr = GenerateForReportTagExcel<ReportInfrastructure_FileModel>(ReportTag);
        //            break;
        //        case "Mike_Scenario":
        //            retStr = GenerateForReportTagExcel<ReportMike_ScenarioModel>(ReportTag);
        //            break;
        //        case "Mike_Source":
        //            retStr = GenerateForReportTagExcel<ReportMike_SourceModel>(ReportTag);
        //            break;
        //        case "Mike_Source_Start_End":
        //            retStr = GenerateForReportTagExcel<ReportMike_Source_Start_EndModel>(ReportTag);
        //            break;
        //        case "Mike_Boundary_Condition":
        //            retStr = GenerateForReportTagExcel<ReportMike_Boundary_ConditionModel>(ReportTag);
        //            break;
        //        case "Mike_Scenario_File":
        //            retStr = GenerateForReportTagExcel<ReportMike_Scenario_FileModel>(ReportTag);
        //            break;
        //        case "Municipality_Contact":
        //            retStr = GenerateForReportTagExcel<ReportMunicipality_ContactModel>(ReportTag);
        //            break;
        //        case "Municipality_Contact_Address":
        //            retStr = GenerateForReportTagExcel<ReportMunicipality_Contact_AddressModel>(ReportTag);
        //            break;
        //        case "Municipality_Contact_Tel":
        //            retStr = GenerateForReportTagExcel<ReportMunicipality_Contact_TelModel>(ReportTag);
        //            break;
        //        case "Municipality_Contact_Email":
        //            retStr = GenerateForReportTagExcel<ReportMunicipality_Contact_EmailModel>(ReportTag);
        //            break;
        //        case "Municipality_File":
        //            retStr = GenerateForReportTagExcel<ReportMunicipality_FileModel>(ReportTag);
        //            break;
        //        case "Subsector_Climate_Site":
        //            retStr = GenerateForReportTagExcel<ReportSubsector_Climate_SiteModel>(ReportTag);
        //            break;
        //        case "Subsector_Climate_Site_Data":
        //            retStr = GenerateForReportTagExcel<ReportSubsector_Climate_Site_DataModel>(ReportTag);
        //            break;
        //        case "Subsector_Hydrometric_Site":
        //            retStr = GenerateForReportTagExcel<ReportSubsector_Hydrometric_SiteModel>(ReportTag);
        //            break;
        //        case "Subsector_Hydrometric_Site_Data":
        //            retStr = GenerateForReportTagExcel<ReportSubsector_Hydrometric_Site_DataModel>(ReportTag);
        //            break;
        //        case "Subsector_Hydrometric_Site_Rating_Curve":
        //            retStr = GenerateForReportTagExcel<ReportSubsector_Hydrometric_Site_Rating_CurveModel>(ReportTag);
        //            break;
        //        case "Subsector_Hydrometric_Site_Rating_Curve_Value":
        //            retStr = GenerateForReportTagExcel<ReportSubsector_Hydrometric_Site_Rating_Curve_ValueModel>(ReportTag);
        //            break;
        //        case "Subsector_Tide_Site":
        //            retStr = GenerateForReportTagExcel<ReportSubsector_Tide_SiteModel>(ReportTag);
        //            break;
        //        case "Subsector_Tide_Site_Data":
        //            retStr = GenerateForReportTagExcel<ReportSubsector_Tide_Site_DataModel>(ReportTag);
        //            break;
        //        case "Subsector_Lab_Sheet":
        //            retStr = GenerateForReportTagExcel<ReportSubsector_Lab_SheetModel>(ReportTag);
        //            break;
        //        case "Subsector_Lab_Sheet_Detail":
        //            retStr = GenerateForReportTagExcel<ReportSubsector_Lab_Sheet_DetailModel>(ReportTag);
        //            break;
        //        case "Subsector_Lab_Sheet_Tube_And_MPN_Detail":
        //            retStr = GenerateForReportTagExcel<ReportSubsector_Lab_Sheet_Tube_And_MPN_DetailModel>(ReportTag);
        //            break;
        //        case "Subsector_File":
        //            retStr = GenerateForReportTagExcel<ReportSubsector_FileModel>(ReportTag);
        //            break;
        //        case "Sector_File":
        //            retStr = GenerateForReportTagExcel<ReportSector_FileModel>(ReportTag);
        //            break;
        //        case "Area_File":
        //            retStr = GenerateForReportTagExcel<ReportArea_FileModel>(ReportTag);
        //            break;
        //        case "Sampling_Plan":
        //            retStr = GenerateForReportTagExcel<ReportSampling_PlanModel>(ReportTag);
        //            break;
        //        case "Sampling_Plan_Lab_Sheet":
        //            retStr = GenerateForReportTagExcel<ReportSampling_Plan_Lab_SheetModel>(ReportTag);
        //            break;
        //        case "Sampling_Plan_Lab_Sheet_Detail":
        //            retStr = GenerateForReportTagExcel<ReportSampling_Plan_Lab_Sheet_DetailModel>(ReportTag);
        //            break;
        //        case "Sampling_Plan_Lab_Sheet_Tube_And_MPN_Detail":
        //            retStr = GenerateForReportTagExcel<ReportSampling_Plan_Lab_Sheet_Tube_And_MPN_DetailModel>(ReportTag);
        //            break;
        //        case "Sampling_Plan_Subsector":
        //            retStr = GenerateForReportTagExcel<ReportSampling_Plan_SubsectorModel>(ReportTag);
        //            break;
        //        case "Sampling_Plan_Subsector_Site":
        //            retStr = GenerateForReportTagExcel<ReportSampling_Plan_Subsector_SiteModel>(ReportTag);
        //            break;
        //        case "Climate_Site":
        //            retStr = GenerateForReportTagExcel<ReportClimate_SiteModel>(ReportTag);
        //            break;
        //        case "Climate_Site_Data":
        //            retStr = GenerateForReportTagExcel<ReportClimate_Site_DataModel>(ReportTag);
        //            break;
        //        case "Hydrometric_Site":
        //            retStr = GenerateForReportTagExcel<ReportHydrometric_SiteModel>(ReportTag);
        //            break;
        //        case "Hydrometric_Site_Data":
        //            retStr = GenerateForReportTagExcel<ReportHydrometric_Site_DataModel>(ReportTag);
        //            break;
        //        case "Hydrometric_Site_Rating_Curve":
        //            retStr = GenerateForReportTagExcel<ReportHydrometric_Site_Rating_CurveModel>(ReportTag);
        //            break;
        //        case "Hydrometric_Site_Rating_Curve_Value":
        //            retStr = GenerateForReportTagExcel<ReportHydrometric_Site_Rating_Curve_ValueModel>(ReportTag);
        //            break;
        //        case "Province_File":
        //            retStr = GenerateForReportTagExcel<ReportProvince_FileModel>(ReportTag);
        //            break;
        //        case "Country_File":
        //            retStr = GenerateForReportTagExcel<ReportCountry_FileModel>(ReportTag);
        //            break;
        //        case "MPN_Lookup":
        //            retStr = GenerateForReportTagExcel<ReportMPN_LookupModel>(ReportTag);
        //            break;
        //        case "Root_File":
        //            retStr = GenerateForReportTagExcel<ReportRoot_FileModel>(ReportTag);
        //            break;
        //        default:
        //            {
        //                retStr = string.Format(ReportServiceRes._NotImplementedIn_, ReportTag.TagItem, "ReportGetDBOfType");
        //            }
        //            break;
        //    }


        //    return retStr;
        //}
        //public string GenerateReportFromTemplateExcel(FileInfo fiDoc, int StartTVItemID, int Take)
        //{
        //    string retStr = "";

        //    if (!fiDoc.Exists)
        //        return string.Format(ReportServiceRes.FileDoesNotExist_, fiDoc.FullName);

        //    Microsoft.Office.Interop.Excel.Application wordApp = new Microsoft.Office.Interop.Excel.Application();
        //    Microsoft.Office.Interop.Excel.Document doc = wordApp.Documents.Open(fiDoc.FullName);
        //    Microsoft.Office.Interop.Excel.Range Range = doc.Range();

        //    wordApp.Visible = true;

        //    List<ReportTag> reportTagList = new List<ReportTag>();
        //    ReportTag reportTagStart = new ReportTag()
        //    {
        //        wordApp = wordApp,
        //        doc = doc,
        //        OnlyImmediateChildren = true,
        //        UnderTVItemID = StartTVItemID,
        //        Take = Take
        //    };

        //    retStr = FillTemplateWithDBInfoExcel(reportTagStart);
        //    if (!string.IsNullOrWhiteSpace(retStr))
        //    {
        //        doc.SaveAs2(fiDoc.FullName);
        //        doc.Close();
        //        wordApp.Quit();
        //        return retStr;
        //    }
        //    else
        //    {
        //        Range = doc.Range();
        //        Range.Start = 0;
        //        Range.End = 0;
        //        Range.Select();

        //        doc.SaveAs2(fiDoc.FullName);
        //        doc.Close();
        //        wordApp.Quit();
        //    }

        //    return "";
        //}
        #endregion Functions public
    }
}
