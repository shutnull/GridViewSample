using GridViewSample.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WpfColor = System.Windows.Media.Color;

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

            List<MergeableHeaderConfig> headerConfigList = new List<MergeableHeaderConfig>();
            #region ヘッダー作る
            // ヘッダーの設定
            MergeableHeaderConfig headerConfig1, headerConfig2;
            MergeableHeaderConfigItem[] items1, items2;

            // 一行目
            items1 = new MergeableHeaderConfigItem[headerCount]
            {
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:100, rowSize:66),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:100, rowSize:66),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:100, rowSize:66),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:100, rowSize:66),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:100, rowSize:66),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:100, rowSize:66),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:100, rowSize:66),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:100, rowSize:66),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:100, rowSize:66),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:400, rowSize:33),
                new MergeableHeaderConfigItem(text:"", columnSize:0, rowSize:33),
                new MergeableHeaderConfigItem(text:"", columnSize:0, rowSize:33),
                new MergeableHeaderConfigItem(text:"", columnSize:0, rowSize:33),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:100, rowSize:66),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:300, rowSize:33),
                new MergeableHeaderConfigItem(text:"", columnSize:0, rowSize:33),
                new MergeableHeaderConfigItem(text:"", columnSize:0, rowSize:33),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:100, rowSize:66),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:100, rowSize:66),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:100, rowSize:66),
            };
            headerConfig1 = new MergeableHeaderConfig
            {
                Items = items1
            };
            headerConfigList.Add(headerConfig1);

            // 二行目
            items2 = new MergeableHeaderConfigItem[headerCount]
            {
                new MergeableHeaderConfigItem(text:"", columnSize:100, rowSize:0),
                new MergeableHeaderConfigItem(text:"", columnSize:100, rowSize:0),
                new MergeableHeaderConfigItem(text:"", columnSize:100, rowSize:0),
                new MergeableHeaderConfigItem(text:"", columnSize:100, rowSize:0),
                new MergeableHeaderConfigItem(text:"", columnSize:100, rowSize:0),
                new MergeableHeaderConfigItem(text:"", columnSize:100, rowSize:0),
                new MergeableHeaderConfigItem(text:"", columnSize:100, rowSize:0),
                new MergeableHeaderConfigItem(text:"", columnSize:100, rowSize:0),
                new MergeableHeaderConfigItem(text:"", columnSize:100, rowSize:0),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:100, rowSize:33),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:100, rowSize:33),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:100, rowSize:33),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:100, rowSize:33),
                new MergeableHeaderConfigItem(text:"", columnSize:100, rowSize:0),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:100, rowSize:33),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:100, rowSize:33),
                new MergeableHeaderConfigItem(text:"ヘッダー", columnSize:100, rowSize:33),
                new MergeableHeaderConfigItem(text:"", columnSize:100, rowSize:0),
                new MergeableHeaderConfigItem(text:"", columnSize:100, rowSize:0),
                new MergeableHeaderConfigItem(text:"", columnSize:100, rowSize:0),
            };
            headerConfig2 = new MergeableHeaderConfig
            {
                Items = items2
            };
            headerConfigList.Add(headerConfig2);
            #endregion

            // データグリッドの設定
            double[] colSize = new double[headerCount]
            {
                100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100,100
            };
            MergebleGridBoxConfig gridBoxConfig = new MergebleGridBoxConfig(33, colSize, false, true);


            margableDataGridView.HeaderConfigList = headerConfigList;
            margableDataGridView.GridBoxConfig = gridBoxConfig;
            margableDataGridView.DataSource = dataTable;
            margableDataGridView.Update();
        }

    }
}
