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
using CSSPDBDLL.Services.Resources;

namespace CSSPReportWriterHelperDLL.Services
{
    public partial class ReportBaseService
    {
        #region Functions public       
        public string CheckTagsAndContentOKWord(ReportTag reportTagStart, List<ReportTag> reportTagList)
        {
            string retStr = "";

            retStr = FindTagsWord(reportTagStart, reportTagList);
            if (!string.IsNullOrWhiteSpace(retStr))
                return retStr;

            retStr = CheckTagsPositionOKWord(reportTagList);
            if (!string.IsNullOrWhiteSpace(retStr))
                return retStr;

            foreach (ReportTag reportTag in reportTagList)
            {
                retStr = CheckTagsFirstLineWord(reportTag);
                if (!string.IsNullOrWhiteSpace(retStr))
                    return retStr;
            }

            retStr = FillTagsVariablesWord(reportTagList);
            if (!string.IsNullOrWhiteSpace(retStr))
                return retStr;

            retStr = FillTagsParentChildRelationshipWord(reportTagList);
            if (!string.IsNullOrWhiteSpace(retStr))
                return retStr;

            foreach (ReportTag reportTag in reportTagList.Where(c => c.ReportTagParent == null))
            {
                retStr = FillTagsLevelsWord(reportTag, 0);
                if (!string.IsNullOrWhiteSpace(retStr))
                    return retStr;
            }

            //retStr = CheckVariablesWord(reportTagList);
            //if (!string.IsNullOrWhiteSpace(retStr))
            //    return retStr;

            return "";
        }
        public string CheckTagsFirstLineWord(ReportTag reportTag)
        {
            int FirstLineCount = 3;
            Microsoft.Office.Interop.Word.Range tempRangeBeginning = reportTag.doc.Range().Duplicate;
            tempRangeBeginning.End = reportTag.doc.Range().Start;

            if (reportTag.RangeStartTag.Text.IndexOf("\r") == -1)
            {
                reportTag.Error = ReportServiceRes.CouldNotFindFirstLineReturnsAreRequired;
                tempRangeBeginning.Comments.Add(tempRangeBeginning, reportTag.Error);
                return reportTag.Error;
            }

            string FirstLine = reportTag.RangeStartTag.Text.Substring(0, reportTag.RangeStartTag.Text.IndexOf("\r"));

            List<string> strList = FirstLine.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

            if (strList.Count != FirstLineCount)
            {
                reportTag.Error = string.Format(ReportServiceRes._DoesNotContain_Items, FirstLine, FirstLineCount.ToString()) + "\r\n\r\n" +
                    string.Format(ReportServiceRes.Example_, Marker + "Start Root en");
                tempRangeBeginning.Comments.Add(tempRangeBeginning, reportTag.Error);
                return reportTag.Error;
            }

            reportTag.TagItem = strList[1];

            reportTag.ReportType = _ReportBase.GetReportType(reportTag.TagItem);
            if (reportTag.ReportType == null)
            {
                reportTag.Error = string.Format(ReportServiceRes.ItemName_NotAllowed, reportTag.TagItem) + "\r\n\r\n" +
                     string.Format(ReportServiceRes.AllowableValues_, _ReportBase.AllowableReportType());
                tempRangeBeginning.Comments.Add(tempRangeBeginning, reportTag.Error);
                return reportTag.Error;
            }

            reportTag.Language = strList[2];

            if (!(reportTag.Language == "en" || reportTag.Language == "fr"))
            {
                reportTag.Error = string.Format(ReportServiceRes.Language_NotAllowed, reportTag.Language) + "\r\n\r\n" +
                    string.Format(ReportServiceRes.AllowableLanguages_, "en, fr");
                tempRangeBeginning.Comments.Add(tempRangeBeginning, reportTag.Error);
                return reportTag.Error;
            }

            return "";
        }
        public string CheckTagsPositionOKWord(List<ReportTag> reportTagList)
        {
            for (int i = 0, count = reportTagList.Count; i < count; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    if (reportTagList[i].RangeEndTag.Start == reportTagList[j].RangeEndTag.Start)
                    {
                        string ErrStr = string.Format(ReportServiceRes.YouHaveATag_WhichIsNotClosedProperly, reportTagList[i].CSVTagText);
                        reportTagList[i].RangeStartTag.Comments.Add(reportTagList[i].RangeStartTag, ErrStr);
                        return ErrStr;
                    }
                }
            }

            foreach (ReportTag reportTagLoop in reportTagList.Where(c => c.TagName == "LoopStart"))
            {
                bool IsOutside = true;
                foreach (ReportTag reportTagStart in reportTagList.Where(c => c.TagName == "Start"))
                {
                    if (reportTagLoop.RangeStartTag.Start >= reportTagStart.RangeInnerTag.Start
                        && reportTagLoop.RangeStartTag.End <= reportTagStart.RangeInnerTag.End)
                    {
                        IsOutside = false;
                        continue;
                    }
                }

                if (IsOutside)
                {
                    string ErrStr = ReportServiceRes.AllTagsMustBeWithinAStartTag;
                    reportTagLoop.RangeStartTag.Comments.Add(reportTagLoop.RangeStartTag, ErrStr);
                    return ErrStr;
                }
            }

