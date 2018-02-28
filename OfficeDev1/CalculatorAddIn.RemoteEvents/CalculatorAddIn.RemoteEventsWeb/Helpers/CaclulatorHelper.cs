using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CalculatorAddIn.RemoteEventsWeb.Helpers
{
    public class CaclulatorHelper
    {
        public static bool ValidateCalculation(int itemId, Dictionary<string, object> props, ClientContext ctx)
        {
            List list = ctx.Web.GetListByTitle("Calculator");
            ListItem item = list.GetItemById(itemId);
            ctx.Load(item);
            ctx.ExecuteQuery();

            string calcOperator = "";

            if (props.ContainsKey("calcOperator"))
            {
                calcOperator = props["calcOperator"].ToString();
            }
            else
            {
                calcOperator = item["calcOperator"].ToString();
            }


            int num2 = -1;
            if (props.ContainsKey("calcnum2"))
            {
                num2 = int.Parse(props["calcnum2"].ToString());
            }
            else
            {
                num2 = int.Parse(item["calcnum2"].ToString());
            }

            if (calcOperator == "devided" && num2 == 0)
            {
                return false;
            }
            return true;

        }

            public static void DoCalcuation(int itemId, ClientContext ctx)
        {
         
            List list = ctx.Web.GetListByTitle("Calculator");
            ListItem item = list.GetItemById(itemId);
            ctx.Load(item);
            ctx.ExecuteQuery();

            string calcOperator = item["calcOperator"].ToString();
            int num1 = int.Parse(item["calcnum1"].ToString());
            int num2 = int.Parse(item["calcnum2"].ToString());
            int? oldresult = null; 
           if (item["calcResult"] != null)
            {
                oldresult = int.Parse(item["calcResult"].ToString());
            }


            int newResult = 1;
            switch (calcOperator)
            {
                case "plus":
                    newResult = num1 + num2;
                    break;
                case "minus":
                    newResult = num1 - num2;
                    break;
                case "devided":
                    double dresult = (num1 / num2);
                    newResult = (int)(Math.Round(dresult));
                    break;
                case "multipy":
                    newResult = num1 * num2;
                    break;
                    
            }

            if (!oldresult.HasValue || oldresult.Value != newResult)
            {
                item["calcResult"] = newResult;
                item.Update();
                ctx.ExecuteQuery();
            }



           


        }
        

    }
}