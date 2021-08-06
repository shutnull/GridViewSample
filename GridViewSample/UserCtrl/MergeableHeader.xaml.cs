using GridViewSample.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GridViewSample.UserCtrl
{
    /// <summary>
    /// MargableHeader.xaml の相互作用ロジック
    /// </summary>
    public partial class MargableHeader : UserControl
    {
        [Browsable(true)]
        [Description("ヘッダー背景色")]
        public Color HeaderBackColor { get; set; } = Color.FromArgb(0xFF, 0x01, 0x25, 0x97);
        [Browsable(true)]
        [Description("ヘッダー前景色")]
        public Color HeadeForeColor { get; set; } = Colors.White;

        [Browsable(true)]
        [Description("スクロールしない固定行数")]
        public int FrozenCount { get; set; } = 0;

        [Browsable(true)]
        [Description("ヘッダーの設定情報リスト")]
        public List<MergeableHeaderConfig> HeaderConfigList { get; set; } = new List<MergeableHeaderConfig>(0);


        public MargableHeader()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 表示更新
        /// </summary>
        public void Update()
        {
            if (HeaderConfigList.Count <= 0)
            {
                return;
            }

            // スクロール領域のサイズを取得する
            double wSize = HeaderConfigList.Select(list => list.Items.Take(FrozenCount).Sum(item => item?.ColumnSize ?? 0)).Max();
            FrozenSize.Width = new GridLength(wSize, GridUnitType.Pixel);
            // スクロール領域のサイズを取得する
            CanvasNoFrozen.Width = HeaderConfigList.Select(list => list.Items.Skip(FrozenCount).Sum(item => item?.ColumnSize ?? 0)).Max();

            double[] LastTopSize = new double[HeaderConfigList[0].Items.Length];
            foreach (MergeableHeaderConfig headerConfig in HeaderConfigList)
            {
                int count = 0;
                double leftSizeCnt = 0;
                foreach (MergeableHeaderConfigItem items in headerConfig.Items)
                {
                    if(items == null)
                    {
                        count++;
                        continue;
                    }

                    if (count < FrozenCount)
                    {
                        Label label = InitializeLabel(items.RowSize, items.ColumnSize, LastTopSize[count], leftSizeCnt, items.Text);
                        CanvasFrozen.Children.Add(label);
                     }
                    else
                    {
                        Label label = InitializeLabel(items.RowSize, items.ColumnSize, LastTopSize[count], leftSizeCnt - wSize, items.Text);
                        CanvasNoFrozen.Children.Add(label);
                    }
                    LastTopSize[count] += items.RowSize;
                    leftSizeCnt += items.ColumnSize;
                    count++;
                }
            }

            Height = LastTopSize.Max();
        }

        private Label InitializeLabel(double height, double width, double canvasTop, double canvasLeft, string text)
        {
            Label label = new Label
            {
                Height = height,
                Width = width,
                Content = text,
                BorderThickness = new Thickness(1),
                BorderBrush = new SolidColorBrush(Color.FromRgb(0x80, 0x80, 0x80)),
                Background = new SolidColorBrush(HeaderBackColor),
                Foreground = new SolidColorBrush(HeadeForeColor),
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center
            };
            Canvas.SetTop(label, canvasTop);
            Canvas.SetLeft(label, canvasLeft);

            return label;
        }
    }
}