            return "";
        }
        public string CheckVariablesWord(List<ReportTag> reportTagList)
        {
            foreach (ReportTag reportTag in reportTagList)
            {
                string InnerText = reportTag.RangeInnerTag.Text;
                string Search = "|||" + reportTag.TagItem;
                int SearchLength = Search.Length;
                int PosStart = InnerText.IndexOf(Search, 0);
                int PosEnd = 0;
                while (PosStart != -1)
                {
                    PosEnd = InnerText.IndexOf(Marker, SearchLength + PosStart);

                    if (PosEnd == -1)
                        break;

                    string VarStr = InnerText.Substring(PosStart + Marker.Length, PosEnd - PosStart - Marker.Length);
                    if (!string.IsNullOrWhiteSpace(VarStr))
                        VarStr = VarStr.Trim();
                    List<string> strList = VarStr.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
                    ReportTreeNode reportTreeNode = new ReportTreeNode();
                    bool IsDBFiltering = false;
                    string retStr = GetReportTreeNodesForField(strList, reportTreeNode, reportTag.ReportType, IsDBFiltering, 0);
                    if (!string.IsNullOrWhiteSpace(retStr))
                    {
                        Microsoft.Office.Interop.Word.Range tempRange = reportTag.RangeInnerTag.Duplicate;
                        tempRange.Start = reportTag.RangeInnerTag.Start + PosStart;
                        tempRange.End = reportTag.RangeInnerTag.Start + PosEnd + Marker.Length;

                        string ErrorText = retStr;
                        tempRange.Comments.Add(tempRange, ErrorText);
                        return ErrorText;
                    }

                    PosStart = InnerText.IndexOf("|||" + reportTag.TagItem, PosEnd + 1);
                }
            }

            return "";
        }
        public string FillTagsLevelsWord(ReportTag reportTag, int CurrentLevel)
        {
            reportTag.Level = CurrentLevel;

            foreach (ReportTag reportTagChild in reportTag.ReportTagChildList)
            {
                FillTagsLevelsWord(reportTagChild, CurrentLevel + 1);
            }

            return "";
        }
        public string FillTagsParentChildRelationshipWord(List<ReportTag> reportTagList)
        {
            foreach (ReportTag reportTag in reportTagList)
            {
                // Getting Parent
                reportTag.ReportTagParent = (from c in reportTagList
                                             where c.Guid != reportTag.Guid
                                             && reportTag.RangeStartTag.Start >= c.RangeStartTag.End
                                             && reportTag.RangeEndTag.End <= c.RangeEndTag.Start
                                             orderby c.RangeStartTag.End descending
                                             select c).FirstOrDefault();


                var reList = (from c in reportTagList
                              where c.RangeStartTag != reportTag.RangeStartTag
                              && reportTag.RangeStartTag.Start >= c.RangeStartTag.End
                              && reportTag.RangeEndTag.End <= c.RangeEndTag.Start
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
            }

            return "";
        }
        public string FillTagsVariablesWord(List<ReportTag> reportTagList)
        {
            string retStr = "";
            foreach (ReportTag reportTag in reportTagList)
            {
                bool IsDBFiltering = true;
                retStr = GetReportTreeNodesFromTagText(reportTag.RangeStartTag.Text, reportTag.TagItem, reportTag.ReportType, reportTag.ReportTreeNodeList, IsDBFiltering);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    string ErrStr = retStr;
                    reportTag.RangeStartTag.Comments.Add(reportTag.RangeStartTag, ErrStr);
                    return ErrStr;
                }
            }

            return "";
        }
        public string FindTagsWord(ReportTag reportTag, List<ReportTag> reportTagList)
        {
            bool TopTag = reportTag.RangeInnerTag == null ? true : false;
            int MinRange = reportTag.RangeInnerTag == null ? reportTag.doc.Range().Start : reportTag.RangeInnerTag.Start;
            int MaxRange = reportTag.RangeInnerTag == null ? reportTag.doc.Range().End : reportTag.RangeInnerTag.End;
            string retStr = "";

            Microsoft.Office.Interop.Word.Range tempRange = reportTag.doc.Range().Duplicate;

            List<string> tagNameList = new List<string>() { "Start", "LoopStart", "ShowStart" };

            tempRange = reportTag.doc.Range().Duplicate;
            tempRange.Start = MinRange;
            tempRange.End = MaxRange;

            int TagNumber = -1;

            bool Found = true;
            while (Found)
            {
                int oldStart = tempRange.Start;

                tempRange.Find.ClearFormatting();
                tempRange.Find.MatchWildcards = true;
                string FindText = Marker + "*" + Marker;

                tempRange.Find.Execute(FindText: FindText, Forward: true);

                if (tempRange.Find.Found)
                {
                    tempRange.Select();
                    if (tempRange.Start >= MaxRange)
                        break;

                    if (oldStart > tempRange.Start)
                    {
                        if (tempRange.Tables.Count > 0)
                        {
                            tempRange.Start = oldStart;
                            tempRange.End = tempRange.Start;
                            tempRange.End = tempRange.Cells[1].Range.End - 1;

                            tempRange.Find.ClearFormatting();
                            tempRange.Find.MatchWildcards = true;
                            tempRange.Find.Execute(FindText: FindText, Forward: true);
                        }
                    }

                    if (tempRange.Text == null || !(tempRange.Text.StartsWith(Marker + "Start")
                        || tempRange.Text.StartsWith(Marker + "LoopStart")
                        || tempRange.Text.StartsWith(Marker + "ShowStart")))
                    {
                        continue;
                    }
                    else
                    {
                        if (tempRange.Text.StartsWith(Marker + "Start"))
                        {
                            TagNumber = 0;
                        }
                        else if (tempRange.Text.StartsWith(Marker + "LoopStart"))
                        {
                            TagNumber = 1;
                        }
                        else if (tempRange.Text.StartsWith(Marker + "ShowStart"))
                        {
                            TagNumber = 2;
                        }
                        else
                        {
                            // nothing
                        }
                    }

                    tempRange.End = tempRange.End + 1;

                    if (!tempRange.Text.EndsWith("\r"))
                    {
                        string ErrorText = string.Format(ReportServiceRes._TagMustBeFollowedWithAReturn, Marker + tagNameList[TagNumber] + "*" + Marker);
                        tempRange.Comments.Add(tempRange, ErrorText);
                        return ErrorText;
                    }

                    if (!tempRange.Text.StartsWith(Marker + tagNameList[TagNumber] + " "))
                    {
                        string ErrorText = string.Format(ReportServiceRes.Tag_NotWellFormedItShouldHaveAStructureLike_, tagNameList[TagNumber], Marker + tagNameList[TagNumber] + " Root en" + Marker);
                        tempRange.Comments.Add(tempRange, ErrorText);
                        return ErrorText;
                    }

                    string TagItem = tempRange.Text.Substring((Marker + tagNameList[TagNumber] + " ").Length);
                    TagItem = TagItem.Substring(0, TagItem.IndexOf(" "));

                    string Language = tempRange.Text.Substring((Marker + tagNameList[TagNumber] + " " + TagItem + " ").Length);
                    if (string.IsNullOrWhiteSpace(Language) || Language.Length < 3)
                    {
                        string ErrorText = string.Format(ReportServiceRes.Tag_NotWellFormedItShouldHaveAStructureLike_, tagNameList[TagNumber], Marker + tagNameList[TagNumber] + " Root en" + Marker);
                        tempRange.Comments.Add(tempRange, ErrorText);
                        return ErrorText;
                    }

                    Language = Language.Trim().Substring(0, 2);
                    if (!(Language == "en" || Language == "fr"))
                    {
                        string ErrorText = string.Format(ReportServiceRes.Tag_NotWellFormedItShouldHaveAStructureLike_, tagNameList[TagNumber], Marker + tagNameList[TagNumber] + " Root en" + Marker);
                        tempRange.Comments.Add(tempRange, ErrorText);
                        return ErrorText;
                    }

                    FindText = Marker + tagNameList[TagNumber].Replace("Start", "End") + " " + TagItem + " " + Language + Marker;

                    ReportTag reportTagChild = new ReportTag();
                    reportTagChild.wordApp = reportTag.wordApp;
                    reportTagChild.doc = reportTag.doc;
                    reportTagChild.RangeStartTag = reportTag.doc.Range().Duplicate;
                    reportTagChild.RangeStartTag.Start = tempRange.Start;
                    reportTagChild.RangeStartTag.End = tempRange.End;
                    reportTagChild.DocumentType = DocumentType.Word;
                    reportTagChild.OnlyImmediateChildren = reportTag.OnlyImmediateChildren;

                    if (tempRange.End < MaxRange)
                    {
                        tempRange.Start = tempRange.End;
                        tempRange.End = MaxRange;
                    }

                    reportTagChild.TagName = tagNameList[TagNumber];
                    reportTagChild.TagItem = TagItem;
                    reportTagChild.Take = reportTag.Take;
                    reportTagChild.UnderTVItemID = reportTag.UnderTVItemID;
                    reportTagChild.AppTaskID = reportTag.AppTaskID;
                    reportTagChild.DoingPart = reportTag.DoingPart;
                    reportTagChild.TotalPart = reportTag.TotalPart;

                    if (TopTag)
                    {
                        reportTagChild.ReportTagParent = null;
                        reportTagChild.Level = 0;
                    }
                    else
                    {
                        reportTagChild.ReportTagParent = reportTag;
                        reportTagChild.Level = reportTag.Level + 1;
                    }

                    tempRange.Find.Execute(FindText: FindText, Forward: true);

                    if (!tempRange.Find.Found)
                    {
                        string ErrorText = string.Format(ReportServiceRes.CouldNotFindEndTag_Of_, FindText, reportTagChild.RangeStartTag.Text);
                        reportTagChild.RangeStartTag.Comments.Add(reportTagChild.RangeStartTag, ErrorText);
                        return ErrorText;
                    }

                    tempRange.End = tempRange.End + 1;

                    if (!tempRange.Text.EndsWith("\r"))
                    {
                        string ErrorText = string.Format(ReportServiceRes._TagMustBeFollowedWithAReturn, FindText);
                        tempRange.Comments.Add(tempRange, ErrorText);
                        return ErrorText;
                    }

                    reportTagChild.RangeEndTag = reportTag.doc.Range().Duplicate;
                    reportTagChild.RangeEndTag.Start = tempRange.Start;
                    reportTagChild.RangeEndTag.End = tempRange.End;

                    reportTagChild.RangeInnerTag = reportTag.doc.Range().Duplicate;
                    if (reportTagChild.RangeStartTag.Tables.Count > 0)
                    {
                        if (reportTagChild.TagName == "Start")
                        {
                            string ErrorText = ReportServiceRes.StartTagMustNotResideInATable;
                            reportTagChild.RangeStartTag.Comments.Add(reportTagChild.RangeStartTag, ErrorText);
                            return ErrorText;
                        }
                        else if (reportTagChild.TagName == "LoopStart")
                        {
                            //if (TagItem == "Subsector_Special_Table")
                            //{
                            //    retStr = DoSubsectorSpecialTable(reportTagChild);
                            //    return retStr;
                            //}
                            reportTagChild.RangeInnerTag = reportTagChild.RangeStartTag.Tables[1].Rows[reportTagChild.RangeStartTag.Rows[1].Index + 1].Range;
                        }
                        else
                        {
                            reportTagChild.RangeInnerTag.Start = reportTagChild.RangeStartTag.End;
                            reportTagChild.RangeInnerTag.End = reportTagChild.RangeEndTag.Start;
                        }
                    }
                    else
                    {
                        reportTagChild.RangeInnerTag.Start = reportTagChild.RangeStartTag.End;
                        reportTagChild.RangeInnerTag.End = reportTagChild.RangeEndTag.Start;
                    }

                    retStr = CheckTagsFirstLineWord(reportTagChild);
                    if (!string.IsNullOrWhiteSpace(retStr))
                        return retStr;

                    bool IsDBFiltering = reportTagChild.TagName == "ShowStart" ? false : true;
                    retStr = GetReportTreeNodesFromTagText(reportTagChild.RangeStartTag.Text, reportTagChild.TagItem, reportTagChild.ReportType, reportTagChild.ReportTreeNodeList, IsDBFiltering);
                    if (!string.IsNullOrWhiteSpace(retStr))
                    {
                        reportTagChild.RangeStartTag.Comments.Add(reportTagChild.RangeStartTag, retStr);
                        return retStr;
                    }

                    //retStr = CheckTagsVariableBeingUsedWord(reportTagChild);
                    //if (!string.IsNullOrWhiteSpace(retStr))
                    //    return retStr;

                    //retStr = CheckTagsVariableNotDeclaredWord(reportTagChild);
                    //if (!string.IsNullOrWhiteSpace(retStr))
                    //    return retStr;


                    if (reportTagList.Count == 0 && !reportTag.OnlyImmediateChildren)
                    {
                        if (reportTagChild.TagName != tagNameList[0])
                        {
                            string ErrorText = ReportServiceRes.FirstTagInDocumentMustBeAStartTag;
                            tempRange.Comments.Add(tempRange, ErrorText);
                            return ErrorText;
                        }
                    }

                    reportTagList.Add(reportTagChild);

                    if (!reportTag.OnlyImmediateChildren)
                    {
                        retStr = FindTagsWord(reportTagChild, reportTagList);
                        if (!string.IsNullOrWhiteSpace(retStr))
                            return retStr;

                        tempRange.Start = reportTagChild.RangeEndTag.End;
                        tempRange.End = MaxRange;
                    }
                    else
                    {
                        tempRange.Start = reportTagChild.RangeEndTag.End;
                        tempRange.End = MaxRange;
                    }
                }
                else
                {
                    Found = false;
                }
            }

            return "";
        }
        public string FillTemplateWithDBInfoWord(ReportTag reportTag)
        {
            Microsoft.Office.Interop.Word.Range nextTagRange = reportTag.doc.Range();

            List<ReportTag> reportTagList = new List<ReportTag>();

            string retStr = FindTagsWord(reportTag, reportTagList);
            if (!string.IsNullOrWhiteSpace(retStr))
                return retStr;

            foreach (ReportTag reportTagStart in reportTagList.Where(c => c.ReportTagParent == null).OrderByDescending(c => c.RangeStartTag.Start))
            {
                retStr = GenerateNextReportTagWord(reportTagStart);
                if (!string.IsNullOrWhiteSpace(retStr))
                    return retStr;
            }

            nextTagRange = reportTag.doc.Range();
            nextTagRange.Start = 0;
            nextTagRange.End = 0;
            nextTagRange.Select();

            return "";
        }
        public string GenerateForReportTagWord<T>(ReportTag reportTag) where T : new()
        {
            string ErrStr = "";
            Microsoft.Office.Interop.Word.Range tempRange = reportTag.doc.Range();
            Microsoft.Office.Interop.Word.Range CurrentCopyInnerTagRange = reportTag.doc.Range();
            Microsoft.Office.Interop.Word.Range midRowRange = reportTag.doc.Range();
            Microsoft.Office.Interop.Word.Range newRowRange = reportTag.doc.Range();

            Microsoft.Office.Interop.Word.Table table = null;
            Microsoft.Office.Interop.Word.Row rowTemplate = null;
            Microsoft.Office.Interop.Word.Row beforeRow = null;

            try
            {
                table = reportTag.doc.Tables[1];
                rowTemplate = reportTag.doc.Tables[1].Rows[1];
                beforeRow = reportTag.doc.Tables[1].Rows[1];
            }
            catch (Exception)
            {
                // nothing
            }

            ReportModelDynamic reportModelDynamic = new ReportModelDynamic();
            string retStr = ReportGetDBOfType(reportTag, reportModelDynamic);
            if (!string.IsNullOrWhiteSpace(retStr))
            {
                CurrentCopyInnerTagRange.Comments.Add(CurrentCopyInnerTagRange, retStr);
                return retStr;
            }

            List<T> reportModelList = reportModelDynamic.ReportModel;

            if (reportTag.TagItem == "Subsector_Special_Table")
            {
                if (reportTag.RangeStartTag.Tables.Count > 0)
                {
                    ErrStr = string.Format(ReportServiceRes._TagShouldNotBeWithinATable, "|||LoopStart Subsector_Special_Table ...");
                    reportTag.RangeStartTag.Comments.Add(reportTag.RangeStartTag, ErrStr);
                    return ErrStr;
                }

                bool IsDBFiltering = true;
                retStr = GetReportTreeNodesFromTagText(reportTag.RangeStartTag.Text, reportTag.TagItem, reportTag.ReportType, reportTag.ReportTreeNodeList, IsDBFiltering);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    reportTag.RangeStartTag.Comments.Add(reportTag.RangeStartTag, retStr);
                    return retStr;
                }

                if (reportModelList.Count != 1)
                    return string.Format(ReportServiceRes._ShouldOnlyHave_Items, "reportModelList", "1");

                foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                {
                    if (propertyInfo.Name == reportTag.TagItem + "_Error")
                    {
                        ErrStr = (string)propertyInfo.GetValue(reportModelList[0]);
                        if (!string.IsNullOrWhiteSpace(ErrStr))
                        {
                            reportTag.RangeInnerTag.Comments.Add(reportTag.RangeStartTag, ErrStr);
                            return ErrStr;
                        }
                    }
                }

                ReportSubsector_Special_TableModel reportSubsector_Special_TableModel = reportModelList[0] as ReportSubsector_Special_TableModel;

                if (!string.IsNullOrWhiteSpace(reportSubsector_Special_TableModel.Subsector_Special_Table_Error))
                {
                    ErrStr = reportSubsector_Special_TableModel.Subsector_Special_Table_Error;
                    reportTag.RangeInnerTag.Comments.Add(reportTag.RangeStartTag, ErrStr);
                    return ErrStr;
                }

                string TitleText = reportTag.RangeInnerTag.Text;

                reportTag.RangeEndTag.Text = "";
                reportTag.RangeInnerTag.Text = "";
                reportTag.RangeStartTag.Text = "";

                List<DateTime> DateTimeSampleList = new List<DateTime>();
                List<string> MWQMSiteList = new List<string>();
                List<SubTypeAndLetter> MWQMSiteStatResultList = new List<SubTypeAndLetter>();
                List<string> TideList = new List<string>();
                List<float?> RainDay0List = new List<float?>();
                List<float?> RainDay1List = new List<float?>();
                List<float?> RainDay2List = new List<float?>();
                List<float?> RainDay3List = new List<float?>();
                List<float?> RainDay4List = new List<float?>();
                List<float?> RainDay5List = new List<float?>();
                List<float?> RainDay6List = new List<float?>();
                List<float?> RainDay7List = new List<float?>();
                List<float?> RainDay8List = new List<float?>();
                List<float?> RainDay9List = new List<float?>();
                List<float?> RainDay10List = new List<float?>();
                List<List<float?>> ParamValueList = new List<List<float?>>();
                List<string> tempList = new List<string>();

                //reportTag.wordApp.Selection.TypeText("Subsector_Special_Table_Error");
                //reportTag.wordApp.Selection.TypeParagraph();
                //reportTag.wordApp.Selection.TypeText(reportSubsector_Special_TableModel.Subsector_Special_Table_Error);
                //reportTag.wordApp.Selection.TypeParagraph();
                //reportTag.wordApp.Selection.TypeText("Subsector_Special_Table_Last_X_Runs");
                //reportTag.wordApp.Selection.TypeParagraph();
                //reportTag.wordApp.Selection.TypeText(reportSubsector_Special_TableModel.Subsector_Special_Table_Last_X_Runs.ToString());
                //reportTag.wordApp.Selection.TypeParagraph();
                //reportTag.wordApp.Selection.TypeText("Subsector_Special_Table_Type");
                //reportTag.wordApp.Selection.TypeParagraph();
                //reportTag.wordApp.Selection.TypeText(reportSubsector_Special_TableModel.Subsector_Special_Table_Type.ToString());
                //reportTag.wordApp.Selection.TypeParagraph();
                //reportTag.wordApp.Selection.TypeText("Subsector_Special_Table_MWQM_Site_Is_Active");
                //reportTag.wordApp.Selection.TypeParagraph();
                //reportTag.wordApp.Selection.TypeText(reportSubsector_Special_TableModel.Subsector_Special_Table_MWQM_Site_Is_Active.ToString());
                //reportTag.wordApp.Selection.TypeParagraph();
                //reportTag.wordApp.Selection.TypeText("Subsector_Special_Table_Number_Of_Samples_For_Stat_Max");
                //reportTag.wordApp.Selection.TypeParagraph();
                //reportTag.wordApp.Selection.TypeText(reportSubsector_Special_TableModel.Subsector_Special_Table_Number_Of_Samples_For_Stat_Max.ToString());
                //reportTag.wordApp.Selection.TypeParagraph();
                //reportTag.wordApp.Selection.TypeText("Subsector_Special_Table_Number_Of_Samples_For_Stat_Min");
                //reportTag.wordApp.Selection.TypeParagraph();
                //reportTag.wordApp.Selection.TypeText(reportSubsector_Special_TableModel.Subsector_Special_Table_Number_Of_Samples_For_Stat_Min.ToString());
                //reportTag.wordApp.Selection.TypeParagraph();
                //reportTag.wordApp.Selection.TypeText("Subsector_Special_Table_Highlight_Above_Min_Number");
                //reportTag.wordApp.Selection.TypeParagraph();
                //reportTag.wordApp.Selection.TypeText(reportSubsector_Special_TableModel.Subsector_Special_Table_Highlight_Above_Min_Number.ToString());
                //reportTag.wordApp.Selection.TypeParagraph();
                //reportTag.wordApp.Selection.TypeText("Subsector_Special_Table_Highlight_Below_Max_Number");
                //reportTag.wordApp.Selection.TypeParagraph();
                //reportTag.wordApp.Selection.TypeText(reportSubsector_Special_TableModel.Subsector_Special_Table_Highlight_Below_Max_Number.ToString());
                //reportTag.wordApp.Selection.TypeParagraph();
                //reportTag.wordApp.Selection.TypeText("Subsector_Special_Table_Show_Number_Of_Days_Of_Precipitation");
                //reportTag.wordApp.Selection.TypeParagraph();
                //reportTag.wordApp.Selection.TypeText(reportSubsector_Special_TableModel.Subsector_Special_Table_Show_Number_Of_Days_Of_Precipitation.ToString());
                //reportTag.wordApp.Selection.TypeParagraph();
                //reportTag.wordApp.Selection.TypeText("Subsector_Special_Table_Max_Number_Of_Sites_Per_Table_Part");
                //reportTag.wordApp.Selection.TypeParagraph();
                //reportTag.wordApp.Selection.TypeText(reportSubsector_Special_TableModel.Subsector_Special_Table_Max_Number_Of_Sites_Per_Table_Part.ToString());
                //reportTag.wordApp.Selection.TypeParagraph();
                //reportTag.wordApp.Selection.TypeText("Subsector_Special_Table_MWQM_Run_Date_List");
                //reportTag.wordApp.Selection.TypeParagraph();
                //reportTag.wordApp.Selection.TypeText(reportSubsector_Special_TableModel.Subsector_Special_Table_MWQM_Run_Date_List);
                //reportTag.wordApp.Selection.TypeParagraph();

                if (!string.IsNullOrWhiteSpace(reportSubsector_Special_TableModel.Subsector_Special_Table_MWQM_Run_Date_List))
                {
                    tempList = reportSubsector_Special_TableModel.Subsector_Special_Table_MWQM_Run_Date_List.Split("|".ToCharArray(), StringSplitOptions.None).ToList();
                }
                else
                {
                    tempList = new List<string>();
                }

                foreach (string s in tempList)
                {
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        if (s.Length != 8)
                        {
                            ErrStr = string.Format(ReportServiceRes.DateStringNotWellFormed_ItShouldBeSomethingLike_, s, "yyyymmdd");
                            reportTag.RangeStartTag.Comments.Add(reportTag.RangeStartTag, ErrStr);
                            return ErrStr;
                        }
                        int Year = 0;
                        int Month = 0;
                        int Day = 0;
                        if (!(int.TryParse(s.Substring(0, 4), out Year) && int.TryParse(s.Substring(4, 2), out Month) && int.TryParse(s.Substring(6, 2), out Day)))
                        {
                            ErrStr = string.Format(ReportServiceRes.DateStringNotWellFormed_ItShouldBeSomethingLike_, "yyyymmdd");
                            reportTag.RangeStartTag.Comments.Add(reportTag.RangeStartTag, ErrStr);
                            return ErrStr;
                        }
                        DateTimeSampleList.Add(new DateTime(Year, Month, Day));
                    }
                }

