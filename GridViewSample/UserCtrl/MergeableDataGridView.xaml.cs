using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GridViewSample.UserCtrl
{
    /// <summary>
    /// MargableDataGridView.xaml の相互作用ロジック
    /// </summary>
    public partial class MargableDataGridView : UserControl
    {
        private Color _BackColor = Color.FromArgb(0xFF, 0x32, 0x32, 0x32);
        [Browsable(true)]
        [Description("背景色")]
        public Color BackColor
        {
            get => _BackColor;
            set
            {
                _BackColor = value;
                SolidColorBrush colorBrush = new SolidColorBrush(_BackColor);
                Background = colorBrush;
                Header.Background = colorBrush;
                GridBox.Background = colorBrush;
            }
        }
        private Color _BorderColor = Color.FromArgb(0xFF, 0x32, 0x32, 0x32);
        [Browsable(true)]
        [Description("ボーダー色")]
        public Color BorderColor
        {
            get => _BorderColor;
            set
            {
                _BorderColor = value;
                BorderBrush = new SolidColorBrush(_BorderColor);
            }
        }

        [Browsable(true)]
        [Description("アイテムのボーダー色")]
        public Color ItemsBorderColor { get; set; } = Color.FromRgb(0x80, 0x80, 0x80);

        [Browsable(true)]
        [Description("ヘッダー背景色")]
        public Color HeaderBackColor { get; set; } = Color.FromArgb(0xFF, 0x01, 0x25, 0x97);
        [Browsable(true)]
        [Description("ヘッダー前景色")]
        public Color HeaderForeColor { get; set; } = Colors.White;

        [Browsable(true)]
        [Description("グリッドボックス背景色")]
        public Color GridBoxBackColor { get; set; } = Colors.White;
        [Browsable(true)]
        [Description("グリッドボックス前景色")]
        public Color GridBoxForeColor { get; set; } = Colors.Black;
        [Browsable(true)]
        [Description("グリッドボックス選択時背景色")]
        public Color GridBoxSelectingBackColor { get; set; } = Colors.DodgerBlue;
        [Browsable(true)]
        [Description("グリッドボックス選択時前景色")]
        public Color GridBoxSelectingForeColor { get; set; } = Colors.White;

        [Browsable(true)]
        [Description("スクロールしない固定行数")]
        public int FrozenCount { get; set; } = 0;

        [Browsable(true)]
        [Description("ヘッダーの設定情報リスト")]
        public List<MergeableHeaderConfig> HeaderConfigList { get; set; } = new List<MergeableHeaderConfig>(0);

        [Browsable(true)]
        [Description("グリッドボックス設定情報")]
        public MergebleGridBoxConfig GridBoxConfig { get; set; } = new MergebleGridBoxConfig();

        //[Browsable(true)]
        //[Description("データソースのデータと列を紐づける 未設定で順番に出力する")]
        //public string[] = Array.Empty<string>();

        [Browsable(true)]
        [Description("データソース")]
        public DataTable DataSource { get; set; } = new DataTable();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MargableDataGridView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 表示更新
        /// </summary>
        public void Update()
        {
            // ヘッダーの設定
            Header.BackColor = HeaderBackColor;
            Header.ForeColor = HeaderForeColor;
            Header.BorderColor = ItemsBorderColor;
            Header.FrozenCount = FrozenCount;
            Header.HeaderConfigList = HeaderConfigList;
            // ヘッダーを描画して高さを取得する
            double hSize = Header.Update();
            HeaderRowSize.Height = new GridLength(hSize, GridUnitType.Pixel);

            // グリッドボックスの設定
            GridBox.BackColor = GridBoxBackColor;
            GridBox.ForeColor = GridBoxForeColor;
            GridBox.BorderColor = ItemsBorderColor;
            GridBox.SelectingBackColor = GridBoxSelectingBackColor;
            GridBox.SelectingForeColor = GridBoxSelectingForeColor;
            GridBox.FrozenCount = FrozenCount;
            GridBox.GridBoxConfig = GridBoxConfig;
            GridBox.DataSource = DataSource;
            // グリッドボックスを描画する
            GridBox.Update();

            // 高さ等の調整
            DmyControl.Height = GridBoxConfig.RowSize * DataSource.Rows.Count + hSize;
            DmyControl.Width = HeaderConfigList.Select(list => list.Items.Sum(item => item?.ColumnSize ?? 0)).Max();
            // スクロールバー分のサイズ調整
            Header.Margin = new Thickness(0, 0, DmyControl.Height > ActualHeight ? 17 : 0, 0);
            GridBox.Margin = new Thickness(0, 0, DmyControl.Height > ActualHeight ? 17 : 0, DmyControl.Width > ActualWidth ? 17 : 0);
        }

        /// <summary>
        /// スクロール連動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HVScrollBar_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            Header.ScrollToHorizontal(e.HorizontalOffset);
            GridBox.ScrollToHorizontal(e.HorizontalOffset);
            GridBox.ScrollToVertical(e.VerticalOffset);
        }

        private void Grid_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            HVScrollBar.ScrollToVerticalOffset(-e.Delta);
        }
    }


    #region コンフィグクラス - ヘッダー
    public class MergeableHeaderConfig
    {
        /// <summary>
        /// ヘッダーアイテム設定
        /// </summary>
        public MergeableHeaderConfigItem[] Items = Array.Empty<MergeableHeaderConfigItem>();
    }

    public class MergeableHeaderConfigItem
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="text">ヘッダーに表示するテキスト</param>
        /// <param name="columnSize">列サイズ</param>
        /// <param name="rowSize">行サイズ</param>
        public MergeableHeaderConfigItem(string text, double columnSize, double rowSize)
        {
            Text = text;
            ColumnSize = columnSize;
            RowSize = rowSize;
        }

        /// <summary>
        /// ヘッダーに表示するテキスト
        /// </summary>
        public string Text = string.Empty;

        /// <summary>
        /// 列サイズ
        /// </summary>
        public double ColumnSize = double.NaN;

        /// <summary>
        /// 行サイズ
        /// </summary>
        public double RowSize = double.NaN;
    }
    #endregion

    #region コンフィグクラス - グリッドボックス
    public class MergebleGridBoxConfig
    {
        public MergebleGridBoxConfig()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="rowSize">行サイズ</param>
        /// <param name="columnSize">列サイズ</param>
        /// <param name="mergeVertical">セルの結合（垂直方向）</param>
        /// <param name="mergeColCount">セル結合を有効にする最後の列のインデックスを設定する</param>
        /// <param name="dispChekBoxColIndex">チェックボックス表示にする列インデックスを指定する</param>
        public MergebleGridBoxConfig(double rowSize, double[] columnSize, bool mergeVertical = true, int mergeColCount = -1, int[] dispChekBoxColIndex = null)
        {
            RowSize = rowSize;
            ColumnSize = columnSize;
            MergeVertical = mergeVertical;
            MergeColCount = mergeColCount;
            DispChekBoxColIndex = dispChekBoxColIndex;
        }

        /// <summary>
        /// 行サイズ
        /// </summary>
        public double RowSize = double.NaN;

        /// <summary>
        /// 列サイズ
        /// </summary>
        public double[] ColumnSize = Array.Empty<double>();

        /// <summary>
        /// セルの結合（垂直方向）
        /// 下方向に結合するか
        /// </summary>
        public bool MergeVertical = true;

        /// <summary>
        /// セル結合を有効にする最後の列のインデックスを設定する
        /// 制限しない場合「-1」を設定
        /// </summary>
        public int MergeColCount = -1;

        /// <summary>
        /// チェックボックス表示にする列インデックスを指定する
        /// </summary>
        public int[] DispChekBoxColIndex = null;
    }
    #endregion
}
