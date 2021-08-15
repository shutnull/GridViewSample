using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GridViewSample.UserCtrl
{
    /// <summary>
    /// MargableHeader.xaml の相互作用ロジック
    /// </summary>
    public partial class MargableHeader : UserControl
    {
        [Browsable(true)]
        [Description("ヘッダー背景色")]
        public Color BackColor { get; set; } = Color.FromArgb(0xFF, 0x01, 0x25, 0x97);
        [Browsable(true)]
        [Description("ヘッダー前景色")]
        public Color ForeColor { get; set; } = Colors.White;
        [Browsable(true)]
        [Description("ボーダー色")]
        public Color BorderColor { get; set; } = Color.FromRgb(0x80, 0x80, 0x80);

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
        /// <returns>表示の高さ</returns>
        public double Update()
        {
            // ヘッダーの設定がされていない場合処理しない
            if (HeaderConfigList.Count <= 0)
            {
                // 描画無しのため高さ「0」を返す
                return 0;
            }

            // スクロールしない固定領域のサイズを取得・設定する
            double wSize = HeaderConfigList.Select(list => list.Items.Take(FrozenCount).Sum(item => item?.ColumnSize ?? 0)).Max();
            FrozenSize.Width = new GridLength(wSize, GridUnitType.Pixel);
            // スクロール領域のサイズを取得・設定する
            CanvasNoFrozen.Width = HeaderConfigList.Select(list => list.Items.Skip(FrozenCount).Sum(item => item?.ColumnSize ?? 0)).Max();

            // 垂直方向のサイズ合計値を列毎に保持しておく
            double[] topSizeCnt = new double[HeaderConfigList[0].Items.Length];

            // ヘッダー行数分、ループ
            foreach (MergeableHeaderConfig headerConfig in HeaderConfigList)
            {
                // 列数カウント
                int count = 0;
                // 水平方向のサイズを保持しておく
                double leftSizeCnt = 0;

                // ヘッダー列数分、ループ
                foreach (MergeableHeaderConfigItem items in headerConfig.Items)
                {
                    // アイテムが未設定のとき
                    if (items == null)
                    {
                        count++;
                        continue;
                    }

                    // 固定列のとき
                    if (count < FrozenCount)
                    {
                        // ラベルを取得
                        Label label = InitializeLabel(items.RowSize, items.ColumnSize, topSizeCnt[count], leftSizeCnt, items.Text);
                        // 固定列用キャンバスに追加
                        CanvasFrozen.Children.Add(label);
                    }
                    // スクロール対象列のとき
                    else
                    {
                        // ラベルを取得
                        Label label = InitializeLabel(items.RowSize, items.ColumnSize, topSizeCnt[count], leftSizeCnt - wSize, items.Text);
                        // スクロール対処用キャンバスに追加
                        CanvasNoFrozen.Children.Add(label);
                    }
                    // 垂直方向のサイズをカウント
                    topSizeCnt[count] += items.RowSize;
                    // 水平方向のサイズをカウント
                    leftSizeCnt += items.ColumnSize;
                    // 列数カウント
                    count++;
                }
            }

            // 垂直方向のサイズ最大値をこのコントロールの高さに設定
            Height = topSizeCnt.Max();
            // 高さを返す
            return Height;
        }

        /// <summary>
        /// ヘッダー領域に表示するラベルを取得する
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
                BorderBrush = new SolidColorBrush(BorderColor),
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

        public void ScrollToHorizontal(double offset)
        {
            HScrollBar.ScrollToHorizontalOffset(offset);
        }
    }
}
