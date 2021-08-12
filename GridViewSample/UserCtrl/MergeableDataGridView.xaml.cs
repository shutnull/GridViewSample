using GridViewSample.Common;
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
        private Color _BackColor = Color.FromArgb(0xFF, 0x80, 0x80, 0x80);
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
        [Description("スクロールしない固定行数")]
        public int FrozenCount { get; set; } = 0;

        [Browsable(true)]
        [Description("ヘッダーの設定情報リスト")]
        public List<MergeableHeaderConfig> HeaderConfigList { get; set; } = new List<MergeableHeaderConfig>(0);

        [Browsable(true)]
        [Description("グリッドボックス設定情報")]
        public MergebleGridBoxConfig GridBoxConfig { get; set; } = new MergebleGridBoxConfig();

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
            Header.FrozenCount = FrozenCount;
            Header.HeaderConfigList = HeaderConfigList;
            // ヘッダーを描画して高さを取得する
            double hSize = Header.Update();
            HeaderRowSize.Height = new GridLength(hSize, GridUnitType.Pixel);

            // グリッドボックスの設定
            GridBox.BackColor = GridBoxBackColor;
            GridBox.ForeColor = GridBoxForeColor;
            GridBox.FrozenCount = FrozenCount;
            GridBox.GridBoxConfig = GridBoxConfig;
            GridBox.DataSource = DataSource;
            // グリッドボックスを描画する
            GridBox.Update();

            DmyControl.Height = GridBoxConfig.RowSize * DataSource.Rows.Count + hSize;
            DmyControl.Width = HeaderConfigList.Select(list => list.Items.Sum(item => item?.ColumnSize ?? 0)).Max();

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
}
