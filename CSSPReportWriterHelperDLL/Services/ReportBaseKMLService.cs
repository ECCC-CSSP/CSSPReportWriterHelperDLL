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
using DHI.PFS;
using DHI.Generic.MikeZero.DFS.dfsu;
using System.Security.Principal;
using DHI.Generic.MikeZero.DFS;
using DHI.Generic.MikeZero;
using System.Diagnostics;

namespace CSSPReportWriterHelperDLL.Services
{
    public partial class ReportBaseService
    {
        #region Functions public
        public string CheckNumberOfVariableOnFirstLineIsEqualToMaximumNumberOfVariablesKML(StringBuilder sbFileText, List<ReportTag> reportTagList)
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
        public string CheckTagsAndContentOKKML(StringBuilder sbFileText, List<ReportTag> reportTagList, int StartTVItemID, int Take)
        {
            string retStr = "";

            retStr = FindAllTagsKML(sbFileText, reportTagList, StartTVItemID, Take);
            if (!string.IsNullOrWhiteSpace(retStr))
                return retStr;

            retStr = CheckTagsPositionOKKML(sbFileText, reportTagList);
            if (!string.IsNullOrWhiteSpace(retStr))
                return retStr;

            retStr = CheckTagsFirstLineKML(sbFileText, reportTagList);
            if (!string.IsNullOrWhiteSpace(retStr))
                return retStr;

            retStr = FillTagsVariablesKML(sbFileText, reportTagList);
            if (!string.IsNullOrWhiteSpace(retStr))
                return retStr;

            retStr =
                FillTagsParentChildRelationshipKML(sbFileText, reportTagList);
            if (!string.IsNullOrWhiteSpace(retStr))
                return retStr;

            foreach (ReportTag reportTag in reportTagList.Where(c => c.ReportTagParent == null))
            {
                retStr = FillTagsLevelsKML(sbFileText, reportTag, 0);
                if (!string.IsNullOrWhiteSpace(retStr))
                    return retStr;
            }

            //retStr = CheckNumberOfVariableOnFirstLineIsEqualToMaximumNumberOfVariablesKML(sbFileText, reportTagList);
            //if (!string.IsNullOrWhiteSpace(retStr))
            //    return retStr;

            return "";
        }
        public string CheckTagsFirstLineKML(StringBuilder sbFileText, List<ReportTag> reportTagList)
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
        public string CheckTagsPositionOKKML(StringBuilder sbFileText, List<ReportTag> reportTagList)
        {
            for (int i = 0, count = reportTagList.Count; i < count; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    if (reportTagList[i].RangeEndTagStartPos == reportTagList[j].RangeEndTagStartPos)
                    {
                        string ErrorText = "\r\n\r\n" + string.Format(ReportServiceRes.YouHaveATag_WhichIsNotClosedProperly, reportTagList[i].KMLTagText) + "\r\n\r\n";
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
        public string FindAllTagsKML(StringBuilder sbFileText, List<ReportTag> reportTagList, int StartTVItemID, int Take)
        {
            string tempTest = "";
            bool Found = true;
            string FileText = sbFileText.ToString();
            int MaxRange = 0;
            int RangeEndTagStartPos = 0;
            int RangeEndTagEndPos = 0;
            int RangeStartTagStartPos = -1;
            int RangeStartTagEndPos = -1;

            MaxRange = sbFileText.Length;

            foreach (string s in new List<string>() { "Start", "LoopStart" })
            {
                //RangeStartTagEndPos = -1;
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

                        reportTag.KMLTagText = FileText.Substring(reportTag.RangeInnerTagStartPos, reportTag.RangeInnerTagEndPos - reportTag.RangeInnerTagStartPos);

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
                reportTag.KMLTagText = FileText.Substring(reportTag.RangeStartTagStartPos, reportTag.RangeStartTagEndPos - reportTag.RangeStartTagStartPos);
                if (reportTag.KMLTagText.Substring(reportTag.KMLTagText.Length - 2) == "\r\n")
                {
                    reportTag.KMLTagText = reportTag.KMLTagText.Substring(0, reportTag.KMLTagText.Length - 2);
                }
                reportTag.DocumentType = DocumentType.KML;
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
                string ErrorText = "\r\n\r\n" + ReportServiceRes.OnlyOneStartTagIsAllowedInKMLTemplate + "\r\n\r\n";
                sbFileText = sbFileText.Insert(0, ErrorText);
                return ErrorText;
            }


            return "";
        }
        private void DrawKMLContourPolygon(List<ContourPolygon> ContourPolygonList, DfsuFile dfsuFile, int ParamCount, StringBuilder sbNewFileText)
        {
            int Count = 0;
            float MaxXCoord = -180;
            float MaxYCoord = -90;
            float MinXCoord = 180;
            float MinYCoord = 90;
            sbNewFileText.AppendLine(@"  <Folder>");
            sbNewFileText.AppendLine(@"    <visibility>0</visibility>");
            sbNewFileText.AppendLine(string.Format(@"    <name>{0:yyyy-MM-dd} {0:HH:mm:ss tt}</name>", dfsuFile.StartDateTime.AddSeconds(ParamCount * dfsuFile.TimeStepInSeconds)));
            sbNewFileText.AppendLine(@"    <TimeSpan>");
            sbNewFileText.AppendLine(string.Format(@"    <begin>{0:yyyy-MM-dd}T{0:HH:mm:ss}</begin>", dfsuFile.StartDateTime.AddSeconds(ParamCount * dfsuFile.TimeStepInSeconds)));
            sbNewFileText.AppendLine(string.Format(@"    <end>{0:yyyy-MM-dd}T{0:HH:mm:ss}</end>", dfsuFile.StartDateTime.AddSeconds((ParamCount + 1) * dfsuFile.TimeStepInSeconds)));
            sbNewFileText.AppendLine(@"    </TimeSpan>");
            foreach (ContourPolygon contourPolygon in ContourPolygonList)
            {
                Count += 1;
                // draw the polygons
                sbNewFileText.AppendLine(@"    <Placemark>");
                sbNewFileText.AppendLine(@"      <visibility>0</visibility>");
                sbNewFileText.AppendLine(string.Format(@"      <name>{0} {1}</name>", contourPolygon.ContourValue, ReportServiceRes.PollutionContour));
                if (contourPolygon.ContourValue >= 14 && contourPolygon.ContourValue < 88)
                {
                    sbNewFileText.AppendLine(@"      <styleUrl>#fc_14</styleUrl>");
                }
                else if (contourPolygon.ContourValue >= 88)
                {
                    sbNewFileText.AppendLine(@"      <styleUrl>#fc_88</styleUrl>");
                }
                else
                {
                    sbNewFileText.AppendLine(@"      <styleUrl>#fc_LT14</styleUrl>");
                }

                sbNewFileText.AppendLine(@"        <Polygon>");
                //sbPlacemarkFeacalColiformContour.AppendLine(@"<extrude>1</extrude>");
                //sbPlacemarkFeacalColiformContour.AppendLine(@"<tessellate>1</tessellate>");
                //sbPlacemarkFeacalColiformContour.AppendLine(@"<altitudeMode>absolute</altitudeMode>");
                //sbPlacemarkFeacalColiformContour.AppendLine(@"<gx:drawOrder>" + contourPolygon.Layer +"</gx:drawOrder>"); 
                sbNewFileText.AppendLine(@"        <outerBoundaryIs>");
                sbNewFileText.AppendLine(@"        <LinearRing>");
                sbNewFileText.AppendLine(@"        <coordinates>");
                foreach (Node node in contourPolygon.ContourNodeList)
                {
                    if (MaxXCoord < node.X) MaxXCoord = node.X;
                    if (MaxYCoord < node.Y) MaxYCoord = node.Y;
                    if (MinXCoord > node.X) MinXCoord = node.X;
                    if (MinYCoord > node.Y) MinYCoord = node.Y;
                    sbNewFileText.Append("        " + node.X.ToString().Replace(",", ".") + @"," + node.Y.ToString().Replace(",", ".") + "," + node.Z.ToString().Replace(",", ".") + " ");
                }
                sbNewFileText.AppendLine(@"        </coordinates>");
                sbNewFileText.AppendLine(@"        </LinearRing>");
                sbNewFileText.AppendLine(@"        </outerBoundaryIs>");
                sbNewFileText.AppendLine(@"        </Polygon>");
                sbNewFileText.AppendLine(@"    </Placemark>");
            }
            sbNewFileText.AppendLine(@"  </Folder>");
        }
        private void DrawKMLContourStyle(StringBuilder sbNewFileText)
        {
            sbNewFileText.AppendLine(@"	<StyleMap id=""msn_ylw-pushpin"">");
            sbNewFileText.AppendLine(@"		<Pair>");
            sbNewFileText.AppendLine(@"			<key>normal</key>");
            sbNewFileText.AppendLine(@"			<styleUrl>#sn_ylw-pushpin</styleUrl>");
            sbNewFileText.AppendLine(@"		</Pair>");
            sbNewFileText.AppendLine(@"		<Pair>");
            sbNewFileText.AppendLine(@"			<key>highlight</key>");
            sbNewFileText.AppendLine(@"			<styleUrl>#sh_ylw-pushpin</styleUrl>");
            sbNewFileText.AppendLine(@"		</Pair>");
            sbNewFileText.AppendLine(@"	</StyleMap>");
            sbNewFileText.AppendLine(@"	<Style id=""sn_ylw-pushpin"">");
            sbNewFileText.AppendLine(@"		<IconStyle>");
            sbNewFileText.AppendLine(@"			<scale>1.1</scale>");
            sbNewFileText.AppendLine(@"			<Icon>");
            sbNewFileText.AppendLine(@"				<href>http://maps.google.com/mapfiles/kml/pushpin/ylw-pushpin.png</href>");
            sbNewFileText.AppendLine(@"			</Icon>");
            sbNewFileText.AppendLine(@"			<hotSpot x=""20"" y=""2"" xunits=""pixels"" yunits=""pixels""/>");
            sbNewFileText.AppendLine(@"		</IconStyle>");
            sbNewFileText.AppendLine(@"      <LineStyle>");
            sbNewFileText.AppendLine(@"         <color>ff000000</color>");
            sbNewFileText.AppendLine(@"       </LineStyle>");
            sbNewFileText.AppendLine(@"	</Style>");
            sbNewFileText.AppendLine(@"	<Style id=""sh_ylw-pushpin"">");
            sbNewFileText.AppendLine(@"		<IconStyle>");
            sbNewFileText.AppendLine(@"			<scale>1.3</scale>");
            sbNewFileText.AppendLine(@"			<Icon>");
            sbNewFileText.AppendLine(@"				<href>http://maps.google.com/mapfiles/kml/pushpin/ylw-pushpin.png</href>");
            sbNewFileText.AppendLine(@"			</Icon>");
            sbNewFileText.AppendLine(@"			<hotSpot x=""20"" y=""2"" xunits=""pixels"" yunits=""pixels""/>");
            sbNewFileText.AppendLine(@"		</IconStyle>");
            sbNewFileText.AppendLine(@"      <LineStyle>");
            sbNewFileText.AppendLine(@"         <color>ff000000</color>");
            sbNewFileText.AppendLine(@"       </LineStyle>");
            sbNewFileText.AppendLine(@"	</Style>");

            sbNewFileText.AppendLine(@"	<StyleMap id=""msn_grn-pushpin"">");
            sbNewFileText.AppendLine(@"		<Pair>");
            sbNewFileText.AppendLine(@"			<key>normal</key>");
            sbNewFileText.AppendLine(@"			<styleUrl>#sn_grn-pushpin</styleUrl>");
            sbNewFileText.AppendLine(@"		</Pair>");
            sbNewFileText.AppendLine(@"		<Pair>");
            sbNewFileText.AppendLine(@"			<key>highlight</key>");
            sbNewFileText.AppendLine(@"			<styleUrl>#sh_grn-pushpin</styleUrl>");
            sbNewFileText.AppendLine(@"		</Pair>");
            sbNewFileText.AppendLine(@"	</StyleMap>");
            sbNewFileText.AppendLine(@"	<Style id=""sn_grn-pushpin"">");
            sbNewFileText.AppendLine(@"		<IconStyle>");
            sbNewFileText.AppendLine(@"			<scale>1.1</scale>");
            sbNewFileText.AppendLine(@"			<Icon>");
            sbNewFileText.AppendLine(@"				<href>http://maps.google.com/mapfiles/kml/pushpin/grn-pushpin.png</href>");
            sbNewFileText.AppendLine(@"			</Icon>");
            sbNewFileText.AppendLine(@"			<hotSpot x=""20"" y=""2"" xunits=""pixels"" yunits=""pixels""/>");
            sbNewFileText.AppendLine(@"		</IconStyle>");
            sbNewFileText.AppendLine(@"      <LineStyle>");
            sbNewFileText.AppendLine(@"         <color>ff000000</color>");
            sbNewFileText.AppendLine(@"       </LineStyle>");
            sbNewFileText.AppendLine(@"	</Style>");
            sbNewFileText.AppendLine(@"	<Style id=""sh_grn-pushpin"">");
            sbNewFileText.AppendLine(@"		<IconStyle>");
            sbNewFileText.AppendLine(@"			<scale>1.3</scale>");
            sbNewFileText.AppendLine(@"			<Icon>");
            sbNewFileText.AppendLine(@"				<href>http://maps.google.com/mapfiles/kml/pushpin/grn-pushpin.png</href>");
            sbNewFileText.AppendLine(@"			</Icon>");
            sbNewFileText.AppendLine(@"			<hotSpot x=""20"" y=""2"" xunits=""pixels"" yunits=""pixels""/>");
            sbNewFileText.AppendLine(@"		</IconStyle>");
            sbNewFileText.AppendLine(@"      <LineStyle>");
            sbNewFileText.AppendLine(@"         <color>ff000000</color>");
            sbNewFileText.AppendLine(@"       </LineStyle>");
            sbNewFileText.AppendLine(@"	</Style>");

            sbNewFileText.AppendLine(@"	<StyleMap id=""msn_blue-pushpin"">");
            sbNewFileText.AppendLine(@"		<Pair>");
            sbNewFileText.AppendLine(@"			<key>normal</key>");
            sbNewFileText.AppendLine(@"			<styleUrl>#sn_blue-pushpin</styleUrl>");
            sbNewFileText.AppendLine(@"		</Pair>");
            sbNewFileText.AppendLine(@"		<Pair>");
            sbNewFileText.AppendLine(@"			<key>highlight</key>");
            sbNewFileText.AppendLine(@"			<styleUrl>#sh_blue-pushpin</styleUrl>");
            sbNewFileText.AppendLine(@"		</Pair>");
            sbNewFileText.AppendLine(@"	</StyleMap>");
            sbNewFileText.AppendLine(@"	<Style id=""sn_blue-pushpin"">");
            sbNewFileText.AppendLine(@"		<IconStyle>");
            sbNewFileText.AppendLine(@"			<scale>1.1</scale>");
            sbNewFileText.AppendLine(@"			<Icon>");
            sbNewFileText.AppendLine(@"				<href>http://maps.google.com/mapfiles/kml/pushpin/blue-pushpin.png</href>");
            sbNewFileText.AppendLine(@"			</Icon>");
            sbNewFileText.AppendLine(@"			<hotSpot x=""20"" y=""2"" xunits=""pixels"" yunits=""pixels""/>");
            sbNewFileText.AppendLine(@"		</IconStyle>");
            sbNewFileText.AppendLine(@"      <LineStyle>");
            sbNewFileText.AppendLine(@"         <color>ff000000</color>");
            sbNewFileText.AppendLine(@"       </LineStyle>");
            sbNewFileText.AppendLine(@"	</Style>");
            sbNewFileText.AppendLine(@"	<Style id=""sh_blue-pushpin"">");
            sbNewFileText.AppendLine(@"		<IconStyle>");
            sbNewFileText.AppendLine(@"			<scale>1.3</scale>");
            sbNewFileText.AppendLine(@"			<Icon>");
            sbNewFileText.AppendLine(@"				<href>http://maps.google.com/mapfiles/kml/pushpin/blue-pushpin.png</href>");
            sbNewFileText.AppendLine(@"			</Icon>");
            sbNewFileText.AppendLine(@"			<hotSpot x=""20"" y=""2"" xunits=""pixels"" yunits=""pixels""/>");
            sbNewFileText.AppendLine(@"		</IconStyle>");
            sbNewFileText.AppendLine(@"      <LineStyle>");
            sbNewFileText.AppendLine(@"         <color>ff000000</color>");
            sbNewFileText.AppendLine(@"       </LineStyle>");
            sbNewFileText.AppendLine(@"	</Style>");

            sbNewFileText.AppendLine(@"<Style id=""fc_LT14"">");
            sbNewFileText.AppendLine(@"<LineStyle>");
            sbNewFileText.AppendLine(@"<color>ff000000</color>");
            sbNewFileText.AppendLine(@"</LineStyle>");
            sbNewFileText.AppendLine(@"<PolyStyle>");
            sbNewFileText.AppendLine(@"<color>6600ff00</color>");
            sbNewFileText.AppendLine(@"<outline>1</outline>");
            sbNewFileText.AppendLine(@"</PolyStyle>");
            sbNewFileText.AppendLine(@"</Style>");

            sbNewFileText.AppendLine(@"<Style id=""fc_14"">");
            sbNewFileText.AppendLine(@"<LineStyle>");
            sbNewFileText.AppendLine(@"<color>ff000000</color>");
            sbNewFileText.AppendLine(@"</LineStyle>");
            sbNewFileText.AppendLine(@"<PolyStyle>");
            sbNewFileText.AppendLine(@"<color>66ff0000</color>");
            sbNewFileText.AppendLine(@"<outline>1</outline>");
            sbNewFileText.AppendLine(@"</PolyStyle>");
            sbNewFileText.AppendLine(@"</Style>");

            sbNewFileText.AppendLine(@"<Style id=""fc_88"">");
            sbNewFileText.AppendLine(@"<LineStyle>");
            sbNewFileText.AppendLine(@"<color>ff000000</color>");
            sbNewFileText.AppendLine(@"</LineStyle>");
            sbNewFileText.AppendLine(@"<PolyStyle>");
            sbNewFileText.AppendLine(@"<color>660000ff</color>");
            sbNewFileText.AppendLine(@"<outline>1</outline>");
            sbNewFileText.AppendLine(@"</PolyStyle>");
            sbNewFileText.AppendLine(@"</Style>");
        }
        public void FillElementLayerList(DfsuFile dfsuFile, List<Element> ElementList, List<ElementLayer> ElementLayerList, List<NodeLayer> topNodeLayerList, List<NodeLayer> bottomNodeLayerList, ReportTag reportTag)
        {
            if (dfsuFile.DfsuFileType == DfsuFileType.Dfsu3DSigma || dfsuFile.DfsuFileType == DfsuFileType.Dfsu3DSigmaZ)
            {
                //reportTag.Error = ReportServiceRes.MIKE3NotImplementedYet;

                float Depth = 0.0f;
                List<Element> TempElementList;

                // doing type 32
                TempElementList = (from el in ElementList
                                   where el.Type == 32
                                   && (dfsuFile.Z[el.NodeList[3].ID - 1] == Depth
                                   && dfsuFile.Z[el.NodeList[4].ID - 1] == Depth
                                   && dfsuFile.Z[el.NodeList[5].ID - 1] == Depth)
                                   select el).ToList<Element>();

                foreach (Element el in TempElementList)
                {
                    int Layer = 1;
                    List<Element> ColumnElementList = (from el1 in ElementList
                                                       where el1.Type == 32
                                                       && el1.NodeList[0].X == el.NodeList[0].X
                                                       && el1.NodeList[1].X == el.NodeList[1].X
                                                       && el1.NodeList[2].X == el.NodeList[2].X
                                                       orderby dfsuFile.Z[el1.NodeList[0].ID - 1] descending
                                                       select el1).ToList<Element>();

                    for (int j = 0; j < ColumnElementList.Count; j++)
                    {
                        ElementLayer elementLayer = new ElementLayer();
                        elementLayer.Layer = Layer;
                        elementLayer.ZMin = (from nz in ColumnElementList[j].NodeList select dfsuFile.Z[nz.ID - 1]).Min();
                        elementLayer.ZMax = (from nz in ColumnElementList[j].NodeList select dfsuFile.Z[nz.ID - 1]).Max();
                        elementLayer.Element = ColumnElementList[j];
                        ElementLayerList.Add(elementLayer);

                        NodeLayer nl3 = new NodeLayer();
                        nl3.Layer = Layer;
                        nl3.Z = dfsuFile.Z[ColumnElementList[j].NodeList[3].ID - 1];
                        nl3.Node = ColumnElementList[j].NodeList[3];

                        NodeLayer nl4 = new NodeLayer();
                        nl4.Layer = Layer;
                        nl4.Z = dfsuFile.Z[ColumnElementList[j].NodeList[4].ID - 1];
                        nl4.Node = ColumnElementList[j].NodeList[4];

                        NodeLayer nl5 = new NodeLayer();
                        nl5.Layer = Layer;
                        nl5.Z = dfsuFile.Z[ColumnElementList[j].NodeList[5].ID - 1];
                        nl5.Node = ColumnElementList[j].NodeList[5];

                        topNodeLayerList.Add(nl3);
                        topNodeLayerList.Add(nl4);
                        topNodeLayerList.Add(nl5);

                        NodeLayer nl0 = new NodeLayer();
                        nl0.Layer = Layer;
                        nl0.Z = dfsuFile.Z[ColumnElementList[j].NodeList[0].ID - 1];
                        nl0.Node = ColumnElementList[j].NodeList[0];

                        NodeLayer nl1 = new NodeLayer();
                        nl1.Layer = Layer;
                        nl1.Z = dfsuFile.Z[ColumnElementList[j].NodeList[1].ID - 1];
                        nl1.Node = ColumnElementList[j].NodeList[1];

                        NodeLayer nl2 = new NodeLayer();
                        nl2.Layer = Layer;
                        nl2.Z = dfsuFile.Z[ColumnElementList[j].NodeList[2].ID - 1];
                        nl2.Node = ColumnElementList[j].NodeList[2];

                        bottomNodeLayerList.Add(nl0);
                        bottomNodeLayerList.Add(nl1);
                        bottomNodeLayerList.Add(nl2);

                        Layer += 1;
                    }
                }

                // doing type 33
                TempElementList = (from el in ElementList
                                   where el.Type == 33
                                   && (dfsuFile.Z[el.NodeList[4].ID - 1] == Depth
                                   && dfsuFile.Z[el.NodeList[5].ID - 1] == Depth
                                   && dfsuFile.Z[el.NodeList[6].ID - 1] == Depth
                                   && dfsuFile.Z[el.NodeList[7].ID - 1] == Depth)
                                   select el).ToList<Element>();

                foreach (Element el in TempElementList)
                {
                    int Layer = 1;
                    List<Element> ColumElementList = (from el1 in ElementList
                                                      where el1.Type == 33
                                                      && el1.NodeList[0].X == el.NodeList[0].X
                                                      && el1.NodeList[1].X == el.NodeList[1].X
                                                      && el1.NodeList[2].X == el.NodeList[2].X
                                                      && el1.NodeList[3].X == el.NodeList[3].X
                                                      orderby dfsuFile.Z[el1.NodeList[0].ID - 1] descending
                                                      select el1).ToList<Element>();

                    for (int j = 0; j < ColumElementList.Count; j++)
                    {
                        ElementLayer elementLayer = new ElementLayer();
                        elementLayer.Layer = Layer;
                        elementLayer.ZMin = (from nz in ColumElementList[j].NodeList select dfsuFile.Z[nz.ID - 1]).Min();
                        elementLayer.ZMax = (from nz in ColumElementList[j].NodeList select dfsuFile.Z[nz.ID - 1]).Max();
                        elementLayer.Element = ColumElementList[j];
                        ElementLayerList.Add(elementLayer);

                        NodeLayer nl4 = new NodeLayer();
                        nl4.Layer = Layer;
                        nl4.Z = 0;
                        nl4.Node = ColumElementList[j].NodeList[4];

                        NodeLayer nl5 = new NodeLayer();
                        nl5.Layer = Layer;
                        nl5.Z = 0;
                        nl5.Node = ColumElementList[j].NodeList[5];

                        NodeLayer nl6 = new NodeLayer();
                        nl6.Layer = Layer;
                        nl6.Z = 0;
                        nl6.Node = ColumElementList[j].NodeList[6];

                        NodeLayer nl7 = new NodeLayer();
                        nl7.Layer = Layer;
                        nl7.Z = 0;
                        nl7.Node = ColumElementList[j].NodeList[7];


                        topNodeLayerList.Add(nl4);
                        topNodeLayerList.Add(nl5);
                        topNodeLayerList.Add(nl6);
                        topNodeLayerList.Add(nl7);

                        NodeLayer nl0 = new NodeLayer();
                        nl0.Layer = Layer;
                        nl0.Z = dfsuFile.Z[ColumElementList[j].NodeList[0].ID - 1];
                        nl0.Node = ColumElementList[j].NodeList[0];

                        NodeLayer nl1 = new NodeLayer();
                        nl1.Layer = Layer;
                        nl1.Z = dfsuFile.Z[ColumElementList[j].NodeList[1].ID - 1];
                        nl1.Node = ColumElementList[j].NodeList[1];

                        NodeLayer nl2 = new NodeLayer();
                        nl2.Layer = Layer;
                        nl2.Z = dfsuFile.Z[ColumElementList[j].NodeList[2].ID - 1];
                        nl2.Node = ColumElementList[j].NodeList[2];

                        NodeLayer nl3 = new NodeLayer();
                        nl3.Layer = Layer;
                        nl3.Z = dfsuFile.Z[ColumElementList[j].NodeList[3].ID - 1];
                        nl3.Node = ColumElementList[j].NodeList[3];

                        bottomNodeLayerList.Add(nl0);
                        bottomNodeLayerList.Add(nl1);
                        bottomNodeLayerList.Add(nl2);
                        bottomNodeLayerList.Add(nl3);

                        Layer += 1;
                    }
                }

                List<ElementLayer> TempElementLayerList = (from el in ElementLayerList
                                                           orderby el.Element.ID
                                                           select el).Distinct().ToList();

                //ElementLayerList = new List<ElementLayer>();
                int OldElemID = 0;
                foreach (ElementLayer el in TempElementLayerList)
                {
                    if (OldElemID == el.Element.ID)
                    {
                        ElementLayerList.Remove(el);
                    }
                    OldElemID = el.Element.ID;
                }

                List<NodeLayer> TempNodeLayerList = (from nl in topNodeLayerList
                                                     orderby nl.Node.ID
                                                     select nl).Distinct().ToList();

                topNodeLayerList = new List<NodeLayer>();
                int OldID = 0;
                foreach (NodeLayer nl in TempNodeLayerList)
                {
                    if (OldID != nl.Node.ID)
                    {
                        topNodeLayerList.Add(nl);
                        OldID = nl.Node.ID;
                    }
                }

                if (bottomNodeLayerList.Count() > 0)
                {
                    TempNodeLayerList = (from nl in bottomNodeLayerList
                                         orderby nl.Node.ID
                                         select nl).Distinct().ToList();

                    bottomNodeLayerList = new List<NodeLayer>();
                    OldID = 0;
                    foreach (NodeLayer nl in TempNodeLayerList)
                    {
                        if (OldID != nl.Node.ID)
                        {
                            bottomNodeLayerList.Add(nl);
                            OldID = nl.Node.ID;
                        }
                    }
                }

            }
            else if (dfsuFile.DfsuFileType == DfsuFileType.Dfsu2D)
            {
                int Layer = 1;
                for (int j = 0; j < ElementList.Count; j++)
                {
                    ElementLayer elementLayer = new ElementLayer();
                    elementLayer.Layer = Layer;
                    elementLayer.ZMin = 0;
                    elementLayer.ZMax = 0;
                    elementLayer.Element = ElementList[j];
                    ElementLayerList.Add(elementLayer);

                    // doing Nodes
                    if (ElementList[j].Type == 21)
                    {
                        NodeLayer nl0 = new NodeLayer();
                        nl0.Layer = Layer;
                        nl0.Z = 0;
                        nl0.Node = ElementList[j].NodeList[0];

                        NodeLayer nl1 = new NodeLayer();
                        nl1.Layer = Layer;
                        nl1.Z = 0;
                        nl1.Node = ElementList[j].NodeList[1];

                        NodeLayer nl2 = new NodeLayer();
                        nl2.Layer = Layer;
                        nl2.Z = 0;
                        nl2.Node = ElementList[j].NodeList[2];

                        topNodeLayerList.Add(nl0);
                        topNodeLayerList.Add(nl1);
                        topNodeLayerList.Add(nl2);
                    }
                    else if (ElementList[j].Type == 24)
                    {
                        reportTag.Error = string.Format(ReportServiceRes.NotImplementedYet, dfsuFile.NumberOfSigmaLayers.ToString());
                        return;
                    }
                    else if (ElementList[j].Type == 25)
                    {
                        NodeLayer nl0 = new NodeLayer();
                        nl0.Layer = Layer;
                        nl0.Z = 0;
                        nl0.Node = ElementList[j].NodeList[0];

                        NodeLayer nl1 = new NodeLayer();
                        nl1.Layer = Layer;
                        nl1.Z = 0;
                        nl1.Node = ElementList[j].NodeList[1];

                        NodeLayer nl2 = new NodeLayer();
                        nl2.Layer = Layer;
                        nl2.Z = 0;
                        nl2.Node = ElementList[j].NodeList[2];

                        NodeLayer nl3 = new NodeLayer();
                        nl3.Layer = Layer;
                        nl3.Z = 0;
                        nl3.Node = ElementList[j].NodeList[3];


                        topNodeLayerList.Add(nl0);
                        topNodeLayerList.Add(nl1);
                        topNodeLayerList.Add(nl2);
                        topNodeLayerList.Add(nl3);
                    }
                }

                List<ElementLayer> TempElementLayerList = (from el in ElementLayerList
                                                           orderby el.Element.ID
                                                           select el).Distinct().ToList();

                ElementLayerList = new List<ElementLayer>();
                foreach (ElementLayer el in TempElementLayerList)
                {
                    ElementLayerList.Add(el);
                }

                List<NodeLayer> TempNodeLayerList = (from nl in topNodeLayerList
                                                     orderby nl.Node.ID
                                                     select nl).Distinct().ToList();

                topNodeLayerList = new List<NodeLayer>();
                int OldID = 0;
                foreach (NodeLayer nl in TempNodeLayerList)
                {
                    if (OldID != nl.Node.ID)
                    {
                        topNodeLayerList.Add(nl);
                        OldID = nl.Node.ID;
                    }
                }
            }
            else
            {
                reportTag.Error = string.Format(ReportServiceRes.NotImplementedYet, dfsuFile.NumberOfSigmaLayers.ToString());
                return;
            }
        }
        public void FillElementListAndNodeList(DfsuFile dfsuFile, List<Element> ElementList, List<Node> NodeList)
        {
            for (int i = 0; i < dfsuFile.NumberOfNodes; i++)
            {
                Node n = new Node()
                {
                    Code = dfsuFile.Code[i],
                    ID = dfsuFile.NodeIds[i],
                    X = (float)dfsuFile.X[i],
                    Y = (float)dfsuFile.Y[i],
                    Z = dfsuFile.Z[i],
                    Value = 0,
                    ConnectNodeList = new List<Node>(),
                    ElementList = new List<Element>()
                };
                NodeList.Add(n);
            }

            for (int i = 0; i < dfsuFile.NumberOfElements; i++)
            {
                Element el = new Element()
                {
                    ID = dfsuFile.ElementIds[i],
                    Type = dfsuFile.ElementType[i],
                    Value = 0,
                    NodeList = new List<Node>(),
                    NumbOfNodes = 0
                };
                ElementList.Add(el);
            }

            for (int i = 0; i < dfsuFile.NumberOfElements; i++)
            {
                int CountNode = 0;
                for (int j = 0; j < dfsuFile.ElementTable[i].Count(); j++)
                {
                    CountNode += 1;
                    ElementList[i].NodeList.Add(NodeList[dfsuFile.ElementTable[i][j] - 1]);
                    if (!NodeList[dfsuFile.ElementTable[i][j] - 1].ElementList.Contains(ElementList[i]))
                    {
                        NodeList[dfsuFile.ElementTable[i][j] - 1].ElementList.Add(ElementList[i]);
                    }
                    for (int k = 0; k < dfsuFile.ElementTable[i].Count(); k++)
                    {
                        if (k != j)
                        {
                            if (!NodeList[dfsuFile.ElementTable[i][j] - 1].ConnectNodeList.Contains(NodeList[dfsuFile.ElementTable[i][k] - 1]))
                            {
                                NodeList[dfsuFile.ElementTable[i][j] - 1].ConnectNodeList.Add(NodeList[dfsuFile.ElementTable[i][k] - 1]);
                            }
                        }
                    }
                }
                ElementList[i].NumbOfNodes = CountNode;
            }
        }
        public string FillRequiredList(DfsuFile dfsuFile, List<ElementLayer> elementLayerList, List<Element> elementList, List<Node> nodeList, List<NodeLayer> topNodeLayerList, List<NodeLayer> bottomNodeLayerList, ReportTag reportTag)
        {
            FillElementListAndNodeList(dfsuFile, elementList, nodeList);
            FillElementLayerList(dfsuFile, elementList, elementLayerList, topNodeLayerList, bottomNodeLayerList, reportTag);
            if (!string.IsNullOrWhiteSpace(reportTag.Error))
            {
                return reportTag.Error;
            }

            return "";
        }
        public string FillTagsLevelsKML(StringBuilder sbFileText, ReportTag reportTag, int CurrentLevel)
        {
            reportTag.Level = CurrentLevel;

            foreach (ReportTag reportTagChild in reportTag.ReportTagChildList)
            {
                FillTagsLevelsKML(sbFileText, reportTagChild, CurrentLevel + 1);
            }

            return "";
        }
        public string FillTagsParentChildRelationshipKML(StringBuilder sbFileText, List<ReportTag> reportTagList)
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

            }

            return "";
        }
        public string FillTagsVariablesKML(StringBuilder sbFileText, List<ReportTag> reportTagList)
        {
            string retStr = "";
            foreach (ReportTag reportTag in reportTagList)
            {
                bool IsDBFiltering = true;
                retStr = GetReportTreeNodesFromTagText(reportTag.KMLTagText, reportTag.TagItem, reportTag.ReportType, reportTag.ReportTreeNodeList, IsDBFiltering);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    sbFileText.Insert(0, "\r\n\r\n" + retStr + "\r\n\r\n");
                    return retStr;
                }
            }

            return "";
        }
        public string FillTemplateWithDBInfoKML(StringBuilder sbFileText, ReportTag reportTag)
        {
            string retStr = GenerateNextReportTagKML(sbFileText, reportTag);
            if (!string.IsNullOrWhiteSpace(retStr))
            {
                sbFileText.AppendLine("");
                sbFileText.AppendLine(retStr);
            }

            return "";
        }
        private void FillVectors21_32(Element el, List<Element> UniqueElementList, float ContourValue, bool Is3D, bool IsTop, ReportTag reportTag)
        {
            Node Node0 = new Node();
            Node Node1 = new Node();
            Node Node2 = new Node();
            if (Is3D && IsTop)
            {
                Node0 = el.NodeList[3];
                Node1 = el.NodeList[4];
                Node2 = el.NodeList[5];
            }
            else
            {
                Node0 = el.NodeList[0];
                Node1 = el.NodeList[1];
                Node2 = el.NodeList[2];
            }

            int ElemCount01 = (from el1 in UniqueElementList
                               from el2 in Node0.ElementList
                               from el3 in Node1.ElementList
                               where el1 == el2 && el1 == el3
                               select el1).Count();

            int ElemCount02 = (from el1 in UniqueElementList
                               from el2 in Node0.ElementList
                               from el3 in Node2.ElementList
                               where el1 == el2 && el1 == el3
                               select el1).Count();

            int ElemCount12 = (from el1 in UniqueElementList
                               from el2 in Node1.ElementList
                               from el3 in Node2.ElementList
                               where el1 == el2 && el1 == el3
                               select el1).Count();

            if (Node0.Value >= ContourValue && Node1.Value >= ContourValue && Node2.Value >= ContourValue)
            {
                if (Node0.Code != 0 && Node1.Code != 0 && ElemCount01 == 1)
                {
                    ForwardVector.Add(Node0.ID.ToString() + "," + Node1.ID.ToString(), new Vector() { StartNode = Node0, EndNode = Node1 });
                    BackwardVector.Add(Node1.ID.ToString() + "," + Node0.ID.ToString(), new Vector() { StartNode = Node1, EndNode = Node0 });
                }
                if (Node0.Code != 0 && Node2.Code != 0 && ElemCount02 == 1)
                {
                    ForwardVector.Add(Node0.ID.ToString() + "," + Node2.ID.ToString(), new Vector() { StartNode = Node0, EndNode = Node2 });
                    BackwardVector.Add(Node2.ID.ToString() + "," + Node0.ID.ToString(), new Vector() { StartNode = Node2, EndNode = Node0 });
                }
                if (Node1.Code != 0 && Node2.Code != 0 && ElemCount12 == 1)
                {
                    ForwardVector.Add(Node1.ID.ToString() + "," + Node2.ID.ToString(), new Vector() { StartNode = Node1, EndNode = Node2 });
                    BackwardVector.Add(Node2.ID.ToString() + "," + Node1.ID.ToString(), new Vector() { StartNode = Node2, EndNode = Node1 });
                }
            }
            else if (Node0.Value >= ContourValue && Node1.Value >= ContourValue && Node2.Value < ContourValue)
            {
                if (Node0.Code != 0 && Node1.Code != 0 && ElemCount01 == 1)
                {
                    ForwardVector.Add(Node0.ID.ToString() + "," + Node1.ID.ToString(), new Vector() { StartNode = Node0, EndNode = Node1 });
                    BackwardVector.Add(Node1.ID.ToString() + "," + Node0.ID.ToString(), new Vector() { StartNode = Node1, EndNode = Node0 });
                }
                Node TempInt1 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node0.ID * 100000 + Node2.ID).First();
                if (Node0.Code != 0 && Node2.Code != 0 && ElemCount02 == 1)
                {
                    if (TempInt1 != null)
                    {
                        ForwardVector.Add(Node0.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = Node0, EndNode = TempInt1 });
                        BackwardVector.Add(TempInt1.ID.ToString() + "," + Node0.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = Node0 });
                    }


                }
                Node TempInt2 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node1.ID * 100000 + Node2.ID).First();
                if (Node1.Code != 0 && Node2.Code != 0 && ElemCount12 == 1)
                {
                    if (TempInt2 != null)
                    {
                        ForwardVector.Add(Node1.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = Node1, EndNode = TempInt2 });
                        BackwardVector.Add(TempInt2.ID.ToString() + "," + Node1.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = Node1 });
                    }
                }

                if (TempInt1 != null && TempInt2 != null)
                {
                    ForwardVector.Add(TempInt1.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = TempInt2 });
                    BackwardVector.Add(TempInt2.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = TempInt1 });
                }
            }
            else if (Node0.Value >= ContourValue && Node1.Value < ContourValue && Node2.Value >= ContourValue)
            {
                if (Node0.Code != 0 && Node2.Code != 0 && ElemCount02 == 1)
                {
                    ForwardVector.Add(Node0.ID.ToString() + "," + Node2.ID.ToString(), new Vector() { StartNode = Node0, EndNode = Node2 });
                    BackwardVector.Add(Node2.ID.ToString() + "," + Node0.ID.ToString(), new Vector() { StartNode = Node2, EndNode = Node0 });
                }
                Node TempInt1 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node0.ID * 100000 + Node1.ID).First();
                if (Node0.Code != 0 && Node1.Code != 0 && ElemCount01 == 1)
                {
                    if (TempInt1 != null)
                    {
                        ForwardVector.Add(Node0.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = Node0, EndNode = TempInt1 });
                        BackwardVector.Add(TempInt1.ID.ToString() + "," + Node0.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = Node0 });
                    }


                }
                Node TempInt2 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node2.ID * 100000 + Node1.ID).First();
                if (Node1.Code != 0 && Node2.Code != 0 && ElemCount12 == 1)
                {
                    if (TempInt2 != null)
                    {
                        ForwardVector.Add(Node2.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = Node2, EndNode = TempInt2 });
                        BackwardVector.Add(TempInt2.ID.ToString() + "," + Node2.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = Node2 });
                    }
                }

                if (TempInt1 != null && TempInt2 != null)
                {
                    ForwardVector.Add(TempInt1.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = TempInt2 });
                    BackwardVector.Add(TempInt2.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = TempInt1 });
                }
            }
            else if (Node0.Value >= ContourValue && Node1.Value < ContourValue && Node2.Value < ContourValue)
            {
                Node TempInt1 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node0.ID * 100000 + Node1.ID).First();
                if (Node0.Code != 0 && Node1.Code != 0 && ElemCount01 == 1)
                {
                    if (TempInt1 != null)
                    {
                        ForwardVector.Add(Node0.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = Node0, EndNode = TempInt1 });
                        BackwardVector.Add(TempInt1.ID.ToString() + "," + Node0.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = Node0 });
                    }


                }
                Node TempInt2 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node0.ID * 100000 + Node2.ID).First();
                if (Node0.Code != 0 && Node2.Code != 0 && ElemCount02 == 1)
                {
                    if (TempInt2 != null)
                    {
                        ForwardVector.Add(Node0.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = Node0, EndNode = TempInt2 });
                        BackwardVector.Add(TempInt2.ID.ToString() + "," + Node0.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = Node0 });
                    }
                }

                if (TempInt1 != null && TempInt2 != null)
                {
                    ForwardVector.Add(TempInt1.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = TempInt2 });
                    BackwardVector.Add(TempInt2.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = TempInt1 });
                }
            }
            else if (Node0.Value < ContourValue && Node1.Value >= ContourValue && Node2.Value >= ContourValue)
            {
                if (Node1.Code != 0 && Node2.Code != 0 && ElemCount12 == 1)
                {
                    ForwardVector.Add(Node1.ID.ToString() + "," + Node2.ID.ToString(), new Vector() { StartNode = Node1, EndNode = Node2 });
                    BackwardVector.Add(Node2.ID.ToString() + "," + Node1.ID.ToString(), new Vector() { StartNode = Node2, EndNode = Node1 });
                }
                Node TempInt1 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node1.ID * 100000 + Node0.ID).First();
                if (Node0.Code != 0 && Node1.Code != 0 && ElemCount01 == 1)
                {
                    if (TempInt1 != null)
                    {
                        ForwardVector.Add(Node1.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = Node1, EndNode = TempInt1 });
                        BackwardVector.Add(TempInt1.ID.ToString() + "," + Node1.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = Node1 });
                    }


                }
                Node TempInt2 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node2.ID * 100000 + Node0.ID).First();
                if (Node0.Code != 0 && Node2.Code != 0 && ElemCount02 == 1)
                {
                    if (TempInt2 != null)
                    {
                        ForwardVector.Add(Node2.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = Node2, EndNode = TempInt2 });
                        BackwardVector.Add(TempInt2.ID.ToString() + "," + Node2.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = Node2 });
                    }
                }

                if (TempInt1 != null && TempInt2 != null)
                {
                    ForwardVector.Add(TempInt1.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = TempInt2 });
                    BackwardVector.Add(TempInt2.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = TempInt1 });
                }
            }
            else if (Node0.Value < ContourValue && Node1.Value >= ContourValue && Node2.Value < ContourValue)
            {
                Node TempInt1 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node1.ID * 100000 + Node0.ID).First();
                if (Node0.Code != 0 && Node1.Code != 0 && ElemCount01 == 1)
                {
                    if (TempInt1 != null)
                    {
                        ForwardVector.Add(Node1.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = Node1, EndNode = TempInt1 });
                        BackwardVector.Add(TempInt1.ID.ToString() + "," + Node1.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = Node1 });
                    }


                }
                Node TempInt2 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node1.ID * 100000 + Node2.ID).First();
                if (Node1.Code != 0 && Node2.Code != 0 && ElemCount12 == 1)
                {
                    if (TempInt2 != null)
                    {
                        ForwardVector.Add(Node1.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = Node1, EndNode = TempInt2 });
                        BackwardVector.Add(TempInt2.ID.ToString() + "," + Node1.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = Node1 });
                    }
                }

                if (TempInt1 != null && TempInt2 != null)
                {
                    ForwardVector.Add(TempInt1.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = TempInt2 });
                    BackwardVector.Add(TempInt2.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = TempInt1 });
                }
            }
            else if (Node0.Value < ContourValue && Node1.Value < ContourValue && Node2.Value >= ContourValue)
            {
                Node TempInt1 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node2.ID * 100000 + Node0.ID).First();
                if (Node0.Code != 0 && Node2.Code != 0 && ElemCount02 == 1)
                {
                    if (TempInt1 != null)
                    {
                        ForwardVector.Add(Node2.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = Node2, EndNode = TempInt1 });
                        BackwardVector.Add(TempInt1.ID.ToString() + "," + Node2.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = Node2 });
                    }


                }
                Node TempInt2 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node2.ID * 100000 + Node1.ID).First();
                if (Node1.Code != 0 && Node2.Code != 0 && ElemCount12 == 1)
                {
                    if (TempInt2 != null)
                    {
                        ForwardVector.Add(Node2.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = Node2, EndNode = TempInt2 });
                        BackwardVector.Add(TempInt2.ID.ToString() + "," + Node2.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = Node2 });
                    }
                }

                if (TempInt1 != null && TempInt2 != null)
                {
                    ForwardVector.Add(TempInt1.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = TempInt2 });
                    BackwardVector.Add(TempInt2.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = TempInt1 });
                }
            }
            else if (Node0.Value < ContourValue && Node1.Value < ContourValue && Node2.Value < ContourValue)
            {
                // no vector to create
            }
            else
            {
                reportTag.Error = ReportServiceRes.AllNodesAreSmallerThanContourValue;
                return;
            }
        }
        private void FillVectors25_33(Element el, List<Element> UniqueElementList, float ContourValue, bool Is3D, bool IsTop, ReportTag reportTag)
        {

            Node Node0 = new Node();
            Node Node1 = new Node();
            Node Node2 = new Node();
            Node Node3 = new Node();

            if (Is3D && IsTop)
            {
                Node0 = el.NodeList[4];
                Node1 = el.NodeList[5];
                Node2 = el.NodeList[6];
                Node3 = el.NodeList[7];
            }
            else
            {
                Node0 = el.NodeList[0];
                Node1 = el.NodeList[1];
                Node2 = el.NodeList[2];
                Node3 = el.NodeList[3];
            }

            int ElemCount01 = (from el1 in UniqueElementList
                               from el2 in Node0.ElementList
                               from el3 in Node1.ElementList
                               where el1 == el2 && el1 == el3
                               select el1).Count();

            int ElemCount03 = (from el1 in UniqueElementList
                               from el2 in Node0.ElementList
                               from el3 in Node3.ElementList
                               where el1 == el2 && el1 == el3
                               select el1).Count();

            int ElemCount12 = (from el1 in UniqueElementList
                               from el2 in Node1.ElementList
                               from el3 in Node2.ElementList
                               where el1 == el2 && el1 == el3
                               select el1).Count();

            int ElemCount23 = (from el1 in UniqueElementList
                               from el2 in Node2.ElementList
                               from el3 in Node3.ElementList
                               where el1 == el2 && el1 == el3
                               select el1).Count();

            if (Node0.Value >= ContourValue && Node1.Value >= ContourValue && Node2.Value >= ContourValue && Node3.Value >= ContourValue)
            {
                if (Node0.Code != 0 && Node1.Code != 0 && ElemCount01 == 1)
                {
                    ForwardVector.Add(Node0.ID.ToString() + "," + Node1.ID.ToString(), new Vector() { StartNode = Node0, EndNode = Node1 });
                    BackwardVector.Add(Node1.ID.ToString() + "," + Node0.ID.ToString(), new Vector() { StartNode = Node1, EndNode = Node0 });
                }
                if (Node0.Code != 0 && Node2.Code != 0 && ElemCount03 == 1)
                {
                    ForwardVector.Add(Node0.ID.ToString() + "," + Node3.ID.ToString(), new Vector() { StartNode = Node0, EndNode = Node3 });
                    BackwardVector.Add(Node3.ID.ToString() + "," + Node0.ID.ToString(), new Vector() { StartNode = Node3, EndNode = Node0 });
                }
                if (Node1.Code != 0 && Node2.Code != 0 && ElemCount12 == 1)
                {
                    ForwardVector.Add(Node1.ID.ToString() + "," + Node2.ID.ToString(), new Vector() { StartNode = Node1, EndNode = Node2 });
                    BackwardVector.Add(Node2.ID.ToString() + "," + Node1.ID.ToString(), new Vector() { StartNode = Node2, EndNode = Node1 });
                }
                if (Node2.Code != 0 && Node3.Code != 0 && ElemCount23 == 1)
                {
                    ForwardVector.Add(Node2.ID.ToString() + "," + Node3.ID.ToString(), new Vector() { StartNode = Node2, EndNode = Node3 });
                    BackwardVector.Add(Node3.ID.ToString() + "," + Node2.ID.ToString(), new Vector() { StartNode = Node3, EndNode = Node2 });
                }
            }
            else if (Node0.Value >= ContourValue && Node1.Value >= ContourValue && Node2.Value >= ContourValue && Node3.Value < ContourValue)
            {
                if (Node0.Code != 0 && Node1.Code != 0 && ElemCount01 == 1)
                {
                    ForwardVector.Add(Node0.ID.ToString() + "," + Node1.ID.ToString(), new Vector() { StartNode = Node0, EndNode = Node1 });
                    BackwardVector.Add(Node1.ID.ToString() + "," + Node0.ID.ToString(), new Vector() { StartNode = Node1, EndNode = Node0 });
                }
                if (Node1.Code != 0 && Node2.Code != 0 && ElemCount12 == 1)
                {
                    ForwardVector.Add(Node1.ID.ToString() + "," + Node2.ID.ToString(), new Vector() { StartNode = Node1, EndNode = Node2 });
                    BackwardVector.Add(Node2.ID.ToString() + "," + Node1.ID.ToString(), new Vector() { StartNode = Node2, EndNode = Node1 });
                }
                Node TempInt1 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node0.ID * 100000 + Node3.ID).First();
                if (Node0.Code != 0 && Node3.Code != 0 && ElemCount03 == 1)
                {
                    if (TempInt1 != null)
                    {
                        ForwardVector.Add(Node0.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = Node0, EndNode = TempInt1 });
                        BackwardVector.Add(TempInt1.ID.ToString() + "," + Node0.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = Node0 });
                    }


                }
                Node TempInt2 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node2.ID * 100000 + Node3.ID).First();
                if (Node2.Code != 0 && Node3.Code != 0 && ElemCount23 == 1)
                {
                    if (TempInt2 != null)
                    {
                        ForwardVector.Add(Node2.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = Node2, EndNode = TempInt2 });
                        BackwardVector.Add(TempInt2.ID.ToString() + "," + Node2.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = Node2 });
                    }
                }

                if (TempInt1 != null && TempInt2 != null)
                {
                    ForwardVector.Add(TempInt1.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = TempInt2 });
                    BackwardVector.Add(TempInt2.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = TempInt1 });
                }
            }
            else if (Node0.Value >= ContourValue && Node1.Value >= ContourValue && Node2.Value < ContourValue && Node3.Value >= ContourValue)
            {
                if (Node0.Code != 0 && Node1.Code != 0 && ElemCount01 == 1)
                {
                    ForwardVector.Add(Node0.ID.ToString() + "," + Node1.ID.ToString(), new Vector() { StartNode = Node0, EndNode = Node1 });
                    BackwardVector.Add(Node1.ID.ToString() + "," + Node0.ID.ToString(), new Vector() { StartNode = Node1, EndNode = Node0 });
                }
                if (Node0.Code != 0 && Node3.Code != 0 && ElemCount03 == 1)
                {
                    ForwardVector.Add(Node0.ID.ToString() + "," + Node3.ID.ToString(), new Vector() { StartNode = Node0, EndNode = Node3 });
                    BackwardVector.Add(Node3.ID.ToString() + "," + Node0.ID.ToString(), new Vector() { StartNode = Node3, EndNode = Node0 });
                }
                Node TempInt1 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node1.ID * 100000 + Node2.ID).First();
                if (Node1.Code != 0 && Node2.Code != 0 && ElemCount12 == 1)
                {
                    if (TempInt1 != null)
                    {
                        ForwardVector.Add(Node1.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = Node1, EndNode = TempInt1 });
                        BackwardVector.Add(TempInt1.ID.ToString() + "," + Node1.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = Node1 });
                    }


                }
                Node TempInt2 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node3.ID * 100000 + Node2.ID).First();
                if (Node3.Code != 0 && Node2.Code != 0 && ElemCount23 == 1)
                {
                    if (TempInt2 != null)
                    {
                        ForwardVector.Add(Node3.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = Node3, EndNode = TempInt2 });
                        BackwardVector.Add(TempInt2.ID.ToString() + "," + Node3.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = Node3 });
                    }
                }

                if (TempInt1 != null && TempInt2 != null)
                {
                    ForwardVector.Add(TempInt1.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = TempInt2 });
                    BackwardVector.Add(TempInt2.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = TempInt1 });
                }
            }
            else if (Node0.Value >= ContourValue && Node1.Value < ContourValue && Node2.Value >= ContourValue && Node3.Value >= ContourValue)
            {
                if (Node0.Code != 0 && Node3.Code != 0 && ElemCount03 == 1)
                {
                    ForwardVector.Add(Node0.ID.ToString() + "," + Node3.ID.ToString(), new Vector() { StartNode = Node0, EndNode = Node3 });
                    BackwardVector.Add(Node3.ID.ToString() + "," + Node0.ID.ToString(), new Vector() { StartNode = Node3, EndNode = Node0 });
                }
                if (Node2.Code != 0 && Node3.Code != 0 && ElemCount23 == 1)
                {
                    ForwardVector.Add(Node2.ID.ToString() + "," + Node3.ID.ToString(), new Vector() { StartNode = Node2, EndNode = Node3 });
                    BackwardVector.Add(Node3.ID.ToString() + "," + Node2.ID.ToString(), new Vector() { StartNode = Node3, EndNode = Node2 });
                }
                Node TempInt1 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node0.ID * 100000 + Node1.ID).First();
                if (Node0.Code != 0 && Node1.Code != 0 && ElemCount01 == 1)
                {
                    if (TempInt1 != null)
                    {
                        ForwardVector.Add(Node0.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = Node0, EndNode = TempInt1 });
                        BackwardVector.Add(TempInt1.ID.ToString() + "," + Node0.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = Node0 });
                    }


                }
                Node TempInt2 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node2.ID * 100000 + Node1.ID).First();
                if (Node2.Code != 0 && Node1.Code != 0 && ElemCount12 == 1)
                {
                    if (TempInt2 != null)
                    {
                        ForwardVector.Add(Node2.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = Node2, EndNode = TempInt2 });
                        BackwardVector.Add(TempInt2.ID.ToString() + "," + Node2.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = Node2 });
                    }
                }

                if (TempInt1 != null && TempInt2 != null)
                {
                    ForwardVector.Add(TempInt1.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = TempInt2 });
                    BackwardVector.Add(TempInt2.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = TempInt1 });
                }
            }
            else if (Node0.Value < ContourValue && Node1.Value >= ContourValue && Node2.Value >= ContourValue && Node3.Value >= ContourValue)
            {
                if (Node1.Code != 0 && Node2.Code != 0 && ElemCount12 == 1)
                {
                    ForwardVector.Add(Node1.ID.ToString() + "," + Node2.ID.ToString(), new Vector() { StartNode = Node1, EndNode = Node2 });
                    BackwardVector.Add(Node2.ID.ToString() + "," + Node1.ID.ToString(), new Vector() { StartNode = Node2, EndNode = Node1 });
                }
                if (Node2.Code != 0 && Node3.Code != 0 && ElemCount23 == 1)
                {
                    ForwardVector.Add(Node2.ID.ToString() + "," + Node3.ID.ToString(), new Vector() { StartNode = Node2, EndNode = Node3 });
                    BackwardVector.Add(Node3.ID.ToString() + "," + Node2.ID.ToString(), new Vector() { StartNode = Node3, EndNode = Node2 });
                }
                Node TempInt1 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node1.ID * 100000 + Node0.ID).First();
                if (Node1.Code != 0 && Node0.Code != 0 && ElemCount01 == 1)
                {
                    if (TempInt1 != null)
                    {
                        ForwardVector.Add(Node1.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = Node1, EndNode = TempInt1 });
                        BackwardVector.Add(TempInt1.ID.ToString() + "," + Node1.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = Node1 });
                    }


                }
                Node TempInt2 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node3.ID * 100000 + Node0.ID).First();
                if (Node3.Code != 0 && Node0.Code != 0 && ElemCount03 == 1)
                {
                    if (TempInt2 != null)
                    {
                        ForwardVector.Add(Node3.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = Node3, EndNode = TempInt2 });
                        BackwardVector.Add(TempInt2.ID.ToString() + "," + Node3.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = Node3 });
                    }
                }

                if (TempInt1 != null && TempInt2 != null)
                {
                    ForwardVector.Add(TempInt1.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = TempInt2 });
                    BackwardVector.Add(TempInt2.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = TempInt1 });
                }
            }
            else if (Node0.Value >= ContourValue && Node1.Value >= ContourValue && Node2.Value < ContourValue && Node3.Value < ContourValue)
            {
                if (Node0.Code != 0 && Node1.Code != 0 && ElemCount01 == 1)
                {
                    ForwardVector.Add(Node0.ID.ToString() + "," + Node1.ID.ToString(), new Vector() { StartNode = Node0, EndNode = Node1 });
                    BackwardVector.Add(Node1.ID.ToString() + "," + Node0.ID.ToString(), new Vector() { StartNode = Node1, EndNode = Node0 });
                }
                Node TempInt1 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node0.ID * 100000 + Node3.ID).First();
                if (Node0.Code != 0 && Node3.Code != 0 && ElemCount03 == 1)
                {
                    if (TempInt1 != null)
                    {
                        ForwardVector.Add(Node0.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = Node0, EndNode = TempInt1 });
                        BackwardVector.Add(TempInt1.ID.ToString() + "," + Node0.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = Node0 });
                    }


                }
                Node TempInt2 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node1.ID * 100000 + Node2.ID).First();
                if (Node1.Code != 0 && Node2.Code != 0 && ElemCount12 == 1)
                {
                    if (TempInt2 != null)
                    {
                        ForwardVector.Add(Node1.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = Node1, EndNode = TempInt2 });
                        BackwardVector.Add(TempInt2.ID.ToString() + "," + Node1.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = Node1 });
                    }
                }

                if (TempInt1 != null && TempInt2 != null)
                {
                    ForwardVector.Add(TempInt1.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = TempInt2 });
                    BackwardVector.Add(TempInt2.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = TempInt1 });
                }
            }
            else if (Node0.Value >= ContourValue && Node1.Value < ContourValue && Node2.Value >= ContourValue && Node3.Value < ContourValue)
            {
                Node TempInt1 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node0.ID * 100000 + Node3.ID).First();
                if (Node0.Code != 0 && Node3.Code != 0 && ElemCount03 == 1)
                {
                    if (TempInt1 != null)
                    {
                        ForwardVector.Add(Node0.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = Node0, EndNode = TempInt1 });
                        BackwardVector.Add(TempInt1.ID.ToString() + "," + Node0.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = Node0 });
                    }


                }
                Node TempInt2 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node0.ID * 100000 + Node1.ID).First();
                if (Node0.Code != 0 && Node1.Code != 0 && ElemCount01 == 1)
                {
                    if (TempInt2 != null)
                    {
                        ForwardVector.Add(Node0.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = Node0, EndNode = TempInt2 });
                        BackwardVector.Add(TempInt2.ID.ToString() + "," + Node0.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = Node0 });
                    }
                }

                if (TempInt1 != null && TempInt2 != null)
                {
                    ForwardVector.Add(TempInt1.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = TempInt2 });
                    BackwardVector.Add(TempInt2.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = TempInt1 });
                }

                Node TempInt3 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node2.ID * 100000 + Node1.ID).First();
                if (Node2.Code != 0 && Node1.Code != 0 && ElemCount12 == 1)
                {
                    if (TempInt3 != null)
                    {
                        ForwardVector.Add(Node2.ID.ToString() + "," + TempInt3.ID.ToString(), new Vector() { StartNode = Node2, EndNode = TempInt3 });
                        BackwardVector.Add(TempInt3.ID.ToString() + "," + Node2.ID.ToString(), new Vector() { StartNode = TempInt3, EndNode = Node2 });
                    }


                }
                Node TempInt4 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node2.ID * 100000 + Node3.ID).First();
                if (Node2.Code != 0 && Node3.Code != 0 && ElemCount23 == 1)
                {
                    if (TempInt4 != null)
                    {
                        ForwardVector.Add(Node2.ID.ToString() + "," + TempInt4.ID.ToString(), new Vector() { StartNode = Node2, EndNode = TempInt4 });
                        BackwardVector.Add(TempInt4.ID.ToString() + "," + Node2.ID.ToString(), new Vector() { StartNode = TempInt4, EndNode = Node2 });
                    }
                }

                if (TempInt3 != null && TempInt4 != null)
                {
                    ForwardVector.Add(TempInt3.ID.ToString() + "," + TempInt4.ID.ToString(), new Vector() { StartNode = TempInt3, EndNode = TempInt4 });
                    BackwardVector.Add(TempInt4.ID.ToString() + "," + TempInt3.ID.ToString(), new Vector() { StartNode = TempInt4, EndNode = TempInt3 });
                }

            }
            else if (Node0.Value < ContourValue && Node1.Value >= ContourValue && Node2.Value >= ContourValue && Node3.Value < ContourValue)
            {
                Node TempInt1 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node1.ID * 100000 + Node0.ID).First();
                if (Node1.Code != 0 && Node0.Code != 0 && ElemCount01 == 1)
                {
                    if (TempInt1 != null)
                    {
                        ForwardVector.Add(Node1.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = Node1, EndNode = TempInt1 });
                        BackwardVector.Add(TempInt1.ID.ToString() + "," + Node1.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = Node1 });
                    }


                }
                Node TempInt2 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node2.ID * 100000 + Node3.ID).First();
                if (Node2.Code != 0 && Node3.Code != 0 && ElemCount23 == 1)
                {
                    if (TempInt2 != null)
                    {
                        ForwardVector.Add(Node2.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = Node2, EndNode = TempInt2 });
                        BackwardVector.Add(TempInt2.ID.ToString() + "," + Node2.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = Node2 });
                    }
                }

                if (TempInt1 != null && TempInt2 != null)
                {
                    ForwardVector.Add(TempInt1.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = TempInt2 });
                    BackwardVector.Add(TempInt2.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = TempInt1 });
                }

            }
            else if (Node0.Value >= ContourValue && Node1.Value < ContourValue && Node2.Value < ContourValue && Node3.Value >= ContourValue)
            {
                Node TempInt1 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node0.ID * 100000 + Node1.ID).First();
                if (Node0.Code != 0 && Node1.Code != 0 && ElemCount01 == 1)
                {
                    if (TempInt1 != null)
                    {
                        ForwardVector.Add(Node0.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = Node0, EndNode = TempInt1 });
                        BackwardVector.Add(TempInt1.ID.ToString() + "," + Node0.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = Node0 });
                    }


                }
                Node TempInt2 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node3.ID * 100000 + Node2.ID).First();
                if (Node3.Code != 0 && Node2.Code != 0 && ElemCount23 == 1)
                {
                    if (TempInt2 != null)
                    {
                        ForwardVector.Add(Node3.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = Node3, EndNode = TempInt2 });
                        BackwardVector.Add(TempInt2.ID.ToString() + "," + Node3.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = Node3 });
                    }
                }

                if (TempInt1 != null && TempInt2 != null)
                {
                    ForwardVector.Add(TempInt1.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = TempInt2 });
                    BackwardVector.Add(TempInt2.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = TempInt1 });
                }

            }
            else if (Node0.Value < ContourValue && Node1.Value >= ContourValue && Node2.Value < ContourValue && Node3.Value >= ContourValue)
            {
                Node TempInt1 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node3.ID * 100000 + Node0.ID).First();
                if (Node3.Code != 0 && Node0.Code != 0 && ElemCount03 == 1)
                {
                    if (TempInt1 != null)
                    {
                        ForwardVector.Add(Node3.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = Node3, EndNode = TempInt1 });
                        BackwardVector.Add(TempInt1.ID.ToString() + "," + Node3.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = Node3 });
                    }


                }
                Node TempInt2 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node3.ID * 100000 + Node2.ID).First();
                if (Node3.Code != 0 && Node2.Code != 0 && ElemCount23 == 1)
                {
                    if (TempInt2 != null)
                    {
                        ForwardVector.Add(Node3.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = Node3, EndNode = TempInt2 });
                        BackwardVector.Add(TempInt2.ID.ToString() + "," + Node3.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = Node3 });
                    }
                }

                if (TempInt1 != null && TempInt2 != null)
                {
                    ForwardVector.Add(TempInt1.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = TempInt2 });
                    BackwardVector.Add(TempInt2.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = TempInt1 });
                }

                Node TempInt3 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node1.ID * 100000 + Node0.ID).First();
                if (Node1.Code != 0 && Node0.Code != 0 && ElemCount01 == 1)
                {
                    if (TempInt3 != null)
                    {
                        ForwardVector.Add(Node1.ID.ToString() + "," + TempInt3.ID.ToString(), new Vector() { StartNode = Node1, EndNode = TempInt3 });
                        BackwardVector.Add(TempInt3.ID.ToString() + "," + Node1.ID.ToString(), new Vector() { StartNode = TempInt3, EndNode = Node1 });
                    }


                }
                Node TempInt4 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node1.ID * 100000 + Node2.ID).First();
                if (Node1.Code != 0 && Node2.Code != 0 && ElemCount12 == 1)
                {
                    if (TempInt4 != null)
                    {
                        ForwardVector.Add(Node1.ID.ToString() + "," + TempInt4.ID.ToString(), new Vector() { StartNode = Node1, EndNode = TempInt4 });
                        BackwardVector.Add(TempInt4.ID.ToString() + "," + Node1.ID.ToString(), new Vector() { StartNode = TempInt4, EndNode = Node1 });
                    }
                }

                if (TempInt3 != null && TempInt4 != null)
                {
                    ForwardVector.Add(TempInt3.ID.ToString() + "," + TempInt4.ID.ToString(), new Vector() { StartNode = TempInt3, EndNode = TempInt4 });
                    BackwardVector.Add(TempInt4.ID.ToString() + "," + TempInt3.ID.ToString(), new Vector() { StartNode = TempInt4, EndNode = TempInt3 });
                }

            }
            else if (Node0.Value < ContourValue && Node1.Value < ContourValue && Node2.Value >= ContourValue && Node3.Value >= ContourValue)
            {
                Node TempInt1 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node3.ID * 100000 + Node0.ID).First();
                if (Node3.Code != 0 && Node0.Code != 0 && ElemCount03 == 1)
                {
                    if (TempInt1 != null)
                    {
                        ForwardVector.Add(Node3.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = Node3, EndNode = TempInt1 });
                        BackwardVector.Add(TempInt1.ID.ToString() + "," + Node3.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = Node3 });
                    }


                }
                Node TempInt2 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node2.ID * 100000 + Node1.ID).First();
                if (Node2.Code != 0 && Node1.Code != 0 && ElemCount12 == 1)
                {
                    if (TempInt2 != null)
                    {
                        ForwardVector.Add(Node2.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = Node2, EndNode = TempInt2 });
                        BackwardVector.Add(TempInt2.ID.ToString() + "," + Node2.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = Node2 });
                    }
                }

                if (TempInt1 != null && TempInt2 != null)
                {
                    ForwardVector.Add(TempInt1.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = TempInt2 });
                    BackwardVector.Add(TempInt2.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = TempInt1 });
                }

            }
            else if (Node0.Value >= ContourValue && Node1.Value < ContourValue && Node2.Value < ContourValue && Node3.Value < ContourValue)
            {
                Node TempInt1 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node0.ID * 100000 + Node1.ID).First();
                if (Node0.Code != 0 && Node1.Code != 0 && ElemCount01 == 1)
                {
                    if (TempInt1 != null)
                    {
                        ForwardVector.Add(Node0.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = Node0, EndNode = TempInt1 });
                        BackwardVector.Add(TempInt1.ID.ToString() + "," + Node0.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = Node0 });
                    }


                }
                Node TempInt2 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node0.ID * 100000 + Node3.ID).First();
                if (Node0.Code != 0 && Node3.Code != 0 && ElemCount03 == 1)
                {
                    if (TempInt2 != null)
                    {
                        ForwardVector.Add(Node0.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = Node0, EndNode = TempInt2 });
                        BackwardVector.Add(TempInt2.ID.ToString() + "," + Node0.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = Node0 });
                    }
                }

                if (TempInt1 != null && TempInt2 != null)
                {
                    ForwardVector.Add(TempInt1.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = TempInt2 });
                    BackwardVector.Add(TempInt2.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = TempInt1 });
                }

            }
            else if (Node0.Value < ContourValue && Node1.Value >= ContourValue && Node2.Value < ContourValue && Node3.Value < ContourValue)
            {
                Node TempInt1 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node1.ID * 100000 + Node0.ID).First();
                if (Node1.Code != 0 && Node0.Code != 0 && ElemCount01 == 1)
                {
                    if (TempInt1 != null)
                    {
                        ForwardVector.Add(Node1.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = Node1, EndNode = TempInt1 });
                        BackwardVector.Add(TempInt1.ID.ToString() + "," + Node1.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = Node1 });
                    }


                }
                Node TempInt2 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node1.ID * 100000 + Node2.ID).First();
                if (Node1.Code != 0 && Node2.Code != 0 && ElemCount12 == 1)
                {
                    if (TempInt2 != null)
                    {
                        ForwardVector.Add(Node1.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = Node1, EndNode = TempInt2 });
                        BackwardVector.Add(TempInt2.ID.ToString() + "," + Node1.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = Node1 });
                    }
                }

                if (TempInt1 != null && TempInt2 != null)
                {
                    ForwardVector.Add(TempInt1.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = TempInt2 });
                    BackwardVector.Add(TempInt2.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = TempInt1 });
                }

            }
            else if (Node0.Value < ContourValue && Node1.Value < ContourValue && Node2.Value >= ContourValue && Node3.Value < ContourValue)
            {
                Node TempInt1 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node2.ID * 100000 + Node1.ID).First();
                if (Node2.Code != 0 && Node1.Code != 0 && ElemCount12 == 1)
                {
                    if (TempInt1 != null)
                    {
                        ForwardVector.Add(Node2.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = Node2, EndNode = TempInt1 });
                        BackwardVector.Add(TempInt1.ID.ToString() + "," + Node2.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = Node2 });
                    }


                }
                Node TempInt2 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node2.ID * 100000 + Node3.ID).First();
                if (Node2.Code != 0 && Node3.Code != 0 && ElemCount23 == 1)
                {
                    if (TempInt2 != null)
                    {
                        ForwardVector.Add(Node2.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = Node2, EndNode = TempInt2 });
                        BackwardVector.Add(TempInt2.ID.ToString() + "," + Node2.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = Node2 });
                    }
                }

                if (TempInt1 != null && TempInt2 != null)
                {
                    ForwardVector.Add(TempInt1.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = TempInt2 });
                    BackwardVector.Add(TempInt2.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = TempInt1 });
                }

            }
            else if (Node0.Value < ContourValue && Node1.Value < ContourValue && Node2.Value < ContourValue && Node3.Value >= ContourValue)
            {
                Node TempInt1 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node3.ID * 100000 + Node0.ID).First();
                if (Node3.Code != 0 && Node0.Code != 0 && ElemCount03 == 1)
                {
                    if (TempInt1 != null)
                    {
                        ForwardVector.Add(Node3.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = Node3, EndNode = TempInt1 });
                        BackwardVector.Add(TempInt1.ID.ToString() + "," + Node3.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = Node3 });
                    }


                }
                Node TempInt2 = InterpolatedContourNodeList.Where(IntNode => IntNode.ID == Node3.ID * 100000 + Node2.ID).First();
                if (Node3.Code != 0 && Node2.Code != 0 && ElemCount23 == 1)
                {
                    if (TempInt2 != null)
                    {
                        ForwardVector.Add(Node3.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = Node3, EndNode = TempInt2 });
                        BackwardVector.Add(TempInt2.ID.ToString() + "," + Node3.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = Node3 });
                    }
                }

                if (TempInt1 != null && TempInt2 != null)
                {
                    ForwardVector.Add(TempInt1.ID.ToString() + "," + TempInt2.ID.ToString(), new Vector() { StartNode = TempInt1, EndNode = TempInt2 });
                    BackwardVector.Add(TempInt2.ID.ToString() + "," + TempInt1.ID.ToString(), new Vector() { StartNode = TempInt2, EndNode = TempInt1 });
                }

            }
            else if (Node0.Value < ContourValue && Node1.Value < ContourValue && Node2.Value < ContourValue && Node3.Value < ContourValue)
            {
                // no vector to create
            }
            else
            {
                reportTag.Error = ReportServiceRes.AllNodesAreSmallerThanContourValue;
                return;
            }
        }
        public string GenerateForReportTagKML<T>(StringBuilder sbNewFileText, ReportTag reportTag) where T : new()
        {
            if (reportTag.TagItem == "Mike_Scenario_Special_Result_KML")
            {
                if (reportTag.ReportTreeNodeList.Count == 0)
                {
                    return "reportTag.ReportTreeNodeList count should be > 0";
                }

                if (reportTag.ReportTreeNodeList[0].dbFilteringEnumFieldList.Count == 0)
                {
                    return "reportTag.ReportTreeNodeList[0].dbFilteringEnumFieldList count should be > 0";
                }

                string condText = reportTag.ReportTreeNodeList[0].dbFilteringEnumFieldList[0].EnumConditionText;

                switch (condText)
                {
                    case "Mesh":
                        {
                            GenerateKMLMesh(sbNewFileText, reportTag);
                        }
                        break;
                    case "BoundaryConditions":
                        {
                            GenerateKMLBoundaryConditions(sbNewFileText, reportTag);
                        }
                        break;
                    case "StudyArea":
                        {
                            GenerateKMLStudyArea(sbNewFileText, reportTag);
                        }
                        break;
                    case "PollutionLimit":
                        {
                            GenerateKMLPollutionLimit(sbNewFileText, reportTag);
                        }
                        break;
                    case "PollutionAnimation":
                        {
                            GenerateKMLPollutionAnimation(sbNewFileText, reportTag);
                        }
                        break;
                    default:
                        {
                            return condText + " is not a recognized option it should be one of [Mesh, BoundaryConditions, StudyArea, PollutionLimit, PollutionAnimation]";
                        }
                }
            }
            else
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
                    //string PreTextOfLineAtLevel = PreTextOfLine;

                    foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                    {
                        if (propertyInfo.Name == reportTag.TagItem + "_Error")
                        {
                            string errStr = (string)propertyInfo.GetValue(reportModel);
                            if (!string.IsNullOrWhiteSpace(errStr))
                            {
                                //PreTextOfLineAtLevel += "ERROR: " + errStr;
                                //sbNewFileText.Append("ERROR: " + errStr);
                                //return PreTextOfLineAtLevel;

                                return "eeeeeeeeeeeee";
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
                                    //PreTextOfLineAtLevel += "empty" + ",";

                                    return "dddddddddddddd";
                                }
                                else
                                {
                                    return "hhhhhhhhhhhh";

                                    //PreTextOfLineAtLevel += ((string)ReportGetFieldTextOrValue<T>(reportModel, true, propertyInfo, "", reportTag, reportTag.ReportTreeNodeList[i])).Replace(",", " ").Replace("\t", " ").Replace("\r\n", " ") + ",";
                                    //if (!string.IsNullOrWhiteSpace(reportTag.Error))
                                    //    return reportTag.Error;

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
                            retStr = GenerateNextReportTagKML(sbNewFileText, reportTagChild);
                            if (!string.IsNullOrWhiteSpace(retStr))
                                return retStr;
                        }
                    }
                    else
                    {
                        //PreTextOfLine = PreTextOfLineAtLevel;
                        //sbNewFileText.AppendLine(PreTextOfLineAtLevel.Substring(0, PreTextOfLineAtLevel.Length - 1));
                    }
                }
            }

            return "";

        }
        public string GenerateKMLBoundaryConditions(StringBuilder sbNewFileText, ReportTag reportTag)
        {
            #region Boundary Condition Nodes
            WriteKMLTop(sbNewFileText);
            sbNewFileText.AppendLine(string.Format(@"<name>{0}</name>", ReportServiceRes.MIKEBoundaryConditions + "_" + GetScenarioName(reportTag)));
            WriteKMLBoundaryConditionNode(sbNewFileText, reportTag);
            WriteKMLBottom(sbNewFileText, reportTag);

            #endregion Boundary Condition Nodes

            return "";
        }
        public string GenerateKMLMesh(StringBuilder sbNewFileText, ReportTag reportTag)
        {
            #region Mesh
            DfsuFile dfsuFile = null;
            List<ElementLayer> elementLayerList = new List<ElementLayer>();
            List<Element> elementList = new List<Element>();
            List<Node> nodeList = new List<Node>();
            List<NodeLayer> topNodeLayerList = new List<NodeLayer>();
            List<NodeLayer> bottomNodeLayerList = new List<NodeLayer>();

            dfsuFile = GetHydrodynamicDfsuFile(reportTag);
            string retStr = FillRequiredList(dfsuFile, elementLayerList, elementList, nodeList, topNodeLayerList, bottomNodeLayerList, reportTag);

            WriteKMLTop(sbNewFileText);
            sbNewFileText.AppendLine(string.Format(@"<name>{0}</name>", ReportServiceRes.MIKEMesh + "_" + GetScenarioName(reportTag)));
            WriteKMLMesh(sbNewFileText, elementLayerList, reportTag);
            WriteKMLBottom(sbNewFileText, reportTag);
            dfsuFile.Close();
            #endregion Mesh

            return "";
        }
        public string GenerateKMLPollutionAnimation(StringBuilder sbNewFileText, ReportTag reportTag)
        {
            #region PollutionAnimation
            DfsuFile dfsuFile = null;
            List<ElementLayer> elementLayerList = new List<ElementLayer>();
            List<Element> elementList = new List<Element>();
            List<Node> nodeList = new List<Node>();
            List<NodeLayer> topNodeLayerList = new List<NodeLayer>();
            List<NodeLayer> bottomNodeLayerList = new List<NodeLayer>();

            dfsuFile = GetTransportDfsuFile(reportTag);
            string retStr = FillRequiredList(dfsuFile, elementLayerList, elementList, nodeList, topNodeLayerList, bottomNodeLayerList, reportTag);

            WriteKMLTop(sbNewFileText);
            sbNewFileText.AppendLine(string.Format(@"<name>{0}</name>", ReportServiceRes.MIKEPollutionAnimation + "_" + GetScenarioName(reportTag)));
            WriteKMLStyleModelInput(sbNewFileText);
            DrawKMLContourStyle(sbNewFileText);
            WriteKMLModelInput(sbNewFileText, reportTag);
            WriteKMLFecalColiformContourLine(dfsuFile, sbNewFileText, elementLayerList, topNodeLayerList, bottomNodeLayerList, reportTag);
            WriteKMLBottom(sbNewFileText, reportTag);
            dfsuFile.Close();
            #endregion PollutionAnimation

            return "";
        }
        public string GenerateKMLPollutionLimit(StringBuilder sbNewFileText, ReportTag reportTag)
        {
            #region PollutionLimit
            DfsuFile dfsuFile = null;
            List<ElementLayer> elementLayerList = new List<ElementLayer>();
            List<Element> elementList = new List<Element>();
            List<Node> nodeList = new List<Node>();
            List<NodeLayer> topNodeLayerList = new List<NodeLayer>();
            List<NodeLayer> bottomNodeLayerList = new List<NodeLayer>();

            dfsuFile = GetTransportDfsuFile(reportTag);
            string retStr = FillRequiredList(dfsuFile, elementLayerList, elementList, nodeList, topNodeLayerList, bottomNodeLayerList, reportTag);

            WriteKMLTop(sbNewFileText);
            sbNewFileText.AppendLine(string.Format(@"<name>{0}</name>", ReportServiceRes.MIKEPollutionLimit + "_" + GetScenarioName(reportTag)));
            WriteKMLStyleModelInput(sbNewFileText);
            DrawKMLContourStyle(sbNewFileText);
            WriteKMLModelInput(sbNewFileText, reportTag);
            WriteKMLPollutionLimitsContourLine(dfsuFile, sbNewFileText, elementLayerList, topNodeLayerList, bottomNodeLayerList, reportTag);
            WriteKMLBottom(sbNewFileText, reportTag);
            dfsuFile.Close();
            #endregion PollutionLimit

            return "";
        }
        public string GenerateKMLStudyArea(StringBuilder sbNewFileText, ReportTag reportTag)
        {
            #region Study Area
            DfsuFile dfsuFile = null;
            List<ElementLayer> elementLayerList = new List<ElementLayer>();
            List<Element> elementList = new List<Element>();
            List<Node> nodeList = new List<Node>();
            List<NodeLayer> topNodeLayerList = new List<NodeLayer>();
            List<NodeLayer> bottomNodeLayerList = new List<NodeLayer>();

            dfsuFile = GetHydrodynamicDfsuFile(reportTag);
            string retStr = FillRequiredList(dfsuFile, elementLayerList, elementList, nodeList, topNodeLayerList, bottomNodeLayerList, reportTag);

            WriteKMLTop(sbNewFileText);
            sbNewFileText.AppendLine(string.Format(@"<name>{0}</name>", ReportServiceRes.MIKEStudyArea + "_" + GetScenarioName(reportTag)));
            WriteKMLStudyAreaLine(sbNewFileText, elementLayerList, nodeList, reportTag);
            WriteKMLBottom(sbNewFileText, reportTag);
            dfsuFile.Close();
            #endregion Study Area

            return "";
        }
        public string GenerateNextReportTagKML(StringBuilder sbNewFileText, ReportTag ReportTag)
        {
            string retStr = "";
            switch (ReportTag.TagItem)
            {
                //case "Root":
                //    retStr = GenerateForReportTagKML<ReportRootModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Country":
                //    retStr = GenerateForReportTagKML<ReportCountryModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Province":
                //    retStr = GenerateForReportTagKML<ReportProvinceModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Area":
                //    retStr = GenerateForReportTagKML<ReportAreaModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Sector":
                //    retStr = GenerateForReportTagKML<ReportSectorModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Subsector":
                //    retStr = GenerateForReportTagKML<ReportSubsectorModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "MWQM_Site":
                //    retStr = GenerateForReportTagKML<ReportMWQM_SiteModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "MWQM_Site_Sample":
                //    retStr = GenerateForReportTagKML<ReportMWQM_Site_SampleModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "MWQM_Site_Start_And_End_Date":
                //    retStr = GenerateForReportTagKML<ReportMWQM_Site_Start_And_End_DateModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "MWQM_Site_File":
                //    retStr = GenerateForReportTagKML<ReportMWQM_Site_FileModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "MWQM_Run":
                //    retStr = GenerateForReportTagKML<ReportMWQM_RunModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "MWQM_Run_Sample":
                //    retStr = GenerateForReportTagKML<ReportMWQM_Run_SampleModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "MWQM_Run_Lab_Sheet":
                //    retStr = GenerateForReportTagKML<ReportMWQM_Run_Lab_SheetModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "MWQM_Run_Lab_Sheet_Detail":
                //    retStr = GenerateForReportTagKML<ReportMWQM_Run_Lab_Sheet_DetailModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "MWQM_Run_Lab_Sheet_Tube_And_MPN_Detail":
                //    retStr = GenerateForReportTagKML<ReportMWQM_Run_Lab_Sheet_Tube_And_MPN_DetailModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "MWQM_Run_File":
                //    retStr = GenerateForReportTagKML<ReportMWQM_Run_FileModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Pol_Source_Site":
                //    retStr = GenerateForReportTagKML<ReportPol_Source_SiteModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Pol_Source_Site_Obs":
                //    retStr = GenerateForReportTagKML<ReportPol_Source_Site_ObsModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Pol_Source_Site_Obs_Issue":
                //    retStr = GenerateForReportTagKML<ReportPol_Source_Site_Obs_IssueModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Pol_Source_Site_File":
                //    retStr = GenerateForReportTagKML<ReportPol_Source_Site_FileModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Pol_Source_Site_Address":
                //    retStr = GenerateForReportTagKML<ReportPol_Source_Site_AddressModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Municipality":
                //    retStr = GenerateForReportTagKML<ReportMunicipalityModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Infrastructure":
                //    retStr = GenerateForReportTagKML<ReportInfrastructureModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Box_Model":
                //    retStr = GenerateForReportTagKML<ReportBox_ModelModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Box_Model_Result":
                //    retStr = GenerateForReportTagKML<ReportBox_Model_ResultModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Visual_Plumes_Scenario":
                //    retStr = GenerateForReportTagKML<ReportVisual_Plumes_ScenarioModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Visual_Plumes_Scenario_Ambient":
                //    retStr = GenerateForReportTagKML<ReportVisual_Plumes_Scenario_AmbientModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Visual_Plumes_Scenario_Result":
                //    retStr = GenerateForReportTagKML<ReportVisual_Plumes_Scenario_ResultModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Infrastructure_Address":
                //    retStr = GenerateForReportTagKML<ReportInfrastructure_AddressModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Infrastructure_File":
                //    retStr = GenerateForReportTagKML<ReportInfrastructure_FileModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                case "Mike_Scenario":
                    retStr = GenerateForReportTagKML<ReportMike_ScenarioModel>(sbNewFileText, ReportTag);
                    break;
                case "Mike_Scenario_Special_Result_KML":
                    retStr = GenerateForReportTagKML<ReportMike_Scenario_Special_Result_KMLModel>(sbNewFileText, ReportTag);
                    break;
                //case "Mike_Source":
                //    retStr = GenerateForReportTagKML<ReportMike_SourceModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Mike_Source_Start_End":
                //    retStr = GenerateForReportTagKML<ReportMike_Source_Start_EndModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Mike_Boundary_Condition":
                //    retStr = GenerateForReportTagKML<ReportMike_Boundary_ConditionModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Mike_Scenario_File":
                //    retStr = GenerateForReportTagKML<ReportMike_Scenario_FileModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Municipality_Contact":
                //    retStr = GenerateForReportTagKML<ReportMunicipality_ContactModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Municipality_Contact_Address":
                //    retStr = GenerateForReportTagKML<ReportMunicipality_Contact_AddressModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Municipality_Contact_Tel":
                //    retStr = GenerateForReportTagKML<ReportMunicipality_Contact_TelModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Municipality_Contact_Email":
                //    retStr = GenerateForReportTagKML<ReportMunicipality_Contact_EmailModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Municipality_File":
                //    retStr = GenerateForReportTagKML<ReportMunicipality_FileModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Subsector_Special_Table":
                //    retStr = GenerateForReportTagKML<ReportSubsector_Special_TableModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Subsector_Climate_Site":
                //    retStr = GenerateForReportTagKML<ReportSubsector_Climate_SiteModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Subsector_Climate_Site_Data":
                //    retStr = GenerateForReportTagKML<ReportSubsector_Climate_Site_DataModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Subsector_Hydrometric_Site":
                //    retStr = GenerateForReportTagKML<ReportSubsector_Hydrometric_SiteModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Subsector_Hydrometric_Site_Data":
                //    retStr = GenerateForReportTagKML<ReportSubsector_Hydrometric_Site_DataModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Subsector_Hydrometric_Site_Rating_Curve":
                //    retStr = GenerateForReportTagKML<ReportSubsector_Hydrometric_Site_Rating_CurveModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Subsector_Hydrometric_Site_Rating_Curve_Value":
                //    retStr = GenerateForReportTagKML<ReportSubsector_Hydrometric_Site_Rating_Curve_ValueModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Subsector_Tide_Site":
                //    retStr = GenerateForReportTagKML<ReportSubsector_Tide_SiteModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Subsector_Tide_Site_Data":
                //    retStr = GenerateForReportTagKML<ReportSubsector_Tide_Site_DataModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Subsector_Lab_Sheet":
                //    retStr = GenerateForReportTagKML<ReportSubsector_Lab_SheetModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Subsector_Lab_Sheet_Detail":
                //    retStr = GenerateForReportTagKML<ReportSubsector_Lab_Sheet_DetailModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Subsector_Lab_Sheet_Tube_And_MPN_Detail":
                //    retStr = GenerateForReportTagKML<ReportSubsector_Lab_Sheet_Tube_And_MPN_DetailModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Subsector_File":
                //    retStr = GenerateForReportTagKML<ReportSubsector_FileModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Sector_File":
                //    retStr = GenerateForReportTagKML<ReportSector_FileModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Area_File":
                //    retStr = GenerateForReportTagKML<ReportArea_FileModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Sampling_Plan":
                //    retStr = GenerateForReportTagKML<ReportSampling_PlanModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Sampling_Plan_Lab_Sheet":
                //    retStr = GenerateForReportTagKML<ReportSampling_Plan_Lab_SheetModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Sampling_Plan_Lab_Sheet_Detail":
                //    retStr = GenerateForReportTagKML<ReportSampling_Plan_Lab_Sheet_DetailModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Sampling_Plan_Lab_Sheet_Tube_And_MPN_Detail":
                //    retStr = GenerateForReportTagKML<ReportSampling_Plan_Lab_Sheet_Tube_And_MPN_DetailModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Sampling_Plan_Subsector":
                //    retStr = GenerateForReportTagKML<ReportSampling_Plan_SubsectorModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Sampling_Plan_Subsector_Site":
                //    retStr = GenerateForReportTagKML<ReportSampling_Plan_Subsector_SiteModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Climate_Site":
                //    retStr = GenerateForReportTagKML<ReportClimate_SiteModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Climate_Site_Data":
                //    retStr = GenerateForReportTagKML<ReportClimate_Site_DataModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Hydrometric_Site":
                //    retStr = GenerateForReportTagKML<ReportHydrometric_SiteModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Hydrometric_Site_Data":
                //    retStr = GenerateForReportTagKML<ReportHydrometric_Site_DataModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Hydrometric_Site_Rating_Curve":
                //    retStr = GenerateForReportTagKML<ReportHydrometric_Site_Rating_CurveModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Hydrometric_Site_Rating_Curve_Value":
                //    retStr = GenerateForReportTagKML<ReportHydrometric_Site_Rating_Curve_ValueModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Province_File":
                //    retStr = GenerateForReportTagKML<ReportProvince_FileModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Country_File":
                //    retStr = GenerateForReportTagKML<ReportCountry_FileModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "MPN_Lookup":
                //    retStr = GenerateForReportTagKML<ReportMPN_LookupModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                //case "Root_File":
                //    retStr = GenerateForReportTagKML<ReportRoot_FileModel>(PreTextOfLine, sbNewFileText, ReportTag);
                //    break;
                default:
                    {
                        retStr = string.Format(ReportServiceRes._NotImplementedIn_, ReportTag.TagItem, "ReportGetDBOfType");
                    }
                    break;
            }


            return retStr;
        }
        public string GenerateReportFromTemplateKML(FileInfo fiKML, int StartTVItemID, int Take, int AppTaskID)
        {
            string retStr = "";

            if (!fiKML.Exists)
                return string.Format(ReportServiceRes.FileDoesNotExist_, fiKML.FullName);

            StringBuilder sbFileText = new StringBuilder();
            StreamReader sr = fiKML.OpenText();
            sbFileText.Append(sr.ReadToEnd());
            sr.Close();

            if (_User != null)
            {
                int PercentCompleted = 2;
                retStr = UpdateAppTaskPercentCompleted(AppTaskID, PercentCompleted);
                if (!string.IsNullOrWhiteSpace(retStr))
                {
                    SaveFileKML(sbFileText, fiKML);
                    return retStr;
                }
            }

            List<ReportTag> reportTagList = new List<Services.ReportTag>();
            retStr = CheckTagsAndContentOKKML(sbFileText, reportTagList, StartTVItemID, Take);
            if (!string.IsNullOrWhiteSpace(retStr))
            {
                SaveFileKML(sbFileText, fiKML);
                return retStr;
            }
            sbFileText = new StringBuilder();

            ReportModelDynamic reportModelDynamic = new ReportModelDynamic();
            if (reportTagList.Count > 0)
            {
                foreach (ReportTag reportTag in reportTagList)
                {
                    reportTag.AppTaskID = AppTaskID;
                }

                foreach (ReportTag reportTag in reportTagList.Where(c => c.ReportTagParent == null))
                {
                    retStr = FillTemplateWithDBInfoKML(sbFileText, reportTag);
                    if (!string.IsNullOrWhiteSpace(retStr))
                    {
                        sbFileText.Insert(0, "\r\n\r\n" + retStr + "\r\n\r\n");

                        SaveFileKML(sbFileText, fiKML);
                        return retStr;
                    }
                }
            }


            sbFileText = new StringBuilder(sbFileText.ToString().Trim());
            SaveFileKML(sbFileText, fiKML);

            return "";
        }
        private string GetFileName(PFSFile pfsFile, string Path, string Keyword, ReportTag reportTag)
        {
            string FileName = "";

            PFSSection pfsSectionFileName = pfsFile.GetSectionFromHandle(Path);

            if (pfsSectionFileName != null)
            {
                PFSKeyword keyword = null;
                try
                {
                    keyword = pfsSectionFileName.GetKeyword(Keyword);
                }
                catch (Exception ex)
                {
                    reportTag.Error = string.Format(ReportServiceRes.PFS_Error_, "GetKeyword", ex.Message + (ex.InnerException != null ? " Inner: " + ex.InnerException.Message : ""));
                    return FileName;
                }

                if (keyword != null)
                {
                    try
                    {
                        FileName = keyword.GetParameter(1).ToFileNamePath();
                    }
                    catch (Exception ex)
                    {
                        reportTag.Error = string.Format(ReportServiceRes.PFS_Error_, "GetParameter", ex.Message + (ex.InnerException != null ? " Inner: " + ex.InnerException.Message : ""));
                        return FileName;
                    }
                }
            }

            return FileName;
        }
        private string GetFileNameOnlyText(PFSFile pfsFile, string Path, string Keyword, ReportTag reportTag)
        {
            string FileName = "";

            PFSSection pfsSectionFileName = pfsFile.GetSectionFromHandle(Path);

            if (pfsSectionFileName != null)
            {
                PFSKeyword keyword = null;
                try
                {
                    keyword = pfsSectionFileName.GetKeyword(Keyword);
                }
                catch (Exception ex)
                {
                    reportTag.Error = string.Format(ReportServiceRes.PFS_Error_, "GetKeyword", ex.Message + (ex.InnerException != null ? " Inner: " + ex.InnerException.Message : ""));
                    return FileName;
                }

                if (keyword != null)
                {
                    try
                    {
                        FileName = keyword.GetParameter(1).ToString();
                    }
                    catch (Exception ex)
                    {
                        reportTag.Error = string.Format(ReportServiceRes.PFS_Error_, "GetParameter", ex.Message + (ex.InnerException != null ? " Inner: " + ex.InnerException.Message : ""));
                        return FileName;
                    }
                }
            }

            return FileName;
        }
        public DfsuFile GetHydrodynamicDfsuFile(ReportTag reportTag)
        {
            DfsuFile dfsuFile = null;
            if (reportTag.UnderTVItemID == 0)
            {
                reportTag.Error = "reportTag.UnderTVItemID == 0 in GetElementLayerList()";
                return dfsuFile;
            }

            TVFileService tvFileService = new TVFileService(LanguageEnum.en, _User);
            TVFileModel tvFileModelM21_3fm = tvFileService.GetTVFileModelWithTVItemIDAndTVFileTypeM21FMOrM3FMDB(reportTag.UnderTVItemID);
            if (!string.IsNullOrWhiteSpace(tvFileModelM21_3fm.Error))
            {
                reportTag.Error = tvFileModelM21_3fm.Error;
                return dfsuFile;
            }

            PFSFile pfsFile = new PFSFile(tvFileModelM21_3fm.ServerFilePath + tvFileModelM21_3fm.ServerFileName);
            string HydroFileName = GetFileNameOnlyText(pfsFile, "FemEngineHD/HYDRODYNAMIC_MODULE/OUTPUTS/OUTPUT_1", "file_name", reportTag);
            if (string.IsNullOrWhiteSpace(HydroFileName))
            {
                reportTag.Error = "HydroFileName is empty";
                return dfsuFile;
            }

            FileInfo fiHydro = new FileInfo(tvFileModelM21_3fm.ServerFilePath + HydroFileName);
            if (!fiHydro.Exists)
            {
                reportTag.Error = "HydroFileName does not exist";
                return dfsuFile;
            }

            try
            {
                dfsuFile = DfsuFile.Open(fiHydro.FullName);
            }
            catch (Exception)
            {
                reportTag.Error = "Could not open file [" + fiHydro.FullName + "]";
                return dfsuFile;
            }

            return dfsuFile;
        }
        public string GetScenarioName(ReportTag reportTag)
        {
            MikeScenarioService mikeScenarioService = new MikeScenarioService((reportTag.Language == "fr" ? LanguageEnum.fr : LanguageEnum.en), _User);

            MikeScenarioModel mikeScenarioModel = mikeScenarioService.GetMikeScenarioModelWithMikeScenarioTVItemIDDB(reportTag.UnderTVItemID);
            if (string.IsNullOrWhiteSpace(mikeScenarioModel.Error))
            {
                return mikeScenarioModel.MikeScenarioTVText;
            }

            return "";
        }
        public void GetSelectedFieldsAndPropertiesKML(ReportTreeNode reportTreeNodeTable, StringBuilder sb)
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
        public void GetSelectedFieldsContainerKML(ReportTreeNode reportTreeNodeTable, StringBuilder sb)
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
        private Coord GetSourceCoord(PFSSection pfsSectionSource, ReportTag reportTag)
        {
            Coord SourceCoord = null;
            PFSKeyword pfsKeywordCoord = null;
            try
            {
                pfsKeywordCoord = pfsSectionSource.GetKeyword("coordinates");
            }
            catch (Exception ex)
            {
                reportTag.Error = string.Format(ReportServiceRes.PFS_Error_, "GetKeyword", ex.Message + (ex.InnerException != null ? " Inner: " + ex.InnerException.Message : ""));
                return SourceCoord;
            }

            if (pfsKeywordCoord != null)
            {
                try
                {
                    float Lng = (float)pfsKeywordCoord.GetParameter(1).ToDouble();
                    float Lat = (float)pfsKeywordCoord.GetParameter(2).ToDouble();
                    SourceCoord = new Coord() { Lat = (float)Lat, Lng = (float)Lng, Ordinal = 0 };
                }
                catch (Exception ex)
                {
                    reportTag.Error = string.Format(ReportServiceRes.PFS_Error_, "GetParameter", ex.Message + (ex.InnerException != null ? " Inner: " + ex.InnerException.Message : ""));
                    return SourceCoord;
                }
            }

            return SourceCoord;
        }
        private float? GetSourceFlow(PFSSection pfsSectionSource, ReportTag reportTag)
        {
            float? SourceFlow = null;
            PFSKeyword pfsKeywordFlow = null;
            try
            {
                pfsKeywordFlow = pfsSectionSource.GetKeyword("constant_value");
            }
            catch (Exception ex)
            {
                reportTag.Error = string.Format(ReportServiceRes.PFS_Error_, "GetKeyword", ex.Message + (ex.InnerException != null ? " Inner: " + ex.InnerException.Message : ""));
                return SourceFlow;
            }

            if (pfsKeywordFlow != null)
            {
                try
                {
                    SourceFlow = (float)pfsKeywordFlow.GetParameter(1).ToDouble();
                }
                catch (Exception ex)
                {
                    reportTag.Error = string.Format(ReportServiceRes.PFS_Error_, "GetParameter", ex.Message + (ex.InnerException != null ? " Inner: " + ex.InnerException.Message : ""));
                    return SourceFlow;
                }
            }

            return SourceFlow;
        }
        private int? GetSourceIncluded(PFSSection pfsSectionSource, ReportTag reportTag)
        {
            int? SourceIncluded = null;
            PFSKeyword pfsKeywordInculde = null;
            try
            {
                pfsKeywordInculde = pfsSectionSource.GetKeyword("include");
            }
            catch (Exception ex)
            {
                reportTag.Error = string.Format(ReportServiceRes.PFS_Error_, "GetKeyword", ex.Message + (ex.InnerException != null ? " Inner: " + ex.InnerException.Message : ""));
                return SourceIncluded;
            }

            if (pfsKeywordInculde != null)
            {
                try
                {
                    SourceIncluded = pfsKeywordInculde.GetParameter(1).ToInt();
                }
                catch (Exception ex)
                {
                    reportTag.Error = string.Format(ReportServiceRes.PFS_Error_, "GetParameter", ex.Message + (ex.InnerException != null ? " Inner: " + ex.InnerException.Message : ""));
                    return SourceIncluded;
                }
            }

            return SourceIncluded;
        }
        private string GetSourceName(PFSSection pfsSectionSource, ReportTag reportTag)
        {
            string Name = "";
            PFSKeyword pfsKeywordName = null;
            try
            {
                pfsKeywordName = pfsSectionSource.GetKeyword("Name");
            }
            catch (Exception ex)
            {
                reportTag.Error = string.Format(ReportServiceRes.PFS_Error_, "GetKeyword", ex.Message + (ex.InnerException != null ? " Inner: " + ex.InnerException.Message : ""));
                return Name;
            }

            if (pfsKeywordName != null)
            {
                try
                {
                    Name = pfsKeywordName.GetParameter(1).ToString();
                }
                catch (Exception ex)
                {
                    reportTag.Error = string.Format(ReportServiceRes.PFS_Error_, "GetParameter", ex.Message + (ex.InnerException != null ? " Inner: " + ex.InnerException.Message : ""));
                    return Name;
                }
            }

            if (Name.StartsWith("'"))
            {
                Name = Name.Substring(1);
            }
            if (Name.EndsWith("'"))
            {
                Name = Name.Substring(0, Name.Length - 1);
            }

            return Name;
        }
        private int? GetSourcePollution(PFSSection pfsSectionSourceTransport, ReportTag reportTag)
        {
            int? SourcePollution = null;
            PFSKeyword pfsKeywordPollution = null;
            try
            {
                pfsKeywordPollution = pfsSectionSourceTransport.GetKeyword("constant_value");
            }
            catch (Exception ex)
            {
                reportTag.Error = string.Format(ReportServiceRes.PFS_Error_, "GetKeyword", ex.Message + (ex.InnerException != null ? " Inner: " + ex.InnerException.Message : ""));
                return SourcePollution;
            }

            if (pfsKeywordPollution != null)
            {
                try
                {
                    SourcePollution = (int)pfsKeywordPollution.GetParameter(1).ToInt();
                }
                catch (Exception ex)
                {
                    reportTag.Error = string.Format(ReportServiceRes.PFS_Error_, "GetParameter", ex.Message + (ex.InnerException != null ? " Inner: " + ex.InnerException.Message : ""));
                    return SourcePollution;
                }
            }

            return SourcePollution;
        }
        private int? GetSourcePollutionContinuous(PFSSection pfsSectionSourceTransport, ReportTag reportTag)
        {
            int? SourcePollutionContinuous = null;
            PFSKeyword pfsKeywordPollutionContinuous = null;
            try
            {
                pfsKeywordPollutionContinuous = pfsSectionSourceTransport.GetKeyword("format");
            }
            catch (Exception ex)
            {
                reportTag.Error = string.Format(ReportServiceRes.PFS_Error_, "GetKeyword", ex.Message + (ex.InnerException != null ? " Inner: " + ex.InnerException.Message : ""));
                return SourcePollutionContinuous;
            }

            if (pfsKeywordPollutionContinuous != null)
            {
                try
                {
                    SourcePollutionContinuous = (int)pfsKeywordPollutionContinuous.GetParameter(1).ToInt();
                }
                catch (Exception ex)
                {
                    reportTag.Error = string.Format(ReportServiceRes.PFS_Error_, "GetParameter", ex.Message + (ex.InnerException != null ? " Inner: " + ex.InnerException.Message : ""));
                    return SourcePollutionContinuous;
                }
            }

            return SourcePollutionContinuous;
        }
        private float? GetSourceSalinity(PFSSection pfsSectionSourceSalinity, ReportTag reportTag)
        {
            float? SourceSalinity = null;
            PFSKeyword pfsKeywordSalinity = null;
            try
            {
                pfsKeywordSalinity = pfsSectionSourceSalinity.GetKeyword("constant_value");
            }
            catch (Exception ex)
            {
                reportTag.Error = string.Format(ReportServiceRes.PFS_Error_, "GetKeyword", ex.Message + (ex.InnerException != null ? " Inner: " + ex.InnerException.Message : ""));
                return SourceSalinity;
            }

            if (pfsKeywordSalinity != null)
            {
                try
                {
                    SourceSalinity = (float)pfsKeywordSalinity.GetParameter(1).ToDouble();
                }
                catch (Exception ex)
                {
                    reportTag.Error = string.Format(ReportServiceRes.PFS_Error_, "GetParameter", ex.Message + (ex.InnerException != null ? " Inner: " + ex.InnerException.Message : ""));
                    return SourceSalinity;
                }
            }

            return SourceSalinity;
        }
        private float? GetSourceTemperature(PFSSection pfsSectionSourceTemperature, ReportTag reportTag)
        {
            float? SourceTemperature = null;
            PFSKeyword pfsKeywordTemperature = null;
            try
            {
                pfsKeywordTemperature = pfsSectionSourceTemperature.GetKeyword("constant_value");
            }
            catch (Exception ex)
            {
                reportTag.Error = string.Format(ReportServiceRes.PFS_Error_, "GetKeyword", ex.Message + (ex.InnerException != null ? " Inner: " + ex.InnerException.Message : ""));
                return SourceTemperature;
            }

            if (pfsKeywordTemperature != null)
            {
                try
                {
                    SourceTemperature = (float)pfsKeywordTemperature.GetParameter(1).ToDouble();
                }
                catch (Exception ex)
                {
                    reportTag.Error = string.Format(ReportServiceRes.PFS_Error_, "GetParameter", ex.Message + (ex.InnerException != null ? " Inner: " + ex.InnerException.Message : ""));
                    return SourceTemperature;
                }
            }

            return SourceTemperature;
        }
        public DfsuFile GetTransportDfsuFile(ReportTag reportTag)
        {
            DfsuFile dfsuFile = null;
            if (reportTag.UnderTVItemID == 0)
            {
                reportTag.Error = "reportTag.UnderTVItemID == 0 in GetElementLayerList()";
                return dfsuFile;
            }
            IPrincipal user = new GenericPrincipal(new GenericIdentity("charles.leblanc2@canada.ca", "Forms"), null);

            TVFileService tvFileService = new TVFileService(LanguageEnum.en, user);
            TVFileModel tvFileModelM21_3fm = tvFileService.GetTVFileModelWithTVItemIDAndTVFileTypeM21FMOrM3FMDB(reportTag.UnderTVItemID);
            if (!string.IsNullOrWhiteSpace(tvFileModelM21_3fm.Error))
            {
                reportTag.Error = tvFileModelM21_3fm.Error;
                return dfsuFile;
            }

            PFSFile pfsFile = new PFSFile(tvFileModelM21_3fm.ServerFilePath + tvFileModelM21_3fm.ServerFileName);
            string TransFileName = GetFileNameOnlyText(pfsFile, "FemEngineHD/TRANSPORT_MODULE/OUTPUTS/OUTPUT_1", "file_name", reportTag);
            if (string.IsNullOrWhiteSpace(TransFileName))
            {
                reportTag.Error = "TransFileName is empty";
                return dfsuFile;
            }

            FileInfo fiTrans = new FileInfo(tvFileModelM21_3fm.ServerFilePath + TransFileName);
            if (!fiTrans.Exists)
            {
                reportTag.Error = "TransFileName does not exist";
                return dfsuFile;
            }

            try
            {
                dfsuFile = DfsuFile.Open(fiTrans.FullName);
            }
            catch (Exception)
            {
                reportTag.Error = "Could not open file [" + fiTrans.FullName + "]";
                return dfsuFile;
            }

            return dfsuFile;
        }
        public void GetTreeViewSelectedStatusKML(ReportTreeNode reportTreeNode, StringBuilder sb, int Level)
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
                            sb.AppendLine(Marker + "Start " + reportTreeNode.Text + " " + LanguageRequest.ToString());
                            GetSelectedFieldsAndPropertiesKML(reportTreeNode, sb);
                            sb.AppendLine(Marker);
                            GetSelectedFieldsContainerKML(reportTreeNode, sb);
                        }
                        else
                        {
                            bool CheckExist = GetSubFieldIsChecked(reportTreeNode);
                            if (CheckExist)
                            {
                                sb.AppendLine(Marker + "LoopStart " + reportTreeNode.Text + " " + LanguageRequest.ToString());
                                GetSelectedFieldsAndPropertiesKML(reportTreeNode, sb);
                                sb.AppendLine(Marker);
                                GetSelectedFieldsContainerKML(reportTreeNode, sb);
                            }
                        }
                        foreach (ReportTreeNode RTN in reportTreeNode.Nodes)
                        {
                            if (RTN.Checked)
                            {
                                GetTreeViewSelectedStatusKML(RTN, sb, Level + 1);
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
        private void InsertNewNodeInInterpolatedContourNodeList(DfsuFile dfsuFile, Node NodeLarge, Node NodeSmall, float ContourValue)
        {
            PolyPoint point = new PolyPoint();
            point.XCoord = NodeSmall.X + (NodeLarge.X - NodeSmall.X) * (ContourValue - NodeSmall.Value) / (NodeLarge.Value - NodeSmall.Value);
            point.YCoord = NodeSmall.Y + (NodeLarge.Y - NodeSmall.Y) * (ContourValue - NodeSmall.Value) / (NodeLarge.Value - NodeSmall.Value);
            point.Z = dfsuFile.Z[NodeSmall.ID - 1] + (dfsuFile.Z[NodeLarge.ID - 1] - dfsuFile.Z[NodeSmall.ID - 1]) * (ContourValue - NodeSmall.Value) / (NodeLarge.Value - NodeSmall.Value);

            Node NewNode = new Node();
            NewNode.ID = 100000 * NodeLarge.ID + NodeSmall.ID;
            NewNode.X = point.XCoord;
            NewNode.Y = point.YCoord;
            NewNode.Z = point.Z;
            NewNode.Value = ContourValue;

            if (InterpolatedContourNodeList.Where(nn => nn.ID == NewNode.ID).Count() == 0)
            {
                InterpolatedContourNodeList.Add(NewNode);
            }
        }
        public string SaveFileKML(StringBuilder sbFileText, FileInfo fiKML)
        {
            System.Text.Encoding encDefault = System.Text.Encoding.GetEncoding(0);
            System.IO.FileStream fs = new System.IO.FileStream(fiKML.FullName, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite);
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
        private void WriteKMLBoundaryConditionNode(StringBuilder sbNewFileText, ReportTag reportTag)
        {
            string[] Colors = { "ylw", "grn", "blue", "ltblu", "pink", "red" };

            foreach (string color in Colors)
            {
                sbNewFileText.AppendLine(string.Format(@"	<Style id=""sn_{0}-pushpin"">", color));
                sbNewFileText.AppendLine(@"		<IconStyle>");
                sbNewFileText.AppendLine(@"			<scale>1.1</scale>");
                sbNewFileText.AppendLine(@"			<Icon>");
                sbNewFileText.AppendLine(string.Format(@"				<href>http://maps.google.com/mapfiles/kml/pushpin/{0}-pushpin.png</href>", color));
                sbNewFileText.AppendLine(@"			</Icon>");
                sbNewFileText.AppendLine(@"			<hotSpot x=""20"" y=""2"" xunits=""pixels"" yunits=""pixels""/>");
                sbNewFileText.AppendLine(@"		</IconStyle>");
                sbNewFileText.AppendLine(@"		<ListStyle>");
                sbNewFileText.AppendLine(@"		</ListStyle>");
                sbNewFileText.AppendLine(@"	</Style>");
                sbNewFileText.AppendLine(string.Format(@"	<StyleMap id=""msn_{0}-pushpin"">", color));
                sbNewFileText.AppendLine(@"		<Pair>");
                sbNewFileText.AppendLine(@"			<key>normal</key>");
                sbNewFileText.AppendLine(string.Format(@"			<styleUrl>#sn_{0}-pushpin</styleUrl>", color));
                sbNewFileText.AppendLine(@"		</Pair>");
                sbNewFileText.AppendLine(@"		<Pair>");
                sbNewFileText.AppendLine(@"			<key>highlight</key>");
                sbNewFileText.AppendLine(string.Format(@"			<styleUrl>#sh_{0}-pushpin</styleUrl>", color));
                sbNewFileText.AppendLine(@"		</Pair>");
                sbNewFileText.AppendLine(@"	</StyleMap>");
                sbNewFileText.AppendLine(string.Format(@"	<Style id=""sh_{0}-pushpin"">", color));
                sbNewFileText.AppendLine(@"		<IconStyle>");
                sbNewFileText.AppendLine(@"			<scale>1.3</scale>");
                sbNewFileText.AppendLine(@"			<Icon>");
                sbNewFileText.AppendLine(string.Format(@"				<href>http://maps.google.com/mapfiles/kml/pushpin/{0}-pushpin.png</href>", color));
                sbNewFileText.AppendLine(@"			</Icon>");
                sbNewFileText.AppendLine(@"			<hotSpot x=""20"" y=""2"" xunits=""pixels"" yunits=""pixels""/>");
                sbNewFileText.AppendLine(@"		</IconStyle>");
                sbNewFileText.AppendLine(@"		<ListStyle>");
                sbNewFileText.AppendLine(@"		</ListStyle>");
                sbNewFileText.AppendLine(@"	</Style>");
            }

            //UpdateTask(AppTaskID, "30 %");

            TVItemService tvItemService = new TVItemService((reportTag.Language == "fr" ? LanguageEnum.fr : LanguageEnum.en), _User);
            TVItemModel tvItemModelMikeScenario = tvItemService.GetTVItemModelWithTVItemIDDB(reportTag.UnderTVItemID);
            if (!string.IsNullOrWhiteSpace(tvItemModelMikeScenario.Error))
            {
                sbNewFileText.AppendLine(@"<Folder>");
                sbNewFileText.AppendLine(@"    <name>" + ReportServiceRes.Error + "</name>");
                sbNewFileText.AppendLine(@"    <description><![CDATA[");
                sbNewFileText.AppendLine(@"    <h4>" + tvItemModelMikeScenario.Error + "</h4");
                sbNewFileText.AppendLine(@"    ]]></description>");
                sbNewFileText.AppendLine(@"</Folder>");
                return;
            }

            MikeBoundaryConditionService mikeBoundaryConditionService = new MikeBoundaryConditionService((reportTag.Language == "fr" ? LanguageEnum.fr : LanguageEnum.en), _User);
            List<MikeBoundaryConditionModel> mbcModelList = mikeBoundaryConditionService.GetMikeBoundaryConditionModelListWithMikeScenarioTVItemIDAndTVTypeDB(tvItemModelMikeScenario.TVItemID, TVTypeEnum.MikeBoundaryConditionMesh);

            int countColor = 0;
            foreach (MikeBoundaryConditionModel mbcm in mbcModelList)
            {
                sbNewFileText.AppendLine(@"<Folder>");
                sbNewFileText.AppendLine(@"<name>" + mbcm.MikeBoundaryConditionName + " (" + mbcm.MikeBoundaryConditionCode + ") length [" + mbcm.MikeBoundaryConditionLength_m.ToString("F0") + "] </name>");
                sbNewFileText.AppendLine(@"<visibility>1</visibility>");
                sbNewFileText.AppendLine(@"<description><![CDATA[");
                sbNewFileText.AppendLine(@"<p>(" + mbcm.MikeBoundaryConditionFormat + ") " + mbcm.MikeBoundaryConditionLevelOrVelocity + " " + mbcm.WebTideDataSet.ToString() + " " + mbcm.NumberOfWebTideNodes + " " + ReportServiceRes.Nodes + "</p>");
                sbNewFileText.AppendLine(@"]]></description>");

                // drawing Boundary Nodes
                MapInfoService mapInfoService = new MapInfoService((reportTag.Language == "fr" ? LanguageEnum.fr : LanguageEnum.en), _User);
                List<MapInfoPointModel> mapInfoPointModelList = mapInfoService._MapInfoPointService.GetMapInfoPointModelListWithTVItemIDAndTVTypeAndMapInfoDrawTypeDB(mbcm.MikeBoundaryConditionTVItemID, TVTypeEnum.MikeBoundaryConditionMesh, MapInfoDrawTypeEnum.Polyline);

                sbNewFileText.AppendLine(@"    <Folder>");
                sbNewFileText.AppendLine(@"    <name>" + ReportServiceRes.ElementNodes + @"</name>");
                sbNewFileText.AppendLine(@"    <open>1</open>");
                foreach (MapInfoPointModel mapInfoPointModel in mapInfoPointModelList)
                {
                    sbNewFileText.AppendLine(@"    <Placemark>");
                    sbNewFileText.AppendLine(@"    <name>Node " + mapInfoPointModel.Ordinal + "</name>");
                    sbNewFileText.AppendLine(string.Format(@"    <styleUrl>#msn_{0}-pushpin</styleUrl>", Colors[countColor]));
                    sbNewFileText.AppendLine(@"    <Point>");
                    sbNewFileText.AppendLine(@"    <coordinates>" + mapInfoPointModel.Lng.ToString().Replace(",", ".") + @"," + mapInfoPointModel.Lat.ToString().Replace(",", ".") + @",0</coordinates>");
                    sbNewFileText.AppendLine(@"    </Point>");
                    sbNewFileText.AppendLine(@"    </Placemark>");
                }
                sbNewFileText.AppendLine(@"    </Folder>");


                countColor += 1;
                if (countColor > 5) countColor = 0;
                MikeBoundaryConditionModel mbcModel2 = mikeBoundaryConditionService.GetMikeBoundaryConditionModelListWithMikeScenarioTVItemIDAndTVTypeDB(tvItemModelMikeScenario.TVItemID, TVTypeEnum.MikeBoundaryConditionWebTide).Where(c => c.MikeBoundaryConditionName == mbcm.MikeBoundaryConditionName).FirstOrDefault();
                List<MapInfoPointModel> mapInfoPointModelList2 = mapInfoService._MapInfoPointService.GetMapInfoPointModelListWithTVItemIDAndTVTypeAndMapInfoDrawTypeDB(mbcModel2.MikeBoundaryConditionTVItemID, TVTypeEnum.MikeBoundaryConditionWebTide, MapInfoDrawTypeEnum.Polyline);

                sbNewFileText.AppendLine(@"    <Folder>");
                sbNewFileText.AppendLine(@"    <name>" + ReportServiceRes.WebTideNodes + @"</name>");
                sbNewFileText.AppendLine(@"    <open>1</open>");
                foreach (MapInfoPointModel mapInfoPointModel in mapInfoPointModelList2)
                {
                    sbNewFileText.AppendLine(@"    <Placemark>");
                    sbNewFileText.AppendLine(@"    <name>Node " + mapInfoPointModel.Ordinal + "</name>");
                    sbNewFileText.AppendLine(string.Format(@"    <styleUrl>#msn_{0}-pushpin</styleUrl>", Colors[countColor]));
                    sbNewFileText.AppendLine(@"    <Point>");
                    sbNewFileText.AppendLine(@"    <coordinates>" + mapInfoPointModel.Lng.ToString().Replace(",", ".") + @"," + mapInfoPointModel.Lat.ToString().Replace(",", ".") + @",0</coordinates>");
                    sbNewFileText.AppendLine(@"    </Point>");
                    sbNewFileText.AppendLine(@"    </Placemark>");
                }
                sbNewFileText.AppendLine(@"    </Folder>");

                sbNewFileText.AppendLine(@"</Folder>");

                countColor += 1;
                if (countColor > 5) countColor = 0;
            }
        }
        private void WriteKMLBottom(StringBuilder sbNewFileText, ReportTag reportTag)
        {
            sbNewFileText.AppendLine(@"</Document>");
            sbNewFileText.AppendLine(@"</kml>");
        }
        private void WriteKMLFecalColiformContourLine(DfsuFile dfsuFile, StringBuilder sbNewFileText, List<ElementLayer> elementLayerList, List<NodeLayer> topNodeLayerList, List<NodeLayer> bottomNodeLayerList, ReportTag reportTag)
        {
            int PercentCompleted = 3;
            int ItemNumber = 0;
            double RefreshEveryXSeconds = double.Parse("5");
            DateTime RefreshDateTime = DateTime.Now.AddSeconds(RefreshEveryXSeconds);

            List<float> ContourValueList = new List<float>();

            ReportTreeNode reportTreeNode = reportTag.ReportTreeNodeList.Where(c => c.Text == "Mike_Scenario_Special_Result_KML_Limit_Values").FirstOrDefault();
            if (reportTreeNode != null)
            {
                foreach (ReportConditionTextField reportConditionTextField in reportTreeNode.dbFilteringTextFieldList)
                {
                    if (reportConditionTextField.ReportCondition == ReportConditionEnum.ReportConditionEqual)
                    {
                        ContourValueList = reportConditionTextField.TextCondition.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(c => float.Parse(c)).ToList();
                    }
                }
            }

            if (ContourValueList.Count == 0)
            {
                reportTag.Error = string.Format(ReportServiceRes._IsRequiredIn_, "Mike_Scenario_Special_Result_KML_Limit_Values", "WriteKMLPollutionLimitsContourLine");
                return;
            }

            // getting the ItemNumber
            foreach (IDfsSimpleDynamicItemInfo dfsDyInfo in dfsuFile.ItemInfo)
            {
                if (dfsDyInfo.Quantity.Item == eumItem.eumIConcentration
                     || dfsDyInfo.Quantity.Item == eumItem.eumIConcentration1
                     || dfsDyInfo.Quantity.Item == eumItem.eumIConcentration_1
                     || dfsDyInfo.Quantity.Item == eumItem.eumIConcentration_2
                     || dfsDyInfo.Quantity.Item == eumItem.eumIConcentration_3
                     || dfsDyInfo.Quantity.Item == eumItem.eumIConcentration_4)
                {
                    ItemNumber = dfsDyInfo.ItemNumber;
                    break;
                }
            }

            if (ItemNumber == 0)
            {
                reportTag.Error = string.Format(ReportServiceRes.CouldNotFind__, ReportServiceRes.ParameterType, "eumIConcentration");
                return;
            }

            //            int pcount = 0;
            sbNewFileText.AppendLine(@"<Folder>");
            sbNewFileText.AppendLine(@"  <name>" + ReportServiceRes.PollutionAnimation + "</name>");
            sbNewFileText.AppendLine(@"  <visibility>0</visibility>");

            int CountAt = 0;
            int CountLayer = (dfsuFile.NumberOfSigmaLayers == 0 ? 1 : dfsuFile.NumberOfSigmaLayers);
            int CurrentContourValue = 0;
            int CurrentTimeSteps = 0;

            int TotalCount = CountLayer * ContourValueList.Count * dfsuFile.NumberOfTimeSteps;

            for (int Layer = 1; Layer <= CountLayer; Layer++)
            {
                //CurrentLayer += 1;
                CurrentContourValue = 1;
                CurrentTimeSteps = 1;

                #region Top of Layer
                if (Layer == 1)
                {
                    sbNewFileText.AppendLine(string.Format(@"<Folder><name>" + ReportServiceRes.TopOfLayer + @" [{0}] </name>", Layer));
                }
                else
                {
                    sbNewFileText.AppendLine(string.Format(@"<Folder><name>" + ReportServiceRes.TopOfLayer + @" [{0}] " + ReportServiceRes.BottomOfLayer + @" [{1}] </name>", Layer, Layer - 1));
                }
                sbNewFileText.AppendLine(@"<visibility>0</visibility>");
                int CountContourValue = 1;
                foreach (float ContourValue in ContourValueList)
                {
                    sbNewFileText.AppendLine(string.Format(@"  <Folder><name>" + ReportServiceRes.ContourValue + @" [{0}]</name>", ContourValue));
                    sbNewFileText.AppendLine(@"  <visibility>0</visibility>");

                    int vcount = 0;
                    CurrentContourValue += 1;
                    //for (int timeStep = 30; timeStep < 35 /*dfsuFile.NumberOfTimeSteps */; timeStep++)
                    for (int timeStep = 0; timeStep < dfsuFile.NumberOfTimeSteps; timeStep++)
                    {
                        CountAt += 1;
                        int PercentCompletedTemp = (int)(((CountAt - 1) * (float)100.0f) / (float)TotalCount);
                        if (PercentCompleted != PercentCompletedTemp)
                        {
                            string retStr = UpdateAppTaskPercentCompleted(reportTag.AppTaskID, PercentCompleted);
                        }
                        PercentCompleted = PercentCompletedTemp;

                        float[] ValueList = (float[])dfsuFile.ReadItemTimeStep(ItemNumber, timeStep).Data;

                        List<ContourPolygon> ContourPolygonList = new List<ContourPolygon>();

                        for (int i = 0; i < elementLayerList.Count; i++)
                        {
                            elementLayerList[i].Element.Value = ValueList[i];
                        }

                        List<ElementLayer> elementLayerListAtLayer = elementLayerList.Where(c => c.Layer == Layer).ToList();
                        List<NodeLayer> topNodeLayerListAtLayer = topNodeLayerList.Where(c => c.Layer == Layer).ToList();

                        foreach (NodeLayer nl in topNodeLayerListAtLayer)
                        {
                            float Total = 0;
                            foreach (Element element in nl.Node.ElementList)
                            {
                                Total += element.Value;
                            }
                            nl.Node.Value = Total / nl.Node.ElementList.Count;
                        }

                        List<Node> AllNodeList = new List<Node>();

                        List<NodeLayer> AboveNodeLayerList = (from n in topNodeLayerListAtLayer
                                                              where (n.Node.Value >= ContourValue)
                                                              && n.Layer == Layer
                                                              select n).ToList<NodeLayer>();

                        foreach (NodeLayer snl in AboveNodeLayerList)
                        {
                            List<NodeLayer> EndNodeLayerList = null;

                            List<NodeLayer> NodeLayerConnectedList = (from nll in topNodeLayerListAtLayer
                                                                      from n in snl.Node.ConnectNodeList
                                                                      where (n.ID == nll.Node.ID)
                                                                      select nll).ToList<NodeLayer>();

                            EndNodeLayerList = (from nll in NodeLayerConnectedList
                                                where (nll.Node.ID != snl.Node.ID)
                                                && (nll.Node.Value < ContourValue)
                                                && nll.Layer == Layer
                                                select nll).ToList<NodeLayer>();

                            foreach (NodeLayer en in EndNodeLayerList)
                            {
                                AllNodeList.Add(en.Node);
                            }

                            if (snl.Node.Code != 0)
                            {
                                AllNodeList.Add(snl.Node);
                            }

                        }

                        //if (AllNodeList.Count == 0)
                        //{
                        //    //vcount += 1;
                        //    continue;
                        //}

                        List<Element> TempUniqueElementList = new List<Element>();
                        List<Element> UniqueElementList = new List<Element>();
                        foreach (ElementLayer el in elementLayerListAtLayer)
                        {
                            if (dfsuFile.DfsuFileType == DfsuFileType.Dfsu3DSigma || dfsuFile.DfsuFileType == DfsuFileType.Dfsu3DSigmaZ)
                            {
                                if (el.Element.Type == 32)
                                {
                                    bool NodeBigger = false;
                                    for (int i = 3; i < 6; i++)
                                    {
                                        if (el.Element.NodeList[i].Value >= ContourValue)
                                        {
                                            NodeBigger = true;
                                            break;
                                        }
                                    }
                                    if (NodeBigger)
                                    {
                                        int countTrue = 0;
                                        for (int i = 3; i < 6; i++)
                                        {
                                            if (el.Element.NodeList[i].Value >= ContourValue && el.Element.NodeList[i].Code == 0)
                                            {
                                                countTrue += 1;
                                            }
                                        }
                                        if (countTrue != el.Element.NodeList.Count)
                                        {
                                            TempUniqueElementList.Add(el.Element);
                                        }
                                    }
                                }
                                else if (el.Element.Type == 33)
                                {
                                    bool NodeBigger = false;
                                    for (int i = 4; i < 8; i++)
                                    {
                                        if (el.Element.NodeList[i].Value >= ContourValue)
                                        {
                                            NodeBigger = true;
                                            break;
                                        }
                                    }
                                    if (NodeBigger)
                                    {
                                        int countTrue = 0;
                                        for (int i = 4; i < 8; i++)
                                        {
                                            if (el.Element.NodeList[i].Value >= ContourValue && el.Element.NodeList[i].Code == 0)
                                            {
                                                countTrue += 1;
                                            }
                                        }
                                        if (countTrue != el.Element.NodeList.Count)
                                        {
                                            TempUniqueElementList.Add(el.Element);
                                        }
                                    }
                                }
                                else
                                {
                                    reportTag.Error = string.Format(ReportServiceRes.ElementType_IsNotSupported, el.Element.Type.ToString());
                                    return;
                                }
                            }
                            else if (dfsuFile.DfsuFileType == DfsuFileType.Dfsu2D)
                            {
                                bool NodeBigger = false;
                                for (int i = 0; i < el.Element.NodeList.Count; i++)
                                {
                                    if (el.Element.NodeList[i].Value >= ContourValue)
                                    {
                                        NodeBigger = true;
                                        break;
                                    }
                                }
                                if (NodeBigger)
                                {
                                    int countTrue = 0;
                                    for (int i = 0; i < el.Element.NodeList.Count; i++)
                                    {
                                        if (el.Element.NodeList[i].Value >= ContourValue && el.Element.NodeList[i].Code == 0)
                                        {
                                            countTrue += 1;
                                        }
                                    }
                                    if (countTrue != el.Element.NodeList.Count)
                                    {
                                        TempUniqueElementList.Add(el.Element);
                                    }
                                }
                            }
                        }

                        UniqueElementList = (from el in TempUniqueElementList select el).Distinct().ToList<Element>();

                        // filling InterpolatedContourNodeList
                        InterpolatedContourNodeList = new List<Node>();

                        foreach (Element el in UniqueElementList)
                        {
                            if (dfsuFile.DfsuFileType == DfsuFileType.Dfsu3DSigma || dfsuFile.DfsuFileType == DfsuFileType.Dfsu3DSigmaZ)
                            {
                                if (el.Type == 32)
                                {
                                    if (el.NodeList[3].Value >= ContourValue && el.NodeList[4].Value < ContourValue)
                                    {
                                        InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[3], el.NodeList[4], ContourValue);
                                    }
                                    if (el.NodeList[3].Value >= ContourValue && el.NodeList[5].Value < ContourValue)
                                    {
                                        InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[3], el.NodeList[5], ContourValue);
                                    }
                                    if (el.NodeList[4].Value >= ContourValue && el.NodeList[3].Value < ContourValue)
                                    {
                                        InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[4], el.NodeList[3], ContourValue);
                                    }
                                    if (el.NodeList[4].Value >= ContourValue && el.NodeList[5].Value < ContourValue)
                                    {
                                        InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[4], el.NodeList[5], ContourValue);
                                    }
                                    if (el.NodeList[5].Value >= ContourValue && el.NodeList[4].Value < ContourValue)
                                    {
                                        InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[5], el.NodeList[4], ContourValue);
                                    }
                                    if (el.NodeList[5].Value >= ContourValue && el.NodeList[3].Value < ContourValue)
                                    {
                                        InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[5], el.NodeList[3], ContourValue);
                                    }
                                }
                                else if (el.Type == 33)
                                {
                                    if (el.NodeList[4].Value >= ContourValue && el.NodeList[5].Value < ContourValue)
                                    {
                                        InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[4], el.NodeList[5], ContourValue);
                                    }
                                    if (el.NodeList[4].Value >= ContourValue && el.NodeList[7].Value < ContourValue)
                                    {
                                        InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[4], el.NodeList[7], ContourValue);
                                    }
                                    if (el.NodeList[5].Value >= ContourValue && el.NodeList[4].Value < ContourValue)
                                    {
                                        InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[5], el.NodeList[4], ContourValue);
                                    }
                                    if (el.NodeList[5].Value >= ContourValue && el.NodeList[6].Value < ContourValue)
                                    {
                                        InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[5], el.NodeList[6], ContourValue);
                                    }
                                    if (el.NodeList[6].Value >= ContourValue && el.NodeList[5].Value < ContourValue)
                                    {
                                        InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[6], el.NodeList[5], ContourValue);
                                    }
                                    if (el.NodeList[6].Value >= ContourValue && el.NodeList[7].Value < ContourValue)
                                    {
                                        InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[6], el.NodeList[7], ContourValue);
                                    }
                                    if (el.NodeList[7].Value >= ContourValue && el.NodeList[4].Value < ContourValue)
                                    {
                                        InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[7], el.NodeList[4], ContourValue);
                                    }
                                    if (el.NodeList[7].Value >= ContourValue && el.NodeList[6].Value < ContourValue)
                                    {
                                        InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[7], el.NodeList[6], ContourValue);
                                    }
                                }
                                else
                                {
                                    reportTag.Error = string.Format(ReportServiceRes.ElementType_IsNotSupported, el.Type.ToString());
                                    return;
                                }
                            }
                            else if (dfsuFile.DfsuFileType == DfsuFileType.Dfsu2D)
                            {
                                if (el.Type == 21)
                                {
                                    if (el.NodeList[0].Value >= ContourValue && el.NodeList[1].Value < ContourValue)
                                    {
                                        InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[0], el.NodeList[1], ContourValue);
                                    }
                                    if (el.NodeList[0].Value >= ContourValue && el.NodeList[2].Value < ContourValue)
                                    {
                                        InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[0], el.NodeList[2], ContourValue);
                                    }
                                    if (el.NodeList[1].Value >= ContourValue && el.NodeList[0].Value < ContourValue)
                                    {
                                        InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[1], el.NodeList[0], ContourValue);
                                    }
                                    if (el.NodeList[1].Value >= ContourValue && el.NodeList[2].Value < ContourValue)
                                    {
                                        InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[1], el.NodeList[2], ContourValue);
                                    }
                                    if (el.NodeList[2].Value >= ContourValue && el.NodeList[1].Value < ContourValue)
                                    {
                                        InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[2], el.NodeList[1], ContourValue);
                                    }
                                    if (el.NodeList[2].Value >= ContourValue && el.NodeList[0].Value < ContourValue)
                                    {
                                        InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[2], el.NodeList[0], ContourValue);
                                    }
                                }
                                else if (el.Type == 24)
                                {
                                }
                                else if (el.Type == 25)
                                {
                                    if (el.NodeList[0].Value >= ContourValue && el.NodeList[1].Value < ContourValue)
                                    {
                                        InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[0], el.NodeList[1], ContourValue);
                                    }
                                    if (el.NodeList[0].Value >= ContourValue && el.NodeList[3].Value < ContourValue)
                                    {
                                        InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[0], el.NodeList[3], ContourValue);
                                    }
                                    if (el.NodeList[1].Value >= ContourValue && el.NodeList[0].Value < ContourValue)
                                    {
                                        InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[1], el.NodeList[0], ContourValue);
                                    }
                                    if (el.NodeList[1].Value >= ContourValue && el.NodeList[2].Value < ContourValue)
                                    {
                                        InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[1], el.NodeList[2], ContourValue);
                                    }
                                    if (el.NodeList[2].Value >= ContourValue && el.NodeList[1].Value < ContourValue)
                                    {
                                        InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[2], el.NodeList[1], ContourValue);
                                    }
                                    if (el.NodeList[2].Value >= ContourValue && el.NodeList[3].Value < ContourValue)
                                    {
                                        InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[2], el.NodeList[3], ContourValue);
                                    }
                                    if (el.NodeList[3].Value >= ContourValue && el.NodeList[0].Value < ContourValue)
                                    {
                                        InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[3], el.NodeList[0], ContourValue);
                                    }
                                    if (el.NodeList[3].Value >= ContourValue && el.NodeList[2].Value < ContourValue)
                                    {
                                        InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[3], el.NodeList[2], ContourValue);
                                    }
                                }
                                else
                                {
                                    reportTag.Error = string.Format(ReportServiceRes.ElementType_IsNotSupported, el.Type.ToString());
                                    return;
                                }
                            }
                        }

                        List<Node> UniqueNodeList = (from n in AllNodeList orderby n.ID select n).Distinct().ToList<Node>();

                        // ------------------------- new code --------------------------
                        //                     

                        ForwardVector = new Dictionary<string, Vector>();
                        BackwardVector = new Dictionary<string, Vector>();

                        foreach (Element el in UniqueElementList)
                        {
                            if (el.Type == 21)
                            {
                                FillVectors21_32(el, UniqueElementList, ContourValue, false, true, reportTag);
                            }
                            else if (el.Type == 24)
                            {
                                reportTag.Error = ReportServiceRes.AllNodesAreSmallerThanContourValue;
                                return;
                            }
                            else if (el.Type == 25)
                            {
                                FillVectors25_33(el, UniqueElementList, ContourValue, false, true, reportTag);
                            }
                            else if (el.Type == 32)
                            {
                                FillVectors21_32(el, UniqueElementList, ContourValue, true, true, reportTag);
                            }
                            else if (el.Type == 33)
                            {
                                FillVectors25_33(el, UniqueElementList, ContourValue, true, true, reportTag);
                            }
                            else
                            {
                                reportTag.Error = string.Format(ReportServiceRes.ElementType_IsNotSupported, el.Type.ToString());
                                return;
                            }

                        }

                        //-------------- new code ------------------------



                        bool MoreContourLine = true;
                        MapInfoService mapInfoService = new MapInfoService((reportTag.Language == "fr" ? LanguageEnum.fr : LanguageEnum.en), _User);
                        while (MoreContourLine && ForwardVector.Count > 0)
                        {
                            List<Node> FinalContourNodeList = new List<Node>();
                            Vector LastVector = new Vector();
                            LastVector = ForwardVector.First().Value;
                            FinalContourNodeList.Add(LastVector.StartNode);
                            FinalContourNodeList.Add(LastVector.EndNode);
                            ForwardVector.Remove(LastVector.StartNode.ID.ToString() + "," + LastVector.EndNode.ID.ToString());
                            BackwardVector.Remove(LastVector.EndNode.ID.ToString() + "," + LastVector.StartNode.ID.ToString());
                            bool PolygonCompleted = false;
                            while (!PolygonCompleted)
                            {
                                List<string> KeyStrList = (from k in ForwardVector.Keys
                                                           where k.StartsWith(LastVector.EndNode.ID.ToString() + ",")
                                                           && !k.EndsWith("," + LastVector.StartNode.ID.ToString())
                                                           select k).ToList<string>();

                                if (KeyStrList.Count != 1)
                                {
                                    KeyStrList = (from k in BackwardVector.Keys
                                                  where k.StartsWith(LastVector.EndNode.ID.ToString() + ",")
                                                  && !k.EndsWith("," + LastVector.StartNode.ID.ToString())
                                                  select k).ToList<string>();

                                    if (KeyStrList.Count != 1)
                                    {
                                        PolygonCompleted = true;
                                        break;
                                    }
                                    else
                                    {
                                        LastVector = BackwardVector[KeyStrList[0]];
                                        BackwardVector.Remove(LastVector.StartNode.ID.ToString() + "," + LastVector.EndNode.ID.ToString());
                                        ForwardVector.Remove(LastVector.EndNode.ID.ToString() + "," + LastVector.StartNode.ID.ToString());
                                    }
                                }
                                else
                                {
                                    LastVector = ForwardVector[KeyStrList[0]];
                                    ForwardVector.Remove(LastVector.StartNode.ID.ToString() + "," + LastVector.EndNode.ID.ToString());
                                    BackwardVector.Remove(LastVector.EndNode.ID.ToString() + "," + LastVector.StartNode.ID.ToString());
                                }
                                FinalContourNodeList.Add(LastVector.EndNode);
                                if (FinalContourNodeList[FinalContourNodeList.Count - 1] == FinalContourNodeList[0])
                                {
                                    PolygonCompleted = true;
                                }
                            }

                            if (mapInfoService.CalculateAreaOfPolygon(FinalContourNodeList) < 0)
                            {
                                FinalContourNodeList.Reverse();
                            }

                            FinalContourNodeList.Add(FinalContourNodeList[0]);
                            ContourPolygon contourPolygon = new ContourPolygon() { };
                            contourPolygon.ContourNodeList = FinalContourNodeList;
                            contourPolygon.ContourValue = ContourValue;
                            contourPolygon.Layer = Layer;
                            ContourPolygonList.Add(contourPolygon);

                            if (ForwardVector.Count == 0)
                            {
                                MoreContourLine = false;
                            }

                        }
                        DrawKMLContourPolygon(ContourPolygonList, dfsuFile, vcount, sbNewFileText);

                        vcount += 1;
                    }
                    sbNewFileText.AppendLine(@"  </Folder>");
                    CountContourValue += 1;
                }
                sbNewFileText.AppendLine(@"  </Folder>");
                #endregion Top of Layer

                #region Bottom of Layer
                //// doing the bottom layer if the current layer is == NumberOfSigmaLayers
                //if (Layer == dfsuFile.NumberOfSigmaLayers)
                //{
                //    sbPlacemarkFeacalColiformContour.AppendLine(string.Format(@"<Folder><name>Bottom of Layer [{0}]</name>", Layer));
                //    sbPlacemarkFeacalColiformContour.AppendLine(@"<visibility>0</visibility>");
                //    CountContourValue = 1;
                //    foreach (float ContourValue in ContourValueList)
                //    {
                //        sbPlacemarkFeacalColiformContour.AppendLine(string.Format(@"<Folder><name>Contour Value [{0}]</name>", ContourValue));
                //        sbPlacemarkFeacalColiformContour.AppendLine(@"<visibility>0</visibility>");

                //        int vcount = 0;
                //        //for (int timeStep = 30; timeStep < 35 /*dfsuFile.NumberOfTimeSteps */; timeStep++)
                //        for (int timeStep = 0; timeStep < dfsuFile.NumberOfTimeSteps; timeStep++)
                //        {
                //            CountRefresh += 1;
                //            CountAt += 1;
                //            if (CountRefresh > UpdateAfter)
                //            {
                //                string AppTaskStatus = "";
                //                if (SigmaLayerValueList.Contains(dfsuFile.NumberOfSigmaLayers))
                //                {
                //                    AppTaskStatus = ((int)((CountAt * 100) / (dfsuFile.NumberOfTimeSteps * (SigmaLayerValueList.Count + 1) * ContourValueList.Count))).ToString() + " %";
                //                }
                //                else
                //                {
                //                    AppTaskStatus = ((int)((CountAt * 100) / (dfsuFile.NumberOfTimeSteps * SigmaLayerValueList.Count * ContourValueList.Count))).ToString() + " %";
                //                }
                //                UpdateTask(AppTaskID, AppTaskStatus);
                //                CountRefresh = 0;
                //            }

                //            float[] ValueList = (float[])dfsuFile.ReadItemTimeStep(ItemNumber, timeStep).Data;

                //            List<ContourPolygon> ContourPolygonList = new List<ContourPolygon>();

                //            for (int i = 0; i < ElementLayerList.Count; i++)
                //            {
                //                ElementLayerList[i].Element.Value = ValueList[i];
                //            }

                //            foreach (NodeLayer nl in BottomNodeLayerList)
                //            {
                //                float Total = 0;
                //                foreach (Element element in nl.Node.ElementList)
                //                {
                //                    Total += element.Value;
                //                }
                //                nl.Node.Value = Total / nl.Node.ElementList.Count;
                //            }


                //            List<Node> AllNodeList = new List<Node>();

                //            List<NodeLayer> AboveNodeLayerList = new List<NodeLayer>();

                //            AboveNodeLayerList = (from n in BottomNodeLayerList
                //                                  where (n.Node.Value >= ContourValue)
                //                                  && n.Layer == Layer
                //                                  select n).ToList<NodeLayer>();

                //            foreach (NodeLayer snl in AboveNodeLayerList)
                //            {
                //                List<NodeLayer> EndNodeLayerList = null;

                //                List<NodeLayer> NodeLayerConnectedList = (from nll in BottomNodeLayerList
                //                                                          from n in snl.Node.ConnectNodeList
                //                                                          where (n.ID == nll.Node.ID)
                //                                                          select nll).ToList<NodeLayer>();

                //                EndNodeLayerList = (from nll in NodeLayerConnectedList
                //                                    where (nll.Node.ID != snl.Node.ID)
                //                                    && (nll.Node.Value < ContourValue)
                //                                    && nll.Layer == Layer
                //                                    select nll).ToList<NodeLayer>();

                //                foreach (NodeLayer en in EndNodeLayerList)
                //                {
                //                    AllNodeList.Add(en.Node);
                //                }

                //                if (snl.Node.Code != 0)
                //                {
                //                    AllNodeList.Add(snl.Node);
                //                }

                //            }

                //            if (AllNodeList.Count == 0)
                //            {
                //                //vcount += 1;
                //                continue;
                //            }

                //            List<Element> TempUniqueElementList = new List<Element>();
                //            List<Element> UniqueElementList = new List<Element>();
                //            foreach (ElementLayer el in ElementLayerList.Where(l => l.Layer == Layer))
                //            {
                //                if (dfsuFile.DfsuFileType == DfsuFileType.Dfsu3DSigma || dfsuFile.DfsuFileType == DfsuFileType.Dfsu3DSigmaZ)
                //                {
                //                    if (el.Element.Type == 32)
                //                    {
                //                        bool NodeBigger = false;
                //                        for (int i = 3; i < 6; i++)
                //                        {
                //                            if (el.Element.NodeList[i].Value >= ContourValue)
                //                            {
                //                                NodeBigger = true;
                //                                break;
                //                            }
                //                        }
                //                        if (NodeBigger)
                //                        {
                //                            int countTrue = 0;
                //                            for (int i = 3; i < 6; i++)
                //                            {
                //                                if (el.Element.NodeList[i].Value >= ContourValue && el.Element.NodeList[i].Code == 0)
                //                                {
                //                                    countTrue += 1;
                //                                }
                //                            }
                //                            if (countTrue != el.Element.NodeList.Count)
                //                            {
                //                                TempUniqueElementList.Add(el.Element);
                //                            }
                //                        }
                //                    }
                //                    else if (el.Element.Type == 33)
                //                    {
                //                        bool NodeBigger = false;
                //                        for (int i = 4; i < 8; i++)
                //                        {
                //                            if (el.Element.NodeList[i].Value >= ContourValue)
                //                            {
                //                                NodeBigger = true;
                //                                break;
                //                            }
                //                        }
                //                        if (NodeBigger)
                //                        {
                //                            int countTrue = 0;
                //                            for (int i = 4; i < 8; i++)
                //                            {
                //                                if (el.Element.NodeList[i].Value >= ContourValue && el.Element.NodeList[i].Code == 0)
                //                                {
                //                                    countTrue += 1;
                //                                }
                //                            }
                //                            if (countTrue != el.Element.NodeList.Count)
                //                            {
                //                                TempUniqueElementList.Add(el.Element);
                //                            }
                //                        }
                //                    }
                //                    else
                //                    {
                //                        UpdateTask(AppTaskID, "");
                //                        throw new Exception("Element type is not supported: Element type = [" + el.Element.Type + "]");
                //                    }
                //                }
                //                else
                //                {
                //                    UpdateTask(AppTaskID, "");
                //                    throw new Exception("Bottom only exist for Dfsu3DSigma and Dfsu3DSigmaZ.");
                //                }
                //            }

                //            UniqueElementList = (from el in TempUniqueElementList select el).Distinct().ToList<Element>();

                //            // filling InterpolatedContourNodeList
                //            InterpolatedContourNodeList = new List<Node>();

                //            foreach (Element el in UniqueElementList)
                //            {
                //                if (dfsuFile.DfsuFileType == DfsuFileType.Dfsu3DSigma || dfsuFile.DfsuFileType == DfsuFileType.Dfsu3DSigmaZ)
                //                {
                //                    if (el.Type == 32)
                //                    {
                //                        if (el.NodeList[0].Value >= ContourValue && el.NodeList[1].Value < ContourValue)
                //                        {
                //                            InsertNewNodeInInterpolatedContourNodeList(el.NodeList[0], el.NodeList[1], ContourValue);
                //                        }
                //                        if (el.NodeList[0].Value >= ContourValue && el.NodeList[2].Value < ContourValue)
                //                        {
                //                            InsertNewNodeInInterpolatedContourNodeList(el.NodeList[0], el.NodeList[2], ContourValue);
                //                        }
                //                        if (el.NodeList[1].Value >= ContourValue && el.NodeList[0].Value < ContourValue)
                //                        {
                //                            InsertNewNodeInInterpolatedContourNodeList(el.NodeList[1], el.NodeList[0], ContourValue);
                //                        }
                //                        if (el.NodeList[1].Value >= ContourValue && el.NodeList[2].Value < ContourValue)
                //                        {
                //                            InsertNewNodeInInterpolatedContourNodeList(el.NodeList[1], el.NodeList[2], ContourValue);
                //                        }
                //                        if (el.NodeList[2].Value >= ContourValue && el.NodeList[1].Value < ContourValue)
                //                        {
                //                            InsertNewNodeInInterpolatedContourNodeList(el.NodeList[2], el.NodeList[1], ContourValue);
                //                        }
                //                        if (el.NodeList[2].Value >= ContourValue && el.NodeList[0].Value < ContourValue)
                //                        {
                //                            InsertNewNodeInInterpolatedContourNodeList(el.NodeList[2], el.NodeList[0], ContourValue);
                //                        }
                //                    }
                //                    else if (el.Type == 33)
                //                    {
                //                        if (el.NodeList[0].Value >= ContourValue && el.NodeList[1].Value < ContourValue)
                //                        {
                //                            InsertNewNodeInInterpolatedContourNodeList(el.NodeList[0], el.NodeList[1], ContourValue);
                //                        }
                //                        if (el.NodeList[0].Value >= ContourValue && el.NodeList[3].Value < ContourValue)
                //                        {
                //                            InsertNewNodeInInterpolatedContourNodeList(el.NodeList[0], el.NodeList[3], ContourValue);
                //                        }
                //                        if (el.NodeList[1].Value >= ContourValue && el.NodeList[0].Value < ContourValue)
                //                        {
                //                            InsertNewNodeInInterpolatedContourNodeList(el.NodeList[1], el.NodeList[0], ContourValue);
                //                        }
                //                        if (el.NodeList[1].Value >= ContourValue && el.NodeList[2].Value < ContourValue)
                //                        {
                //                            InsertNewNodeInInterpolatedContourNodeList(el.NodeList[1], el.NodeList[2], ContourValue);
                //                        }
                //                        if (el.NodeList[2].Value >= ContourValue && el.NodeList[1].Value < ContourValue)
                //                        {
                //                            InsertNewNodeInInterpolatedContourNodeList(el.NodeList[2], el.NodeList[1], ContourValue);
                //                        }
                //                        if (el.NodeList[2].Value >= ContourValue && el.NodeList[3].Value < ContourValue)
                //                        {
                //                            InsertNewNodeInInterpolatedContourNodeList(el.NodeList[2], el.NodeList[3], ContourValue);
                //                        }
                //                        if (el.NodeList[3].Value >= ContourValue && el.NodeList[0].Value < ContourValue)
                //                        {
                //                            InsertNewNodeInInterpolatedContourNodeList(el.NodeList[3], el.NodeList[0], ContourValue);
                //                        }
                //                        if (el.NodeList[3].Value >= ContourValue && el.NodeList[2].Value < ContourValue)
                //                        {
                //                            InsertNewNodeInInterpolatedContourNodeList(el.NodeList[3], el.NodeList[2], ContourValue);
                //                        }
                //                    }
                //                    else
                //                    {
                //                        UpdateTask(AppTaskID, "");
                //                        throw new Exception("Element type is not supported: Element type = [" + el.Type + "]");
                //                    }
                //                }
                //                else
                //                {
                //                    UpdateTask(AppTaskID, "");
                //                    throw new Exception("Bottom only exist for Dfsu3DSigma and Dfsu3DSigmaZ.");
                //                }
                //            }

                //            List<Node> UniqueNodeList = (from n in AllNodeList orderby n.ID select n).Distinct().ToList<Node>();

                //            // ------------------------- new code --------------------------
                //            //                     

                //            ForwardVector = new Dictionary<string, Vector>();
                //            BackwardVector = new Dictionary<string, Vector>();

                //            foreach (Element el in UniqueElementList)
                //            {
                //                if (el.Type == 21)
                //                {
                //                    FillVectors21_32(el, UniqueElementList, ContourValue, AppTaskID, false, false);
                //                }
                //                else if (el.Type == 24)
                //                {
                //                    UpdateTask(AppTaskID, "");
                //                    throw new Exception("This should never happen. Node0, Node1 nd Node2 all < ContourValue");
                //                }
                //                else if (el.Type == 25)
                //                {
                //                    FillVectors25_33(el, UniqueElementList, ContourValue, AppTaskID, false, false);
                //                }
                //                else if (el.Type == 32)
                //                {
                //                    FillVectors21_32(el, UniqueElementList, ContourValue, AppTaskID, true, false);
                //                }
                //                else if (el.Type == 33)
                //                {
                //                    FillVectors25_33(el, UniqueElementList, ContourValue, AppTaskID, true, false);
                //                }
                //                else
                //                {
                //                    UpdateTask(AppTaskID, "");
                //                    throw new Exception("Element type is not supported: Element type = [" + el.Type + "]");
                //                }

                //            }

                //            //-------------- new code ------------------------



                //            bool MoreContourLine = true;
                //            while (MoreContourLine)
                //            {
                //                List<Node> FinalContourNodeList = new List<Node>();
                //                Vector LastVector = new Vector();
                //                LastVector = ForwardVector.First().Value;
                //                FinalContourNodeList.Add(LastVector.StartNode);
                //                FinalContourNodeList.Add(LastVector.EndNode);
                //                ForwardVector.Remove(LastVector.StartNode.ID.ToString() + "," + LastVector.EndNode.ID.ToString());
                //                BackwardVector.Remove(LastVector.EndNode.ID.ToString() + "," + LastVector.StartNode.ID.ToString());
                //                bool PolygonCompleted = false;
                //                while (!PolygonCompleted)
                //                {
                //                    List<string> KeyStrList = (from k in ForwardVector.Keys
                //                                               where k.StartsWith(LastVector.EndNode.ID.ToString() + ",")
                //                                               && !k.EndsWith("," + LastVector.StartNode.ID.ToString())
                //                                               select k).ToList<string>();

                //                    if (KeyStrList.Count != 1)
                //                    {
                //                        KeyStrList = (from k in BackwardVector.Keys
                //                                      where k.StartsWith(LastVector.EndNode.ID.ToString() + ",")
                //                                      && !k.EndsWith("," + LastVector.StartNode.ID.ToString())
                //                                      select k).ToList<string>();

                //                        if (KeyStrList.Count != 1)
                //                        {
                //                            PolygonCompleted = true;
                //                            break;
                //                        }
                //                        else
                //                        {
                //                            LastVector = BackwardVector[KeyStrList[0]];
                //                            BackwardVector.Remove(LastVector.StartNode.ID.ToString() + "," + LastVector.EndNode.ID.ToString());
                //                            ForwardVector.Remove(LastVector.EndNode.ID.ToString() + "," + LastVector.StartNode.ID.ToString());
                //                        }
                //                    }
                //                    else
                //                    {
                //                        LastVector = ForwardVector[KeyStrList[0]];
                //                        ForwardVector.Remove(LastVector.StartNode.ID.ToString() + "," + LastVector.EndNode.ID.ToString());
                //                        BackwardVector.Remove(LastVector.EndNode.ID.ToString() + "," + LastVector.StartNode.ID.ToString());
                //                    }
                //                    FinalContourNodeList.Add(LastVector.EndNode);
                //                    if (FinalContourNodeList[FinalContourNodeList.Count - 1] == FinalContourNodeList[0])
                //                    {
                //                        PolygonCompleted = true;
                //                    }
                //                }

                //                if (CalculateAreaOfPolygon(FinalContourNodeList) < 0)
                //                {
                //                    FinalContourNodeList.Reverse();
                //                }

                //                FinalContourNodeList.Add(FinalContourNodeList[0]);
                //                ContourPolygon contourPolygon = new ContourPolygon() { };
                //                contourPolygon.ContourNodeList = FinalContourNodeList;
                //                contourPolygon.ContourValue = ContourValue;
                //                contourPolygon.Layer = Layer;
                //                ContourPolygonList.Add(contourPolygon);

                //                if (ForwardVector.Count == 0)
                //                {
                //                    MoreContourLine = false;
                //                }

                //            }
                //            DrawKMLContourPolygon(ContourPolygonList, dfsuFile, vcount, sbStyleFeacalColiformContour, sbPlacemarkFeacalColiformContour);
                //            vcount += 1;
                //        }
                //        sbPlacemarkFeacalColiformContour.AppendLine(@"</Folder>");
                //        CountContourValue += 1;
                //    }
                //    sbPlacemarkFeacalColiformContour.AppendLine(@"</Folder>");
                //}
                #endregion Bottom of Layer
            }
            sbNewFileText.AppendLine(@"</Folder>");

            string retStr2 = UpdateAppTaskPercentCompleted(reportTag.AppTaskID, 100);

            return;
        }
        private void WriteKMLMesh(StringBuilder sbNewFileText, List<ElementLayer> ElementLayerList, ReportTag reportTag)
        {
            List<Node> nodeList = new List<Node>();

            sbNewFileText.AppendLine(@"<Style id=""_line"">");
            sbNewFileText.AppendLine("<LineStyle>");
            sbNewFileText.AppendLine(@"<color>ff99ff99</color>");
            sbNewFileText.AppendLine(@"<width>1</width>");
            sbNewFileText.AppendLine("</LineStyle>");
            sbNewFileText.AppendLine(@"</Style>");


            sbNewFileText.AppendLine(@"<Folder>");
            sbNewFileText.AppendLine(@"<visibility>0</visibility>");
            sbNewFileText.AppendLine(@"<name>" + ReportServiceRes.MIKEMesh + "</name>");

            int CountRefresh = 0;
            int CountAt = 0;
            int UpdateAfter = (int)(ElementLayerList.Count() / 10);
            foreach (ElementLayer ElemLayer in ElementLayerList.Where(c => c.Layer == 1).OrderBy(c => c.Element.ID))
            {
                CountRefresh += 1;
                CountAt += 1;
                if (CountRefresh > UpdateAfter)
                {
                    //_TaskRunnerBaseService.SendPercentToDB(_TaskRunnerBaseService._BWObj.appTaskModel.AppTaskID, (int)((CountAt * 10) / ElementLayerList.Count()));

                    CountRefresh = 0;
                }

                StringBuilder sbCoord = new StringBuilder();
                float total = 0;
                string LastPart = "";
                foreach (Node node in ElemLayer.Element.NodeList)
                {

                    nodeList.Add(node);

                    if (LastPart == "")
                        LastPart = node.X.ToString().Replace(",", ".") + @"," + node.Y.ToString().Replace(",", ".") + ",0 ";

                    total += node.Z;
                    sbCoord.Append(node.X.ToString().Replace(",", ".") + @"," + node.Y.ToString().Replace(",", ".") + ",0 ");

                }
                sbCoord.Append(LastPart);

                string PolyName = ElemLayer.Element.ID.ToString();

                // Inserting the Placemark
                sbNewFileText.AppendLine(@"<Placemark>");
                sbNewFileText.AppendLine(@"<visibility>0</visibility>");
                sbNewFileText.AppendLine(string.Format(@"<name>{0}</name>", PolyName));
                sbNewFileText.AppendLine(@"<styleUrl>#_line</styleUrl>");
                sbNewFileText.AppendLine(@"<LineString>");
                sbNewFileText.AppendLine(@"<coordinates>");
                sbNewFileText.AppendLine(sbCoord.ToString());
                sbNewFileText.AppendLine(@"</coordinates>");
                sbNewFileText.AppendLine(@"</LineString>");
                sbNewFileText.AppendLine(@"</Placemark>");
            }

            sbNewFileText.AppendLine(@"</Folder>");

            //List<Node> uniqueNode = (from n in nodeList
            //                         select n).Distinct().ToList();

            //sbKMLMesh.AppendLine(@"<Folder>");

            //foreach (Node node in uniqueNode)
            //{
            //    sbKMLMesh.AppendLine("<Placemark>");
            //    sbKMLMesh.AppendLine(@"<visibility>0</visibility>");
            //    sbKMLMesh.AppendLine(string.Format(@"<name>{0}</name>", node.ID));
            //    sbKMLMesh.AppendLine(@"<Point>");
            //    sbKMLMesh.AppendLine(string.Format(@"<coordinates>{0},{1},0</coordinates>", node.X, node.Y));
            //    sbKMLMesh.AppendLine("@</Point>");
            //    sbKMLMesh.AppendLine("</Placemark>");
            //}
            //sbKMLMesh.AppendLine(@"</Folder>");

        }
        private void WriteKMLModelInput(StringBuilder sbNewFileText, ReportTag reportTag)
        {
            PFSFile pfsFile = null;
            MikeScenarioService mikeScenarioService = new MikeScenarioService((reportTag.Language == "fr" ? LanguageEnum.fr : LanguageEnum.en), _User);
            TVItemService tvItemService = new TVItemService((reportTag.Language == "fr" ? LanguageEnum.fr : LanguageEnum.en), _User);
            MikeSourceService mikeSourceService = new MikeSourceService((reportTag.Language == "fr" ? LanguageEnum.fr : LanguageEnum.en), _User);
            MapInfoService mapInfoService = new MapInfoService((reportTag.Language == "fr" ? LanguageEnum.fr : LanguageEnum.en), _User);
            MikeSourceStartEndService mikeSourceStartEndService = new MikeSourceStartEndService((reportTag.Language == "fr" ? LanguageEnum.fr : LanguageEnum.en), _User);
            TVFileService tvFileService = new TVFileService((reportTag.Language == "fr" ? LanguageEnum.fr : LanguageEnum.en), _User);

            List<float> ContourValueList = new List<float>();

            ReportTreeNode reportTreeNode = reportTag.ReportTreeNodeList.Where(c => c.Text == "Mike_Scenario_Special_Result_KML_Limit_Values").FirstOrDefault();
            if (reportTreeNode != null)
            {
                foreach (ReportConditionTextField reportConditionTextField in reportTreeNode.dbFilteringTextFieldList)
                {
                    if (reportConditionTextField.ReportCondition == ReportConditionEnum.ReportConditionEqual)
                    {
                        ContourValueList = reportConditionTextField.TextCondition.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(c => float.Parse(c)).ToList();
                    }
                }
            }

            if (ContourValueList.Count == 0)
            {
                reportTag.Error = string.Format(ReportServiceRes._IsRequiredIn_, "Mike_Scenario_Special_Result_KML_Limit_Values", "WriteKMLPollutionLimitsContourLine");
                return;
            }

            TVFileModel tvFileModelM21_3FM = tvFileService.GetTVFileModelWithTVItemIDAndTVFileTypeM21FMOrM3FMDB(reportTag.UnderTVItemID);
            if (tvFileModelM21_3FM == null)
            {
                reportTag.Error = string.Format(ReportServiceRes.CouldNotFind_With_Equal_, ReportServiceRes.TVFile, ReportServiceRes.TVItemID + "," + ReportServiceRes.FileType, reportTag.UnderTVItemID + " .m21fm or .m3fm");
                return;
            }

            string ServerFileName = tvFileModelM21_3FM.ServerFileName;
            string ServerFilePath = tvFileService.GetServerFilePath(reportTag.UnderTVItemID);

            FileInfo fiServer = new FileInfo(ServerFilePath + ServerFileName);

            if (!fiServer.Exists)
            {
                reportTag.Error = string.Format(ReportServiceRes.CouldNotFindFile_, fiServer.FullName);
                return;
            }

            pfsFile = new PFSFile(fiServer.FullName);

            MikeScenarioModel mikeScenarioModel = mikeScenarioService.GetMikeScenarioModelWithMikeScenarioTVItemIDDB(reportTag.UnderTVItemID);
            if (!string.IsNullOrWhiteSpace(mikeScenarioModel.Error))
            {
                reportTag.Error = string.Format(ReportServiceRes.CouldNotFind_With_Equal_, ReportServiceRes.MikeScenario, ReportServiceRes.MikeScenarioTVItemID, reportTag.UnderTVItemID.ToString());
                return;
            }

            TVItemModel tvItemModelMikeScenario = tvItemService.GetTVItemModelWithTVItemIDDB(reportTag.UnderTVItemID);
            if (!string.IsNullOrWhiteSpace(tvItemModelMikeScenario.Error))
            {
                reportTag.Error = string.Format(ReportServiceRes.CouldNotFind_With_Equal_, ReportServiceRes.TVItem, ReportServiceRes.TVItemID, reportTag.UnderTVItemID.ToString());
                return;
            }

            List<MikeSourceModel> mikeSourceModelList = mikeSourceService.GetMikeSourceModelListWithMikeScenarioTVItemIDDB(mikeScenarioModel.MikeScenarioTVItemID);
            if (mikeSourceModelList.Count == 0)
            {
                reportTag.Error = string.Format(ReportServiceRes.CouldNotFind_With_Equal_, ReportServiceRes.MikeSource, ReportServiceRes.TVItemID, mikeScenarioModel.MikeScenarioTVItemID.ToString());
                return;
            }


            sbNewFileText.Append("  <Folder><name>" + ReportServiceRes.ModelInput + "</name>");
            sbNewFileText.AppendLine(@"  <visibility>0</visibility>");

            #region Source Description
            sbNewFileText.AppendLine(@"<description><![CDATA[");
            sbNewFileText.AppendLine(string.Format(@"<h2>{0}</h2>", ReportServiceRes.ModelParameters));
            sbNewFileText.AppendLine(@"<ul>");
            sbNewFileText.AppendLine(string.Format(@"<li><b>{0}:</b> {1:yyyy/MM/dd HH:mm:ss tt}</li>", ReportServiceRes.ScenarioStartTime, mikeScenarioModel.MikeScenarioStartDateTime_Local));
            sbNewFileText.AppendLine(string.Format(@"<li><b>{0}:</b> {1:yyyy/MM/dd HH:mm:ss tt}</li>", ReportServiceRes.ScenarioEndTime, mikeScenarioModel.MikeScenarioEndDateTime_Local));

            foreach (float cv in ContourValueList)
            {
                if (cv >= 14 && cv < 88)
                {
                    sbNewFileText.AppendLine(string.Format(@"<li><span style=""background-color:Blue; color:White"">{0} = {1:F0}</span</li>", ReportServiceRes.FCMPNPollutionContour, cv));
                }
                else if (cv >= 88)
                {
                    sbNewFileText.AppendLine(string.Format(@"<li><span style=""background-color:Red; color:White"">{0} = {1:F0}</span></li>", ReportServiceRes.FCMPNPollutionContour, cv));
                }
                else
                {
                    sbNewFileText.AppendLine(string.Format(@"<li><span style=""background-color:Green; color:White"">{0} = {1:F0}</span></li>", ReportServiceRes.FCMPNPollutionContour, cv));
                }

            }
            sbNewFileText.AppendLine(@"<li><b>" + ReportServiceRes.AverageDecayFactor + @":</b> " + mikeScenarioModel.DecayFactor_per_day.ToString("F6").Replace(",", ".") + @" /" + ReportServiceRes.DayLowerCase + @"</li>");
            if (mikeScenarioModel.DecayIsConstant)
            {
                sbNewFileText.AppendLine(@"<li><b>" + ReportServiceRes.DecayIsConstant + @"</b></li>");
            }
            else
            {
                sbNewFileText.AppendLine(@"<li><b>" + ReportServiceRes.DecayIsVariable + @"</b></li>");
                sbNewFileText.AppendLine(@"<ul><li><b>" + ReportServiceRes.Amplitude + @":</b> " + ((double)mikeScenarioModel.DecayFactorAmplitude).ToString("F6").Replace(",", ".") + @"</li></ul>");
            }
            if (mikeScenarioModel.WindSpeed_km_h > 0)
            {
                sbNewFileText.AppendLine(@"<li><b>" + ReportServiceRes.Wind + @":</b> " + mikeScenarioModel.WindSpeed_km_h.ToString("F1").Replace(",", ".") + @" (km/h)   " + (mikeScenarioModel.WindSpeed_km_h / 3.6).ToString("F1").Replace(",", ".") + @" (m/s)</li>");
                sbNewFileText.AppendLine(@"<li><b>" + ReportServiceRes.WindDirection + @":</b> " + mikeScenarioModel.WindDirection_deg.ToString("F1").Replace(",", ".") + @" " + ReportServiceRes.DegreeLowerCase + " (0 = " + ReportServiceRes.NorthClockwiseLowerCase + @")</li>");
            }
            else
            {
                sbNewFileText.AppendLine("<li><b>" + ReportServiceRes.NoWind + @"</b></li>");
            }
            sbNewFileText.AppendLine(@"<li><b>" + ReportServiceRes.Temperature + @":</b> " + mikeScenarioModel.AmbientTemperature_C.ToString("F1").Replace(",", ".") + " " + ReportServiceRes.Celcius + @"</li>");
            sbNewFileText.AppendLine(@"<li><b>" + ReportServiceRes.Salinity + @":</b> " + mikeScenarioModel.AmbientSalinity_PSU.ToString("F1").Replace(",", ".") + " " + ReportServiceRes.PSU + @"</li>");
            sbNewFileText.AppendLine(@"<li><b>" + ReportServiceRes.ManningNumber + @":</b> " + mikeScenarioModel.ManningNumber.ToString().Replace(",", ".") + @"</li>");
            sbNewFileText.AppendLine(string.Format(@"<li><b>" + ReportServiceRes.ResultFrequency + @":</b> {0:F0} {1}</li>", mikeScenarioModel.ResultFrequency_min, ReportServiceRes.MinutesLowerCase));
            sbNewFileText.AppendLine(@"</ul>");

            List<MikeSourceModel> mikeSourceModelListAll = mikeSourceModelList.Where(c => c.Include == true && c.IsRiver == false).Concat(mikeSourceModelList.Where(c => c.Include == false && c.IsRiver == false).Concat(mikeSourceModelList.Where(c => c.IsRiver == true))).ToList();

            // Do Mike Source 
            foreach (MikeSourceModel mikeSourceModel in mikeSourceModelListAll)
            {
                List<MikeSourceStartEndModel> mikeSourceStartEndModelListLocal = mikeSourceStartEndService.GetMikeSourceStartEndModelListWithMikeSourceIDDB(mikeSourceModel.MikeSourceID);

                if (mikeSourceStartEndModelListLocal.Count == 0)
                {
                    reportTag.Error = string.Format(ReportServiceRes.CouldNotFind_With_Equal_, ReportServiceRes.MikeSourceStartEnd, ReportServiceRes.MikeSourceID, mikeSourceModel.MikeSourceID.ToString());
                }

                if (mikeSourceModel.Include)
                {
                    if (mikeSourceModel.IsRiver)
                    {
                        sbNewFileText.AppendLine(string.Format(@"<h2 style='Color: Blue'>{0} ({1})</h2>", mikeSourceModel.MikeSourceTVText, ReportServiceRes.IncludedLowerCase));
                    }
                    else
                    {
                        sbNewFileText.AppendLine(string.Format(@"<h2 style='Color: Green'>{0} ({1})</h2>", mikeSourceModel.MikeSourceTVText, ReportServiceRes.IncludedLowerCase));
                    }
                }
                else
                {
                    sbNewFileText.AppendLine(string.Format(@"<h2 style='Color: Red'>{0} ({1})</h2>", mikeSourceModel.MikeSourceTVText, ReportServiceRes.NotIncludedLowerCase));
                }
                sbNewFileText.AppendLine(@"<h3>" + ReportServiceRes.Effluent + @"</h3>");
                sbNewFileText.AppendLine(@"<ul>");
                sbNewFileText.AppendLine(@"<li><b>" + ReportServiceRes.Coordinates + @"</b> " + string.Format(@"&nbsp;&nbsp;&nbsp; {0:F5} &nbsp; {1:F5}</li>", mikeSourceModel.Lat, mikeSourceModel.Lng).Replace(",", "."));

                if ((bool)mikeSourceModel.IsContinuous)
                {
                    sbNewFileText.AppendLine(@"<li><b>" + ReportServiceRes.IsContinuous + @"</b></li>");
                    sbNewFileText.AppendLine(@"<li><b>" + ReportServiceRes.Flow + @":</b> " + (mikeSourceStartEndModelListLocal[0].SourceFlowStart_m3_day / 24 / 3600).ToString("F6").Replace(",", ".") + " (m3/s)  " + mikeSourceStartEndModelListLocal[0].SourceFlowStart_m3_day.ToString("F1").Replace(",", ".") + @" (m3/" + ReportServiceRes.DayLowerCase + @")</li>");
                    sbNewFileText.AppendLine(@"<li><b>" + ReportServiceRes.FCMPNPer100ML + @":</b> " + mikeSourceStartEndModelListLocal[0].SourcePollutionStart_MPN_100ml.ToString("F0").Replace(",", ".") + @"</li>");
                    sbNewFileText.AppendLine(@"<li><b>" + ReportServiceRes.Temperature + @":</b> " + mikeSourceStartEndModelListLocal[0].SourceTemperatureStart_C.ToString("F1").Replace(",", ".") + @" " + ReportServiceRes.Celcius + @"</li>");
                    sbNewFileText.AppendLine(@"<li><b>" + ReportServiceRes.Salinity + @":</b> " + mikeSourceStartEndModelListLocal[0].SourceSalinityStart_PSU.ToString("F1").Replace(",", ".") + @" " + ReportServiceRes.PSU + @"</li>");
                }
                else
                {
                    sbNewFileText.AppendLine(@"<li><b>" + ReportServiceRes.IsNotContinuous + @"</b></li>");

                    int CountMikeSourceStartEnd = 0;
                    foreach (MikeSourceStartEndModel mssem in mikeSourceStartEndModelListLocal)
                    {
                        CountMikeSourceStartEnd += 1;
                        sbNewFileText.AppendLine(@"<br /><b>" + ReportServiceRes.Spill + @": " + CountMikeSourceStartEnd + "</b><br />");
                        sbNewFileText.AppendLine(string.Format(@"<li><b>" + ReportServiceRes.SpillStartTime + @":</b> {0:yyyy/MM/dd HH:mm:ss tt} (UTC)</li>", mssem.StartDateAndTime_Local));
                        sbNewFileText.AppendLine(string.Format(@"<li><b>" + ReportServiceRes.SpillEndTime + @":</b> {0:yyyy/MM/dd HH:mm:ss tt} (UTC)</li>", mssem.EndDateAndTime_Local));
                        sbNewFileText.AppendLine(@"<li><b>" + ReportServiceRes.FlowStart + @":</b> " + ((double)mssem.SourceFlowStart_m3_day / 24 / 3600).ToString("F6").Replace(",", ".") + @" (m3/s)  " + ((double)mssem.SourceFlowStart_m3_day).ToString("F0").Replace(",", ".") + @" (m3/" + ReportServiceRes.DayLowerCase + @")</li>");
                        sbNewFileText.AppendLine(@"<li><b>" + ReportServiceRes.FlowEnd + @":</b> " + ((double)mssem.SourceFlowEnd_m3_day / 24 / 3600).ToString("F6").Replace(",", ".") + @" (m3/s)  " + ((double)mssem.SourceFlowEnd_m3_day).ToString("F0").Replace(",", ".") + @" (m3/" + ReportServiceRes.DayLowerCase + @")</li>");
                        sbNewFileText.AppendLine(@"<li><b>" + ReportServiceRes.FCMPNPer100MLStart + @":</b> " + ((double)mssem.SourcePollutionStart_MPN_100ml).ToString("F0").Replace(",", ".") + @"</li>");
                        sbNewFileText.AppendLine(@"<li><b>" + ReportServiceRes.FCMPNPer100MLEnd + @":</b> " + ((double)mssem.SourcePollutionEnd_MPN_100ml).ToString("F0").Replace(",", ".") + @"</li>");
                        sbNewFileText.AppendLine(@"<li><b>" + ReportServiceRes.TemperatureStart + @":</b> " + ((double)mssem.SourceTemperatureStart_C).ToString("F0").Replace(",", ".") + @" " + ReportServiceRes.Celcius + @"</li>");
                        sbNewFileText.AppendLine(@"<li><b>" + ReportServiceRes.TemperatureEnd + @":</b> " + ((double)mssem.SourceTemperatureEnd_C).ToString("F0").Replace(",", ".") + @" " + ReportServiceRes.Celcius + @"</li>");
                        sbNewFileText.AppendLine(@"<li><b>" + ReportServiceRes.SalinityStart + @":</b> " + ((double)mssem.SourceSalinityStart_PSU).ToString("F0").Replace(",", ".") + @" " + ReportServiceRes.PSU + @"</li>");
                        sbNewFileText.AppendLine(@"<li><b>" + ReportServiceRes.SalinityEnd + @":</b> " + ((double)mssem.SourceSalinityEnd_PSU).ToString("F0").Replace(",", ".") + @" " + ReportServiceRes.PSU + @"</li>");
                    }
                }
                sbNewFileText.AppendLine(@"</ul>");
            }


            sbNewFileText.AppendLine(@"<iframe src=""about:"" width=""600"" height=""1"" />");
            sbNewFileText.AppendLine(@"]]></description>");

            #endregion Source Description

            sbNewFileText.Append(" <Folder>");
            sbNewFileText.Append("    <name>" + ReportServiceRes.SourceIncluded + @"</name>");
            sbNewFileText.AppendLine(@"    <visibility>0</visibility>");

            for (int i = 1; i < 1000; i++)
            {
                PFSSection pfsSectionSource = pfsFile.GetSectionFromHandle("FemEngineHD/HYDRODYNAMIC_MODULE/SOURCES/SOURCE_" + i.ToString());

                if (pfsSectionSource == null)
                {
                    break;
                }

                int? SourceIncluded = GetSourceIncluded(pfsSectionSource, reportTag);
                if (SourceIncluded == null)
                {
                    pfsFile.Close();
                    return;
                }

                if (SourceIncluded == 1)
                {
                    MikeSourceModel mikeSourceModelLocal = (from msl in mikeSourceModelList
                                                            where msl.SourceNumberString == "SOURCE_" + i.ToString()
                                                            select msl).FirstOrDefault<MikeSourceModel>();

                    if (mikeSourceModelLocal == null)
                    {
                        reportTag.Error = string.Format(ReportServiceRes.CouldNotFind_With_Equal_, ReportServiceRes.MikeSource, ReportServiceRes.SourceNumberString, "Source_" + i.ToString());
                        pfsFile.Close();
                        return;
                    }

                    WriteKMLSourcePlacemark(sbNewFileText, pfsFile, pfsSectionSource, i, mikeSourceModelLocal, reportTag);
                    if (!string.IsNullOrWhiteSpace(reportTag.Error))
                    {
                        pfsFile.Close();
                        return;
                    }
                }

            }

            sbNewFileText.Append("</Folder>");

            sbNewFileText.Append("<Folder><name>" + ReportServiceRes.SourceNotIncluded + @"</name>");
            sbNewFileText.AppendLine(@"<visibility>0</visibility>");

            // showing not used sources 
            for (int i = 1; i < 1000; i++)
            {
                PFSSection pfsSectionSource = pfsFile.GetSectionFromHandle("FemEngineHD/HYDRODYNAMIC_MODULE/SOURCES/SOURCE_" + i.ToString());

                if (pfsSectionSource == null)
                {
                    break;
                }

                int? SourceIncluded = GetSourceIncluded(pfsSectionSource, reportTag);
                if (SourceIncluded == null)
                {
                    pfsFile.Close();
                    return;
                }

                if (SourceIncluded == 0)
                {
                    MikeSourceModel mikeSourceModelLocal = (from msl in mikeSourceModelList
                                                            where msl.SourceNumberString == "SOURCE_" + i.ToString()
                                                            select msl).FirstOrDefault<MikeSourceModel>();

                    if (mikeSourceModelLocal == null)
                    {
                        reportTag.Error = string.Format(ReportServiceRes.CouldNotFind_With_Equal_, ReportServiceRes.MikeSource, ReportServiceRes.SourceNumberString, "Source_" + i.ToString());
                        pfsFile.Close();
                        return;
                    }

                    WriteKMLSourcePlacemark(sbNewFileText, pfsFile, pfsSectionSource, i, mikeSourceModelLocal, reportTag);
                    if (!string.IsNullOrWhiteSpace(reportTag.Error))
                    {
                        pfsFile.Close();
                        return;
                    }
                }

            }

            sbNewFileText.Append("</Folder>");

            sbNewFileText.Append("</Folder>");

            return;
        }
        private void WriteKMLPollutionLimitsContourLine(DfsuFile dfsuFile, StringBuilder sbNewFileText, List<ElementLayer> elementLayerList, List<NodeLayer> topNodeLayerList, List<NodeLayer> bottomNodeLayerList, ReportTag reportTag)
        {
            List<List<ContourPolygon>> ContourPolygonListList = new List<List<ContourPolygon>>();
            List<int> LayerList = new List<int>();

            int PercentCompleted = 3;
            List<float> ContourValueList = new List<float>();

            ReportTreeNode reportTreeNode = reportTag.ReportTreeNodeList.Where(c => c.Text == "Mike_Scenario_Special_Result_KML_Limit_Values").FirstOrDefault();
            if (reportTreeNode != null)
            {
                foreach (ReportConditionTextField reportConditionTextField in reportTreeNode.dbFilteringTextFieldList)
                {
                    if (reportConditionTextField.ReportCondition == ReportConditionEnum.ReportConditionEqual)
                    {
                        ContourValueList = reportConditionTextField.TextCondition.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(c => float.Parse(c)).ToList();
                    }
                }
            }

            if (ContourValueList.Count == 0)
            {
                reportTag.Error = string.Format(ReportServiceRes._IsRequiredIn_, "Mike_Scenario_Special_Result_KML_Limit_Values", "WriteKMLPollutionLimitsContourLine");
                return;
            }

            int ItemNumber = 0;

            // getting the ItemNumber
            foreach (IDfsSimpleDynamicItemInfo dfsDyInfo in dfsuFile.ItemInfo)
            {
                if (dfsDyInfo.Quantity.Item == eumItem.eumIConcentration
                    || dfsDyInfo.Quantity.Item == eumItem.eumIConcentration1
                    || dfsDyInfo.Quantity.Item == eumItem.eumIConcentration_1
                    || dfsDyInfo.Quantity.Item == eumItem.eumIConcentration_2
                    || dfsDyInfo.Quantity.Item == eumItem.eumIConcentration_3
                    || dfsDyInfo.Quantity.Item == eumItem.eumIConcentration_4)
                {
                    ItemNumber = dfsDyInfo.ItemNumber;
                    break;
                }
            }

            if (ItemNumber == 0)
            {
                reportTag.Error = string.Format(ReportServiceRes.CouldNotFind__, ReportServiceRes.ParameterType, "eumIConcentration1");
                return;
            }

            //int pcount = 0;
            int CountLayer = (dfsuFile.NumberOfSigmaLayers == 0 ? 1 : dfsuFile.NumberOfSigmaLayers);
            int CountAt = 0;

            int TotalCount = CountLayer * ContourValueList.Count;

            for (int Layer = 1; Layer <= CountLayer; Layer++)
            {
                if (!LayerList.Contains(Layer))
                {
                    LayerList.Add(Layer);
                }

                #region Top of Layer
                int CountContour = 1;
                foreach (float ContourValue in ContourValueList)
                {
                    CountAt += 1;

                    int PercentCompletedTemp = ((int)(((CountAt - 1) * (float)100.0) / (float)TotalCount));
                    if (PercentCompleted > 3 && PercentCompleted != PercentCompletedTemp)
                    {
                        string retStr = UpdateAppTaskPercentCompleted(reportTag.AppTaskID, PercentCompleted);
                    }
                    PercentCompleted = PercentCompletedTemp;

                    List<Node> AllNodeList = new List<Node>();
                    List<ContourPolygon> ContourPolygonList = new List<ContourPolygon>();

                    for (int timeStep = 0; timeStep < dfsuFile.NumberOfTimeSteps; timeStep++)
                    {
                        float[] ValueList = (float[])dfsuFile.ReadItemTimeStep(ItemNumber, timeStep).Data;

                        for (int i = 0; i < elementLayerList.Count; i++)
                        {
                            if (elementLayerList[i].Element.Value < ValueList[i])
                            {
                                elementLayerList[i].Element.Value = ValueList[i];
                            }
                        }
                    }

                    List<ElementLayer> elementLayerListAtLayer = elementLayerList.Where(c => c.Layer == Layer).ToList();
                    List<NodeLayer> topNodeLayerListAtLayer = topNodeLayerList.Where(c => c.Layer == Layer).ToList();

                    foreach (NodeLayer nl in topNodeLayerListAtLayer)
                    {
                        float Total = 0;
                        foreach (Element element in nl.Node.ElementList)
                        {
                            Total += element.Value;
                        }
                        nl.Node.Value = Total / nl.Node.ElementList.Count;
                    }


                    List<NodeLayer> AboveNodeLayerList = (from n in topNodeLayerListAtLayer
                                                          where (n.Node.Value >= ContourValue)
                                                          && n.Layer == Layer
                                                          select n).ToList<NodeLayer>();

                    foreach (NodeLayer snl in AboveNodeLayerList)
                    {
                        List<NodeLayer> EndNodeLayerList = null;

                        List<NodeLayer> NodeLayerConnectedList = (from nll in topNodeLayerListAtLayer
                                                                  from n in snl.Node.ConnectNodeList
                                                                  where (n.ID == nll.Node.ID)
                                                                  select nll).ToList<NodeLayer>();

                        EndNodeLayerList = (from nll in NodeLayerConnectedList
                                            where (nll.Node.ID != snl.Node.ID)
                                            && (nll.Node.Value < ContourValue)
                                            && nll.Layer == Layer
                                            select nll).ToList<NodeLayer>();

                        foreach (NodeLayer en in EndNodeLayerList)
                        {
                            AllNodeList.Add(en.Node);
                        }

                        if (snl.Node.Code != 0)
                        {
                            AllNodeList.Add(snl.Node);
                        }

                    }

                    List<Element> TempUniqueElementList = new List<Element>();
                    List<Element> UniqueElementList = new List<Element>();
                    foreach (ElementLayer el in elementLayerListAtLayer)
                    {
                        if (dfsuFile.DfsuFileType == DfsuFileType.Dfsu3DSigma || dfsuFile.DfsuFileType == DfsuFileType.Dfsu3DSigmaZ)
                        {
                            if (el.Element.Type == 32)
                            {
                                bool NodeBigger = false;
                                for (int i = 3; i < 6; i++)
                                {
                                    if (el.Element.NodeList[i].Value >= ContourValue)
                                    {
                                        NodeBigger = true;
                                        break;
                                    }
                                }
                                if (NodeBigger)
                                {
                                    int countTrue = 0;
                                    for (int i = 3; i < 6; i++)
                                    {
                                        if (el.Element.NodeList[i].Value >= ContourValue && el.Element.NodeList[i].Code == 0)
                                        {
                                            countTrue += 1;
                                        }
                                    }
                                    if (countTrue != el.Element.NodeList.Count)
                                    {
                                        TempUniqueElementList.Add(el.Element);
                                    }
                                }
                            }
                            else if (el.Element.Type == 33)
                            {
                                bool NodeBigger = false;
                                for (int i = 4; i < 8; i++)
                                {
                                    if (el.Element.NodeList[i].Value >= ContourValue)
                                    {
                                        NodeBigger = true;
                                        break;
                                    }
                                }
                                if (NodeBigger)
                                {
                                    int countTrue = 0;
                                    for (int i = 4; i < 8; i++)
                                    {
                                        if (el.Element.NodeList[i].Value >= ContourValue && el.Element.NodeList[i].Code == 0)
                                        {
                                            countTrue += 1;
                                        }
                                    }
                                    if (countTrue != el.Element.NodeList.Count)
                                    {
                                        TempUniqueElementList.Add(el.Element);
                                    }
                                }
                            }
                            else
                            {
                                reportTag.Error = string.Format(ReportServiceRes.ElementType_IsNotSupported, el.Element.Type.ToString());
                                return;
                            }
                        }
                        else if (dfsuFile.DfsuFileType == DfsuFileType.Dfsu2D)
                        {
                            bool NodeBigger = false;
                            for (int i = 0; i < el.Element.NodeList.Count; i++)
                            {
                                if (el.Element.NodeList[i].Value >= ContourValue)
                                {
                                    NodeBigger = true;
                                    break;
                                }
                            }
                            if (NodeBigger)
                            {
                                int countTrue = 0;
                                for (int i = 0; i < el.Element.NodeList.Count; i++)
                                {
                                    if (el.Element.NodeList[i].Value >= ContourValue && el.Element.NodeList[i].Code == 0)
                                    {
                                        countTrue += 1;
                                    }
                                }
                                if (countTrue != el.Element.NodeList.Count)
                                {
                                    TempUniqueElementList.Add(el.Element);
                                }
                            }
                        }
                    }

                    UniqueElementList = (from el in TempUniqueElementList select el).Distinct().ToList<Element>();

                    // filling InterpolatedContourNodeList
                    InterpolatedContourNodeList = new List<Node>();

                    int count = 0;
                    foreach (Element el in UniqueElementList)
                    {
                        count += 1;
                        if (dfsuFile.DfsuFileType == DfsuFileType.Dfsu3DSigma || dfsuFile.DfsuFileType == DfsuFileType.Dfsu3DSigmaZ)
                        {
                            if (el.Type == 32)
                            {
                                if (el.NodeList[3].Value >= ContourValue && el.NodeList[4].Value < ContourValue)
                                {
                                    InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[3], el.NodeList[4], ContourValue);
                                }
                                if (el.NodeList[3].Value >= ContourValue && el.NodeList[5].Value < ContourValue)
                                {
                                    InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[3], el.NodeList[5], ContourValue);
                                }
                                if (el.NodeList[4].Value >= ContourValue && el.NodeList[3].Value < ContourValue)
                                {
                                    InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[4], el.NodeList[3], ContourValue);
                                }
                                if (el.NodeList[4].Value >= ContourValue && el.NodeList[5].Value < ContourValue)
                                {
                                    InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[4], el.NodeList[5], ContourValue);
                                }
                                if (el.NodeList[5].Value >= ContourValue && el.NodeList[4].Value < ContourValue)
                                {
                                    InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[5], el.NodeList[4], ContourValue);
                                }
                                if (el.NodeList[5].Value >= ContourValue && el.NodeList[3].Value < ContourValue)
                                {
                                    InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[5], el.NodeList[3], ContourValue);
                                }
                            }
                            else if (el.Type == 33)
                            {
                                if (el.NodeList[4].Value >= ContourValue && el.NodeList[5].Value < ContourValue)
                                {
                                    InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[4], el.NodeList[5], ContourValue);
                                }
                                if (el.NodeList[4].Value >= ContourValue && el.NodeList[7].Value < ContourValue)
                                {
                                    InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[4], el.NodeList[7], ContourValue);
                                }
                                if (el.NodeList[5].Value >= ContourValue && el.NodeList[4].Value < ContourValue)
                                {
                                    InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[5], el.NodeList[4], ContourValue);
                                }
                                if (el.NodeList[5].Value >= ContourValue && el.NodeList[6].Value < ContourValue)
                                {
                                    InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[5], el.NodeList[6], ContourValue);
                                }
                                if (el.NodeList[6].Value >= ContourValue && el.NodeList[5].Value < ContourValue)
                                {
                                    InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[6], el.NodeList[5], ContourValue);
                                }
                                if (el.NodeList[6].Value >= ContourValue && el.NodeList[7].Value < ContourValue)
                                {
                                    InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[6], el.NodeList[7], ContourValue);
                                }
                                if (el.NodeList[7].Value >= ContourValue && el.NodeList[4].Value < ContourValue)
                                {
                                    InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[7], el.NodeList[4], ContourValue);
                                }
                                if (el.NodeList[7].Value >= ContourValue && el.NodeList[6].Value < ContourValue)
                                {
                                    InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[7], el.NodeList[6], ContourValue);
                                }
                            }
                            else
                            {
                                reportTag.Error = string.Format(ReportServiceRes.ElementType_IsNotSupported, el.Type.ToString());
                                return;
                            }
                        }
                        else if (dfsuFile.DfsuFileType == DfsuFileType.Dfsu2D)
                        {
                            if (el.Type == 21)
                            {
                                if (el.NodeList[0].Value >= ContourValue && el.NodeList[1].Value < ContourValue)
                                {
                                    InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[0], el.NodeList[1], ContourValue);
                                }
                                if (el.NodeList[0].Value >= ContourValue && el.NodeList[2].Value < ContourValue)
                                {
                                    InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[0], el.NodeList[2], ContourValue);
                                }
                                if (el.NodeList[1].Value >= ContourValue && el.NodeList[0].Value < ContourValue)
                                {
                                    InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[1], el.NodeList[0], ContourValue);
                                }
                                if (el.NodeList[1].Value >= ContourValue && el.NodeList[2].Value < ContourValue)
                                {
                                    InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[1], el.NodeList[2], ContourValue);
                                }
                                if (el.NodeList[2].Value >= ContourValue && el.NodeList[1].Value < ContourValue)
                                {
                                    InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[2], el.NodeList[1], ContourValue);
                                }
                                if (el.NodeList[2].Value >= ContourValue && el.NodeList[0].Value < ContourValue)
                                {
                                    InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[2], el.NodeList[0], ContourValue);
                                }
                            }
                            else if (el.Type == 24)
                            {
                            }
                            else if (el.Type == 25)
                            {
                                if (el.NodeList[0].Value >= ContourValue && el.NodeList[1].Value < ContourValue)
                                {
                                    InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[0], el.NodeList[1], ContourValue);
                                }
                                if (el.NodeList[0].Value >= ContourValue && el.NodeList[3].Value < ContourValue)
                                {
                                    InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[0], el.NodeList[3], ContourValue);
                                }
                                if (el.NodeList[1].Value >= ContourValue && el.NodeList[0].Value < ContourValue)
                                {
                                    InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[1], el.NodeList[0], ContourValue);
                                }
                                if (el.NodeList[1].Value >= ContourValue && el.NodeList[2].Value < ContourValue)
                                {
                                    InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[1], el.NodeList[2], ContourValue);
                                }
                                if (el.NodeList[2].Value >= ContourValue && el.NodeList[1].Value < ContourValue)
                                {
                                    InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[2], el.NodeList[1], ContourValue);
                                }
                                if (el.NodeList[2].Value >= ContourValue && el.NodeList[3].Value < ContourValue)
                                {
                                    InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[2], el.NodeList[3], ContourValue);
                                }
                                if (el.NodeList[3].Value >= ContourValue && el.NodeList[0].Value < ContourValue)
                                {
                                    InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[3], el.NodeList[0], ContourValue);
                                }
                                if (el.NodeList[3].Value >= ContourValue && el.NodeList[2].Value < ContourValue)
                                {
                                    InsertNewNodeInInterpolatedContourNodeList(dfsuFile, el.NodeList[3], el.NodeList[2], ContourValue);
                                }
                            }
                            else
                            {
                                reportTag.Error = string.Format(ReportServiceRes.ElementType_IsNotSupported, el.Type.ToString());
                                return;
                            }
                        }
                    }
                    List<Node> UniqueNodeList = (from n in AllNodeList orderby n.ID select n).Distinct().ToList<Node>();

                    ForwardVector = new Dictionary<String, Vector>();
                    BackwardVector = new Dictionary<String, Vector>();

                    // ------------------------- new code --------------------------
                    //                     

                    foreach (Element el in UniqueElementList)
                    {
                        if (el.Type == 21)
                        {
                            FillVectors21_32(el, UniqueElementList, ContourValue, false, true, reportTag);
                        }
                        else if (el.Type == 24)
                        {
                            reportTag.Error = string.Format(ReportServiceRes.ElementType_IsNotSupported, el.Type.ToString());
                            return;
                        }
                        else if (el.Type == 25)
                        {
                            FillVectors25_33(el, UniqueElementList, ContourValue, false, true, reportTag);
                        }
                        else if (el.Type == 32)
                        {
                            FillVectors21_32(el, UniqueElementList, ContourValue, true, true, reportTag);
                        }
                        else if (el.Type == 33)
                        {
                            FillVectors25_33(el, UniqueElementList, ContourValue, true, true, reportTag);
                        }
                        else
                        {
                            reportTag.Error = string.Format(ReportServiceRes.ElementType_IsNotSupported, el.Type.ToString());
                            return;
                        }

                    }

                    bool MoreContourLine = true;
                    MapInfoService mapInfoService = new MapInfoService((reportTag.Language == "fr" ? LanguageEnum.fr : LanguageEnum.en), _User);
                    while (MoreContourLine && ForwardVector.Count > 0)
                    {
                        List<Node> FinalContourNodeList = new List<Node>();
                        Vector LastVector = new Vector();
                        LastVector = ForwardVector.First().Value;
                        FinalContourNodeList.Add(LastVector.StartNode);
                        FinalContourNodeList.Add(LastVector.EndNode);
                        ForwardVector.Remove(LastVector.StartNode.ID.ToString() + "," + LastVector.EndNode.ID.ToString());
                        BackwardVector.Remove(LastVector.EndNode.ID.ToString() + "," + LastVector.StartNode.ID.ToString());
                        bool PolygonCompleted = false;
                        while (!PolygonCompleted)
                        {
                            List<string> KeyStrList = (from k in ForwardVector.Keys
                                                       where k.StartsWith(LastVector.EndNode.ID.ToString() + ",")
                                                       && !k.EndsWith("," + LastVector.StartNode.ID.ToString())
                                                       select k).ToList<string>();

                            if (KeyStrList.Count != 1)
                            {
                                KeyStrList = (from k in BackwardVector.Keys
                                              where k.StartsWith(LastVector.EndNode.ID.ToString() + ",")
                                              && !k.EndsWith("," + LastVector.StartNode.ID.ToString())
                                              select k).ToList<string>();

                                if (KeyStrList.Count != 1)
                                {
                                    PolygonCompleted = true;
                                    break;
                                }
                                else
                                {
                                    LastVector = BackwardVector[KeyStrList[0]];
                                    BackwardVector.Remove(LastVector.StartNode.ID.ToString() + "," + LastVector.EndNode.ID.ToString());
                                    ForwardVector.Remove(LastVector.EndNode.ID.ToString() + "," + LastVector.StartNode.ID.ToString());
                                }
                            }
                            else
                            {
                                LastVector = ForwardVector[KeyStrList[0]];
                                ForwardVector.Remove(LastVector.StartNode.ID.ToString() + "," + LastVector.EndNode.ID.ToString());
                                BackwardVector.Remove(LastVector.EndNode.ID.ToString() + "," + LastVector.StartNode.ID.ToString());
                            }
                            FinalContourNodeList.Add(LastVector.EndNode);
                            if (FinalContourNodeList[FinalContourNodeList.Count - 1] == FinalContourNodeList[0])
                            {
                                PolygonCompleted = true;
                            }
                        }

                        if (mapInfoService.CalculateAreaOfPolygon(FinalContourNodeList) < 0)
                        {
                            FinalContourNodeList.Reverse();
                        }

                        FinalContourNodeList.Add(FinalContourNodeList[0]);
                        ContourPolygon contourPolygon = new ContourPolygon() { };
                        contourPolygon.ContourNodeList = FinalContourNodeList;
                        contourPolygon.ContourValue = ContourValue;
                        contourPolygon.Layer = Layer;

                        ContourPolygonList.Add(contourPolygon);

                        if (ForwardVector.Count == 0)
                        {
                            MoreContourLine = false;
                        }
                    }

                    ContourPolygonListList.Add(ContourPolygonList);

                    CountContour += 1;
                }
                #endregion Top of Layer

                #region Bottom of Layer
                //// 
                //if (Layer == dfsuFile.NumberOfSigmaLayers)
                //{
                //    sbKMLPollutionLimitsContour.AppendLine(string.Format(@"<Folder><name>Bottom of Layer [{0}]</name>", Layer));
                //    sbKMLPollutionLimitsContour.AppendLine(@"<visibility>0</visibility>");
                //    CountContour = 1;
                //    foreach (float ContourValue in ContourValueList)
                //    {
                //        CountAt += 1;
                //        sbKMLPollutionLimitsContour.AppendLine(string.Format(@"<Folder><name>Contour Value [{0}]</name>", ContourValue));
                //        sbKMLPollutionLimitsContour.AppendLine(@"<visibility>0</visibility>");
                //        string AppTaskStatus = ((int)((CountAt * 100) / (SigmaLayerValueList.Count * ContourValueList.Count))).ToString() + " %";
                //        UpdateTask(AppTaskID, AppTaskStatus);

                //        List<Node> AllNodeList = new List<Node>();
                //        List<ContourPolygon> ContourPolygonList = new List<ContourPolygon>();

                //        //foreach (Dfs.Parameter.TimeSeriesValue v in p.TimeSeriesValueList)
                //        //{
                //        for (int timeStep = 0; timeStep < dfsuFile.NumberOfTimeSteps; timeStep++)
                //        {

                //            float[] ValueList = (float[])dfsuFile.ReadItemTimeStep(ItemNumber, timeStep).Data;

                //            for (int i = 0; i < ElementLayerList.Count; i++)
                //            {
                //                if (ElementLayerList[i].Element.Value < ValueList[i])
                //                {
                //                    ElementLayerList[i].Element.Value = ValueList[i];
                //                }
                //            }
                //        }
                //        //}

                //        foreach (NodeLayer nl in BottomNodeLayerList)
                //        {
                //            float Total = 0;
                //            foreach (Element element in nl.Node.ElementList)
                //            {
                //                Total += element.Value;
                //            }
                //            nl.Node.Value = Total / nl.Node.ElementList.Count;
                //        }

                //        List<NodeLayer> AboveNodeLayerList = new List<NodeLayer>();

                //        AboveNodeLayerList = (from n in BottomNodeLayerList
                //                              where (n.Node.Value >= ContourValue)
                //                              && n.Layer == Layer
                //                              select n).ToList<NodeLayer>();

                //        foreach (NodeLayer snl in AboveNodeLayerList)
                //        {
                //            List<NodeLayer> EndNodeLayerList = null;

                //            List<NodeLayer> NodeLayerConnectedList = (from nll in BottomNodeLayerList
                //                                                      from n in snl.Node.ConnectNodeList
                //                                                      where (n.ID == nll.Node.ID)
                //                                                      select nll).ToList<NodeLayer>();

                //            EndNodeLayerList = (from nll in NodeLayerConnectedList
                //                                where (nll.Node.ID != snl.Node.ID)
                //                                && (nll.Node.Value < ContourValue)
                //                                && nll.Layer == Layer
                //                                select nll).ToList<NodeLayer>();

                //            foreach (NodeLayer en in EndNodeLayerList)
                //            {
                //                AllNodeList.Add(en.Node);
                //            }

                //            if (snl.Node.Code != 0)
                //            {
                //                AllNodeList.Add(snl.Node);
                //            }

                //        }

                //        if (AllNodeList.Count == 0)
                //        {
                //            continue;
                //        }

                //        List<Element> TempUniqueElementList = new List<Element>();
                //        List<Element> UniqueElementList = new List<Element>();
                //        foreach (ElementLayer el in ElementLayerList.Where(l => l.Layer == Layer))
                //        {
                //            if (dfsuFile.DfsuFileType == DfsuFileType.Dfsu3DSigma || dfsuFile.DfsuFileType == DfsuFileType.Dfsu3DSigmaZ)
                //            {
                //                if (el.Element.Type == 32)
                //                {
                //                    bool NodeBigger = false;
                //                    for (int i = 0; i < 3; i++)
                //                    {
                //                        if (el.Element.NodeList[i].Value >= ContourValue)
                //                        {
                //                            NodeBigger = true;
                //                            break;
                //                        }
                //                    }
                //                    if (NodeBigger)
                //                    {
                //                        int countTrue = 0;
                //                        for (int i = 0; i < 3; i++)
                //                        {
                //                            if (el.Element.NodeList[i].Value >= ContourValue && el.Element.NodeList[i].Code == 0)
                //                            {
                //                                countTrue += 1;
                //                            }
                //                        }
                //                        if (countTrue != el.Element.NodeList.Count)
                //                        {
                //                            TempUniqueElementList.Add(el.Element);
                //                        }
                //                    }
                //                }
                //                else if (el.Element.Type == 33)
                //                {
                //                    bool NodeBigger = false;
                //                    for (int i = 0; i < 4; i++)
                //                    {
                //                        if (el.Element.NodeList[i].Value >= ContourValue)
                //                        {
                //                            NodeBigger = true;
                //                            break;
                //                        }
                //                    }
                //                    if (NodeBigger)
                //                    {
                //                        int countTrue = 0;
                //                        for (int i = 0; i < 4; i++)
                //                        {
                //                            if (el.Element.NodeList[i].Value >= ContourValue && el.Element.NodeList[i].Code == 0)
                //                            {
                //                                countTrue += 1;
                //                            }
                //                        }
                //                        if (countTrue != el.Element.NodeList.Count)
                //                        {
                //                            TempUniqueElementList.Add(el.Element);
                //                        }
                //                    }
                //                }
                //                else
                //                {
                //                    UpdateTask(AppTaskID, "");
                //                    throw new Exception("Element type is not supported: Element type = [" + el.Element.Type + "]");
                //                }
                //            }
                //            else if (dfsuFile.DfsuFileType == DfsuFileType.Dfsu2D)
                //            {
                //                bool NodeBigger = false;
                //                for (int i = 0; i < el.Element.NodeList.Count; i++)
                //                {
                //                    if (el.Element.NodeList[i].Value >= ContourValue)
                //                    {
                //                        NodeBigger = true;
                //                        break;
                //                    }
                //                }
                //                if (NodeBigger)
                //                {
                //                    int countTrue = 0;
                //                    for (int i = 0; i < el.Element.NodeList.Count; i++)
                //                    {
                //                        if (el.Element.NodeList[i].Value >= ContourValue && el.Element.NodeList[i].Code == 0)
                //                        {
                //                            countTrue += 1;
                //                        }
                //                    }
                //                    if (countTrue != el.Element.NodeList.Count)
                //                    {
                //                        TempUniqueElementList.Add(el.Element);
                //                    }
                //                }
                //            }
                //        }

                //        UniqueElementList = (from el in TempUniqueElementList select el).Distinct().ToList<Element>();

                //        // filling InterpolatedContourNodeList
                //        InterpolatedContourNodeList = new List<Node>();

                //        foreach (Element el in UniqueElementList)
                //        {
                //            if (dfsuFile.DfsuFileType == DfsuFileType.Dfsu3DSigma || dfsuFile.DfsuFileType == DfsuFileType.Dfsu3DSigmaZ)
                //            {
                //                if (el.Type == 32)
                //                {
                //                    if (el.NodeList[0].Value >= ContourValue && el.NodeList[1].Value < ContourValue)
                //                    {
                //                        InsertNewNodeInInterpolatedContourNodeList(el.NodeList[0], el.NodeList[1], ContourValue);
                //                    }
                //                    if (el.NodeList[0].Value >= ContourValue && el.NodeList[2].Value < ContourValue)
                //                    {
                //                        InsertNewNodeInInterpolatedContourNodeList(el.NodeList[0], el.NodeList[2], ContourValue);
                //                    }
                //                    if (el.NodeList[1].Value >= ContourValue && el.NodeList[0].Value < ContourValue)
                //                    {
                //                        InsertNewNodeInInterpolatedContourNodeList(el.NodeList[1], el.NodeList[0], ContourValue);
                //                    }
                //                    if (el.NodeList[1].Value >= ContourValue && el.NodeList[2].Value < ContourValue)
                //                    {
                //                        InsertNewNodeInInterpolatedContourNodeList(el.NodeList[1], el.NodeList[2], ContourValue);
                //                    }
                //                    if (el.NodeList[2].Value >= ContourValue && el.NodeList[1].Value < ContourValue)
                //                    {
                //                        InsertNewNodeInInterpolatedContourNodeList(el.NodeList[2], el.NodeList[1], ContourValue);
                //                    }
                //                    if (el.NodeList[2].Value >= ContourValue && el.NodeList[0].Value < ContourValue)
                //                    {
                //                        InsertNewNodeInInterpolatedContourNodeList(el.NodeList[2], el.NodeList[0], ContourValue);
                //                    }
                //                }
                //                else if (el.Type == 33)
                //                {
                //                    if (el.NodeList[0].Value >= ContourValue && el.NodeList[1].Value < ContourValue)
                //                    {
                //                        InsertNewNodeInInterpolatedContourNodeList(el.NodeList[0], el.NodeList[1], ContourValue);
                //                    }
                //                    if (el.NodeList[0].Value >= ContourValue && el.NodeList[3].Value < ContourValue)
                //                    {
                //                        InsertNewNodeInInterpolatedContourNodeList(el.NodeList[0], el.NodeList[3], ContourValue);
                //                    }
                //                    if (el.NodeList[1].Value >= ContourValue && el.NodeList[0].Value < ContourValue)
                //                    {
                //                        InsertNewNodeInInterpolatedContourNodeList(el.NodeList[1], el.NodeList[0], ContourValue);
                //                    }
                //                    if (el.NodeList[1].Value >= ContourValue && el.NodeList[2].Value < ContourValue)
                //                    {
                //                        InsertNewNodeInInterpolatedContourNodeList(el.NodeList[1], el.NodeList[2], ContourValue);
                //                    }
                //                    if (el.NodeList[2].Value >= ContourValue && el.NodeList[1].Value < ContourValue)
                //                    {
                //                        InsertNewNodeInInterpolatedContourNodeList(el.NodeList[2], el.NodeList[1], ContourValue);
                //                    }
                //                    if (el.NodeList[2].Value >= ContourValue && el.NodeList[3].Value < ContourValue)
                //                    {
                //                        InsertNewNodeInInterpolatedContourNodeList(el.NodeList[2], el.NodeList[3], ContourValue);
                //                    }
                //                    if (el.NodeList[3].Value >= ContourValue && el.NodeList[0].Value < ContourValue)
                //                    {
                //                        InsertNewNodeInInterpolatedContourNodeList(el.NodeList[3], el.NodeList[0], ContourValue);
                //                    }
                //                    if (el.NodeList[3].Value >= ContourValue && el.NodeList[2].Value < ContourValue)
                //                    {
                //                        InsertNewNodeInInterpolatedContourNodeList(el.NodeList[3], el.NodeList[2], ContourValue);
                //                    }
                //                }
                //                else
                //                {
                //                    UpdateTask(AppTaskID, "");
                //                    throw new Exception("Element type is not supported: Element type = [" + el.Type + "]");
                //                }
                //            }
                //            else
                //            {
                //                UpdateTask(AppTaskID, "");
                //                throw new Exception("Bottom does not exist outside the Dfsu3DSigma and Dfsu3DSigmaZ.");
                //            }
                //        }

                //        List<Node> UniqueNodeList = (from n in AllNodeList orderby n.ID select n).Distinct().ToList<Node>();

                //        ForwardVector = new Dictionary<String, Vector>();
                //        BackwardVector = new Dictionary<String, Vector>();

                //        // ------------------------- new code --------------------------
                //        //                     

                //        foreach (Element el in UniqueElementList)
                //        {
                //            if (el.Type == 21)
                //            {
                //                FillVectors21_32(el, UniqueElementList, ContourValue, AppTaskID, false, false);
                //            }
                //            else if (el.Type == 24)
                //            {
                //                UpdateTask(AppTaskID, "");
                //                throw new Exception("Element type is not supported: Element type = [" + el.Type + "]");
                //            }
                //            else if (el.Type == 25)
                //            {
                //                FillVectors25_33(el, UniqueElementList, ContourValue, AppTaskID, false, false);
                //            }
                //            else if (el.Type == 32)
                //            {
                //                FillVectors21_32(el, UniqueElementList, ContourValue, AppTaskID, true, false);
                //            }
                //            else if (el.Type == 33)
                //            {
                //                FillVectors25_33(el, UniqueElementList, ContourValue, AppTaskID, true, false);
                //            }
                //            else
                //            {
                //                UpdateTask(AppTaskID, "");
                //                throw new Exception("Element type is not supported: Element type = [" + el.Type + "]");
                //            }

                //        }

                //        //-------------- new code ------------------------


                //        bool MoreContourLine = true;
                //        while (MoreContourLine)
                //        {
                //            List<Node> FinalContourNodeList = new List<Node>();
                //            Vector LastVector = new Vector();
                //            LastVector = ForwardVector.First().Value;
                //            FinalContourNodeList.Add(LastVector.StartNode);
                //            FinalContourNodeList.Add(LastVector.EndNode);
                //            ForwardVector.Remove(LastVector.StartNode.ID.ToString() + "," + LastVector.EndNode.ID.ToString());
                //            BackwardVector.Remove(LastVector.EndNode.ID.ToString() + "," + LastVector.StartNode.ID.ToString());
                //            bool PolygonCompleted = false;
                //            while (!PolygonCompleted)
                //            {
                //                List<string> KeyStrList = (from k in ForwardVector.Keys
                //                                           where k.StartsWith(LastVector.EndNode.ID.ToString() + ",")
                //                                           && !k.EndsWith("," + LastVector.StartNode.ID.ToString())
                //                                           select k).ToList<string>();

                //                if (KeyStrList.Count != 1)
                //                {
                //                    KeyStrList = (from k in BackwardVector.Keys
                //                                  where k.StartsWith(LastVector.EndNode.ID.ToString() + ",")
                //                                  && !k.EndsWith("," + LastVector.StartNode.ID.ToString())
                //                                  select k).ToList<string>();

                //                    if (KeyStrList.Count != 1)
                //                    {
                //                        PolygonCompleted = true;
                //                        break;
                //                    }
                //                    else
                //                    {
                //                        LastVector = BackwardVector[KeyStrList[0]];
                //                        BackwardVector.Remove(LastVector.StartNode.ID.ToString() + "," + LastVector.EndNode.ID.ToString());
                //                        ForwardVector.Remove(LastVector.EndNode.ID.ToString() + "," + LastVector.StartNode.ID.ToString());
                //                    }
                //                }
                //                else
                //                {
                //                    LastVector = ForwardVector[KeyStrList[0]];
                //                    ForwardVector.Remove(LastVector.StartNode.ID.ToString() + "," + LastVector.EndNode.ID.ToString());
                //                    BackwardVector.Remove(LastVector.EndNode.ID.ToString() + "," + LastVector.StartNode.ID.ToString());
                //                }
                //                FinalContourNodeList.Add(LastVector.EndNode);
                //                if (FinalContourNodeList[FinalContourNodeList.Count - 1] == FinalContourNodeList[0])
                //                {
                //                    PolygonCompleted = true;
                //                }
                //            }

                //            if (CalculateAreaOfPolygon(FinalContourNodeList) < 0)
                //            {
                //                FinalContourNodeList.Reverse();
                //            }

                //            FinalContourNodeList.Add(FinalContourNodeList[0]);
                //            ContourPolygon contourPolygon = new ContourPolygon() { };
                //            contourPolygon.ContourNodeList = FinalContourNodeList;
                //            contourPolygon.ContourValue = ContourValue;
                //            ContourPolygonList.Add(contourPolygon);

                //            if (ForwardVector.Count == 0)
                //            {
                //                MoreContourLine = false;
                //            }
                //        }
                //        //sbKMLPollutionLimitsContour.AppendLine(@"<Folder>");
                //        //sbKMLPollutionLimitsContour.AppendLine(string.Format(@"<name>{0} Pollution Limits Contour</name>", ContourValue));
                //        //sbKMLPollutionLimitsContour.AppendLine(@"<visibility>0</visibility>");

                //        foreach (ContourPolygon contourPolygon in ContourPolygonList)
                //        {
                //            sbKMLPollutionLimitsContour.AppendLine(@"<Folder>");
                //            sbKMLPollutionLimitsContour.AppendLine(@"<visibility>0</visibility>");
                //            sbKMLPollutionLimitsContour.AppendLine(@"<Placemark>");
                //            sbKMLPollutionLimitsContour.AppendLine(@"<visibility>0</visibility>");
                //            if (contourPolygon.ContourValue >= 14 && contourPolygon.ContourValue < 88)
                //            {
                //                sbKMLPollutionLimitsContour.AppendLine(@"<styleUrl>#fc_14</styleUrl>");
                //            }
                //            else if (contourPolygon.ContourValue >= 88)
                //            {
                //                sbKMLPollutionLimitsContour.AppendLine(@"<styleUrl>#fc_88</styleUrl>");
                //            }
                //            else
                //            {
                //                sbKMLPollutionLimitsContour.AppendLine(@"<styleUrl>#fc_LT14</styleUrl>");
                //            }
                //            sbKMLPollutionLimitsContour.AppendLine(@"<Polygon>");
                //            sbKMLPollutionLimitsContour.AppendLine(@"<outerBoundaryIs>");
                //            sbKMLPollutionLimitsContour.AppendLine(@"<LinearRing>");
                //            sbKMLPollutionLimitsContour.AppendLine(@"<coordinates>");
                //            foreach (Node node in contourPolygon.ContourNodeList)
                //            {
                //                sbKMLPollutionLimitsContour.Append(string.Format(@"{0},{1},0 ", node.X, node.Y));
                //            }
                //            sbKMLPollutionLimitsContour.AppendLine(@"</coordinates>");
                //            sbKMLPollutionLimitsContour.AppendLine(@"</LinearRing>");
                //            sbKMLPollutionLimitsContour.AppendLine(@"</outerBoundaryIs>");
                //            sbKMLPollutionLimitsContour.AppendLine(@"</Polygon>");
                //            sbKMLPollutionLimitsContour.AppendLine(@"</Placemark>");
                //            sbKMLPollutionLimitsContour.AppendLine(@"</Folder>");
                //        }
                //        sbKMLPollutionLimitsContour.AppendLine(@"</Folder>");
                //        CountContour += 1;
                //    }
                //    sbKMLPollutionLimitsContour.AppendLine(@"</Folder>");
                //}
                #endregion Bottom of Layer
            }

            // Geneating Pollution Limits
            sbNewFileText.AppendLine(@"<Folder><name>" + ReportServiceRes.PollutionLimits + @"</name>");
            sbNewFileText.AppendLine(@"<visibility>0</visibility>");

            // Generating Polygons 
            sbNewFileText.AppendLine(@"<Folder><name>" + ReportServiceRes.Polygons + @"</name>");
            sbNewFileText.AppendLine(@"<visibility>0</visibility>");

            foreach (int Layer in LayerList)
            {
                if (Layer == 1)
                {
                    sbNewFileText.AppendLine(string.Format(@"<Folder><name>" + ReportServiceRes.TopOfLayer + @" [{0}] </name>", Layer));
                }
                else
                {
                    sbNewFileText.AppendLine(string.Format(@"<Folder><name>" + ReportServiceRes.TopOfLayer + @" [{0}] " + ReportServiceRes.BottomOfLayer + @" [{1}] </name>", Layer, Layer - 1));
                }
                sbNewFileText.AppendLine(@"<visibility>0</visibility>");

                foreach (List<ContourPolygon> contourPolygonList in ContourPolygonListList)
                {
                    foreach (ContourPolygon contourPolygon in contourPolygonList)
                    {
                        if (contourPolygon.Layer == Layer)
                        {
                            foreach (float contourValue in ContourValueList)
                            {
                                if (contourPolygon.ContourValue == contourValue)
                                {
                                    sbNewFileText.AppendLine(string.Format(@"<Folder><name>" + ReportServiceRes.ContourValue + @" [{0}]</name>", contourValue));

                                    sbNewFileText.AppendLine(@"<Folder>");
                                    sbNewFileText.AppendLine(@"<visibility>0</visibility>");
                                    sbNewFileText.AppendLine(@"<Placemark>");
                                    sbNewFileText.AppendLine(@"<visibility>0</visibility>");
                                    if (contourPolygon.ContourValue >= 14 && contourPolygon.ContourValue < 88)
                                    {
                                        sbNewFileText.AppendLine(@"<styleUrl>#fc_14</styleUrl>");
                                    }
                                    else if (contourPolygon.ContourValue >= 88)
                                    {
                                        sbNewFileText.AppendLine(@"<styleUrl>#fc_88</styleUrl>");
                                    }
                                    else
                                    {
                                        sbNewFileText.AppendLine(@"<styleUrl>#fc_LT14</styleUrl>");
                                    }
                                    sbNewFileText.AppendLine(@"<Polygon>");
                                    sbNewFileText.AppendLine(@"<outerBoundaryIs>");
                                    sbNewFileText.AppendLine(@"<LinearRing>");
                                    sbNewFileText.AppendLine(@"<coordinates>");
                                    foreach (Node node in contourPolygon.ContourNodeList)
                                    {
                                        sbNewFileText.Append(node.X.ToString().Replace(",", ".") + @"," + node.Y.ToString().Replace(",", ".") + "," + node.Z.ToString().Replace(",", ".") + " ");
                                    }
                                    sbNewFileText.AppendLine(@"</coordinates>");
                                    sbNewFileText.AppendLine(@"</LinearRing>");
                                    sbNewFileText.AppendLine(@"</outerBoundaryIs>");
                                    sbNewFileText.AppendLine(@"</Polygon>");
                                    sbNewFileText.AppendLine(@"</Placemark>");
                                    sbNewFileText.AppendLine(@"</Folder>");

                                    sbNewFileText.AppendLine(@"</Folder>");
                                }

                            }
                        }
                    }
                }
                sbNewFileText.AppendLine(@"</Folder>");
            }
            sbNewFileText.AppendLine(@"</Folder>");


            // Generating Depths 
            sbNewFileText.AppendLine(@"<Folder><name>" + ReportServiceRes.Depths + @"</name>");
            sbNewFileText.AppendLine(@"<visibility>0</visibility>");

            foreach (int Layer in LayerList)
            {
                if (Layer == 1)
                {
                    sbNewFileText.AppendLine(string.Format(@"<Folder><name>" + ReportServiceRes.TopOfLayer + @" [{0}] </name>", Layer));
                }
                else
                {
                    sbNewFileText.AppendLine(string.Format(@"<Folder><name>" + ReportServiceRes.TopOfLayer + @" [{0}] " + ReportServiceRes.BottomOfLayer + @" [{1}] </name>", Layer, Layer - 1));
                }
                sbNewFileText.AppendLine(@"<visibility>0</visibility>");

                foreach (List<ContourPolygon> contourPolygonList in ContourPolygonListList)
                {
                    foreach (ContourPolygon contourPolygon in contourPolygonList)
                    {
                        if (contourPolygon.Layer == Layer)
                        {
                            foreach (float contourValue in ContourValueList)
                            {
                                if (contourPolygon.ContourValue == contourValue)
                                {
                                    sbNewFileText.AppendLine(string.Format(@"<Folder><name>" + ReportServiceRes.ContourValue + @" [{0}]</name>", contourValue));

                                    sbNewFileText.AppendLine(@"<Folder>");
                                    sbNewFileText.AppendLine(@"<visibility>0</visibility>");
                                    sbNewFileText.AppendLine(@"<name>Depths</name>");
                                    foreach (Node node in contourPolygon.ContourNodeList)
                                    {
                                        sbNewFileText.AppendLine(@"<Placemark>");
                                        sbNewFileText.AppendLine(@"<visibility>0</visibility>");
                                        sbNewFileText.AppendLine(@"<name>" + node.Z.ToString("F1").Replace(",", ".") + "(m)</name>");
                                        if (contourPolygon.ContourValue >= 14 && contourPolygon.ContourValue < 88)
                                        {
                                            sbNewFileText.AppendLine(@"<styleUrl>#fc_14</styleUrl>");
                                        }
                                        else if (contourPolygon.ContourValue >= 88)
                                        {
                                            sbNewFileText.AppendLine(@"<styleUrl>#fc_88</styleUrl>");
                                        }
                                        else
                                        {
                                            sbNewFileText.AppendLine(@"<styleUrl>#fc_LT14</styleUrl>");
                                        }
                                        sbNewFileText.AppendLine(@"<Point>");
                                        sbNewFileText.AppendLine(@"<coordinates>");
                                        sbNewFileText.Append(node.X.ToString().Replace(",", ".") + @"," + node.Y.ToString().Replace(",", ".") + "," + node.Z.ToString().Replace(",", ".") + " ");
                                        sbNewFileText.AppendLine(@"</coordinates>");
                                        sbNewFileText.AppendLine(@"</Point>");
                                        sbNewFileText.AppendLine(@"</Placemark>");
                                    }
                                    sbNewFileText.AppendLine(@"</Folder>");

                                    sbNewFileText.AppendLine(@"</Folder>");
                                }
                            }
                        }
                    }
                }
                sbNewFileText.AppendLine(@"</Folder>");
            }
            sbNewFileText.AppendLine(@"</Folder>");

            sbNewFileText.AppendLine(@"</Folder>");

            string retStr2 = UpdateAppTaskPercentCompleted(reportTag.AppTaskID, 100);

        }
        private void WriteKMLSourcePlacemark(StringBuilder sbNewFileText, PFSFile pfsFile, PFSSection pfsSectionSource, int SourceNumber, MikeSourceModel mikeSourceModel, ReportTag reportTag)
        {
            MikeSourceStartEndService mikeSourceStartEndService = new MikeSourceStartEndService((reportTag.Language == "fr" ? LanguageEnum.fr : LanguageEnum.en), _User);

            string SourceName = GetSourceName(pfsSectionSource, reportTag);
            if (string.IsNullOrWhiteSpace(SourceName))
            {
                reportTag.Error = ReportServiceRes.MIKESourceNameIsEmpty;
                return;
            }

            int? SourceIncluded = GetSourceIncluded(pfsSectionSource, reportTag);
            if (SourceIncluded == null)
            {
                return;
            }

            float? SourceFlow = GetSourceFlow(pfsSectionSource, reportTag);
            if (SourceFlow == null)
            {
                return;
            }

            Coord SourceCoord = GetSourceCoord(pfsSectionSource, reportTag);
            if (SourceCoord.Lat == 0.0f || SourceCoord.Lng == 0.0f)
            {
                return;
            }

            List<Coord> coordList = new List<Coord>()
                    {
                        SourceCoord,
                    };

            PFSSection pfsSectionSourceTransport = pfsFile.GetSectionFromHandle("FemEngineHD/TRANSPORT_MODULE/SOURCES/SOURCE_" + SourceNumber.ToString() + "/COMPONENT_1");

            if (pfsSectionSourceTransport == null)
            {
                return;
            }

            float? SourcePollution = GetSourcePollution(pfsSectionSourceTransport, reportTag);
            if (SourcePollution == null)
            {
                return;
            }

            int? SourcePollutionContinuous = GetSourcePollutionContinuous(pfsSectionSourceTransport, reportTag);
            if (SourcePollutionContinuous == null)
            {
                return;
            }

            PFSSection pfsSectionSourceTemperature = pfsFile.GetSectionFromHandle("FemEngineHD/HYDRODYNAMIC_MODULE/TEMPERATURE_SALINITY_MODULE/SOURCES/SOURCE_" + SourceNumber.ToString() + "/TEMPERATURE");

            if (pfsSectionSourceTemperature == null)
            {
                return;
            }

            float? SourceTemperature = GetSourceTemperature(pfsSectionSourceTemperature, reportTag);
            if (SourceTemperature == null)
            {
                return;
            }

            PFSSection pfsSectionSourceSalinity = pfsFile.GetSectionFromHandle("FemEngineHD/HYDRODYNAMIC_MODULE/TEMPERATURE_SALINITY_MODULE/SOURCES/SOURCE_" + SourceNumber.ToString() + "/Salinity");

            if (pfsSectionSourceSalinity == null)
            {
                return;
            }

            float? SourceSalinity = GetSourceSalinity(pfsSectionSourceSalinity, reportTag);
            if (SourceSalinity == null)
            {
                return;
            }


            sbNewFileText.Append("<Placemark>");

            if (SourceIncluded == 1)
            {
                if (mikeSourceModel.IsRiver)
                {
                    sbNewFileText.AppendLine(string.Format(@"<name>{0} (" + ReportServiceRes.UsedLowerCase + @")</name>", SourceName));
                    sbNewFileText.AppendLine(@"<styleUrl>#msn_blue-pushpin</styleUrl>");
                }
                else
                {
                    sbNewFileText.AppendLine(string.Format(@"<name>{0} (" + ReportServiceRes.UsedLowerCase + @")</name>", SourceName));
                    sbNewFileText.AppendLine(@"<styleUrl>#msn_grn-pushpin</styleUrl>");
                }
            }
            else
            {
                sbNewFileText.AppendLine(string.Format(@"<name>{0} (" + ReportServiceRes.NotUsedLowerCase + @")</name>", SourceName));
                sbNewFileText.AppendLine(@"<styleUrl>#msn_red-pushpin</styleUrl>");
            }
            sbNewFileText.AppendLine(@"<visibility>0</visibility>");
            sbNewFileText.AppendLine(@"<description><![CDATA[");
            sbNewFileText.AppendLine(string.Format(@"<h2>{0}</h2>", SourceName));
            sbNewFileText.AppendLine(@"<h3>" + ReportServiceRes.Effluent + @"</h3>");
            sbNewFileText.AppendLine(@"<ul>");
            sbNewFileText.AppendLine(@"<li><b>" + ReportServiceRes.Coordinates + @"</b>");
            sbNewFileText.AppendLine(string.Format(@"&nbsp;&nbsp;&nbsp; {0:F5} &nbsp; {1:F5}</li>", SourceCoord.Lat, SourceCoord.Lng).Replace(",", "."));

            List<MikeSourceStartEndModel> mikeSourceStartEndModelList = mikeSourceStartEndService.GetMikeSourceStartEndModelListWithMikeSourceIDDB(mikeSourceModel.MikeSourceID);
            if ((bool)mikeSourceModel.IsContinuous)
            {
                if (mikeSourceStartEndModelList.Count > 0)
                {
                    sbNewFileText.AppendLine(@"<li><b>" + ReportServiceRes.IsContinuous + "</b></li>");
                    sbNewFileText.AppendLine(string.Format(@"<li><b>" + ReportServiceRes.Flow + @":</b> {0:F6} (m3/s)  {1:F0} (m3/" + ReportServiceRes.DayLowerCase + @")</li>", mikeSourceStartEndModelList[0].SourceFlowStart_m3_day / 24 / 3600, mikeSourceStartEndModelList[0].SourceFlowStart_m3_day).ToString().Replace(",", "."));
                    sbNewFileText.AppendLine(string.Format(@"<li><b>" + ReportServiceRes.FCMPNPer100ML + @":</b> {0:F0}</li>", mikeSourceStartEndModelList[0].SourcePollutionStart_MPN_100ml).ToString().Replace(",", "."));
                    sbNewFileText.AppendLine(string.Format(@"<li><b>" + ReportServiceRes.Temperature + @":</b> {0:F0} " + ReportServiceRes.Celcius + @"</li>", mikeSourceStartEndModelList[0].SourceTemperatureStart_C).ToString().Replace(",", "."));
                    sbNewFileText.AppendLine(string.Format(@"<li><b>" + ReportServiceRes.Salinity + @":</b> {0:F0} " + ReportServiceRes.PSU + @"</li>", mikeSourceStartEndModelList[0].SourceSalinityStart_PSU).ToString().Replace(",", "."));
                }
            }
            else
            {
                sbNewFileText.AppendLine(@"<li><b>" + ReportServiceRes.IsNotContinuous + @"</b></li>");

                mikeSourceStartEndModelList = mikeSourceStartEndService.GetMikeSourceStartEndModelListWithMikeSourceIDDB(mikeSourceModel.MikeSourceID);
                int CountMikeSourceStartEnd = 0;
                foreach (MikeSourceStartEndModel mssem in mikeSourceStartEndModelList)
                {
                    CountMikeSourceStartEnd += 1;
                    sbNewFileText.AppendLine(@"<ul>");
                    sbNewFileText.AppendLine("<b>" + ReportServiceRes.Spill + @" " + CountMikeSourceStartEnd + "</b>");
                    sbNewFileText.AppendLine(string.Format(@"<li><b>" + ReportServiceRes.SpillStartTime + @":</b> {0:yyyy/MM/dd HH:mm:ss tt}</li>", mssem.StartDateAndTime_Local));
                    sbNewFileText.AppendLine(string.Format(@"<li><b>" + ReportServiceRes.SpillEndTime + @":</b> {0:yyyy/MM/dd HH:mm:ss tt}</li>", mssem.EndDateAndTime_Local));
                    sbNewFileText.AppendLine(string.Format(@"<li><b>" + ReportServiceRes.FlowStart + @":</b> {0:F6} (m3/s)  {1:F0} (m3/" + ReportServiceRes.DayLowerCase + @")</li>", mssem.SourceFlowStart_m3_day / 24 / 3600, mssem.SourceFlowStart_m3_day).ToString().Replace(",", "."));
                    sbNewFileText.AppendLine(string.Format(@"<li><b>" + ReportServiceRes.FlowEnd + @":</b> {0:F6} (m3/s)  {1:F0} (m3/" + ReportServiceRes.DayLowerCase + @")</li>", mssem.SourceFlowEnd_m3_day / 24 / 3600, mssem.SourceFlowEnd_m3_day).ToString().Replace(",", "."));
                    sbNewFileText.AppendLine(string.Format(@"<li><b>" + ReportServiceRes.FCMPNPer100MLStart + @":</b> {0:F0}</li>", mssem.SourcePollutionStart_MPN_100ml).ToString().Replace(",", "."));
                    sbNewFileText.AppendLine(string.Format(@"<li><b>" + ReportServiceRes.FCMPNPer100MLEnd + @":</b> {0:F0}</li>", mssem.SourcePollutionEnd_MPN_100ml).ToString().Replace(",", "."));
                    sbNewFileText.AppendLine(string.Format(@"<li><b>" + ReportServiceRes.TemperatureStart + @":</b> {0:F0} " + ReportServiceRes.Celcius + @"</li>", mssem.SourceTemperatureStart_C).ToString().Replace(",", "."));
                    sbNewFileText.AppendLine(string.Format(@"<li><b>" + ReportServiceRes.TemperatureEnd + @":</b> {0:F0} " + ReportServiceRes.Celcius + @"</li>", mssem.SourceTemperatureEnd_C).ToString().Replace(",", "."));
                    sbNewFileText.AppendLine(string.Format(@"<li><b>" + ReportServiceRes.SalinityStart + @":</b> {0:F0} " + ReportServiceRes.PSU + @"</li>", mssem.SourceSalinityStart_PSU).ToString().Replace(",", "."));
                    sbNewFileText.AppendLine(string.Format(@"<li><b>" + ReportServiceRes.SalinityEnd + @":</b> {0:F0} " + ReportServiceRes.PSU + @"</li>", mssem.SourceSalinityEnd_PSU).ToString().Replace(",", "."));
                    sbNewFileText.AppendLine(@"</ul>");
                }
            }
            sbNewFileText.AppendLine(@"</ul>");
            sbNewFileText.AppendLine(@"<iframe src=""about:"" width=""500"" height=""1"" />");
            sbNewFileText.AppendLine(@"]]></description>");

            sbNewFileText.AppendLine(@"<Point>");
            sbNewFileText.AppendLine(@"<coordinates>");
            sbNewFileText.AppendLine(SourceCoord.Lng.ToString().Replace(",", ".") + @"," + SourceCoord.Lat.ToString().Replace(",", ".") + ",0 ");
            sbNewFileText.AppendLine(@"</coordinates>");
            sbNewFileText.AppendLine(@"</Point>");
            sbNewFileText.AppendLine(@"</Placemark>");
        }
        private void WriteKMLStudyAreaLine(StringBuilder sbNewFileText, List<ElementLayer> elementLayerList, List<Node> nodeList, ReportTag reportTag)
        {
            sbNewFileText.AppendLine(@"  <Style id=""StudyArea"">");
            sbNewFileText.AppendLine(@"  <LineStyle>");
            sbNewFileText.AppendLine(@"  <color>ffffff00</color>");
            sbNewFileText.AppendLine(@"  <width>2</width>");
            sbNewFileText.AppendLine(@"  </LineStyle>");
            sbNewFileText.AppendLine(@"  </Style>");

            float MaxDepth = Math.Abs(nodeList.Min(n => n.Z));

            List<Node> AllNodeList = new List<Node>();
            List<ContourPolygon> ContourPolygonList = new List<ContourPolygon>();

            List<Node> AboveNodeList = (from n in nodeList
                                        where n.Code != 0
                                        select n).ToList<Node>();

            foreach (Node sn in AboveNodeList)
            {
                List<Node> EndNodeList = (from n in sn.ConnectNodeList
                                          where n.Code != 0
                                          select n).ToList<Node>();

                foreach (Node en in EndNodeList)
                {
                    AllNodeList.Add(en);
                }

                if (sn.Code != 0)
                {
                    AllNodeList.Add(sn);
                }

            }

            List<Element> UniqueElementList = new List<Element>();


            // filling UniqueElementList triangle
            UniqueElementList = (from el in elementLayerList.Where(c => c.Layer == 1)
                                 where (el.Element.Type == 21 || el.Element.Type == 32)
                                 && (el.Element.NodeList[0].Code != 0 && el.Element.NodeList[1].Code != 0)
                                 || (el.Element.NodeList[0].Code != 0 && el.Element.NodeList[2].Code != 0)
                                 || (el.Element.NodeList[1].Code != 0 && el.Element.NodeList[2].Code != 0)
                                 select el.Element).Distinct().ToList<Element>();

            UniqueElementList.Concat((from el in elementLayerList.Where(c => c.Layer == 1)
                                      where (el.Element.Type == 25 || el.Element.Type == 33)
                                      && (el.Element.NodeList[0].Code != 0 && el.Element.NodeList[1].Code != 0)
                                      || (el.Element.NodeList[0].Code != 0 && el.Element.NodeList[2].Code != 0)
                                      || (el.Element.NodeList[1].Code != 0 && el.Element.NodeList[2].Code != 0)
                                      select el.Element).Distinct().ToList<Element>());

            List<Node> UniqueNodeList = (from n in AllNodeList orderby n.ID select n).Distinct().ToList<Node>();


            ForwardVector = new Dictionary<String, Vector>();
            BackwardVector = new Dictionary<String, Vector>();

            //UpdateTask(AppTaskID, "30 %");

            foreach (Element el in UniqueElementList)
            {
                if (el.Type == 21 || el.Type == 32)
                {
                    List<Node> OrderedNodes = (from nv in el.NodeList
                                               select nv).ToList<Node>();
                    Node Node0 = OrderedNodes[0];
                    Node Node1 = OrderedNodes[1];
                    Node Node2 = OrderedNodes[2];

                    int ElemCount01 = (from el1 in UniqueElementList
                                       from el2 in Node0.ElementList
                                       from el3 in Node1.ElementList
                                       where el1 == el2 && el1 == el3
                                       select el1).Count();

                    int ElemCount02 = (from el1 in UniqueElementList
                                       from el2 in Node0.ElementList
                                       from el3 in Node2.ElementList
                                       where el1 == el2 && el1 == el3
                                       select el1).Count();

                    int ElemCount12 = (from el1 in UniqueElementList
                                       from el2 in Node1.ElementList
                                       from el3 in Node2.ElementList
                                       where el1 == el2 && el1 == el3
                                       select el1).Count();


                    if (Node0.Code != 0 && Node1.Code != 0)
                    {
                        if (ElemCount01 == 1)
                        {
                            ForwardVector.Add(Node0.ID.ToString() + "," + Node1.ID.ToString(), new Vector() { StartNode = Node0, EndNode = Node1 });
                            BackwardVector.Add(Node1.ID.ToString() + "," + Node0.ID.ToString(), new Vector() { StartNode = Node1, EndNode = Node0 });
                        }
                    }
                    if (Node0.Code != 0 && Node2.Code != 0)
                    {
                        if (ElemCount02 == 1)
                        {
                            ForwardVector.Add(Node0.ID.ToString() + "," + Node2.ID.ToString(), new Vector() { StartNode = Node0, EndNode = Node2 });
                            BackwardVector.Add(Node2.ID.ToString() + "," + Node0.ID.ToString(), new Vector() { StartNode = Node2, EndNode = Node0 });
                        }
                    }
                    if (Node1.Code != 0 && Node2.Code != 0)
                    {
                        if (ElemCount12 == 1)
                        {
                            ForwardVector.Add(Node1.ID.ToString() + "," + Node2.ID.ToString(), new Vector() { StartNode = Node1, EndNode = Node2 });
                            BackwardVector.Add(Node2.ID.ToString() + "," + Node1.ID.ToString(), new Vector() { StartNode = Node2, EndNode = Node1 });
                        }
                    }
                }
                else if (el.Type == 25 || el.Type == 33)
                {
                    List<Node> OrderedNodes = (from nv in el.NodeList
                                               select nv).ToList<Node>();
                    Node Node0 = OrderedNodes[0];
                    Node Node1 = OrderedNodes[1];
                    Node Node2 = OrderedNodes[2];
                    Node Node3 = OrderedNodes[3];

                    int ElemCount01 = (from el1 in UniqueElementList
                                       from el2 in Node0.ElementList
                                       from el3 in Node1.ElementList
                                       where el1 == el2 && el1 == el3
                                       select el1).Count();

                    int ElemCount03 = (from el1 in UniqueElementList
                                       from el2 in Node0.ElementList
                                       from el3 in Node3.ElementList
                                       where el1 == el2 && el1 == el3
                                       select el1).Count();

                    int ElemCount12 = (from el1 in UniqueElementList
                                       from el2 in Node1.ElementList
                                       from el3 in Node2.ElementList
                                       where el1 == el2 && el1 == el3
                                       select el1).Count();

                    int ElemCount23 = (from el1 in UniqueElementList
                                       from el2 in Node2.ElementList
                                       from el3 in Node3.ElementList
                                       where el1 == el2 && el1 == el3
                                       select el1).Count();


                    if (Node0.Code != 0 && Node1.Code != 0)
                    {
                        if (ElemCount01 == 1)
                        {
                            ForwardVector.Add(Node0.ID.ToString() + "," + Node1.ID.ToString(), new Vector() { StartNode = Node0, EndNode = Node1 });
                            BackwardVector.Add(Node1.ID.ToString() + "," + Node0.ID.ToString(), new Vector() { StartNode = Node1, EndNode = Node0 });
                        }
                    }
                    if (Node0.Code != 0 && Node3.Code != 0)
                    {
                        if (ElemCount03 == 1)
                        {
                            ForwardVector.Add(Node0.ID.ToString() + "," + Node3.ID.ToString(), new Vector() { StartNode = Node0, EndNode = Node3 });
                            BackwardVector.Add(Node3.ID.ToString() + "," + Node0.ID.ToString(), new Vector() { StartNode = Node3, EndNode = Node0 });
                        }
                    }
                    if (Node1.Code != 0 && Node2.Code != 0)
                    {
                        if (ElemCount12 == 1)
                        {
                            ForwardVector.Add(Node1.ID.ToString() + "," + Node2.ID.ToString(), new Vector() { StartNode = Node1, EndNode = Node2 });
                            BackwardVector.Add(Node2.ID.ToString() + "," + Node1.ID.ToString(), new Vector() { StartNode = Node2, EndNode = Node1 });
                        }
                    }
                    if (Node2.Code != 0 && Node3.Code != 0)
                    {
                        if (ElemCount23 == 1)
                        {
                            ForwardVector.Add(Node2.ID.ToString() + "," + Node3.ID.ToString(), new Vector() { StartNode = Node2, EndNode = Node3 });
                            BackwardVector.Add(Node3.ID.ToString() + "," + Node2.ID.ToString(), new Vector() { StartNode = Node3, EndNode = Node2 });
                        }
                    }
                }
            }


            //DrawKMLUniqueNodes(UniqueNodeList, 0, sbStyleStudyAreaLine, sbKMLStudyAreaLine);
            //DrawKMLInterpolatedContourNodes(InterpolatedContourNodeList, sbStyleStudyAreaLine, sbKMLStudyAreaLine);
            //DrawUniqueElements(UniqueElementList, sbStyleStudyAreaLine, sbKMLStudyAreaLine);

            //UpdateTask(AppTaskID, "60 %");

            bool MoreStudyAreaLine = true;
            int PolygonCount = 0;
            MapInfoService mapInfoService = new MapInfoService((reportTag.Language == "fr" ? LanguageEnum.fr : LanguageEnum.en), _User);
            while (MoreStudyAreaLine)
            {
                PolygonCount += 1;

                List<Node> FinalContourNodeList = new List<Node>();
                Vector LastVector = new Vector();
                LastVector = ForwardVector.First().Value;
                FinalContourNodeList.Add(LastVector.StartNode);
                FinalContourNodeList.Add(LastVector.EndNode);
                ForwardVector.Remove(LastVector.StartNode.ID.ToString() + "," + LastVector.EndNode.ID.ToString());
                BackwardVector.Remove(LastVector.EndNode.ID.ToString() + "," + LastVector.StartNode.ID.ToString());
                bool PolygonCompleted = false;
                while (!PolygonCompleted)
                {
                    List<string> KeyStrList = (from k in ForwardVector.Keys
                                               where k.StartsWith(LastVector.EndNode.ID.ToString() + ",")
                                               && !k.EndsWith("," + LastVector.StartNode.ID.ToString())
                                               select k).ToList<string>();

                    if (KeyStrList.Count != 1)
                    {
                        KeyStrList = (from k in BackwardVector.Keys
                                      where k.StartsWith(LastVector.EndNode.ID.ToString() + ",")
                                      && !k.EndsWith("," + LastVector.StartNode.ID.ToString())
                                      select k).ToList<string>();

                        if (KeyStrList.Count != 1)
                        {
                            PolygonCompleted = true;
                            break;
                        }
                        else
                        {
                            LastVector = BackwardVector[KeyStrList[0]];
                            BackwardVector.Remove(LastVector.StartNode.ID.ToString() + "," + LastVector.EndNode.ID.ToString());
                            ForwardVector.Remove(LastVector.EndNode.ID.ToString() + "," + LastVector.StartNode.ID.ToString());
                        }
                    }
                    else
                    {
                        LastVector = ForwardVector[KeyStrList[0]];
                        ForwardVector.Remove(LastVector.StartNode.ID.ToString() + "," + LastVector.EndNode.ID.ToString());
                        BackwardVector.Remove(LastVector.EndNode.ID.ToString() + "," + LastVector.StartNode.ID.ToString());
                    }
                    FinalContourNodeList.Add(LastVector.EndNode);
                    if (FinalContourNodeList[FinalContourNodeList.Count - 1] == FinalContourNodeList[0])
                    {
                        PolygonCompleted = true;
                    }
                }

                double Area = mapInfoService.CalculateAreaOfPolygon(FinalContourNodeList);
                if (Area < 0)
                {
                    FinalContourNodeList.Reverse();
                    Area = mapInfoService.CalculateAreaOfPolygon(FinalContourNodeList);
                }

                FinalContourNodeList.Add(FinalContourNodeList[0]);

                ContourPolygon contourLine = new ContourPolygon() { };
                contourLine.ContourNodeList = FinalContourNodeList;
                contourLine.ContourValue = 0;
                ContourPolygonList.Add(contourLine);

                if (ForwardVector.Count == 0)
                {
                    MoreStudyAreaLine = false;
                }

            }

            sbNewFileText.AppendLine(@"  <Folder>");
            sbNewFileText.AppendLine(@"    <name>" + ReportServiceRes.MIKEStudyArea + @"</name>");
            sbNewFileText.AppendLine(@"    <visibility>0</visibility>");
            foreach (ContourPolygon contourPolygon in ContourPolygonList)
            {
                sbNewFileText.AppendLine(@"    <Folder>");
                sbNewFileText.AppendLine(@"      <visibility>0</visibility>");
                sbNewFileText.AppendLine(@"      <Placemark>");
                sbNewFileText.AppendLine(@"        <visibility>0</visibility>");
                sbNewFileText.AppendLine(@"        <styleUrl>#StudyArea</styleUrl>");
                sbNewFileText.AppendLine(@"        <LineString>");
                sbNewFileText.AppendLine(@"        <coordinates>");
                foreach (Node node in contourPolygon.ContourNodeList)
                {
                    sbNewFileText.Append("        " + node.X.ToString().Replace(",", ".") + @"," + node.Y.ToString().Replace(",", ".") + ",0 ");
                }
                sbNewFileText.AppendLine(@"        </coordinates>");
                sbNewFileText.AppendLine(@"        </LineString>");
                sbNewFileText.AppendLine(@"      </Placemark>");
                sbNewFileText.AppendLine(@"    </Folder>");
            }
            sbNewFileText.AppendLine(@"  </Folder>");
        }
        private void WriteKMLStyleModelInput(StringBuilder sbNewFileText)
        {
            sbNewFileText.AppendLine(@"<Style id=""sn_grn-pushpin"">");
            sbNewFileText.AppendLine(@"<IconStyle>");
            sbNewFileText.AppendLine(@"<scale>1.1</scale>");
            sbNewFileText.AppendLine(@"<Icon>");
            sbNewFileText.AppendLine(@"<href>http://maps.google.com/mapfiles/kml/pushpin/grn-pushpin.png</href>");
            sbNewFileText.AppendLine(@"</Icon>");
            sbNewFileText.AppendLine(@"<hotSpot x=""20"" y=""2"" xunits=""pixels"" yunits=""pixels""/>");
            sbNewFileText.AppendLine(@"</IconStyle>");
            sbNewFileText.AppendLine(@"<ListStyle>");
            sbNewFileText.AppendLine(@"</ListStyle>");
            sbNewFileText.AppendLine(@"</Style>");

            sbNewFileText.AppendLine(@"<Style id=""sh_grn-pushpin"">");
            sbNewFileText.AppendLine(@"<IconStyle>");
            sbNewFileText.AppendLine(@"<scale>1.3</scale>");
            sbNewFileText.AppendLine(@"<Icon>");
            sbNewFileText.AppendLine(@"<href>http://maps.google.com/mapfiles/kml/pushpin/grn-pushpin.png</href>");
            sbNewFileText.AppendLine(@"</Icon>");
            sbNewFileText.AppendLine(@"<hotSpot x=""20"" y=""2"" xunits=""pixels"" yunits=""pixels""/>");
            sbNewFileText.AppendLine(@"</IconStyle>");
            sbNewFileText.AppendLine(@"<ListStyle>");
            sbNewFileText.AppendLine(@"</ListStyle>");
            sbNewFileText.AppendLine(@"</Style>");

            sbNewFileText.AppendLine(@"<StyleMap id=""msn_grn-pushpin"">");
            sbNewFileText.AppendLine(@"<Pair>");
            sbNewFileText.AppendLine(@"<key>normal</key>");
            sbNewFileText.AppendLine(@"<styleUrl>#sn_grn-pushpin</styleUrl>");
            sbNewFileText.AppendLine(@"</Pair>");
            sbNewFileText.AppendLine(@"<Pair>");
            sbNewFileText.AppendLine(@"<key>highlight</key>");
            sbNewFileText.AppendLine(@"<styleUrl>#sh_grn-pushpin</styleUrl>");
            sbNewFileText.AppendLine(@"</Pair>");
            sbNewFileText.AppendLine(@"</StyleMap>");

            sbNewFileText.AppendLine(@"<Style id=""sn_red-pushpin"">");
            sbNewFileText.AppendLine(@"<IconStyle>");
            sbNewFileText.AppendLine(@"<scale>1.1</scale>");
            sbNewFileText.AppendLine(@"<Icon>");
            sbNewFileText.AppendLine(@"<href>http://maps.google.com/mapfiles/kml/pushpin/red-pushpin.png</href>");
            sbNewFileText.AppendLine(@"</Icon>");
            sbNewFileText.AppendLine(@"<hotSpot x=""20"" y=""2"" xunits=""pixels"" yunits=""pixels""/>");
            sbNewFileText.AppendLine(@"</IconStyle>");
            sbNewFileText.AppendLine(@"<ListStyle>");
            sbNewFileText.AppendLine(@"</ListStyle>");
            sbNewFileText.AppendLine(@"</Style>");

            sbNewFileText.AppendLine(@"<Style id=""sh_red-pushpin"">");
            sbNewFileText.AppendLine(@"<IconStyle>");
            sbNewFileText.AppendLine(@"<scale>1.3</scale>");
            sbNewFileText.AppendLine(@"<Icon>");
            sbNewFileText.AppendLine(@"<href>http://maps.google.com/mapfiles/kml/pushpin/red-pushpin.png</href>");
            sbNewFileText.AppendLine(@"</Icon>");
            sbNewFileText.AppendLine(@"<hotSpot x=""20"" y=""2"" xunits=""pixels"" yunits=""pixels""/>");
            sbNewFileText.AppendLine(@"</IconStyle>");
            sbNewFileText.AppendLine(@"<ListStyle>");
            sbNewFileText.AppendLine(@"</ListStyle>");
            sbNewFileText.AppendLine(@"</Style>");

            sbNewFileText.AppendLine(@"<StyleMap id=""msn_red-pushpin"">");
            sbNewFileText.AppendLine(@"<Pair>");
            sbNewFileText.AppendLine(@"<key>normal</key>");
            sbNewFileText.AppendLine(@"<styleUrl>#sn_red-pushpin</styleUrl>");
            sbNewFileText.AppendLine(@"</Pair>");
            sbNewFileText.AppendLine(@"<Pair>");
            sbNewFileText.AppendLine(@"<key>highlight</key>");
            sbNewFileText.AppendLine(@"<styleUrl>#sh_red-pushpin</styleUrl>");
            sbNewFileText.AppendLine(@"</Pair>");
            sbNewFileText.AppendLine(@"</StyleMap>");

            sbNewFileText.AppendLine(@"<Style id=""sn_blue-pushpin"">");
            sbNewFileText.AppendLine(@"<IconStyle>");
            sbNewFileText.AppendLine(@"<scale>1.1</scale>");
            sbNewFileText.AppendLine(@"<Icon>");
            sbNewFileText.AppendLine(@"<href>http://maps.google.com/mapfiles/kml/pushpin/blue-pushpin.png</href>");
            sbNewFileText.AppendLine(@"</Icon>");
            sbNewFileText.AppendLine(@"<hotSpot x=""20"" y=""2"" xunits=""pixels"" yunits=""pixels""/>");
            sbNewFileText.AppendLine(@"</IconStyle>");
            sbNewFileText.AppendLine(@"<ListStyle>");
            sbNewFileText.AppendLine(@"</ListStyle>");
            sbNewFileText.AppendLine(@"</Style>");

            sbNewFileText.AppendLine(@"<Style id=""sh_blue-pushpin"">");
            sbNewFileText.AppendLine(@"<IconStyle>");
            sbNewFileText.AppendLine(@"<scale>1.3</scale>");
            sbNewFileText.AppendLine(@"<Icon>");
            sbNewFileText.AppendLine(@"<href>http://maps.google.com/mapfiles/kml/pushpin/blue-pushpin.png</href>");
            sbNewFileText.AppendLine(@"</Icon>");
            sbNewFileText.AppendLine(@"<hotSpot x=""20"" y=""2"" xunits=""pixels"" yunits=""pixels""/>");
            sbNewFileText.AppendLine(@"</IconStyle>");
            sbNewFileText.AppendLine(@"<ListStyle>");
            sbNewFileText.AppendLine(@"</ListStyle>");
            sbNewFileText.AppendLine(@"</Style>");

            sbNewFileText.AppendLine(@"<StyleMap id=""msn_blue-pushpin"">");
            sbNewFileText.AppendLine(@"<Pair>");
            sbNewFileText.AppendLine(@"<key>normal</key>");
            sbNewFileText.AppendLine(@"<styleUrl>#sn_blue-pushpin</styleUrl>");
            sbNewFileText.AppendLine(@"</Pair>");
            sbNewFileText.AppendLine(@"<Pair>");
            sbNewFileText.AppendLine(@"<key>highlight</key>");
            sbNewFileText.AppendLine(@"<styleUrl>#sh_blue-pushpin</styleUrl>");
            sbNewFileText.AppendLine(@"</Pair>");
            sbNewFileText.AppendLine(@"</StyleMap>");
        }
        private void WriteKMLTop(StringBuilder sbNewFileText)
        {
            sbNewFileText.AppendLine(@"<?xml version=""1.0"" encoding=""UTF-8""?>");
            sbNewFileText.AppendLine(@"<kml xmlns=""http://www.opengis.net/kml/2.2"" xmlns:gx=""http://www.google.com/kml/ext/2.2"" xmlns:kml=""http://www.opengis.net/kml/2.2"" xmlns:atom=""http://www.w3.org/2005/Atom"">");
            sbNewFileText.AppendLine(@"<Document>");
            return;
        }
        #endregion Functions public
    }
}
