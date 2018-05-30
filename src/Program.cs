﻿using System;
using System.IO;
using System.Text;

namespace 金融数据整理大赛
{
    class Program
    {

        public static StreamWriter Logger = new StreamWriter("Log.log");

        public static String DocBase = @"E:\金融数据整理大赛";

        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Traning.InitContract();
            Traning.InitIncreaseStock();
            Traning.InitStockChange();

            //分词系统
            //WordAnlayze.Init();
            //UT.RunWordAnlayze();
            //UT.IncreaseStockTest();
            //Logger.Close();
            //return;

            var IsRunContract = true;
            var IsRunContract_TEST = true;

            var IsRunStockChange = false;
            var IsRunStockChange_TEST = false;

            var IsRunIncreaseStock = false;
            var IsRunIncreaseStock_TEST = false;

            if (IsRunContract)
            {
                //合同处理
                var ContractPath_TRAIN = DocBase + @"\FDDC_announcements_round1_train_20180518\round1_train_20180518\重大合同";
                Console.WriteLine("Start To Extract Info Contract TRAIN");
                StreamWriter ResultCSV = new StreamWriter("hetong_train.csv", false, Encoding.GetEncoding("gb2312"));
                foreach (var filename in System.IO.Directory.GetFiles(ContractPath_TRAIN + @"\html\"))
                {
                    foreach (var item in Contract.Extract(filename))
                    {
                        ResultCSV.WriteLine(Contract.ConvertToString(item));
                    }
                }
                ResultCSV.Close();
                Console.WriteLine("标准主键数：" + Traning.ContractList.Count);
                Console.WriteLine("正确主键数：" + Contract.CorrectKey);
                Console.WriteLine("Complete Extract Info Contract");
            }
            if (IsRunContract_TEST)
            {
                var ContractPath_TEST = DocBase + @"\FDDC_announcements_round1_test_a_20180524\重大合同";
                StreamWriter ResultCSV = new StreamWriter("hetong.csv", false, Encoding.GetEncoding("gb2312"));
                Console.WriteLine("Start To Extract Info Contract TEST");
                foreach (var filename in System.IO.Directory.GetFiles(ContractPath_TEST + @"\html\"))
                {
                    foreach (var item in Contract.Extract(filename))
                    {
                        ResultCSV.WriteLine(Contract.ConvertToString(item));
                    }
                }
                ResultCSV.Close();
                Console.WriteLine("Complete Extract Info Contract");
            }


            if (IsRunStockChange)
            {
                //增减持
                Console.WriteLine("Start To Extract Info StockChange TRAIN");
                var StockChangePath_TRAIN = DocBase + @"\FDDC_announcements_round1_train_20180518\round1_train_20180518\增减持";
                StockChange.HolderFullNameCnt = 0;
                StockChange.HolderNameCnt = 0;
                StockChange.ChangeEndDateCnt = 0;
                foreach (var filename in System.IO.Directory.GetFiles(StockChangePath_TRAIN + @"\html\"))
                {
                    StockChange.Extract(filename);
                }
                Console.WriteLine("股东全称计数:" + StockChange.HolderFullNameCnt);
                Console.WriteLine("股东简称计数:" + StockChange.HolderNameCnt);
                Console.WriteLine("变动截止日期计数:" + StockChange.ChangeEndDateCnt);
                Console.WriteLine("Complete Extract Info StockChange");
            }
            if (IsRunStockChange_TEST)
            {
                StreamWriter ResultCSV = new StreamWriter("zengjianchi.csv", false, Encoding.GetEncoding("gb2312"));
                var StockChangePath_TEST = DocBase + @"\FDDC_announcements_round1_test_a_20180524\增减持";
                Console.WriteLine("Start To Extract Info StockChange TEST");
                StockChange.HolderFullNameCnt = 0;
                StockChange.HolderNameCnt = 0;
                StockChange.ChangeEndDateCnt = 0;
                foreach (var filename in System.IO.Directory.GetFiles(StockChangePath_TEST + @"\html\"))
                {
                    StockChange.Extract(filename);
                }
                ResultCSV.Close();
                Console.WriteLine("股东全称计数:" + StockChange.HolderFullNameCnt);
                Console.WriteLine("股东简称计数:" + StockChange.HolderNameCnt);
                Console.WriteLine("变动截止日期计数:" + StockChange.ChangeEndDateCnt);
                Console.WriteLine("Complete Extract Info StockChange");
            }



            if (IsRunIncreaseStock)
            {
                //定增
                StreamWriter ResultCSV = new StreamWriter("dingzeng_train.csv", false, Encoding.GetEncoding("gb2312"));
                var IncreaseStockPath_TRAIN = DocBase + @"\FDDC_announcements_round1_train_20180518\round1_train_20180518\定增";
                Console.WriteLine("Start To Extract Info IncreaseStock TRAIN");

                IncreaseStock.findPublishMethodcount = 0;
                IncreaseStock.findBuyMethodcount = 0;
                foreach (var filename in System.IO.Directory.GetFiles(IncreaseStockPath_TRAIN + @"\html\"))
                {
                    foreach (var item in IncreaseStock.Extract(filename))
                    {
                        ResultCSV.WriteLine(IncreaseStock.ConvertToString(item));
                    }
                }
                ResultCSV.Close();
                Console.WriteLine("抽取定增 发行方式（包含疑似）:" + IncreaseStock.findPublishMethodcount);
                Console.WriteLine("抽取定增 认购方式（包含疑似）:" + IncreaseStock.findBuyMethodcount);
                Console.WriteLine("Complete Extract Info IncreaseStock");
            }

            if (IsRunIncreaseStock_TEST)
            {
                StreamWriter ResultCSV = new StreamWriter("dingzeng.csv", false, Encoding.GetEncoding("gb2312"));
                var IncreaseStockPath_TEST = DocBase + @"\FDDC_announcements_round1_test_a_20180524\定增";
                Console.WriteLine("Start To Extract Info IncreaseStock TRAIN");
                IncreaseStock.findPublishMethodcount = 0;
                IncreaseStock.findBuyMethodcount = 0;
                foreach (var filename in System.IO.Directory.GetFiles(IncreaseStockPath_TEST + @"\html\"))
                {
                    foreach (var item in IncreaseStock.Extract(filename))
                    {
                        ResultCSV.WriteLine(IncreaseStock.ConvertToString(item));
                    }
                }
                ResultCSV.Close();
                Console.WriteLine("抽取定增 发行方式（包含疑似）:" + IncreaseStock.findPublishMethodcount);
                Console.WriteLine("抽取定增 认购方式（包含疑似）:" + IncreaseStock.findBuyMethodcount);
                Console.WriteLine("Complete Extract Info IncreaseStock");
            }


            Logger.Close();
        }
    }
}
