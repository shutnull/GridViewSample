using GridViewSample.Common;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GridViewSample.UserCtrl
{
    /// <summary>
    /// MergeableGridItem.xaml の相互作用ロジック
    /// </summary>
    public partial class MergeableGridItem : UserControl
    {
        [Browsable(true)]
        [Description("スクロールしない固定領域のサイズ")]
        public double FrozenWidth { get; set; } = 200;
        [Browsable(true)]
        [Description("スクロール領域のサイズ")]
        public double NoFrozenWidth { get; set; } = 200;

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
        public DataRow[] DataSource { get; set; } = Array.Empty<DataRow>();

        public MergeableGridItem()
        {
            InitializeComponent();
        }

        public void Update()
        {
            FrozenSize.Width = new GridLength(FrozenWidth, GridUnitType.Pixel);
            CanvasNoFrozen.Width = NoFrozenWidth;

            int rowMax = DataSource.Length;
            int colMax = GridBoxConfig.ColumnSize.Length;
            double hSize = rowMax * GridBoxConfig.RowSize;
            double[] lastSumRow = new double[colMax];

            for (int row = 0; row < rowMax; row++)
            {
                double colSize = 0;

                for (int col = 0; col < colMax; col++)
                {
                    DataRow dataRow = DataSource[row];
                    bool mCCntFlag = GridBoxConfig.MergeColCount == -1 || GridBoxConfig.MergeColCount >= col;

                    int mergeRowCount = 1;

                    // 素直方向に結合するか
                    if (GridBoxConfig.MergeVertical && mCCntFlag && (row + 1) < DataSource.Length)
                    {
                        foreach (DataRow dRow in DataSource.Skip(row + 1))
                        {
                            if ((string)dRow[col] != (string)dataRow[col])
                            {
                                break;
                            }

                            // 値が同じ行をカウント
                            mergeRowCount++;
                        }
                    }


                    int mergeColCount = 1;
                    double mergeColSize = GridBoxConfig.ColumnSize[col];

                    // 水平方向に結合するか
                    if (GridBoxConfig.MergeHorizontal && mCCntFlag && (col + 1) < dataRow.ItemArray.Length)
                    {
                        foreach (string dCol in dataRow.ItemArray.Skip(col + 1))
                        {
                            if (dCol != (string)dataRow[col])
                            {
                                break;
                            }

                            // 値が同じ列をカウント
                            mergeColSize += GridBoxConfig.ColumnSize[col + mergeColCount];
                            mergeColCount++;
                        }
                    }

                    // 固定列のとき
                    if (col < FrozenCount)
                    {
                        // ラベルを取得
                        Label label = InitializeLabel(GridBoxConfig.RowSize * mergeRowCount, mergeColSize, lastSumRow[col], colSize, (string)dataRow[col]);
                        // 固定列用キャンバスに追加
                        CanvasFrozen.Children.Add(label);
                    }
                    // スクロール対象列のとき
                    else
                    {
                        // ラベルを取得
                        Label label = InitializeLabel(GridBoxConfig.RowSize * mergeRowCount, mergeColSize, lastSumRow[col], colSize - FrozenWidth, (string)dataRow[col]);
                        // スクロール対処用キャンバスに追加
                        CanvasNoFrozen.Children.Add(label);
                    }
                    lastSumRow[col] += mergeRowCount * GridBoxConfig.RowSize;
                    col += mergeColCount - 1;
                    colSize += mergeColSize;


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

            label.MouseEnter += Label_MouseEnter;
            label.MouseLeave += Label_MouseLeave;
            label.PreviewMouseLeftButtonDown += Label_PreviewMouseLeftButtonDown;

            // キャンバス上の位置設定
            Canvas.SetTop(label, canvasTop);
            Canvas.SetLeft(label, canvasLeft);

            return label;
        }

        /// <summary>
        /// クリックイベントをハンドルします。
        /// </summary>
        public event EventHandler<RoutedEventArgs> Click;
        private void Label_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Click?.Invoke(this, e);
        }


        private void Label_MouseLeave(object sender, MouseEventArgs e)
        {
            Label label = (Label)sender;
            label.Background = new SolidColorBrush(BackColor);
        }

        private void Label_MouseEnter(object sender, MouseEventArgs e)
        {
            Label label = (Label)sender;
            label.Background = new SolidColorBrush(Color.FromRgb(0xE5, 0xF1, 0xFB));
        }

        public void ScrollToHorizontal(double offset)
        {
            HVScrollBar.ScrollToHorizontalOffset(offset);
        }



    }
}
