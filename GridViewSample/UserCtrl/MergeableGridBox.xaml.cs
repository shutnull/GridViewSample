using GridViewSample.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    /// MargableGridBox.xaml の相互作用ロジック
    /// </summary>
    public partial class MargableGridBox : UserControl
    {
        [Browsable(true)]
        [Description("グリッドボックス背景色")]
        public Color BackColor { get; set; } = Colors.White;
        [Browsable(true)]
        [Description("グリッドボックス前景色")]
        public Color ForeColor { get; set; } = Colors.Black;

        [Browsable(true)]
        [Description("スクロールしない固定行数")]
        public int FrozenCount { get; set; } = 0;

        [Browsable(true)]
        [Description("グリッドボックス設定情報")]
        public MergebleGridBoxConfig GridBoxConfig { get; set; } = new MergebleGridBoxConfig();

        [Browsable(true)]
        [Description("データソース")]
        public DataTable DataSource { get; set; } = new DataTable();


        public MargableGridBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 表示更新
        /// </summary>
        public void Update()
        {
            // 列サイズの設定がされていない場合処理しない
            if (GridBoxConfig.ColumnSize.Length <= 0)
            {
                return;
            }

            // スクロールしない固定領域のサイズを取得・設定する
            double wSize = GridBoxConfig.ColumnSize.Take(FrozenCount).Sum();
            FrozenSize.Width = new GridLength(wSize, GridUnitType.Pixel);
            // スクロール領域のサイズを取得・設定する
            CanvasNoFrozen.Width = GridBoxConfig.ColumnSize.Skip(FrozenCount).Sum();

            // データ行情報を列挙
            DataRow[] dataRowArray = DataSource.Rows.Cast<DataRow>().ToArray();

            for (int row = 0; row < dataRowArray.Length; row++)
            {
                if((row + 1) < dataRowArray.Length)
                {
                    object[] dataRow = dataRowArray[row].ItemArray;
                    for (int col = 0; col < dataRow.Length; col++)
                    {
                    }
                }
            }
        }

        /// <summary>
        /// ラベルを取得する
        /// </summary>
        /// <param name="height">ラベルの高さ</param>
        /// <param name="width">ラベルの幅</param>
        /// <param name="canvasTop">キャンバス上の位置(Y軸方向左上)</param>
        /// <param name="canvasLeft">キャンバス上の位置(X軸方向左上)</param>
        /// <param name="text">ラベルに表示する文字列</param>
        /// <returns></returns>
        private Label InitializeLabel(double height, double width, double canvasTop, double canvasLeft, string text)
        {
            Label label = new Label
            {
                // 高さ
                Height = height,
                // 幅
                Width = width,
                // 文字列
                Content = text,
                // ボーダーの設定
                BorderThickness = new Thickness(1),
                BorderBrush = new SolidColorBrush(Color.FromRgb(0x80, 0x80, 0x80)),
                // 背景色
                Background = new SolidColorBrush(BackColor),
                // 前景色
                Foreground = new SolidColorBrush(ForeColor),
                // テキスト表示位置
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center
            };
            // キャンバス上の位置設定
            Canvas.SetTop(label, canvasTop);
            Canvas.SetLeft(label, canvasLeft);

            return label;
        }




        // 選択位置取得？
        // 選択データ取得
    }
}