                //foreach (DateTime dt in DateTimeSampleList)
                //{
                //    reportTag.wordApp.Selection.TypeText(dt.ToLongDateString());
                //    reportTag.wordApp.Selection.TypeParagraph();
                //}

                //reportTag.wordApp.Selection.TypeParagraph();
                //reportTag.wordApp.Selection.TypeText("Subsector_Special_Table_MWQM_Site_Name_List");
                //reportTag.wordApp.Selection.TypeParagraph();
                //reportTag.wordApp.Selection.TypeText(reportSubsector_Special_TableModel.Subsector_Special_Table_MWQM_Site_Name_List);
                //reportTag.wordApp.Selection.TypeParagraph();

                if (!string.IsNullOrWhiteSpace(reportSubsector_Special_TableModel.Subsector_Special_Table_MWQM_Site_Name_List))
                {
                    tempList = reportSubsector_Special_TableModel.Subsector_Special_Table_MWQM_Site_Name_List.Split("|".ToCharArray(), StringSplitOptions.None).ToList();
                }
                else
                {
                    tempList = new List<string>();
                }

                foreach (string s in tempList)
                {
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        MWQMSiteList.Add(s);
                    }
                    else
                    {
                        //MWQMSiteList.Add("empty");
                    }
                }

                //foreach (string site in MWQMSiteList)
                //{
                //    reportTag.wordApp.Selection.TypeText(site);
                //    reportTag.wordApp.Selection.TypeParagraph();
                //}

                //reportTag.wordApp.Selection.TypeParagraph();
                //reportTag.wordApp.Selection.TypeText("Subsector_Special_Table_Stat_Letter_List");
                //reportTag.wordApp.Selection.TypeParagraph();
                //reportTag.wordApp.Selection.TypeText(reportSubsector_Special_TableModel.Subsector_Special_Table_Stat_Letter_List);
                //reportTag.wordApp.Selection.TypeParagraph();

                if (!string.IsNullOrWhiteSpace(reportSubsector_Special_TableModel.Subsector_Special_Table_Stat_Letter_List))
                {
                    tempList = reportSubsector_Special_TableModel.Subsector_Special_Table_Stat_Letter_List.Split("|".ToCharArray(), StringSplitOptions.None).ToList();
                }
                else
                {
                    tempList = new List<string>();
                }

                foreach (string s in tempList)
                {
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        TVTypeEnum subType = TVTypeEnum.Error;
                        string subTypeText = s.Substring(0, s.IndexOf("_"));
                        int subTypeInt = 0;
                        if (!int.TryParse(subTypeText, out subTypeInt))
                        {
                            ErrStr = string.Format(ReportServiceRes.SubTypeAndLetterStringNotWellFormed_ItShouldBeSomethingLike_, s, "34_C");
                            reportTag.RangeStartTag.Comments.Add(reportTag.RangeStartTag, ErrStr);
                            return ErrStr;
                        }

                        subType = (TVTypeEnum)subTypeInt;
                        string letter = s.Substring(s.IndexOf("_") + 1, 1);

                        SubTypeAndLetter subTypeAndLetter = new Services.SubTypeAndLetter();
                        subTypeAndLetter.SubType = subType;
                        subTypeAndLetter.Letter = letter;
                        MWQMSiteStatResultList.Add(subTypeAndLetter);
                    }
                    else
                    {
                        //MWQMSiteStatResultList.Add(new SubTypeAndLetter());
                    }
                }

                //foreach (SubTypeAndLetter subTypeAndLetter in MWQMSiteStatResultList)
                //{
                //    reportTag.wordApp.Selection.TypeText(subTypeAndLetter.SubType.ToString() + " " + subTypeAndLetter.Letter);
                //    reportTag.wordApp.Selection.TypeParagraph();
                //}

                //reportTag.wordApp.Selection.TypeParagraph();
                //reportTag.wordApp.Selection.TypeText("Subsector_Special_Table_Parameter_Value_List");
                //reportTag.wordApp.Selection.TypeParagraph();
                //reportTag.wordApp.Selection.TypeText(reportSubsector_Special_TableModel.Subsector_Special_Table_Parameter_Value_List);
                //reportTag.wordApp.Selection.TypeParagraph();

                foreach (string site in MWQMSiteList)
                {
                    int posStart = reportSubsector_Special_TableModel.Subsector_Special_Table_Parameter_Value_List.IndexOf("Start|" + site + "|") + ("Start|" + site + "|").Length;
                    int posEnd = reportSubsector_Special_TableModel.Subsector_Special_Table_Parameter_Value_List.IndexOf("|End|", posStart);
                    string SiteParamValueString = "";
                    if (posEnd != -1)
                        SiteParamValueString = reportSubsector_Special_TableModel.Subsector_Special_Table_Parameter_Value_List.Substring(posStart, posEnd - posStart);

                    List<float?> paramValueSiteList = new List<float?>();
                    if (!string.IsNullOrWhiteSpace(SiteParamValueString))
                    {
                        List<string> tempList2 = new List<string>();
                        if (!string.IsNullOrWhiteSpace(SiteParamValueString))
                        {
                            tempList2 = SiteParamValueString.Split("|".ToCharArray(), StringSplitOptions.None).ToList();
                        }
                        else
                        {
                            tempList2 = new List<string>();
                        }
                        foreach (string s2 in tempList2)
                        {
                            if (string.IsNullOrWhiteSpace(s2))
                            {
                                paramValueSiteList.Add(null);
                            }
                            else
                            {
                                float tempFloat = -1f;
                                if (!float.TryParse(s2, out tempFloat))
                                {
                                    tempFloat = -1f;
                                }
                                paramValueSiteList.Add(tempFloat);
                            }
                        }
                    }
                    ParamValueList.Add(paramValueSiteList);
                }

                //// Rain Day 0
                //if (!string.IsNullOrWhiteSpace(reportSubsector_Special_TableModel.Subsector_Special_Table_Rain_Day_0_Value_List))
                //{
                //    tempList = reportSubsector_Special_TableModel.Subsector_Special_Table_Rain_Day_0_Value_List.Split("|".ToCharArray(), StringSplitOptions.None).ToList().Take((int)reportSubsector_Special_TableModel.Subsector_Special_Table_Last_X_Runs).ToList();
                //}
                //else
                //{
                //    tempList = new List<string>();
                //}

                //foreach (string s in tempList)
                //{
                //    if (string.IsNullOrWhiteSpace(s))
                //    {
                //        PPTDay0List.Add(null);
                //    }
                //    else
                //    {
                //        float tempFloat = -1f;
                //        if (!float.TryParse(s, out tempFloat))
                //        {
                //            tempFloat = -1f;
                //        }
                //        PPTDay0List.Add(tempFloat);
                //    }
                //}

                //// Rain Day 1
                //if (!string.IsNullOrWhiteSpace(reportSubsector_Special_TableModel.Subsector_Special_Table_Rain_Day_1_Value_List))
                //{
                //    tempList = reportSubsector_Special_TableModel.Subsector_Special_Table_Rain_Day_1_Value_List.Split("|".ToCharArray(), StringSplitOptions.None).ToList().Take((int)reportSubsector_Special_TableModel.Subsector_Special_Table_Last_X_Runs).ToList();
                //}
                //else
                //{
                //    tempList = new List<string>();
                //}

                //foreach (string s in tempList)
                //{
                //    if (string.IsNullOrWhiteSpace(s))
                //    {
                //        PPTDay1List.Add(null);
                //    }
                //    else
                //    {
                //        float tempFloat = -1f;
                //        if (!float.TryParse(s, out tempFloat))
                //        {
                //            tempFloat = -1f;
                //        }
                //        PPTDay1List.Add(tempFloat);
                //    }
                //}

                //// Rain Day 2
                //if (!string.IsNullOrWhiteSpace(reportSubsector_Special_TableModel.Subsector_Special_Table_Rain_Day_2_Value_List))
                //{
                //    tempList = reportSubsector_Special_TableModel.Subsector_Special_Table_Rain_Day_2_Value_List.Split("|".ToCharArray(), StringSplitOptions.None).ToList().Take((int)reportSubsector_Special_TableModel.Subsector_Special_Table_Last_X_Runs).ToList();
                //}
                //else
                //{
                //    tempList = new List<string>();
                //}

                //foreach (string s in tempList)
                //{
                //    if (string.IsNullOrWhiteSpace(s))
                //    {
                //        PPTDay2List.Add(null);
                //    }
                //    else
                //    {
                //        float tempFloat = -1f;
                //        if (!float.TryParse(s, out tempFloat))
                //        {
                //            tempFloat = -1f;
                //        }
                //        PPTDay2List.Add(tempFloat);
                //    }
                //}

                //// Rain Day 3
                //if (!string.IsNullOrWhiteSpace(reportSubsector_Special_TableModel.Subsector_Special_Table_Rain_Day_3_Value_List))
                //{
                //    tempList = reportSubsector_Special_TableModel.Subsector_Special_Table_Rain_Day_3_Value_List.Split("|".ToCharArray(), StringSplitOptions.None).ToList().Take((int)reportSubsector_Special_TableModel.Subsector_Special_Table_Last_X_Runs).ToList();
                //}
                //else
                //{
                //    tempList = new List<string>();
                //}

                //foreach (string s in tempList)
                //{
                //    if (string.IsNullOrWhiteSpace(s))
                //    {
                //        PPTDay3List.Add(null);
                //    }
                //    else
                //    {
                //        float tempFloat = -1f;
                //        if (!float.TryParse(s, out tempFloat))
                //        {
                //            tempFloat = -1f;
                //        }
                //        PPTDay3List.Add(tempFloat);
                //    }
                //}

                //// Rain Day 4
                //if (!string.IsNullOrWhiteSpace(reportSubsector_Special_TableModel.Subsector_Special_Table_Rain_Day_4_Value_List))
                //{
                //    tempList = reportSubsector_Special_TableModel.Subsector_Special_Table_Rain_Day_4_Value_List.Split("|".ToCharArray(), StringSplitOptions.None).ToList().Take((int)reportSubsector_Special_TableModel.Subsector_Special_Table_Last_X_Runs).ToList();
                //}
                //else
                //{
                //    tempList = new List<string>();
                //}

                //foreach (string s in tempList)
                //{
                //    if (string.IsNullOrWhiteSpace(s))
                //    {
                //        PPTDay4List.Add(null);
                //    }
                //    else
                //    {
                //        float tempFloat = -1f;
                //        if (!float.TryParse(s, out tempFloat))
                //        {
                //            tempFloat = -1f;
                //        }
                //        PPTDay4List.Add(tempFloat);
                //    }
                //}

                //// Rain Day 5
                //if (!string.IsNullOrWhiteSpace(reportSubsector_Special_TableModel.Subsector_Special_Table_Rain_Day_5_Value_List))
                //{
                //    tempList = reportSubsector_Special_TableModel.Subsector_Special_Table_Rain_Day_5_Value_List.Split("|".ToCharArray(), StringSplitOptions.None).ToList().Take((int)reportSubsector_Special_TableModel.Subsector_Special_Table_Last_X_Runs).ToList();
                //}
                //else
                //{
                //    tempList = new List<string>();
                //}

                //foreach (string s in tempList)
                //{
                //    if (string.IsNullOrWhiteSpace(s))
                //    {
                //        PPTDay5List.Add(null);
                //    }
                //    else
                //    {
                //        float tempFloat = -1f;
                //        if (!float.TryParse(s, out tempFloat))
                //        {
                //            tempFloat = -1f;
                //        }
                //        PPTDay5List.Add(tempFloat);
                //    }
                //}

                //// Rain Day 6
                //if (!string.IsNullOrWhiteSpace(reportSubsector_Special_TableModel.Subsector_Special_Table_Rain_Day_6_Value_List))
                //{
                //    tempList = reportSubsector_Special_TableModel.Subsector_Special_Table_Rain_Day_6_Value_List.Split("|".ToCharArray(), StringSplitOptions.None).ToList().Take((int)reportSubsector_Special_TableModel.Subsector_Special_Table_Last_X_Runs).ToList();
                //}
                //else
                //{
                //    tempList = new List<string>();
                //}

                //foreach (string s in tempList)
                //{
                //    if (string.IsNullOrWhiteSpace(s))
                //    {
                //        PPTDay6List.Add(null);
                //    }
                //    else
                //    {
                //        float tempFloat = -1f;
                //        if (!float.TryParse(s, out tempFloat))
                //        {
                //            tempFloat = -1f;
                //        }
                //        PPTDay6List.Add(tempFloat);
                //    }
                //}

                //// Rain Day 7
                //if (!string.IsNullOrWhiteSpace(reportSubsector_Special_TableModel.Subsector_Special_Table_Rain_Day_7_Value_List))
                //{
                //    tempList = reportSubsector_Special_TableModel.Subsector_Special_Table_Rain_Day_7_Value_List.Split("|".ToCharArray(), StringSplitOptions.None).ToList().Take((int)reportSubsector_Special_TableModel.Subsector_Special_Table_Last_X_Runs).ToList();
                //}
                //else
                //{
                //    tempList = new List<string>();
                //}

                //foreach (string s in tempList)
                //{
                //    if (string.IsNullOrWhiteSpace(s))
                //    {
                //        PPTDay7List.Add(null);
                //    }
                //    else
                //    {
                //        float tempFloat = -1f;
                //        if (!float.TryParse(s, out tempFloat))
                //        {
                //            tempFloat = -1f;
                //        }
                //        PPTDay7List.Add(tempFloat);
                //    }
                //}

                //// Rain Day 8
                //if (!string.IsNullOrWhiteSpace(reportSubsector_Special_TableModel.Subsector_Special_Table_Rain_Day_8_Value_List))
                //{
                //    tempList = reportSubsector_Special_TableModel.Subsector_Special_Table_Rain_Day_8_Value_List.Split("|".ToCharArray(), StringSplitOptions.None).ToList().Take((int)reportSubsector_Special_TableModel.Subsector_Special_Table_Last_X_Runs).ToList();
                //}
                //else
                //{
                //    tempList = new List<string>();
                //}

                //foreach (string s in tempList)
                //{
                //    if (string.IsNullOrWhiteSpace(s))
                //    {
                //        PPTDay8List.Add(null);
                //    }
                //    else
                //    {
                //        float tempFloat = -1f;
                //        if (!float.TryParse(s, out tempFloat))
                //        {
                //            tempFloat = -1f;
                //        }
                //        PPTDay8List.Add(tempFloat);
                //    }
                //}

                //// Rain Day 9
                //if (!string.IsNullOrWhiteSpace(reportSubsector_Special_TableModel.Subsector_Special_Table_Rain_Day_9_Value_List))
                //{
                //    tempList = reportSubsector_Special_TableModel.Subsector_Special_Table_Rain_Day_9_Value_List.Split("|".ToCharArray(), StringSplitOptions.None).ToList().Take((int)reportSubsector_Special_TableModel.Subsector_Special_Table_Last_X_Runs).ToList();
                //}
                //else
                //{
                //    tempList = new List<string>();
                //}

                //foreach (string s in tempList)
                //{
                //    if (string.IsNullOrWhiteSpace(s))
                //    {
                //        PPTDay9List.Add(null);
                //    }
                //    else
                //    {
                //        float tempFloat = -1f;
                //        if (!float.TryParse(s, out tempFloat))
                //        {
                //            tempFloat = -1f;
                //        }
                //        PPTDay9List.Add(tempFloat);
                //    }
                //}

                //// Rain Day 10
                //if (!string.IsNullOrWhiteSpace(reportSubsector_Special_TableModel.Subsector_Special_Table_Rain_Day_10_Value_List))
                //{
                //    tempList = reportSubsector_Special_TableModel.Subsector_Special_Table_Rain_Day_10_Value_List.Split("|".ToCharArray(), StringSplitOptions.None).ToList().Take((int)reportSubsector_Special_TableModel.Subsector_Special_Table_Last_X_Runs).ToList();
                //}
                //else
                //{
                //    tempList = new List<string>();
                //}

                //foreach (string s in tempList)
                //{
                //    if (string.IsNullOrWhiteSpace(s))
                //    {
                //        PPTDay10List.Add(null);
                //    }
                //    else
                //    {
                //        float tempFloat = -1f;
                //        if (!float.TryParse(s, out tempFloat))
                //        {
                //            tempFloat = -1f;
                //        }
                //        PPTDay10List.Add(tempFloat);
                //    }
                //}


                if (!string.IsNullOrWhiteSpace(reportSubsector_Special_TableModel.Subsector_Special_Table_Tide_Value_List))
                {
                    tempList = reportSubsector_Special_TableModel.Subsector_Special_Table_Tide_Value_List.Split("|".ToCharArray(), StringSplitOptions.None).ToList().Take((int)reportSubsector_Special_TableModel.Subsector_Special_Table_Last_X_Runs).ToList();
                }
                else
                {
                    tempList = new List<string>();
                }

                foreach (string s in tempList)
                {
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        TideList.Add(s + " ");
                    }
                    else
                    {
                        TideList.Add("empty ");
                    }
                }

                if (reportSubsector_Special_TableModel.Subsector_Special_Table_Max_Number_Of_Sites_Per_Table_Part == null
                    || reportSubsector_Special_TableModel.Subsector_Special_Table_Max_Number_Of_Sites_Per_Table_Part == 0)
                {
                    ErrStr = string.Format(ReportServiceRes._ShouldNotBeNullOrEmpty, "Subsector_Special_Table_Max_Number_Of_Sites_Per_Table_Part");
                    reportTag.RangeStartTag.Comments.Add(reportTag.RangeStartTag, ErrStr);
                    return ErrStr;
                }

                if (reportSubsector_Special_TableModel.Subsector_Special_Table_Show_Number_Of_Days_Of_Precipitation == null)
                {
                    ErrStr = string.Format(ReportServiceRes._ShouldNotBeNullOrEmpty, "Subsector_Special_Table_Show_Number_Of_Days_Of_Precipitation");
                    reportTag.RangeStartTag.Comments.Add(reportTag.RangeStartTag, ErrStr);
                    return ErrStr;
                }

                int NumberOfDaysPrecipitation = (int)reportSubsector_Special_TableModel.Subsector_Special_Table_Show_Number_Of_Days_Of_Precipitation;

                if (NumberOfDaysPrecipitation > 5)
                {
                    ErrStr = string.Format(ReportServiceRes.MaximumNumberOfPrecipitationDaysIs_, "5");
                    reportTag.RangeStartTag.Comments.Add(reportTag.RangeStartTag, ErrStr);
                    return ErrStr;
                }

                int PartsForAllSitesNumber = MWQMSiteList.Count / (int)reportSubsector_Special_TableModel.Subsector_Special_Table_Max_Number_Of_Sites_Per_Table_Part +
                    (MWQMSiteList.Count % (int)reportSubsector_Special_TableModel.Subsector_Special_Table_Max_Number_Of_Sites_Per_Table_Part > 0 ? 1 : 0);
                int PartsForAllRunsNumber = (DateTimeSampleList.Count / 15 + (DateTimeSampleList.Count % 15 > 0 ? 1 : 0));
                if (PartsForAllRunsNumber == 0)
                    PartsForAllRunsNumber = 1;

                int TotalPart = PartsForAllSitesNumber * PartsForAllRunsNumber;

                int CurrentTablePartNumber = 0;
                Microsoft.Office.Interop.Word.Table newTable = null;
                Microsoft.Office.Interop.Word.Range rangeTable = reportTag.doc.Range(reportTag.RangeEndTag.End, reportTag.RangeEndTag.End);
                reportTag.TotalPart = PartsForAllRunsNumber * PartsForAllSitesNumber;
                reportTag.DoingPart = 0;
                for (int runNumber = 1; runNumber < PartsForAllRunsNumber + 1; runNumber++)
                {
                    for (int siteNumber = 1; siteNumber < PartsForAllSitesNumber + 1; siteNumber++)
                    {
                        if (newTable != null)
                        {
                            newTable.Select();
                            reportTag.wordApp.Selection.MoveDown(Microsoft.Office.Interop.Word.WdUnits.wdLine, 1);
                            reportTag.wordApp.Selection.InsertBreak(Microsoft.Office.Interop.Word.WdBreakType.wdPageBreak);
                            rangeTable = reportTag.wordApp.Selection.Range;
                        }

                        CurrentTablePartNumber += 1;

                        ////////////////////////////////////////////////////////////////////
                        ////     Start Creating Table /////////////
                        ////////////////////////////////////////////////////////////////////
                        float margin = 40.0f;
                        reportTag.doc.PageSetup.LeftMargin = margin;
                        reportTag.doc.PageSetup.RightMargin = margin + 10;

                        int TitleRowCount = 3;
                        int SiteRowCount = 0;
                        if (siteNumber == 1)
                        {
                            SiteRowCount = Math.Min(MWQMSiteList.Count, (int)reportSubsector_Special_TableModel.Subsector_Special_Table_Max_Number_Of_Sites_Per_Table_Part);
                        }
                        else
                        {
                            int NextSitesCount = MWQMSiteList.Count - ((siteNumber - 1) * (int)reportSubsector_Special_TableModel.Subsector_Special_Table_Max_Number_Of_Sites_Per_Table_Part);
                            if (NextSitesCount > 0)
                            {
                                SiteRowCount = Math.Min(NextSitesCount, (int)reportSubsector_Special_TableModel.Subsector_Special_Table_Max_Number_Of_Sites_Per_Table_Part);
                            }
                        }
                        int TideRowCount = 3;
                        int PrecRowNumber = (NumberOfDaysPrecipitation == 0 ? 0 : NumberOfDaysPrecipitation + 1);
                        newTable = rangeTable.Tables.Add(rangeTable,
                            TitleRowCount + SiteRowCount + TideRowCount + PrecRowNumber, 16,
                            Microsoft.Office.Interop.Word.WdDefaultTableBehavior.wdWord9TableBehavior,
                            Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitFixed);

                        newTable.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                        newTable.Range.Font.Size = 8;
                        newTable.Range.Font.Name = "Calibri";
                        newTable.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone;
                        newTable.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone;
                        newTable.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone;
                        newTable.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone;
                        newTable.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderHorizontal].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone;
                        newTable.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone;
                        newTable.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderDiagonalDown].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone;
                        newTable.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderDiagonalUp].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone;

                        newTable.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                        newTable.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                        newTable.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                        newTable.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

                        // Title
                        newTable.Columns[1].Width = 50;
                        newTable.Cell(1, 1).Merge(newTable.Cell(1, 16));
                        newTable.Cell(1, 1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        newTable.Cell(1, 1).Range.Text = TitleText.Replace("|||PartCount|||", CurrentTablePartNumber.ToString()).Replace("|||PartTotalCount|||", TotalPart.ToString());
                        newTable.Cell(1, 1).Range.Font.Size = 11;
                        newTable.Cell(1, 1).Range.Bold = 1;
                        newTable.Cell(1, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone;
                        newTable.Cell(1, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone;
                        newTable.Cell(1, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone;
                        newTable.Cell(1, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone;

                        // Site
                        newTable.Cell(2, 1).Merge(newTable.Cell(3, 1));
                        newTable.Cell(2, 1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        newTable.Cell(2, 1).VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                        newTable.Cell(2, 1).Range.Text = ReportServiceRes.Site;
                        newTable.Cell(2, 1).Range.Bold = 1;
                        newTable.Cell(2, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                        newTable.Cell(2, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                        newTable.Cell(2, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                        newTable.Cell(2, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

                        newTable.Cell(2 + SiteRowCount + 2, 1).Range.Text = ReportServiceRes.StartAndEndTides;
                        newTable.Cell(2 + SiteRowCount + 2, 1).Merge(newTable.Cell(2 + SiteRowCount + 2, 16));

                        newTable.Cell(2 + SiteRowCount + 2, 1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        newTable.Cell(2 + SiteRowCount + 2, 1).Range.Bold = 1;
                        newTable.Cell(2 + SiteRowCount + 2, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                        newTable.Cell(2 + SiteRowCount + 2, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                        newTable.Cell(2 + SiteRowCount + 2, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                        newTable.Cell(2 + SiteRowCount + 2, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

                        newTable.Cell(2 + SiteRowCount + 3, 1).Range.Text = ReportServiceRes.Start;
                        newTable.Cell(2 + SiteRowCount + 3, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                        newTable.Cell(2 + SiteRowCount + 4, 1).Range.Text = ReportServiceRes.End;
                        newTable.Cell(2 + SiteRowCount + 4, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

                        if (PrecRowNumber > 0)
                        {
                            newTable.Cell(2 + SiteRowCount + 5, 1).Range.Text = ReportServiceRes.Rain_mm;
                            newTable.Cell(2 + SiteRowCount + 5, 1).Merge(newTable.Cell(2 + SiteRowCount + 5, 16));
                            newTable.Cell(2 + SiteRowCount + 5, 1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            newTable.Cell(2 + SiteRowCount + 5, 1).Range.Bold = 1;
                            newTable.Cell(2 + SiteRowCount + 5, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                            newTable.Cell(2 + SiteRowCount + 5, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                            newTable.Cell(2 + SiteRowCount + 5, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                            newTable.Cell(2 + SiteRowCount + 5, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                        }
                        if (PrecRowNumber > 1)
                        {
                            newTable.Cell(2 + SiteRowCount + 6, 1).Range.Text = ReportServiceRes.Day1;
                            newTable.Cell(2 + SiteRowCount + 6, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                        }
                        if (PrecRowNumber > 2)
                        {
                            newTable.Cell(2 + SiteRowCount + 7, 1).Range.Text = ReportServiceRes.Day2;
                            newTable.Cell(2 + SiteRowCount + 7, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                        }
                        if (PrecRowNumber > 3)
                        {
                            newTable.Cell(2 + SiteRowCount + 8, 1).Range.Text = ReportServiceRes.Day3;
                            newTable.Cell(2 + SiteRowCount + 8, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                        }
                        if (PrecRowNumber > 4)
                        {
                            newTable.Cell(2 + SiteRowCount + 9, 1).Range.Text = ReportServiceRes.Day4;
                            newTable.Cell(2 + SiteRowCount + 9, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                        }
                        if (PrecRowNumber > 5)
                        {
                            newTable.Cell(2 + SiteRowCount + 10, 1).Range.Text = ReportServiceRes.Day5;
                            newTable.Cell(2 + SiteRowCount + 10, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                        }

                        int StartYearColumn = 2;
                        int OldYear = 0;
                        int NumberOfYear = 0;
                        int StartRunNumber = ((runNumber - 1) * 15);
                        int EndRunNumber = Math.Min(DateTimeSampleList.Count, StartRunNumber + 15);
                        for (int i = StartRunNumber; i < EndRunNumber; i++)
                        {
                            if (OldYear != DateTimeSampleList[i].Year)
                            {
                                newTable.Cell(2, StartYearColumn).Range.Text = DateTimeSampleList[i].Year.ToString();
                                newTable.Cell(2, StartYearColumn).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                                newTable.Cell(2, StartYearColumn).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                                newTable.Cell(2, StartYearColumn).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                                newTable.Cell(2, StartYearColumn).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                                StartYearColumn += 1;
                                OldYear = DateTimeSampleList[i].Year;
                                NumberOfYear += 1;
                            }
                            else
                            {
                                newTable.Cell(2, StartYearColumn - 1).Merge(newTable.Cell(2, StartYearColumn));
                            }
                        }

                        int Col = -1;
                        for (int i = StartRunNumber; i < EndRunNumber; i++)
                        {
                            Col += 1;
                            newTable.Cell(3, Col + 2).Range.Text = DateTimeSampleList[i].ToString("MMM") + "\r\n" + DateTimeSampleList[i].ToString("dd");
                            newTable.Cell(3, Col + 2).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                            newTable.Cell(3, Col + 2).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                            newTable.Cell(3, Col + 2).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                            newTable.Cell(3, Col + 2).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

                            if (TideList[i].Trim().Length == 5)
                            {
                                newTable.Cell(3 + SiteRowCount + 2, Col + 2).Range.Text = "--"; // TideList[i].Substring(0, 2);
                                newTable.Cell(3 + SiteRowCount + 3, Col + 2).Range.Text = "--"; // TideList[i].Substring(3, 2);
                            }
                            else
                            {
                                if (TideList[i].Trim().Length == 3)
                                {
                                    newTable.Cell(3 + SiteRowCount + 2, Col + 2).Range.Text = GetTideText2Letter(((TideTextEnum)int.Parse(TideList[i].Substring(0, 1))));
                                    newTable.Cell(3 + SiteRowCount + 3, Col + 2).Range.Text = GetTideText2Letter(((TideTextEnum)int.Parse(TideList[i].Substring(2, 1))));
                                }
                            }
                            if (NumberOfDaysPrecipitation > 0)
                            {
                                newTable.Cell(3 + SiteRowCount + 5, Col + 2).Range.Text = (RainDay1List[i] == -1 ? "" : RainDay1List[i].ToString());
                            }
                            if (NumberOfDaysPrecipitation > 1)
                            {
                                newTable.Cell(3 + SiteRowCount + 6, Col + 2).Range.Text = (RainDay2List[i] == -1 ? "" : RainDay2List[i].ToString());
                            }
                            if (NumberOfDaysPrecipitation > 2)
                            {
                                newTable.Cell(3 + SiteRowCount + 7, Col + 2).Range.Text = (RainDay3List[i] == -1 ? "" : RainDay3List[i].ToString());
                            }
                            if (NumberOfDaysPrecipitation > 3)
                            {
                                newTable.Cell(3 + SiteRowCount + 8, Col + 2).Range.Text = (RainDay4List[i] == -1 ? "" : RainDay4List[i].ToString());
                            }
                            if (NumberOfDaysPrecipitation > 4)
                            {
                                newTable.Cell(3 + SiteRowCount + 9, Col + 2).Range.Text = (RainDay5List[i] == -1 ? "" : RainDay5List[i].ToString());
                            }
                        }

                        for (int i = 0; i < NumberOfYear; i++)
                        {
                            newTable.Cell(2, i + 2).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            newTable.Cell(2, i + 2).Range.Bold = 1;
                        }

                        Col = -1;
                        for (int i = StartRunNumber; i < EndRunNumber; i++)
                        {
                            Col += 1;
                            newTable.Cell(3, Col + 2).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            newTable.Cell(3, Col + 2).Range.Bold = 1;
                        }

                        int StartSiteNumber = ((siteNumber - 1) * (int)reportSubsector_Special_TableModel.Subsector_Special_Table_Max_Number_Of_Sites_Per_Table_Part);
                        int EndSiteNumber = Math.Min(MWQMSiteList.Count, StartSiteNumber + (int)reportSubsector_Special_TableModel.Subsector_Special_Table_Max_Number_Of_Sites_Per_Table_Part);
                        int Row = -1;
                        for (int i = StartSiteNumber, count = EndSiteNumber; i < count; i++)
                        {
                            if (i % 10 == 0)
                            {
                                int PercentCompletedRow = (int)Math.Max(100f * (((float)reportTag.DoingPart / (float)reportTag.TotalPart) + ((1f / (float)reportTag.TotalPart) * (float)Row / (float)EndSiteNumber)), 3f);
                                retStr = UpdateAppTaskPercentCompleted(reportTag.AppTaskID, PercentCompletedRow);
                            }

                            Row += 1;
                            newTable.Cell(Row + 4, 1).Range.Text = MWQMSiteList[i]; // + " (" + MWQMSiteStatResultList[i].Letter + ")";
                            //if (MWQMSiteStatResultList[i].SubType == TVTypeEnum.NoDepuration)
                            //{
                            //    newTable.Cell(Row + 4, 1).Range.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorPlum;
                            //}
                            //else if (MWQMSiteStatResultList[i].SubType == TVTypeEnum.Failed)
                            //{
                            //    newTable.Cell(Row + 4, 1).Range.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorRed;
                            //}
                            //else if (MWQMSiteStatResultList[i].SubType == TVTypeEnum.Passed)
                            //{
                            //    newTable.Cell(Row + 4, 1).Range.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorLightGreen;
                            //}
                            //else if (MWQMSiteStatResultList[i].SubType == TVTypeEnum.NoData)
                            //{
                            //    newTable.Cell(Row + 4, 1).Range.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray10;
                            //}
                            //else if (MWQMSiteStatResultList[i].SubType == TVTypeEnum.LessThan10)
                            //{
                            //    newTable.Cell(Row + 4, 1).Range.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray20;
                            //}
                            newTable.Cell(Row + 4, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                            if ((Row + 1) % 5 == 0)
                            {
                                newTable.Cell(Row + 4, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                            }

                            Col = -1;
                            for (int v = StartRunNumber; v < EndRunNumber; v++)
                            {
                                Col += 1;
                                if (ParamValueList[i][v] == null || ParamValueList[i][v] == -1)
                                {
                                    newTable.Cell(Row + 4, Col + 2).Range.Text = "";
                                }
                                else if (ParamValueList[i][v] == 1)
                                {
                                    newTable.Cell(Row + 4, Col + 2).Range.Text = "<2";
                                    newTable.Cell(Row + 4, Col + 2).WordWrap = false;
                                }
                                else if (ParamValueList[i][v] == 1700)
                                {
                                    newTable.Cell(Row + 4, Col + 2).Range.Text = ">1600";
                                    newTable.Cell(Row + 4, Col + 2).WordWrap = false;
                                }
                                else
                                {
                                    newTable.Cell(Row + 4, Col + 2).Range.Text = ((float)ParamValueList[i][v]).ToString("F0");
                                }
                                if (ParamValueList[i][v] != null
                                    && ParamValueList[i][v] > reportSubsector_Special_TableModel.Subsector_Special_Table_Highlight_Above_Min_Number
                                    && ParamValueList[i][v] < reportSubsector_Special_TableModel.Subsector_Special_Table_Highlight_Below_Max_Number)
                                {
                                    newTable.Cell(Row + 4, Col + 2).Range.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorYellow;
                                }
                                if ((Row + 1) % 5 == 0)
                                {
                                    newTable.Cell(Row + 4, Col + 2).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                                }
                            }

                            ////////////////////////////////////////////////////////////////////
                            ////     End Creating Table /////////////
                            ////////////////////////////////////////////////////////////////////
                        }

                        reportTag.DoingPart += 1;
                        int PercentCompleted = (int)Math.Max(100f * ((float)reportTag.DoingPart / (float)reportTag.TotalPart), 10f);
                        retStr = UpdateAppTaskPercentCompleted(reportTag.AppTaskID, PercentCompleted);
                    }
                }

                return "";
            }
            else
            {
                reportTag.TotalPart += reportModelList.Count;
                for (int rmCount = reportModelList.Count - 1; rmCount > -1; rmCount--)
                {
                    CurrentCopyInnerTagRange.Start = reportTag.RangeInnerTag.Start;
                    CurrentCopyInnerTagRange.End = reportTag.RangeInnerTag.End;

                    CurrentCopyInnerTagRange.Select();

                    foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                    {
                        if (propertyInfo.Name == reportTag.TagItem + "_Error")
                        {
                            ErrStr = (string)propertyInfo.GetValue(reportModelList[rmCount]);
                            if (!string.IsNullOrWhiteSpace(ErrStr))
                            {
                                reportTag.RangeInnerTag.Comments.Add(reportTag.RangeStartTag, ErrStr);
                                return ErrStr;
                            }
                        }
                    }

                    if (reportTag.TagName != "Start")
                    {
                        if (reportTag.RangeInnerTag.Tables.Count > 0)
                        {
                            midRowRange.Start = (int)((reportTag.RangeInnerTag.Start + reportTag.RangeInnerTag.End) / 2);
                            midRowRange.End = midRowRange.Start + 1;
                            rowTemplate = midRowRange.Rows[1];
                            rowTemplate.Select();
                            table = midRowRange.Tables[1];
                            beforeRow = table.Rows[rowTemplate.Index + 1];
                            newRowRange = rowTemplate.Range;
                            newRowRange.Start = rowTemplate.Range.End;
                            newRowRange.End = rowTemplate.Range.End;
                            newRowRange.Select();
                            newRowRange.FormattedText = rowTemplate.Range.FormattedText;
                            newRowRange.Select();
                            CurrentCopyInnerTagRange.Start = newRowRange.Start;
                            CurrentCopyInnerTagRange.End = newRowRange.End;
                        }
                        else
                        {
                            CurrentCopyInnerTagRange.Start = reportTag.RangeEndTag.End;
                            CurrentCopyInnerTagRange.End = CurrentCopyInnerTagRange.Start;
                            CurrentCopyInnerTagRange.FormattedText = reportTag.RangeInnerTag.FormattedText;
                            CurrentCopyInnerTagRange.End = CurrentCopyInnerTagRange.Start + reportTag.RangeInnerTag.FormattedText.Text.Length;
                        }

                        CurrentCopyInnerTagRange.Select();
                    }
                    else
                    {
                        reportTag.RangeStartTag.Text = "";
                        reportTag.RangeEndTag.Text = "";
                    }

                    for (int i = 0, count = reportTag.ReportTreeNodeList.Count(); i < count; i++)
                    {
                        foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                        {
                            if (propertyInfo.Name == reportTag.ReportTreeNodeList[i].Text)
                            {
                                if (propertyInfo.GetValue(reportModelList[rmCount]) == null)
                                {

                                    bool Found = true;
                                    while (Found)
                                    {
                                        tempRange.Start = CurrentCopyInnerTagRange.Start;
                                        tempRange.End = CurrentCopyInnerTagRange.End;

                                        tempRange.Select();

                                        tempRange.Find.ClearFormatting();
                                        tempRange.Find.MatchWildcards = true;
                                        string FindText = Marker + propertyInfo.Name + "*" + Marker;
                                        tempRange.Find.Execute(FindText: FindText, Forward: true); // + "\r");

                                        if (!tempRange.Find.Found)
                                            break;

                                        tempRange.Select();
                                        tempRange.Text = "empty";
                                    }
                                }
                                else
                                {
                                    bool Found = true;
                                    while (Found)
                                    {
                                        tempRange.Start = CurrentCopyInnerTagRange.Start;
                                        tempRange.End = CurrentCopyInnerTagRange.End;

                                        tempRange.Select();

                                        tempRange.Find.ClearFormatting();
                                        tempRange.Find.MatchWildcards = true;
                                        string FindText = Marker + propertyInfo.Name + "*" + Marker;
                                        tempRange.Find.Execute(FindText: FindText, Forward: true); // + "\r");

                                        if (!tempRange.Find.Found)
                                            break;

                                        tempRange.Select();
                                        //string InnerText = tempRange.Text.Substring((Marker + propertyInfo.Name).Length);
                                        //InnerText = InnerText.Substring(0, InnerText.Length - Marker.Length);

                                        retStr = (string)ReportGetFieldTextOrValue<T>(reportModelList[rmCount], true, propertyInfo, tempRange.Text, reportTag, reportTag.ReportTreeNodeList[i]);
                                        if (!string.IsNullOrWhiteSpace(reportTag.Error))
                                        {
                                            ErrStr = reportTag.Error;
                                            tempRange.Comments.Add(tempRange, ErrStr);
                                            return ErrStr;
                                        }

                                        tempRange.Text = retStr;
                                    }
                                }
                            }
                        }
                    }

                    foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                    {
                        if (propertyInfo.Name == reportTag.TagItem + "_ID")
                        {
                            reportTag.UnderTVItemID = (int)propertyInfo.GetValue(reportModelList[rmCount]);
                            break;
                        }
                    }

                    CurrentCopyInnerTagRange.Select();

                    List<ReportTag> reportTagChildrenList = new List<ReportTag>();

                    ReportTag reportTagInner = new ReportTag()
                    {
                        wordApp = reportTag.wordApp,
                        doc = reportTag.doc,
                        OnlyImmediateChildren = true,
                        UnderTVItemID = reportTag.UnderTVItemID,
                        Take = reportTag.Take,
                        RangeInnerTag = CurrentCopyInnerTagRange,
                    };

                    retStr = FindTagsWord(reportTagInner, reportTagChildrenList);
                    if (!string.IsNullOrWhiteSpace(retStr))
                    {
                        ErrStr = retStr;
                        tempRange.Comments.Add(tempRange, ErrStr);
                        return ErrStr;
                    }

                    for (int tagCount = reportTagChildrenList.Count - 1; tagCount > -1; tagCount--)
                    {
                        reportTagChildrenList[tagCount].ReportTagParent = reportTag;
                        reportTagChildrenList[tagCount].AppTaskID = reportTag.AppTaskID;
                        reportTagChildrenList[tagCount].TotalPart = reportTag.TotalPart;
                        reportTagChildrenList[tagCount].DoingPart = reportTag.DoingPart;
                        reportTagChildrenList[tagCount].RangeInnerTag.Select();

                        if (reportTagChildrenList[tagCount].TagName == "ShowStart")
                        {
                            bool IsDBFiltering = false;
                            bool KeepShow = true;
                            for (int j = 0, countShow = reportTagChildrenList[tagCount].ReportTreeNodeList.Count(); j < countShow; j++)
                            {
                                KeepShow = ReturnKeepShow(reportModelList[rmCount], typeof(T), reportTagChildrenList[tagCount].ReportTreeNodeList[j], IsDBFiltering);

                                if (!string.IsNullOrWhiteSpace(reportTagChildrenList[tagCount].ReportTreeNodeList[j].Error))
                                {
                                    ErrStr = reportTagChildrenList[tagCount].ReportTreeNodeList[j].Error;
                                    tempRange.Comments.Add(tempRange, ErrStr);
                                    return ErrStr;
                                }
                            }

                            if (KeepShow)
                            {
                                reportTagChildrenList[tagCount].RangeEndTag.Text = "";
                                reportTagChildrenList[tagCount].RangeStartTag.Text = "";
                            }
                            else
                            {
                                reportTagChildrenList[tagCount].RangeEndTag.Start = reportTagChildrenList[tagCount].RangeStartTag.Start;
                                reportTagChildrenList[tagCount].RangeEndTag.Text = "";
                            }
                        }
                        else
                        {
                            retStr = GenerateNextReportTagWord(reportTagChildrenList[tagCount]);
                            if (!string.IsNullOrWhiteSpace(retStr))
                            {
                                ErrStr = retStr;
                                tempRange.Comments.Add(tempRange, ErrStr);
                                return ErrStr;
                            }
                        }
                    }

                    reportTag.DoingPart += 1;
                    int PercentCompleted = (int)Math.Max(100f * ((float)reportTag.DoingPart / (float)reportTag.TotalPart), 3f);
                    retStr = UpdateAppTaskPercentCompleted(reportTag.AppTaskID, PercentCompleted);
                }

                if (!(reportTag.TagName == "Start"))
                {
                    if (reportTag.RangeStartTag.Tables.Count > 0)
                    {
                        reportTag.RangeStartTag.Rows[1].Delete();
                        reportTag.RangeEndTag.Start = reportTag.RangeEndTag.End - 1;
                        reportTag.RangeEndTag.Rows[1].Delete();
                        reportTag.RangeInnerTag.Start = reportTag.RangeInnerTag.End - 1;
                        reportTag.RangeInnerTag.Rows[1].Delete();
                    }
                    else
                    {
                        reportTag.RangeStartTag.Text = "";
                        reportTag.RangeEndTag.Text = "";
                        reportTag.RangeInnerTag.Text = "";
                    }
                }
            }

            return "";

        }

        private string GetTideText2Letter(TideTextEnum tideText)
        {
            string TwoLetterTideText = "EE";
            switch (tideText)
            {
                case TideTextEnum.LowTide:
                    TwoLetterTideText = "LT";
                    break;
                case TideTextEnum.LowTideFalling:
                    TwoLetterTideText = "LF";
                    break;
                case TideTextEnum.LowTideRising:
                    TwoLetterTideText = "LR";
                    break;
                case TideTextEnum.MidTide:
                    TwoLetterTideText = "MT";
                    break;
                case TideTextEnum.MidTideFalling:
                    TwoLetterTideText = "MF";
                    break;
                case TideTextEnum.MidTideRising:
                    TwoLetterTideText = "MR";
                    break;
                case TideTextEnum.HighTide:
                    TwoLetterTideText = "HT";
                    break;
                case TideTextEnum.HighTideFalling:
                    TwoLetterTideText = "HF";
                    break;
                case TideTextEnum.HighTideRising:
                    TwoLetterTideText = "HR";
                    break;
                default:
                    break;
            }

            return TwoLetterTideText;
        }

        public string GenerateNextReportTagWord(ReportTag ReportTag)
        {
            string retStr = "";
            switch (ReportTag.TagItem)
            {
                case "Root":
                    retStr = GenerateForReportTagWord<ReportRootModel>(ReportTag);
                    break;
                case "Country":
                    retStr = GenerateForReportTagWord<ReportCountryModel>(ReportTag);
                    break;
                case "Province":
                    retStr = GenerateForReportTagWord<ReportProvinceModel>(ReportTag);
                    break;
                case "Area":
                    retStr = GenerateForReportTagWord<ReportAreaModel>(ReportTag);
                    break;
                case "Sector":
                    retStr = GenerateForReportTagWord<ReportSectorModel>(ReportTag);
                    break;
                case "Subsector":
                    retStr = GenerateForReportTagWord<ReportSubsectorModel>(ReportTag);
                    break;
                case "MWQM_Site":
                    retStr = GenerateForReportTagWord<ReportMWQM_SiteModel>(ReportTag);
                    break;
                case "MWQM_Site_Sample":
                    retStr = GenerateForReportTagWord<ReportMWQM_Site_SampleModel>(ReportTag);
                    break;
                case "MWQM_Site_Start_And_End_Date":
                    retStr = GenerateForReportTagWord<ReportMWQM_Site_Start_And_End_DateModel>(ReportTag);
                    break;
                case "MWQM_Site_File":
                    retStr = GenerateForReportTagWord<ReportMWQM_Site_FileModel>(ReportTag);
                    break;
                case "MWQM_Run":
                    retStr = GenerateForReportTagWord<ReportMWQM_RunModel>(ReportTag);
                    break;
                case "MWQM_Run_Sample":
                    retStr = GenerateForReportTagWord<ReportMWQM_Run_SampleModel>(ReportTag);
                    break;
                case "MWQM_Run_Lab_Sheet":
                    retStr = GenerateForReportTagWord<ReportMWQM_Run_Lab_SheetModel>(ReportTag);
                    break;
                case "MWQM_Run_Lab_Sheet_Detail":
                    retStr = GenerateForReportTagWord<ReportMWQM_Run_Lab_Sheet_DetailModel>(ReportTag);
                    break;
                case "MWQM_Run_Lab_Sheet_Tube_And_MPN_Detail":
                    retStr = GenerateForReportTagWord<ReportMWQM_Run_Lab_Sheet_Tube_And_MPN_DetailModel>(ReportTag);
                    break;
                case "MWQM_Run_File":
                    retStr = GenerateForReportTagWord<ReportMWQM_Run_FileModel>(ReportTag);
                    break;
                case "Pol_Source_Site":
                    retStr = GenerateForReportTagWord<ReportPol_Source_SiteModel>(ReportTag);
                    break;
                case "Pol_Source_Site_Obs":
                    retStr = GenerateForReportTagWord<ReportPol_Source_Site_ObsModel>(ReportTag);
                    break;
                case "Pol_Source_Site_Obs_Issue":
                    retStr = GenerateForReportTagWord<ReportPol_Source_Site_Obs_IssueModel>(ReportTag);
                    break;
                case "Pol_Source_Site_File":
                    retStr = GenerateForReportTagWord<ReportPol_Source_Site_FileModel>(ReportTag);
                    break;
                case "Pol_Source_Site_Address":
                    retStr = GenerateForReportTagWord<ReportPol_Source_Site_AddressModel>(ReportTag);
                    break;
                case "Municipality":
                    retStr = GenerateForReportTagWord<ReportMunicipalityModel>(ReportTag);
                    break;
                case "Infrastructure":
                    retStr = GenerateForReportTagWord<ReportInfrastructureModel>(ReportTag);
                    break;
                case "Box_Model":
                    retStr = GenerateForReportTagWord<ReportBox_ModelModel>(ReportTag);
                    break;
                case "Box_Model_Result":
                    retStr = GenerateForReportTagWord<ReportBox_Model_ResultModel>(ReportTag);
                    break;
                case "Visual_Plumes_Scenario":
                    retStr = GenerateForReportTagWord<ReportVisual_Plumes_ScenarioModel>(ReportTag);
                    break;
                case "Visual_Plumes_Scenario_Ambient":
                    retStr = GenerateForReportTagWord<ReportVisual_Plumes_Scenario_AmbientModel>(ReportTag);
                    break;
                case "Visual_Plumes_Scenario_Result":
                    retStr = GenerateForReportTagWord<ReportVisual_Plumes_Scenario_ResultModel>(ReportTag);
                    break;
                case "Infrastructure_Address":
                    retStr = GenerateForReportTagWord<ReportInfrastructure_AddressModel>(ReportTag);
                    break;
                case "Infrastructure_File":
                    retStr = GenerateForReportTagWord<ReportInfrastructure_FileModel>(ReportTag);
                    break;
                case "Mike_Scenario":
                    retStr = GenerateForReportTagWord<ReportMike_ScenarioModel>(ReportTag);
                    break;
                case "Mike_Source":
                    retStr = GenerateForReportTagWord<ReportMike_SourceModel>(ReportTag);
                    break;
                case "Mike_Source_Start_End":
                    retStr = GenerateForReportTagWord<ReportMike_Source_Start_EndModel>(ReportTag);
                    break;
                case "Mike_Boundary_Condition":
                    retStr = GenerateForReportTagWord<ReportMike_Boundary_ConditionModel>(ReportTag);
                    break;
                case "Mike_Scenario_File":
                    retStr = GenerateForReportTagWord<ReportMike_Scenario_FileModel>(ReportTag);
                    break;
                case "Municipality_Contact":
                    retStr = GenerateForReportTagWord<ReportMunicipality_ContactModel>(ReportTag);
                    break;
                case "Municipality_Contact_Address":
                    retStr = GenerateForReportTagWord<ReportMunicipality_Contact_AddressModel>(ReportTag);
                    break;
                case "Municipality_Contact_Tel":
                    retStr = GenerateForReportTagWord<ReportMunicipality_Contact_TelModel>(ReportTag);
                    break;
                case "Municipality_Contact_Email":
                    retStr = GenerateForReportTagWord<ReportMunicipality_Contact_EmailModel>(ReportTag);
                    break;
                case "Municipality_File":
                    retStr = GenerateForReportTagWord<ReportMunicipality_FileModel>(ReportTag);
                    break;
                case "Subsector_Special_Table":
                    retStr = GenerateForReportTagWord<ReportSubsector_Special_TableModel>(ReportTag);
                    break;
                case "Subsector_Climate_Site":
                    retStr = GenerateForReportTagWord<ReportSubsector_Climate_SiteModel>(ReportTag);
                    break;
                case "Subsector_Climate_Site_Data":
                    retStr = GenerateForReportTagWord<ReportSubsector_Climate_Site_DataModel>(ReportTag);
                    break;
                case "Subsector_Hydrometric_Site":
                    retStr = GenerateForReportTagWord<ReportSubsector_Hydrometric_SiteModel>(ReportTag);
                    break;
                case "Subsector_Hydrometric_Site_Data":
                    retStr = GenerateForReportTagWord<ReportSubsector_Hydrometric_Site_DataModel>(ReportTag);
                    break;
                case "Subsector_Hydrometric_Site_Rating_Curve":
                    retStr = GenerateForReportTagWord<ReportSubsector_Hydrometric_Site_Rating_CurveModel>(ReportTag);
                    break;
                case "Subsector_Hydrometric_Site_Rating_Curve_Value":
                    retStr = GenerateForReportTagWord<ReportSubsector_Hydrometric_Site_Rating_Curve_ValueModel>(ReportTag);
                    break;
                case "Subsector_Tide_Site":
                    retStr = GenerateForReportTagWord<ReportSubsector_Tide_SiteModel>(ReportTag);
                    break;
                case "Subsector_Tide_Site_Data":
                    retStr = GenerateForReportTagWord<ReportSubsector_Tide_Site_DataModel>(ReportTag);
                    break;
                case "Subsector_Lab_Sheet":
                    retStr = GenerateForReportTagWord<ReportSubsector_Lab_SheetModel>(ReportTag);
                    break;
                case "Subsector_Lab_Sheet_Detail":
                    retStr = GenerateForReportTagWord<ReportSubsector_Lab_Sheet_DetailModel>(ReportTag);
                    break;
                case "Subsector_Lab_Sheet_Tube_And_MPN_Detail":
                    retStr = GenerateForReportTagWord<ReportSubsector_Lab_Sheet_Tube_And_MPN_DetailModel>(ReportTag);
                    break;
                case "Subsector_File":
                    retStr = GenerateForReportTagWord<ReportSubsector_FileModel>(ReportTag);
                    break;
                case "Sector_File":
                    retStr = GenerateForReportTagWord<ReportSector_FileModel>(ReportTag);
                    break;
                case "Area_File":
                    retStr = GenerateForReportTagWord<ReportArea_FileModel>(ReportTag);
                    break;
                case "Sampling_Plan":
                    retStr = GenerateForReportTagWord<ReportSampling_PlanModel>(ReportTag);
                    break;
                case "Sampling_Plan_Lab_Sheet":
                    retStr = GenerateForReportTagWord<ReportSampling_Plan_Lab_SheetModel>(ReportTag);
                    break;
                case "Sampling_Plan_Lab_Sheet_Detail":
                    retStr = GenerateForReportTagWord<ReportSampling_Plan_Lab_Sheet_DetailModel>(ReportTag);
                    break;
                case "Sampling_Plan_Lab_Sheet_Tube_And_MPN_Detail":
                    retStr = GenerateForReportTagWord<ReportSampling_Plan_Lab_Sheet_Tube_And_MPN_DetailModel>(ReportTag);
                    break;
                case "Sampling_Plan_Subsector":
                    retStr = GenerateForReportTagWord<ReportSampling_Plan_SubsectorModel>(ReportTag);
                    break;
                case "Sampling_Plan_Subsector_Site":
                    retStr = GenerateForReportTagWord<ReportSampling_Plan_Subsector_SiteModel>(ReportTag);
                    break;
                case "Climate_Site":
                    retStr = GenerateForReportTagWord<ReportClimate_SiteModel>(ReportTag);
                    break;
                case "Climate_Site_Data":
                    retStr = GenerateForReportTagWord<ReportClimate_Site_DataModel>(ReportTag);
                    break;
                case "Hydrometric_Site":
                    retStr = GenerateForReportTagWord<ReportHydrometric_SiteModel>(ReportTag);
                    break;
                case "Hydrometric_Site_Data":
                    retStr = GenerateForReportTagWord<ReportHydrometric_Site_DataModel>(ReportTag);
                    break;
                case "Hydrometric_Site_Rating_Curve":
                    retStr = GenerateForReportTagWord<ReportHydrometric_Site_Rating_CurveModel>(ReportTag);
                    break;
                case "Hydrometric_Site_Rating_Curve_Value":
                    retStr = GenerateForReportTagWord<ReportHydrometric_Site_Rating_Curve_ValueModel>(ReportTag);
                    break;
                case "Province_File":
                    retStr = GenerateForReportTagWord<ReportProvince_FileModel>(ReportTag);
                    break;
                case "Country_File":
                    retStr = GenerateForReportTagWord<ReportCountry_FileModel>(ReportTag);
                    break;
                case "MPN_Lookup":
                    retStr = GenerateForReportTagWord<ReportMPN_LookupModel>(ReportTag);
                    break;
                case "Root_File":
                    retStr = GenerateForReportTagWord<ReportRoot_FileModel>(ReportTag);
                    break;
                default:
                    {
                        retStr = string.Format(ReportServiceRes._NotImplementedIn_, ReportTag.TagItem, "ReportGetDBOfType");
                    }
                    break;
            }


            return retStr;
        }
        public string GenerateReportFromTemplateWord(FileInfo fiDoc, int StartTVItemID, int Take, int AppTaskID)
        {
            string retStr = "";

            if (!fiDoc.Exists)
                return string.Format(ReportServiceRes.FileDoesNotExist_, fiDoc.FullName);

            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Open(fiDoc.FullName);
            Microsoft.Office.Interop.Word.Range Range = doc.Range();

            // should remove after testing
            wordApp.Visible = true;

            if (_User != null)
            {
                wordApp.Visible = true;
                int PercentCompleted = 2;
                retStr = UpdateAppTaskPercentCompleted(AppTaskID, PercentCompleted);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    Range.Comments.Add(Range, retStr);
                    return retStr;
                } 
            }

            List<ReportTag> reportTagList = new List<ReportTag>();
            ReportTag reportTagStart = new ReportTag()
            {
                wordApp = wordApp,
                doc = doc,
                OnlyImmediateChildren = true,
                UnderTVItemID = StartTVItemID,
                Take = Take,
                AppTaskID = AppTaskID,
            };

            reportTagStart.OnlyImmediateChildren = false;
            retStr = CheckTagsAndContentOKWord(reportTagStart, reportTagList);
            if (!string.IsNullOrWhiteSpace(retStr))
            {
                doc.SaveAs2(fiDoc.FullName);
                doc.Close();
                wordApp.Quit();
                return retStr;
            }

            retStr = FindTagsWord(reportTagStart, reportTagList);
            if (!string.IsNullOrWhiteSpace(retStr))
            {
                doc.SaveAs2(fiDoc.FullName);
                doc.Close();
                wordApp.Quit();
                return retStr;
            }

            reportTagStart.OnlyImmediateChildren = true;
            retStr = FillTemplateWithDBInfoWord(reportTagStart);
            if (!string.IsNullOrWhiteSpace(retStr))
            {
                doc.SaveAs2(fiDoc.FullName);
                doc.Close();
                wordApp.Quit();
                return retStr;
            }
            else
            {
                Range = doc.Range();
                Range.Start = 0;
                Range.End = 0;
                Range.Select();

                doc.SaveAs2(fiDoc.FullName);
                doc.Close();
                wordApp.Quit();
            }

            return "";
        }
        public void GetSelectedFieldsAndPropertiesWord(ReportTreeNode reportTreeNodeTable, Microsoft.Office.Interop.Word.Document doc)
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
                                        doc.Application.Selection.TypeText("Error: " + RTN.Text);
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
                                        doc.Application.Selection.TypeText(RTN.Text);
                                        StringBuilder sbFilter = new StringBuilder();
                                        GetFieldDBFilterText(RTN, sbFilter);
                                        doc.Application.Selection.TypeText(sbFilter.ToString());
                                        doc.Application.Selection.TypeParagraph();
                                    }
                                    break;
                                default:
                                    {
                                        doc.Application.Selection.TypeText("NotImplemented: " + RTN.Text);
                                        doc.Application.Selection.TypeParagraph();
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
        }
        public void GetSelectedFieldsContainerWord(ReportTreeNode reportTreeNodeTable, Microsoft.Office.Interop.Word.Document doc, int Level)
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
                                        doc.Application.Selection.TypeText("Error: " + RTN.Text);
                                        doc.Application.Selection.TypeParagraph();
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
                                        doc.Application.Selection.TypeText(Marker + RTN.Text + GetFieldReportFormatText(RTN) + GetFieldReportConditionText(RTN) + Marker + " " + "\t");
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
                                        doc.Range().InsertAfter("NotImplemented: " + RTN.Text + "\r\n");
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

                        doc.Application.Selection.TypeText(sbCondition.ToString());
                        doc.Application.Selection.TypeParagraph();
                    }
                    else
                    {
                        if (fieldList.Count > 0)
                        {
                            doc.Application.Selection.TypeParagraph();
                        }
                    }
                }
            }
        }
        public void GetTreeViewSelectedStatusWord(ReportTreeNode reportTreeNode, Microsoft.Office.Interop.Word.Document doc, int Level)
        {
            if (reportTreeNode == null)
                return;

            switch (reportTreeNode.ReportTreeNodeSubType)
            {
                case ReportTreeNodeSubTypeEnum.Error:
                    {
                        doc.Application.Selection.TypeText("Error occured. Please select an item on the left tree view.");
                        doc.Application.Selection.TypeParagraph();
                    }
                    break;
                case ReportTreeNodeSubTypeEnum.TableNotSelectable:
                case ReportTreeNodeSubTypeEnum.TableSelectable:
                    {
                        if (Level == 0)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                doc.Application.Selection.Paragraphs.Outdent();
                            }
                            doc.Application.Selection.Font.Bold = 1;
                            for (int i = 0; i < Level; i++)
                            {
                                doc.Application.Selection.Paragraphs.Indent();
                            }
                            doc.Application.Selection.Font.Size = 14;
                            doc.Application.Selection.TypeText(reportTreeNode.Text);
                            doc.Application.Selection.TypeParagraph();
                            doc.Application.Selection.Font.Size = 10;
                            doc.Application.Selection.Font.Bold = 0;
                            doc.Application.Selection.TypeText(Marker + "Start " + reportTreeNode.Text + " " + LanguageRequest.ToString());
                            doc.Application.Selection.TypeParagraph();
                            GetSelectedFieldsAndPropertiesWord(reportTreeNode, doc);
                            doc.Application.Selection.TypeText(Marker);
                            doc.Application.Selection.TypeParagraph();
                            GetSelectedFieldsContainerWord(reportTreeNode, doc, Level);
                        }
                        else
                        {
                            bool CheckExist = GetSubFieldIsChecked(reportTreeNode);
                            if (CheckExist)
                            {
                                for (int i = 0; i < 5; i++)
                                {
                                    doc.Application.Selection.Paragraphs.Outdent();
                                }
                                doc.Application.Selection.Font.Bold = 1;
                                for (int i = 0; i < Level; i++)
                                {
                                    doc.Application.Selection.Paragraphs.Indent();
                                }
                                doc.Application.Selection.Font.Size = 14;
                                doc.Application.Selection.TypeText(reportTreeNode.Text);
                                doc.Application.Selection.TypeParagraph();
                                doc.Application.Selection.Font.Size = 10;
                                doc.Application.Selection.Font.Bold = 0;
                                //doc.Application.Selection.Paragraphs.Indent();
                                doc.Application.Selection.TypeText(Marker + "LoopStart " + reportTreeNode.Text + " " + LanguageRequest.ToString());
                                doc.Application.Selection.TypeParagraph();
                                GetSelectedFieldsAndPropertiesWord(reportTreeNode, doc);
                                doc.Application.Selection.TypeText(Marker);
                                doc.Application.Selection.TypeParagraph();
                                GetSelectedFieldsContainerWord(reportTreeNode, doc, Level);
                            }
                        }
                        foreach (ReportTreeNode RTN in reportTreeNode.Nodes)
                        {
                            if (RTN.Checked)
                            {
                                GetTreeViewSelectedStatusWord(RTN, doc, Level + 1);
                            }
                        }
                        if (Level == 0)
                        {
                            doc.Range().InsertAfter(Marker + "End " + reportTreeNode.Text + " " + LanguageRequest.ToString() + Marker + "\r\n");
                        }
                        else
                        {
                            bool CheckExist = GetSubFieldIsChecked(reportTreeNode);
                            if (CheckExist)
                            {
                                doc.Range().InsertAfter(Marker + "LoopEnd " + reportTreeNode.Text + " " + LanguageRequest.ToString() + Marker + "\r\n");
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
                        doc.Range().InsertAfter(Marker + "NotImplemented " + reportTreeNode.Text + Marker + "\r\n");
                    }
                    break;
            }
        }
        public string UpdateAppTaskPercentCompleted(int AppTaskID, int PercentCompleted)
        {
            if (_User != null)
            {
                using (AppTaskService appTaskService = new AppTaskService(LanguageRequest, _User))
                {
                    AppTaskModel appTaskModel = appTaskService.GetAppTaskModelWithAppTaskIDDB(AppTaskID);
                    if (!string.IsNullOrWhiteSpace(appTaskModel.Error))
                        return appTaskModel.Error;

                    appTaskModel.PercentCompleted = PercentCompleted;
                    AppTaskModel appTaskModelRet = appTaskService.PostUpdateAppTask(appTaskModel);
                    if (!string.IsNullOrWhiteSpace(appTaskModelRet.Error))
                        return appTaskModel.Error;
                }
            }

            return "";
        }
        #endregion Functions public
    }

    public class SubTypeAndLetter
    {
        public TVTypeEnum SubType { get; set; }
        public string Letter { get; set; }
    }
}
