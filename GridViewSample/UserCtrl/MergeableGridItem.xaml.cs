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

                    double gcColSize = GridBoxConfig.ColumnSize[col];

                    // 固定列のとき
                    if (col < FrozenCount)
                    {
                        // コントロールを取得
                        UIElement ctr = InitializeControl(col, GridBoxConfig.RowSize * mergeRowCount, gcColSize, lastSumRow[col], colSize, (string)dataRow[col]);
                        // 固定列用キャンバスに追加
                        CanvasFrozen.Children.Add(ctr);
                    }
                    // スクロール対象列のとき
                    else
                    {
                        // コントロールを取得
                        UIElement ctr = InitializeControl(col, GridBoxConfig.RowSize * mergeRowCount, gcColSize, lastSumRow[col], colSize - FrozenWidth, (string)dataRow[col]);
                        // スクロール対処用キャンバスに追加
                        CanvasNoFrozen.Children.Add(ctr);
                    }
                    lastSumRow[col] += mergeRowCount * GridBoxConfig.RowSize;
                    colSize += gcColSize;
                }
            }
        }

        /// <summary>
        /// コントロールを取得する
        /// </summary>
        /// <param name="colIndex">処理対象の列</param>
        /// <param name="height">高さ</param>
        /// <param name="width">幅</param>
        /// <param name="canvasTop">キャンバス上の位置(Y軸方向左上)</param>
        /// <param name="canvasLeft">キャンバス上の位置(X軸方向左上)</param>
        /// <param name="text">表示する文字列</param>
        /// <returns></returns>
        private UIElement InitializeControl(int colIndex, double height, double width, double canvasTop, double canvasLeft, string text)
        {
            UIElement ctr;

            if (GridBoxConfig.DispChekBoxColIndex.Where(index => index == colIndex).Count() > 0)
            {
                ctr = new Border
                {
                    // 高さ
                    Height = height,
                    // 幅
                    Width = width,
                    // ボーダーの設定
                    BorderThickness = new Thickness(1),
                    BorderBrush = new SolidColorBrush(Color.FromRgb(0x80, 0x80, 0x80)),
                    // 背景色
                    Background = new SolidColorBrush(BackColor),
                    // 表示位置
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                };
                CheckBox cb = new CheckBox
                {
                    // ボーダーのClickイベントでチェック状態を入れ替える為、ヒット判定を不可にしておく
                    IsHitTestVisible = false,
                    // 表示位置
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    // チェックボックスサイズ調整
                    LayoutTransform = new ScaleTransform()
                    {
                        ScaleX = 1.1,
                        ScaleY = 1.1
                    }
                };
                ((Border)ctr).Child = cb;
            }
            else
            {
                ctr = new Label
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
            }

            ctr.MouseEnter += Label_MouseEnter;
            ctr.MouseLeave += Label_MouseLeave;
            ctr.PreviewMouseLeftButtonDown += UIElement_PreviewMouseLeftButtonDown;

            // キャンバス上の位置設定
            Canvas.SetTop(ctr, canvasTop);
            Canvas.SetLeft(ctr, canvasLeft);

            return ctr;
        }

        /// <summary>
        /// クリックイベントをハンドルします。
        /// </summary>
        public event EventHandler<RoutedEventArgs> Click;
        private void UIElement_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // クリックされたコントロールがボーダーの時
            if (sender.GetType() == typeof(Border))
            {
                // チェックボックスのチェックを入れ替える
                Border ctr = (Border)sender;
                CheckBox cb = ((CheckBox)ctr.Child);
                cb.IsChecked = !cb.IsChecked;
            }
            Click?.Invoke(this, e);
        }


        private void Label_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender.GetType() == typeof(Label))
            {
                Label ctr = (Label)sender;
                ctr.Background = new SolidColorBrush(BackColor);
            }
            else
            {
                Border ctr = (Border)sender;
                ctr.Background = new SolidColorBrush(BackColor);
            }
        }

        private void Label_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender.GetType() == typeof(Label))
            {
                Label ctr = (Label)sender;
                ctr.Background = new SolidColorBrush(Color.FromRgb(0xE5, 0xF1, 0xFB));
            }
            else
            {
                Border ctr = (Border)sender;
                ctr.Background = new SolidColorBrush(Color.FromRgb(0xE5, 0xF1, 0xFB));
            }
        }

        public void ScrollToHorizontal(double offset)
        {
            HVScrollBar.ScrollToHorizontalOffset(offset);
        }



    }
}
