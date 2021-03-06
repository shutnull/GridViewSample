using GridViewSample.UserCtrl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace GridViewSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            const int headerCount = 20;
            DataTable dataTable = new DataTable();
            #region DataTable作る
            for (int iLoop = 0; iLoop < headerCount; iLoop++)
            {
                dataTable.Columns.Add("ヘッダー" + iLoop);
            }

            DataRow dataRow = dataTable.NewRow();
            for (int iLoop = 0; iLoop < headerCount; iLoop++)
            {
                dataRow[iLoop] = "データ" + iLoop;
            }
            dataTable.Rows.Add(dataRow);

            dataRow = dataTable.NewRow();
            for (int iLoop = 0; iLoop < headerCount; iLoop++)
            {
                if (iLoop < 5)
                {
                    dataRow[iLoop] = "データ" + iLoop;
                }
                else if (iLoop == 7 || iLoop == 9)
                {
                    dataRow[iLoop] = "データ" + iLoop;
                }
                else
                {
                    dataRow[iLoop] = "データ：" + iLoop;
                }
            }
            dataTable.Rows.Add(dataRow);

            dataRow = dataTable.NewRow();
            for (int iLoop = 0; iLoop < headerCount; iLoop++)
            {
                dataRow[iLoop] = "_データ" + iLoop;
            }
            dataTable.Rows.Add(dataRow);

            dataRow = dataTable.NewRow();
            for (int iLoop = 0; iLoop < headerCount; iLoop++)
            {
                if (iLoop < 5)
                {
                    dataRow[iLoop] = "_データ" + iLoop;
                }
                else if (iLoop == 8 || iLoop == 10)
                {
                    dataRow[iLoop] = "_データ" + iLoop;
                }
                else
                {
                    dataRow[iLoop] = "_データ：" + iLoop;
                }
            }
            dataTable.Rows.Add(dataRow);
            #endregion
            #region test
            dataRow = dataTable.NewRow();
            for (int iLoop = 0; iLoop < headerCount; iLoop++)
            {
                if (iLoop < 5)
                {
                    dataRow[iLoop] = "データ" + iLoop;
                }
                else if (iLoop == 7 || iLoop == 9)
                {
                    dataRow[iLoop] = "データ" + iLoop;
                }
                else
                {
                    dataRow[iLoop] = "データ：" + iLoop;
                }
            }
            dataTable.Rows.Add(dataRow);
            dataRow = dataTable.NewRow();
            for (int iLoop = 0; iLoop < headerCount; iLoop++)
            {
                dataRow[iLoop] = "_データ" + iLoop;
            }
            dataTable.Rows.Add(dataRow);
            dataRow = dataTable.NewRow();
            for (int iLoop = 0; iLoop < headerCount; iLoop++)
            {
                if (iLoop < 5)
                {
                    dataRow[iLoop] = "_データ" + iLoop;
                }
                else if (iLoop == 8 || iLoop == 10)
                {
                    dataRow[iLoop] = "_データ" + iLoop;
                }
                else
                {
                    dataRow[iLoop] = "_データ：" + iLoop;
                }
            }
            dataTable.Rows.Add(dataRow);
            dataRow = dataTable.NewRow();
            for (int iLoop = 0; iLoop < headerCount; iLoop++)
            {
                if (iLoop < 5)
                {
                    dataRow[iLoop] = "データ" + iLoop;
                }
                else if (iLoop == 7 || iLoop == 9)
                {
                    dataRow[iLoop] = "データ" + iLoop;
                }
                else
                {
                    dataRow[iLoop] = "データ：" + iLoop;
                }
            }
            dataTable.Rows.Add(dataRow);
            dataRow = dataTable.NewRow();
            for (int iLoop = 0; iLoop < headerCount; iLoop++)
            {
                dataRow[iLoop] = "_データ" + iLoop;
            }
            dataTable.Rows.Add(dataRow);
            dataRow = dataTable.NewRow();
            for (int iLoop = 0; iLoop < headerCount; iLoop++)
            {
                if (iLoop < 5)
                {
                    dataRow[iLoop] = "_データ" + iLoop;
                }
                else if (iLoop == 8 || iLoop == 10)
                {
                    dataRow[iLoop] = "_データ" + iLoop;
                }
                else
                {
                    dataRow[iLoop] = "_データ：" + iLoop;
                }
            }
            dataTable.Rows.Add(dataRow);
            #endregion

            double[] colSize = new double[headerCount]
            {
                100,100,120,100,100,150,100,50,100,100,100,100,100,60,100,100,100,100,100,100
            };

            List<MergeableHeaderConfig> headerConfigList = new List<MergeableHeaderConfig>();
            #region ヘッダー作る
            // ヘッダーの設定
            MergeableHeaderConfig headerConfig1, headerConfig2;
            MergeableHeaderConfigItem[] items1, items2;

            // 一行目
            items1 = new MergeableHeaderConfigItem[headerCount]
            {
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:colSize[0],     rowSize:70),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:colSize[1],     rowSize:70),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:colSize[2],     rowSize:70),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:colSize[3],     rowSize:70),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:colSize[4],     rowSize:70),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:colSize[5],     rowSize:70),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:colSize[6],     rowSize:70),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:colSize[7],     rowSize:70),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:colSize[8],     rowSize:70),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:400,            rowSize:35),
                new MergeableHeaderConfigItem(text:"",         columnSize:0,              rowSize:35),
                new MergeableHeaderConfigItem(text:"",         columnSize:0,              rowSize:35),
                new MergeableHeaderConfigItem(text:"",         columnSize:0,              rowSize:35),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:colSize[13],    rowSize:70),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:300,            rowSize:35),
                new MergeableHeaderConfigItem(text:"",         columnSize:0,              rowSize:35),
                new MergeableHeaderConfigItem(text:"",         columnSize:0,              rowSize:35),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:colSize[17],    rowSize:70),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:colSize[18],    rowSize:70),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:colSize[19],    rowSize:70), 
            };
            headerConfig1 = new MergeableHeaderConfig
            {
                Items = items1
            };
            headerConfigList.Add(headerConfig1);

            // 二行目
            items2 = new MergeableHeaderConfigItem[headerCount]
            {
                new MergeableHeaderConfigItem(text:"",               columnSize:colSize[0],     rowSize:0),
                new MergeableHeaderConfigItem(text:"",               columnSize:colSize[1],     rowSize:0),
                new MergeableHeaderConfigItem(text:"",               columnSize:colSize[2],     rowSize:0),
                new MergeableHeaderConfigItem(text:"",               columnSize:colSize[3],     rowSize:0),
                new MergeableHeaderConfigItem(text:"",               columnSize:colSize[4],     rowSize:0),
                new MergeableHeaderConfigItem(text:"",               columnSize:colSize[5],     rowSize:0),
                new MergeableHeaderConfigItem(text:"",               columnSize:colSize[6],     rowSize:0),
                new MergeableHeaderConfigItem(text:"",               columnSize:colSize[7],     rowSize:0),
                new MergeableHeaderConfigItem(text:"",               columnSize:colSize[8],     rowSize:0),
                new MergeableHeaderConfigItem(text:"ヘッダー\ntest", columnSize:colSize[9],     rowSize:35),
                new MergeableHeaderConfigItem(text:"ヘッダー",       columnSize:colSize[10],    rowSize:35),
                new MergeableHeaderConfigItem(text:"ヘッダー",       columnSize:colSize[11],    rowSize:35),
                new MergeableHeaderConfigItem(text:"ヘッダー",       columnSize:colSize[12],    rowSize:35),
                new MergeableHeaderConfigItem(text:"",               columnSize:colSize[13],    rowSize:0),
                new MergeableHeaderConfigItem(text:"ヘッダー",       columnSize:colSize[14],    rowSize:35),
                new MergeableHeaderConfigItem(text:"ヘッダー",       columnSize:colSize[15],    rowSize:35),
                new MergeableHeaderConfigItem(text:"ヘッダー",       columnSize:colSize[16],    rowSize:35),
                new MergeableHeaderConfigItem(text:"",               columnSize:colSize[17],    rowSize:0),
                new MergeableHeaderConfigItem(text:"",               columnSize:colSize[18],    rowSize:0),
                new MergeableHeaderConfigItem(text:"",               columnSize:colSize[19],    rowSize:0),
            };
            headerConfig2 = new MergeableHeaderConfig
            {
                Items = items2
            };
            headerConfigList.Add(headerConfig2);
            #endregion

            // データグリッドの設定
            MergebleGridBoxConfig gridBoxConfig = new MergebleGridBoxConfig(33, colSize, true, 7, new int[] { 7 });

            // ヘッダー設定
            margableDataGridView.HeaderConfigList = headerConfigList;
            // グリッドボックス設定
            margableDataGridView.GridBoxConfig = gridBoxConfig;
            // データソース設定
            margableDataGridView.DataSource = dataTable;
            // 固定列数設定
            margableDataGridView.FrozenCount = 2;
            // 表示更新
            margableDataGridView.Update();
        }

    }
}
