using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laundry.BLL
{
    public class HashHelper
    {
        string strMyDay, strMyMth, strMyYear, strMyDte, FinalDate;
        public string ValidateDate(string MyDate)
        {
            string message = "";
            string[] myarrData = MyDate.Split('/');
            if ((myarrData.Length != 3))
            {
                message = "Incomplete date of birth";
                return message;
            }
            strMyDay = myarrData[0];
            strMyMth = myarrData[1];
            strMyYear = myarrData[2].Substring(0, 4);
            strMyDay = double.Parse(strMyDay).ToString("00");
            strMyMth = double.Parse(strMyMth).ToString("00");
            strMyYear = double.Parse(strMyYear).ToString("0000");
            strMyDte = (strMyDay.Trim() + ("/"
                        + (strMyMth.Trim() + ("/" + strMyYear.Trim()))));

            if ((!gnTest_TransDate(strMyDte)))
            {
                message = "Invalid date - " + strMyDte.ToString();
                return message;
            }


            try
            {

                FinalDate = RearrangetoDateFormat(strMyDte);
                //if (FinalDate.Substring(0, 5) != "ERROR")
                if (FinalDate.Substring(0, 5) == "ERROR")
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                message = "Invalid date - " + strMyDte.ToString();
                return message;
            }
            return FinalDate;
        }

        public static bool gnTest_TransDate(string MyFunc_Date)
        {
            bool pvbln;
            pvbln = false;

            if (((MyFunc_Date.Length == 10) && (((MyFunc_Date.Substring(2, 1) == "-") || (MyFunc_Date.Substring(2, 1) == "/"))
                && ((MyFunc_Date.Substring(5, 1) == "-")
            || (MyFunc_Date.Substring(5, 1) == "/")))))
            {

            }
            else
            {
                return pvbln;
            }
            string strDteMsg = "Invalid Date";
            string strDteErr = "0";
            //DateTime DteTst;
            string strDte_Start;
            string strDte_End;
            string strDteYY;
            string strDteMM;
            string strDteDD;
            strDteMsg = "";
            strDteErr = "0";
            strDteMsg = "";
            strDteErr = "0";

            strDteDD = MyFunc_Date.Substring(0, 2);
            strDteMM = MyFunc_Date.Substring(3, 2);
            strDteYY = MyFunc_Date.Substring((MyFunc_Date.Length - 4));
            strDteDD = strDteDD.Trim();
            strDteMM = strDteMM.Trim();
            strDteYY = strDteYY.Trim();


            if (((Convert.ToInt16(strDteDD) < 01) || (Convert.ToInt16(strDteDD.Trim()) > 31)))
            {
                strDteMsg = ("  -> Day < 01 or Day > 31 ..." + "\r\n");
                strDteErr = "1";
            }
            if (((Convert.ToInt16(strDteMM.Trim()) < 01)
                        || (Convert.ToInt16(strDteMM.Trim()) > 12)))
            {
                strDteMsg = (strDteMsg + ("  -> Month < 01 or Month > 12 ..." + "\r\n"));
                strDteErr = "1";
            }
            if ((strDteYY.Trim().Length < 4))
            {
                strDteMsg = (strDteMsg + ("  -> Year = 0 digit or Year < 4 digits..." + "\r\n"));
                strDteErr = "1";
            }
            strDte_Start = "";
            strDte_End = "";
            strDte_Start = MyFunc_Date;
            strDte_End = MyFunc_Date;

            switch (strDteMM.Trim())
            {
                case "01":
                case "03":
                case "05":
                case "07":
                case "08":
                case "10":
                case "12":
                    if ((double.Parse(strDteDD) > 31))
                    {
                        strDteMsg = (strDteMsg + ("  -> Invalid day in month. Month <"
                                    + (strDteMM + (">" + (" ends in <" + (" 31 " + (">" + (". Full Date: "
                                    + (strDte_End + "\r\n")))))))));
                        strDteErr = "1";
                    }
                    break;
                case "02":
                    if (double.Parse(strDteYY) % 4 == 0)
                    {
                        if ((double.Parse(strDteDD) > 29))
                        {
                            strDteMsg = (strDteMsg + ("  -> Invalid day in month. Month <"
                                        + (strDteMM + (">" + (" ends in <" + (" 29 " + (">" + (". Full Date: "
                                        + (strDte_End + "\r\n")))))))));
                            strDteErr = "1";
                        }
                    }
                    else if ((double.Parse(strDteDD) > 28))
                    {
                        strDteMsg = (strDteMsg + ("  -> Invalid day in month. Month <"
                                    + (strDteMM + (">" + (" ends in <" + (" 28 " + (">" + (". Full Date: "
                                    + (strDte_End + "\r\n")))))))));
                        strDteErr = "1";
                    }
                    break;
                case "04":
                case "06":
                case "09":
                case "11":
                    if ((double.Parse(strDteDD) > 30))
                    {
                        strDteMsg = (strDteMsg + ("  -> Invalid day in month. Month <"
                                    + (strDteMM + (">" + (" ends in <" + (" 30 " + (">" + (". Full Date: "
                                    + (strDte_End + "\r\n")))))))));
                        strDteErr = "1";
                    }
                    break;
            }

            if ((strDteErr != "0"))
            {
                //gnTest_TransDate = false;
                pvbln = false;
            }
            //gnTest_TransDate = true;
            pvbln = true;
            return pvbln;
        }

        public static string removeDateSeperators(String dte)
        {
            //split to constituents
            string[] dparts = dte.Split('/');
            if (dparts.Length != 3)
                return "ERROR: Invalid Date Format!";

            string dy = dparts[0];
            string myday = dparts[0];
            string mt = dparts[1];
            string mth = dparts[1];
            if (mt.Length == 1)
                mth = "0" + mt;

            if (dy.Length == 1)
                myday = "0" + dy;

            string ky = dparts[2] + mth + myday;
            return ky;
        }

        public static string RearrangetoDateFormat(String dte)
        {
            //split to constituents
            string[] dparts = dte.Split('/');
            if (dparts.Length != 3)
                return "ERROR: Invalid Date Format!";

            string dy = dparts[0];
            string myday = dparts[0];
            string mt = dparts[1];
            string mth = dparts[1];
            if (mt.Length == 1)
                mth = "0" + mt;

            if (dy.Length == 1)
                myday = "0" + dy;

            string ky = dparts[2] + "/"+ mth + "/" + myday;
            return ky;
        }
    }
}
